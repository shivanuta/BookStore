using BookStore_Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Requests
{
    public class CartItems
    {
        public BooksDetailsResponse BooksDetailsResponse { get; set; }
        public int Quantity { get; set; }
    }
}
