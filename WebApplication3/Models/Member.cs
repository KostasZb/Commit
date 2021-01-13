using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Member
    {

        public int id { get; set; }
        [Required]
        public String lastName { get; set; }
        [Required]
        public String firstName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public String email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String password { get; set; }
        public Boolean admin { get; set; }
        public virtual List<CauseModel> CausesCommitedTo { get; set; }
    }
}