using CabinetInfirmier.Csharp;

namespace CabinetInfirmier;

using System.Xml.Serialization;

[XmlRoot("infirmiers", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class InfirmiersSerealisation
{
    [XmlElement("infirmier")] public List<InfirmierSerealisation> Infirmier { get; set; }

    public InfirmiersSerealisation(){}

    public InfirmiersSerealisation(List<InfirmierSerealisation> infirmier)
    {
        this.Infirmier = infirmier;   
    }
    
    
    public string toString()
    {
        string res = "Liste d'infirmiers du cabinet : \n";
        for (int i = 0; i < Infirmier.Count; i++)
        {
            res += Infirmier[i].toString() + "\n";
        }

        return res;
    }
    
}