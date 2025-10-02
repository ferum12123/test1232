using Asystem.Core.Entities;
using Asystem.DesktopClient.Services;

namespace Asystem.DesktopClient
{
    public partial class PrepressForm : Form
    {
        private readonly ApiClient _api = new("http://localhost:5000/");
        private readonly int _orderId;

        public PrepressForm(int orderId)
        {
            _orderId = orderId;
            InitializeComponent();
        }

        private async void PrepressForm_Load(object sender, EventArgs e)
        {
            var order = await _api.GetOrderAsync(_orderId);
            if (order != null)
            {
                lblOrder.Text = $"{order.Number} — {order.CustomerName}";
                dgvTasks.DataSource = order.Tasks.ToList();
            }
        }

        private async void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow?.DataBoundItem is OrderTask t)
            {
                var ok = await _api.CompleteTaskAsync(_orderId, t.Id);
                if (ok)
                {
                    MessageBox.Show("Отмечено");
                    var order = await _api.GetOrderAsync(_orderId);
                    dgvTasks.DataSource = order?.Tasks.ToList();
                }
            }
        }

        private async void btnToPrint_Click(object sender, EventArgs e)
        {
            var ok = await _api.ChangeStageAsync(_orderId, Asystem.Core.Entities.Enums.OrderStage.Print);
            if (ok) MessageBox.Show("Переведено в этап Печать");
        }
    }
}
