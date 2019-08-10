using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.wndMain
{
    /// <summary>
    /// new item used to make a list of line items for the main menu 
    /// </summary>
    class clsLineItems
    {
        public int InvoiceNum { get; set; }
        public int LineItemNum { get; set; }
        public string ItemCode { get; set; }

    }
}
