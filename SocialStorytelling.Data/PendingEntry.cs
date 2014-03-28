using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Data
{
    public class PendingEntryData
    {
        public int id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int StoryIBelongTo { get; set; }
        public int VotesCastForMe { get; set; }
        public string Voters { get; set; }

        public PendingEntryData() { }

        public PendingEntryData(int idNumber, string text, string author, int storyId)
        {
            this.id = idNumber;
            this.Text = text;
            this.Author = author;
            this.SubmissionDate = DateTime.Now;
            this.StoryIBelongTo = storyId;
            this.VotesCastForMe = 0;
            this.Voters = "";
        }
    }
}
