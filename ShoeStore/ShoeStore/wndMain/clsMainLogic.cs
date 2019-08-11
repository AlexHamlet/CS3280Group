using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using ShoeStore.Search;

namespace mainWindow
{
    class clsMainLogic
    {
        #region Declarations
        //declare a list of all the items in the database for the data grid

        ShoeStore.clsDataAccess db;

        clsMainSQL Query;


        /// <summary>
        /// initialization of all class level variables
        /// </summary>
        public clsMainLogic()
        {
            try
            {
                Query = new clsMainSQL();
                db = new ShoeStore.clsDataAccess();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        #endregion


        #region Getting objects for display

        /// <summary>
        /// gets all the invoices()
        /// </summary>
        /// <returns></returns>
        public List<clsLineItems> ListItems()
        {

            try
            {
                List<clsLineItems> lstItems = new List<clsLineItems>();

                int rows = 0;

                DataSet rawdata = db.ExecuteSQLStatement(Query.GetItems(), ref rows);


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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }


        public List<clsLineItems> GetLineItems(int id)
        {
            try
            {
                List<clsLineItems> lstItems = new List<clsLineItems>();

                int rows = 0;

                string sSQL = Query.getLineItems(id);

                DataSet rawdata = db.ExecuteSQLStatement(sSQL, ref rows);


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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }



        #endregion

        #region Creators and Updater

        /// <summary>
        /// creates a new invoice
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Line"></param>
        public int CreateInvoice(string date, string cost, List<clsLineItems> lstItems)
        {

            try
            {
                db.ExecuteNonQuery(Query.CreateInvoice(date, cost));


                Int32.TryParse(db.ExecuteScalarSQL(Query.GetInsertedInvoiceId(date, cost)), out int id);
                InsertLineItems(id, lstItems);
                return id;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }


        /// <summary>
        /// updates the specified invoice
        /// </summary>
        /// <param name="UpdatedInvoice"></param>
        public void UpdateInvoice(int id, string date, string cost)
        {
            try
            {
                db.ExecuteNonQuery(Query.UpdateInvoice(id, date, cost));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }


        /// <summary>
        /// updates the line items of a selected record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lstItems"></param>
        public void UpdateLineItems(int id, List<clsLineItems> lstItems)
        {
            try
            {
                //deletes all line items with id = 
                DeleteLineItems(id);

                //adds all items
                foreach (var item in lstItems)
                {
                    int x = item.LineItemNum;
                    string y = item.ItemCode;
                    db.ExecuteNonQuery(Query.InsertLineItems(id, item));

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }


        }

        /// <summary>
        /// adds all line items in a new invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lstItems"></param>
        public void InsertLineItems(int id, List<clsLineItems> lstItems)
        {
            try
            {

                //adds all items
                foreach (var item in lstItems)
                {
                    int x = item.LineItemNum;
                    string y = item.ItemCode;
                    db.ExecuteNonQuery(Query.InsertLineItems(id, item));

                }
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
        /// deletes the invoice with the id equal to the selected invoice
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteInvoice(int ID)
        {
            try
            {
                DeleteLineItems(ID);

                db.ExecuteNonQuery(Query.DeleteInvoice(ID));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public void DeleteLineItems(int id)
        {
            try
            {
                db.ExecuteNonQuery(Query.DeleteLineItems(id));
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
