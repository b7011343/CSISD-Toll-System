﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public int Cvv { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string NameOnCard { get; set; }
    }
}
