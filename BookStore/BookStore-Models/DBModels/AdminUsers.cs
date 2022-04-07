using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore_Models.DBModels
{
    public class AdminUsers
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        [Required, DefaultValue(true)]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        [ForeignKey("Shops")]
        public int ShopId { get; set; }
        public Shops Shops { get; set; }
    }
}
