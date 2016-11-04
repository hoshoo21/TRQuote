using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TRQuoteCore.GridStructure;
using TRQuoteCore.QuoteStructure;

namespace TRQuoteCore.WindowsHelperClasses
{
    class QuoteMonitorRowsManager
    {
        private DataGridView grid;
        //private IQuoteMonitorUpdateManager updateMgr;
        private Dictionary<string, List<DataGridViewRow>> indexedRows;
        private IQuoteMonitorUpdateManager updateMgr;
        private TRQuoteCore.Core.QuoteProcessor processor;
        
        public QuoteMonitorRowsManager(DataGridView gridView,IQuoteMonitorUpdateManager updateManager,TRQuoteCore.Core.QuoteProcessor  processor)
        {
            this.processor = processor;
            grid = gridView;
            updateMgr = updateManager;

            indexedRows = new Dictionary<string, List<DataGridViewRow>>();
            grid.CellEndEdit += new DataGridViewCellEventHandler(grid_CellEndEdit);
            grid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(grid_RowsRemoved);

        }

        public void ClearGrid()
        {
            grid.Rows.Clear();
            indexedRows.Clear();
            ValidateRows();
            //this.UnsubscribeAll();

        }

        public void DeleteRowAtIndex(int index)
        {
            try
            {
                //indexedRows.Remove(grid.Rows[index].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString());
                grid.Rows.RemoveAt(index);
            }
            catch
            { }

        }


        public void InsertLabelRow(string caption, int index)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(grid);

            row.Cells[3].Value = caption;
            row.DefaultCellStyle.BackColor = SystemColors.Highlight;
            row.DefaultCellStyle.ForeColor = SystemColors.HighlightText;
            row.DefaultCellStyle.Font = new Font(grid.DefaultCellStyle.Font, FontStyle.Bold);
            row.ContextMenuStrip = null;
            grid.Rows.Insert(index, row);

        }

        void grid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ValidateRows();
        }

        void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = grid.Rows[e.RowIndex];
            OnNewSymbol(row, true);
        }

        private void OnNewSymbol(DataGridViewRow row, bool isKeyBoardInput)
        {
            if (row.Cells[QuoteMonitorColumnNames.Symbol].Value != null && row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString() != string.Empty)
            {
                //Change Symbol case to upper
                string symbol = string.Empty;
                if (isKeyBoardInput)
                {
                    if (row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().StartsWith("\""))
                    {
                        symbol = row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().Trim(new char[] { '"' });
                    }
                    else
                    {
                        symbol = row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString().ToUpper();
                    }
                }
                else
                {
                    symbol = row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();

                }
                row.Cells[0].Value = symbol;

                lock (indexedRows)
                {
                    if (indexedRows.ContainsKey(symbol))
                    {
                        indexedRows[symbol].Add(row);
                    }
                    else
                    {
                        indexedRows.Add(symbol, new List<DataGridViewRow>());
                        indexedRows[symbol].Add(row);

                    }
                }

                updateMgr.AddSymbol(symbol);

            }

            if (row.Cells[QuoteMonitorColumnNames.Symbol].Value == null)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Value = null;
                }
            }
        }

        public void AddSymbol(string symbol)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.DefaultCellStyle = grid.DefaultCellStyle;
            row.Height = grid.RowTemplate.Height;
            row.CreateCells(grid, new object[] { symbol });
            row.Cells[0].Value = symbol;
            grid.Rows.Add(row);
            OnNewSymbol(row, false);
        }

        public void AddLabel(string label, int rowIndx)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.DefaultCellStyle.BackColor = SystemColors.Highlight;
            row.DefaultCellStyle.ForeColor = Color.White;
            row.DefaultCellStyle.Font = new Font(grid.Font.FontFamily, grid.Font.Size, FontStyle.Regular);
            row.Height = grid.RowTemplate.Height;
            grid.Rows.Add(row);
            grid.Rows[rowIndx].Cells[QuoteMonitorColumnNames.Bid].Value = label;
        }

        public void AddSymbol(string symbol, int index)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.DefaultCellStyle = grid.DefaultCellStyle;
            row.Height = grid.RowTemplate.Height;
            row.CreateCells(grid, new object[] { symbol });
            row.Cells[0].Value = symbol;
            grid.Rows.Insert(index, row);
            OnNewSymbol(row, false);
        }

        public void UpdateComposite(CompositeData data)
        {
            lock (indexedRows)
            {
                try
                {
                    if (indexedRows.ContainsKey(data.Symbol))
                    {
                        List<DataGridViewRow> rows = indexedRows[data.Symbol];

                        for (int i = 0; i < rows.Count; i++)
                        {
                            rows[i].Cells[QuoteMonitorColumnNames.Ask].Value = data.Ask;
                            rows[i].Cells[QuoteMonitorColumnNames.AskSize].Value = data.Asksize;
                            rows[i].Cells[QuoteMonitorColumnNames.Bid].Value = data.Bid;
                            rows[i].Cells[QuoteMonitorColumnNames.BidSize].Value = data.Bidsize;
                            rows[i].Cells[QuoteMonitorColumnNames.TradeTime].Value = data.TradeTime;
                            rows[i].Cells[QuoteMonitorColumnNames.AskTime].Value = data.AskTime;
                            rows[i].Cells[QuoteMonitorColumnNames.BidTime].Value = data.BidTime;

                            if (data.Last == 0)
                            {
                                rows[i].Cells[QuoteMonitorColumnNames.Last].Value = data.PrevClose;
                            }
                            else
                            {
                                rows[i].Cells[QuoteMonitorColumnNames.Last].Value = data.Last;
                            }
                            rows[i].Cells[QuoteMonitorColumnNames.Change].Value = data.Netchange;
                            rows[i].Cells[QuoteMonitorColumnNames.Close].Value = data.PrevClose;
                            rows[i].Cells[QuoteMonitorColumnNames.CompanyName].Value = data.Description.Replace('_', ' ');
                            rows[i].Cells[QuoteMonitorColumnNames.CUSIP].Value = data.CUSIP;
                            rows[i].Cells[QuoteMonitorColumnNames.Currency].Value = data.Currency;
                            rows[i].Cells[QuoteMonitorColumnNames.ISIN].Value = data.ISIN;
                            rows[i].Cells[QuoteMonitorColumnNames.Exchange].Value = "Not Avaliable";
                            rows[i].Cells[QuoteMonitorColumnNames.Expiry].Value = data.OptionExpire;
                            rows[i].Cells[QuoteMonitorColumnNames.High].Value = data.High;
                            rows[i].Cells[QuoteMonitorColumnNames.Low].Value = data.Low;
                            rows[i].Cells[QuoteMonitorColumnNames.Open].Value = data.Open;
                            rows[i].Cells[QuoteMonitorColumnNames.OpenInterest].Value = data.OpenInterest;
                            rows[i].Cells[QuoteMonitorColumnNames.PrimaryExchange].Value = processor.getPrimaryExchange(data.Exchange);
                            rows[i].Cells[QuoteMonitorColumnNames.StrikePrice].Value = data.OptionStrikePrice;
                            rows[i].Cells[QuoteMonitorColumnNames.TradeTime].Value = "Not Avaliable";
                            rows[i].Cells[QuoteMonitorColumnNames.Underlying].Value = data.UnderlyingSymbol;
                            rows[i].Cells[QuoteMonitorColumnNames.Volume].Value = data.Volume;
                            rows[i].Cells[QuoteMonitorColumnNames.Wk52High].Value = data.High52;
                            rows[i].Cells[QuoteMonitorColumnNames.Wk52Low].Value = data.Low52;

                            double percentChange = 0;
                            try
                            {
                                percentChange = (data.Netchange / data.PrevClose) * 100;
                                rows[i].Cells[QuoteMonitorColumnNames.ChangePercent].Value = percentChange;

                            }
                            catch
                            {
                                //Any Maths Exception.
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    string text = ex.Message;
                }
            }

        }

        private void ValidateRows()
        {
            lock (indexedRows)
            {
                List<string> keysToDelete = new List<string>();
                try
                {

                    foreach (string symbol in indexedRows.Keys)
                    {
                        List<DataGridViewRow> rowsList = indexedRows[symbol];
                        List<DataGridViewRow> toRemove = new List<DataGridViewRow>();

                        foreach (DataGridViewRow row in rowsList)
                        {
                            if (row.Index ==-1)
                            {
                                    toRemove.Add(row);
                            }
                        }

                        foreach (DataGridViewRow row in toRemove)
                        {
                            rowsList.Remove(row);
                        }

                        if (rowsList.Count == 0)
                        {
                           updateMgr.RemoveSymbol(symbol);
                            keysToDelete.Add(symbol);

                        }
                    }
                    foreach (string symbol in keysToDelete)
                    {
                        indexedRows.Remove(symbol);
                    }
                }
                catch
                {

                }
            }
        }

        public string GetSymbolsAsCSV()
        {
            string csvList = string.Empty;
            try
            {
                bool rowInsertAllowed = false;
                int Indx = 0;
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    if (grid.Rows[i].Cells[QuoteMonitorColumnNames.Symbol].Value != null)
                        rowInsertAllowed = true;
                    if (rowInsertAllowed)
                    {
                        if (grid.Rows[i].Cells[QuoteMonitorColumnNames.Symbol].Value == null)
                        {
                            if (grid.Rows[i].Cells[QuoteMonitorColumnNames.Bid].Value == null)
                            {
                                csvList += ",";
                            }
                            else
                            {
                                csvList += grid.Rows[i].Cells[QuoteMonitorColumnNames.Bid].Value.ToString() + "#" + Indx + ",";
                            }
                        }
                        else
                        {
                            csvList += grid.Rows[i].Cells[QuoteMonitorColumnNames.Symbol].Value.ToString() + ",";
                        }
                        Indx++;
                    }

                }
            }
            catch
            {
                return csvList;

            }

            return csvList;
        }
        private double ConditionalRound(double value)
        {
            if (value > 1)
            {
                return Math.Round(value, 2);
            }
            else
            {
                return Math.Round(value, 4);
            }

        }

        public void UpdateToGrid(Dictionary<string, QuoteMonitorUpdateData> updated)
        {
            ValidateRows();
            lock (indexedRows)
            {
                foreach (string symbol in indexedRows.Keys)
                {
                    List<DataGridViewRow> rowsList = indexedRows[symbol];

                    foreach (DataGridViewRow row in rowsList)
                    {

                        if (row.Cells[QuoteMonitorColumnNames.Symbol].Value != null)
                        {
                            string row_symbol = row.Cells[QuoteMonitorColumnNames.Symbol].Value.ToString();
                            string tempsymbol = symbol;
                            if (symbol == row_symbol && updated.ContainsKey(tempsymbol))
                            {
                                row.Cells[QuoteMonitorColumnNames.Ask].Value = ConditionalRound(updated[tempsymbol].Ask);
                                row.Cells[QuoteMonitorColumnNames.AskSize].Value = ConditionalRound(updated[tempsymbol].AskSize);
                                row.Cells[QuoteMonitorColumnNames.Bid].Value = ConditionalRound(updated[tempsymbol].Bid);
                                row.Cells[QuoteMonitorColumnNames.BidSize].Value = updated[tempsymbol].BidSize;
                                if (!double.IsNaN(updated[tempsymbol].Change))
                                {
                                    row.Cells[QuoteMonitorColumnNames.Change].Value = ConditionalRound(updated[tempsymbol].Change);
                                }

                                row.Cells[QuoteMonitorColumnNames.Exchange].Value = processor.getTradeExchange( updated[tempsymbol].Exchange);
                                row.Cells[QuoteMonitorColumnNames.High].Value = ConditionalRound(updated[tempsymbol].High);
                                row.Cells[QuoteMonitorColumnNames.Last].Value = ConditionalRound(updated[tempsymbol].Last);
                                row.Cells[QuoteMonitorColumnNames.Low].Value = ConditionalRound(updated[tempsymbol].Low);
                                row.Cells[QuoteMonitorColumnNames.Open].Value = ConditionalRound(updated[tempsymbol].Open);
                                row.Cells[QuoteMonitorColumnNames.Close].Value = ConditionalRound(updated[tempsymbol].Close);
                                row.Cells[QuoteMonitorColumnNames.TradeTime].Value = updated[tempsymbol].TradeTime;
                                row.Cells[QuoteMonitorColumnNames.Volume].Value = updated[tempsymbol].Volume;
                                row.Cells[QuoteMonitorColumnNames.BidTime].Value = updated[tempsymbol].BidTime;
                                row.Cells[QuoteMonitorColumnNames.AskTime].Value = updated[tempsymbol].AskTime;

                                if (!double.IsNaN(updated[tempsymbol].PrecentChange))
                                {
                                    row.Cells[QuoteMonitorColumnNames.ChangePercent].Value = updated[tempsymbol].PrecentChange;
                                }
                                //row.Cells[QuoteMonitorColumnNames.Volatility].Value = updated[symbol].Volatility;
                                row.Cells[QuoteMonitorColumnNames.AvgPx30d].Value = updated[symbol].AvgPx30d;
                                //row.Cells[QuoteMonitorColumnNames.ADV30d].Value = updated[symbol].ADV30d;
                                row.Cells[QuoteMonitorColumnNames.VWAP].Value = updated[tempsymbol].VWAP;
                                row.Cells[QuoteMonitorColumnNames.CompanyName].Value = updated[tempsymbol].Description;

                                row.Cells[QuoteMonitorColumnNames.DIV].Value = updated[symbol].Div;
                               // row.Cells[QuoteMonitorColumnNames.MktCap].Value = updated[symbol].MktCap;
                            }
                        }
                    }
                }
            }
        }
    }
}
