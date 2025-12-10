namespace CabinetInfirmier.Csharp;

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
    
    
}