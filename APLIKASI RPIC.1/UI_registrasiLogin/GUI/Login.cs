using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_registrasiLogin.Managers;
using UI_registrasiLogin.Models;

namespace UI_registrasiLogin
{
    public partial class Login : Form
    {
        private UserManager<User> userMgr;
        public Login()
        {
            InitializeComponent();
            userMgr = new UserManager<User>();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (userMgr.AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful.");
                ReservationForm mngRsv = new ReservationForm();

                this.Close();
                mngRsv.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registrasi reg = new Registrasi();

            this.Close();
            reg.Show();

        }
    }
}
