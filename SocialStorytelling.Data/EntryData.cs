using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Data
{
    public class EntryData
    {
        public int IdNumber { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime SubmissionDate { get; set; }

        public EntryData(int idNumber, string text, string author, int storyContainerId)
        {
            this.IdNumber = idNumber;
            this.Text = text;
            this.Author = author;
            this.SubmissionDate = DateTime.Now;
        }
    }
}
