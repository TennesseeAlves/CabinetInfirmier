<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:pat="http://www.univ-grenoble-alpes.fr/l3miage/patient"
                version="1.0"
>
    <xsl:output method="html" indent="yes"/>
    
    <xsl:template match="/">
        <html>
            <head>
                <title>page patient</title>
                <link href="../css/pagePatient2.css" rel="stylesheet"/>
            </head>
            <body>
                <xsl:apply-templates select="pat:patient"/>
            </body>
        </html>
    </xsl:template>
    
    <!-- Il devrait y avoir un seul match pour cette template (la racine patient) -->
    <xsl:template match="pat:patient">
                <div class="infos">
                    <p><strong>Nom : </strong><xsl:value-of select="pat:nom"/></p>
                    <p><strong>prénom : </strong><xsl:value-of select="pat:prénom"/></p>
                    <p><strong>Sexe : </strong><xsl:value-of select="pat:sexe"/></p>
                    <p><strong>Date de naissance : </strong><xsl:value-of select="pat:naissance"/></p>
                    <p><strong>Numéro de sécurité sociale : </strong><xsl:value-of select="pat:numéroSS"/></p>
                    <p><strong>Adresse : </strong><xsl:apply-templates select="pat:adresse"/></p>
                </div>
                <div class="visites">
                    <xsl:apply-templates select="pat:visite">
                        <xsl:sort select="@date" order="ascending"/>
                    </xsl:apply-templates>
                </div>
    </xsl:template>
    
    <!-- Templage visite -->
    <xsl:template match="pat:visite">
        <div class="visite-box">
            <h3>Visite du <xsl:value-of select="@date"/></h3>
            <div class="intervenant">
                <strong>Intervenant : </strong><xsl:value-of select="pat:intervenant/pat:nom"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="pat:intervenant/pat:prénom"/>
            </div>
            <ul class="actes">
                <xsl:apply-templates select="pat:acte"/>
            </ul>
        </div>
    </xsl:template>
    
    <!-- Template acte -->
    <xsl:template match="pat:acte">
        <li><xsl:value-of select="./text()"/></li>
    </xsl:template>
    
    <!-- Template adresse -->
    <xsl:template match="pat:adresse">
        <span>
            <xsl:if test="pat:étage">
                <xsl:value-of select="pat:étage"/><xsl:text>e étage,</xsl:text>
            </xsl:if>
            <xsl:if test="pat:numéro">
                <xsl:value-of select="pat:numéro"/><xsl:text> </xsl:text>
            </xsl:if>
            <xsl:value-of select="pat:rue"/><xsl:text>, </xsl:text>
            <xsl:value-of select="pat:codePostal"/><xsl:text> </xsl:text>
            <xsl:value-of select="pat:ville"/>
        </span>
    </xsl:template>
    
</xsl:stylesheet>