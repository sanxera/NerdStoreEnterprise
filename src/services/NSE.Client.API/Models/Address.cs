using System;
using NSE.Core.DomainObjects;

namespace NSE.Client.API.Models
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string ZipCod { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid ClientId { get; protected set; }

        public Client Client { get;  protected set; }

        public Address(string street, string number, string complement, string district, string zipCod, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            ZipCod = zipCod;
            City = city;
            State = state;
        }
    }
}