using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SocialStorytelling.Data
{
    public class UserData
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }

        public UserData() {}

        public UserData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
