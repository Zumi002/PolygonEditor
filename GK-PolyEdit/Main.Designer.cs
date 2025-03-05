namespace GK_PolyEdit
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            PolygonBox = new PictureBox();
            MainPanel = new TableLayoutPanel();
            radioButton1 = new RadioButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            DrawButton = new Button();
            groupBox1 = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            EdgeEditContextMenuStrip = new ContextMenuStrip(components);
            addNToolStripMenuItem = new ToolStripMenuItem();
            ewToolStripMenuItem = new ToolStripMenuItem();
            lengthToolStripMenuItem = new ToolStripMenuItem();
            verticalToolStripMenuItem = new ToolStripMenuItem();
            horizontalToolStripMenuItem = new ToolStripMenuItem();
            removeRelationToolStripMenuItem = new ToolStripMenuItem();
            makeBezierToolStripMenuItem = new ToolStripMenuItem();
            VertexEditContextMenuStrip = new ContextMenuStrip(components);
            removeVertexToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            g0ToolStripMenuItem = new ToolStripMenuItem();
            g1ToolStripMenuItem = new ToolStripMenuItem();
            c1ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            helpToolStripMenuItem = new ToolStripMenuItem();
            controlsToolStripMenuItem = new ToolStripMenuItem();
            relationImplementationInfoToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)PolygonBox).BeginInit();
            MainPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            EdgeEditContextMenuStrip.SuspendLayout();
            VertexEditContextMenuStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // PolygonBox
            // 
            PolygonBox.Dock = DockStyle.Fill;
            PolygonBox.Location = new Point(3, 27);
            PolygonBox.Name = "PolygonBox";
            PolygonBox.Size = new Size(594, 445);
            PolygonBox.TabIndex = 0;
            PolygonBox.TabStop = false;
            PolygonBox.MouseDown += PolygonBox_MouseDown;
            PolygonBox.MouseMove += PolygonBox_MouseMove;
            PolygonBox.MouseUp += PolygonBox_MouseUp;
            // 
            // MainPanel
            // 
            MainPanel.ColumnCount = 2;
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            MainPanel.Controls.Add(PolygonBox, 0, 1);
            MainPanel.Controls.Add(radioButton1, 0, 0);
            MainPanel.Controls.Add(tableLayoutPanel1, 1, 1);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.RowCount = 2;
            MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            MainPanel.RowStyles.Add(new RowStyle());
            MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            MainPanel.Size = new Size(800, 475);
            MainPanel.TabIndex = 2;
            MainPanel.Resize += MainPanel_Resize;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(94, 18);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(DrawButton, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(603, 27);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new Size(194, 445);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // DrawButton
            // 
            DrawButton.Location = new Point(3, 3);
            DrawButton.Name = "DrawButton";
            DrawButton.Size = new Size(188, 81);
            DrawButton.TabIndex = 2;
            DrawButton.Text = "Reset";
            DrawButton.UseVisualStyleBackColor = true;
            DrawButton.Click += ResetButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(3, 90);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(188, 94);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "LineDrawingAlg";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(6, 47);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(103, 19);
            radioButton3.TabIndex = 1;
            radioButton3.Text = "Bresenham alg";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Location = new Point(6, 22);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(80, 19);
            radioButton2.TabIndex = 0;
            radioButton2.TabStop = true;
            radioButton2.Text = "Library alg";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // EdgeEditContextMenuStrip
            // 
            EdgeEditContextMenuStrip.Items.AddRange(new ToolStripItem[] { addNToolStripMenuItem, ewToolStripMenuItem, removeRelationToolStripMenuItem, makeBezierToolStripMenuItem });
            EdgeEditContextMenuStrip.Name = "contextMenuStrip1";
            EdgeEditContextMenuStrip.Size = new Size(161, 92);
            // 
            // addNToolStripMenuItem
            // 
            addNToolStripMenuItem.Name = "addNToolStripMenuItem";
            addNToolStripMenuItem.Size = new Size(160, 22);
            addNToolStripMenuItem.Text = "Add new vertex";
            addNToolStripMenuItem.Click += AddVertexClick;
            // 
            // ewToolStripMenuItem
            // 
            ewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lengthToolStripMenuItem, verticalToolStripMenuItem, horizontalToolStripMenuItem });
            ewToolStripMenuItem.Name = "ewToolStripMenuItem";
            ewToolStripMenuItem.Size = new Size(160, 22);
            ewToolStripMenuItem.Text = "Add relation";
            // 
            // lengthToolStripMenuItem
            // 
            lengthToolStripMenuItem.Name = "lengthToolStripMenuItem";
            lengthToolStripMenuItem.Size = new Size(129, 22);
            lengthToolStripMenuItem.Text = "Length";
            lengthToolStripMenuItem.Click += lengthToolStripMenuItem_Click;
            // 
            // verticalToolStripMenuItem
            // 
            verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            verticalToolStripMenuItem.Size = new Size(129, 22);
            verticalToolStripMenuItem.Text = "Vertical";
            verticalToolStripMenuItem.Click += verticalToolStripMenuItem_Click;
            // 
            // horizontalToolStripMenuItem
            // 
            horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            horizontalToolStripMenuItem.Size = new Size(129, 22);
            horizontalToolStripMenuItem.Text = "Horizontal";
            horizontalToolStripMenuItem.Click += horizontalToolStripMenuItem_Click;
            // 
            // removeRelationToolStripMenuItem
            // 
            removeRelationToolStripMenuItem.Name = "removeRelationToolStripMenuItem";
            removeRelationToolStripMenuItem.Size = new Size(160, 22);
            removeRelationToolStripMenuItem.Text = "Remove relation";
            removeRelationToolStripMenuItem.Click += removeRelationToolStripMenuItem_Click;
            // 
            // makeBezierToolStripMenuItem
            // 
            makeBezierToolStripMenuItem.Name = "makeBezierToolStripMenuItem";
            makeBezierToolStripMenuItem.Size = new Size(160, 22);
            makeBezierToolStripMenuItem.Text = "Make Bezier";
            makeBezierToolStripMenuItem.Click += makeBezierToolStripMenuItem_Click;
            // 
            // VertexEditContextMenuStrip
            // 
            VertexEditContextMenuStrip.Items.AddRange(new ToolStripItem[] { removeVertexToolStripMenuItem, addToolStripMenuItem });
            VertexEditContextMenuStrip.Name = "VertexEditContextMenuStrip";
            VertexEditContextMenuStrip.Size = new Size(173, 48);
            // 
            // removeVertexToolStripMenuItem
            // 
            removeVertexToolStripMenuItem.Name = "removeVertexToolStripMenuItem";
            removeVertexToolStripMenuItem.Size = new Size(172, 22);
            removeVertexToolStripMenuItem.Text = "Remove vertex";
            removeVertexToolStripMenuItem.Click += removeVertexToolStripMenuItem_Click;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { g0ToolStripMenuItem, g1ToolStripMenuItem, c1ToolStripMenuItem });
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(172, 22);
            addToolStripMenuItem.Text = "Change continuity";
            // 
            // g0ToolStripMenuItem
            // 
            g0ToolStripMenuItem.AccessibleName = "G0CMS";
            g0ToolStripMenuItem.Name = "g0ToolStripMenuItem";
            g0ToolStripMenuItem.Size = new Size(88, 22);
            g0ToolStripMenuItem.Text = "G0";
            g0ToolStripMenuItem.Click += g0ToolStripMenuItem_Click;
            // 
            // g1ToolStripMenuItem
            // 
            g1ToolStripMenuItem.AccessibleName = "G1CMS";
            g1ToolStripMenuItem.Name = "g1ToolStripMenuItem";
            g1ToolStripMenuItem.Size = new Size(88, 22);
            g1ToolStripMenuItem.Text = "G1";
            g1ToolStripMenuItem.Click += g1ToolStripMenuItem_Click;
            // 
            // c1ToolStripMenuItem
            // 
            c1ToolStripMenuItem.AccessibleName = "C1CMS";
            c1ToolStripMenuItem.Name = "c1ToolStripMenuItem";
            c1ToolStripMenuItem.Size = new Size(88, 22);
            c1ToolStripMenuItem.Text = "C1";
            c1ToolStripMenuItem.Click += c1ToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { controlsToolStripMenuItem, relationImplementationInfoToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // controlsToolStripMenuItem
            // 
            controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            controlsToolStripMenuItem.Size = new Size(229, 22);
            controlsToolStripMenuItem.Text = "Controls";
            controlsToolStripMenuItem.Click += controlsToolStripMenuItem_Click;
            // 
            // relationImplementationInfoToolStripMenuItem
            // 
            relationImplementationInfoToolStripMenuItem.Name = "relationImplementationInfoToolStripMenuItem";
            relationImplementationInfoToolStripMenuItem.Size = new Size(229, 22);
            relationImplementationInfoToolStripMenuItem.Text = "Relation implementation info";
            relationImplementationInfoToolStripMenuItem.Click += relationImplementationInfoToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 475);
            Controls.Add(menuStrip1);
            Controls.Add(MainPanel);
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(816, 514);
            MinimumSize = new Size(816, 514);
            Name = "Main";
            Text = "PolygonEditor";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)PolygonBox).EndInit();
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            EdgeEditContextMenuStrip.ResumeLayout(false);
            VertexEditContextMenuStrip.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox PolygonBox;
        private TableLayoutPanel MainPanel;
        private ContextMenuStrip EdgeEditContextMenuStrip;
        private ToolStripMenuItem addNToolStripMenuItem;
        private ToolStripMenuItem ewToolStripMenuItem;
        private ToolStripMenuItem lengthToolStripMenuItem;
        private ToolStripMenuItem verticalToolStripMenuItem;
        private ToolStripMenuItem horizontalToolStripMenuItem;
        private ContextMenuStrip VertexEditContextMenuStrip;
        private ToolStripMenuItem removeVertexToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem g0ToolStripMenuItem;
        private ToolStripMenuItem g1ToolStripMenuItem;
        private ToolStripMenuItem c1ToolStripMenuItem;
        private ToolStripMenuItem removeRelationToolStripMenuItem;
        private ToolStripMenuItem makeBezierToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem controlsToolStripMenuItem;
        private ToolStripMenuItem relationImplementationInfoToolStripMenuItem;
        private RadioButton radioButton1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button DrawButton;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
    }
}
