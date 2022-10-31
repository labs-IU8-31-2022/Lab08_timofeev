using System.Xml.Serialization;
using animal;

var xmlSerializer = new XmlSerializer(typeof(Cow));
using (var file = new FileStream("Cow.xml", FileMode.OpenOrCreate))
{
    var cow = xmlSerializer.Deserialize(file) as Cow;
    Console.WriteLine("deserialized");
    Console.WriteLine($"{cow?.Country} {cow?.HideFromOtherAnimals} {cow?.Name} {cow?.WhatAnimal}");
}
