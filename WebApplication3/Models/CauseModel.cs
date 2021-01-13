using System;
using System.Collections.Generic;


namespace WebApplication3.Models
{
    public class CauseModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String orgInvolved { get; set; }
        public String groups { get; set; }
        public String description { get; set; }
        public String causetype { get; set; }
        public virtual List<Member> PeopleSigned { get; set; }
    }
}