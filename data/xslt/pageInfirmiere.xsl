<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
            xmlns:cab="http://www.univ-grenoble-alpes.fr/l3miage/medical"
>
    <xsl:output method="html"/>
    
    <!-- Modules de transformation -->

    <xsl:param name="destinedId">001</xsl:param>
    
    <xsl:template match="/">
        <html>
            <head> </head>
            
            <body>
                <p>Bonjour <xsl:value-of select="./cab:cabinet/cab:infirmiers/cab:infirmier[@id=$destinedId]/cab:prénom/text()"/>,</p>
                <br/>
                <p>Aujourd'hui, vous avez <xsl:value-of select="count(//cab:patient/cab:visite[@intervenant=$destinedId])"/> patients</p>
            </body>
            
        </html>
    </xsl:template>
    
</xsl:stylesheet>