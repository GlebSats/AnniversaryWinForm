using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstWFProject
{
    public partial class PersonForm : Form
    {
        private PeopleManager peopleManager;
        public PersonForm(PeopleManager peopleManager)
        {
            InitializeComponent();
            this.peopleManager = peopleManager;
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try 
            {
                peopleManager.AddPerson(nameTextBox.Text, birthDateTimePicker.Value);
                Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
