//------------------------------------------------------------------------------
// <auto-generated>
//    Ten kod źródłowy został wygenerowany na podstawie szablonu.
//
//    Ręczne modyfikacje tego pliku mogą spowodować nieoczekiwane zachowanie aplikacji.
//    Ręczne modyfikacje tego pliku zostaną zastąpione w przypadku ponownego wygenerowania kodu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp15
{
    using System;
    using System.Collections.Generic;
    using WpfApp15.AddClasses;
    public partial class Categories 
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ProductsId { get; set; }

        public int ProductsCount { get { return Products.Count; } }
        public virtual ICollection<Products> Products { get; set; }
        
        public Categories(string categoryName)
        {
            //Products = new Products();

            this.CategoryName = categoryName;
            
        }
       
        public Categories()
        {

        }
    }
}
