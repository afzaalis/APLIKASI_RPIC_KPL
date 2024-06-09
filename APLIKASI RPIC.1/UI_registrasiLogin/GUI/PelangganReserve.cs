using System.Linq;
using System.Windows.Forms;
using UI_registrasiLogin.Models;

namespace UI_registrasiLogin.GUI
{
    public partial class PelangganReserve : Form
    {
        public PelangganReserve()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 3; 
            dataGridView1.Columns[0].Name = "Number";
            dataGridView1.Columns[1].Name = "Specification";
            dataGridView1.Columns[2].Name = "State";

        
            PopulateSampleData();
        }

        private void PopulateSampleData()
        {
            ReservationState availableState = ReservationState.Available;
            ReservationState reservedState = ReservationState.Reserved;

            dataGridView1.Rows.Add(1, "Specs1", availableState);
            dataGridView1.Rows.Add(2, "Specs2", availableState);
            dataGridView1.Rows.Add(3, "Specs3", reservedState);
        }

        public void ReceiveDataFromReservationForm(DataGridView dataGridView)
        {
            dataGridView1.Rows.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                dataGridView1.Rows.Add(row.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string state = selectedRow.Cells["State"].Value.ToString();

                if (state == "Available")
                {
                    MessageBox.Show("Reservasi Berhasil!");
                }
                else
                {
                    MessageBox.Show("Reservasi Gagal! PC sedang digunakan.");
                }
            }
            else
            {
                MessageBox.Show("Please select a PC.");
            }
        }
    }
}
