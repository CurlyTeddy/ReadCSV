using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    /// <summary>
    ///  This is the structure of every single row in summary
    /// </summary>
    public class StockSummary
    {
        private decimal Average;

        /// <summary>
        /// Gets and set the ID of the stock
        /// </summary>
        public string StockID { get; set; }

        /// <summary>
        /// The name of the stock
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// The buyTotal of the stock
        /// </summary>
        public long BuyTotal { get; set; }

        /// <summary>
        /// The sell Total of the stock
        /// </summary>
        public long SellTotal { get; set; }

        /// <summary>
        /// The average price of the stock
        /// </summary>
        public decimal AvgPrice
        {
            get
            {
                if (Average == 0)
                {
                    return Math.Truncate(Average);
                }
                return Average;
            }
            set
            {
                Average = value;
            }
        }

        /// <summary>
        /// The buysellover of the stock
        /// </summary>
        public long BuySellOver { get; set; }

        /// <summary>
        /// The secbroker count of the stock
        /// </summary>
        public int SecBrokerCnt { get; set; }

        /// <summary>
        /// The method calculates the answer of summary's field
        /// </summary>
        /// <param name="summary">The summary to calculate</param>
        /// <param name="setBroker">The set to record different second broker's name</param>
        public static void CalculateField(StockSummary summary, HashSet<string> setBroker)
        {
            summary.BuySellOver = summary.BuyTotal - summary.SellTotal;
            try
            {
                summary.AvgPrice /= (summary.BuyTotal + summary.SellTotal);
            }
            catch (DivideByZeroException)
            {
                summary.AvgPrice = summary.AvgPrice;
            }
            summary.SecBrokerCnt = setBroker.Count;
        }

        /// <summary>
        /// The method produces the summary of each selected stocks
        /// </summary>
        /// <param name="stocks">This is the list of all selected stocks</param>
        /// <param name="categories">This is the dictionary of stocks</param>
        /// <returns>It returns the summary of all selected stocks</returns>
        public static List<StockSummary> ProduceSummary(List<Stock> stocks, Dictionary<string, List<Stock>> categories)
        {
            // Variable currentStock is used to detect whether stock has changed or not
            string currentStock = null;
            HashSet<string> setBroker = new HashSet<string>();
            List<StockSummary> summaryList = new List<StockSummary>();
            StockSummary summary = new StockSummary();

            foreach (Stock s in stocks)
            {
                if (s.StockID != currentStock)
                {
                    // Stock is changed or first stock appears
                    if (currentStock != null)
                    {
                        // Stock is changed
                        CalculateField(summary, setBroker);

                        summaryList.Add(summary);
                        summary = new StockSummary();
                        setBroker.Clear();
                    }
                    summary.StockID = s.StockID;
                    summary.StockName = s.StockName;
                    currentStock = s.StockID;
                }

                summary.BuyTotal += s.BuyQty;
                summary.SellTotal += s.SellQty;
                setBroker.Add(s.SecBrokerID);

                summary.AvgPrice += s.Price * (s.BuyQty + s.SellQty);
            }

            // Calculate last stock's summary
            CalculateField(summary, setBroker);

            summary.StockID = currentStock;
            summary.StockName = categories[currentStock][0].StockName;
            summaryList.Add(summary);

            return summaryList;
        }
    }
}