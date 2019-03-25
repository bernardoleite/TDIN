namespace DinningRoom
{
    partial class ClientWindow
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.ordersListView = new System.Windows.Forms.ListView();
            this.OrderNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Table = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Qty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ordersListView
            // 
            this.ordersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OrderNumber,
            this.Description,
            this.Table,
            this.Qty});
            this.ordersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.ordersListView.Location = new System.Drawing.Point(293, 89);
            this.ordersListView.Name = "ordersListView";
            this.ordersListView.Size = new System.Drawing.Size(334, 295);
            this.ordersListView.TabIndex = 0;
            this.ordersListView.UseCompatibleStateImageBehavior = false;
            this.ordersListView.View = System.Windows.Forms.View.Details;
            // 
            // OrderNumber
            // 
            this.OrderNumber.Width = 85;
            // 
            // Table
            // 
            this.Table.DisplayIndex = 1;
            this.Table.Width = 90;
            // 
            // Qty
            // 
            this.Qty.DisplayIndex = 2;
            this.Qty.Width = 100;
            // 
            // Description
            // 
            this.Description.DisplayIndex = 3;
            this.Description.Width = 82;
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ordersListView);
            this.Name = "ClientWindow";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ordersListView;
        private System.Windows.Forms.ColumnHeader OrderNumber;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader Table;
        private System.Windows.Forms.ColumnHeader Qty;
    }
}

