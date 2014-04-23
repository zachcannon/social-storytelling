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

        /// <summary>
        /// Use the following command to set SocialStorytell as the current user. 
        /// TwitterCredentials.SetCredentials(SocialStorytellAccessToken, SocialStorytellAccessVerifier, ConsumerKey, ConsumerSecret);
        /// </summary>
        private string SocialStoriezAccessToken = "2372951286-8u4NNyblRpippMnlzsbCjf7fcGy6nZqPT0oAQfL";
        private string SocialStoriesAccessVerifier = "ooWWJsM2Ge1uVeh4dIHxUUj03GbVVr9B0SfXbBb7ysdmI";

        private ApplicationContext data = new ApplicationContext();

        public List<Story> GetStoryBook()
        {            

            List<StoryData> storyData = data.GetStories();

            List<Story> storyBook = new List<Story>();
            foreach (StoryData story in storyData)
            {
                storyBook.Add(new Story(story.id, story.Title, story.Prompt, story.StoryClosed));
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
                entryList.Add(new PendingEntry(entry.Text, entry.Author, entry.id, entry.StoryIBelongTo, entry.SubmissionDate, entry.VotesCastForMeFromSite+entry.VotesCastForMeFromTwitter));
            }

            return entryList;
        }

        public List<PendingEntry> GetPendingEntryListForGivenStory(int storyId)
        {
            List<PendingEntryData> pendingEntryData = data.GetPendingEntries();

            List<PendingEntry> entryList = new List<PendingEntry>();
            foreach (PendingEntryData entry in pendingEntryData)
            {
                if (entry.StoryIBelongTo == storyId)
                    entryList.Add(new PendingEntry(entry.Text, entry.Author, entry.id, entry.StoryIBelongTo, entry.SubmissionDate, entry.VotesCastForMeFromSite+entry.VotesCastForMeFromTwitter));
            }

            return entryList;
        }

        public string GetSpecificStoryStatus(int storyId)
        {
            List<StoryData> stories = data.GetStories();
            foreach(StoryData story in stories)
            {
                if (story.id == storyId)
                {
                    if (story.StoryClosed)
                        return "Closed!";
                    else
                        return "Open!";
                }
            }
            return "Story not Found";
        }

        public string GetSpecificStoryTitle(int storyId)
        {
            List<StoryData> stories = data.GetStories();
            foreach (StoryData story in stories)
            {
                if (story.id == storyId)
                {
                    return story.Title;
                }
            }
            return "Story not Found";
        }

        public string GetSpecificStoryPrompt(int storyIdToView)
        {
            List<StoryData> stories = data.GetStories();
            foreach (StoryData story in stories)
            {
                if (story.id == storyIdToView)
                {
                    return story.Prompt;
                }
            }
            return "Story not Found";
        }

        public bool IsStoryInBook(int storyId)
        {
            List<StoryData> stories = data.GetStories();
            foreach (StoryData story in stories)
            {
                if (story.id == storyId)
                {
                    return true;
                }
            }
            return false;
        }

        //----------------------USER COMMANDS---------------------
        
        public void AddPendingEntryToList(int storyId, string author, string text, string access_token, string access_verifier)
        {
            Censor censor = new Censor();
            author = censor.CensorText(author);
            text = censor.CensorText(text);

            if(data.AddPendingEntryToDb(text, author, storyId))
            {
                TwitterCredentials.SetCredentials(access_token, access_verifier, ConsumerKey, ConsumerSecret);
                Tweet.PublishTweet("I just posted a new pending entry: " + text + " to Story " + storyId + " on Social Storytelling!");
            }
        }

        public void VoteForPendingEntry(int idToVoteFor, string userWhoIsVoting, string access_token, string access_verifier)
        {
            if (data.CastVoteForStoryFromSite(idToVoteFor, userWhoIsVoting))
            {
                //TwitterCredentials.SetCredentials(access_token, access_verifier, ConsumerKey, ConsumerSecret);
                //Tweet.PublishTweet("I just voted for pending entry number: " + idToVoteFor + " on Social Storytelling!");
            }
        }
        
        //---------------------ADMIN COMMANDS - SITE -------------------

        public void AddNewStoryToBook(string title, string prompt)
        {
            Censor censor = new Censor();
            title = censor.CensorText(title);
            prompt = censor.CensorText(prompt);

            if (data.AddStoryToDb(new StoryData(1, title, prompt)))
            {
                List<Story> allStories = GetStoryBook();

                foreach(Story story in allStories)
                {
                    if (story.Prompt.Equals(prompt))
                    {
                        TwitterCredentials.SetCredentials(SocialStoriezAccessToken, SocialStoriesAccessVerifier, ConsumerKey, ConsumerSecret);
                        Tweet.PublishTweet("Story #" + story.Id + " - Title: " + story.Title + " - Prompt: " + story.Prompt);
                        Tweet.PublishTweet("Entry #" + 1 + " of Story #" + story.Id + ": " + story.Prompt);
                        break;
                    }
                }                
            }
        }
        
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
        
        //---------------------ADMIN COMMANDS - Twitter -------------------

        public void CheckForNewPendingEntriesOnTwitter(int idToCheck)
        {

            int numberOfEntriesInStory = data.GetNumberOfEntriesInStory(idToCheck);
            string pendingEntryIDString = "Entry #" + numberOfEntriesInStory + " of Story #" + idToCheck;

            TwitterCredentials.SetCredentials(SocialStoriezAccessToken, SocialStoriesAccessVerifier, ConsumerKey, ConsumerSecret);            
            var timeline = Timeline.GetHomeTimeline();

            foreach(var tweet in timeline)
            {
                if(tweet.Text.Contains(pendingEntryIDString))
                {
                    var replies = Search.SearchDirectRepliesTo(tweet);

                    foreach (var reply in replies)
                    {
                        CheckIfPendingEntryExistsAndAddIt(idToCheck, reply.Creator.ScreenName, reply.Text);
                        UpdateTwitterVotesForPendingEntry(idToCheck, reply.Text, reply.RetweetCount);
                    }

                    break;
                }
            }

        }

        private void UpdateTwitterVotesForPendingEntry(int storyId, string pendingEntryText, int twitterVotes)
        {
            List<PendingEntryData> pendingEntries = data.GetPendingEntriesForStoryFromDb(storyId);
            foreach(PendingEntryData entry in pendingEntries)
            {
                if (pendingEntryText.Contains(entry.Text))
                {
                    data.UpdateVotesFromTwitter(entry.id, twitterVotes);
                    break;
                }
            }
        }

        private void CheckIfPendingEntryExistsAndAddIt(int storyId, string author, string text)
        {
            if (!text.Contains("Submission: "))
                return;

            int stringParse = text.IndexOf("Submission: ");
            text = text.Substring(stringParse + 12);

            List<PendingEntryData> pendingEntries = data.GetPendingEntriesForStoryFromDb(storyId);

            bool exists = false;
            foreach (PendingEntryData entry in pendingEntries)
            {
                if (entry.Text.Contains(text))
                    exists = true;
            }

            if (!exists)
                data.AddPendingEntryToDb(text, author, storyId);

        }

        public void PromoteMostPopularPendingEntryAndTweet(int idToPromote)
        {
            List<PendingEntryData> pendingEntriesForStory = data.GetPendingEntriesForStoryFromDb(idToPromote);

            if (pendingEntriesForStory.Count() < 1)
                return;

            int pendingIdToPromote = -1;
            int highestCount = 0;
            PendingEntryData entryToPromote = pendingEntriesForStory.First();

            foreach (PendingEntryData entry in pendingEntriesForStory)
            {
                if ((entry.VotesCastForMeFromSite+entry.VotesCastForMeFromTwitter) >= highestCount)
                {
                    highestCount = entry.VotesCastForMeFromSite+entry.VotesCastForMeFromTwitter;
                    pendingIdToPromote = entry.id;
                    entryToPromote = entry;
                }

            }

            PromotePendingEntryFromList(pendingIdToPromote);
            TweetAboutPromotedPendingEntry(entryToPromote);


            foreach (PendingEntryData entry in pendingEntriesForStory)
            {
                if (entry.id != pendingIdToPromote)
                    RemovePendingEntryFromList(entry.id);
            }

        }

        private void TweetAboutPromotedPendingEntry(PendingEntryData entryToPromote)
        {
            int entryNumber = data.GetNumberOfEntriesInStory(entryToPromote.StoryIBelongTo);
            int storyNumber = entryToPromote.StoryIBelongTo;
            
            TwitterCredentials.SetCredentials(SocialStoriezAccessToken, SocialStoriesAccessVerifier, ConsumerKey, ConsumerSecret);
            Tweet.PublishTweet("Entry #" + entryNumber + " of Story #" + storyNumber + ": " + entryToPromote.Text);

            string previousEntryPost = "Entry #" + (entryNumber-1) + " of Story #" + storyNumber;

            TwitterCredentials.SetCredentials(SocialStoriezAccessToken, SocialStoriesAccessVerifier, ConsumerKey, ConsumerSecret);
            var timeline = Timeline.GetHomeTimeline();

            foreach (var tweet in timeline)
            {
                if (tweet.Text.Contains(previousEntryPost))
                {
                    tweet.PublishReply("This entry is now closed! Go to - Entry #" + entryNumber + " of Story - " + storyNumber);
                    break;
                }
            }
        }
        
        private int ParseStoryIdFromTweet(string text)
        {
            if (!text.Substring(0, 5).Equals("Story"))
                return -1;

            int startChar = 7;
            int length = 0;
            
            for(int i=startChar; i<15; i++)
            {
                if(text[i].CompareTo(' ') == 0)
                {
                    length = i;
                    break;
                }
            }

            string substring = text.Substring(startChar, length-startChar);

            return Convert.ToInt32(substring);
        } 
    }
}
