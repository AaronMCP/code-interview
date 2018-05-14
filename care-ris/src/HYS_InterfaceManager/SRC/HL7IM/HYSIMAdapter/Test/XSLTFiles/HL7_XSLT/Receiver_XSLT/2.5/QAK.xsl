<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="QAK">
		<QAK>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="QueryTag"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:attribute name="QueryResponseStatus"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:attribute name="HitCount"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:attribute name="thisPayload"><xsl:value-of select="FIELD.5/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:attribute name="HitsRemaining"><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="MessageQueryName">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			
		</QAK>
	</xsl:template>
</xsl:stylesheet>
