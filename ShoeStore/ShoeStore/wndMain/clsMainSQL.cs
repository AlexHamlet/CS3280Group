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


        /// <summary>
        /// initialization of all class level variables
        /// </summary>
        public clsMainSQL()
        {
        }


        #region Getting Objects


        /// <summary>
        /// this returns a all invoices
        /// </summary>
        public string GetItems()
        {
            try
            {

                string SQL = "Select * From ItemDesc";
                return SQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets the line items and there description from the tables ItemsDesc and LineItems
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public string getLineItems(int invoiceId)
        {
            try
            {

                string SQL = "Select a.ItemCode, a.ItemDesc, a.Cost, b.Quantity " +
                    "From ItemDesc AS a INNER JOIN LineItems as b ON a.ItemCode = b.ItemCode WHERE InvoiceNum = " + invoiceId;

                return SQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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

        #endregion

        #region Creates and Updates

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
        /// Inserts a new line item into the line item databse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
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
        /// this updates selected invoice
        /// </summary>
        public string UpdateInvoice(int id, string date, string cost)
        {
            try
            {

                string sSQL = "UPDATE Invoices SET InvoiceDate = " + date + ", TotalCost = " + cost
                              + " WHERE InvoiceNum = " + id.ToString() + ";";
                return sSQL;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }



        #endregion

        #region Deletes

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


        #endregion
    }
}
