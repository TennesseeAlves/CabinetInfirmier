using System.Collections.Generic;

using System.Xml;

namespace CabinetInfirmier.Csharp;

public class Cabinet
{
    //Partie XmlReader
    public static void AnalyseGlobale(string filepath)
    {
        XmlReader reader = XmlReader.Create(filepath);
    
        Console.WriteLine("On entre dans le document");
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    Console.WriteLine("-> Element '{0}' trouvé", reader.Name);
                    int attributCount =  reader.AttributeCount; 
                    Console.WriteLine("     L'element '{0}' a {1} attribut",reader.Name, attributCount);
                    break;
                
                case XmlNodeType.EndElement:
                    Console.WriteLine("-> On sort de l'element '{0}' ", reader.Name);
                    break;
                
                case XmlNodeType.Text:
                    Console.WriteLine("-> Texte '{0}' trouvé ", reader.Value);
                    break;
                
                case XmlNodeType.Attribute:
                    Console.WriteLine("-> Attribut '{0}' de valeur '{1}' trouvé", reader.Name, reader.Value);
                    break;
            }
            
            
        }
        
    }

    public static List<string> AnalyseNomsInfirmiers(string filepath)
    {
        
        XmlReader reader = XmlReader.Create(filepath);
        List<string> nomsInfirmiers = new List<string>();
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name == "infirmier")
                    {
                        Console.WriteLine("-> Element 'infirmier' {0}", reader.Name);
                        reader.Read();
                        reader.MoveToContent();
                        Console.WriteLine("-> Element suivant '{0}'", reader.Name);
                        if (reader.Name == "nom") 
                        {
                             reader.Read();
                             if (reader.NodeType == XmlNodeType.Text)
                             {
                                  Console.WriteLine("-> Nom de l'infirmier : '{0}'", reader.Value);
                                  nomsInfirmiers.Add(reader.Value);
                             }
                        }

                    }
                    break;
                /*case XmlNodeType.EndElement:
                    if (reader.Name == "infirmier")
                    {
                        Console.WriteLine("C'est fini");
                    }
                    break;*/
                
            }
        }
        Console.WriteLine(nomsInfirmiers.Count);
        return nomsInfirmiers;
        
    }
    
    public static List<string> AnalyseNoms(string filepath, string recherche)
    {
        
        XmlReader reader = XmlReader.Create(filepath);
        List<string> nomsCabinet = new List<string>();
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name == recherche)
                    {
                        //Console.WriteLine("-> Element '{0}' trouvé ", reader.Name);
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text){
                            Console.WriteLine("-> Nom detecté : '{0}'", reader.Value);
                                nomsCabinet.Add(reader.Value);
                        }

                    }
                    break;
                /*case XmlNodeType.EndElement:
                    if (reader.Name == "nom")
                    {
                        Console.WriteLine("C'est fini");
                    }
                    break;*/
            }
        }
        Console.WriteLine(nomsCabinet.Count);
        return nomsCabinet;
    }

    public static int countActes(string filepath) //TODO : faire un hashset : compter le nombre d'actes different
    {
        XmlReader reader = XmlReader.Create(filepath);
        int actes = 0;
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name == "acte" && reader.HasAttributes)
                    {
                        //Console.WriteLine("-> Element 'acte' {0}", reader.Name);
                        
                        reader.MoveToFirstAttribute();
                        Console.WriteLine("Acte -> {0}", reader.Value);
                        actes++;
                    }
                    break;
            }
            {
                
            }
        }
        Console.WriteLine(actes);
        return actes;
    }
    
}