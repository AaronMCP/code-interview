<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template name ="TOut_ID">
 <ID><xsl:value-of select ="current()"/></ID>
</xsl:template>

<xsl:template name ="TOut_PNM">
 <LAST><xsl:value-of select ="substring-after(current(),'^')"/></LAST>
 <FIRST><xsl:value-of select ="substring-before(current(),'^')"/></FIRST>
</xsl:template>

</xsl:stylesheet>
