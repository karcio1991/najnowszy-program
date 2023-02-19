using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalny_program_managementSystem.Model
{
    public class Customers1 
    {
        [Key]
        public int Id { get; set; }

        
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        public DateTime LastOrder { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int OrdersAmountValue { get; set; }

        public int OrdersCount { get; set; }

        public Customers1()
        {

        }


        public int OrdersId { get; set; }

        public ICollection<Products1> Products { get; set; }

    }
}
