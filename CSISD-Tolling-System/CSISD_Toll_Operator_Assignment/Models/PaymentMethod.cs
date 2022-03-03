﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}