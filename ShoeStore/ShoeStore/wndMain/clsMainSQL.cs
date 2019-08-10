using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using ShoeStore.Search;

namespace mainWindow
{

    /// <summary>
    /// this contains all the SQL logic for the main window
    /// </summary>
    class clsMainSQL
    {

        private string sConnectionString;


        /// <summary>
        /// initialization of all class level variables
        /// </summary>
        public clsMainSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }




        /// <summary>
        /// this returns a all invoices
        /// </summary>
        public List<clsInvoice> GetInvoices()
        {
            try
            {
                List<clsInvoice> lstInvoices = new List<clsInvoice>();

                int rows = 0;
                string SQL = "Select * From Invoices";
                DataSet rawdata = ExcecuteSQLStatement(SQL, ref rows);

                int id;
                double total;
                for (int x = 0; x < rows; x++)
                {
                    Int32.TryParse(rawdata.Tables[0].Rows[x][0].ToString(), out id);
                    Double.TryParse(rawdata.Tables[0].Rows[x][2].ToString(), out total);

                    lstInvoices.Add(new clsInvoice(id, rawdata.Tables[0].Rows[x][1].ToString(), total));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// creates a new invoice
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="line"></param>
        public void CreateInvoice(string Date, ShoeStore.wndMain.clsLineItems line)
        {
            try
            {
                string sSQL = "INSERT INTO Invoice VALUES @Date, @TotalCost";
                ExecuteNonQuery(sSQL);


                //gets the id of the last data entered
                sSQL = "SELECT SCOPE_IDENTITY()";
                Int32.TryParse(ExecuteScalarSQL(sSQL), out int InvoiceNumber);



                //add all line items from the invoice into the ListItemsTable
                //foreach (var item in line)
                //{
                //parse out all the items in the line Item list

                //sSQL = "INSERT INTO LineItems VALUES @InvoiceNumber, @LineItemNumber, @ItemCode";
                //  db.ExecuteNonQuery(sSQL);
                //}


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }


        }



        /// <summary>
        /// this deletes the invoice with specified id number
        /// </summary>
        public void DeleteInvoice(int IDNum)
        {
            try
            {


                string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = @IDNum";
                ExecuteNonQuery(sSQL);


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// this updates selected invoice
        /// </summary>
        public void UpdateInvoice(clsInvoice UpdatedInvoice)
        {
            try
            {


                string sSQL = "UPDATE Invoices SET InvoiceDate = @UpdatedInvoice.InvoiceDate,"
                              + " TotalCost = @UpdatedInvoice.TotalCost WHERE InvoiceNum = @UpdatedInvoice.InvoiceNum;";
                ExecuteNonQuery(sSQL);


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }


        /// <summary>
        /// returns the count of how many invoices there are
        /// </summary>
        /// <returns></returns>
        public int GetCountOfInvoices()
        {
            try
            {
                string sSQL = "SELECT COUNT(*) FROM Invoices;";
                
                Int32.TryParse(ExecuteScalarSQL(sSQL), out int num);



                return num;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }



        }



        #region data access class



        /// <summary>
        /// Used to execute all SQL commands.
        /// </summary>
        /// <param name="sql">SQL command</param>
        /// <param name="retval">Number of resulting rows</param>
        /// <returns>A Dataset result of the command</returns>
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

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
        public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        #endregion


    }
}
