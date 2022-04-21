using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Responses
{
    public class BooksDetailsResponse
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string BookImage { get; set; }
        public string PublishedDate { get; set; }
        public string Author { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
