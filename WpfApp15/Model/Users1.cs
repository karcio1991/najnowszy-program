using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalny_program_managementSystem.Model
{
    public class Users1
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
       
        public string PhoneNumber { get; set; }

        public Users1()
        {

        }

        public ICollection<Orders1> Orders { get; set; }

        // public int OrdersId { get; set; }

    }
}
