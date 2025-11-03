<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:met="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                version="1.0">
    <xsl:output method="html"/>

    <xsl:param name="destinedId">001</xsl:param>
    <xsl:variable name="actes" select="document('xml/actes.xml')/act:ngap"/>

    <!-- Template principal se charge de generer la page HTML -->
    <xsl:template match="/">
        <html>
            <head>
                <script src="../factures.js"> </script>
                <title>Page infirmiere</title>
            </head>
            <body>
                <h1>Page infirmière</h1>
                <p>Bonjour <xsl:value-of select="//met:infirmier[@id=$destinedId]/met:prénom"/></p>
                <p>Aujourd'hui, vous avez <xsl:value-of select="count(//met:visite[@intervenant=$destinedId])"/> patients</p>

                <!-- ici on renvoie le traitement de chaque patient dans un template dédié -->
                <xsl:apply-templates select="//met:visite[@intervenant=$destinedId]"/>

            </body>
        </html>
    </xsl:template>

    <!-- Template dédié au patient -->
    <xsl:template match="met:visite[@intervenant=$destinedId]">
        <ul>
            <li>Nom : <xsl:value-of select="../met:nom"/></li>
            <li>Adresse : <xsl:value-of select="../met:adresse"/></li>
            <li>Listes des soins à effectuer : </li>
            <ol>

                <!-- ici on renvoie le traitement de chaque acte(soins) des patients dans un template dédié -->
                <xsl:apply-templates select="./met:acte"/>

            </ol>
            <!-- implémentation du bouton factures -->
            <xsl:element name="button">
                <xsl:text>Factures</xsl:text>
                <xsl:attribute name="onclick">
                    afficherFacture('<xsl:value-of select="../met:prénom"/>',
                    '<xsl:value-of select="../met:nom"/>',
                    '<xsl:value-of select="./met:acte"/>')
                </xsl:attribute>
            </xsl:element>
            <!-- <button type="button"/> -->

        </ul>
    </xsl:template>

    <!-- Template dédié au soins -->
    <xsl:template match="met:acte">
        <xsl:variable name="id_acte" select="./@id"/>
        <li><xsl:value-of select="$actes/act:actes/act:acte[@id=$id_acte]"/> </li>
    </xsl:template>
</xsl:stylesheet>