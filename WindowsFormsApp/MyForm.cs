using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApp
{
    /// <summary>
    /// This class defines the function of all buttons
    /// </summary>
    public partial class MyForm : Form
    {
        /// <summary>
        /// Initializes all components that will be used in windows form and sets their properties
        /// </summary>
        public MyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The dictionary stores stock IDs as keys and list of stock with same stock ID as values
        /// </summary>
        private Dictionary<string, List<Stock>> Categories;

        /// <summary>
        /// The list maintains the order of the dictionary
        /// </summary>
        private List<(string id, string name)> DictionaryOrder;

        /// <summary>
        /// The file path that user chose
        /// </summary>
        private string FilePath;

        /// <summary>
        /// Record the time used in each section
        /// </summary>
        private Stopwatch Watch;

        /// <summary>
        /// This method define how to interact with user when read file button is clicked
        /// </summary>
        /// <param name="sender">It's the object that triggers the button</param>
        /// <param name="e">It specifies which kind of event is triggered</param>
        private void ReadButtonClick(object sender, EventArgs e)
        {
            // Set the attributes of the file dialog
            OpenFileDialog readFileDialog = new OpenFileDialog();
            readFileDialog.Title = "請開啟(.csv)檔案";
            readFileDialog.Filter = "csv files (.csv)|*.csv";

            // What to do when user clicks OK in the dialog
            if (readFileDialog.ShowDialog() == DialogResult.OK)
            {
                Watch = Stopwatch.StartNew();

                StatusRead.Text = "讀檔中";

                FilePath = readFileDialog.FileName;
                readFileDialog.Dispose();
                FilenameDisplay.Text = FilePath;

                ReadFileWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// This method define how to interact with user when search file button is clicked
        /// </summary>
        /// <param name="sender">It's the object that triggers the button</param>
        /// <param name="e">It specifies which kind of event is triggered</param>
        private void SearchButtonClick(object sender, EventArgs e)
        {
            Watch.Restart();
            string[] stockList = ComboBoxGetSet.GetSelectedStock(StockComboBox.Text);
            List<Stock> searchResult = new List<Stock>();
            
            try
            {
                // Add filtered stock to list
                foreach (string num in stockList)
                {
                    searchResult.AddRange(Categories[num]);
                }
                List<StockSummary> summaryGrid = StockSummary.ProduceSummary(searchResult, Categories);
                ListAll.DataSource = searchResult;
                ListFiltered.DataSource = summaryGrid;
                Watch.Stop();
                TimeUsed.OutputTime(Watch.Elapsed, "查詢");
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("搜尋條件錯誤");
            }
        }

        /// <summary>
        /// This method define how to interact with user when Top fifty button is clicked
        /// </summary>
        /// <param name="sender">It's the object that triggers the button</param>
        /// <param name="e">It specifies which kind of event is triggered</param>
        private void FiftyButtonClick(object sender, EventArgs e)
        {
            Watch.Restart();
            string[] stockList = ComboBoxGetSet.GetSelectedStock(StockComboBox.Text);
            List<Stock> searchResult = new List<Stock>();

            try
            {
                // Add filtered stock to list
                foreach (string num in stockList)
                {
                    searchResult.AddRange(Categories[num]);
                }
                ListFifty.DataSource = BrokerBuySell.MergeBroker(searchResult);
                Watch.Stop();
                TimeUsed.OutputTime(Watch.Elapsed, "Top50 產生");
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("搜尋條件錯誤");
            }
        }

        /// <summary>
        /// This is the event handler of asynchronous reading
        /// </summary>
        /// <param name="sender">It's the object that triggers the asynchronous</param>
        /// <param name="e">It specifies which kind of event is triggered</param>
        private void ReadFileWorkerRun(object sender, EventArgs e)
        {
            List<Stock> lineTemp = new List<Stock>();
            List<Stock> allStock = new List<Stock>();
            List<List<Stock>> orderedAll = new List<List<Stock>>();
            Categories = new Dictionary<string, List<Stock>>() { { GlobalVariable.ALL, allStock } };
            DictionaryOrder = new List<(string, string)>();

            using (StreamReader lines = new StreamReader(FilePath))
            {
                // Don't need to add headers, it'll add it automatically
                string line = lines.ReadLine();

                // Add each stock to dictionary
                while ((line = lines.ReadLine()) != null)
                {
                    lineTemp.Add(new Stock(line));
                }
            }

            foreach (Stock temp in lineTemp)
            {
                if (!Categories.TryGetValue(temp.StockID, out List<Stock> targetList))
                {
                    targetList = new List<Stock>();
                    orderedAll.Add(targetList);
                    Categories.Add(temp.StockID, targetList);
                    DictionaryOrder.Add((temp.StockID, temp.StockName));
                }
                targetList.Add(temp);
            }

            foreach (List<Stock> stockList in orderedAll)
            {
                allStock.AddRange(stockList);
            }
        }

        /// <summary>
        /// The remaining part of the read botton's event handler
        /// </summary>
        /// <param name="sender">It's the object that triggers the asynchronous</param>
        /// <param name="e">It specifies which kind of event is triggered</param>
        private void ReadFileWorkerComplete(object sender, EventArgs e)
        {
            // Display data and change label status
            ListAll.DataSource = Categories[GlobalVariable.ALL];
            Watch.Stop();
            StatusRead.Text = "讀檔完成";
            TimeUsed.OutputTime(Watch.Elapsed, "讀取");

            // Create combo box list
            Watch.Restart();
            StockComboBox.CreateComboBox(DictionaryOrder);
            Watch.Stop();
            TimeUsed.OutputTime(Watch.Elapsed, "Combobox產生");
        }
    }
}