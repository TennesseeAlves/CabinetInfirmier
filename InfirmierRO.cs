namespace CabinetInfirmier;

using System.Text.RegularExpressions;
using System.Xml.Serialization;

[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical/Inf")]
[Serializable]

public class InfirmierRO
{
    [XmlAttribute("id")] public uint Id {
        get
        {
            return Id;
        }
        init
        {
            String pattern = @"^[1-9]*$";
            if (Regex.IsMatch(value.ToString(), pattern)) Id = value;
            else throw new Exception("Un id infimier doit être un entier positif.");
        } 
    }
    
    [XmlElement("nom")] public string Nom { get; init; }
    
    [XmlElement("prénom")] public string Prenom { get; init; }
    
    [XmlElement("photo")] public string Photo { get; init; }
    
    public InfirmierRO() {}

    public InfirmierRO(uint id, string nom, string prenom, string photo)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Photo = photo;
    }
}
