<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="EVN">

  <EVN>
         <xsl:if test="FIELD.1/FIELD_ITEM != ''">
			 <xsl:attribute name="EventTypeCode"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
			 <xsl:attribute name="RecordDT"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.3/FIELD_ITEM != ''">
			 <xsl:attribute name="DTPlannedEvent"><xsl:value-of select="FIELD.3/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.4/FIELD_ITEM != ''">
			 <xsl:attribute name="EventReasonCode"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.6/FIELD_ITEM != ''">
			 <xsl:attribute name="EventOccurred"><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
         <xsl:if test="FIELD.5/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.5/FIELD_ITEM">
		             <xsl:element name="OperatorID">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>		 
  </EVN>
  
  </xsl:template>

</xsl:stylesheet>

