<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="RCP">
		<RCP>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="QueryPriority"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:attribute name="ExcutionandDeliveryTime"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:attribute name="ModifyIndicator"><xsl:value-of select="FIELD.5/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:attribute name="SegmentGroupInclusion"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.2/FIELD_ITEM">
		             <xsl:element name="QuantityLimitedRequest">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="ResponseModality">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>			
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="SortByField">
						<xsl:call-template name="SRT"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>			
		</RCP>
	</xsl:template>

</xsl:stylesheet>
