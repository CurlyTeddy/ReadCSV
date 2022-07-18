using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp
{
    /// <summary>
    /// Create combo box or get the selected options in it
    /// </summary>
    public static class ComboBoxGetSet
    {
        /// <summary>
        /// The method creates the combo box
        /// </summary>
        /// <param name="source">The argument is the combo box to which stock options are added</param>
        /// <param name="dictionaryOrder">The list is uesd to maintain the order of stock in the dicitonary</param>
        public static void CreateComboBox(this ComboBox source, List<(string id, string name)> dictionaryOrder)
        {
            List<string> comboBoxList = new List<string>();
            comboBoxList.Add(GlobalVariable.ALL);
            foreach((string id, string name) pair in dictionaryOrder)
            {
                comboBoxList.Add($"{pair.id} - {pair.name}");
            }
            source.DataSource = comboBoxList;
            source.Text = GlobalVariable.ALL;
        }

        /// <summary>
        /// The method gives the stock user selected
        /// </summary>
        /// <param name="selectedStock">This is the stocks that user chose or entered</param>
        /// <returns>A list of all selected stocks' ID</returns>
        public static string[] GetSelectedStock(string selectedStock)
        {
            string[] stockList;

            if (selectedStock.Contains("-"))
            {
                stockList = new string[] { selectedStock.Substring(0, selectedStock.IndexOf(' ')) };
            }
            else
            {
                stockList = selectedStock.Split(',');
            }

            return stockList;
        }
    }
}
