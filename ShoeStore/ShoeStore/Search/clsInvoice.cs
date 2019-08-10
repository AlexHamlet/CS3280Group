using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Search
{
    public class clsInvoice
    {
        private int id;
        private string date;
        private double total;
        /// <summary>
        /// Contains the InvoiceID of the Invoice Object
        /// </summary>
        public int InvoiceID { get { return id; } set => this.id = value; }
        /// <summary>
        /// Contains the InvoiceDate of the Invoice Object
        /// </summary>
        public string InvoiceDate { get { return date; } }
        /// <summary>
        /// Contains the InvoiceTotal of the Invoice Object
        /// </summary>
        public double InvoiceTotal { get { return total; } }

        /// <summary>
        /// Initializes the Invoice Object
        /// </summary>
        /// <param name="id">InvoiceID</param>
        /// <param name="date">InvoiceDate</param>
        /// <param name="total">InvoiceTotal</param>
        public clsInvoice(int id, string date, double total)
        {
            try
            {
                this.id = id;
                this.date = date;
                this.total = total;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
