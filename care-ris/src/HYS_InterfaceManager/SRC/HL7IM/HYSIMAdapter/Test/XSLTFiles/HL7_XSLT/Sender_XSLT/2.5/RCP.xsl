<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="RCP">

  <RCP>
     <FIELD.1>
         <xsl:if test="count(@QueryPriority ) > 0">
             <FIELD_ITEM><xsl:value-of select="@QueryPriority"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:for-each select="QuantityLimitedRequest">
             <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>
     
     <FIELD.3>
             <xsl:for-each select="ResponseModality">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.3>
        
        <FIELD.4>
         <xsl:if test="count(@ExcutionandDeliveryTime ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ExcutionandDeliveryTime"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:if test="count(@ModifyIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ModifyIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.5>
     
     
        <FIELD.6>
             <xsl:for-each select="SortByField">
                 <xsl:call-template name="SRT"></xsl:call-template>
             </xsl:for-each>
         </FIELD.6>
         
         <FIELD.7>
         <xsl:if test="count(@SegmentGroupInclusion ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SegmentGroupInclusion"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
     
  </RCP>
  
  </xsl:template>

</xsl:stylesheet>

