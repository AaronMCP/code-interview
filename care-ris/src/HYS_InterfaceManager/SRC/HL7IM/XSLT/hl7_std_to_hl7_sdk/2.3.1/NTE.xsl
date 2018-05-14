<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="NTE">
  
  <NTE>
     <FIELD.1>
         <FIELD_ITEM><xsl:value-of select="@SetID-NTE"/></FIELD_ITEM>
     </FIELD.1>
     
     <FIELD.2>
         <FIELD_ITEM><xsl:value-of select="@SourceOfComment"/></FIELD_ITEM>
     </FIELD.2>
     
     <FIELD.3>
         <xsl:for-each select="NET/Comment">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.3>
     
     <FIELD.4>
         <xsl:for-each select="NET/Comment">
            <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.4>
  </NTE>
  
  </xsl:template>

</xsl:stylesheet>

