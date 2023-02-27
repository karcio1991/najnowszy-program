using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalny_program_managementSystem.Model
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Model;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Products1 
    {
        [Key]
        public int Id { get; set; }


        public string ProductName { get; set; }

        

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int NumberOfProducts { get; set; }
    

        public Products1()
        {
           
        }

        public Categories1 Categories { get; set; }
        // public int CategoriesId { get; set; }
        public int CategoriesId { get; set; }

        public Orders1 Orders { get; set; }
        public int OrderId { get; set; }

        //dobrze

     

    }

}
