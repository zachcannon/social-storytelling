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
            data.AddStoryToDB(new StoryData(1, "TestStory1", "TestPrompt1"));
            data.AddStoryToDB(new StoryData(2, "TestStory2", "TestPrompt2"));
            data.AddStoryToDB(new StoryData(3, "TestStory3", "TestPrompt3"));
            data.AddStoryToDB(new StoryData(4, "TestStory4", "TestPrompt4"));

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
