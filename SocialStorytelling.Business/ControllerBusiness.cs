using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Business
{
    public class ControllerBusiness
    {
        public ControllerBusiness() {}

        public List<Story> GetStoryBook()
        {
            List<Story> storybook = new List<Story>();
            storybook.Add(new Story(1, "TestStory1", "TestPrompt1"));
            storybook.Add(new Story(2, "TestStory2", "TestPrompt2"));
            storybook.Add(new Story(3, "TestStory3", "TestPrompt3"));
            storybook.Add(new Story(4, "TestStory4", "TestPrompt4"));
            return storybook;
        }
    }
}
