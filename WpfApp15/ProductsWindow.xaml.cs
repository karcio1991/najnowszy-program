using finalny_program_managementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp15.AddClasses;

namespace WpfApp15
{
    /// <summary>
    /// Logika interakcji dla klasy ProductsWindow.xaml
    /// </summary>
    /// 



    public partial class ProductsWindow : Window
    {
        ObservableCollection<Products> ListOfProducts = new ObservableCollection<Products>();
        ObservableCollection<Products> ListOfProducts2 = new ObservableCollection<Products>();
        public ProductsWindow()
        {
            InitializeComponent();
            //ustaw pierwsza opcje dla comboboxa
            ChooseOptionForComboBox();


            DisplayDataGrid();
            DisplayComboBox();


        }

        public void ChooseOptionForComboBox()
        {

            cbCategorii.SelectedValue = "All Records";

        }


        public void DisplayDataGrid()
        {




            //dlaczeho to porzebuje konstruktora bez parametrowego ???


            ListOfProducts2.Clear();
            /*

            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Category";

            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            DataGridTextColumn col4 = new DataGridTextColumn();
            DataGridTextColumn col5 = new DataGridTextColumn();

            col1.Header = "ID";
            col2.Header = "CategoriesName";
            col3.Header = "ProductName";
            col4.Header = "Description";
            col5.Header = "Price";

            gridProductsList.Columns.Add(col1);
            gridProductsList.Columns.Add(col2);
            gridProductsList.Columns.Add(col3);
            gridProductsList.Columns.Add(col4);
            gridProductsList.Columns.Add(col5);

            col1.Binding = new Binding("Id");
            col2.Binding = new Binding("CategoriesName");
            col3.Binding = new Binding("ProductName");
            col4.Binding = new Binding("Description");
            col5.Binding = new Binding("Price");
            */

            
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    ListOfProducts2.Add(new Products { Id = item.Id, CategoriesName = item.CategoriesName, ProductName = item.ProductName, Description = item.Description, Price = item.Price, NumberOfProducts = item.NumberOfProducts });
                }
                gridProductsList.ItemsSource = ListOfProducts2;
            }





        }


        public void AktualizacjaGrida()
        {
            ListOfProducts2.Clear();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    ListOfProducts2.Add(new Products { Id = item.Id, CategoriesName = item.CategoriesName, ProductName = item.ProductName, Description = item.Description, Price = item.Price, NumberOfProducts = item.NumberOfProducts });
                }
                gridProductsList.ItemsSource = ListOfProducts2;
            }
        }
        public void DisplayComboBox()
        {
            List<string> listOfComboBox = new List<string>();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Categories)
                {
                    listOfComboBox.Add(item.CategoryName);
                }
                listOfComboBox.Add("All Records");

                cbCategorii.ItemsSource = listOfComboBox;
                cbCategorii.SelectedValue = "All Records";
            }
        }

        private void SliderNumberOfProducts_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbNumberOfProducts.Text = "Ilosc produktow(" + ((int)SliderNumberOfProducts.Value).ToString() + ")";
        }

        private void gridProductsList_Selected(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbProductName.Text))
            {
                MessageBox.Show("Write a ProductName.");
                return;
            }


            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {


                Products products = new Products();

                //jesli jest taka kategoria to nie dodawaj nowej
                bool z = bazaDanychOkazjaEntities.Categories.Any(x => x.CategoryName == tbCategories.Text);
                if (z)
                {
                    products.Categories = bazaDanychOkazjaEntities.Categories.First(c => c.CategoryName == tbCategories.Text);
                }
                else
                {
                    Categories categories = new Categories()
                    {
                        CategoryName = tbCategories.Text,
                        Products = new List<Products>(),
                        ProductsId = bazaDanychOkazjaEntities.Products.Count() + 1,

                    };

                    MessageBox.Show(categories.CategoryName);
                    products.Categories = categories;
                    //bazaDanychOkazjaEntities.Categories.Add(products.Categories);
                }





                products.ProductName = tbProductName.Text;
                products.Description = tbProductDesc.Text;

                products.Price = decimal.Parse(tbProductPrice.Text);

                products.Orders = null; //laczenie nie dopuszcza wartosci zero z tej strony

                products.NumberOfProducts = (int)SliderNumberOfProducts.Value;
                bazaDanychOkazjaEntities.Products.Add(products);
                bazaDanychOkazjaEntities.SaveChanges();



                // DisplayDataGrid();
                // DisplayComboBox();


                //AktualizacjaGrida();
                AktualizacjaGrida();
                DisplayComboBox();

            }
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Products prod1 = (gridProductsList.SelectedItem as Products);
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                Products prod2 = bazaDanychOkazjaEntities.Products.ToList().Where(x => x.Id == prod1.Id).First();
                bazaDanychOkazjaEntities.Products.Remove(prod2);
                bazaDanychOkazjaEntities.SaveChanges();
            }

            //sprawdz czy usunac kategorie? 

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                Categories toRemove = null;

                //to tylko Ci usunie jeden rekord z zerem ostatni
                int length = bazaDanychOkazjaEntities.Categories.Count();
                do
                {
                    foreach (var item in bazaDanychOkazjaEntities.Categories)
                    {
                        if (item.ProductsCount == 0)
                            toRemove = item;
                    }

                    if (!(toRemove == null))
                    {
                        bazaDanychOkazjaEntities.Categories.ToList().Remove(toRemove);
                        bazaDanychOkazjaEntities.SaveChanges();
                    }
                } while (length-- > 0);


            }






            AktualizacjaGrida();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbProductDesc.Text = "";
            tbProductName.Text = "";
            tbProductPrice.Text = "";
            tbCategories.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int prod = (gridProductsList.SelectedItem as Products).Id;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                //dorob edita!

                bazaDanychOkazjaEntities.Products.ToList()[prod].Price = decimal.Parse(tbProductPrice.Text);
                bazaDanychOkazjaEntities.Products.ToList()[prod].ProductName = tbProductName.Text;
                bazaDanychOkazjaEntities.Products.ToList()[prod].Description = tbProductDesc.Text;
                bazaDanychOkazjaEntities.Products.ToList()[prod].CategoriesName = tbCategories.Text;
                bazaDanychOkazjaEntities.SaveChanges();
            }

        }


        private void cbCategorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = cbCategorii.SelectedValue.ToString();
            if (selectedItem == null) return;

            List<Products> listOfProducts = new List<Products>();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    if (item.CategoriesName == selectedItem)
                    {
                        listOfProducts.Add(new Products { Id = item.Id, CategoriesName = item.CategoriesName, ProductName = item.ProductName, Description = item.Description,Price = item.Price,NumberOfProducts=item.NumberOfProducts });

                    }
                    else if (selectedItem.ToString() == "All Records")
                    {
                        listOfProducts.Clear();
                        foreach (var item2 in bazaDanychOkazjaEntities.Products)
                        {
                            listOfProducts.Add(new Products { Id = item2.Id, CategoriesName = item2.CategoriesName, ProductName = item2.ProductName, Description = item2.Description, Price = item2.Price, NumberOfProducts = item2.NumberOfProducts });
                        }
                        break;
                    }
                }
            }
            gridProductsList.ItemsSource = listOfProducts;
        }
    }
}
