using System;
using System.Collections.Generic;

namespace WindowsFormsApp
{
    /// <summary>
    /// This class defines the structure of a stock
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// The data of a stock
        /// </summary>
        public string StockDate { get; set; }

        /// <summary>
        /// The identification number of a stock
        /// </summary>
        public string StockID { get; set; }

        /// <summary>
        /// The stock's name corresponding to a stock
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// The second broker's identification number
        /// </summary>
        public string SecBrokerID { get; set; }

        /// <summary>
        /// The second broker's name corresponding to the second broker's identificatoin number
        /// </summary>
        public string SecBrokerName { get; set; }

        /// <summary>
        /// The price of a stock
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The number that the stock is bought
        /// </summary>
        public long BuyQty{ get; set; }

        /// <summary>
        /// The number that the stock is sold
        /// </summary>
        public long SellQty { get; set; }

        /// <summary>
        /// The constuctor extracts information from raw data and assigns to each variable
        /// </summary>
        /// <param name="s">Raw data of a stock</param>
        public Stock(string s)
        {
            string[] data = s.Split(',');
            StockDate = data[0];
            StockID = data[1];
            StockName = data[2];
            SecBrokerID = data[3];
            SecBrokerName = data[4];
            Price = decimal.Parse(data[5]);
            BuyQty = long.Parse(data[6]);
            SellQty = long.Parse(data[7]);
        }
    }
}
