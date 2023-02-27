using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalny_program_managementSystem.Model
{
    public partial class Categories1
    {

        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public Categories1()
        {

        }

        public ICollection<Products1> Products { get; set; }


        [ForeignKey("Product")]
        public int ProductsId { get; set; }
    }
}
