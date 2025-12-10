namespace CabinetInfirmier;

using System.Text.RegularExpressions;
using System.Xml.Serialization;

[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical/Inf")]
[Serializable]

public class InfirmierSerialisation
{
    String patternEntierPositif = @"^[1-9]*$";
    [XmlAttribute("id")] public uint Id {
        get
        {
            return Id;
        }
        set
        {
            if (Regex.IsMatch(value.ToString(), patternEntierPositif)) Id = value;
            else throw new Exception("Un id infimier doit être un entier positif.");
        } 
    }
    
    [XmlElement("nom")] public string Nom { get; set; }
    
    [XmlElement("prénom")] public string Prenom { get; set; }
    
    [XmlElement("photo")] public string Photo { get; set; }
    
    public InfirmierSerialisation(){}

    public InfirmierSerialisation(uint id, string nom, string prenom, string photo)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Photo = photo;
    }
}