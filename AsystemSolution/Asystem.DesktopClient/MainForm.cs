using Asystem.Core.Entities;
using Asystem.DesktopClient.Services;
using System.Net.Http.Json;

namespace Asystem.DesktopClient
{
    public partial class MainForm : Form
    {
        private readonly ApiClient _api;
        private List<Order> _orders = new();
        private List<Formula> _formulas = new();
    private List<Product> _products = new();
        private Formula? _editingFormula = null;
    private Product? _editingProduct = null;

        public MainForm()
        {
            InitializeComponent();
            _api = new ApiClient("http://localhost:5000/"); // убедись, что API запущен
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadOrders();
            await LoadMaterials();
            await LoadFormulas();
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                _products = await _api.GetProductsAsync();
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось получить продукты с API. Проверьте, что сервер запущен на http://localhost:5000.\n\nОшибка: {ex.Message}", "Ошибка сети", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefreshProducts_Click(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            _editingProduct = null;
            ShowProductEditForm();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
            {
                _editingProduct = p;
                ShowProductEditForm();
            }
            else
            {
                MessageBox.Show("Выберите продукт для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
            {
                var res = MessageBox.Show($"Удалить продукт '{p.Name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    var ok = await _api.DeleteProductAsync(p.Id);
                    if (ok) await LoadProducts();
                    else MessageBox.Show("Ошибка удаления продукта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void ShowProductEditForm()
        {
            grpProductEdit.Visible = true;
            dgvProducts.Visible = false;
            btnRefreshProducts.Visible = false;
            btnNewProduct.Visible = false;
            btnEditProduct.Visible = false;
            btnDeleteProduct.Visible = false;

            if (_editingProduct != null)
            {
                txtProductCode.Text = _editingProduct.Code;
                txtProductName.Text = _editingProduct.Name;
                cmbProductType_Product.SelectedItem = _editingProduct.ProductType;
                numProductPrice.Value = _editingProduct.Price;
                txtProductDescription.Text = _editingProduct.Description;
                chkProductActive.Checked = _editingProduct.IsActive;
            }
            else
            {
                txtProductCode.Text = "";
                txtProductName.Text = "";
                cmbProductType_Product.SelectedIndex = 0;
                numProductPrice.Value = 0;
                txtProductDescription.Text = "";
                chkProductActive.Checked = true;
            }
        }

        private async void btnSaveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var prod = new Product
                {
                    Code = txtProductCode.Text,
                    Name = txtProductName.Text,
                    ProductType = cmbProductType_Product.SelectedItem?.ToString() ?? "",
                    Price = numProductPrice.Value,
                    Description = txtProductDescription.Text,
                    IsActive = chkProductActive.Checked
                };

                if (_editingProduct != null)
                {
                    prod.Id = _editingProduct.Id;
                    var ok = await _api.UpdateProductAsync(prod.Id, prod);
                    if (!ok) MessageBox.Show("Ошибка обновления продукта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var created = await _api.CreateProductAsync(prod);
                    if (created == null) MessageBox.Show("Ошибка создания продукта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                await LoadProducts();
                HideProductEditForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении продукта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            HideProductEditForm();
        }

        private void HideProductEditForm()
        {
            grpProductEdit.Visible = false;
            dgvProducts.Visible = true;
            btnRefreshProducts.Visible = true;
            btnNewProduct.Visible = true;
            btnEditProduct.Visible = true;
            btnDeleteProduct.Visible = true;
            _editingProduct = null;
        }

        private async Task LoadOrders()
        {
            try
            {
                _orders = await _api.GetOrdersAsync();
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = _orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось получить заказы с API. Проверьте, что сервер запущен на http://localhost:5000.\n\nОшибка: {ex.Message}", "Ошибка сети", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadMaterials()
        {
            try
            {
                var mats = await _api.GetMaterialsAsync();
                dgvMaterials.DataSource = null;
                dgvMaterials.DataSource = mats;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось получить материалы с API. Проверьте, что сервер запущен на http://localhost:5000.\n\nОшибка: {ex.Message}", "Ошибка сети", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadOrders();
            await LoadMaterials();
            await LoadFormulas();
        }

        private async void btnCompleteTask_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                if (order.Tasks?.Any() != true)
                {
                    MessageBox.Show("У заказа нет задач");
                    return;
                }

                var task = order.Tasks.FirstOrDefault(t => !t.IsCompleted);
                if (task == null)
                {
                    MessageBox.Show("Нет незавершённых задач");
                    return;
                }

                var ok = await _api.CompleteTaskAsync(order.Id, task.Id);
                if (ok) MessageBox.Show("Задача отмечена выполненной");
                await LoadOrders();
            }
        }

        private async void btnNextStage_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                // калька: переход на следующий этап
                var next = order.Stage switch
                {
                    Asystem.Core.Entities.Enums.OrderStage.New => Asystem.Core.Entities.Enums.OrderStage.Prepress,
                    Asystem.Core.Entities.Enums.OrderStage.Prepress => Asystem.Core.Entities.Enums.OrderStage.Print,
                    Asystem.Core.Entities.Enums.OrderStage.Print => Asystem.Core.Entities.Enums.OrderStage.Postpress,
                    Asystem.Core.Entities.Enums.OrderStage.Postpress => Asystem.Core.Entities.Enums.OrderStage.Ready,
                    Asystem.Core.Entities.Enums.OrderStage.Ready => Asystem.Core.Entities.Enums.OrderStage.Delivered,
                    _ => order.Stage
                };

                var ok = await _api.ChangeStageAsync(order.Id, next);
                if (ok) MessageBox.Show($"Этап изменён: {next}");
                await LoadOrders();
            }
        }

        private void btnOpenPrepress_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                var f = new PrepressForm(order.Id);
                f.ShowDialog();
                _ = LoadOrders();
            }
        }

        private async Task LoadFormulas()
        {
            try
            {
                _formulas = await _api.GetFormulasAsync();
                dgvFormulas.DataSource = null;
                dgvFormulas.DataSource = _formulas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось получить формулы с API. Проверьте, что сервер запущен на http://localhost:5000.\n\nОшибка: {ex.Message}", "Ошибка сети", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefreshFormulas_Click(object sender, EventArgs e)
        {
            await LoadFormulas();
        }

        private void btnNewFormula_Click(object sender, EventArgs e)
        {
            _editingFormula = null;
            ShowFormulaEditForm();
        }

        private void btnEditFormula_Click(object sender, EventArgs e)
        {
            if (dgvFormulas.CurrentRow?.DataBoundItem is Formula formula)
            {
                _editingFormula = formula;
                ShowFormulaEditForm();
            }
            else
            {
                MessageBox.Show("Выберите формулу для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDeleteFormula_Click(object sender, EventArgs e)
        {
            if (dgvFormulas.CurrentRow?.DataBoundItem is Formula formula)
            {
                var result = MessageBox.Show($"Удалить формулу '{formula.Name}'?", "Подтверждение", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _api.DeleteFormulaAsync(formula.Id);
                        if (success)
                        {
                            MessageBox.Show("Формула удалена успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadFormulas();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка удаления формулы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления формулы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите формулу для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowFormulaEditForm()
        {
            grpFormulaEdit.Visible = true;
            dgvFormulas.Visible = false;
            btnRefreshFormulas.Visible = false;
            btnNewFormula.Visible = false;
            btnEditFormula.Visible = false;
            btnDeleteFormula.Visible = false;

            if (_editingFormula != null)
            {
                txtFormulaName.Text = _editingFormula.Name;
                cmbFormulaProductType.SelectedItem = _editingFormula.ProductType;
                numFormulaBasePrice.Value = _editingFormula.BasePrice;
                txtFormulaPaper.Text = _editingFormula.PaperMultiplierFormula;
                txtFormulaPrint.Text = _editingFormula.PrintMultiplierFormula;
                txtFormulaSize.Text = _editingFormula.SizeMultiplierFormula;
                txtFormulaVolume.Text = _editingFormula.VolumeDiscountFormula;
                txtFormulaFinal.Text = _editingFormula.FinalFormula;
                chkFormulaActive.Checked = _editingFormula.IsActive;
            }
            else
            {
                txtFormulaName.Text = "";
                cmbFormulaProductType.SelectedIndex = 0;
                numFormulaBasePrice.Value = 0;
                txtFormulaPaper.Text = "";
                txtFormulaPrint.Text = "";
                txtFormulaSize.Text = "";
                txtFormulaVolume.Text = "";
                txtFormulaFinal.Text = "";
                chkFormulaActive.Checked = true;
            }
        }

        private async void btnSaveFormula_Click(object sender, EventArgs e)
        {
            try
            {
                var formula = new Formula
                {
                    Name = txtFormulaName.Text,
                    ProductType = cmbFormulaProductType.SelectedItem?.ToString() ?? "",
                    BasePrice = numFormulaBasePrice.Value,
                    PaperMultiplierFormula = txtFormulaPaper.Text,
                    PrintMultiplierFormula = txtFormulaPrint.Text,
                    SizeMultiplierFormula = txtFormulaSize.Text,
                    VolumeDiscountFormula = txtFormulaVolume.Text,
                    FinalFormula = txtFormulaFinal.Text,
                    IsActive = chkFormulaActive.Checked
                };

                if (_editingFormula != null)
                {
                    formula.Id = _editingFormula.Id;
                    var success = await _api.UpdateFormulaAsync(formula.Id, formula);
                    if (success)
                    {
                        MessageBox.Show("Формула успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка обновления формулы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var createdFormula = await _api.CreateFormulaAsync(formula);
                    if (createdFormula != null)
                    {
                        MessageBox.Show("Формула успешно создана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка создания формулы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                await LoadFormulas();
                HideFormulaEditForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении формулы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelFormula_Click(object sender, EventArgs e)
        {
            HideFormulaEditForm();
        }

        private void HideFormulaEditForm()
        {
            grpFormulaEdit.Visible = false;
            dgvFormulas.Visible = true;
            btnRefreshFormulas.Visible = true;
            btnNewFormula.Visible = true;
            btnEditFormula.Visible = true;
            btnDeleteFormula.Visible = true;
            _editingFormula = null;
        }
    }
}
