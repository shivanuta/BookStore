using BookStore_Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Requests
{
    public class StockRequest
    {
        public int Id { get; set; }

        [Display(Name = "Total Available Stock")]
        public int TotalStock { get; set; }

        [Display(Name = "Amount Per Unit (in USD)")]
        public decimal AmountPerBook { get; set; }

        [Display(Name = "Discount Percentage")]
        public int DiscountPercentage { get; set; }

        public AutoListResponse AutoListResponse { get; set; }

        public List<AutoListResponse>? booksList { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
