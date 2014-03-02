﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SocialStorytelling.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() {}

        public DbSet<StoryData> Stories { get; set; }
        public DbSet<EntryData> Entries { get; set; }

        public List<StoryData> GetStories()
        {
            using (var db = new ApplicationContext())
            {
                List<StoryData> stories = db.Stories.ToList();
                return stories;
            }
        }

        public void AddStoryToDB(StoryData newStory)
        {
            using (var db = new ApplicationContext())
            {

                var story = db.Stories.Find(newStory.id);

                if (story == null)
                {
                    db.Stories.Add(newStory);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(story).CurrentValues.SetValues(newStory);
                }
            }
        }

        public void RemoveStory(int idToRemove)
        {
            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(idToRemove);

                if (story != null)
                {
                    db.Stories.Remove(story);
                    db.SaveChanges();
                }
            }
        }

        public void AddEntryToDB(int entryId, string text, string author, int storyContainerId)
        {
            EntryData entry = new EntryData(entryId, text, author);

            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(storyContainerId);
                entry.Story = story;
                story.Entries.Add(entry);

                db.Entries.Add(entry);

                db.SaveChanges();
            }
        }
    }
}
