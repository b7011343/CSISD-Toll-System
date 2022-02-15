using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Tolling_System.Models
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Uses decimal instead of double, owing to floating point
        /// not being able to accurately represent the base 10 multiples
        /// that we use for money.
        ///
        /// References:
        ///     https://stackoverflow.com/a/3730040
        ///     https://docs.microsoft.com/en-us/dotnet/api/system.decimal?view=net-6.0#remarks
        /// </summary>
        [Required]
        public decimal Fee { get; set; }

        [Required, StringLength(450)]
        public string UserId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public Invoice(long id, decimal fee, string userid, int vehicleid)
        {
            this.Id = id;
            this.Fee = fee;
            this.UserId = userid;
            this.VehicleId = vehicleid;
        }
    }
}
