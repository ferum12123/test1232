namespace Asystem.DesktopClient
{
    partial class PrepressForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblOrder;
        private DataGridView dgvTasks;
        private Button btnComplete;
        private Button btnToPrint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOrder = new Label();
            this.dgvTasks = new DataGridView();
            this.btnComplete = new Button();
            this.btnToPrint = new Button();

            this.lblOrder.Text = "Order:";
            this.lblOrder.Top = 10;
            this.lblOrder.Left = 10;

            this.dgvTasks.Top = 40;
            this.dgvTasks.Left = 10;
            this.dgvTasks.Height = 300;
            this.dgvTasks.Width = 560;

            this.btnComplete.Text = "Выполнить задачу";
            this.btnComplete.Top = 350;
            this.btnComplete.Left = 10;
            this.btnComplete.Click += new EventHandler(this.btnComplete_Click);

            this.btnToPrint.Text = "Перевести в Печать";
            this.btnToPrint.Top = 350;
            this.btnToPrint.Left = 160;
            this.btnToPrint.Click += new EventHandler(this.btnToPrint_Click);

            this.ClientSize = new System.Drawing.Size(600, 420);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.dgvTasks);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.btnToPrint);
            this.Load += new EventHandler(this.PrepressForm_Load);
            this.Text = "Prepress";
        }
    }
}
