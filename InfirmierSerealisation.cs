using System.Xml.Serialization;

namespace CabinetInfirmier.Csharp;


[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical/Inf")]
[Serializable]

public class InfirmierSerealisation
{
    [XmlAttribute("id")] public uint Id { get; set; }
    
    [XmlElement("nom")] public string Nom { get; set; }
    
    [XmlElement("prénom")] public string Prenom { get; set; }
    
    [XmlElement("photo")] public string Photo { get; set; }
    
    

    public InfirmierSerealisation(){}

    public InfirmierSerealisation(uint id, string nom, string prenom, string photo)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Photo = photo;
    }
}