<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="NTE">
		<NTE>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="SetIDNTE"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:attribute name="SourceofComment"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:for-each select="FIELD.3/FIELD_ITEM">
			    <xsl:element name="Comment"><xsl:value-of select="."/></xsl:element>
			</xsl:for-each>
			
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.4/FIELD_ITEM">
			         <xsl:element name="CommentType">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
			 </xsl:if>
		 </NTE>
	 </xsl:template>
</xsl:stylesheet>
