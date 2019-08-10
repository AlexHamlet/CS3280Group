using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using ShoeStore.Search;

namespace mainWindow
{
    class clsMainLogic
    {
        //declare a list of all the items in the database for the data grid
        List<clsLineItems> MyList;




        clsMainSQL Query;

        //sets a constant value when opened tot he maximum amount of invoices
        readonly int MAXINVOICES;


        /// <summary>
        /// initialization of all class level variables
        /// </summary>
        public clsMainLogic()
        {
            Query = new clsMainSQL();
            MAXINVOICES = (Query.GetCountOfInvoices() + 1);
        }

        /// <summary>
        /// gets all the invoices()
        /// </summary>
        /// <returns></returns>
        public List<clsLineItems> listItems()
        {

            try
            {
                return Query.GetItems();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// creates a new invoice
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Line"></param>
        public void CreateInvoice(string date)
        {

            try
            {
                Query.CreateInvoice(date);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }



        /// <summary>
        /// deletes the invoice with the id equal to the selected invoice
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            try
            {
                Query.DeleteInvoice(ID);
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
        public void Update(clsInvoice update)
        {
            try
            {
                Query.UpdateInvoice(update);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// determines what if we have entered the maximum amount of invoice entries
        /// </summary>
        /// <returns></returns>
        public bool IsDataBaseFull()
        {
            try
            {
                int countOfInvoices = Query.GetCountOfInvoices();

                if (countOfInvoices == MAXINVOICES)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }



    }
}
