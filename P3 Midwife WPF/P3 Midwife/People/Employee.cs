using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace P3_Midwife
{
    public class Employee : DependencyObject
    {

        public static DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(Employee));
        public static DependencyProperty IDProperty = DependencyProperty.Register(nameof(ID), typeof(int), typeof(Employee));
        public static DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(Employee));
        public static DependencyProperty TelephoneNumberProperty = DependencyProperty.Register(nameof(TelephoneNumber), typeof(int), typeof(Employee));
        public static DependencyProperty EmailProperty = DependencyProperty.Register(nameof(Email), typeof(string), typeof(Employee));



        public int ID { get { return (int)this.GetValue(IDProperty); } set { this.SetValue(IDProperty, value); } }
        public string Name { get { return (string)this.GetValue(NameProperty); } set { this.SetValue(NameProperty, value); } }
        public string Password { get { return (string)this.GetValue(PasswordProperty); } set { this.SetValue(PasswordProperty, value); } }
        public int TelephoneNumber { get { return (int)this.GetValue(TelephoneNumberProperty); } set { this.SetValue(TelephoneNumberProperty, value); } }
        public string Email { get { return (string)this.GetValue(EmailProperty); } set { this.SetValue(EmailProperty, value); } }

        public Employee(int id, string name, string password, int telephonenumber, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
        }

        public Employee()
        {
        }

        public override string ToString()
        {
            return "Name: " + Name + " - ID: " + ID;
        }
    }
}
