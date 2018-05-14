<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="MSA">
  
  <MSA>
     <FIELD.1><FIELD_ITEM><xsl:value-of select="@AcknowledgmentCode"/></FIELD_ITEM></FIELD.1>
     
     <FIELD.2><FIELD_ITEM><xsl:value-of select="@MessageControlID"/></FIELD_ITEM></FIELD.2>
     
     <FIELD.3><FIELD_ITEM><xsl:value-of select="@TextMessage"/></FIELD_ITEM></FIELD.3>
     
     <FIELD.4><FIELD_ITEM><xsl:value-of select="@ExpectedSequenceNumber"/></FIELD_ITEM></FIELD.4>
     
     <FIELD.5><FIELD_ITEM><xsl:value-of select="@DelayedAcknowledgmentType"/></FIELD_ITEM></FIELD.5>     
     
     <FIELD.6>
         <xsl:for-each select="ErrorCondition">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.6>     
     
  </MSA>
  
  </xsl:template>

</xsl:stylesheet>

