using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWFProject
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday{ get; set; }
        public Person(string name, DateTime birthday) { 
            Name = name;
            Birthday = birthday;
        }
        public int calculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (today < Birthday.AddYears(age)) {
                age--;
            }
            return age;
        }
        public int daysLeft()
        {
            DateTime today = DateTime.Today;
            DateTime nextBirthday = Birthday.AddYears(calculateAge() + 1);
            TimeSpan difference = nextBirthday - today;
            return Convert.ToInt32(difference.TotalDays);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
