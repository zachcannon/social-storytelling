using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStorytelling.Data
{
    public class StoryData
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public virtual List<EntryData> Entries { get; set; }

        public StoryData() {}

        public StoryData(int id, string title, string prompt)
        {
            this.id = id;
            this.Title = title;
            this.Prompt = prompt;
        }
    }
}
