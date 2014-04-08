using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SocialStorytelling.Data;
using Tweetinvi;

namespace SocialStorytelling.Business
{
    public class SocialStorytellingService
    {
        private int MaximumNumberOfEntriesInAStory = 5;
        private string ConsumerKey = "qNLcTNRZVCYvzktylhw";
        private string ConsumerSecret = "6mbLcXOiaZT2kMMjdqxQ2CTrSsdbkJvpcGKrduoBxk";
        private ApplicationContext data = new ApplicationContext();

        public List<Story> GetStoryBook()
        {            

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
            List<PendingEntryData> pendingEntryData = data.GetPendingEntries();

            List<PendingEntry> entryList = new List<PendingEntry>();
            foreach (PendingEntryData entry in pendingEntryData)
            {
                entryList.Add(new PendingEntry(entry.Text, entry.Author, entry.id, entry.StoryIBelongTo, entry.SubmissionDate));
            }

            return entryList;
        }

        //----------------------USER COMMANDS---------------------

        public void AddNewStoryToBook(string title, string prompt, string access_token, string access_verifier)
        {
            Censor censor = new Censor();
            title = censor.CensorText(title);
            prompt = censor.CensorText(prompt);

            if(data.AddStoryToDb(new StoryData(1, title, prompt)))
            {
                TwitterCredentials.SetCredentials(access_token, access_verifier, ConsumerKey, ConsumerSecret);
                Tweet.PublishTweet("I just posted a new story titled: " + title + " on Social Storytelling!");
            }

            
        }

        public void AddPendingEntryToList(int storyId, string author, string text, string access_token, string access_verifier)
        {
            Censor censor = new Censor();
            author = censor.CensorText(author);
            text = censor.CensorText(text);

            if(data.AddPendingEntryToDb(text, author, storyId))
            {
                TwitterCredentials.SetCredentials(access_token, access_verifier, ConsumerKey, ConsumerSecret);
                Tweet.PublishTweet("I just posted a new pending entry: " + text + " on Social Storytelling!");
            }
        }

        public void VoteForPendingEntry(int idToVoteFor, string userWhoIsVoting, string access_token, string access_verifier)
        {
            if (data.CastVoteForStoryFromUser(idToVoteFor, userWhoIsVoting))
            {
                TwitterCredentials.SetCredentials(access_token, access_verifier, ConsumerKey, ConsumerSecret);
                Tweet.PublishTweet("I just voted for pending entry number: " + idToVoteFor + " on Social Storytelling!");
            }
        }
        
        //---------------------ADMIN COMMANDS-------------------

        public void AddEntryToStory(int storyId, string author, string text)
        {
            Censor censor = new Censor();
            author = censor.CensorText(author);
            text = censor.CensorText(text);

            int numberOfEntriesInStory = data.AddEntryToDb(1, text, author, storyId);
            if (numberOfEntriesInStory >= MaximumNumberOfEntriesInAStory)
            {
                data.CloseStory(storyId);
            }
        }
        
        public void RemoveStoryFromBook(int idToRemove)
        {
            data.RemoveStory(idToRemove);
        }

        public void RemoveEntryFromList(int idToRemove)
        {
            data.RemoveEntry(idToRemove);
        }

        public void RemovePendingEntryFromList(int idToRemove)
        {
            data.RemovePendingEntry(idToRemove);
        }

        public void PromotePendingEntryFromList(int idToPromote)
        {
            PendingEntryData pendingEntryToPromote = data.GetPendingEntryById(idToPromote);

            AddEntryToStory(pendingEntryToPromote.StoryIBelongTo, pendingEntryToPromote.Author, pendingEntryToPromote.Text);
            data.RemovePendingEntry(pendingEntryToPromote.id);
        }

        public void CloseAStory(int storyIdToClose)
        {
            data.CloseStory(storyIdToClose);
        }

        public void PromoteMostPopularPendingEntry(int idToPromote)
        {
            List<PendingEntryData> pendingEntriesForStory = data.GetPendingEntriesForStoryFromDb(idToPromote);

            int pendingIdToPromote = -1;
            int highestCount = 0;

            foreach(PendingEntryData entry in pendingEntriesForStory)
            {
                if (entry.VotesCastForMe >= highestCount)
                {
                    highestCount = entry.VotesCastForMe;
                    pendingIdToPromote = entry.id;
                }
                    
            }

            PromotePendingEntryFromList(pendingIdToPromote);

            foreach (PendingEntryData entry in pendingEntriesForStory)
            {
                if (entry.id != pendingIdToPromote)
                    RemovePendingEntryFromList(entry.id);
            }

        }
    }
}
