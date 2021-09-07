using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace QRadmin
{
    public partial class handler : Form
    {
        FirestoreDb database;
        public handler()
        {
            InitializeComponent();
        }


        //-------------------------Connection code To Connect TO GOOGLE FIREBASE-----------------------------------//
        private void handler_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"qrparking.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("qrparking-b6fa9");
        }



        //----------------------------------FUNCTION TO ADD DATA INTO THE FIRESTORE--------------------------------------------//

        private void addbtn_Click(object sender, EventArgs e)
        {
            handlerAdd();
        }

        async void handlerAdd()
        {
            string Email = textBox1.Text;
            string Contactnumber = textBox2.Text;
            string Handlername = textBox3.Text;
            string Parkingsiteincharge = textBox6.Text;



            DocumentReference doc = database.Collection("parkingDetails").Document(Parkingsiteincharge).Collection("Handler").Document(Contactnumber);

            //---------------------INITIALLY SEARCHING OF THE DATA WILL BE DONE TO CHECK IF IT IS ALREADY PRESENT OR NOT----------------------//
            string db;
            DocumentReference docRef = database.Collection("parkingDetails").Document(Parkingsiteincharge).Collection("Handler").Document(Contactnumber);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
           
            if (snapshot.Exists)
            {
                qr qr = snapshot.ConvertTo<qr>();

                db = qr.name;
                MessageBox.Show("Handler already Exist.");
                textBox2.Text = null;
            }

            //---------------------------------CODE TO ADD DATA INTO THE FIRESTORE---------------------------------------------//
            else
            {
                if (Email.LastIndexOf("@") > -1)
                {
                    Dictionary<string, object> data1 = new Dictionary<string, object>
                          {
                               {"Email",Email },
                               {"Contactnumber",Contactnumber },
                               {"Handlername",Handlername },
                               {"Parkingsiteincharge",Parkingsiteincharge },

                           };

                    doc.SetAsync(data1);

                    MessageBox.Show("Data Added Successfully");

                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    textBox6.Text = null;
                }

                else if(Email == null)
                {
                    MessageBox.Show("Email Field is empty");
                }

                else
                {
                    MessageBox.Show("Invalid Email Address");
                }
            }
        }



        //--------------------------------------CODE TO UPDATE DATA INTO THE FIRESTORE-------------------------------------------------//
        private void updatebtn_Click(object sender, EventArgs e)
        {
            handlerUpdate();
        }

        async void handlerUpdate()
        {
            string Email = textBox1.Text;
            string Contactnumber = textBox2.Text;
            string Handlername = textBox3.Text;
            string Parkingsiteincharge = textBox6.Text;

                DocumentReference docRef = database.Collection("parkingDetails").Document(Parkingsiteincharge).Collection("Handler").Document(Contactnumber);
                Dictionary<string, object> data1 = new Dictionary<string, object>
            {
                               {"Email",Email },
                               {"Handlername",Handlername },
            };

                docRef.UpdateAsync(data1);
                MessageBox.Show("Updated successfully");

                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox6.Text = null;
            
        }




        //--------------------------------------CODE TO DELETE DATA INTO THE FIRESTORE-------------------------------------------------//
        private void deletebtn_Click(object sender, EventArgs e)
        {
            handlerDelete();
        }

        void handlerDelete()
        {
            string Contactnumber = textBox2.Text;
            string Parkingsiteincharge = textBox6.Text;
            DocumentReference doc = database.Collection("parkingDetails").Document(Parkingsiteincharge).Collection("Handler").Document(Contactnumber);
            doc.DeleteAsync();
            MessageBox.Show("Data Deleted Successfully.");

            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox6.Text = null;
        }



        //--------------------------------------CODE TO SEARCH DATA INTO THE FIRESTORE-------------------------------------------------//
        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        async void search()
        {
            string Contactnumber = textBox2.Text;
            string site = textBox6.Text;
            DocumentReference docRef = database.Collection("parkingDetails").Document(site).Collection("Handler").Document(Contactnumber);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                MessageBox.Show("Data Found");
                qr qr = snapshot.ConvertTo<qr>();

                textBox1.Text = qr.Email;
                textBox2.Text = qr.Contactnumber;
                textBox3.Text = qr.Handlername;
                textBox6.Text = qr.Parkingsiteincharge;
            }

            else
            {
                MessageBox.Show("Data Not Found.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            homepage home = new homepage();
            home.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
 }
