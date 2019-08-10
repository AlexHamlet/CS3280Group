using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// Object used to query the database
        /// </summary>
        clsSearchSQL query;

        /// <summary>
        /// Initializes the Logic class for the Search window
        /// </summary>
        public clsSearchLogic()
        {
            try
            {
                query = new clsSearchSQL();
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoice numbers from the invoices table
        /// </summary>
        /// <returns>A List<int> of invoice numbers</returns>
        public List<int> InvoiceNumbers()
        {
            try
            {
                List<int> num = new List<int>();

                int rows = 0;
                DataSet rawdata = query.ExcecuteSQLStatement(query.getInvoiceNumbers(), ref rows);

                int id;
                for (int p = 0; p < rows; p++)
                {
                    Int32.TryParse(rawdata.Tables[0].Rows[p][0].ToString(), out id);
                    num.Add(id);
                }

                return num;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoice dates from teh invoices table
        /// </summary>
        /// <returns>A List<string> of invoice dates</returns>
        public List<string> InvoiceDates()
        {
            try
            {
                List<string> date = new List<string>();

                int rows = 0;
                DataSet rawdata = query.ExcecuteSQLStatement(query.getInvoiceDates(), ref rows);

                for (int p = 0; p < rows; p++)
                {
                    date.Add(rawdata.Tables[0].Rows[p][0].ToString());
                }

                return date;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoice dates from the invoices table
        /// </summary>
        /// <returns>A List<double> of invoice totals</returns>
        public List<double> InvoiceTotals()
        {
            try
            {
                List<double> tot = new List<double>();

                int rows = 0;
                DataSet rawdata = query.ExcecuteSQLStatement(query.getInvoiceTotals(), ref rows);

                double total;
                for (int p = 0; p < rows; p++)
                {
                    Double.TryParse(rawdata.Tables[0].Rows[p][0].ToString(), out total);
                    tot.Add(total);
                }

                tot.Sort();

                return tot;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices from the Invoices table
        /// </summary>
        /// <returns>A List of invoice objects</returns>
        public List<clsInvoice> Invoices()
        {
            try
            {
                List<clsInvoice> inv = new List<clsInvoice>();

                int rows = 0;
                DataSet rawdata = query.ExcecuteSQLStatement(query.getInvoices(), ref rows);

                int id;
                double total;
                for (int p = 0; p < rows; p++)
                {
                    Int32.TryParse(rawdata.Tables[0].Rows[p][0].ToString(), out id);
                    Double.TryParse(rawdata.Tables[0].Rows[p][2].ToString(), out total);

                    inv.Add(new clsInvoice(id, rawdata.Tables[0].Rows[p][1].ToString(), total));
                }

                return inv;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns Invoices that match the given parameters
        /// </summary>
        /// <param name="num">InvoiceID</param>
        /// <param name="date">Invoice Date</param>
        /// <param name="total">Invoice Total</param>
        /// <returns>A List of Invoice Objects</returns>
        public List<clsInvoice> Invoices(string num, string date, string total)
        {
            try
            {
                List<clsInvoice> inv = new List<clsInvoice>();
                int rows = 0;
                
                DataSet rawdata = query.ExcecuteSQLStatement(query.getInvoices(num, date, total), ref rows);

                int id;
                double tot;
                for (int p = 0; p < rows; p++)
                {
                    Int32.TryParse(rawdata.Tables[0].Rows[p][0].ToString(), out id);
                    Double.TryParse(rawdata.Tables[0].Rows[p][2].ToString(), out tot);

                    inv.Add(new clsInvoice(id, rawdata.Tables[0].Rows[p][1].ToString(), tot));
                }

                return inv;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
