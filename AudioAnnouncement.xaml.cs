<?xml version="1.0" encoding="utf-8"?>
<Page
    x:Class="VHT_CMS_V1.AudioAnnouncement"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:VHT_CMS_V1"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    RequestedTheme="Dark"
    Background="{ThemeResource SystemControlBackgroundAltHighBrush}">

    <Grid Padding="32" HorizontalAlignment="Center" MaxWidth="1400">
        

            <!-- MAIN PAGE CONTENT -->
            <ScrollViewer Margin="0,50,0,0">

                <Grid HorizontalAlignment="Center" MaxWidth="1500">

                    <Border
        CornerRadius="16"
        Padding="32"
        Background="{ThemeResource SystemControlBackgroundAltHighBrush}"
        BorderBrush="{ThemeResource SystemControlForegroundBaseLowBrush}"
        BorderThickness="1"
        UseLayoutRounding="True">

                        <!-- Two-column layout: form (left) | history table (right) -->
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="1*" />
                                <ColumnDefinition Width="1.6*" />
                            </Grid.ColumnDefinitions>
                            <Grid.ColumnSpacing>32</Grid.ColumnSpacing>

                            <!-- LEFT: Form -->
                            <StackPanel Grid.Column="0" Spacing="24">

                                <!-- Header -->
                                <Grid Margin="0,0,0,8">
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="*" />
                                        <ColumnDefinition Width="Auto" />
                                    </Grid.ColumnDefinitions>

                                    <StackPanel Grid.Column="0" VerticalAlignment="Center">
                                        <TextBlock Text="Audio Announcement"
                                   FontSize="24"
                                   FontWeight="SemiBold"/>
                                        <TextBlock Text="Manage audio messages quickly and securely"
                                   Opacity="0.7"
                                   FontSize="14"
                                   Margin="0,4,0,0"/>
                                    </StackPanel>


                                <!-- ADD / REPLACE inside header Grid (Grid.Column="1") -->
                                <Button x:Name="BtnToggleHistory"
                                       Content="Show History"
                                        Click="BtnToggleHistory_Click"
                                         Grid.Column="1"
                                          HorizontalAlignment="Right"
                                           VerticalAlignment="Center"
                                            Height="36"
                                            Padding="16,8"
                                             CornerRadius="8"
                                             Background="Black"
                                             Foreground="White"
                                             FontWeight="SemiBold"
                                             BorderThickness="0"/>


                            </Grid>

                                <!-- Mode radio buttons -->
                                <StackPanel Orientation="Horizontal" Spacing="20" VerticalAlignment="Center">
                                    <TextBlock Text="Mode:" VerticalAlignment="Center" FontWeight="SemiBold" FontSize="14"/>
                                    <RadioButton x:Name="PredefinedRadio"
                                 Content="Predefined"
                                 IsChecked="True"
                                 GroupName="ModeGroup"
                                 Checked="ModeRadio_Checked"
                                 FontSize="13"/>
                                    <RadioButton x:Name="CustomRadio"
                                 Content="Custom"
                                 GroupName="ModeGroup"
                                 Checked="ModeRadio_Checked"
                                 FontSize="13"/>
                                </StackPanel>

                                <!-- Form grid -->
                                <Grid>
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="1*" />
                                        <ColumnDefinition Width="1*" />
                                    </Grid.ColumnDefinitions>
                                    <Grid.RowDefinitions>
                                        <RowDefinition Height="Auto" />
                                        <RowDefinition Height="Auto" />
                                        <RowDefinition Height="Auto" />
                                    </Grid.RowDefinitions>
                                    <Grid.ColumnSpacing>24</Grid.ColumnSpacing>
                                    <Grid.RowSpacing>16</Grid.RowSpacing>

                                    <!-- LEFT COLUMN -->
                                    <StackPanel Grid.Column="0" Grid.Row="0" Grid.RowSpan="3" Spacing="16">
                                        <!-- Username -->
                                        <StackPanel Spacing="8">
                                            <TextBlock Text="Username" FontSize="13" FontWeight="SemiBold" Opacity="0.9"/>
                                            <TextBox x:Name="UsernameTextBox" 
                                     IsEnabled="False" 
                                     Height="40"
                                     CornerRadius="8"
                                     FontSize="13"/>
                                        </StackPanel>

                                        <!-- Volume -->
                                        <StackPanel Spacing="8">
                                            <TextBlock Text="Volume" FontSize="13" FontWeight="SemiBold" Opacity="0.9"/>
                                            <StackPanel Orientation="Horizontal" VerticalAlignment="Center" Spacing="12">
                                                <Slider
                                    x:Name="VolumeSlider"
                                    Minimum="0"
                                    Maximum="20"
                                    Value="10"
                                    StepFrequency="1"
                                    TickFrequency="1"
                                    Width="165"
                                    Height="32"/>
                                                <Border Padding="7,5"
                                        CornerRadius="6"
                                        Background="{ThemeResource SystemControlBackgroundBaseLowBrush}"
                                        VerticalAlignment="Center"
                                        BorderThickness="1"
                                        BorderBrush="{ThemeResource SystemControlForegroundBaseLowBrush}">
                                                    <TextBlock
                                        FontSize="13"
                                        FontWeight="SemiBold"
                                        Foreground="{ThemeResource SystemControlForegroundBaseHighBrush}"
                                        Text="{Binding Value, ElementName=VolumeSlider, Mode=OneWay}" />
                                                </Border>
                                            </StackPanel>
                                        </StackPanel>

                                        <!-- Submit Button BELOW Volume -->
                                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right" Spacing="12" Margin="5,12,45,0">
                                            <Button x:Name="SubmitButton" 
                                    Content="Submit Announcement" 
                                    Click="SubmitButton_Click" 
                                    Width="170"
                                    Height="44"
                                    CornerRadius="10"
                                    FontSize="14"
                                    FontWeight="SemiBold"
                                    Background="{ThemeResource SystemAccentColor}"
                                    Foreground="White"
                                    BorderThickness="0"
                                    Padding="0"/>
                                        </StackPanel>
                                    </StackPanel>

                                    <!-- RIGHT COLUMN -->
                                    <StackPanel Grid.Column="1" Grid.Row="0" Grid.RowSpan="3" Spacing="16">
                                        <StackPanel Spacing="8">
                                            <TextBlock Text="Text" FontSize="13" FontWeight="SemiBold" Opacity="0.9"/>
                                            <TextBox x:Name="TextTextBox"
                                     AcceptsReturn="True"
                                     Height="144"
                                     TextWrapping="Wrap"
                                     CornerRadius="8"
                                     FontSize="13"
                                     IsEnabled="{Binding IsChecked, ElementName=CustomRadio}"
                                     TextChanged="TextTextBox_TextChanged" />
                                        </StackPanel>

                                        <StackPanel Spacing="8">
                                            <TextBlock Text="Predefined descriptions" FontSize="13" FontWeight="SemiBold" Opacity="0.9"/>
                                            <ComboBox x:Name="PredefinedComboBox"
                                      SelectionChanged="PredefinedComboBox_SelectionChanged"
                                      PlaceholderText="Select a predefined description"
                                      Height="40"
                                      CornerRadius="8"
                                      FontSize="13"/>
                                        </StackPanel>
                                    </StackPanel>
                                </Grid>
                            </StackPanel>

                            <!-- RIGHT: Inline history table -->
                            <Border x:Name="HistoryPanel"
                    Grid.Column="1"
                    Visibility="Collapsed"
                    Padding="20"
                    CornerRadius="12"
                    Background="{ThemeResource SystemControlBackgroundChromeMediumLowBrush}"
                    BorderBrush="{ThemeResource SystemControlForegroundBaseLowBrush}"
                    BorderThickness="1">

                                <StackPanel Spacing="16">
                                    <!-- History Header with Refresh Button -->
                                    <Grid VerticalAlignment="Center">
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="*" />
                                            <ColumnDefinition Width="Auto" />
                                        </Grid.ColumnDefinitions>

                                        <StackPanel Grid.Column="0" Orientation="Horizontal" VerticalAlignment="Center" Spacing="8">
                                            <TextBlock Text="History" 
                                       FontWeight="SemiBold" 
                                       FontSize="16" />
                                        </StackPanel>

                                        <Button Content=" Refresh" 
                                Click="RefreshHistoryButton_Click" 
                                Grid.Column="1" 
                                HorizontalAlignment="Right"
                                Height="36"
                                CornerRadius="8"
                                FontSize="13"
                                FontWeight="SemiBold"
                                Padding="16,8"
                                Background="{ThemeResource SystemControlBackgroundListLowBrush}"
                                BorderBrush="{ThemeResource SystemControlForegroundBaseMediumBrush}"
                                BorderThickness="1"
                                Foreground="{ThemeResource SystemControlForegroundBaseHighBrush}"/>
                                    </Grid>

                                    <!-- History Table -->
                                    <Border CornerRadius="8"
                            Background="{ThemeResource SystemControlBackgroundBaseLowBrush}"
                            BorderThickness="1"
                            BorderBrush="{ThemeResource SystemControlForegroundBaseLowBrush}">
                                        <ListView x:Name="HistoryListView" 
                                  MaxHeight="560" 
                                  IsItemClickEnabled="False"
                                  ScrollViewer.HorizontalScrollBarVisibility="Auto"
                                  ScrollViewer.VerticalScrollBarVisibility="Auto">
                                            <ListView.Header>
                                                <Grid Margin="16,12,16,8" Background="Transparent">
                                                    <Grid.ColumnDefinitions>
                                                        <ColumnDefinition Width="160"/>
                                                        <ColumnDefinition Width="90"/>
                                                        <ColumnDefinition Width="160"/>
                                                        <ColumnDefinition Width="100"/>
                                                        <ColumnDefinition Width="80"/>
                                                        <ColumnDefinition Width="*"/>
                                                    </Grid.ColumnDefinitions>
                                                    <TextBlock Grid.Column="3" Text="Created" FontWeight="SemiBold" FontSize="12" Opacity="0.8"/>
                                                    <TextBlock Grid.Column="0" Text="User" FontWeight="SemiBold" FontSize="12" Opacity="0.8"/>
                                                    <TextBlock Grid.Column="1" Text="Volume" FontWeight="SemiBold" FontSize="12" Opacity="0.8"/>
                                                    <TextBlock Grid.Column="2" Text="TextDescription" FontWeight="SemiBold" FontSize="12" Opacity="0.8"/>

                                                </Grid>
                                            </ListView.Header>

                                            <ListView.ItemContainerStyle>
                                                <Style TargetType="ListViewItem">
                                                    <Setter Property="Padding" Value="16,12"/>
                                                    <Setter Property="Background" Value="Transparent"/>
                                                    <Setter Property="BorderThickness" Value="0"/>
                                                    <Setter Property="Margin" Value="0,2"/>
                                                </Style>
                                            </ListView.ItemContainerStyle>

                                            <ListView.ItemTemplate>
                                                <DataTemplate>
                                                    <Grid Margin="0,2,0,2">
                                                        <Grid.ColumnDefinitions>
                                                            <ColumnDefinition Width="160"/>
                                                            <ColumnDefinition Width="90"/>
                                                            <ColumnDefinition Width="160"/>
                                                            <ColumnDefinition Width="100"/>
                                                            <ColumnDefinition Width="80"/>
                                                            <ColumnDefinition Width="*"/>
                                                        </Grid.ColumnDefinitions>


                                                        <TextBlock Grid.Column="0" Text="{Binding UserName}" TextTrimming="CharacterEllipsis" FontSize="12"/>
                                                        <TextBlock Grid.Column="1" Text="{Binding VolumeLevel}" TextTrimming="CharacterEllipsis" FontSize="12"/>
                                                        <TextBlock Grid.Column="2"
                                                   Text="{Binding TextDescription}"
                                                   TextWrapping="Wrap"
                                                   MaxHeight="200"
                                                   TextTrimming="None"
                                                   FontSize="12" />
                                                        <TextBlock Grid.Column="3" Text="{Binding CreatedDate}" TextTrimming="CharacterEllipsis" FontSize="12"/>
                                                    </Grid>
                                                </DataTemplate>
                                            </ListView.ItemTemplate>
                                        </ListView>
                                    </Border>
                                </StackPanel>
                            </Border>
                        </Grid>
                    </Border>
                </Grid>

            </ScrollViewer>

        </Grid>
</Page>

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


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

            // ⭐ ADDED — hide history by default
            HistoryPanel.Visibility = Visibility.Collapsed;
            BtnToggleHistory.Content = "Show History";
        }

        private async void AudioAnnouncement_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = UserSession.CurrentUser?.Username ?? string.Empty;

            try { await LoadPredefinedFromDbAsync(); }
            catch (Exception ex) { Debug.WriteLine($"Failed loading predefined items: {ex}"); }

            await LoadHistoryAndBindAsync();

            // ⭐ ADDED — still hidden after load
            HistoryPanel.Visibility = Visibility.Collapsed;

            ApplyMode(predefined: PredefinedRadio.IsChecked == true);
        }

        private async void ModeRadio_Checked(object sender, RoutedEventArgs e)
        {
            var predefined = PredefinedRadio.IsChecked == true;
            ApplyMode(predefined);

            if (predefined)
            {
                try { await LoadPredefinedFromDbAsync(); }
                catch (Exception ex) { await ShowMessageAsync("Error", ex.Message); }
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
            PredefinedComboBox.ItemsSource = displayList;

            PredefinedComboBox.SelectedIndex = displayList.Count > 0 ? 0 : -1;
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox?.Text.Trim() ?? string.Empty;

            short? messageId = null;
            string selectedOption = "Custom";

            if (PredefinedRadio.IsChecked == true && PredefinedComboBox.SelectedIndex >= 0)
            {
                messageId = _predefinedItems[PredefinedComboBox.SelectedIndex].MessageId;
                selectedOption = "Predefined";
            }

            string text = TextTextBox?.Text.Trim() ?? string.Empty;
            int volumeLevel = (int)Math.Round(VolumeSlider.Value);

            if (string.IsNullOrEmpty(username))
            {
                await ShowMessageAsync("Validation", "Username is required.");
                return;
            }

            var preview = $"Username: {username}\nMode: {selectedOption}\nMessageID: {(messageId.HasValue ? messageId.ToString() : "<none>")}\nVolume: {volumeLevel}\n\nSubmit this entry?";

            var dialog = new ContentDialog
            {
                Title = "Confirm Submit",
                Content = preview,
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            if (await dialog.ShowAsync() != ContentDialogResult.Primary)
                return;

            try
            {
                await InsertRecordAsync(username, messageId, text, volumeLevel, selectedOption);
                await ShowMessageAsync("Success", "Record inserted successfully.");

                await LoadHistoryAndBindAsync(); // Refresh grid

                if (CustomRadio.IsChecked == true)
                {
                    TextTextBox.Text = string.Empty;
                    VolumeSlider.Value = 10;
                }
            }
            catch (Exception ex)
            {
                await ShowMessageAsync("Error", ex.Message);
            }
        }

        private async Task InsertRecordAsync(string username, short? messageId, string text, int volumeLevel, string selectedOption)
        {
            if (string.IsNullOrEmpty(AppConfig.ConnectionString))
                throw new InvalidOperationException("Connection string not configured.");

            const string insertSql = @"
INSERT INTO SAAU_TBHST (UserId, UserName, MessageID, VolumeLevel, TextDescription, SelectedOption)
VALUES (@userid, @username, @messageid, @volume, @text, @option);";

            await using var conn = new SqlConnection(AppConfig.ConnectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(insertSql, conn);

            cmd.Parameters.AddWithValue("@userid", DBNull.Value);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@messageid", messageId ?? 21);
            cmd.Parameters.AddWithValue("@volume", (short)volumeLevel);
            cmd.Parameters.AddWithValue("@text", string.IsNullOrEmpty(text) ? DBNull.Value : text);
            cmd.Parameters.AddWithValue("@option", selectedOption);

            await cmd.ExecuteNonQueryAsync();
        }

        // ⭐ ADDED — TOGGLE HISTORY BUTTON
        private void BtnToggleHistory_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryPanel.Visibility == Visibility.Collapsed)
            {
                HistoryPanel.Visibility = Visibility.Visible;
                BtnToggleHistory.Content = "Hide History";
            }
            else
            {
                HistoryPanel.Visibility = Visibility.Collapsed;
                BtnToggleHistory.Content = "Show History";
            }
        }

        private async void RefreshHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadHistoryAndBindAsync();
        }

        private async Task<List<HistoryItem>> LoadHistoryAsync()
        {
            var result = new List<HistoryItem>();

            const string query = @"
SELECT TOP 100 SequenceNumber, UserId, UserName, MessageID, VolumeLevel, TextDescription, CreatedDate
FROM SAAU_TBHST
ORDER BY CreatedDate DESC, SequenceNumber DESC;";

            await using var conn = new SqlConnection(AppConfig.ConnectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new HistoryItem
                {
                    SequenceNumber = reader.GetInt32(0),
                    UserId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    UserName = reader["UserName"]?.ToString() ?? "",
                    MessageID = reader.IsDBNull(3) ? null : reader.GetInt16(3),
                    VolumeLevel = reader.GetInt16(4),
                    TextDescription = reader["TextDescription"]?.ToString() ?? "",
                    CreatedDate = reader["CreatedDate"]?.ToString() ?? ""
                });
            }

            return result;
        }

        private async Task LoadHistoryAndBindAsync()
        {
            HistoryListView.ItemsSource = await LoadHistoryAsync();
        }

        private async Task ShowMessageAsync(string title, string message)
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
        private void TextTextBox_TextChanged(object sender, Microsoft.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            // Do nothing or add logic here
        }
    }
}

