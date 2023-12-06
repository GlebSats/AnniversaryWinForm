using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace FirstWFProject
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private PeopleManager peopleManager = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BirthdayDB"].ConnectionString);
            try
            {
                sqlConnection.Open();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Сonnection to the database could not be established", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            peopleManager = new PeopleManager(sqlConnection);
            todayLabel.Text = DateTime.Now.ToLongDateString();
            listBox.DataSource = peopleManager.people;
            refreshClosest();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void addButton_Click(object sender, EventArgs e)
        {
            PersonForm personForm = new PersonForm(peopleManager);
            personForm.ShowDialog();
            refreshClosest();
        }
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                Person person = (Person)listBox.SelectedItem;
                peopleManager.RemovePerson(person);

                SqlCommand command = new SqlCommand(
                $"DELETE FROM People WHERE Name = @name",
                sqlConnection);
                command.Parameters.AddWithValue("name", person.Name);
                command.ExecuteNonQuery();

                refreshClosest();
            }
        }
        private void refreshClosest()
        {
            if (peopleManager.people.Count > 0)
            {
                Person closest = peopleManager.ClosestBirthday();
                int age = closest.calculateAge();
                    if (DateTime.Today != closest.Birthday)
                    {
                        age++;
                    }
                    nearestLabel.Text = closest.Name + " (" + age + " years in " + closest.daysLeft() + " days)";
            }
            else
            {
                nearestLabel.Text = "No people in the list";
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                Person selected = (Person)listBox.SelectedItem;
                birthdayLabel.Text = selected.Birthday.ToLongDateString();
                ageLabel.Text = selected.calculateAge().ToString();
                monthCalendar.SelectionStart = selected.Birthday;
            }
        }
    }
}
