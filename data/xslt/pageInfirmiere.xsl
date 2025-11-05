<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
            xmlns:cab="http://www.univ-grenoble-alpes.fr/l3miage/medical"
            xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
>
    <xsl:output method="html"/>
    
    <!-- Modules de transformation -->

    <!-- param globale avec l'id de l'infirmierer -->
    <xsl:param name="destinedId">001</xsl:param>
    
    <!-- variable globale contenant les noeuds ngap du document actes.xml -->
    <xsl:variable name="actes" select="document('../xml/actes.xml', /)/act:ngap"/>
    
    
    <xsl:template match="/">
        <xsl:variable name="prenomInf" select="./cab:cabinet/cab:infirmiers/cab:infirmier[@id=$destinedId]/cab:prénom/text()"/>
        <xsl:variable name="patients" select="//cab:patient[cab:visite[@intervenant=$destinedId]]"/>
        <html>
            <head>
                <title>page infirmiere</title>
                <link rel="stylesheet" href="../css/pageInfirmiere.css"/>
            </head>
            
            <body>
                <div>
                    <p>Bonjour <xsl:value-of select="$prenomInf"/>,</p>
                    <p>Aujourd'hui, vous avez <xsl:value-of select="count($patients)"/> patients</p>
                </div>
                <!-- Liste des patients et des soins -->
                <div>
                    <h1>Voici la liste des patients à visiter :</h1>
                    <xsl:apply-templates select="$patients"/>
                </div>
            </body>
            
        </html>
    </xsl:template>
    
    <!-- Template patient -->
    <xsl:template match="cab:patient">
        <xsl:variable name="nom" select="cab:nom/text()"/>
        <xsl:variable name="prenom" select="cab:prénom/text()"/>
        <div class="patient">
            <p><b>Nom : </b><xsl:value-of select="$nom"/></p>
            <p><b>Prénom : </b><xsl:value-of select="$prenom"/></p>
            <p><b>Sexe : </b><xsl:value-of select="cab:sexe/text()"/></p>
            <p><b>Date de naissace : </b><xsl:value-of select="cab:naissance/text()"/></p>
            <p><b>NIR : </b><xsl:value-of select="cab:numéro/text()"/></p>
            <p><b>Adresse : </b><xsl:apply-templates select="cab:adresse"/></p>
            
            <!-- Liste des soins -->
            <h4><u>Liste des soins à effectuer pour ce patient</u> :</h4>
            <xsl:apply-templates select="cab:visite">
                <xsl:sort select="@date" order="ascending"/>
            </xsl:apply-templates>
            
            <!-- Bouton facture -->
            <h4><u>Facture</u> :</h4>
            <script type="text/javascript">
                function openFacture(prenom, nom, actes) {
                    var width = 500;
                    var height = 300;
                    if (window.innerWidth) {
                        var left = (window.innerWidth-width)/2;
                        var top = (window.innerHeight-height)/2;
                    } else {
                        var left = (document.body.clientWidth-width)/2;
                        var top = (document.body.clientHeight-height)/2;
                    }
                    var factureWindow = window.open('','facture','menubar=yes, scrollbars = yes, top='+top+', left='+left+', width='+width+', height='+height +'');
                    factureText = "Facture pour : " + prenom + " " + nom;
                    factureWindow.document.write(factureText)
                }
            </script>
            <xsl:element name="button">
                <xsl:attribute name="onclick">
                    openFacture('<xsl:value-of select="$prenom"/>',
                    '<xsl:value-of select="$nom"/>',
                    '<xsl:value-of select="cab:visite/cab:acte"/>')
                </xsl:attribute>
                Facture
            </xsl:element>
            
        </div>
    </xsl:template>
    
    <!-- Template visites -->
    <xsl:template match="//cab:patient/cab:visite">
        <h4><xsl:value-of select="@date"/></h4>
        <ul>
            <xsl:apply-templates select="cab:acte"/>
        </ul>
    </xsl:template>
    
    <!-- Template acte -->
    <xsl:template match="//cab:patient//cab:acte">
        <xsl:variable name="idActe" select="@id"/>
        <li><xsl:value-of select="$actes/act:actes/act:acte[@id=$idActe]/text()"/></li>
    </xsl:template>
    
    <!-- Template adresse -->
    <xsl:template match="cab:adresse">
        <span>
            <!-- on vérifire si l'adresse à un étage -->
            <xsl:if test="cab:étage">
                <xsl:value-of select="cab:étage/text()"/>
                <xsl:text>è étage, </xsl:text>
            </xsl:if>
            <!-- on vérifire si l'adresse à un numéro de rue -->
            <xsl:if test="cab:numéro">
                <xsl:value-of select="cab:numéro/text()"/>
                <xsl:text> </xsl:text>
            </xsl:if>
            <xsl:value-of select="cab:rue/text()"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="cab:codePostal/text()"/>
            <xsl:text> </xsl:text>
            <xsl:value-of select="cab:ville/text()"/>
        </span>
    </xsl:template>
    
    <!-- Template pour les nœuds text (pour redefinir le comportement par défaut) -->
    <xsl:template match="text()"/>

    <!-- Template pour les nœuds commentaires (pour redefinir le comportement par défaut) -->
    <xsl:template match="comment()"/>
    
</xsl:stylesheet>