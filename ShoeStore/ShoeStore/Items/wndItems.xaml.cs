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
        /// <summary>
        /// create an object for the buisness class
        /// </summary>
        clsItemsLogic ItemsLogic;

        
        /// <summary>
        /// create main window object for closing items window
        /// </summary>
        mainWindow.MainWindow main;

        /// <summary>
        /// creatte a dataset object for the datagrid
        /// </summary>
        DataSet ds;


        public wndItems()
        {
            InitializeComponent();
            ItemsLogic = new clsItemsLogic();
            main = mainWindow.MainWindow.main;
            ds = new DataSet();
            UpdateDG();

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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// add information to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCost.Text == null || txtDesc == null)
                {
                    txtWarning.Text = "Must fill Cost and Description to Add.";
                    return;
                }
                ItemsLogic.AddItem(txtCost.Text, txtDesc.Text);
                UpdateDG();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                DataRowView row = (DataRowView)dgItems.SelectedItem;

                ItemsLogic.EditItem(txtCost.Text, txtDesc.Text, row[0].ToString());
                UpdateDG();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete selected row
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sDeleted;
                DataRowView row = (DataRowView)dgItems.SelectedItem;
                sDeleted = ItemsLogic.DeleteItem(row[0].ToString());
                txtWarning.Text = sDeleted;
                UpdateDG();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Datagrid selection has changed. get the new information from the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgItems.SelectedItem == null)
                return;
            DataRowView rowview = dgItems.SelectedItem as DataRowView;
            txtCost.Text = rowview[2].ToString();
            txtDesc.Text = rowview[1].ToString();
        }

        /// <summary>
        /// don't close the window, hide it so the main can show it in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            //main.Show();
        }
    }
}
