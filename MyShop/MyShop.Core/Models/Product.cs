using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public String Id { get; set; }
        [DisplayName("Product Name")]
        [StringLength(20)]
        public String Name { get; set; }
        public String Dscription { get; set; }
        [Range(0,1000)]
        public decimal Price { get; set; }
        public String Catergary { get; set;}
        public String Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
