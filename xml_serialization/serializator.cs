using System.Xml.Serialization;
using animal;

Animal ex = new Cow("Russia", true, "Burenka", eClassificationAnimal.Herbivores);

var xmlSerializer = new XmlSerializer(typeof(Cow));
using (var file = new FileStream("Cow.xml", FileMode.OpenOrCreate))
{
    xmlSerializer.Serialize(file, ex);
    Console.WriteLine("serialized");
}