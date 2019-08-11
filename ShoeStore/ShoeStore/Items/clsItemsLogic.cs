using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace ShoeStore.Items
{
    class clsItemsLogic
    {
        /// <summary>
        /// create a sql class object
        /// </summary>
        clsItemsSQL query;

        /// <summary>
        /// create a dataset object for the datagrid
        /// </summary>
        DataSet ds;


        public clsItemsLogic()
        {
            query = new clsItemsSQL();
            ds = new DataSet();
        }

        /// <summary>
        /// update the information in the datagrid
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateDG()
        {
            try
            {
                ds = query.GetItems();
                return ds;
            }
            catch (Exception ex)
            {
                
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// add item to data base from information entered into textboxes
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        public void AddItem(string cost, string desc)
        {
            try
            {
                int result = 0;
                Int32.TryParse(cost, out result);
                query.AddItems(result, desc);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// edit selected row with information entered into textboxes
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public void EditItem(string cost, string desc, string code)
        {
            try
            {
                int resultCost = 0;
                int resultCode = 0;
                Int32.TryParse(cost, out resultCost);
                Int32.TryParse(code, out resultCode);
                query.EditItems(resultCost, desc, resultCode);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// delete selected row
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string DeleteItem(string code)
        {
            try
            {
                int resultCode = 0;
                Int32.TryParse(code, out resultCode);
                string test;
                test = query.DeleteItems(resultCode);
                return test;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
