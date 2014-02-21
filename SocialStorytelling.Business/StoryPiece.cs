using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialStorytelling.Business
{
    public class StoryPiece
    {
        public int IdNumber { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int StoryContainerId { get; set; }

        public StoryPiece(string text, string author, int idNumber, int storyContainerId)
        {
            this.IdNumber = idNumber;
            this.Text = text;
            this.Author = author;
            this.StoryContainerId = storyContainerId;
        }
    }
}
