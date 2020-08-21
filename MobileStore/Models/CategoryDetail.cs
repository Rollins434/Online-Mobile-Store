using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class CategoryDetail
    {
        [Required]
       public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name Required")]
        [StringLength(100,ErrorMessage ="Min length 3",MinimumLength =3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}