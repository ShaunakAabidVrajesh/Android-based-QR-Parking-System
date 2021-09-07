using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace QRadmin
{
    public partial class createhandler : Form
    {
        FirestoreDb database;
        public createhandler()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            homepage home = new homepage();
            home.Show();
        }

        private void createhandler_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"qrparking.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("qrparking-b6fa9");
        }

        private void createbtn_Click(object sender, EventArgs e)
        {
            custId_add();
        }

        void custId_add()
        {
            string name = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;
            string incharge = comboBox1.Text;
            string number = textBox5.Text;

            DocumentReference doc = database.Collection("parkingDetails").Document(incharge);

            Dictionary<string, object> data1 = new Dictionary<string, object>
            {
                 {"Handler Name",name },
                {"Email_ID",email },
                {"Passwrod",password },
                {"Parking_Site_Name_Incharge",incharge },
                {"Contact_Number",number }

            };

            doc.SetAsync(data1);


            MessageBox.Show("successfull");
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Query allCitiesQuery = database.Collection("parkingDetails");
            QuerySnapshot allCitiesQuerySnapshot = await allCitiesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
                         
        }

        public async Task<createhandler> GetEmployeeData()
        {
            try
            {
                DocumentReference docRef = database.Collection("parkingDetails").Document();
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    createhandler emp = snapshot.ConvertTo<createhandler>();
                    String id = snapshot.Id;
                    String[] array;
                    
                    return emp;
                }
                else
                {
                    return new createhandler();
                }
            }
            catch
            {
                throw;
            }
        }

    }
    }
    
    

