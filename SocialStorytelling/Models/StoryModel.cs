using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialStorytelling.Models
{
    public class StoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }

        public StoryModel(int id, string title, string prompt)
        {
            this.Id = id;
            this.Title = title;
            this.Prompt = prompt;
        }
    }
}