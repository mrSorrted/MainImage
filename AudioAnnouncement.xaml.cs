using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace VHT_CMS_V1
{
    [SupportedOSPlatform("windows")]
    public sealed partial class AudioAnnouncement : Page
    {
        private readonly List<PredefinedItem> _predefinedItems = new();

        private sealed class PredefinedItem
        {
            public short? MessageId { get; init; }
            public string Text { get; init; } = string.Empty;
            public short VolumeLevel { get; init; }
            public string Display => Text;
        }

        public sealed class HistoryItem
        {
            public int SequenceNumber { get; set; }
            public int? UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public short? MessageID { get; set; }
            public short VolumeLevel { get; set; }
            public string TextDescription { get; set; } = string.Empty;
            public string CreatedDate { get; set; } = string.Empty;
        }

        public AudioAnnouncement()
        {
            this.InitializeComponent();
            this.Loaded += AudioAnnouncement_Loaded;
        }

        private async void AudioAnnouncement_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = UserSession.CurrentUser?.Username ?? string.Empty;

            try
            {
                await LoadPredefinedFromDbAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed loading predefined items: {ex}");
            }

            // ⭐ Auto load history when page opens
            await LoadHistoryAndBindAsync();

            ApplyMode(predefined: PredefinedRadio.IsChecked == true);
        }

        private async void ModeRadio_Checked(object sender, RoutedEventArgs e)
        {
            var predefined = PredefinedRadio.IsChecked == true;
            ApplyMode(predefined: predefined);

            if (predefined)
            {
                try
                {
                    await LoadPredefinedFromDbAsync();
                }
                catch (Exception ex)
                {
                    await ShowMessageAsync("Error", $"Failed to load predefined items: {ex.Message}");
                }
            }
        }

        private void ApplyMode(bool predefined)
        {
            if (PredefinedComboBox != null) PredefinedComboBox.IsEnabled = predefined;
            if (TextTextBox != null) TextTextBox.IsReadOnly = predefined;
        }

        private void PredefinedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PredefinedComboBox == null) return;

            var idx = PredefinedComboBox.SelectedIndex;
            if (idx >= 0 && idx < _predefinedItems.Count)
            {
                var item = _predefinedItems[idx];
                if (TextTextBox != null) TextTextBox.Text = item.Text;

                // Fix ambiguity issue safely
                int vol = item.VolumeLevel;
                if (vol < 0) vol = 0;
                if (vol > 20) vol = 20;
                if (VolumeSlider != null) VolumeSlider.Value = vol;
            }
        }

        private async Task LoadPredefinedFromDbAsync()
        {
            _predefinedItems.Clear();

            if (string.IsNullOrEmpty(AppConfig.ConnectionString))
                throw new InvalidOperationException("Connection string not configured.");

            const string query = @"
SELECT SequenceNumber, MessageID, VolumeLevel, TextDescription
FROM SAAU_TB
ORDER BY SequenceNumber;";

            await using var conn = new SqlConnection(AppConfig.ConnectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 20;
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                short? messageId = reader.IsDBNull(reader.GetOrdinal("MessageID"))
                                  ? null
                                  : reader.GetInt16(reader.GetOrdinal("MessageID"));

                short volume = reader.IsDBNull(reader.GetOrdinal("VolumeLevel"))
                               ? (short)0
                               : reader.GetInt16(reader.GetOrdinal("VolumeLevel"));

                string text = reader["TextDescription"]?.ToString() ?? string.Empty;

                _predefinedItems.Add(new PredefinedItem
                {
                    MessageId = messageId,
                    Text = text,
                    VolumeLevel = volume
                });
            }

            var displayList = _predefinedItems.Select(i => i.Display).ToList();
            if (PredefinedComboBox != null) PredefinedComboBox.ItemsSource = displayList;

            if (displayList.Count > 0)
            {
                if (PredefinedComboBox != null) PredefinedComboBox.SelectedIndex = 0;
            }
            else
            {
                if (PredefinedComboBox != null) PredefinedComboBox.SelectedIndex = -1;
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox?.Text.Trim() ?? string.Empty;

            short? messageId = null;
            string selectedOption = "Custom";

            if (PredefinedRadio.IsChecked == true && PredefinedComboBox != null && PredefinedComboBox.SelectedIndex >= 0)
            {
                messageId = _predefinedItems[PredefinedComboBox.SelectedIndex].MessageId;
                selectedOption = "Predefined";
            }

            string text = TextTextBox?.Text.Trim() ?? string.Empty;
            int volumeLevel = VolumeSlider != null ? (int)Math.Round(VolumeSlider.Value) : 0;

            if (string.IsNullOrEmpty(username))
            {
                await ShowMessageAsync("Validation", "Username is required.");
                return;
            }

            var preview = $"Username: {username}\nMode: {selectedOption}\nMessageID: {(messageId.HasValue ? messageId.ToString() : "<none>")}\nVolume: {volumeLevel}\n\nSubmit this entry?";

            var confirm = new ContentDialog
            {
                Title = "Confirm Submit",
                Content = preview,
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await confirm.ShowAsync();
            if (result != ContentDialogResult.Primary) return;

            try
            {
                await InsertRecordAsync(username, messageId, text, volumeLevel, selectedOption);
                await ShowMessageAsync("Success", "Record inserted successfully.");

                // ⭐ Auto refresh history after insert
                await LoadHistoryAndBindAsync();

                if (CustomRadio.IsChecked == true)
                {
                    if (TextTextBox != null) TextTextBox.Text = string.Empty;
                    if (VolumeSlider != null) VolumeSlider.Value = 10;
                }
            }
            catch (Exception ex)
            {
                await ShowMessageAsync("Error", $"Insert failed: {ex.Message}");
            }
        }

        private async Task InsertRecordAsync(string username, short? messageId, string text, int volumeLevel, string selectedOption)
        {
            if (string.IsNullOrEmpty(AppConfig.ConnectionString))
                throw new InvalidOperationException("Connection string not configured.");

            await using var conn = new SqlConnection(AppConfig.ConnectionString);
            await conn.OpenAsync();

            const string insertSql = @"
INSERT INTO SAAU_TBHST (UserId, UserName, MessageID, VolumeLevel, TextDescription, SelectedOption)
VALUES (@userid, @username, @messageid, @volume, @text, @selectedOption);";

            await using var cmd = new SqlCommand(insertSql, conn);
            cmd.CommandTimeout = 30;

            cmd.Parameters.AddWithValue("@userid", DBNull.Value);
            cmd.Parameters.AddWithValue("@username", username);

            if (messageId.HasValue)
                cmd.Parameters.AddWithValue("@messageid", messageId.Value);
            else
                cmd.Parameters.AddWithValue("@messageid", 21);

            cmd.Parameters.AddWithValue("@volume", (short)volumeLevel);
            cmd.Parameters.AddWithValue("@text", string.IsNullOrEmpty(text) ? (object)DBNull.Value : text);
            cmd.Parameters.AddWithValue("@selectedOption", string.IsNullOrEmpty(selectedOption) ? (object)DBNull.Value : selectedOption);

            await cmd.ExecuteNonQueryAsync();
        }

        private async void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = await LoadHistoryAsync();
                HistoryListView.ItemsSource = items;
                HistoryPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                await ShowMessageAsync("Error", ex.Message);
            }
        }

        private async void RefreshHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = await LoadHistoryAsync();
                HistoryListView.ItemsSource = items;
            }
            catch (Exception ex)
            {
                await ShowMessageAsync("Error", ex.Message);
            }
        }

        private void HideHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryPanel.Visibility = Visibility.Collapsed;
        }

        private async Task<List<HistoryItem>> LoadHistoryAsync()
        {
            var result = new List<HistoryItem>();

            if (string.IsNullOrEmpty(AppConfig.ConnectionString))
                throw new InvalidOperationException("Connection string not configured.");

            string query = @"
SELECT TOP (100) SequenceNumber, UserId, UserName, MessageID, VolumeLevel, TextDescription, CreatedDate
FROM SAAU_TBHST
ORDER BY CreatedDate DESC, SequenceNumber DESC;";

            await using var conn = new SqlConnection(AppConfig.ConnectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var item = new HistoryItem
                {
                    SequenceNumber = reader.GetInt32(reader.GetOrdinal("SequenceNumber")),
                    UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetInt32(reader.GetOrdinal("UserId")),
                    UserName = reader["UserName"]?.ToString() ?? string.Empty,
                    MessageID = reader.IsDBNull(reader.GetOrdinal("MessageID")) ? null : reader.GetInt16(reader.GetOrdinal("MessageID")),
                    VolumeLevel = reader.GetInt16(reader.GetOrdinal("VolumeLevel")),
                    TextDescription = reader["TextDescription"]?.ToString() ?? string.Empty,
                    CreatedDate = reader["CreatedDate"]?.ToString() ?? string.Empty
                };

                result.Add(item);
            }

            return result;
        }

        // ⭐ NEW: Auto–bind history list
        private async Task LoadHistoryAndBindAsync()
        {
            var items = await LoadHistoryAsync();
            HistoryListView.ItemsSource = items;
            HistoryPanel.Visibility = Visibility.Visible;
        }

        private async Task ShowMessageAsync(string title, string message)
        {
            try
            {
                var d = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await d.ShowAsync();
            }
            catch
            {
                Debug.WriteLine($"{title}: {message}");
            }
        }

        private bool EnsureControlsAvailable()
        {
            if (PredefinedComboBox == null)
                PredefinedComboBox = FindName("PredefinedComboBox") as ComboBox;

            if (UsernameTextBox == null)
                UsernameTextBox = FindName("UsernameTextBox") as TextBox;

            if (TextTextBox == null)
                TextTextBox = FindName("TextTextBox") as TextBox;

            if (VolumeSlider == null)
                VolumeSlider = FindName("VolumeSlider") as Slider;

            if (HistoryListView == null)
                HistoryListView = FindName("HistoryListView") as ListView;

            if (HistoryPanel == null)
                HistoryPanel = FindName("HistoryPanel") as Border;

            return PredefinedComboBox != null &&
                   UsernameTextBox != null &&
                   TextTextBox != null &&
                   VolumeSlider != null &&
                   HistoryListView != null &&
                   HistoryPanel != null;
        }

        private void TextTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
