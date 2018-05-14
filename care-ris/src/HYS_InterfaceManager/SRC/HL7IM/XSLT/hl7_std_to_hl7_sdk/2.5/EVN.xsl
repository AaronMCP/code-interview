<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="EVN">

  <EVN>
     <FIELD.1>
         <xsl:if test="count(@EventTypeCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EventTypeCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@RecordDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@RecordDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>
     
     <FIELD.3>
         <xsl:if test="count(@DTPlannedEvent ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DTPlannedEvent"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.3>
     
     <FIELD.4>
         <xsl:if test="count(@EventReasonCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EventReasonCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="OperatorID">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:if test="count(@EventOccurred ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EventOccurred"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.6>
     
     <FIELD.7>
		 <xsl:for-each select="EventFacility">
			 <xsl:call-template name="HD"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.7>
     
  </EVN>
  
  </xsl:template>

</xsl:stylesheet>

