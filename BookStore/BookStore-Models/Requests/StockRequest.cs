using BookStore_Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Requests
{
    public class StockRequest
    {
        public int Id { get; set; }

        public int TotalStock { get; set; }

        public AutoListResponse AutoListResponse { get; set; }

        public List<AutoListResponse> booksList { get; set; }

    }
}
