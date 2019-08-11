using System;
using System.Collections.Generic;
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

namespace ShoeStore.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// The logic class for the Search window
        /// </summary>
        private clsSearchLogic ask;
        public clsInvoice SelectedInvoice;

        /// <summary>
        /// Initializes the Search window
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();

                //Initialize logic class
                ask = new clsSearchLogic();

                //Fill DataGrid
                dgInvoices.ItemsSource = ask.Invoices();

                //Fill Comboboxs
                cmbbxInvNum.ItemsSource = ask.InvoiceNumbers();
                cmbbxInvDate.ItemsSource = ask.InvoiceDates();
                cmbbxInvTot.ItemsSource = ask.InvoiceTotals();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Search Window could not be opened\n"+ex.ToString());
            }
        }

        /// <summary>
        /// Attempts to pass the selected data back to the main screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInvoices.SelectedItem != null)
                {
                    SelectedInvoice = (clsInvoice)dgInvoices.SelectedItem;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select an invoice", "Failed to Select Inovice", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Attempts to return to the main screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedInvoice = null;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Changes the contents of the datagrid based on the criteria in the comboboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbbxInvNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string num, date, cost;
                num = date = cost = "";

                if (cmbbxInvNum.SelectedIndex != -1)
                {
                    num = cmbbxInvNum.SelectedItem.ToString();
                }
                if (cmbbxInvDate.SelectedIndex != -1)
                {
                    date = cmbbxInvDate.SelectedItem.ToString();
                }
                if (cmbbxInvTot.SelectedIndex != -1)
                {
                    cost = cmbbxInvTot.SelectedItem.ToString();
                }

                dgInvoices.ItemsSource = ask.Invoices(num, date, cost);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Prevents Window from actually closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //this.SelectedInvoice = null;
                e.Cancel = true;
                this.Hide();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Handles the Clear Button
        /// Resets all of the query criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbbxInvDate.SelectedIndex = -1;
                cmbbxInvNum.SelectedIndex = -1;
                cmbbxInvTot.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Refreshes the datagrid on show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue.ToString().Equals("True"))
                {
                    cmbbxInvDate.SelectedIndex = -1;
                    cmbbxInvNum.SelectedIndex = -1;
                    cmbbxInvTot.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }
    }
}
