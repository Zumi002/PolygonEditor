namespace GK_PolyEdit.Relations
{
    partial class LengthRelationDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            LengthBox = new NumericUpDown();
            OKButton = new Button();
            ((System.ComponentModel.ISupportInitialize)LengthBox).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(146, 25);
            label1.TabIndex = 0;
            label1.Text = "Set edge length";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LengthBox
            // 
            LengthBox.InterceptArrowKeys = false;
            LengthBox.Location = new Point(38, 76);
            LengthBox.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            LengthBox.Name = "LengthBox";
            LengthBox.Size = new Size(120, 23);
            LengthBox.TabIndex = 1;
            // 
            // OKButton
            // 
            OKButton.Location = new Point(54, 105);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 2;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(184, 161);
            Controls.Add(OKButton);
            Controls.Add(LengthBox);
            Controls.Add(label1);
            MaximumSize = new Size(200, 200);
            MinimumSize = new Size(200, 200);
            Name = "Form1";
            Text = "Edge Dialog";
            ((System.ComponentModel.ISupportInitialize)LengthBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown LengthBox;
        private Button OKButton;
    }
}