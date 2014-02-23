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

        public List<Story> RemoveStoryFromBook(int idToRemove)
        {
            ApplicationContext data = new ApplicationContext();
            data.RemoveStory(idToRemove);

            List<StoryData> storyData = data.GetStories();
            List<Story> storyBook = new List<Story>();
            foreach (StoryData story in storyData)
            {
                storyBook.Add(new Story(story.id, story.Title, story.Prompt));
            }

            return storyBook;
        }
    }
}
