/* FileName:        LogEntry.
   Author:          Ritik Sharma
   Date Created:    November 02, 2024
   Description: This is a WPF Learning Log application  which is a tool designed for users to record and track their daily learning activities. 
                It provides a simple interface for creating audio entries, capturing notes, and evaluating daily experiences. Users can view summaries of their entries 
*/

using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Stores the current recording (voice of the users)
        private FileInfo? currentRecording;

        public MainWindow()
        {
            InitializeComponent();

            // Disabling Delete, Play and Save button
            buttonDelete.IsEnabled = false;
            buttonPlay.IsEnabled = false;
            buttonSave.IsEnabled = false;
        }

        /// <summary>
        /// Handle the Record button click, user can record and stop the recording throught this button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRecord_Click(object sender, RoutedEventArgs e)
        {
            // Change button content to Stop and start the recording
            if (labelRecordText.Content.ToString().EndsWith("d"))
            {
                // Start recording
                RecordWav.StartRecording();
                
                // Changing button content
                labelRecordIcon.Content = "\uE15B";
                labelRecordText.Content = "S_top";
                buttonRecord.ToolTip = "Click to stop the audio recording";

                // Enable the button to toggle recording
                buttonRecord.IsEnabled = true; 
                // Updating the status bar
                UpdateStatus("Recording started");
            }
            // Change button content to Record and stop the recording
            else
            {
                // Stop recording
                currentRecording = RecordWav.EndRecording();

                // Changing button content
                labelRecordIcon.Content = "\uE1D6";
                labelRecordText.Content = "_Record";
                buttonRecord.ToolTip = "Click to start the audio recording";

                // Disable the button to toggle recording
                buttonRecord.IsEnabled = false;
                // Enable all other three buttons
                buttonPlay.IsEnabled = buttonDelete.IsEnabled = buttonSave.IsEnabled = true;
                // Updating the status bar
                UpdateStatus("Recording saved");
            }

        }

        /// <summary>
        /// This is the function that will update the status bar
        /// </summary>
        /// <param name="status"></param>
        private void UpdateStatus(string status)
        {
            statusState.Content = $"{status} - {DateTime.Now}";
        }

        /// <summary>
        /// Handle the Play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            // Checks if current recording is not null, there should be something to play
            if (currentRecording != null)
            {
                SoundPlayer player = new SoundPlayer(currentRecording.FullName);
                player.Play();
            }
            UpdateStatus("Playing Recording");
        }

        /// <summary>
        /// Handle the Delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            // Checks if current recording is not null, there should be something to delete
            if (currentRecording != null)
            {
                // Deleting the current recording
                currentRecording.Delete();
                // Updating the status bar
                UpdateStatus("Recording Deleted");

                // Enabling and Disabling the buttons accordingly
                buttonRecord.IsEnabled = true;
                buttonPlay.IsEnabled = false;
                buttonDelete.IsEnabled = false;
            }
        }
        private void textNotes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            
            // If current recording is null
            if (currentRecording == null)
            {
                // then update the status bar
                UpdateStatus("No recording to save");
                return;
            }

            // If Wellness or Quality indexes are not selected
            if (comboWellness.SelectedIndex < 0 || comboQuality.SelectedIndex < 0)
            {
                // then update the status bar
                UpdateStatus("Please select values for wellness and quality");
                return;
            }

            if (string.IsNullOrWhiteSpace(textNotes.Text))
            {
                UpdateStatus("Please enter some notes");
                return;
            }

            try
            {
                // Retrieve values
                int wellness = int.Parse(comboWellness.Text);
                int quality = int.Parse(comboQuality.Text);
                string notes = textNotes.Text;

                // Create new LogEntry
                LogEntry newEntry = new LogEntry(wellness, quality, notes, currentRecording);

                UpdateStatus("Entry saved");

                // Reset form and update summary
                ResetForm();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                UpdateStatus("Failed to save entry: " + ex.Message);
            }
        }

         /// <summary>
         /// This is Reset function which reset the form to it's original form after clicking save button
         /// </summary>
        private void ResetForm()
        {
            buttonRecord.IsEnabled = true;
            buttonPlay.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            buttonSave.IsEnabled = false;
            textNotes.Clear();
            comboWellness.SelectedIndex = 0;
            comboQuality.SelectedIndex = 0;
        }

        /// <summary>
        /// This is the Update Summary function which updates all three values in the summary tab
        /// </summary>
        private void UpdateSummary()
        {
            // Update Total Entries textbox
            textTotalEntries.Text = LogEntry.UserEntries.ToString();
            
            // Update the First Entry and Newest Entry date after checking that Entries are greater than 0, otherwise display No Entries
            textFirstEntry.Text = LogEntry.UserEntries > 0 ? LogEntry.UserFirstDate.ToString() : "No Entries";
            textNewestEntry.Text = LogEntry.UserEntries > 0 ? LogEntry.UserNewestDate.ToString() : "No Entries";
        }

        // Update summary values when the Summary tab is focused
        private void tabSummary_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
            
        }
    }
    
}