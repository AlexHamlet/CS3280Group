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
        #region Declarations 
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


        //declares the is full boolean value
        bool isFull;

        //figures out what type of save we are doing
        bool TypeOfSave;


        /// <summary>
        /// initializes all class level items
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //Main window main logic class
                MainLogic = new clsMainLogic();

                //Flag that checks if the database is full
                isFull = MainLogic.IsDataBaseFull();

                //all the selected items 
                MyList = new List<clsLineItems>();

                //new blank invoice
                MyInvoice = new clsInvoice(0, "", 0.00);

                //flag to switch between and edit save and an add save
                TypeOfSave = false;

                //Populates All items in the database to a combo box
                cbItems.ItemsSource = MainLogic.ListItems();

                //populates all selected into a data grid
                dgAll_Items.ItemsSource = MyList;

                //updates the displays
                UpdateDisplays();


                ItemsWindow = new wndItems();        //initializes the ItemsWindow

                SearchWindow = new wndSearch();  //initializes the SearchWindow

                //disables editing/deleting/saving until there is something to save
                EditInvoice.IsEnabled = false;
                DeleteInvoice.IsEnabled = false;
                SaveInvoice.IsEnabled = false;
                //disables all input 
                DisableAllInput();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region events


        /// <summary>
        /// parses out the data the user entered and sends it to the main logic class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //makes a new blank invoice
                MyInvoice = new clsInvoice(0, "", 0.0);
                MyInvoice.InvoiceDate = DateTime.Now.ToString();
                MyList.Clear();
                cbItems.SelectedIndex = -1;
                AmountOfItems.Clear();


                UpdateDisplays();
                TypeOfSave = false;
                SaveInvoice.IsEnabled = true;
                EnableALLInput();

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
                SaveInvoice.IsEnabled = true;
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
                DisableCreation();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
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
                if (SearchWindow.SelectedInvoice != null)
                {

                    MyInvoice = SearchWindow.SelectedInvoice;

                    //adds all the items that are associated with the invoice into the data grid
                    foreach (var item in MainLogic.GetLineItems(MyInvoice.InvoiceID))
                    {
                        MyList.Add(item);
                    }
                }

                UpdateDisplays();
            }
            catch (Exception ex)
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

        /// <summary>
        /// Adds Items to the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AmountOfItems.Text != "" && cbItems.SelectedIndex != -1)
                {
                    var Item = cbItems.SelectedValue as clsLineItems;
                    Int32.TryParse(AmountOfItems.Text, out int x);
                    Item.LineItemNum = x;
                    MyList.Add(Item);
                    MyInvoice.InvoiceDate = MainWndDateTimePicker.Text;
                    UpdateDisplays();
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// what happens when the save button is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWndDateTimePicker.SelectedDate != null && MyList.Count != 0) {
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
                    else
                    {
                        //combo statement calls another function that will add a invoice and will return the new invoices id
                        MyInvoice.InvoiceID = MainLogic.CreateInvoice(date, sCost, MyList);
                        InvoiceIdLabel.Content = "Invoice ID: " + MyInvoice.InvoiceID;

                    }
                    DisableCreation();
                    EditInvoice.IsEnabled = true;
                    DeleteInvoice.IsEnabled = true;

                    DisableAllInput();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Deletes the row selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deletedItem = dgAll_Items.SelectedValue as clsLineItems;
                MyList.Remove(deletedItem);
                UpdateDisplays();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// closes the program
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                         MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// allows for only numbers to be entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountOfItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                //Allow the user to use the backspace, delete, tab and enter
                if (!(e.Key == Key.Back || e.Key == Key.Delete))
                {
                    //No other keys allowed besides numbers, backspace, delete, tab, and enter
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region function

        /// <summary>
        /// displays all the info to the data grid 
        /// function should be called every time there is a edit to the data that is saved.
        /// </summary>
        private void UpdateDisplays()
        {
            try
            {
                //Populates All items in the database to a combo box
                cbItems.ItemsSource = MainLogic.ListItems();

                //if there is a selected invoice
                if (MyInvoice.InvoiceID != 0)
                {
                    InvoiceIdLabel.Content = "Invoice ID: " + MyInvoice.InvoiceID;

                    MainWndDateTimePicker.SelectedDate = DateTime.Parse(MyInvoice.InvoiceDate);

                }
                //if there isn't a selected invoice
                else
                {
                    InvoiceIdLabel.Content = "Invoice ID: TBD";
                }

                //calculates the total cost.
                double totalCost = 0;

                //calculates the cost
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
        /// disables all input devices
        /// </summary>
        private void DisableAllInput()
        {
            try
            {
                SaveItem.IsEnabled = false;
                dgAll_Items.IsEnabled = false;
                cbItems.IsEnabled = false;
                MainWndDateTimePicker.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// enables all input devices
        /// </summary>
        private void EnableALLInput()
        {
            try
            {
                SaveItem.IsEnabled = true;
                dgAll_Items.IsEnabled = true;
                cbItems.IsEnabled = true;
                MainWndDateTimePicker.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        #endregion



    }
}
