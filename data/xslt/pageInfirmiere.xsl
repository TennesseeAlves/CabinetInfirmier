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
        <!-- les variables -->
        <xsl:variable name="prenomInf" select="./cab:cabinet/cab:infirmiers/cab:infirmier[@id=$destinedId]/cab:prénom/text()"/>
        <xsl:variable name="patients" select="//cab:patient[cab:visite[@intervenant=$destinedId]]"/>
        <!-- -->
        <html>
            <head>
                <title>page infirmiere</title>
            </head>
            
            <body>
                <div>
                    <p>Bonjour <xsl:value-of select="$prenomInf"/>,</p>
                    <p>Aujourd'hui, vous avez <xsl:value-of select="count($patients)"/> patients</p>
                </div>
                <!-- En suite on affichera la liste des patients et des soins -->
                <div>
                    <h1>Voici la liste des patients à visiter :</h1>
                    <xsl:apply-templates select="$patients"/>
                </div>
            </body>
            
        </html>
    </xsl:template>
    
    <!-- Template patient -->
    <xsl:template match="cab:patient">
        <xsl:variable name="patient" select="//cab:patient[cab:visite[@intervenant=$destinedId]]"/>
        <ul>
            <li>Nom : <xsl:value-of select="cab:nom/text()"/></li>
            <li>Prénom : <xsl:value-of select="cab:prénom/text()"/></li>
            <li>Sexe : <xsl:value-of select="cab:sexe/text()"/></li>
            <li>Date de naissace : <xsl:value-of select="cab:naissance/text()"/></li>
            <li>NIR : <xsl:value-of select="cab:numéro/text()"/></li>
            <li>Adresse : <xsl:apply-templates select="cab:adresse"/></li>
        </ul>
        <h3>Liste des soins à effectuer pour ce patient :</h3>
        <xsl:apply-templates select="cab:visite">
            <xsl:sort select="@date" order="ascending"/>
        </xsl:apply-templates>
        <hr/>
    </xsl:template>
    
    <!-- Template visites d'un patient -->
    <xsl:template match="//cab:patient/cab:visite">
        <h4><xsl:value-of select="@date"/></h4>
        <ul>
            <xsl:apply-templates select="cab:acte"/>
        </ul>
    </xsl:template>
    
    <!-- Template actes d'un patient -->
    <xsl:template match="//cab:patient//cab:acte">
        <xsl:variable name="idActe" select="@id"/>
        <li><xsl:value-of select="$actes/act:actes/act:acte[@id=$idActe]/text()"/></li>
    </xsl:template>
    
    <!-- Template pour l'adresse -->
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