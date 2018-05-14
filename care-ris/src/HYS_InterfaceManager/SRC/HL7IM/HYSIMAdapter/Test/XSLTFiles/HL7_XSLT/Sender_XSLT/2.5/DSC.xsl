<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="DSC">

  <DSC>
     <FIELD.1>
         <xsl:if test="count(@ContinuationPointer ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ContinuationPointer"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
        <FIELD.2>
         <xsl:if test="count(@ContinuationStyle ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ContinuationStyle"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>
     
  </DSC>
  
  </xsl:template>

</xsl:stylesheet>

