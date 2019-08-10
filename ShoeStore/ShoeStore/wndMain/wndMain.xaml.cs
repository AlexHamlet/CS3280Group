//created by Jason Hawkins
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShoeStore.Search;
using ShoeStore.Items;

namespace mainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //clsMainLogic mainLogic;

        //declares the search window
        wndSearch SearchWindow;

        //declare the edit window
        wndItems ItemsWindow;


        //this is where the main logic of the window lives
        clsMainLogic MainLogic;

        //this is where all the selected invoices items are stored
        List<clsLineItems> MyList;


        //declares the is full bool value
        bool isFull;



        /// <summary>
        /// initializes all class level items
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            MainLogic = new clsMainLogic();

            isFull = MainLogic.IsDataBaseFull();

            MyList = new List<clsLineItems>();

            //main = this;
            cbItems.ItemsSource = MainLogic.listItems();

            dgAll_Items.ItemsSource = MyList;



            ItemsWindow = new wndItems();        //initializes the EditWindow

            SearchWindow = new wndSearch();  //initializes the SearchWindow
        }



        /// <summary>
        /// displays all the info to the data grid 
        /// function should be called every time there is a edit to the data that is saved.
        /// </summary>
        private void UpdateDisplays()
        {
            try
            {
                //display all info on data grid
                dgAll_Items.Items.Refresh();

                double totalCost = 0;

                foreach (var item in MyList)
                {
                    totalCost += (item.Cost * item.LineItemNum);
                }

                TotalCostLabel.Content = totalCost;


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }



        /// <summary>
        /// creates a message box and displays a message box with text relating to the error.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// parses out the data the user entered and sends it to the main logic class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //parse out the date entered on the data grid

                //parse out the line items entered

                //  mainLogic.CreateInvoice(date,line);

                //checks if database is full then disables create invoice button
                DisableCreation();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// parses out the data the user entered and sends it to the main logic class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //parse out the data that they entered
                //   mainLogic.Update();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// parses out which invoice was deleted and deletes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the id
                //   mainLogic.Delete(id);

                //rechecks if you can add invoices
                DisableCreation();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// disables the create invoice button if the database is deemed full.
        /// </summary>
        private void DisableCreation()
        {
            try
            {
                // isFull = mainLogic.IsDataBaseFull();

                if (isFull)
                {
                    CreateInvoice.IsEnabled = false;
                }
                else
                {
                    CreateInvoice.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }


        /// <summary>
        /// menu control when search is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTab_Click(object sender, RoutedEventArgs e)
        {


            this.Hide();
            SearchWindow.Show();

        }

        /// <summary>
        /// menu control when items is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsTab_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ItemsWindow.Show();

        }


        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Item = cbItems.SelectedValue as clsLineItems;
                Int32.TryParse(AmountOfItems.Text, out int x);
                Item.LineItemNum = x;
                MyList.Add(Item);

                UpdateDisplays();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
