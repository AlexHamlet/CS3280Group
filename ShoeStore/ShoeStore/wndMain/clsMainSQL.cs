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
        ShoeStore.clsDataAccess db;


        /// <summary>
        /// initialization of all class level variables
        /// </summary>
        public clsMainSQL()
        {
            db = new ShoeStore.clsDataAccess();

        }




        /// <summary>
        /// this returns a all invoices
        /// </summary>
        public List<clsLineItems> GetItems()
        {
            try
            {
                List<clsLineItems> lstItems = new List<clsLineItems>();

                int rows = 0;
                string SQL = "Select * From ItemDesc";
                DataSet rawdata = db.ExecuteSQLStatement(SQL, ref rows);


                for (int x = 0; x < rows; x++)
                {
                    Double.TryParse(rawdata.Tables[0].Rows[x][2].ToString(), out double total);

                    lstItems.Add(new clsLineItems
                    {
                        ItemCode = rawdata.Tables[0].Rows[x][0].ToString(),
                        ItemDesc = rawdata.Tables[0].Rows[x][1].ToString(),
                        Cost = total
                    });
                }

                return lstItems;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getLineItems(int invoiceId)
        {
            try
            {

                string SQL = "Select a.ItemCode, a.ItemDesc, a.Cost, b.LineItemNum " +
                    "From ItemDesc AS a INNER JOIN LineItems as b ON a.ItemCode = b.ItemCode WHERE InvoiceNum = " + invoiceId.ToString();

                return SQL;

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
        public string CreateInvoice(string Date, string Cost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES ( '" + Date + "' , " + Cost + " )";

                return sSQL;

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
        public string DeleteInvoice(int IDNum)
        {
            try
            {


                string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = " + IDNum.ToString();

                return sSQL;

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
        public string UpdateInvoice(int id, string date, string cost)
        {
            try
            {


                string sSQL = "UPDATE Invoices SET InvoiceDate = '"+ date + "' TotalCost = " + cost 
                              + "WHERE InvoiceNum = " +id.ToString() + ";";
                return sSQL;



            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        public string InsertLineItems(int id, clsLineItems Item)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems VALUES (" + id.ToString() + ","
                    + Item.LineItemNum.ToString() + ", \"" + Item.ItemCode.ToString() + "\")";
                return sSQL;
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
        public string GetCountOfInvoices()
        {
            try
            {
                string sSQL = "SELECT COUNT(*) FROM Invoices;";




                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }



        }




        /// <summary>
        /// deletes from line items the objects with the invoices of id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteLineItems(int id)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems where InvoiceNum = " + id.ToString();

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Finds the last inserted invoices id
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string GetInsertedInvoiceId(string date, string cost)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum FROM Invoices WHERE InvoiceDate like '" + date + "' AND  TotalCost = " + cost;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }


    }
}
