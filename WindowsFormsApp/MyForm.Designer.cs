namespace WindowsFormsApp
{
    partial class MyForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Background worker thread for reading data from file
        /// </summary>
        private System.ComponentModel.BackgroundWorker ReadFileWorker;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ListFiltered = new System.Windows.Forms.DataGridView();
            this.ListFifty = new System.Windows.Forms.DataGridView();
            this.ListAll = new System.Windows.Forms.DataGridView();
            this.FilenameDisplay = new System.Windows.Forms.TextBox();
            this.ButtonRead = new System.Windows.Forms.Button();
            this.StockComboBox = new System.Windows.Forms.ComboBox();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.ButtonFifty = new System.Windows.Forms.Button();
            this.StatusRead = new System.Windows.Forms.Label();
            this.TimeUsed = new System.Windows.Forms.RichTextBox();
            this.ReadFileWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.ListFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFifty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListAll)).BeginInit();
            this.SuspendLayout();
            // 
            // filtered_data
            // 
            this.ListFiltered.AllowUserToAddRows = false;
            this.ListFiltered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListFiltered.Location = new System.Drawing.Point(12, 363);
            this.ListFiltered.Name = "filtered_data";
            this.ListFiltered.RowTemplate.Height = 24;
            this.ListFiltered.Size = new System.Drawing.Size(795, 236);
            this.ListFiltered.TabIndex = 0;
            // 
            // fifty_data
            // 
            this.ListFifty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListFifty.Location = new System.Drawing.Point(813, 89);
            this.ListFifty.Name = "fifty_data";
            this.ListFifty.RowTemplate.Height = 24;
            this.ListFifty.Size = new System.Drawing.Size(309, 510);
            this.ListFifty.TabIndex = 1;
            // 
            // listed_data
            // 
            this.ListAll.AllowUserToAddRows = false;
            this.ListAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListAll.Location = new System.Drawing.Point(12, 89);
            this.ListAll.Name = "listed_data";
            this.ListAll.RowTemplate.Height = 24;
            this.ListAll.Size = new System.Drawing.Size(795, 268);
            this.ListAll.TabIndex = 2;
            // 
            // filename_display
            // 
            this.FilenameDisplay.Location = new System.Drawing.Point(12, 12);
            this.FilenameDisplay.Name = "filename_display";
            this.FilenameDisplay.Size = new System.Drawing.Size(619, 22);
            this.FilenameDisplay.TabIndex = 3;
            // 
            // read_button
            // 
            this.ButtonRead.BackColor = System.Drawing.Color.Honeydew;
            this.ButtonRead.Location = new System.Drawing.Point(637, 12);
            this.ButtonRead.Name = "read_button";
            this.ButtonRead.Size = new System.Drawing.Size(75, 23);
            this.ButtonRead.TabIndex = 4;
            this.ButtonRead.Text = "讀取檔案";
            this.ButtonRead.UseVisualStyleBackColor = false;
            this.ButtonRead.Click += new System.EventHandler(this.ReadButtonClick);
            this.ReadFileWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadFileWorkerRun);
            this.ReadFileWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ReadFileWorkerComplete);
            // 
            // stock_list
            // 
            this.StockComboBox.FormattingEnabled = true;
            this.StockComboBox.Location = new System.Drawing.Point(12, 50);
            this.StockComboBox.Name = "stock_list";
            this.StockComboBox.Size = new System.Drawing.Size(619, 20);
            this.StockComboBox.TabIndex = 5;
            // 
            // search_button
            // 
            this.ButtonSearch.BackColor = System.Drawing.Color.Honeydew;
            this.ButtonSearch.Location = new System.Drawing.Point(637, 50);
            this.ButtonSearch.Name = "search_button";
            this.ButtonSearch.Size = new System.Drawing.Size(75, 23);
            this.ButtonSearch.TabIndex = 6;
            this.ButtonSearch.Text = "股票查詢";
            this.ButtonSearch.UseVisualStyleBackColor = false;
            this.ButtonSearch.Click += new System.EventHandler(this.SearchButtonClick);
            // 
            // fifty_button
            // 
            this.ButtonFifty.BackColor = System.Drawing.Color.Honeydew;
            this.ButtonFifty.Location = new System.Drawing.Point(718, 50);
            this.ButtonFifty.Name = "fifty_button";
            this.ButtonFifty.Size = new System.Drawing.Size(89, 23);
            this.ButtonFifty.TabIndex = 7;
            this.ButtonFifty.Text = "買賣超Top50";
            this.ButtonFifty.UseVisualStyleBackColor = false;
            this.ButtonFifty.Click += new System.EventHandler(this.FiftyButtonClick);
            // 
            // read_status
            // 
            this.StatusRead.AutoSize = true;
            this.StatusRead.Location = new System.Drawing.Point(732, 15);
            this.StatusRead.Name = "read_status";
            this.StatusRead.Size = new System.Drawing.Size(53, 12);
            this.StatusRead.TabIndex = 9;
            this.StatusRead.Text = "讀取狀態";
            // 
            // time_used
            // 
            this.TimeUsed.Location = new System.Drawing.Point(813, 12);
            this.TimeUsed.Name = "time_used";
            this.TimeUsed.Size = new System.Drawing.Size(309, 71);
            this.TimeUsed.TabIndex = 10;
            this.TimeUsed.Text = "";
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1134, 611);
            this.Controls.Add(this.TimeUsed);
            this.Controls.Add(this.StatusRead);
            this.Controls.Add(this.ButtonFifty);
            this.Controls.Add(this.ButtonSearch);
            this.Controls.Add(this.StockComboBox);
            this.Controls.Add(this.ButtonRead);
            this.Controls.Add(this.FilenameDisplay);
            this.Controls.Add(this.ListAll);
            this.Controls.Add(this.ListFifty);
            this.Controls.Add(this.ListFiltered);
            this.Name = "MyForm";
            this.Text = "ReadCSV";
            ((System.ComponentModel.ISupportInitialize)(this.ListFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFifty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListFiltered;
        private System.Windows.Forms.DataGridView ListFifty;
        private System.Windows.Forms.DataGridView ListAll;
        private System.Windows.Forms.TextBox FilenameDisplay;
        private System.Windows.Forms.Button ButtonRead;
        private System.Windows.Forms.ComboBox StockComboBox;
        private System.Windows.Forms.Button ButtonSearch;
        private System.Windows.Forms.Button ButtonFifty;
        private System.Windows.Forms.Label StatusRead;
        private System.Windows.Forms.RichTextBox TimeUsed;
    }
}

