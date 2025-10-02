using Asystem.Core.Entities;
using Asystem.DesktopClient.Services;

namespace Asystem.DesktopClient
{
    public partial class FormulasForm : Form
    {
        private readonly ApiClient _api = new("http://localhost:5000/");
        private List<Formula> _formulas = new();
        private Formula? _currentFormula;

        public FormulasForm()
        {
            InitializeComponent();
        }

        private async void FormulasForm_Load(object sender, EventArgs e)
        {
            await LoadFormulas();
            LoadProductTypes();
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
                MessageBox.Show($"Ошибка загрузки формул: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductTypes()
        {
            cmbProductType.Items.Clear();
            cmbProductType.Items.AddRange(new[]
            {
                "business-card", "brochure", "poster", "banner", "calendar", "flyer"
            });
        }

        private void dgvFormulas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFormulas.CurrentRow?.DataBoundItem is Formula formula)
            {
                LoadFormulaToForm(formula);
            }
        }

        private void LoadFormulaToForm(Formula formula)
        {
            _currentFormula = formula;
            txtName.Text = formula.Name;
            cmbProductType.Text = formula.ProductType;
            numBasePrice.Value = formula.BasePrice;
            txtPaperFormula.Text = formula.PaperMultiplierFormula;
            txtPrintFormula.Text = formula.PrintMultiplierFormula;
            txtSizeFormula.Text = formula.SizeMultiplierFormula;
            txtVolumeFormula.Text = formula.VolumeDiscountFormula;
            txtFinalFormula.Text = formula.FinalFormula;
            chkIsActive.Checked = formula.IsActive;
        }

        private void ClearForm()
        {
            _currentFormula = null;
            txtName.Text = "";
            cmbProductType.Text = "";
            numBasePrice.Value = 0;
            txtPaperFormula.Text = "";
            txtPrintFormula.Text = "";
            txtSizeFormula.Text = "";
            txtVolumeFormula.Text = "";
            txtFinalFormula.Text = "";
            chkIsActive.Checked = true;
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(cmbProductType.Text))
            {
                MessageBox.Show("Заполните название и тип продукции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var formula = new Formula
                {
                    Id = _currentFormula?.Id ?? 0,
                    Name = txtName.Text,
                    ProductType = cmbProductType.Text,
                    BasePrice = numBasePrice.Value,
                    PaperMultiplierFormula = txtPaperFormula.Text,
                    PrintMultiplierFormula = txtPrintFormula.Text,
                    SizeMultiplierFormula = txtSizeFormula.Text,
                    VolumeDiscountFormula = txtVolumeFormula.Text,
                    FinalFormula = txtFinalFormula.Text,
                    IsActive = chkIsActive.Checked
                };

                if (_currentFormula == null)
                {
                    // Создание новой формулы
                    var created = await _api.CreateFormulaAsync(formula);
                    if (created != null)
                    {
                        MessageBox.Show("Формула создана успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка создания формулы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Обновление существующей формулы
                    var success = await _api.UpdateFormulaAsync(_currentFormula.Id, formula);
                    if (success)
                    {
                        MessageBox.Show("Формула обновлена успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка обновления формулы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                await LoadFormulas();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения формулы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentFormula == null)
            {
                MessageBox.Show("Выберите формулу для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить формулу '{_currentFormula.Name}'?", "Подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _api.DeleteFormulaAsync(_currentFormula.Id);
                    if (success)
                    {
                        MessageBox.Show("Формула удалена успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadFormulas();
                        ClearForm();
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

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (_currentFormula == null)
            {
                MessageBox.Show("Выберите формулу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var success = await _api.ActivateFormulaAsync(_currentFormula.Id);
                if (success)
                {
                    MessageBox.Show("Формула активирована", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadFormulas();
                }
                else
                {
                    MessageBox.Show("Ошибка активации формулы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка активации формулы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (_currentFormula == null)
            {
                MessageBox.Show("Выберите формулу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var success = await _api.DeactivateFormulaAsync(_currentFormula.Id);
                if (success)
                {
                    MessageBox.Show("Формула деактивирована", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadFormulas();
                }
                else
                {
                    MessageBox.Show("Ошибка деактивации формулы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка деактивации формулы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadFormulas();
        }

        public void LoadFormulaForEdit(Formula formula)
        {
            LoadFormulaToForm(formula);
        }
    }
}
