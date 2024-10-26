using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningLog
{
    public class LogEntry
    {
        private int Id;
        private DateOnly EntryDate;
        private int Wellness;
        private int Quality;
        private string Notes = string.Empty;
        public FileInfo RecordingFile;

        private static int Count = 0;
        private static DateOnly FirstEntry;
        private static DateOnly NewestEntry;

        private int UserId
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }

        private DateOnly UserEntryDate
        {
            get
            {
                return EntryDate;
            }
            set
            {
                EntryDate = value;
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

        private FileInfo UserFile
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

        public static int UserEntries
        {
            get
            {
                return Count;
            }
            set
            {
                Count = value;
            }
        }

        public static DateOnly UserFirstDate
        {
            get
            {
                return FirstEntry;
            }
            set
            {
                FirstEntry = value;
            }
        }

        public static DateOnly UserNewestDate
        {
            get
            {
                return NewestEntry;
            }
            set
            {
                NewestEntry = value;
            }
        }

        public LogEntry() 
        { 
            Id = ++Count;
            EntryDate = DateOnly.FromDateTime(DateTime.Now);
            if (Count == 1)
                FirstEntry = EntryDate;
            else
                NewestEntry = EntryDate;
            
        }

        public LogEntry(int Wellness, int Quality, string Notes, FileInfo RecordingFile)
        {
            this.Wellness = Wellness;
            this.Quality = Quality;
            this.Notes = Notes;
            this.RecordingFile = RecordingFile;
        }
    }
}
