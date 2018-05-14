<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  <xsl:template name="ZDS">  
  <ZDS>     
    
     <FIELD.1>
         <xsl:for-each select="ReferencePointer">
             <xsl:call-template name="RP"></xsl:call-template>      
       </xsl:for-each>
     </FIELD.1>     
     
  </ZDS>
  
  </xsl:template>

</xsl:stylesheet>

