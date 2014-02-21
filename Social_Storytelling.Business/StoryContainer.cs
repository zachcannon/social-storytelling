using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Business
{
    public class StoryContainer
    {
        public string Title { get; set; }
        public List<StoryPiece> StoryPieces { get; set; }
        public int Id { get; set; }
        public string DemographicInfo { get; set; }

        public StoryContainer(string storyTitle, int storyId)
        {
            this.Title = storyTitle;
            this.Id = storyId;
        }

        public int GetNumberOfStoryPieces()
        {
            return StoryPieces.Count();
        }

        public void AddStoryPiece(StoryPiece newPiece)
        {
            StoryPieces.Add(newPiece);
        }
    }
}
