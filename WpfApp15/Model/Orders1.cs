using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalny_program_managementSystem.Model
{
    public class Orders1
    {
        public Orders1()
        {
           
        }


        [Key]
        public int Id { get; set; }

       


        public int Quantity { get; set; }

        public decimal UPrice { get; set; } //pojedyncza cena
        public decimal TotPrice { get; set; } //calosc 


        public ICollection<Products1> Products { get; set; }

        public int ProducterId { get; set; }


        public Users1 Users { get; set; }
        public int UserssID { get; set; }
        //public int CustomerId { get; set; } //wazne nazewnictwo

        //public int ProductId { get; set; }


    }
}
