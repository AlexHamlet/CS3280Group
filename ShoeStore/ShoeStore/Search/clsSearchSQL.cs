using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// The connection string for the database
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Initializes the SQL class
        /// </summary>
        public clsSearchSQL()
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs a query, Returns all invoice numbers
        /// </summary>
        /// <returns>A List<int> of invoice numbers</returns>
        public string getInvoiceNumbers()
        {
            try
            {
                return "Select InvoiceNum From Invoices";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs a query, Returns all invoice dates
        /// </summary>
        /// <returns>A List<string> of invoice dates</returns>
        public string getInvoiceDates()
        {
            try
            {
                return "Select InvoiceDate From Invoices";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs query, Returns all invoice totals
        /// </summary>
        /// <returns>A List<double> of invoice totals</returns>
        public string getInvoiceTotals()
        {
            try
            {
                return "Select TotalCost From Invoices";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs query, Returns all invoices
        /// </summary>
        /// <returns>A List of invoice objects</returns>
        public string getInvoices()
        {
            try
            {
                return "Select * From Invoices";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs query, Returns Invoice objects matching parameters
        /// </summary>
        /// <param name="num">InvoiceID</param>
        /// <param name="date">Invoice Date</param>
        /// <param name="tot">Invoice Total</param>
        /// <returns>A List of Invoice objects</returns>
        public string getInvoices(string num, string date, string tot)
        {
            try
            {
                if (date != "")
                {
                    string querydate = date;
                    date = querydate.Substring(0, querydate.IndexOf(' '));
                }
                bool andflag = false;
                string SQL = "Select * From Invoices";
                if(num != "" || date != "" || tot != "")
                {
                    SQL += " Where";
                }
                if (!num.Equals(""))
                {
                    SQL += " InvoiceNum=" + num;
                    andflag = true;
                }
                if (!date.Equals(""))
                {
                    if (andflag)
                    {
                        SQL += " and";
                    }
                    SQL += " InvoiceDate like '" + date + "'";
                    andflag = true;
                }
                if (!tot.Equals(""))
                {
                    if (andflag)
                    {
                        SQL += " and";
                    }
                    SQL += " TotalCost=" + tot;
                }
                return SQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

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
    }
}
