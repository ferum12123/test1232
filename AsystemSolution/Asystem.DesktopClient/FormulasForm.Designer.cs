namespace Asystem.DesktopClient
{
    partial class FormulasForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvFormulas;
        private GroupBox grpFormulaDetails;
        private TextBox txtName;
        private ComboBox cmbProductType;
        private NumericUpDown numBasePrice;
        private TextBox txtPaperFormula;
        private TextBox txtPrintFormula;
        private TextBox txtSizeFormula;
        private TextBox txtVolumeFormula;
        private TextBox txtFinalFormula;
        private CheckBox chkIsActive;
        private Button btnNew;
        private Button btnSave;
        private Button btnDelete;
        private Button btnActivate;
        private Button btnDeactivate;
        private Button btnRefresh;
        private Label lblName;
        private Label lblProductType;
        private Label lblBasePrice;
        private Label lblPaperFormula;
        private Label lblPrintFormula;
        private Label lblSizeFormula;
        private Label lblVolumeFormula;
        private Label lblFinalFormula;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvFormulas = new DataGridView();
            this.grpFormulaDetails = new GroupBox();
            this.lblFinalFormula = new Label();
            this.lblVolumeFormula = new Label();
            this.lblSizeFormula = new Label();
            this.lblPrintFormula = new Label();
            this.lblPaperFormula = new Label();
            this.lblBasePrice = new Label();
            this.lblProductType = new Label();
            this.lblName = new Label();
            this.txtFinalFormula = new TextBox();
            this.txtVolumeFormula = new TextBox();
            this.txtSizeFormula = new TextBox();
            this.txtPrintFormula = new TextBox();
            this.txtPaperFormula = new TextBox();
            this.numBasePrice = new NumericUpDown();
            this.cmbProductType = new ComboBox();
            this.txtName = new TextBox();
            this.chkIsActive = new CheckBox();
            this.btnNew = new Button();
            this.btnSave = new Button();
            this.btnDelete = new Button();
            this.btnActivate = new Button();
            this.btnDeactivate = new Button();
            this.btnRefresh = new Button();

            // DataGridView
            this.dgvFormulas.Dock = DockStyle.Left;
            this.dgvFormulas.Width = 400;
            this.dgvFormulas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvFormulas.MultiSelect = false;
            this.dgvFormulas.ReadOnly = true;
            this.dgvFormulas.SelectionChanged += new EventHandler(this.dgvFormulas_SelectionChanged);

            // GroupBox для деталей формулы
            this.grpFormulaDetails.Dock = DockStyle.Fill;
            this.grpFormulaDetails.Text = "Детали формулы";
            this.grpFormulaDetails.Padding = new Padding(10);

            // Labels
            this.lblName.Text = "Название:";
            this.lblName.Location = new Point(10, 30);
            this.lblName.Size = new Size(100, 20);

            this.lblProductType.Text = "Тип продукции:";
            this.lblProductType.Location = new Point(10, 60);
            this.lblProductType.Size = new Size(100, 20);

            this.lblBasePrice.Text = "Базовая цена:";
            this.lblBasePrice.Location = new Point(10, 90);
            this.lblBasePrice.Size = new Size(100, 20);

            this.lblPaperFormula.Text = "Формула бумаги:";
            this.lblPaperFormula.Location = new Point(10, 120);
            this.lblPaperFormula.Size = new Size(100, 20);

            this.lblPrintFormula.Text = "Формула печати:";
            this.lblPrintFormula.Location = new Point(10, 150);
            this.lblPrintFormula.Size = new Size(100, 20);

            this.lblSizeFormula.Text = "Формула размера:";
            this.lblSizeFormula.Location = new Point(10, 180);
            this.lblSizeFormula.Size = new Size(100, 20);

            this.lblVolumeFormula.Text = "Формула скидки:";
            this.lblVolumeFormula.Location = new Point(10, 210);
            this.lblVolumeFormula.Size = new Size(100, 20);

            this.lblFinalFormula.Text = "Итоговая формула:";
            this.lblFinalFormula.Location = new Point(10, 240);
            this.lblFinalFormula.Size = new Size(100, 20);

            // TextBoxes и ComboBox
            this.txtName.Location = new Point(120, 28);
            this.txtName.Size = new Size(300, 20);

            this.cmbProductType.Location = new Point(120, 58);
            this.cmbProductType.Size = new Size(200, 20);
            this.cmbProductType.DropDownStyle = ComboBoxStyle.DropDownList;

            this.numBasePrice.Location = new Point(120, 88);
            this.numBasePrice.Size = new Size(100, 20);
            this.numBasePrice.DecimalPlaces = 2;
            this.numBasePrice.Maximum = 999999;

            this.txtPaperFormula.Location = new Point(120, 118);
            this.txtPaperFormula.Size = new Size(400, 20);

            this.txtPrintFormula.Location = new Point(120, 148);
            this.txtPrintFormula.Size = new Size(400, 20);

            this.txtSizeFormula.Location = new Point(120, 178);
            this.txtSizeFormula.Size = new Size(400, 20);

            this.txtVolumeFormula.Location = new Point(120, 208);
            this.txtVolumeFormula.Size = new Size(400, 20);

            this.txtFinalFormula.Location = new Point(120, 238);
            this.txtFinalFormula.Size = new Size(400, 20);

            this.chkIsActive.Text = "Активна";
            this.chkIsActive.Location = new Point(120, 268);
            this.chkIsActive.Size = new Size(100, 20);

            // Buttons
            this.btnNew.Text = "Новая";
            this.btnNew.Location = new Point(10, 300);
            this.btnNew.Size = new Size(80, 30);
            this.btnNew.Click += new EventHandler(this.btnNew_Click);

            this.btnSave.Text = "Сохранить";
            this.btnSave.Location = new Point(100, 300);
            this.btnSave.Size = new Size(80, 30);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new Point(190, 300);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnActivate.Text = "Активировать";
            this.btnActivate.Location = new Point(280, 300);
            this.btnActivate.Size = new Size(80, 30);
            this.btnActivate.Click += new EventHandler(this.btnActivate_Click);

            this.btnDeactivate.Text = "Деактивировать";
            this.btnDeactivate.Location = new Point(370, 300);
            this.btnDeactivate.Size = new Size(80, 30);
            this.btnDeactivate.Click += new EventHandler(this.btnDeactivate_Click);

            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Location = new Point(460, 300);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // Добавляем элементы в GroupBox
            this.grpFormulaDetails.Controls.AddRange(new Control[] {
                this.lblName, this.txtName,
                this.lblProductType, this.cmbProductType,
                this.lblBasePrice, this.numBasePrice,
                this.lblPaperFormula, this.txtPaperFormula,
                this.lblPrintFormula, this.txtPrintFormula,
                this.lblSizeFormula, this.txtSizeFormula,
                this.lblVolumeFormula, this.txtVolumeFormula,
                this.lblFinalFormula, this.txtFinalFormula,
                this.chkIsActive,
                this.btnNew, this.btnSave, this.btnDelete,
                this.btnActivate, this.btnDeactivate, this.btnRefresh
            });

            // MainForm
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.grpFormulaDetails);
            this.Controls.Add(this.dgvFormulas);
            this.Text = "Управление формулами расчета";
            this.Load += new EventHandler(this.FormulasForm_Load);
        }
    }
}
