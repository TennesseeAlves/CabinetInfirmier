using System.Xml.Serialization;

namespace CabinetInfirmier;


[XmlRoot("acte", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class Acte
{   
    [XmlAttribute("id")] public string Id { get; set; }
    
    public Acte(){}

    public Acte(string id)
    {
        Id = id;
    }

    public string toString()
    {
        string res = "Acte id = " +  Id + "\n";
        return res;
    }
}