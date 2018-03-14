MapingObjectsFramework
======================

What if you want to copy all or some of the properties from one object to another object? What if you want to convert from your Data Layer to your Domain Layer? Using MappingObjectsFramework that task is very easy, just mark the properties that you want to transfer and it will copy them for you, also if you want to use a specific function to map properties from a data type to another completed different data type, you only need to tell to the framework how this conversion is done and vuala!

An example of data conversion could be the following 

```

using System;
using MappingObjectsFramework;

namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DogFromDataBase databaseDog = new DogFromDataBase { id = 2, Address = "14214 SW", Dog_Name = "Gretel" };

            //because an string can not be converter to an Address by default, we need to specify how the conversion is done.
            //The conversion is done into MyConverter
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

```
