using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NTCommon;
using XQ_Client.Workspace;
using TRQuoteCore.WindowsHelperClasses;
using TRQuoteCore.QuoteStructure;
using TRQuoteCore.Quotes.Common;
using TRQuoteCore.GridStructure;
using TRQuoteCore.Core;

namespace TRQuoteCore.GQuoteMonitor
{
    
    public partial class QuoteMonitorWindow : NTCommon.LoggedForm,  INunnaForm,IQuoteProcessor
    {
        private QuoteMonitorSettings settings;

        private QuoteMonitorColumns columns;
        private QuoteMonitorRowsManager rowsMgr;
        private QuoteMonitorUpdateManager updateMgr;
        public string cmbvalue;
       
        //XQ_Client.Logger logger;
        public string paneSymbol;


        private TRQuoteCore.Core.QuoteProcessor quoteProcessor; 
        public QuoteMonitorWindow(QuoteMonitorSettings settings, int interval)
        {

            paneSymbol = string.Empty;
            //logger = new XQ_Client.Logger();
            //logger.LogFileName = "QMON";

            InitializeComponent();
            quoteProcessor = new TRQuoteCore.Core.QuoteProcessor(this);
            updateMgr = new QuoteMonitorUpdateManager(quoteProcessor);
            rowsMgr = new QuoteMonitorRowsManager(dataGridView,updateMgr, quoteProcessor);

            InitializeDataGridViewSchema();

        this.settings = settings;
        ApplySettings();

            updateTimer.Interval = interval;
        }

        private void InitializeDataGridViewSchema()
        {
            columns = new QuoteMonitorColumns(dataGridView);
            //Set Properties for the DataGridView
            dataGridView.RowHeadersVisible = false;
            //dataGridView.RowTemplate.Height = 15;
            dataGridView.GridColor = Color.Silver;
            dataGridView.AllowUserToOrderColumns = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //menusel = new NT.Quotes.DataGridViewExtensions.ColumnSelectionMenu();
            //menusel.SelectedDataGridView = dataGridView;
            dataGridView.ContextMenuStrip = contextMenuStripOrder;
            dataGridView.RowTemplate.Height = 15;
            dataGridView.Rows.Insert(0, 100);
            dataGridView.Rows[0].Selected = true;
            dataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView_EditingControlShowing);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateMgr.UnRegisterAll();
        }

        private void ApplySettings()
        {
            dataGridView.DefaultCellStyle.BackColor = setting.BackColor;
            dataGridView.BackgroundColor = setting.BackColor;
            dataGridView.ForeColor = setting.ForeColor;
            dataGridView.Font = setting.DisplayFont;
            dataGridView.RowsDefaultCellStyle.Font = setting.DisplayFont;
            dataGridView.GridColor = setting.GridLineColor;
            dataGridView.DefaultCellStyle.SelectionBackColor = setting.SelectionColor;
            dataGridView.DefaultCellStyle.SelectionForeColor = setting.SelectionForeColor;
            dataGridView.RowTemplate.Height = setting.RowHeight;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Height = setting.RowHeight;
            }
            splitContainer.Panel2.BackColor = setting.BackColor;
            splitContainer.Panel2.ForeColor = setting.ForeColor;
            splitContainer.Panel2.Font = setting.DisplayFont;
            textBoxSymbol.BackColor = setting.BackColor;
            textBoxSymbol.ForeColor = setting.ForeColor;
        }

        GridSettings setting = GridSettings.Matrix();
        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            FormGridSettings fgs = new FormGridSettings(setting);
            DialogResult result = fgs.ShowDialog();
            if (result == DialogResult.OK)
            {
                ApplySettings();
            }

        }

        public void OnCompositeSnapshot(CompositeData data)
        {
            updateMgr.Update(data);
            rowsMgr.UpdateComposite(data);
        }

        private delegate void OnServerErrorDelegate(CompositeServerError error, string Symbol);
        public void OnServerError(CompositeServerError error, string Symbol)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new OnServerErrorDelegate(OnServerError), new object[] { error, Symbol });
                    return;
                }
                else
                {
                    labelServerError.Text = "Symbol: " + Symbol + " " + error.ToString().Replace('_', ' ');
                    if (error == CompositeServerError.Composite_Info_Error)
                    {
                        toolTipError.Show(Symbol + " is not a valid symbol.", buttonDummy, 3500);
                    }
                }
            }
            catch
            {
            }
        }

     //   private delegate void OnConnectionStatusDelegate(GQCore.ConnectionStatus status);
        //public void OnConnectionStatus(GQCore.ConnectionStatus status)
        //{
        //    try
        //    {
        //        if (InvokeRequired)
        //        {
        //            BeginInvoke(new OnConnectionStatusDelegate(OnConnectionStatus), new object[] { status });
        //            return;
        //        }
        //        else
        //        {
        //            //logger.Log("OMS-QMON got Ready");
        //            labelConnectionStatus.Text = status.ToString().Replace('_', ' ');
        //            //logger.Log("OMS-QMON now going to re-register");
        //            updateMgr.RequestAll();
        //        }
        //    }
        //    catch (Exception exx)
        //    {
        //        //logger.Log("OMS-QMON got error OnConnectionStat(): " + exx.ToString());
        //    }
        //}

        public void OnTrade(TradeData trade)
        {
            updateMgr.Update(trade);
        }



        



        public void OnBBOQuote(BBOQuoteData quote)
        {
            updateMgr.Update(quote);

        }

        //public void OnQuote(QuoteData quote)
        //{

        //}

        //public void OnMMDepthSnapshot(GQCore.Level2Data level2)
        //{

        //}
        private void toolStripTextBoxSymbol_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string symbol = toolStripTextBoxSymbol.Text;
                toolStripTextBoxSymbol.Text = string.Empty;
                rowsMgr.AddSymbol(symbol);
            }
        }

        private void toolStripButtonImportSymbols_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            if (AgentX.Enabled)
                diag.AutoUpgradeEnabled = false;
            diag.CheckFileExists = true;
            diag.Filter = "Comma Separated Symbols Text Files | *.txt";
            diag.Title = "Import File";
            DialogResult res = diag.ShowDialog();

            if (res == DialogResult.OK)
            {
                //List<string> symbols = Utility.SymbolImport.FromFile(diag.FileName);
                //foreach (string symbol in symbols)
                //{
                //    //updateMgr.AddSymbol(symbol);
                //    rowsMgr.AddSymbol(symbol);

                //}
            }
        }

        //public void OnMMQuote(MMQuoteData quote)
        //{

        //}

        //public void OnOptionChainSnapshot(OptionChainData optionChain)
        //{

        //}

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                rowsMgr.UpdateToGrid(updateMgr._quoteUpdates);
                if (updateMgr._quoteUpdates.ContainsKey(paneSymbol))
                {
                    QuoteMonitorUpdateData update = updateMgr._quoteUpdates[paneSymbol];
                    UpdatePaneQuotes(update);
                }
                
            }
            catch(Exception exx)
            {
                //logger.Log(exx);
            }
        }

        private void UpdatePaneQuotes(QuoteMonitorUpdateData update)
        {

            labelBid.Text = update.Bid.ToString("N2");
            labelAsk.Text = update.Ask.ToString("N2");
            labelLow.Text = update.Low.ToString("N2");
            labelHigh.Text = update.High.ToString("N2");
            labelVolume.Text = update.Volume.ToString("N0");
            if (!double.IsNaN(update.Change))
            {
                labelChange.Text = update.Change.ToString("N2");
            }
            labelClose.Text = update.Close.ToString("N2");
            labelOpen.Text = update.Open.ToString("N2");
            label30dVol.Text = update.Volatility.ToString("N2");
            labelVWAP.Text = update.VWAP.ToString("N5");
            labelDesc.Text = "(" + update.Description + ")";
            labelWk52H.Text = update.Wk52h.ToString("N2");
            labelWk52L.Text = update.Wk52l.ToString("N2");
            labelLast.Text = update.Last.ToString("N2");
            labelPExchange.Text = update.PExchgShort;
            lbl30dADV.Text = update.ADV30d.ToString("N0");
            lbl30dPx.Text = update.AvgPx30d.ToString("N2");
        }

        Rectangle m_Bounds;
        private void toolStripButtonFullScr_Click(object sender, EventArgs e)
        {

            if (FormBorderStyle == FormBorderStyle.Sizable)
            {

                this.FormBorderStyle = FormBorderStyle.None;
                m_Bounds = this.Bounds;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                this.TopMost = true;
                Activate();
            }
            else
            {

                this.Bounds = m_Bounds;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
            }

        }

        //public void OnRegionalSnapshot(GQCore.MontageData montage)
        //{

        //}

        private void DoOrder(bool byCUSIP)
        {
            DoOrder(byCUSIP, false);
        }
        private void DoOrder(bool byCUSIP,bool IsTicket)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();

                if (symbol.StartsWith("/"))
                {
                    symbol = FormatFutureSymbol(symbol);
                }
                double px = double.Parse(dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Last].Value.ToString());
                string underlying = "";
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Underlying].Value != null)
                {
                    underlying = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Underlying].Value.ToString();
                }
                double strikePx = 0.0;
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.StrikePrice].Value != null)
                {
                    strikePx = double.Parse(dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.StrikePrice].Value.ToString());
                }
                string expiry= "";
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Expiry].Value != null)
                {
                    expiry = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Expiry].Value.ToString();
                }
                string cusip = "";
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.CUSIP].Value != null)
                {
                    dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.CUSIP].Value.ToString();
                }


              //  OrderEventArgs args = new OrderEventArgs();
                //args.symbol = symbol;
                //args.Underlying = underlying;
                //args.StrikePx = strikePx;
                //args.code = "LMT";
                //if (expiry != "")
                //{
                //    DateTime exp = DateTime.Parse(expiry);
                //    args.Expiry = exp.ToString("yyyyMMdd");
                //}
                //if (args.symbol[0] == '.')
                //{
                //    args.PutCall = XQ_Client.Helper.IsPut(symbol) ? "Put" : "Call";
                //}
                //if (IsTicket)
                //{
                //    args.IsDialog = true;
                //    args.side = OrdSide;
                //    args.Px = OrdPx;
                //}
                //else
                //{
                //    args.Px = px;
                //}
                //if (byCUSIP)
                //{
                //    args.CUSIP = cusip;
                //}
                //if (OnOrder != null)
                //{
                //    OnOrder(args);
                //}
            }
        }

        private string FormatFutureSymbol(string symbol)
        {
            string[] splits = symbol.Split(':');
            string futureSymbol = "";
            if (splits.Length >= 2)
            {
                futureSymbol = "/" + splits[1];
                char lastChar = futureSymbol[futureSymbol.Length - 1];
                futureSymbol = futureSymbol.Replace(lastChar, ' ').Trim();
                string last_Two = futureSymbol.Substring(3, 2);
                futureSymbol = futureSymbol.Replace(last_Two, "").Trim() + lastChar.ToString();
                futureSymbol += last_Two[1].ToString();
            }

            return futureSymbol;

        }

        private void eXXZacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                string URL = @"http://www.zacks.com/research/report.php?t={0}&type=main&x=0&y=0";
                System.Diagnostics.Process.Start(string.Format(URL, symbol));

            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    //dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
                    rowsMgr.DeleteRowAtIndex(dataGridView.SelectedRows[0].Index);
                }

                //foreach (DataGridViewCell item in dataGridView.SelectedRows[0].Cells)
                //{
                //    item.Value = "";
                //}
                

            }
            catch
            { 
            }

        }

        private void insertRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                dataGridView.Rows.Insert(dataGridView.SelectedRows[0].Index, 1);
            }

        }

        private LabelRowDialog labelDiag = null;
        private void insertLabelRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (labelDiag == null)
                {
                    labelDiag = new LabelRowDialog();
                }

                labelDiag.ShowDialog(this);
                if (labelDiag.result == DialogResult.OK)
                {
                    rowsMgr.InsertLabelRow(labelDiag.textBoxCaption.Text, dataGridView.SelectedRows[0].Index);
                }
            }
        }

        private void toolStripTextBoxLabel_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    rowsMgr.InsertLabelRow(toolStripTextBoxLabel.Text, dataGridView.SelectedRows[0].Index);
                    contextMenuStripOrder.Hide();

                }
            }

        }

        private void toolStripTextBoxLabel_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxLabel.Text == "(Type Label here)")
            {
                toolStripTextBoxLabel.Text = "";
            }

        }

        private void toolStripButtonReadDefault_Click(object sender, EventArgs e)
        {
            try
            {
                XQ_Client.Workspace.WorkSpace ws = XQ_Client.Workspace.WorkSpace.FromFile(Application.UserAppDataPath + "\\default.xntw");
                XQ_Client.Workspace.WorkSpaceEntry qmon = null;


                if (ws != null)
                {
                    for (int i = 0; i < ws.WorkSpaceItems.Length; i++)
                    {
                        if (ws.WorkSpaceItems[i].Name == "QuoteMonitorWindow")
                        {
                            qmon = ws.WorkSpaceItems[i];
                            break;
                        }
                    }

                    if (qmon != null)
                    {
                        string[] symbols = qmon.ParamList[0].Split(',');
                        if (symbols.Length > 0)
                        {
                            rowsMgr.ClearGrid();
                            for (int i = 0; i < symbols.Length; i++)
                            {
                                rowsMgr.AddSymbol(symbols[i]);
                            }
                        }
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(new Form() { TopMost = true, TopLevel = true },"There is no Quote Monitor saved in your workspace file", "Load default", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exx)
            {


            }

        }

        private void viewLevelIIMontageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                    //if (OnLevelII != null)
                    //{
                    //    OrderEventArgs args = new OrderEventArgs();
                    //    args.symbol = symbol;
                    //    //OnOrder(args);
                    //    OnLevelII(args);
                    //}
                }
        }

        private void viewOptionChainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();

                    //if (OnLevelII != null)
                    //{
                    //    OrderEventArgs args = new OrderEventArgs();
                    //    args.symbol = symbol;
                    //    OnChain(args);
                    //}
            }
        }

        #region INunnaForm Members

        public void SetParam(string[] param)
        {
            try
            {
                if (param != null && param.Length > 0)
                {
                    if (param.Length >= 2)
                    {
                        this.setting = GridSettings.FromString(param[2]);
                        ApplySettings();
                    }
                    param[0] = param[0].Trim(',');
                    string[] symbols = param[0].Split(new char[] { ',' });
                    dataGridView.Rows.Clear();
                    bool rowInsertAllowed = false;
                    for (int i = 0; i < symbols.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(symbols[i]))
                            rowInsertAllowed = true;
                        if (rowInsertAllowed)
                        {
                            if (symbols[i].Contains("#"))
                            {
                                string[] label = symbols[i].Split('#');
                                rowsMgr.AddLabel(label[0], int.Parse(label[1]));
                            }
                            else
                            {
                                rowsMgr.AddSymbol(symbols[i]);
                            }
                        }
                    }
                    dataGridView.Rows.Insert(dataGridView.Rows.GetLastRow(DataGridViewElementStates.None), 50);

                    SetColumnSettings(param[1]);
                }
            }
            catch (Exception exx)
            {

                DialogResult result = MessageBox.Show(new Form() { TopMost = true, TopLevel = true },"Unable to restore Quote Monitor due to error." + exx.Message + exx.StackTrace);
            }

        }

        public string[] GetParam()
        {

            return new string[] { rowsMgr.GetSymbolsAsCSV(), GetColumnSettings(), setting.ToString() };
        }

        public void InitializeDialog()
        {
            Show();
        }

        #endregion

        private string GetColumnSettings()
        {
            string columnSetting = "";

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                columnSetting += column.Name + ",";
                columnSetting += column.Visible.ToString() + ",";
                columnSetting += column.DisplayIndex.ToString() + ",";
                columnSetting += column.Width.ToString() + ":";
            }
            return columnSetting;
        }

        private void SetColumnSettings(string columnSetting)
        {

            foreach (string column_setting in columnSetting.Split(':'))
            {
                DataGridViewColumn column = dataGridView.Columns[column_setting.Split(',')[0]];

                if (column != null)
                {
                    column.DisplayIndex = Convert.ToInt16(column_setting.Split(',')[2]);

                    column.Visible = Convert.ToBoolean(column_setting.Split(',')[1]);

                    column.Width = Convert.ToInt16(column_setting.Split(',')[3]);
                }
            }

          //  menusel.SelectedDataGridView = dataGridView;


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (OnSaveWorkSpace != null)
            //{
            //    OnSaveWorkSpace(null);
            //}

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //logger.Log("REFRESH CLICKED");
                //logger.Log("UpdateManagerSymbolCount=" + updateMgr._quoteUpdates.Count.ToString());
                if (updateMgr != null)
                {
                    //updateMgr.stream.Purge();
                }
                updateMgr.RequestAll();
            }
            catch(Exception exx)
            {
                //logger.Log(exx);
            }

        }
        private delegate void OnConnectionStatusDelegate(ConnectionStatus status);
        public void OnConnectionStatus(ConnectionStatus status)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new OnConnectionStatusDelegate(OnConnectionStatus), new object[] { status });
                    return;
                }
                else
                {
                    //logger.Log("OMS-QMON got Ready");
                    labelConnectionStatus.Text = status.ToString().Replace('_', ' ');
                    //logger.Log("OMS-QMON now going to re-register");
                   // updateMgr.RequestAll();
                }
            }
            catch (Exception exx)
            {
                //logger.Log("OMS-QMON got error OnConnectionStat(): " + exx.ToString());
            }
        }
        private void earningCalenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XQ_Client.GlobalSettings gs = XQ_Client.GlobalSettings.GetSettings();

            if (!gs.downloadEarningsData)
            {
                try
                {
                    System.Diagnostics.Process.Start(@"http://www.wallstreethorizon.com/omex.asp");
                    return;
                }
                catch
                { }
            }
            
            //EarningsCalenderAPI.Earnings en = columns.GetEarnings;
            //EarningsCalenderAPI.EPS ep = columns.GetEPS;


            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim();

                //if (ep != null)
                //{
                //    if (ep.SymbolExists(symbol))
                //        OnEarningWindowOpening(symbol, false);
                //}
                //if (en != null)
                //{
                //    if (en.SymbolExists(symbol))
                //    {
                //        OnEarningWindowOpening(symbol, true);
                //    }
                //}
            }

            //    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value;
            //    if (earnings != null && earnings.SymbolExists(symbol))
            //    {
            //        if (OnEarningsCalenderClicked != null)
            //        {
            //            OnEarningsCalenderClicked(symbol);
            //        }
            //    }

            //    if (eps != null && eps.SymbolExists(symbol))
            //    {
            //        if (OnEarningsCalenderClicked != null)
            //        {
            //            OnEPSClicked(symbol);
            //        }
            //    }
            //}

        }



        void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView.RowCount > 1)
            {

                if (e.Control is DataGridViewTextBoxEditingControl)
                {

                    DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    te.AllowDrop = true;
                    te.AutoCompleteCustomSource.AddRange(new string[] { "/CME(020)", "/COMEX(023)", "/CBOT(024)", "/NYMX(25)", "/NYMEX(044)" });
                    te.TextChanged += new EventHandler(te_TextChanged);
                }
            }

        }

        void te_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                t.Text = "/" + t.Text.Split('(')[1].Trim(')') + ":";
            }
            catch
            {


            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            try
            {
               quoteProcessor.Shutdown();
                //realtime.RemoveSymbol(new string[] { "88888" });
                //snapShot.Dispose();
                //realtime = null;
                //snapShot = null;
            }
            catch
            { }
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://omexsystems.com/oms/help/symbology.htm");
        }

        private void addAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim();
                string px = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Last].Value.ToString().Trim();
              // OrderEventArgs args = new OrderEventArgs();
               // args.symbol = symbol;
              //  args.Px = double.Parse(px);
               // OnAlert(args);
            }

            
            
        }

        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                
            }
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            try
            {

                if (e.Data.GetData(typeof(object)) is LevelIIControl.OptionDataObject)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            catch
            {

            }

        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetData(typeof(object)) is LevelIIControl.OptionDataObject)
                {
                    LevelIIControl.OptionDataObject obj = (LevelIIControl.OptionDataObject)e.Data.GetData(typeof(object));
                    Point p = dataGridView.PointToClient(MousePosition);
                    int i = dataGridView.HitTest(p.X, p.Y).RowIndex;
                    rowsMgr.AddSymbol(obj.OSISymbol, i);
                    
                }
            }
            catch
            { }
        }
        private void AddToPane()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
                {
                    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                    paneSymbol = symbol;
                    splitContainer.Panel2Collapsed = false;
                    textBoxSymbol.Text = symbol;
                }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddToPane();
            GetSelectedRowData(e.RowIndex,e.ColumnIndex);
        }

        private void viewChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
                {
                    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                    try
                    {
                        //if (OnCharts != null)
                        //{
                        //    OnCharts(symbol);
                        //}
                        ////OMEXChart.Charts c = new OMEXChart.Charts(symbol);
                        //c.Show();
                    }
                    catch
                    { }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            try
            {
            //    OMEXChart.Charts c = new OMEXChart.Charts(paneSymbol);
            //    c.Show();
            }
            catch
            { }
        }

        private void textBoxSymbol_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paneSymbol = textBoxSymbol.Text;
                updateMgr.AddSymbol(paneSymbol);
                
            }
        }
        void toolStripMarketSummary_Click(object sender, System.EventArgs e)
        {
           // new TimeAndSales.MarketSummary().Show();
        }

        private void toolStripButton4_Click (object sender, EventArgs e)
        {
           // new TimeAndSales.ETFList().Show();
        }

        private void toolStripMenuItemTimeAndSales_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                    string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                    try
                    {
                        //TimeAndSales.TimeSales ts = new TimeAndSales.TimeSales();
                        //ts.SetSymbol(symbol);
                        //ts.Show();
                    }
                    catch
                    { }
            }
        }

        private void toolStripButtonImbalance_Click(object sender, EventArgs e)
        {
            //TimeAndSales.DisbalanceFeed df = new TimeAndSales.DisbalanceFeed();
            //df.StartPosition = FormStartPosition.CenterParent;
            //df.Show();
        }


        

        //public void OnImbalance(GQCore.ImbalanceData trade)
        //{
            
        //}

        private void yahooQuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
            {
                string symbol = dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                string URL = @"http://finance.yahoo.com/q?s={0}";
                System.Diagnostics.Process.Start(string.Format(URL, symbol));

            }
        }
        public string OrdSide = "B";
        public double OrdPx = 0;
        public void GetSelectedRowData(int RowInd,int Col)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value != null && dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim() != "")
                {
                    string s = dataGridView.Columns[Col].Name;
                    if (s == QuoteMonitorColumnNames.Bid || s == QuoteMonitorColumnNames.BidSize)
                    {
                        OrdSide = "S";
                        OrdPx = double.Parse(dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Bid].Value.ToString());
                    }
                    else
                    {
                        if (s == QuoteMonitorColumnNames.Ask || s == QuoteMonitorColumnNames.AskSize)
                        {
                            OrdSide = "B";
                            OrdPx = double.Parse(dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Ask].Value.ToString());
                        }
                        else
                        {
                            OrdSide = "B";
                            OrdPx = double.Parse(dataGridView.SelectedRows[0].Cells[QuoteMonitorColumnNames.Last].Value.ToString());
                        }
                    }
                    DoOrder(false, true);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //new Misc.IndexList().Show();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            
        }
    }
}