namespace TRQuoteCore.GQuoteMonitor
{
    partial class QuoteMonitorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSymbol = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSymbol = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonImportSymbols = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonFullScr = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReadDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMarketSummary = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImbalance = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainerMain = new System.Windows.Forms.ToolStripContainer();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.labelConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelServerNotification = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelServerError = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridView = new TRQuoteCore.Quotes.Common.DGVDoubleBuffered();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelAsk = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labelBid = new System.Windows.Forms.Label();
            this.labelChange = new TRQuoteCore.Quotes.CustomControls.ColoredLabel();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelLast = new TRQuoteCore.Quotes.CustomControls.ColoredLabel();
            this.labelWk52H = new System.Windows.Forms.Label();
            this.labelWk52L = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.label30dVol = new System.Windows.Forms.Label();
            this.labelPExchange = new System.Windows.Forms.Label();
            this.labelVWAP = new System.Windows.Forms.Label();
            this.labelLow = new System.Windows.Forms.Label();
            this.labelClose = new System.Windows.Forms.Label();
            this.labelHigh = new System.Windows.Forms.Label();
            this.labelOpen = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl30dADV = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl30dPx = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxSymbol = new System.Windows.Forms.TextBox();
            this.labelDesc = new System.Windows.Forms.Label();
            this.buttonDummy = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.toolTipError = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxLabel = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMain.SuspendLayout();
            this.toolStripContainerMain.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainerMain.ContentPanel.SuspendLayout();
            this.toolStripContainerMain.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStripOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSymbol,
            this.toolStripTextBoxSymbol,
            this.toolStripSeparator1,
            this.toolStripButtonImportSymbols,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButtonFullScr,
            this.toolStripButtonReadDefault,
            this.toolStripSeparator2,
            this.toolStripButtonSettings,
            this.toolStripSeparator4,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator5,
            this.toolStripMarketSummary,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButtonImbalance,
            this.toolStripButton6});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(969, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "Main Tool bar";
            // 
            // toolStripLabelSymbol
            // 
            this.toolStripLabelSymbol.Name = "toolStripLabelSymbol";
            this.toolStripLabelSymbol.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabelSymbol.Text = "Symbol:";
            this.toolStripLabelSymbol.Visible = false;
            // 
            // toolStripTextBoxSymbol
            // 
            this.toolStripTextBoxSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.toolStripTextBoxSymbol.Name = "toolStripTextBoxSymbol";
            this.toolStripTextBoxSymbol.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxSymbol.Visible = false;
            this.toolStripTextBoxSymbol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxSymbol_KeyUp);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripButtonImportSymbols
            // 
            this.toolStripButtonImportSymbols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportSymbols.Image = global::TRQuoteCore.Properties.Resources.folder_table;
            this.toolStripButtonImportSymbols.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportSymbols.Name = "toolStripButtonImportSymbols";
            this.toolStripButtonImportSymbols.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportSymbols.Text = "Import CSV Symbols File";
            this.toolStripButtonImportSymbols.Click += new System.EventHandler(this.toolStripButtonImportSymbols_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TRQuoteCore.Properties.Resources.disk;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Save Default Workpsace";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonFullScr
            // 
            this.toolStripButtonFullScr.CheckOnClick = true;
            this.toolStripButtonFullScr.Image = global::TRQuoteCore.Properties.Resources.monitor;
            this.toolStripButtonFullScr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFullScr.Name = "toolStripButtonFullScr";
            this.toolStripButtonFullScr.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonFullScr.Text = "Full Screen";
            this.toolStripButtonFullScr.Visible = false;
            this.toolStripButtonFullScr.Click += new System.EventHandler(this.toolStripButtonFullScr_Click);
            // 
            // toolStripButtonReadDefault
            // 
            this.toolStripButtonReadDefault.Image = global::TRQuoteCore.Properties.Resources.table;
            this.toolStripButtonReadDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReadDefault.Name = "toolStripButtonReadDefault";
            this.toolStripButtonReadDefault.Size = new System.Drawing.Size(94, 22);
            this.toolStripButtonReadDefault.Text = "Read Default";
            this.toolStripButtonReadDefault.ToolTipText = "Reads Default workspace saved symbols";
            this.toolStripButtonReadDefault.Visible = false;
            this.toolStripButtonReadDefault.Click += new System.EventHandler(this.toolStripButtonReadDefault_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.Image = global::TRQuoteCore.Properties.Resources.cog;
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(69, 22);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::TRQuoteCore.Properties.Resources.arrow_refresh;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Reload Quotes";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::TRQuoteCore.Properties.Resources.help;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Symbology Help";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripMarketSummary
            // 
            this.toolStripMarketSummary.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMarketSummary.Image = global::TRQuoteCore.Properties.Resources.table_row_insert;
            this.toolStripMarketSummary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMarketSummary.Name = "toolStripMarketSummary";
            this.toolStripMarketSummary.Size = new System.Drawing.Size(118, 22);
            this.toolStripMarketSummary.Text = "Market Summary";
            this.toolStripMarketSummary.Visible = false;
            this.toolStripMarketSummary.Click += new System.EventHandler(this.toolStripMarketSummary_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton5.Image = global::TRQuoteCore.Properties.Resources.table;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton5.Text = "Index List";
            this.toolStripButton5.ToolTipText = "Index List";
            this.toolStripButton5.Visible = false;
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.Image = global::TRQuoteCore.Properties.Resources.table;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton4.Text = "ETF List";
            this.toolStripButton4.Visible = false;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButtonImbalance
            // 
            this.toolStripButtonImbalance.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonImbalance.Image = global::TRQuoteCore.Properties.Resources.table_row_delete;
            this.toolStripButtonImbalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImbalance.Name = "toolStripButtonImbalance";
            this.toolStripButtonImbalance.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonImbalance.Text = "Imbalance";
            this.toolStripButtonImbalance.Visible = false;
            this.toolStripButtonImbalance.Click += new System.EventHandler(this.toolStripButtonImbalance_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripContainerMain
            // 
            // 
            // toolStripContainerMain.BottomToolStripPanel
            // 
            this.toolStripContainerMain.BottomToolStripPanel.Controls.Add(this.statusStripMain);
            // 
            // toolStripContainerMain.ContentPanel
            // 
            this.toolStripContainerMain.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainerMain.ContentPanel.Controls.Add(this.buttonDummy);
            this.toolStripContainerMain.ContentPanel.Size = new System.Drawing.Size(969, 461);
            this.toolStripContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerMain.Name = "toolStripContainerMain";
            this.toolStripContainerMain.Size = new System.Drawing.Size(969, 533);
            this.toolStripContainerMain.TabIndex = 5;
            this.toolStripContainerMain.Text = "toolStripContainer1";
            // 
            // toolStripContainerMain.TopToolStripPanel
            // 
            this.toolStripContainerMain.TopToolStripPanel.Controls.Add(this.toolStripMain);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelConnectionStatus,
            this.labelServerNotification,
            this.labelServerError,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.statusStripMain.Location = new System.Drawing.Point(0, 25);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStripMain.Size = new System.Drawing.Size(969, 22);
            this.statusStripMain.TabIndex = 4;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(13, 17);
            this.labelConnectionStatus.Text = "..";
            // 
            // labelServerNotification
            // 
            this.labelServerNotification.Name = "labelServerNotification";
            this.labelServerNotification.Size = new System.Drawing.Size(13, 17);
            this.labelServerNotification.Text = "..";
            // 
            // labelServerError
            // 
            this.labelServerError.Name = "labelServerError";
            this.labelServerError.Size = new System.Drawing.Size(13, 17);
            this.labelServerError.Text = "..";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(758, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(126, 17);
            this.toolStripStatusLabel1.Text = "Toggle Detailed Quote";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel);
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(969, 461);
            this.splitContainer.SplitterDistance = 360;
            this.splitContainer.TabIndex = 4;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Size = new System.Drawing.Size(965, 457);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            this.dataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 18;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.labelAsk, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label22, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.label20, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.label21, 4, 1);
            this.tableLayoutPanel.Controls.Add(this.label8, 10, 1);
            this.tableLayoutPanel.Controls.Add(this.label9, 4, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 10, 2);
            this.tableLayoutPanel.Controls.Add(this.label16, 12, 1);
         //   this.tableLayoutPanel.Controls.Add(this.label15, 12, 2);
            this.tableLayoutPanel.Controls.Add(this.labelBid, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.labelChange, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.label14, 8, 2);
            this.tableLayoutPanel.Controls.Add(this.label4, 6, 2);
            this.tableLayoutPanel.Controls.Add(this.label11, 8, 1);
            this.tableLayoutPanel.Controls.Add(this.label13, 6, 1);
            this.tableLayoutPanel.Controls.Add(this.labelLast, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelWk52H, 9, 1);
            this.tableLayoutPanel.Controls.Add(this.labelWk52L, 9, 2);
            this.tableLayoutPanel.Controls.Add(this.labelVolume, 7, 1);
           // this.tableLayoutPanel.Controls.Add(this.label30dVol, 13, 2);
            this.tableLayoutPanel.Controls.Add(this.labelPExchange, 7, 2);
            this.tableLayoutPanel.Controls.Add(this.labelVWAP, 13, 1);
            this.tableLayoutPanel.Controls.Add(this.labelLow, 5, 2);
            this.tableLayoutPanel.Controls.Add(this.labelClose, 11, 1);
            this.tableLayoutPanel.Controls.Add(this.labelHigh, 5, 1);
            this.tableLayoutPanel.Controls.Add(this.labelOpen, 11, 2);
            this.tableLayoutPanel.Controls.Add(this.label5, 15, 1);
            this.tableLayoutPanel.Controls.Add(this.lbl30dADV, 16, 1);
            this.tableLayoutPanel.Controls.Add(this.label6, 15, 2);
            this.tableLayoutPanel.Controls.Add(this.lbl30dPx, 16, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 34);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(146, 8);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelAsk
            // 
            this.labelAsk.AutoSize = true;
            this.labelAsk.Location = new System.Drawing.Point(110, 13);
            this.labelAsk.Name = "labelAsk";
            this.labelAsk.Size = new System.Drawing.Size(13, 13);
            this.labelAsk.TabIndex = 4;
            this.labelAsk.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Last:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 13);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 21;
            this.label22.Text = "Change:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bid:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(76, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(28, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "Ask:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(129, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(32, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "High:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Close:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(129, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Low:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Open:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(388, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "VWAP:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(388, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "30d Vol:";
            this.label15.Visible = false;
            // 
            // labelBid
            // 
            this.labelBid.AutoSize = true;
            this.labelBid.Location = new System.Drawing.Point(110, 0);
            this.labelBid.Name = "labelBid";
            this.labelBid.Size = new System.Drawing.Size(13, 13);
            this.labelBid.TabIndex = 3;
            this.labelBid.Text = "0";
            // 
            // labelChange
            // 
            this.labelChange.AutoSize = true;
            this.labelChange.DownImage = null;
            this.labelChange.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelChange.Location = new System.Drawing.Point(57, 13);
            this.labelChange.Name = "labelChange";
            this.labelChange.NegativeColor = System.Drawing.Color.Red;
            this.labelChange.PositiveColor = System.Drawing.Color.Green;
            this.labelChange.Size = new System.Drawing.Size(13, 13);
            this.labelChange.TabIndex = 29;
            this.labelChange.Text = "0";
            this.labelChange.UpImage = null;
            this.labelChange.ZeroColor = System.Drawing.Color.ForestGreen;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(256, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Wk52L:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "P.Exch:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Wk52H:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(186, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Volume:";
            // 
            // labelLast
            // 
            this.labelLast.AutoSize = true;
            this.labelLast.DownImage = null;
            this.labelLast.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelLast.Location = new System.Drawing.Point(57, 0);
            this.labelLast.Name = "labelLast";
            this.labelLast.NegativeColor = System.Drawing.Color.Red;
            this.labelLast.PositiveColor = System.Drawing.Color.Green;
            this.labelLast.Size = new System.Drawing.Size(13, 13);
            this.labelLast.TabIndex = 32;
            this.labelLast.Text = "0";
            this.labelLast.UpImage = null;
            this.labelLast.ZeroColor = System.Drawing.Color.ForestGreen;
            // 
            // labelWk52H
            // 
            this.labelWk52H.AutoSize = true;
            this.labelWk52H.Location = new System.Drawing.Point(307, 0);
            this.labelWk52H.Name = "labelWk52H";
            this.labelWk52H.Size = new System.Drawing.Size(13, 13);
            this.labelWk52H.TabIndex = 11;
            this.labelWk52H.Text = "0";
            // 
            // labelWk52L
            // 
            this.labelWk52L.AutoSize = true;
            this.labelWk52L.Location = new System.Drawing.Point(307, 13);
            this.labelWk52L.Name = "labelWk52L";
            this.labelWk52L.Size = new System.Drawing.Size(13, 13);
            this.labelWk52L.TabIndex = 18;
            this.labelWk52L.Text = "0";
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(237, 0);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(13, 13);
            this.labelVolume.TabIndex = 31;
            this.labelVolume.Text = "0";
            // 
            // label30dVol
            // 
            this.label30dVol.AutoSize = true;
            this.label30dVol.Location = new System.Drawing.Point(440, 13);
            this.label30dVol.Name = "label30dVol";
            this.label30dVol.Size = new System.Drawing.Size(13, 13);
            this.label30dVol.TabIndex = 23;
            this.label30dVol.Text = "0";
            this.label30dVol.Visible = false;
            // 
            // labelPExchange
            // 
            this.labelPExchange.AutoSize = true;
            this.labelPExchange.Location = new System.Drawing.Point(237, 13);
            this.labelPExchange.Name = "labelPExchange";
            this.labelPExchange.Size = new System.Drawing.Size(13, 13);
            this.labelPExchange.TabIndex = 30;
            this.labelPExchange.Text = "0";
            // 
            // labelVWAP
            // 
            this.labelVWAP.AutoSize = true;
            this.labelVWAP.Location = new System.Drawing.Point(440, 0);
            this.labelVWAP.Name = "labelVWAP";
            this.labelVWAP.Size = new System.Drawing.Size(13, 13);
            this.labelVWAP.TabIndex = 24;
            this.labelVWAP.Text = "0";
            // 
            // labelLow
            // 
            this.labelLow.AutoSize = true;
            this.labelLow.Location = new System.Drawing.Point(167, 13);
            this.labelLow.Name = "labelLow";
            this.labelLow.Size = new System.Drawing.Size(13, 13);
            this.labelLow.TabIndex = 27;
            this.labelLow.Text = "0";
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.Location = new System.Drawing.Point(369, 0);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(13, 13);
            this.labelClose.TabIndex = 16;
            this.labelClose.Text = "0";
            // 
            // labelHigh
            // 
            this.labelHigh.AutoSize = true;
            this.labelHigh.Location = new System.Drawing.Point(167, 0);
            this.labelHigh.Name = "labelHigh";
            this.labelHigh.Size = new System.Drawing.Size(13, 13);
            this.labelHigh.TabIndex = 28;
            this.labelHigh.Text = "0";
            // 
            // labelOpen
            // 
            this.labelOpen.AutoSize = true;
            this.labelOpen.Location = new System.Drawing.Point(369, 13);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(13, 13);
            this.labelOpen.TabIndex = 9;
            this.labelOpen.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "30d ADV:";
            this.label5.Visible = false;
            // 
            // lbl30dADV
            // 
            this.lbl30dADV.AutoSize = true;
            this.lbl30dADV.Location = new System.Drawing.Point(517, 0);
            this.lbl30dADV.Name = "lbl30dADV";
            this.lbl30dADV.Size = new System.Drawing.Size(13, 13);
            this.lbl30dADV.TabIndex = 34;
            this.lbl30dADV.Text = "0";
            this.lbl30dADV.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "25d Px:";
            // 
            // lbl30dPx
            // 
            this.lbl30dPx.AutoSize = true;
            this.lbl30dPx.Location = new System.Drawing.Point(430, 0);
            this.lbl30dPx.Name = "lbl30dPx";
            this.lbl30dPx.Size = new System.Drawing.Size(13, 13);
            this.lbl30dPx.TabIndex = 36;
            this.lbl30dPx.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.textBoxSymbol, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDesc, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(146, 34);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textBoxSymbol
            // 
            this.textBoxSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSymbol.Location = new System.Drawing.Point(3, 3);
            this.textBoxSymbol.Name = "textBoxSymbol";
            this.textBoxSymbol.Size = new System.Drawing.Size(48, 21);
            this.textBoxSymbol.TabIndex = 34;
            this.textBoxSymbol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxSymbol_KeyUp);
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDesc.Location = new System.Drawing.Point(57, 0);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Padding = new System.Windows.Forms.Padding(4);
            this.labelDesc.Size = new System.Drawing.Size(8, 27);
            this.labelDesc.TabIndex = 35;
            // 
            // buttonDummy
            // 
            this.buttonDummy.Location = new System.Drawing.Point(44, 1);
            this.buttonDummy.Name = "buttonDummy";
            this.buttonDummy.Size = new System.Drawing.Size(24, 3);
            this.buttonDummy.TabIndex = 3;
            this.buttonDummy.UseVisualStyleBackColor = true;
            this.buttonDummy.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(71, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(4);
            this.linkLabel1.Size = new System.Drawing.Size(72, 27);
            this.linkLabel1.TabIndex = 33;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "&View Chart";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // toolTipError
            // 
            this.toolTipError.AutoPopDelay = 5000;
            this.toolTipError.InitialDelay = 0;
            this.toolTipError.ReshowDelay = 100;
            this.toolTipError.ShowAlways = true;
            this.toolTipError.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.toolTipError.ToolTipTitle = "Invalid Symbol";
            // 
            // contextMenuStripOrder
            // 
            this.contextMenuStripOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.contextMenuStripOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem,
            this.insertRowToolStripMenuItem,
            this.toolStripTextBoxLabel});
            this.contextMenuStripOrder.Name = "contextMenuStripOrder";
            this.contextMenuStripOrder.Size = new System.Drawing.Size(161, 71);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Image = global::TRQuoteCore.Properties.Resources.table_row_delete;
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteRowToolStripMenuItem.Text = "&Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // insertRowToolStripMenuItem
            // 
            this.insertRowToolStripMenuItem.Image = global::TRQuoteCore.Properties.Resources.table_row_insert;
            this.insertRowToolStripMenuItem.Name = "insertRowToolStripMenuItem";
            this.insertRowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.insertRowToolStripMenuItem.Text = "&Insert Row";
            this.insertRowToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
            // 
            // toolStripTextBoxLabel
            // 
            this.toolStripTextBoxLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBoxLabel.Name = "toolStripTextBoxLabel";
            this.toolStripTextBoxLabel.Size = new System.Drawing.Size(100, 21);
            this.toolStripTextBoxLabel.Text = "(Type Label here)";
            this.toolStripTextBoxLabel.ToolTipText = "Type label text here and press enter to insert a label row at the selected locati" +
                "on.";
            this.toolStripTextBoxLabel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxLabel_KeyUp);
            this.toolStripTextBoxLabel.Click += new System.EventHandler(this.toolStripTextBoxLabel_Click);
            // 
            // QuoteMonitorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 533);
            this.Controls.Add(this.toolStripContainerMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "QuoteMonitorWindow";
            this.Text = "Quote Viewer";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.toolStripContainerMain.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMain.BottomToolStripPanel.PerformLayout();
            this.toolStripContainerMain.ContentPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.PerformLayout();
            this.toolStripContainerMain.ResumeLayout(false);
            this.toolStripContainerMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuStripOrder.ResumeLayout(false);
            this.contextMenuStripOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSymbol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripContainer toolStripContainerMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel labelConnectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel labelServerNotification;
        private System.Windows.Forms.ToolStripStatusLabel labelServerError;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportSymbols;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSymbol;
        private System.Windows.Forms.ToolTip toolTipError;
        private System.Windows.Forms.ToolStripButton toolStripButtonFullScr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonDummy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOrder;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxLabel;
        private System.Windows.Forms.ToolStripButton toolStripButtonReadDefault;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.SplitContainer splitContainer;
        private TRQuoteCore.Quotes.Common.DGVDoubleBuffered dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labelClose;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelBid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelWk52H;
        private System.Windows.Forms.Label labelVWAP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHigh;
        private System.Windows.Forms.Label labelVolume;
        private TRQuoteCore.Quotes.CustomControls.ColoredLabel labelLast;
        private System.Windows.Forms.Label labelAsk;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private TRQuoteCore.Quotes.CustomControls.ColoredLabel labelChange;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelWk52L;
        private System.Windows.Forms.Label label30dVol;
        private System.Windows.Forms.Label labelPExchange;
        private System.Windows.Forms.Label labelLow;
        private System.Windows.Forms.Label labelOpen;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxSymbol;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.ToolStripButton toolStripMarketSummary;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButtonImbalance;
        private System.Windows.Forms.ToolStripSeparator toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl30dADV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl30dPx;
    }
}