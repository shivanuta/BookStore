using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.DBModels
{
    public class Books
    {
        public Books()
        {
            this.IsActive = true;
            this.CreatedDate = System.DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter book name")]
        [Display(Name = "Book Name")]
        [StringLength(100)]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Please enter book title")]
        [Display(Name = "Book Title")]
        [StringLength(100)]
        public string BookTitle { get; set; }

        [Display(Name = "Book Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter publisher")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Please enter published")]
        public string Published { get; set; }

        [Required(ErrorMessage = "Please choose book image")]
        public string BookImage { get; set; }


        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Categories Categories { get; set; }

        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
