using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace ShoeStore.Items
{
    /// <summary>
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Attributes
        /// <summary>
        /// create an object for the buisness class
        /// </summary>
        clsItemsLogic ItemsLogic;
        
        /// <summary>
        /// create a dataset object for the datagrid
        /// </summary>
        DataSet ds;
        #endregion

        #region Information updates
        public wndItems()
        {
            try
            {
                InitializeComponent();
                ItemsLogic = new clsItemsLogic();
                ds = new DataSet();
                UpdateDG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Items Window could not be opened\n" + ex.ToString());
            }
}

        /// <summary>
        /// function to update the datagrid information
        /// </summary>
        private void UpdateDG()
        {
            try
            {
                dgItems.ItemsSource = null;
                ds = ItemsLogic.UpdateDG();

                dgItems.ItemsSource = ds.Tables[0].DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Datagrid selection has changed. get the new information from the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                    return;
                DataRowView rowview = dgItems.SelectedItem as DataRowView;
                txtCost.Text = rowview[2].ToString();
                txtDesc.Text = rowview[1].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Check for digits only on key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }

        }
        #endregion

        #region Database buttons
        /// <summary>
        /// add information to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCost.Text == "" || txtDesc.Text == "")
                {
                    MessageBox.Show("Must fill Cost and Description to Add.");
                    
                    return;
                }
                ItemsLogic.AddItem(txtCost.Text, txtDesc.Text);
                UpdateDG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// edit selected row with the cost and description entered in the respective textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    MessageBox.Show("You must select an Item to edit");
                    return;
                }

                DataRowView row = (DataRowView)dgItems.SelectedItem;

                ItemsLogic.EditItem(txtCost.Text, txtDesc.Text, row[0].ToString());
                UpdateDG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Delete selected row
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    MessageBox.Show("You must select an Item to delete");
                    return;
                }

                DataRowView row = (DataRowView)dgItems.SelectedItem;
                int code = (int)row[0];
                ItemsLogic.DeleteItem(code);
                
                UpdateDG();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }
        #endregion
        
        #region Closing
        /// <summary>
        /// don't close the window, hide it so the main can show it in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
                //main.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something Went Wrong!\n" + ex.ToString());
            }
        }
        #endregion
    }
}
