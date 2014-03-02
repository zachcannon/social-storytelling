using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Data
{
    public class EntryData
    {
        public int id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime SubmissionDate { get; set; }
        public virtual StoryData Story { get; set; }

        public EntryData() { }

        public EntryData(int idNumber, string text, string author)
        {
            this.id = idNumber;
            this.Text = text;
            this.Author = author;
            this.SubmissionDate = DateTime.Now;
        }
    }
}
