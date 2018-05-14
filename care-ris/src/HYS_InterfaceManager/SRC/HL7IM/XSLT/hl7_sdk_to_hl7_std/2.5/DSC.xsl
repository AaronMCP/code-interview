<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="DSC">
		<DSC>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="ContinuationPointer"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:attribute name="ContinuationStyle"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>					
		</DSC>
	</xsl:template>
</xsl:stylesheet>
