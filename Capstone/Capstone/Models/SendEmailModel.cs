﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class SendEmailModel
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.EmailAddress), Display(Name = "To")]
        [Required]
        public string ToEmail { get; set; }
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "CC")]
        public string EmailCC { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "BCC")]
        public string EmailBCC { get; set; }
        [Display(Name = "Body")]
        [DataType(DataType.MultilineText)]
        public string EmailBody { get; set; }
    }
}