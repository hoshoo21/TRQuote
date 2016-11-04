using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using System.Drawing;
using TRQuoteCore.GridStructure;

namespace TRQuoteCore.WindowsHelperClasses
{
        class ColorCodedCell : DataGridViewTextBoxCell
        {
            private Color lowColor = Color.Tomato;

            public Color LowColor
            {
                get
                {
                    if (this.OwningColumn == null || OwningColorCodedColumn == null)
                    {
                        return lowColor;
                    }
                    else
                    {
                        return OwningColorCodedColumn.LowColor;
                    }
                }
                set { lowColor = value; }
            }

            private Color highColor = Color.Green;

            public Color HighColor
            {
                get
                {
                    if (this.OwningColumn == null || OwningColorCodedColumn == null)
                    {
                        return highColor;
                    }
                    else
                    {
                        return OwningColorCodedColumn.HighColor;

                    }
                }
                set { highColor = value; }
            }

            private bool changeBackColor = false;

            /// <summary>
            /// If set to true then the BackColor of cell is changed according to value otherwise
            /// the forecolor is changed.
            /// </summary>
            public bool ChangeBackColor
            {
                get
                {
                    if (this.OwningColumn == null || OwningColorCodedColumn == null)
                    {
                        return changeBackColor;
                    }
                    else
                    {
                        return OwningColorCodedColumn.ChangeBackColor;

                    }
                }
                set { changeBackColor = value; }
            }



            private double cellVal = 0;
            protected override bool SetValue(int rowIndex, object value)
            {
                if (value != null)
                {
                    double cellValue = 0;
                    double.TryParse(value.ToString(), out cellValue);
                    cellVal = cellValue;

                    if (cellValue >= 0)
                    {


                        Style.ForeColor = HighColor;



                    }
                    else if (cellValue < 0)
                    {

                        Style.ForeColor = LowColor;


                    }
                }

                return base.SetValue(rowIndex, value);
            }

            private ColorCodedColumn OwningColorCodedColumn
            {
                get { return this.OwningColumn as ColorCodedColumn; }
            }
            public override object Clone()
            {
                ColorCodedCell c = base.Clone() as ColorCodedCell;
                c.HighColor = HighColor;
                c.LowColor = LowColor;
                c.ChangeBackColor = ChangeBackColor;
                return c;
            }

        }
        class ColorCodedColumn : DataGridViewTextBoxColumn
        {
            private Color lowColor = Color.Tomato;

            public Color LowColor
            {
                get { return lowColor; }
                set { lowColor = value; }
            }

            private Color highColor = Color.LightGreen;

            public Color HighColor
            {
                get { return highColor; }
                set { highColor = value; }
            }

            private bool changeBackColor = false;

            /// <summary>
            /// If set to true then the BackColor of cell is changed according to value otherwise
            /// the forecolor is changed.
            /// </summary>
            public bool ChangeBackColor
            {
                get { return changeBackColor; }
                set { changeBackColor = value; }
            }


            public ColorCodedColumn()
            {
                CellTemplate = new ColorCodedCell();
            }


            public override object Clone()
            {
                ColorCodedColumn c = base.Clone() as ColorCodedColumn;
                c.HighColor = HighColor;
                c.LowColor = LowColor;
                c.ChangeBackColor = ChangeBackColor;
                return c;
            }

            private ColorCodedCell ColorCodedCellTemplate
            {
                get { return this.CellTemplate as ColorCodedCell; }
            }
        }
        class QuoteMonitorColumns
        {
            private DataGridView dgv;

            private TextAndImageColumnN symbolColumn;
            private TextAndImageColumnX lastColumn;
            private DataGridViewColumn bidColumn;
            private DataGridViewColumn bidtimeColumn;
            private DataGridViewColumn asktimecolumn;
            private DataGridViewColumn askColumn;
            private DataGridViewColumn bidSizeColumn;
            private DataGridViewColumn askSizeColumn;
            private ColorCodedColumn changeColumn;
            private DataGridViewColumn volumeColumn;
            private DataGridViewColumn companyNameColumn;
            private DataGridViewColumn closeColumn;
            private DataGridViewColumn exchangeColumn;
            private DataGridViewColumn rootColumn;
            private DataGridViewColumn strikePxColumn;
            private DataGridViewColumn expiryColumn;
            private DataGridViewColumn highColumn;
            private DataGridViewColumn lowColumn;
            private DataGridViewColumn wk52HighColumn;
            private DataGridViewColumn wk52LowColumn;
            private DataGridViewColumn tradeTimeColumn;
            private DataGridViewColumn primaryExchangeColumn;
            private DataGridViewColumn isinColumn;
            private DataGridViewColumn currencyColumn;
            private DataGridViewColumn openInterestColumn;
            private DataGridViewColumn cusipColumn;
            private DataGridViewColumn openColumn;
            private ColorCodedColumn changePrecentColumn;
            private DataGridViewTextBoxColumn volatilityColumn;
            private DataGridViewTextBoxColumn AvgPx30dColumn;
            private DataGridViewTextBoxColumn ADV30dColumn;
            private DataGridViewTextBoxColumn VWAPColumn;
            private DataGridViewTextBoxColumn MktCapColumn;
            private DataGridViewTextBoxColumn DivColumn;
            




            

            public QuoteMonitorColumns(DataGridView dataGridView)
            {
                symbolColumn = new TextAndImageColumnN();
                symbolColumn.HeaderText = QuoteMonitorColumnNames.Symbol;
                symbolColumn.DisplayImage =  global::TRQuoteCore.Properties.Resources.Star_Gold;
                symbolColumn.DisplayImage2 = Properties.Resources.Star_Red;

                symbolColumn.ReadOnly = false;
                symbolColumn.Frozen = true;
                symbolColumn.Name = QuoteMonitorColumnNames.Symbol;
                symbolColumn.Width = 90;



                lastColumn = new TextAndImageColumnX();
                lastColumn.UpArrowImage = Properties.Resources.upImage;
                lastColumn.DownArrowImage = Properties.Resources.downImage;
                lastColumn.HeaderText = QuoteMonitorColumnNames.Last;
                lastColumn.ReadOnly = true;
                lastColumn.Name = QuoteMonitorColumnNames.Last;
                lastColumn.Width = 90;
                //  lastColumn.DefaultCellStyle.Format = "N2";




                bidColumn = new DataGridViewTextBoxColumn();
                bidColumn.HeaderText = QuoteMonitorColumnNames.Bid;
                bidColumn.ReadOnly = true;
                bidColumn.Name = QuoteMonitorColumnNames.Bid;
                bidColumn.Width = 60;
                //    bidColumn.DefaultCellStyle.Format = "N2";

                bidtimeColumn = new DataGridViewTextBoxColumn();
                bidtimeColumn.HeaderText = QuoteMonitorColumnNames.BidTime;
                bidtimeColumn.ReadOnly = true;
                bidtimeColumn.Name = QuoteMonitorColumnNames.BidTime;
                bidtimeColumn.Width = 130;
                bidtimeColumn.DefaultCellStyle.Format = "hh:mm:ss tt";
                bidtimeColumn.ValueType = typeof(DateTime);

                asktimecolumn = new DataGridViewTextBoxColumn();
                asktimecolumn.HeaderText = QuoteMonitorColumnNames.AskTime;
                asktimecolumn.ReadOnly = true;
                asktimecolumn.Name = QuoteMonitorColumnNames.AskTime;
                asktimecolumn.Width = 130;
                asktimecolumn.DefaultCellStyle.Format = "hh:mm:ss tt";
                asktimecolumn.ValueType = typeof(DateTime);


                askColumn = new DataGridViewTextBoxColumn();
                askColumn.HeaderText = QuoteMonitorColumnNames.Ask;
                askColumn.ReadOnly = true;
                askColumn.Name = QuoteMonitorColumnNames.Ask;
                askColumn.Width = 60;
                //    askColumn.DefaultCellStyle.Format = "N2";


                bidSizeColumn = new DataGridViewTextBoxColumn();
                bidSizeColumn.HeaderText = QuoteMonitorColumnNames.BidSize;
                bidSizeColumn.ReadOnly = true;
                bidSizeColumn.Name = QuoteMonitorColumnNames.BidSize;
                bidSizeColumn.Width = 90;
                bidSizeColumn.DefaultCellStyle.Format = "N0";


                askSizeColumn = new DataGridViewTextBoxColumn();
                askSizeColumn.HeaderText = QuoteMonitorColumnNames.AskSize;
                askSizeColumn.ReadOnly = true;
                askSizeColumn.Name = QuoteMonitorColumnNames.AskSize;
                askSizeColumn.Width = 100;
                askSizeColumn.DefaultCellStyle.Format = "N0";


                changeColumn = new ColorCodedColumn();
                changeColumn.HeaderText = QuoteMonitorColumnNames.Change;
                changeColumn.ReadOnly = true;
                changeColumn.Name = QuoteMonitorColumnNames.Change;
                changeColumn.Width = 90;
                changeColumn.DefaultCellStyle.Format = "N4";
                changeColumn.HighColor = System.Drawing.Color.Green;


                changePrecentColumn = new ColorCodedColumn();
                changePrecentColumn.HeaderText = QuoteMonitorColumnNames.ChangePercent;
                changePrecentColumn.ReadOnly = true;
                changePrecentColumn.Name = QuoteMonitorColumnNames.ChangePercent;
                changePrecentColumn.Width = 90;
                changePrecentColumn.DefaultCellStyle.Format = "N2";
                changePrecentColumn.HighColor = System.Drawing.Color.Green;

                volumeColumn = new DataGridViewTextBoxColumn();
                volumeColumn.HeaderText = QuoteMonitorColumnNames.Volume;
                volumeColumn.ReadOnly = true;
                volumeColumn.Name = QuoteMonitorColumnNames.Volume;
                volumeColumn.Width = 100;
                volumeColumn.DefaultCellStyle.Format = "N0";

                companyNameColumn = new DataGridViewTextBoxColumn();
                companyNameColumn.HeaderText = QuoteMonitorColumnNames.CompanyName;
                companyNameColumn.Width = 150;
                companyNameColumn.ReadOnly = true;
                companyNameColumn.Name = QuoteMonitorColumnNames.CompanyName;


                closeColumn = new DataGridViewTextBoxColumn();
                closeColumn.HeaderText = QuoteMonitorColumnNames.Close;
                closeColumn.ReadOnly = true;
                closeColumn.Name = QuoteMonitorColumnNames.Close;
                closeColumn.Width = 60;
                //   closeColumn.DefaultCellStyle.Format = "N2";


                exchangeColumn = new DataGridViewTextBoxColumn();
                exchangeColumn.HeaderText = QuoteMonitorColumnNames.Exchange;
                exchangeColumn.ReadOnly = true;
                exchangeColumn.Name = QuoteMonitorColumnNames.Exchange;
                exchangeColumn.Width = 110;



                rootColumn = new DataGridViewTextBoxColumn();
                rootColumn.HeaderText = QuoteMonitorColumnNames.Underlying;
                rootColumn.ReadOnly = true;
                rootColumn.Name = QuoteMonitorColumnNames.Underlying;
                rootColumn.Width = 90;


                strikePxColumn = new DataGridViewTextBoxColumn();
                strikePxColumn.HeaderText = QuoteMonitorColumnNames.StrikePrice;
                strikePxColumn.ReadOnly = true;
                strikePxColumn.Name = QuoteMonitorColumnNames.StrikePrice;
                strikePxColumn.Width = 120;
                //    strikePxColumn.DefaultCellStyle.Format = "N2";


                expiryColumn = new DataGridViewTextBoxColumn();
                expiryColumn.HeaderText = QuoteMonitorColumnNames.Expiry;
                expiryColumn.ReadOnly = true;
                expiryColumn.Name = QuoteMonitorColumnNames.Expiry;
                expiryColumn.Width = 90;
                expiryColumn.ValueType = typeof(DateTime);
                expiryColumn.DefaultCellStyle.Format = "MM/dd/yyyy";





                highColumn = new DataGridViewTextBoxColumn();
                highColumn.HeaderText = QuoteMonitorColumnNames.High;
                highColumn.ReadOnly = true;
                highColumn.Name = QuoteMonitorColumnNames.High;
                highColumn.Width = 60;
                //    highColumn.DefaultCellStyle.Format = "N2";




                lowColumn = new DataGridViewTextBoxColumn();
                lowColumn.HeaderText = QuoteMonitorColumnNames.Low;
                lowColumn.ReadOnly = true;
                lowColumn.Name = QuoteMonitorColumnNames.Low;
                lowColumn.Width = 60;
                //    lowColumn.DefaultCellStyle.Format = "N2";


                wk52HighColumn = new DataGridViewTextBoxColumn();
                wk52HighColumn.HeaderText = QuoteMonitorColumnNames.Wk52High;
                wk52HighColumn.ReadOnly = true;
                wk52HighColumn.Name = QuoteMonitorColumnNames.Wk52High;
                wk52HighColumn.Width = 110;
                wk52HighColumn.DefaultCellStyle.Format = "N2";


                wk52LowColumn = new DataGridViewTextBoxColumn();
                wk52LowColumn.HeaderText = QuoteMonitorColumnNames.Wk52Low;
                wk52LowColumn.ReadOnly = true;
                wk52LowColumn.Name = QuoteMonitorColumnNames.Wk52Low;
                wk52LowColumn.Width = 110;
                wk52LowColumn.DefaultCellStyle.Format = "N2";


                tradeTimeColumn = new DataGridViewTextBoxColumn();
                tradeTimeColumn.HeaderText = QuoteMonitorColumnNames.TradeTime;
                tradeTimeColumn.ReadOnly = true;
                tradeTimeColumn.Name = QuoteMonitorColumnNames.TradeTime;
                tradeTimeColumn.Width = 130;
                tradeTimeColumn.DefaultCellStyle.Format = "hh:mm:ss tt";
                tradeTimeColumn.ValueType = typeof(DateTime);


                primaryExchangeColumn = new DataGridViewTextBoxColumn();
                primaryExchangeColumn.HeaderText = QuoteMonitorColumnNames.PrimaryExchange;
                primaryExchangeColumn.ReadOnly = true;
                primaryExchangeColumn.Name = QuoteMonitorColumnNames.PrimaryExchange;
                primaryExchangeColumn.Width = 200;

                openInterestColumn = new DataGridViewTextBoxColumn();
                openInterestColumn.HeaderText = QuoteMonitorColumnNames.OpenInterest;
                openInterestColumn.ReadOnly = true;
                openInterestColumn.Name = QuoteMonitorColumnNames.OpenInterest;
                openInterestColumn.Width = 130;
                openInterestColumn.DefaultCellStyle.Format = "N0";

                cusipColumn = new DataGridViewTextBoxColumn();
                cusipColumn.HeaderText = QuoteMonitorColumnNames.CUSIP;
                cusipColumn.ReadOnly = true;
                cusipColumn.Name = QuoteMonitorColumnNames.CUSIP;

                isinColumn = new DataGridViewTextBoxColumn();
                isinColumn.HeaderText = QuoteMonitorColumnNames.ISIN;
                isinColumn.ReadOnly = true;
                isinColumn.Name = QuoteMonitorColumnNames.ISIN;

                currencyColumn = new DataGridViewTextBoxColumn();
                currencyColumn.HeaderText = QuoteMonitorColumnNames.Currency;
                currencyColumn.ReadOnly = true;
                currencyColumn.Name = QuoteMonitorColumnNames.Currency;

                openColumn = new DataGridViewTextBoxColumn();
                openColumn.HeaderText = QuoteMonitorColumnNames.Open;
                openColumn.Name = QuoteMonitorColumnNames.Open;
                openColumn.ReadOnly = true;
                //     openColumn.DefaultCellStyle.Format = "N2";


          

                VWAPColumn = new DataGridViewTextBoxColumn();
                VWAPColumn.Name = QuoteMonitorColumnNames.VWAP;
                VWAPColumn.HeaderText = QuoteMonitorColumnNames.VWAP;
                VWAPColumn.Width = 50;
                VWAPColumn.ValueType = typeof(double);
                VWAPColumn.ReadOnly = true;
                VWAPColumn.DefaultCellStyle.Format = "N5";
                VWAPColumn.SortMode = DataGridViewColumnSortMode.Programmatic;

                MktCapColumn = new DataGridViewTextBoxColumn();
                MktCapColumn.Name = QuoteMonitorColumnNames.DIV;
                MktCapColumn.HeaderText = QuoteMonitorColumnNames.DIV;
                MktCapColumn.Width = 50;
                MktCapColumn.ValueType = typeof(double);
                MktCapColumn.ReadOnly = true;
                MktCapColumn.DefaultCellStyle.Format = "N5";

                AvgPx30dColumn = new DataGridViewTextBoxColumn();
                AvgPx30dColumn.Name = QuoteMonitorColumnNames.AvgPx30d;
                AvgPx30dColumn.HeaderText = QuoteMonitorColumnNames.AvgPx30d;
                AvgPx30dColumn.Width = 50;
                AvgPx30dColumn.ValueType = typeof(double);
                AvgPx30dColumn.ReadOnly = true;
                AvgPx30dColumn.DefaultCellStyle.Format = "N5";
  
                dataGridView.Columns.AddRange
                                        (
                                        symbolColumn, lastColumn, changeColumn, bidColumn,
                                        askColumn,
                                                   bidSizeColumn,
                                                   bidtimeColumn,
                                                   asktimecolumn,
                                        askSizeColumn,
                                        lowColumn,
                                        highColumn,
                                        volumeColumn,



                                        openColumn,

                                        changePrecentColumn,

                                        companyNameColumn,
                                        closeColumn,
                                        exchangeColumn,
                                        rootColumn,
                                        strikePxColumn,
                                        expiryColumn,
                                        wk52HighColumn,
                                        wk52LowColumn,
                                        tradeTimeColumn,
                                        primaryExchangeColumn,
                                        currencyColumn,
                                        isinColumn,
                                        openInterestColumn,
                                        cusipColumn,
                                          VWAPColumn,
                                          AvgPx30dColumn,
                                          MktCapColumn
                                    
                                        );
                dgv = dataGridView;

                FormatGrid();

            }

            public void FormatGrid()
            {

                dgv.CellMouseDown += new DataGridViewCellMouseEventHandler(grid_CellMouseDown);

            }

            void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (Control.ModifierKeys != Keys.Control && e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
                {
                    dgv.ClearSelection();
                    dgv.Rows[e.RowIndex].Selected = true;
                }
                else if (e.RowIndex == -1)
                {
                    dgv.ClearSelection();
                }
            }
        }
        public class TextAndImageCell : DataGridViewTextBoxCell
        {

            private double oldValue = 0;
            private Image imageValue;
            private Size imageSize;


            private Image upArrowImage;
            public Image UpArrowImage
            {
                get
                {
                    if (this.OwningColumn == null || this.OwningTextAndImageColumn == null)
                    {
                        return upArrowImage;
                    }

                    else if (this.upArrowImage != null)
                    {
                        return this.upArrowImage;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.UpArrowImage;
                    }
                }
                set { this.upArrowImage = value; }
            }


            private Image downArrowImage;

            public Image DownArrowImage
            {
                get
                {
                    if (this.OwningColumn == null ||
                       this.OwningTextAndImageColumn == null)
                    {
                        return downArrowImage;
                    }

                    else if (this.downArrowImage != null)
                    {
                        return this.downArrowImage;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.DownArrowImage;
                    }
                }
                set { this.downArrowImage = value; }
            }

            public Image Image
            {
                get
                {
                    if (this.OwningColumn == null ||
                         this.OwningTextAndImageColumn == null)
                    {
                        return imageValue;
                    }

                    else if (this.imageValue != null)
                    {
                        return this.imageValue;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.Image;
                    }
                }

                set
                {
                    if (this.imageValue != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.Style.Padding = new Padding(imageSize.Width, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom);
                    }
                }
            }

            public override object Clone()
            {
                TextAndImageCell c = base.Clone() as TextAndImageCell;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.upArrowImage = this.upArrowImage;
                c.downArrowImage = this.downArrowImage;
                c.oldValue = this.oldValue;
                return c;
            }
            protected override bool SetValue(int rowIndex, object value)
            {
                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                return base.SetValue(rowIndex, value);
            }

            protected override void Paint(Graphics graphics, Rectangle clipBounds,
                  Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                  object value, object formattedValue, string errorText,
                  DataGridViewCellStyle cellStyle,
                  DataGridViewAdvancedBorderStyle advancedBorderStyle,
                  DataGridViewPaintParts paintParts)
            {

                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (value != null)
                {
                    double newValue = 0;
                    double.TryParse(value.ToString(), out newValue);


                    if (oldValue - newValue < 0)
                    {
                        Image = UpArrowImage;
                        Style.ForeColor = Color.Green;
                    }
                    else if (oldValue - newValue > 0)
                    {
                        Image = DownArrowImage;
                        Style.ForeColor = Color.LightCoral;
                    }
                    if (newValue == 0)
                    {
                        Image = UpArrowImage;
                        Style.ForeColor = Color.Green;

                    }

                    oldValue = newValue;
                }


                // Paint the base content
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                            value, formattedValue, errorText, cellStyle,
                            advancedBorderStyle, paintParts);

                if (this.Image != null)
                {
                    // Draw the image clipped to the cell.
                    System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                    graphics.SetClip(cellBounds);
                    //graphics.DrawImageUnscaled(this.Image, cellBounds.Location);
                    graphics.DrawImage(this.Image, cellBounds.Location.X, cellBounds.Location.Y, this.Image.Width - 1, this.Image.Height - 1);
                    graphics.EndContainer(container);

                }
            }

            private TextAndImageColumn OwningTextAndImageColumn
            {
                get { return this.OwningColumn as TextAndImageColumn; }
            }
        }
        public class TextAndImageCellX : DataGridViewTextBoxCell
        {
            private double oldValue = 0;
            private Image imageValue;
            private Size imageSize;


            private Image upArrowImage;
            public Image UpArrowImage
            {
                get
                {
                    if (this.OwningColumn == null || this.OwningTextAndImageColumn == null)
                    {
                        return upArrowImage;
                    }

                    else if (this.upArrowImage != null)
                    {
                        return this.upArrowImage;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.UpArrowImage;
                    }
                }
                set { this.upArrowImage = value; }
            }


            private Image downArrowImage;

            public Image DownArrowImage
            {
                get
                {
                    if (this.OwningColumn == null ||
                       this.OwningTextAndImageColumn == null)
                    {
                        return downArrowImage;
                    }

                    else if (this.downArrowImage != null)
                    {
                        return this.downArrowImage;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.DownArrowImage;
                    }
                }
                set { this.downArrowImage = value; }
            }

            public Image Image
            {
                get
                {
                    if (this.OwningColumn == null ||
                         this.OwningTextAndImageColumn == null)
                    {
                        return imageValue;
                    }

                    else if (this.imageValue != null)
                    {
                        return this.imageValue;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.Image;
                    }
                }

                set
                {
                    if (this.imageValue != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.Style.Padding = new Padding(imageSize.Width, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom);
                    }
                }
            }

            public override object Clone()
            {
                TextAndImageCellX c = base.Clone() as TextAndImageCellX;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.upArrowImage = this.upArrowImage;
                c.downArrowImage = this.downArrowImage;
                c.oldValue = this.oldValue;
                return c;
            }
            protected override bool SetValue(int rowIndex, object value)
            {
                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                return base.SetValue(rowIndex, value);
            }

            protected override void Paint(Graphics graphics, Rectangle clipBounds,
                  Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                  object value, object formattedValue, string errorText,
                  DataGridViewCellStyle cellStyle,
                  DataGridViewAdvancedBorderStyle advancedBorderStyle,
                  DataGridViewPaintParts paintParts)
            {

                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (value != null)
                {
                    double newValue = 0;
                    double.TryParse(value.ToString(), out newValue);


                    if (oldValue - newValue < 0)
                    {
                        Image = UpArrowImage;
                        Style.ForeColor = Color.Green;
                    }
                    else if (oldValue - newValue > 0)
                    {
                        Image = DownArrowImage;
                        Style.ForeColor = Color.Red;
                    }
                    if (newValue == 0)
                    {
                        Image = UpArrowImage;
                        Style.ForeColor = Color.Green;

                    }

                    oldValue = newValue;
                }


                // Paint the base content
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                            value, formattedValue, errorText, cellStyle,
                            advancedBorderStyle, paintParts);

                if (this.Image != null)
                {
                    // Draw the image clipped to the cell.
                    System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                    graphics.SetClip(cellBounds);
                    //graphics.DrawImageUnscaled(this.Image, cellBounds.Location);
                    graphics.DrawImage(this.Image, cellBounds.Location.X, cellBounds.Location.Y, this.Image.Width - 1, this.Image.Height - 1);
                    graphics.EndContainer(container);

                }
            }

            private TextAndImageColumnX OwningTextAndImageColumn
            {
                get { return this.OwningColumn as TextAndImageColumnX; }
            }
        }

        public class TextAndImageCellN : DataGridViewTextBoxCell
        {
           
            private double oldValue = 0;
            private Image imageValue;
            private Size imageSize;


            private Image displayimage;
            public Image DisplayImage
            {
                get
                {
                    if (this.OwningColumn == null || this.OwningTextAndImageColumn == null)
                    {
                        return displayimage;
                    }

                    else if (this.displayimage != null)
                    {
                        return this.displayimage;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.DisplayImage;
                    }
                }
                set { this.displayimage = value; }
            }


            private Image displayimage2;
            public Image DisplayImage2
            {
                get
                {
                    if (this.OwningColumn == null || this.OwningTextAndImageColumn == null)
                    {
                        return displayimage2;
                    }

                    else if (this.displayimage2 != null)
                    {
                        return this.displayimage2;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.DisplayImage2;
                    }
                }
                set { this.displayimage2 = value; }
            }

            public TextAndImageCellN()
            {
               
            }
        
      
            public Image Image
            {
                get
                {
                    if (this.OwningColumn == null ||
                         this.OwningTextAndImageColumn == null)
                    {
                        return imageValue;
                    }

                    else if (this.imageValue != null)
                    {
                        return this.imageValue;
                    }

                    else
                    {
                        return this.OwningTextAndImageColumn.Image;
                    }
                }

                set
                {
                    if (this.imageValue != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.Style.Padding = new Padding(imageSize.Width,
                                       inheritedPadding.Top, inheritedPadding.Right,
                                       inheritedPadding.Bottom);
                    }
                }
            }

            public override object Clone()
            {
                TextAndImageCellN c = base.Clone() as TextAndImageCellN;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.displayimage = this.displayimage;
                c.oldValue = this.oldValue;
                return c;
            }
            protected override bool SetValue(int rowIndex, object value)
            {
                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                return base.SetValue(rowIndex, value);
            }

            protected override void Paint(Graphics graphics, Rectangle clipBounds,
                  Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                  object value, object formattedValue, string errorText,
                  DataGridViewCellStyle cellStyle,
                  DataGridViewAdvancedBorderStyle advancedBorderStyle,
                  DataGridViewPaintParts paintParts)
            {

                this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (value != null)
                {
                    // double newValue = 0;
                    //  double.TryParse(value.ToString(), out newValue);


                    //    if (oldValue - newValue < 0)
                    //    {
                    //        Image = UpArrowImage;
                    //         Style.ForeColor = Color.Green;
                    //     }
                    //    else if (oldValue - newValue > 0)
                    //     {
                    //if (earnings != null)
                    //{

                    //    if (earnings.SymbolExists(value.ToString()))
                    //    {
                    //        Image = DisplayImage;
                    //    }
                    //}
                    //if (eps != null)
                    //{
                    //    if (eps.SymbolExists(value.ToString()))
                    //    {
                    //        Image = DisplayImage2;
                    //    }

                    //}
                    ////         Style.ForeColor = Color.Red;
                    //    }
                    //    if (newValue == 0)
                    //    {
                    //        Image = UpArrowImage;
                    //        Style.ForeColor = Color.Green;

                    //     }

                    //  oldValue = newValue;
                }


                // Paint the base content
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                            value, formattedValue, errorText, cellStyle,
                            advancedBorderStyle, paintParts);

                if (this.Image != null)
                {
                    // Draw the image clipped to the cell.
                    System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                    graphics.SetClip(cellBounds);
                    //graphics.DrawImageUnscaled(this.Image, cellBounds.Location);
                    graphics.DrawImage(this.Image, cellBounds.Location.X, cellBounds.Location.Y, this.Image.Width - 1, this.Image.Height - 1);
                    graphics.EndContainer(container);

                }
            }

            private TextAndImageColumnN OwningTextAndImageColumn
            {
                get { return this.OwningColumn as TextAndImageColumnN; }
            }
        }
        public class TextAndImageColumn : DataGridViewTextBoxColumn
        {

            private Image imageValue;
            private Size imageSize;

            private Image upArrowImage;

            public Image UpArrowImage
            {
                get { return upArrowImage; }
                set { upArrowImage = value; }
            }
            private Image downArrowImage;

            public Image DownArrowImage
            {
                get { return downArrowImage; }
                set { downArrowImage = value; }
            }

            public TextAndImageColumn()
            {
                this.CellTemplate = new TextAndImageCell();
            }

            public override object Clone()
            {
                TextAndImageColumn c = base.Clone() as TextAndImageColumn;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.upArrowImage = this.upArrowImage;
                c.downArrowImage = this.downArrowImage;
                return c;
            }

            public Image Image
            {
                get { return this.imageValue; }
                set
                {
                    if (this.Image != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        if (this.InheritedStyle != null)
                        {
                            Padding inheritedPadding = this.InheritedStyle.Padding;
                            this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                                       inheritedPadding.Top, inheritedPadding.Right,
                                       inheritedPadding.Bottom);
                        }
                    }
                }
            }

            private TextAndImageCell TextAndImageCellTemplate
            {
                get { return this.CellTemplate as TextAndImageCell; }
            }

            internal Size ImageSize
            {
                get { return imageSize; }
            }
        }
        public class TextAndImageColumnX : DataGridViewTextBoxColumn
        {

            private Image imageValue;
            private Size imageSize;

            private Image upArrowImage;

            public Image UpArrowImage
            {
                get { return upArrowImage; }
                set { upArrowImage = value; }
            }
            private Image downArrowImage;

            public Image DownArrowImage
            {
                get { return downArrowImage; }
                set { downArrowImage = value; }
            }

            public TextAndImageColumnX()
            {
                this.CellTemplate = new TextAndImageCellX();
            }

            public override object Clone()
            {
                TextAndImageColumnX c = base.Clone() as TextAndImageColumnX;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.upArrowImage = this.upArrowImage;
                c.downArrowImage = this.downArrowImage;
                return c;
            }

            public Image Image
            {
                get { return this.imageValue; }
                set
                {
                    if (this.Image != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        if (this.InheritedStyle != null)
                        {
                            Padding inheritedPadding = this.InheritedStyle.Padding;
                            this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                                       inheritedPadding.Top, inheritedPadding.Right,
                                       inheritedPadding.Bottom);
                        }
                    }
                }
            }

            private TextAndImageCellX TextAndImageCellTemplate
            {
                get { return this.CellTemplate as TextAndImageCellX; }
            }

            internal Size ImageSize
            {
                get { return imageSize; }
            }
        }



        public class TextAndImageColumnN : DataGridViewTextBoxColumn
        {
            private Image imageValue;
            private Size imageSize;
            private Image displayimage;
            private Image displayimage2;
            TextAndImageCellN t;
            public Image DisplayImage
            {
                get { return displayimage; }
                set { displayimage = value; }
            }

            public Image DisplayImage2
            {
                get { return displayimage2; }
                set { displayimage2 = value; }
            }


            public TextAndImageColumnN()
            {
         
            }
         
            public override object Clone()
            {
                TextAndImageColumnN c = base.Clone() as TextAndImageColumnN;
                c.imageValue = this.imageValue;
                c.imageSize = this.imageSize;
                c.displayimage = this.displayimage;
                return c;
            }

            public Image Image
            {
                get { return this.imageValue; }
                set
                {
                    if (this.Image != value)
                    {
                        this.imageValue = value;
                        this.imageSize = value.Size;
                        if (this.InheritedStyle != null)
                        {
                            Padding inheritedPadding = this.InheritedStyle.Padding;
                            this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                                       inheritedPadding.Top, inheritedPadding.Right,
                                       inheritedPadding.Bottom);
                        }
                    }
                }
            }

            private TextAndImageCellN TextAndImageCellTemplate
            {
                get { return this.CellTemplate as TextAndImageCellN; }
            }

            internal Size ImageSize
            {
                get { return imageSize; }
            }
        }


    
}
