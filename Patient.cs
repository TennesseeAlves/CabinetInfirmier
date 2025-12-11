using System.Xml.Serialization;
using CabinetInfirmier.Csharp;

namespace CabinetInfirmier;

[XmlRoot("patient", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Patient
{
    [XmlElement("nom")] public string Nom { get; set; }
    [XmlElement("prénom")] public string Prenom { get; set; }
    [XmlElement("sexe")] public string Sexe { get; set; }
    [XmlElement("naissance")] public string Naissance { get; set; }
    [XmlElement("numéro")] public string Numero { get; set; }
    
    [XmlElement("adresse")] public Adresse Adresse { get; set; }
    [XmlElement("visite")] public Visite Visite { get; set; }
    
    public Patient(){}

    public Patient(string nom, string prenom, string sexe, string naissance, string numero, Adresse adresse, Visite visite)
    {
        Nom = nom;
        Prenom = prenom;
        Sexe = sexe;
        Naissance = naissance;
        Numero = numero;
        Adresse = adresse;
        Visite = visite;
    }
    
    public string toString()
    {
        String res = "=> Patient : \nNom: " + Nom + "\nPrenom : " + Prenom + "\nSexe : " + Sexe + "\nDate de naissance : " + Naissance + "\nNuméro de sécurité social : " + Numero + "\nAdresse " + Adresse.toString() + "\nVisite " + Visite.toString();
        return res;
        
    }
    
}