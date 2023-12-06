using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWFProject
{
    public class PeopleManager
    {
        public BindingList<Person> people { get; set; }
        private SqlConnection sqlConnection = null;
        public PeopleManager(SqlConnection sqlConnection) 
        {
            people = new BindingList<Person>();
            this.sqlConnection = sqlConnection;

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Name, Birthday FROM People", sqlConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row  in dataTable.Rows)
            {
                Person person = new Person(row["Name"].ToString(), (DateTime)row["Birthday"]);
                people.Add(person);
            }
        }
        public void AddPerson(string name, DateTime birthday)
        {
            if (name.Length < 3)
            {
                throw new ArgumentException("The name is too short");
            }

            if (birthday.Date > DateTime.Today) 
            {
                throw new ArgumentException("Date of birth cannot be in the future");
            }

            Person person = new Person(name, birthday.Date);
            people.Add(person);

            SqlCommand command = new SqlCommand(
                $"INSERT INTO [People] (Name, Birthday) VALUES (@Name, @Birthday)",
                sqlConnection);
            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Birthday", birthday.Date);
            command.ExecuteNonQuery();
        }
        public void RemovePerson(Person person)
        {
            people.Remove(person);
        }
        public Person ClosestBirthday()
        {
            var ordered = people.OrderBy(p => p.daysLeft());
            return ordered.First();
        }
    }
}
