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
        public DbSet<PendingEntryData> PendingEntries { get; set; }

        public List<StoryData> GetStories()
        {
            using (var db = new ApplicationContext())
            {
                List<StoryData> stories = db.Stories.ToList();
                return stories;
            }
        }

        public List<EntryData> GetEntries()
        {
            using (var db = new ApplicationContext())
            {
                List<EntryData> entries = db.Entries.ToList();
                return entries;
            }
        }

        public List<PendingEntryData> GetPendingEntries()
        {
            using (var db = new ApplicationContext())
            {
                List<PendingEntryData> pendingEntries = db.PendingEntries.ToList();
                return pendingEntries;
            }
        }

        public List<EntryData> GetEntriesForStoryFromDb(int storyId)
        {
            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(storyId);
                return story.Entries;
            }
        }

        public List<PendingEntryData> GetPendingEntriesForStoryFromDb(int storyId)
        {
            List<PendingEntryData> pendingEntriesForStory = new List<PendingEntryData>();

            using (var db = new ApplicationContext())
            {
                var listOfPendingEntries = db.PendingEntries.ToList();
                
                foreach(PendingEntryData pending in listOfPendingEntries)
                {
                    if (pending.StoryIBelongTo == storyId)
                        pendingEntriesForStory.Add(pending);
                }

            }

            return pendingEntriesForStory;
        }

        public PendingEntryData GetPendingEntryById(int pendingEntryId)
        {
            using (var db = new ApplicationContext())
            {
                var entry = db.PendingEntries.Find(pendingEntryId);
                return entry;
            }
        }

        public bool AddStoryToDb(StoryData newStory)
        {
            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(newStory.id);

                if (story == null)
                {
                    db.Stories.Add(newStory);
                    db.SaveChanges();

                    this.AddEntryToDb(1, newStory.Prompt, "SocialStoriez", newStory.id);

                    return true;
                }

                return false;
            }
        }

        public int AddEntryToDb(int entryId, string text, string author, int storyContainerId)
        {
            EntryData entry = new EntryData(entryId, text, author);

            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(storyContainerId);

                if (!story.StoryClosed)
                {
                    entry.Story = story;
                    story.Entries.Add(entry);

                    db.Entries.Add(entry);

                    db.SaveChanges();
                    return story.Entries.Count();
                }
                return 0;
            }
        }

        public bool AddPendingEntryToDb(string text, string author, int storyId)
        {
            PendingEntryData entry = new PendingEntryData(1, text, author, storyId);

            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(storyId);

                if (story != null && !story.StoryClosed)
                {
                    db.PendingEntries.Add(entry);
                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public void RemoveStory(int idToRemove)
        {
            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(idToRemove);

                var entriesFromStory = this.GetEntriesForStoryFromDb(idToRemove);
                var pendingEntriesFromStory = this.GetPendingEntriesForStoryFromDb(idToRemove);

                if (story != null)
                {
                    foreach (var entry in entriesFromStory)
                    {
                        this.RemoveEntry(entry.id);
                    }

                    foreach (var pendingEntry in pendingEntriesFromStory)
                    {
                        this.RemovePendingEntry(pendingEntry.id);
                    }

                    db.Stories.Remove(story);

                    db.SaveChanges();
                }
            }
        }

        public void RemoveEntry(int idToRemove)
        {
            using (var db = new ApplicationContext())
            {
                var entry = db.Entries.Find(idToRemove);

                if (entry != null)
                {
                    var storyForEntry = entry.Story;
                    storyForEntry.Entries.Remove(entry);

                    db.Entries.Remove(entry);

                    db.SaveChanges();
                }
            }
        }

        public void RemovePendingEntry(int idToRemove)
        {
            using (var db = new ApplicationContext())
            {
                var entry = db.PendingEntries.Find(idToRemove);

                if(entry != null)
                {
                    db.PendingEntries.Remove(entry);
                    db.SaveChanges();
                }
            }
        }

        public bool CastVoteForStoryFromUser(int idToVoteFor, string userWhoIsVoting)
        {
            using (var db = new ApplicationContext())
            {
                var pendingEntry = db.PendingEntries.Find(idToVoteFor);
                
                if (!pendingEntry.Voters.Contains(userWhoIsVoting))
                {
                    pendingEntry.Voters = pendingEntry.Voters+userWhoIsVoting;
                    pendingEntry.VotesCastForMe++;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void CloseStory(int storyIdToClose)
        {
            using (var db = new ApplicationContext())
            {
                var storyToClose = db.Stories.Find(storyIdToClose);
                storyToClose.StoryClosed = true;
                db.SaveChanges();
            }
        }

        public int GetNumberOfEntriesInStory(int storyId)
        {
            using (var db = new ApplicationContext())
            {
                var story = db.Stories.Find(storyId);
                return story.Entries.Count();
            }
        }
    }
}
