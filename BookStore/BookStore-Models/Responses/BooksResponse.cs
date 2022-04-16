using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Responses
{
    public class BooksResponse
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public string BookTitle { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Published { get; set; }

        public string BookImage { get; set; }
        public int CategoryId { get; set; }

        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
