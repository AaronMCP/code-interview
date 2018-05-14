<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="NTE">

  <NTE>
     <FIELD.1>
         <xsl:if test="count(@SetIDNTE ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetIDNTE"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@SourceofComment ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SourceofComment"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>
     
     <FIELD.3>
         <xsl:for-each select="Comment">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.3>
     
     <FIELD.4>
		 <xsl:for-each select="CommentType">
			 <xsl:call-template name="CE"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.4>     
  </NTE>
  
  </xsl:template>

</xsl:stylesheet>

