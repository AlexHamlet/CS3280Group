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
        /// database connection string
        /// </summary>
        private string sConnectionString;

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

        public clsItemsSQL()
        {
            ds = new DataSet();
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
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

                ds = ExcecuteSQLStatement(SQL, ref rows);
        
                //return the DataSet
                return ds;
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
        public void AddItems(string cost, string desc)
        {
            try
            {
                SQL = "INSERT INTO ItemDesc (ItemDesc, Cost)" +
                    "VALUES (" + desc + ", " + cost + "";
                ds = ExcecuteSQLStatement(SQL, ref rows);
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
        public void EditItems(string cost, string desc, string code)
        {
            try
            {
                SQL = "UPDATE ItemDesc" +
                    "SET Cost = " + cost + ", ItemDesc = " + desc + "" +
                    "WHERE ItemCode = " + code + "";
                ds = ExcecuteSQLStatement(SQL, ref rows);
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
        public string DeleteItems(string code)
        {
            try
            {
                SQL = "SELECT LI.LineItemNum FROM LineItems as LI" +
                    "WHERE LI.ItemCode = " + code + "";
                ds = ExcecuteSQLStatement(SQL, ref rows);
                if (rows == 0)
                {
                    SQL = "DELETE FROM ItemDesc" +
                        "WHERE ItemCode = " + code + "";
                    ds = ExcecuteSQLStatement(SQL, ref rows);
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

        /// <summary>
        /// this function interacts with the databaase with the sql statement passed in a returns a dataset and row count
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="retval"></param>
        /// <returns>dataset for datagrid</returns>
        public DataSet ExcecuteSQLStatement(string sql, ref int retval)
        {
            try
            {
                DataSet d = new DataSet();

                using (OleDbConnection con = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapt = new OleDbDataAdapter())
                    {
                        con.Open();

                        adapt.SelectCommand = new OleDbCommand(sql, con);
                        adapt.SelectCommand.CommandTimeout = 0;

                        adapt.Fill(d);

                    }
                }
                retval = d.Tables[0].Rows.Count;

                return d;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
