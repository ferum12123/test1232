using Asystem.Core.Entities;
using System.ComponentModel;

namespace Asystem.DesktopClient
{
    public partial class ProductForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Product? Product { get; private set; }

        private TextBox txtCode = new TextBox();
        private TextBox txtName = new TextBox();
        private ComboBox cmbProductType = new ComboBox();
        private NumericUpDown numPrice = new NumericUpDown();
        private CheckBox chkActive = new CheckBox();
        private TextBox txtDescription = new TextBox();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();

        public ProductForm(Product? product = null)
        {
            Product = product;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = Product == null ? "Новый продукт" : "Редактировать продукт";
            this.ClientSize = new Size(420, 320);

            var lblCode = new Label { Text = "Код:", Location = new Point(10, 20), Size = new Size(100, 20) };
            txtCode.Location = new Point(120, 20); txtCode.Size = new Size(260, 20);

            var lblName = new Label { Text = "Название:", Location = new Point(10, 50), Size = new Size(100, 20) };
            txtName.Location = new Point(120, 50); txtName.Size = new Size(260, 20);

            var lblType = new Label { Text = "Тип продукции:", Location = new Point(10, 80), Size = new Size(100, 20) };
            cmbProductType.Location = new Point(120, 80); cmbProductType.Size = new Size(200, 20);
            cmbProductType.Items.AddRange(new[] { "business-card", "brochure", "poster", "banner", "calendar", "flyer" });

            var lblPrice = new Label { Text = "Цена:", Location = new Point(10, 110), Size = new Size(100, 20) };
            numPrice.Location = new Point(120, 110); numPrice.Size = new Size(100, 20); numPrice.DecimalPlaces = 2; numPrice.Maximum = 999999;

            var lblActive = new Label { Text = "Активен:", Location = new Point(10, 140), Size = new Size(100, 20) };
            chkActive.Location = new Point(120, 140);

            var lblDesc = new Label { Text = "Описание:", Location = new Point(10, 170), Size = new Size(100, 20) };
            txtDescription.Location = new Point(120, 170); txtDescription.Size = new Size(260, 80); txtDescription.Multiline = true;

            btnSave.Text = "Сохранить"; btnSave.Location = new Point(120, 260); btnSave.Click += BtnSave_Click;
            btnCancel.Text = "Отмена"; btnCancel.Location = new Point(220, 260); btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { lblCode, txtCode, lblName, txtName, lblType, cmbProductType, lblPrice, numPrice, lblActive, chkActive, lblDesc, txtDescription, btnSave, btnCancel });

            if (Product != null) LoadProductToForm();
        }

        private void LoadProductToForm()
        {
            txtCode.Text = Product!.Code;
            txtName.Text = Product.Name;
            cmbProductType.Text = Product.ProductType;
            numPrice.Value = Product.Price;
            chkActive.Checked = Product.IsActive;
            txtDescription.Text = Product.Description;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название продукта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product ??= new Product();
            Product.Code = txtCode.Text;
            Product.Name = txtName.Text;
            Product.ProductType = cmbProductType.Text;
            Product.Price = numPrice.Value;
            Product.IsActive = chkActive.Checked;
            Product.Description = txtDescription.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}
