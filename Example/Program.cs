using System;
using MappingObjectsFramework;

namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DogFromDataBase databaseDog = new DogFromDataBase { id = 2, Address = "14214 SW", Dog_Name = "Gretel" };

            DogDomain domainDog = databaseDog.MapTo<DogDomain>(new MyConverter());

            Console.WriteLine(domainDog.Address.number);
            Console.WriteLine(domainDog.Address.street);
        }
    }

    public class MyConverter : Converter
    {
        public MyConverter():base(typeof(string), typeof(Address))
        {
            
        }
        #region implemented abstract members of Converter
        protected override object ConvertFunction(object value)
        {
            string address = (string)value;

            Address result = new Address { number = address.Split(" "[0])[0], street = address.Split(" "[0])[1] };

            return result;
        }
        #endregion
    }

    public class DogDomain
    {
        [MapPropertyName(Name = "id")]
        public int ID { get; set; }

        [MapPropertyName(Name = "Dog_Name")]
        public string Name{ get; set; }

        [MapPropertyName(Name = "Address")]
        public Address Address { get; set; }
    }

    public class DogFromDataBase
    {
        public int id{ get; set; }

        public string Dog_Name{ get; set; }

        public string Address{ get; set; }
    }

    public class Address
    {
        public string street{ get; set; }

        public string number{ get; set; }
    }
}
