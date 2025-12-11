using System.Xml.Serialization;

namespace CabinetInfirmier.Csharp;


[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class Adresse
{
    /* Solution trouve sur internet qui, de ce que j'ai compris c'est un pattern... mais ca ne marche pas car l'element est rajoutee dans tout les cas
    [XmlElement("étage")]
    public int EtageValeur
    {
        get { return Etage.Value; }
        set { Etage = value; }
    }

    [XmlIgnore] public int? Etage {get; set;}
    public bool EtageSpecified => Etage.HasValue;
    */

    /*{
        get => _Etage;
        set
        {
            if (value <= 0) _Etage = 0;
            else _Etage = value;
        }
    }*/
    [XmlElement("étage")]
    public int Etage {get; set;}
    
    

    //[XmlIgnore] public int? Numero {get; set;}
    
    [XmlElement("numéro")]
    public int Numero  { get; set; }
    /*{
        get => _Numero;
        set
        {
            if (value <= 0) _Numero = 0;
            else _Numero = value;
        }
    }*/


    [XmlElement("rue")]
    public string Rue { get; set; }
    
    [XmlElement("codePostal")]
    public string CodePostal { get; set; }
    
    [XmlElement("ville")]
    public string Ville { get; set; }
    
    public Adresse() { }

    public Adresse(int etage, int numero, string rue, string codePostal, string ville)
    {
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Etage = etage;
    }

    
}