using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SocialStorytelling.Data;

namespace SocialStorytelling.Business
{
    public class ControllerBusiness
    {
        public ControllerBusiness() {}

        public List<Story> GetStoryBook()
        {
            ApplicationContext data = new ApplicationContext();

            List<StoryData> storyData = data.GetStories();

            List<Story> storyBook = new List<Story>();
            foreach (StoryData story in storyData)
            {
                storyBook.Add(new Story(story.id, story.Title, story.Prompt));
            }

            return storyBook;
        }

        public List<Entry> GetEntryList()
        {
            ApplicationContext data = new ApplicationContext();

            List<EntryData> entryData = data.GetEntries();

            List<Entry> entryList = new List<Entry>();
            foreach (EntryData entry in entryData)
            {
                entryList.Add(new Entry(entry.Text, entry.Author, entry.id, entry.SubmissionDate));
            }

            return entryList;
        }

        public List<Entry> GetEntriesForGivenStory(int storyId)
        {
            ApplicationContext data = new ApplicationContext();
            List<EntryData> entryData = data.GetEntriesForStoryFromDb(storyId);

            List<Entry> entryList = new List<Entry>();
            foreach (EntryData entry in entryData)
            {
                entryList.Add(new Entry(entry.Text, entry.Author, entry.id, entry.SubmissionDate));
            }

            return entryList;
        }

        public List<PendingEntry> GetPendingEntryList()
        {
            ApplicationContext data = new ApplicationContext();

            List<PendingEntryData> pendingEntryData = data.GetPendingEntries();

            List<PendingEntry> entryList = new List<PendingEntry>();
            foreach (PendingEntryData entry in pendingEntryData)
            {
                entryList.Add(new PendingEntry(entry.Text, entry.Author, entry.id, entry.StoryIBelongTo, entry.SubmissionDate));
            }

            return entryList;
        }

        public void AddNewStoryToBook(string title, string prompt)
        {
            Censor censor = new Censor();
            title = censor.CensorText(title);
            prompt = censor.CensorText(prompt);

            ApplicationContext data = new ApplicationContext();
            data.AddStoryToDb(new StoryData(1, title, prompt));
        }

        public void AddEntryToStory(int storyId, string author, string text)
        {
            Censor censor = new Censor();
            author = censor.CensorText(author);
            text = censor.CensorText(text);

            ApplicationContext data = new ApplicationContext();
            data.AddEntryToDb(1, text, author, storyId);
        }

        public void AddPendingEntryToList(int storyId, string author, string text)
        {
            Censor censor = new Censor();
            author = censor.CensorText(author);
            text = censor.CensorText(text);

            ApplicationContext data = new ApplicationContext();
            data.AddPendingEntryToDb(1, text, author, storyId);
        }

        public void RemoveStoryFromBook(int idToRemove)
        {
            ApplicationContext data = new ApplicationContext();
            data.RemoveStory(idToRemove);
        }

        public void RemoveEntryFromList(int idToRemove)
        {
            ApplicationContext data = new ApplicationContext();
            data.RemoveEntry(idToRemove);
        }

        public void RemovePendingEntryFromList(int idToRemove)
        {
            ApplicationContext data = new ApplicationContext();
            data.RemovePendingEntry(idToRemove);
        }

        public void PromotePendingEntryFromList(int idToPromote)
        {
            ApplicationContext data = new ApplicationContext();

            PendingEntryData pendingEntryToPromote = data.GetPendingEntryById(idToPromote);

            AddEntryToStory(pendingEntryToPromote.StoryIBelongTo, pendingEntryToPromote.Author, pendingEntryToPromote.Text);
            data.RemovePendingEntry(pendingEntryToPromote.id);
        }
    }
}
