using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3HW2
{
    public class Contact : IContact, IComparable
    {
        private string _key;
        public string Number { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Contact(string firstName, string lastName, string number)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            _key = lastName;
        }

        public string GetKey()
        {
            return _key;
        }

        public int CompareTo(object? obj)
        {
            return GetKey().CompareTo(((Contact)obj).GetKey());
        }
    }
}
