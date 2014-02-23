using System;
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

        public DbSet<StoryData> ListOfStories { get; set; }

        public List<StoryData> GetStories()
        {
            var db = new ApplicationContext();

            List<StoryData> stories = db.ListOfStories.ToList();

            return stories;
        }

        public void AddStoryToDB(StoryData newStory)
        {
            var db = new ApplicationContext();

            var story = db.ListOfStories.Find(newStory.id);

            if (story == null)
            {
                db.ListOfStories.Add(newStory);
                db.SaveChanges();
            }
            else
            {
                db.Entry(story).CurrentValues.SetValues(newStory);
            }
        }
    }
}
