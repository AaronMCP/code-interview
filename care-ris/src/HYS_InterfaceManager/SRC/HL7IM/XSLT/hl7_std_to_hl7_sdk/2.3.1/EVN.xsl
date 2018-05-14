<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="EVN">
  
  <EVN>
     <FIELD.1>
         <FIELD_ITEM><xsl:value-of select="@EventTypeCode"/></FIELD_ITEM>
     </FIELD.1>
     
     <FIELD.2>
         <FIELD_ITEM><xsl:value-of select="@RecordDT"/></FIELD_ITEM>
     </FIELD.2>
     
     <FIELD.3>
         <FIELD_ITEM><xsl:value-of select="@DTPlannedEvent"/></FIELD_ITEM>
     </FIELD.3>
     
     <FIELD.4>
         <FIELD_ITEM><xsl:value-of select="@EventReasonCode"/></FIELD_ITEM>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="OperatorID">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <FIELD_ITEM><xsl:value-of select="@EventOccurred"/></FIELD_ITEM>
     </FIELD.6>
  </EVN>
  
  </xsl:template>

</xsl:stylesheet>

