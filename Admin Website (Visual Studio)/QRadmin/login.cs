using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRadmin
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             if(textBox1.Text=="QR@dmin123" && textBox2.Text == "helloworld")
             {
                 MessageBox.Show("Hello Admin Login Successfull !!");
                 this.Hide(); 
                 homepage home = new homepage();
                 home.Show();
             }

            else if(textBox1.Text == null || textBox2.Text == null)
                   {
                       MessageBox.Show("Please Enter the Required Fields.");
                   }

            else
                   {
                       MessageBox.Show("Username or Password not correct, Try Again.");
                   }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
