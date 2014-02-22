using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Business
{
    public class StoryBook
    {
        public int Id { get; set;}
        public string Genre { get; set; }
        public List<Story> Stories { get; set; }

        public StoryBook(int id, string genre)
        {
            this.Id = id;
            this.Genre = genre;
        }

        public void AddStory(Story newStory)
        {
            this.Stories.Add(newStory);
        }
    }
}
