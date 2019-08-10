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

        public List<clsLineItems> getLineItems(int invoiceId)
        {
            try
            {
                List<clsLineItems> lstItems = new List<clsLineItems>();

                int rows = 0;
                string SQL = "Select a.ItemCode, a.ItemDesc, a.Cost, b.LineItemNum" +
                    "From ItemDesc AS a JOIN LineItems as b ON a.ItemCode = b.ItemCode WHERE InvoiceNum = " + invoiceId.ToString();
                DataSet rawdata = db.ExecuteSQLStatement(SQL, ref rows);


                for (int x = 0; x < rows; x++)
                {
                    Double.TryParse(rawdata.Tables[0].Rows[x][2].ToString(), out double total);
                    Int32.TryParse(rawdata.Tables[0].Rows[x][3].ToString(), out int num);
                    lstItems.Add(new clsLineItems
                    {
                        ItemCode = rawdata.Tables[0].Rows[x][0].ToString(),
                        ItemDesc = rawdata.Tables[0].Rows[x][1].ToString(),
                        Cost = total,
                        LineItemNum = num

                    });
                }

                return lstItems;
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
        public void CreateInvoice(string Date)
        {
            try
            {
                string sSQL = "INSERT INTO Invoice VALUES @Date, @TotalCost";
                db.ExecuteNonQuery(sSQL);


                //gets the id of the last data entered
                sSQL = "SELECT SCOPE_IDENTITY()";
                Int32.TryParse(db.ExecuteScalarSQL(sSQL), out int InvoiceNumber);



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
                db.ExecuteNonQuery(sSQL);


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
                db.ExecuteNonQuery(sSQL);


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

                Int32.TryParse(db.ExecuteScalarSQL(sSQL), out int num);



                return num;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }



        }




    }
}
