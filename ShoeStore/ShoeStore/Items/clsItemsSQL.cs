using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Reflection;

namespace ShoeStore.Items
{
    class clsItemsSQL
    {
        /// <summary>
        /// sql statement string
        /// </summary>
        private string SQL;

        /// <summary>
        /// how many rows were returned
        /// </summary>
        private int rows = 0;

        /// <summary>
        /// create a new dataset object for the data grid
        /// </summary>
        DataSet ds;

        /// <summary>
        /// create instance of the data access class
        /// </summary>
        ShoeStore.clsDataAccess db;

        public clsItemsSQL()
        {
            ds = new DataSet();
            db = new ShoeStore.clsDataAccess();
        }

        /// <summary>
        /// get the items table for the dattagrid
        /// </summary>
        /// <returns>table from database</returns>
        public DataSet GetItems()
        {
            try
            {
                SQL = "SELECT * FROM ItemDesc";

                return db.ExecuteSQLStatement(SQL, ref rows);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// add items to the itemdesc table
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        public void AddItems(int cost, string desc)
        {
            try
            {
                SQL = "INSERT INTO ItemDesc (ItemDesc, Cost) " +
                    "VALUES ('" + desc + "', " + cost + ")";
                int test = db.ExecuteNonQuery(SQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// edit the selected row based on item code
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public void EditItems(int cost, string desc, int code)
        {
            try
            {
                SQL = "UPDATE ItemDesc " +
                    "SET Cost = " + cost + ", ItemDesc = '" + desc + "' " +
                    "WHERE ItemCode = " + code + "";
                int test = db.ExecuteNonQuery(SQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// delete selected item if it is not on an invoice
        /// </summary>
        /// <param name="code"></param>
        /// <returns>coonfirmation of deletion</returns>
        public string DeleteItems(int code)
        {
            try
            {
                SQL = "SELECT LI.LineItemNum FROM LineItems as LI " +
                    "WHERE LI.ItemCode = " + code + "";
                ds = db.ExecuteSQLStatement(SQL, ref rows);
                if (rows == 0)
                {
                    SQL = "DELETE FROM ItemDesc " +
                        "WHERE ItemCode = " + code + "";
                    int test = db.ExecuteNonQuery(SQL);
                    return "";
                }
                else
                    return "Item is in an invoice. Can not be deleted";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


    }
}
