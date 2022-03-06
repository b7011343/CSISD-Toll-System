using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Tolling_System.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public int Cvv { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string NameOnCard { get; set; }
        [Required]
        public string OwnerID { get; set; }
    }
    public class CardDBContext : DbContext
    {
        public CardDBContext(DbContextOptions<CardDBContext> options) : base(options)
        { }
        public DbSet<Card> Cards { get; set; }
    }
}
