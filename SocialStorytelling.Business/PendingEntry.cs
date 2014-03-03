using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Business
{
    public class PendingEntry
    {
        public int IdNumber { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int StoryId { get; set; }
        public DateTime SubmissionDate { get; set; }

        public PendingEntry(string text, string author, int idNumber, int storyId, DateTime submissionDate)
        {
            this.IdNumber = idNumber;
            this.Text = text;
            this.Author = author;
            this.StoryId = storyId;
            this.SubmissionDate = submissionDate;
        }
        
    }
}
