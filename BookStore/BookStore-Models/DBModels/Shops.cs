using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.DBModels
{
    public class Shops
    {
        [Key]
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string GeoCoordinates { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
