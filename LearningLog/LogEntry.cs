/* FileName:        LogEntry.
   Author:          Ritik Sharma
   Date Created:    November 02, 2024
   Description: The LogEntry class represents an individual entry in the Learning Log application. It encapsulates the details of a user's daily learning activity, 
                including audio recordings, mood ratings, and notes.
*/

using System;
using System.IO;

namespace LearningLog
{
    public class LogEntry
    {
        // Attributes of the LogEntry class
        private int Id;
        private DateOnly EntryDate;
        private int Wellness;
        private int Quality;
        private string Notes = string.Empty;
        public FileInfo RecordingFile;

        // Static fields for tracking entries
        private static int Count = 0;
        private static DateOnly FirstEntry;
        private static DateOnly NewestEntry;

        // Properties for instance fields
        public int UserId
        {
            get 
            {
                return Id; 
            }
        }

        public DateOnly UserEntryDate
        {
            get
            {
                return EntryDate; 
            }
        }


        public int UserWellness
        {
            get 
            { 
                return Wellness; 
            }
            set 
            { 
                Wellness = value; 
            }
        }

        public int UserQuality
        {
            get 
            {
                return Quality; 
            }
            set 
            {
                Quality = value;
            }
        }

        public string UserNotes
        {
            get 
            {
                return Notes; 
            }
            set
            {
                Notes = value;
            }
        }

        public FileInfo UserFile
        {
            get
            {
                return RecordingFile;
            }
            set
            { 
                RecordingFile = value;
            }
        }

        // Static read-only properties for summary data
        public static int UserEntries
        {
            get 
            {
                return Count; 
            }
        }

        public static DateOnly UserFirstDate
        {
            get
            {
                return FirstEntry; 
            }
        }

        public static DateOnly UserNewestDate
        {
            get
            {
                return NewestEntry;
            }
        }


        // Default constructor
        public LogEntry()
        {
            Id = ++Count;
            EntryDate = DateOnly.FromDateTime(DateTime.Now);
            UpdateEntryDates();
        }

        // Parameterized constructor
        public LogEntry(int wellness, int quality, string notes, FileInfo recordingFile)
        {
            Id = ++Count;
            EntryDate = DateOnly.FromDateTime(DateTime.Now);
            Wellness = wellness;
            Quality = quality;
            Notes = notes;
            RecordingFile = recordingFile;
            UpdateEntryDates();
        }

        // Method to update static entry dates
        private void UpdateEntryDates()
        {
            if (Count == 1)
            {
                // Assign to the static field directly
                FirstEntry = EntryDate;
            }
            NewestEntry = EntryDate; // Also assign to the field directly
        }
    }
}
