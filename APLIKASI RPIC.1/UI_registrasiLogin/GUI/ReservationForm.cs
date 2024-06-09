using System;
using System.Linq;
using System.Windows.Forms;
using UI_registrasiLogin.GUI;
using UI_registrasiLogin.Managers;
using UI_registrasiLogin.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UI_registrasiLogin
{
    // Update the ReservationForm to Use the State Machine
    public partial class ReservationForm : Form
    {
        private ReservationManager manager;
        private PelangganReserve pelangganReserve;

        public ReservationForm()
        {
            InitializeComponent();
            manager = new ReservationManager();
            LoadPCs();
            
            pelangganReserve = new PelangganReserve();
        }

        private void LoadPCs()
        {
         dataGridView1.DataSource = manager.GetPCs().Select(pc => new
            {
                Number = pc.Number,
                Specification = pc.Specification,
                State = pc.State
            }).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int pcNumber) &&
         !string.IsNullOrWhiteSpace(textBox2.Text) &&
         Enum.TryParse(textBox3.Text, out ReservationState state))
            {
                //Code reuse here :
/*                using the PCExists method from the ReservationManager class. 
 *                This ensures that the logic for checking the existence of a PC is encapsulated within the
 *                ReservationManager and reused in your form, promoting better maintainability and avoiding code duplication.
*/                if (!manager.PCExists(pcNumber))
                {
                    bool success = manager.AddPC(pcNumber, textBox2.Text, state);

                    if (success)
                    {
                        LoadPCs();
                        ClearInputs();
                        pelangganReserve.ReceiveDataFromReservationForm(dataGridView1);
                        MessageBox.Show("PC added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add PC. Please check the input details.");
                    }
                }
                else
                {
                    MessageBox.Show("PC number already exists. Please enter a unique PC number.");
                }
            }
            else
            {
                MessageBox.Show("Please enter valid PC details.");
            }
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginP lgnP = new LoginP();

            this.Close();
            lgnP.Show();
        }
    }
}
