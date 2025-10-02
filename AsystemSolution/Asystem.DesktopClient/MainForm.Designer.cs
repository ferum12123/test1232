namespace Asystem.DesktopClient
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvOrders;
        private DataGridView dgvMaterials;
        private Button btnRefresh;
        private Button btnCompleteTask;
        private Button btnNextStage;
        private Button btnOpenPrepress;
        private TabControl tabControl;
        private TabPage tabOrders;
        private TabPage tabMaterials;
        private TabPage tabFormulas;
    private TabPage tabProducts;
        private DataGridView dgvFormulas;
    private DataGridView dgvProducts;
        private Button btnRefreshFormulas;
        private Button btnNewFormula;
        private Button btnEditFormula;
        private Button btnDeleteFormula;
    private Button btnRefreshProducts;
    private Button btnNewProduct;
    private Button btnEditProduct;
    private Button btnDeleteProduct;
    private GroupBox grpProductEdit;
    private TextBox txtProductCode;
    private TextBox txtProductName;
    private ComboBox cmbProductType_Product;
    private NumericUpDown numProductPrice;
    private TextBox txtProductDescription;
    private CheckBox chkProductActive;
    private Button btnSaveProduct;
    private Button btnCancelProduct;
        private GroupBox grpFormulaEdit;
        private TextBox txtFormulaName;
        private ComboBox cmbFormulaProductType;
        private NumericUpDown numFormulaBasePrice;
        private TextBox txtFormulaPaper;
        private TextBox txtFormulaPrint;
        private TextBox txtFormulaSize;
        private TextBox txtFormulaVolume;
        private TextBox txtFormulaFinal;
        private CheckBox chkFormulaActive;
        private Button btnSaveFormula;
        private Button btnCancelFormula;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabOrders = new TabPage();
            this.tabMaterials = new TabPage();
            this.tabFormulas = new TabPage();
            this.dgvOrders = new DataGridView();
            this.dgvMaterials = new DataGridView();
            this.btnRefresh = new Button();
            this.btnCompleteTask = new Button();
            this.btnNextStage = new Button();
            this.btnOpenPrepress = new Button();
            this.dgvFormulas = new DataGridView();
            this.btnRefreshFormulas = new Button();
            this.btnNewFormula = new Button();
            this.btnEditFormula = new Button();
            this.btnDeleteFormula = new Button();
            this.grpFormulaEdit = new GroupBox();
            this.txtFormulaName = new TextBox();
            this.cmbFormulaProductType = new ComboBox();
            this.numFormulaBasePrice = new NumericUpDown();
            this.txtFormulaPaper = new TextBox();
            this.txtFormulaPrint = new TextBox();
            this.txtFormulaSize = new TextBox();
            this.txtFormulaVolume = new TextBox();
            this.txtFormulaFinal = new TextBox();
            this.chkFormulaActive = new CheckBox();
            this.btnSaveFormula = new Button();
            this.btnCancelFormula = new Button();

            // TabControl
            this.tabControl.Controls.Add(this.tabOrders);
            this.tabControl.Controls.Add(this.tabMaterials);
            this.tabControl.Controls.Add(this.tabFormulas);
            this.tabProducts = new TabPage();
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Dock = DockStyle.Fill;

            // Orders tab
            this.tabOrders.Text = "Заказы";
            this.tabOrders.Controls.Add(this.dgvOrders);
            this.tabOrders.Controls.Add(this.btnRefresh);
            this.tabOrders.Controls.Add(this.btnCompleteTask);
            this.tabOrders.Controls.Add(this.btnNextStage);
            this.tabOrders.Controls.Add(this.btnOpenPrepress);

            // Materials tab
            this.tabMaterials.Text = "Материалы";
            this.tabMaterials.Controls.Add(this.dgvMaterials);

            // Formulas tab
            this.tabFormulas.Text = "Формулы";
            this.tabFormulas.Controls.Add(this.dgvFormulas);
            this.tabFormulas.Controls.Add(this.grpFormulaEdit);
            this.tabFormulas.Controls.Add(this.btnRefreshFormulas);
            this.tabFormulas.Controls.Add(this.btnNewFormula);
            this.tabFormulas.Controls.Add(this.btnEditFormula);
            this.tabFormulas.Controls.Add(this.btnDeleteFormula);

            // Products tab
            this.tabProducts.Text = "Продукты";
            this.dgvProducts = new DataGridView();
            this.btnRefreshProducts = new Button();
            this.btnNewProduct = new Button();
            this.btnEditProduct = new Button();
            this.btnDeleteProduct = new Button();

            this.tabProducts.Controls.Add(this.dgvProducts);
            this.tabProducts.Controls.Add(this.btnRefreshProducts);
            this.tabProducts.Controls.Add(this.btnNewProduct);
            this.tabProducts.Controls.Add(this.btnEditProduct);
            this.tabProducts.Controls.Add(this.btnDeleteProduct);

            // Product edit group (hidden by default)
            this.grpProductEdit = new GroupBox();
            this.txtProductCode = new TextBox();
            this.txtProductName = new TextBox();
            this.cmbProductType_Product = new ComboBox();
            this.numProductPrice = new NumericUpDown();
            this.txtProductDescription = new TextBox();
            this.chkProductActive = new CheckBox();
            this.btnSaveProduct = new Button();
            this.btnCancelProduct = new Button();

            this.tabProducts.Controls.Add(this.grpProductEdit);

            this.grpProductEdit.Dock = DockStyle.Fill;
            this.grpProductEdit.Text = "Редактирование продукта";
            this.grpProductEdit.Padding = new Padding(10);
            this.grpProductEdit.Visible = false;

            // product edit controls
            this.txtProductCode.Location = new Point(120, 30);
            this.txtProductCode.Size = new Size(300, 20);

            this.txtProductName.Location = new Point(120, 60);
            this.txtProductName.Size = new Size(300, 20);

            this.cmbProductType_Product.Location = new Point(120, 90);
            this.cmbProductType_Product.Size = new Size(200, 20);
            this.cmbProductType_Product.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProductType_Product.Items.AddRange(new[] { "business-card", "brochure", "poster", "banner", "calendar", "flyer" });

            this.numProductPrice.Location = new Point(120, 120);
            this.numProductPrice.Size = new Size(100, 20);
            this.numProductPrice.DecimalPlaces = 2;
            this.numProductPrice.Maximum = 999999;

            this.txtProductDescription.Location = new Point(120, 150);
            this.txtProductDescription.Size = new Size(400, 80);
            this.txtProductDescription.Multiline = true;

            this.chkProductActive.Text = "Активен";
            this.chkProductActive.Location = new Point(120, 240);
            this.chkProductActive.Size = new Size(100, 20);

            this.btnSaveProduct.Text = "Сохранить";
            this.btnSaveProduct.Location = new Point(120, 270);
            this.btnSaveProduct.Size = new Size(80, 30);
            this.btnSaveProduct.Click += new EventHandler(this.btnSaveProduct_Click);

            this.btnCancelProduct.Text = "Отмена";
            this.btnCancelProduct.Location = new Point(210, 270);
            this.btnCancelProduct.Size = new Size(80, 30);
            this.btnCancelProduct.Click += new EventHandler(this.btnCancelProduct_Click);

            var lblCodeP = new Label { Text = "Код:", Location = new Point(10, 32), Size = new Size(100, 20) };
            var lblNameP = new Label { Text = "Название:", Location = new Point(10, 62), Size = new Size(100, 20) };
            var lblTypeP = new Label { Text = "Тип продукции:", Location = new Point(10, 92), Size = new Size(100, 20) };
            var lblPriceP = new Label { Text = "Цена:", Location = new Point(10, 122), Size = new Size(100, 20) };
            var lblDescP = new Label { Text = "Описание:", Location = new Point(10, 152), Size = new Size(100, 20) };

            this.grpProductEdit.Controls.AddRange(new Control[] {
                lblCodeP, this.txtProductCode,
                lblNameP, this.txtProductName,
                lblTypeP, this.cmbProductType_Product,
                lblPriceP, this.numProductPrice,
                lblDescP, this.txtProductDescription,
                this.chkProductActive,
                this.btnSaveProduct, this.btnCancelProduct
            });

            // dgvOrders
            this.dgvOrders.Dock = DockStyle.Top;
            this.dgvOrders.Height = 300;

            // dgvMaterials
            this.dgvMaterials.Dock = DockStyle.Fill;

            // dgvFormulas
            this.dgvFormulas.Dock = DockStyle.Top;
            this.dgvFormulas.Height = 200;
            this.dgvFormulas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvFormulas.MultiSelect = false;
            this.dgvFormulas.ReadOnly = true;

            // dgvProducts
            this.dgvProducts.Dock = DockStyle.Top;
            this.dgvProducts.Height = 300;
            this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.ReadOnly = true;

            // grpFormulaEdit
            this.grpFormulaEdit.Dock = DockStyle.Fill;
            this.grpFormulaEdit.Text = "Редактирование формулы";
            this.grpFormulaEdit.Padding = new Padding(10);
            this.grpFormulaEdit.Visible = false;

            // Formula edit controls
            this.txtFormulaName.Location = new Point(120, 30);
            this.txtFormulaName.Size = new Size(300, 20);
            this.txtFormulaName.Text = "";

            this.cmbFormulaProductType.Location = new Point(120, 60);
            this.cmbFormulaProductType.Size = new Size(200, 20);
            this.cmbFormulaProductType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFormulaProductType.Items.AddRange(new[] { "business-card", "brochure", "poster", "banner", "calendar", "flyer" });

            this.numFormulaBasePrice.Location = new Point(120, 90);
            this.numFormulaBasePrice.Size = new Size(100, 20);
            this.numFormulaBasePrice.DecimalPlaces = 2;
            this.numFormulaBasePrice.Maximum = 999999;

            this.txtFormulaPaper.Location = new Point(120, 120);
            this.txtFormulaPaper.Size = new Size(400, 20);

            this.txtFormulaPrint.Location = new Point(120, 150);
            this.txtFormulaPrint.Size = new Size(400, 20);

            this.txtFormulaSize.Location = new Point(120, 180);
            this.txtFormulaSize.Size = new Size(400, 20);

            this.txtFormulaVolume.Location = new Point(120, 210);
            this.txtFormulaVolume.Size = new Size(400, 20);

            this.txtFormulaFinal.Location = new Point(120, 240);
            this.txtFormulaFinal.Size = new Size(400, 20);

            this.chkFormulaActive.Text = "Активна";
            this.chkFormulaActive.Location = new Point(120, 270);
            this.chkFormulaActive.Size = new Size(100, 20);

            this.btnSaveFormula.Text = "Сохранить";
            this.btnSaveFormula.Location = new Point(120, 300);
            this.btnSaveFormula.Size = new Size(80, 30);
            this.btnSaveFormula.Click += new EventHandler(this.btnSaveFormula_Click);

            this.btnCancelFormula.Text = "Отмена";
            this.btnCancelFormula.Location = new Point(210, 300);
            this.btnCancelFormula.Size = new Size(80, 30);
            this.btnCancelFormula.Click += new EventHandler(this.btnCancelFormula_Click);

            // Add labels
            var lblName = new Label { Text = "Название:", Location = new Point(10, 32), Size = new Size(100, 20) };
            var lblProductType = new Label { Text = "Тип продукции:", Location = new Point(10, 62), Size = new Size(100, 20) };
            var lblBasePrice = new Label { Text = "Базовая цена:", Location = new Point(10, 92), Size = new Size(100, 20) };
            var lblPaper = new Label { Text = "Формула бумаги:", Location = new Point(10, 122), Size = new Size(100, 20) };
            var lblPrint = new Label { Text = "Формула печати:", Location = new Point(10, 152), Size = new Size(100, 20) };
            var lblSize = new Label { Text = "Формула размера:", Location = new Point(10, 182), Size = new Size(100, 20) };
            var lblVolume = new Label { Text = "Формула скидки:", Location = new Point(10, 212), Size = new Size(100, 20) };
            var lblFinal = new Label { Text = "Итоговая формула:", Location = new Point(10, 242), Size = new Size(100, 20) };

            this.grpFormulaEdit.Controls.AddRange(new Control[] {
                lblName, this.txtFormulaName,
                lblProductType, this.cmbFormulaProductType,
                lblBasePrice, this.numFormulaBasePrice,
                lblPaper, this.txtFormulaPaper,
                lblPrint, this.txtFormulaPrint,
                lblSize, this.txtFormulaSize,
                lblVolume, this.txtFormulaVolume,
                lblFinal, this.txtFormulaFinal,
                this.chkFormulaActive,
                this.btnSaveFormula, this.btnCancelFormula
            });

            // Buttons
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Top = 310;
            this.btnRefresh.Left = 10;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnCompleteTask.Text = "Выполнить задачу";
            this.btnCompleteTask.Top = 310;
            this.btnCompleteTask.Left = 100;
            this.btnCompleteTask.Click += new EventHandler(this.btnCompleteTask_Click);

            this.btnNextStage.Text = "След. этап";
            this.btnNextStage.Top = 310;
            this.btnNextStage.Left = 230;
            this.btnNextStage.Click += new EventHandler(this.btnNextStage_Click);

            this.btnOpenPrepress.Text = "Prepress";
            this.btnOpenPrepress.Top = 310;
            this.btnOpenPrepress.Left = 320;
            this.btnOpenPrepress.Click += new EventHandler(this.btnOpenPrepress_Click);

            // Formula buttons
            this.btnRefreshFormulas.Text = "Обновить";
            this.btnRefreshFormulas.Top = 210;
            this.btnRefreshFormulas.Left = 10;
            this.btnRefreshFormulas.Click += new EventHandler(this.btnRefreshFormulas_Click);

            this.btnNewFormula.Text = "Новая";
            this.btnNewFormula.Top = 210;
            this.btnNewFormula.Left = 100;
            this.btnNewFormula.Click += new EventHandler(this.btnNewFormula_Click);

            this.btnEditFormula.Text = "Редактировать";
            this.btnEditFormula.Top = 210;
            this.btnEditFormula.Left = 180;
            this.btnEditFormula.Click += new EventHandler(this.btnEditFormula_Click);

            this.btnDeleteFormula.Text = "Удалить";
            this.btnDeleteFormula.Top = 210;
            this.btnDeleteFormula.Left = 280;
            this.btnDeleteFormula.Click += new EventHandler(this.btnDeleteFormula_Click);

            // Products buttons
            this.btnRefreshProducts.Text = "Обновить";
            this.btnRefreshProducts.Top = 320;
            this.btnRefreshProducts.Left = 10;
            this.btnRefreshProducts.Click += new EventHandler(this.btnRefreshProducts_Click);

            this.btnNewProduct.Text = "Новый";
            this.btnNewProduct.Top = 320;
            this.btnNewProduct.Left = 100;
            this.btnNewProduct.Click += new EventHandler(this.btnNewProduct_Click);

            this.btnEditProduct.Text = "Редактировать";
            this.btnEditProduct.Top = 320;
            this.btnEditProduct.Left = 180;
            this.btnEditProduct.Click += new EventHandler(this.btnEditProduct_Click);

            this.btnDeleteProduct.Text = "Удалить";
            this.btnDeleteProduct.Top = 320;
            this.btnDeleteProduct.Left = 280;
            this.btnDeleteProduct.Click += new EventHandler(this.btnDeleteProduct_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabControl);
            this.Text = "ASystem Client";
            this.Load += new EventHandler(this.MainForm_Load);
        }
    }
}
