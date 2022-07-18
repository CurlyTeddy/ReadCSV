using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    /// <summary>
    /// This is the structure of each row of top 50 DGV
    /// </summary>
    public class BrokerBuySell
    {
        /// <summary>
        /// The stock name of the row
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// The secbroker name of the row
        /// </summary>
        public string SecBrokerName { get; set; }

        /// <summary>
        /// The buysellover of the row
        /// </summary>
        public long BuySellOver { get; set; }

        /// <summary>
        /// The constuctor assigns stock name, secbroker name, buysellover to corresponding variable
        /// </summary>
        /// <param name="s">It takes stock ID as parameter</param>
        public BrokerBuySell(Stock s)
        {
            this.StockName = s.StockName;
            this.SecBrokerName = s.SecBrokerName;
        }

        /// <summary>
        /// Merge two sorted list
        /// </summary>
        /// <param name="first">First sorted list</param>
        /// <param name="sec">Second sorted list</param>
        /// <returns>The method returns the merged and sorted list</returns>
        public static List<BrokerBuySell> MergeList(List<BrokerBuySell> first, List<BrokerBuySell> sec, string rule)
        {
            List<BrokerBuySell> result = new List<BrokerBuySell>(new BrokerBuySell[first.Count + sec.Count]);
            int ptrOne = 0;
            int ptrTwo = 0;

            while (ptrOne < first.Count && ptrTwo < sec.Count)
            {
                if (rule == "DESC" && first[ptrOne].BuySellOver >= sec[ptrTwo].BuySellOver)
                {
                    result[ptrOne + ptrTwo] = first[ptrOne++];
                }
                else if (rule == "ASC" && first[ptrOne].BuySellOver <= sec[ptrTwo].BuySellOver)
                {
                    result[ptrOne + ptrTwo] = first[ptrOne++];
                }
                else
                {
                    result[ptrOne + ptrTwo] = sec[ptrTwo++];
                }
            }

            if (ptrOne == first.Count)
            {
                for (int i = ptrTwo; i < sec.Count; ++i)
                {
                    result[ptrOne + i] = sec[i];
                }
            }
            else
            {
                for (int i = ptrOne; i < first.Count; ++i)
                {
                    result[i + ptrTwo] = first[i];
                }
            }
            return result;
        }

        /// <summary>
        /// Merge sort for stable sorting
        /// </summary>
        /// <param name="source">List that waits for sorting</param>
        /// <param name="start">Start of the range of the sorting list</param>
        /// <param name="end">End of the range of the sorting list</param>
        /// <returns>The method returns sorted list in a specific range</returns>
        public static List<BrokerBuySell> MergeSort(List<BrokerBuySell> source, int start, int end, string rule)
        {
            if (start == end)
            {
                return new List<BrokerBuySell> { source[start] };
            }

            int mid = (start + end) / 2;
            return MergeList(MergeSort(source, start, mid, rule), MergeSort(source, mid + 1, end, rule), rule);
        }

        /// <summary>
        /// This method will add specific stock's top and last fifty buysellover to the list
        /// </summary>
        /// <param name="dictionaryOrder">The list is used to maintain the order of the dictionary</param>
        /// <param name="brokerDictionary">The dictionary records the buysellover corresponding to secbroker's ID</param>
        /// <param name="merged">The list is used to record the buysellover of all secbroker</param>
        public static void GetTopFifty(List<string> dictionaryOrder, Dictionary<string, BrokerBuySell> brokerDictionary, List<BrokerBuySell> merged)
        {
            List<BrokerBuySell> positive = new List<BrokerBuySell>();
            List<BrokerBuySell> negative = new List<BrokerBuySell>();

            foreach (string secBrokerID in dictionaryOrder)
            {
                BrokerBuySell temp = brokerDictionary[secBrokerID];
                if (temp.BuySellOver > 0)
                {
                    positive.Add(temp);
                }
                else if (temp.BuySellOver < 0)
                {
                    negative.Add(temp);
                }
            }

            if (positive.Count != 0)
            {
                positive = MergeSort(positive, 0, positive.Count - 1, "DESC");
            }
            
            // At most add 50 stocks that the buysellover is positive 
            for (int i = 0; i < positive.Count && i < 50; ++i)
            {
                merged.Add(positive[i]);
            }

            if (negative.Count != 0)
            {
                negative = MergeSort(negative, 0, negative.Count - 1, "ASC");
            }

            // At most add 50 stocks that the buysellover is negative
            for (int i = 0; i < negative.Count && i < 50; ++i)
            {
                merged.Add(negative[i]);
            }
        }

        /// <summary>
        /// This method will add all stock's top and last fifty buysellover to the list
        /// </summary>
        /// <param name="stocks">This argument specifies the stocks to parse</param>
        /// <returns>It returns the list of all stock's top and last fifty buysellover</returns>
        public static List<BrokerBuySell> MergeBroker(List<Stock> stocks)
        {
            string currentStock = null;
            Dictionary<string, BrokerBuySell> brokerDictionary = new Dictionary<string, BrokerBuySell>();
            List<string> dictionaryOrder = new List<string>();
            List<BrokerBuySell> merged = new List<BrokerBuySell>();

            // Iterates through all stocks, and group them by secbroker's ID
            foreach (Stock stock in stocks)
            {
                if (stock.StockID != currentStock)
                {
                    if (currentStock != null)
                    {
                        GetTopFifty(dictionaryOrder, brokerDictionary, merged);
                        brokerDictionary.Clear();
                        dictionaryOrder.Clear();
                    }
                    currentStock = stock.StockID;
                }
                if (!brokerDictionary.TryGetValue(stock.SecBrokerID, out BrokerBuySell temp))
                {
                    temp = new BrokerBuySell(stock);
                    brokerDictionary[stock.SecBrokerID] = temp;
                    dictionaryOrder.Add(stock.SecBrokerID);
                }
                temp.BuySellOver += (stock.BuyQty - stock.SellQty);
            }

            GetTopFifty(dictionaryOrder, brokerDictionary, merged);

            return merged;
        }
    }
}

/*for (int i = unsorted.Count - 1; i >= 0 && unsorted[i].BuySellOver < 0; --i)
            {
                if (unsorted.Count - 1 - i == 50)
                {
                    int exceedNumber = start - i;
                    start = i;

                    while (unsorted[start].BuySellOver == unsorted[start + 1].BuySellOver)
                    {
                        --start;
                    }
                    // There will be data lost if last group of negative buysellover didn't add
                    for (int k = start + 1; k < start + 1 + exceedNumber; ++k)
                    {
                        merged.Add(unsorted[k]);
                    }
                    break;
                }
                if (unsorted[i].BuySellOver != unsorted[i - 1].BuySellOver)
                {
                    for (int k = i; k <= start; ++k)
                    {
                        merged.Add(unsorted[k]);
                    }
                    start = i - 1;
                }
            }
*/