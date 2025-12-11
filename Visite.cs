using System.Xml.Serialization;

namespace CabinetInfirmier;


[XmlRoot("visite", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Visite
{
    [XmlAttribute("intervenant")] public string Intervenant { get; set; }
    
    [XmlAttribute("date")] public string Date { get; set; }
    
    [XmlElement("acte")] public List<Acte> Acte{ get; set; }
    
    public Visite(){}

    public Visite(string intervenant, string date, List<Acte> acte)
    {
        Intervenant = intervenant;
        Date = date;
        Acte = acte;
        
    }

    public string toString()
    {
        string res = "Visite : \nIntervenant : " + Intervenant + "\nDate : " + Date + "\nActe : \n";
        foreach (Acte acte in Acte)
        {
            res += acte.toString() + "\n";
        }
        return res;
        

    }
}