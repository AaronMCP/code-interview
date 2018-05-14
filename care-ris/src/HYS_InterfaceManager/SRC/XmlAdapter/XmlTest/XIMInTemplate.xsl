<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template name ="TIn_ID">
 <xsl:value-of select ="ID"/>
</xsl:template>

<xsl:template name ="TIn_PNM">
 <xsl:value-of select ="LAST"/>^<xsl:value-of select ="FIRST"/>
</xsl:template>

</xsl:stylesheet>
