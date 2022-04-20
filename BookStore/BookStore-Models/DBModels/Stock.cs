using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.DBModels
{
    public class Stock
    {
        public Stock()
        {
            this.IsActive = true;
            this.CreatedDate = System.DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Books")]
        public int BookId { get; set; }
        public Books Books { get; set; }

        [Required(ErrorMessage = "Please enter stock")]
        [Display(Name = "Current Stock")]
        public int TotalStock { get; set; }

        public Nullable<int> AvailableStock { get; set; }

        [Required(ErrorMessage = "Please enter Cost Per Book")]
        [Display(Name = "Book Cost")]
        public decimal AmountPerBook { get; set; }

        public int DiscountPercentage { get; set; }
        public Nullable<decimal> DeliveryCharges { get; set; }

        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
