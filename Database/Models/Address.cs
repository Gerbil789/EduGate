using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Address : INotifyPropertyChanged
    {
        public Address() { }
        public Address(Address other)
        {
            City = other.City;
            Street = other.Street;
            Number = other.Number;
            ZipCode = other.ZipCode;
            State = other.State;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Notify<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int AddressId { get; set; }

        private string city;
        public string City { get { return city; } set { Notify(ref city, value); } }

        private string street;
        public string Street { get { return street; } set { Notify(ref street, value); } }
        private string number;
        public string Number { get { return number; } set { Notify(ref number, value); } }
        private string zipCode;
        public string ZipCode { get { return zipCode; } set { Notify(ref zipCode, value); } }
        private string state;
        public string State { get { return state; } set { Notify(ref state, value); } }

        public override string ToString()
        {
            return $"{Street} {Number}, {City}, {State}, {ZipCode}";
        }
    }
}
