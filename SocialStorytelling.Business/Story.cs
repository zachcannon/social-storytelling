﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Business
{
    public class Story
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public List<Entry> StoryEntries { get; set; }

        public Story(int storyId, string storyTitle, string prompt)
        {   
            this.Id = storyId;
            this.Title = storyTitle;
            this.Prompt = prompt;
        }

        public int GetNumberOfStoryEntries()
        {
            return StoryEntries.Count();
        }

        public void AddEntry(Entry newEntry)
        {
            StoryEntries.Add(newEntry);
        }
    }
}
