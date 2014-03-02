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

        public void RemoveStoryFromBook(int idToRemove)
        {
            ApplicationContext data = new ApplicationContext();
            data.RemoveStory(idToRemove);
        }

        public void AddNewStoryToBook(string title, string prompt)
        {
            ApplicationContext data = new ApplicationContext();
            data.AddStoryToDB(new StoryData(1, title, prompt));
        }

        public void AddEntryToStory(int storyId, string author, string text)
        {
            ApplicationContext data = new ApplicationContext();
            data.AddEntryToDB(1, text, author, storyId);
        }
    }
}
