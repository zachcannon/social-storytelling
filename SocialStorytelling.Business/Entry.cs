using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialStorytelling.Business
{
    public class Entry
    {
        public int IdNumber { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime SubmissionDate { get; set; }

        public Entry(string text, string author, int idNumber, DateTime submissionDate)
        {
            this.IdNumber = idNumber;
            this.Text = text;
            this.Author = author;
            this.SubmissionDate = submissionDate;
        }
    }
}
