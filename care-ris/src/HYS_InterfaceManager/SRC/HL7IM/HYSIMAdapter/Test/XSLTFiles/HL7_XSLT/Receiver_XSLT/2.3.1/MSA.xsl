<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="MSA">
		<MSA>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="AcknowledgmentCode"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:attribute name="MessageControlID"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:attribute name="TextMessage"><xsl:value-of select="FIELD.3/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:attribute name="ExpectedSequenceNumber"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:attribute name="DelayedAcknowledgmentType"><xsl:value-of select="FIELD.5/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="ErrorCondition">
				<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</MSA>
	</xsl:template>
	

</xsl:stylesheet>
