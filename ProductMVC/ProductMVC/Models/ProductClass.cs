using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProductMVC.Models
{
    public class ProductClass
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }

    }
}
