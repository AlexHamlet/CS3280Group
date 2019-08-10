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

        //declares the search window
        wndSearch SearchWindow;

        //declare the edit window
        wndItems ItemsWindow;


        //this is where the main logic of the window lives
        clsMainLogic MainLogic;

        //this is where all the selected invoices items are stored
        List<clsLineItems> MyList;

        //this is my invoice
        clsInvoice MyInvoice;


        //declares the is full bool value
        bool isFull;

        //figures out what type of save we are doing
        bool TypeOfSave;

        mainWindow.MainWindow main;

        /// <summary>
        /// initializes all class level items
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            MainLogic = new clsMainLogic();

            isFull = MainLogic.IsDataBaseFull();

            MyList = new List<clsLineItems>();

            MyInvoice = new clsInvoice(0,"",0.00);

            TypeOfSave = false;

            //main = this;
            cbItems.ItemsSource = MainLogic.ListItems();

            dgAll_Items.ItemsSource = MyList;

            UpdateDisplays();

            ItemsWindow = new wndItems();        //initializes the EditWindow

            SearchWindow = new wndSearch();  //initializes the SearchWindow

            main = this;

            
            EditInvoice.IsEnabled = false;
            DeleteInvoice.IsEnabled = false;
            SaveInvoice.IsEnabled = false;
            DisableAllInput();

            
        }

        /// <summary>
        /// closes the program
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        { 

            Environment.Exit(0);
        }



        /// <summary>
        /// displays all the info to the data grid 
        /// function should be called every time there is a edit to the data that is saved.
        /// </summary>
        private void UpdateDisplays()
        {
            try
            {


                


                //MyInvoice.InvoiceTotal = 5;



                //if there is a selected invoice
                if (MyInvoice.InvoiceID != 0)
                {
                    InvoiceIdLabel.Content = "Invoice ID: " +  MyInvoice.InvoiceID;

                     

                    MainWndDateTimePicker.SelectedDate = DateTime.Parse(MyInvoice.InvoiceDate);



                }
                else
                {
                    InvoiceIdLabel.Content = "Invoice ID: TBD";
                }

                //calculates the total cost.
                double totalCost = 0;

                foreach (var item in MyList)
                {
                    totalCost += (item.Cost * item.LineItemNum);
                }

                TotalCostTextBox.Text = totalCost.ToString();


                //display all selected Items on data grid
                dgAll_Items.Items.Refresh();

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
                TypeOfSave = false;
                SaveInvoice.IsEnabled = true;
                EnableALLInput();
                //checks if database is full then disables create invoice button
                //DisableCreation();

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
                TypeOfSave = true;
                SaveItem.IsEnabled = true;
                EnableALLInput();


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
                MainLogic.DeleteInvoice(MyInvoice.InvoiceID);

                //clears all displays
                MyList.Clear();
                MyInvoice = new clsInvoice(0, "", 0);
                UpdateDisplays();
                //rechecks if you can add invoices
                //DisableCreation();
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
                isFull = MainLogic.IsDataBaseFull();

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
            try
            {

                this.Hide();
                SearchWindow.ShowDialog();
                this.Show();

                DeleteInvoice.IsEnabled = true;
                EditInvoice.IsEnabled = true;

                //check if it has been closed but nothing has been selected
                if (SearchWindow.SelectedInvoice.InvoiceID != 0) {

                    MyInvoice = SearchWindow.SelectedInvoice;

                    //adds all the items that are associated with the invoice into the data grid
                    foreach (var item in MainLogic.GetLineItems(MyInvoice.InvoiceID))
                    {
                        MyList.Add(item);
                    }
                }

                UpdateDisplays();
            }
            catch(Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// menu control when items is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsTab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                ItemsWindow.ShowDialog();
                this.Show();
                UpdateDisplays();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

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

        private void DisableAllInput()
        {
            SaveItem.IsEnabled = false;
            dgAll_Items.IsEnabled = false;
            cbItems.IsEnabled = false;
            MainWndDateTimePicker.IsEnabled = false;
        }

        private void EnableALLInput()
        {
            SaveItem.IsEnabled = true;
            dgAll_Items.IsEnabled = true;
            cbItems.IsEnabled = true;
            MainWndDateTimePicker.IsEnabled = true;
        }

        /// <summary>
        /// what happens when the save button is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            //parse out the date entered on the data grid
            string date = MainWndDateTimePicker.Text;

            //parse out the cost they have entered
            string sCost = TotalCostTextBox.Text;

            if (TypeOfSave)
            {

                MainLogic.UpdateInvoice(MyInvoice.InvoiceID, date, sCost);
                //updates all the invoices added and deleted
                MainLogic.UpdateLineItems(MyInvoice.InvoiceID, MyList);
                
            }
            else { 
                //combo statement calls another function that will add a invoice and will return the new invoices id
                InvoiceIdLabel.Content = "Invoice ID: " + MainLogic.CreateInvoice(date, sCost, MyList);
            }
            EditInvoice.IsEnabled = true;
            DeleteInvoice.IsEnabled = true;


            DisableAllInput();
        }
    }
}
