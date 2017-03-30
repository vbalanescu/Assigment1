using System;

namespace Depozit.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public string firstName;
        public string lastName;

        public string FullName
        {
            get
            { return firstName + " " + lastName; }
        }

        public int age;

        public DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                    dateOfBirth = value;
                    age = DateTime.Today.Year - dateOfBirth.Year;
            }
        }

    }
}
