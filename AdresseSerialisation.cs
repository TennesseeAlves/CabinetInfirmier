namespace CabinetInfirmier;

using System.Xml.Serialization;



[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class AdresseSerialisation
{
    /* Solution trouvée sur internet qui est un pattern... mais ca ne marche pas
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
    [XmlElement("étage", IsNullable = true)]
    public int? _Etage
    {
        get { return _Etage; }
        set
        {
            
        }
        
    }
    
    

    //[XmlIgnore] public int? Numero {get; set;}
    [XmlElement("numéro")]
    public int? _Numero  { get; set; }
    /*{
        get => _Numero;
        set
        {
            if (value <= 0) _Numero = 0;
            else _Numero = value;
        }
    }*/


    [XmlElement("rue")]
    public string _Rue { get; set; }
    
    [XmlElement("codePostal")]
    public string _CodePostal { get; set; }
    
    [XmlElement("ville")]
    public string _Ville { get; set; }
    
    public AdresseSerialisation() { }

    public AdresseSerialisation(int? etage, int? numero, string rue, string codePostal, string ville)
    {
        _Numero = numero;
        _Rue = rue;
        _CodePostal = codePostal;
        _Ville = ville;
        _Etage = etage;
    }

    
}