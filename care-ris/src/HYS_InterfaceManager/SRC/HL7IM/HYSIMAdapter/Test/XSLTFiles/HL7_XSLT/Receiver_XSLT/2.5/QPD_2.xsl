<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="QPD_2">
		<QPD>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:attribute name="QueryTag"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>		
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.1/FIELD_ITEM">
					<xsl:element name="MessageQueryName">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>	
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="DemographicsFields">
						<xsl:call-template name="QIP"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.8/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.8/FIELD_ITEM">
					<xsl:element name="WhatDomainReturned">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</QPD>
	</xsl:template>
</xsl:stylesheet>
