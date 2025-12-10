<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:met="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                version="1.0">
    <xsl:output method="xml" indent="yes"/>

    <xsl:param name="destinedName">Alécole</xsl:param>
    <xsl:variable name="actes" select="document('xml/actes.xml')/act:ngap"/>

    <!-- Template principal se charge de generer le fichier xml -->
    <xsl:template match="/">
        <patient>
            <nom>
                <xsl:value-of select="//met:patient[met:nom=$destinedName]/met:nom"/>
            </nom>
            <prénom>
                <xsl:value-of select="//met:patient[met:nom=$destinedName]/met:prénom"/>
            </prénom>
            <sexe>
                <xsl:value-of select="//met:patient[met:nom=$destinedName]/met:sexe"/>
            </sexe>
            <naissance>
                <xsl:value-of select="//met:patient[met:nom=$destinedName]/met:naissance"/>
            </naissance>
            <numéroSS>
                <xsl:value-of select="//met:patient[met:nom=$destinedName]/met:numéro"/>
            </numéroSS>
            <adresse>
                <rue>
                    <xsl:value-of select="//met:patient[met:nom=$destinedName]//met:rue"/>
                </rue>
                <codePostal>
                    <xsl:value-of select="//met:patient[met:nom=$destinedName]//met:codePostal"/>
                </codePostal>
                <ville>
                    <xsl:value-of select="//met:patient[met:nom=$destinedName]//met:ville"/>
                </ville>
            </adresse>

            <!-- ici on renvoie le traitement des visites médical des patients dans un template dédié -->
            <xsl:apply-templates select="//met:patient[met:nom=$destinedName]/met:visite"/>
            
        </patient>
    </xsl:template>

    <!-- Template dédié aux vistes -->
    <xsl:template match="//met:patient[met:nom=$destinedName]/met:visite">
        <visite>
            <xsl:attribute name="date">
                <xsl:value-of select="@date"/>
            </xsl:attribute>
            <intervenant>
                <xsl:variable name="infirId" select="@intervenant"/>
                <nom>
                    <xsl:value-of select="//met:infirmier[@id=$infirId]/met:nom"/>
                </nom>
                <prénom>
                    <xsl:value-of select="//met:infirmier[@id=$infirId]/met:prénom"/>
                </prénom>
            </intervenant>

            <!-- ici on renvoie le traitement des soins du patients dans un template dédié -->
            <xsl:apply-templates select="met:acte"/>
            
        </visite>
    </xsl:template>
    
    <!-- Template dédié aux soins -->
    <xsl:template match="met:acte">
        
        <xsl:variable name="acteId" select="@id"/>
        <acte>
            <xsl:value-of select="$actes//act:acte[@id=$acteId]"/></acte>
        
    </xsl:template>

</xsl:stylesheet>