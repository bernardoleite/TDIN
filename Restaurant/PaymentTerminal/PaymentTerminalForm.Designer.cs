namespace PaymentTerminal
{
    partial class PaymentTerminalForm
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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            this.closeButton = new System.Windows.Forms.Button();
            this.tablesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tablesServed = new System.Windows.Forms.TextBox();
            this.totalValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.closeButton.Location = new System.Drawing.Point(42, 369);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(155, 29);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // tablesListView
            // 
            this.tablesListView.CheckBoxes = true;
            this.tablesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.tablesListView.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablesListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tablesListView.GridLines = true;
            listViewItem4.StateImageIndex = 0;
            this.tablesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.tablesListView.Location = new System.Drawing.Point(47, 93);
            this.tablesListView.MultiSelect = false;
            this.tablesListView.Name = "tablesListView";
            this.tablesListView.Size = new System.Drawing.Size(476, 256);
            this.tablesListView.TabIndex = 7;
            this.tablesListView.UseCompatibleStateImageBehavior = false;
            this.tablesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "TableId";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Total Value";
            this.columnHeader3.Width = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "TABLES";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.label1.Location = new System.Drawing.Point(546, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Total Value Collected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.label2.Location = new System.Drawing.Point(546, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Number of Tables Served";
            // 
            // tablesServed
            // 
            this.tablesServed.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablesServed.Location = new System.Drawing.Point(550, 200);
            this.tablesServed.Name = "tablesServed";
            this.tablesServed.Size = new System.Drawing.Size(196, 25);
            this.tablesServed.TabIndex = 12;
            // 
            // totalValue
            // 
            this.totalValue.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalValue.Location = new System.Drawing.Point(550, 128);
            this.totalValue.Name = "totalValue";
            this.totalValue.Size = new System.Drawing.Size(196, 25);
            this.totalValue.TabIndex = 13;
            // 
            // PaymentTerminalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 447);
            this.Controls.Add(this.totalValue);
            this.Controls.Add(this.tablesServed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tablesListView);
            this.Controls.Add(this.label4);
            this.Name = "PaymentTerminalForm";
            this.Text = "Payment Terminal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ListView tablesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tablesServed;
        private System.Windows.Forms.TextBox totalValue;
    }
}

