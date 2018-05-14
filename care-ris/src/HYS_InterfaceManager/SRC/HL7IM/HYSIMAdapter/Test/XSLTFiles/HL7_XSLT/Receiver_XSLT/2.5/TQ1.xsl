<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="TQ1">
		<TQ1>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="SetIDTQ1"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:attribute name="StartDT"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.8/FIELD_ITEM != ''">
				<xsl:attribute name="EndDT"><xsl:value-of select="FIELD.8/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.10/FIELD_ITEM != ''">
				<xsl:attribute name="ConditionText"><xsl:value-of select="FIELD.10/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.11/FIELD_ITEM != ''">
				<xsl:attribute name="TextInstruction"><xsl:value-of select="FIELD.11/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.12/FIELD_ITEM != ''">
				<xsl:attribute name="Conjunction"><xsl:value-of select="FIELD.12/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.14/FIELD_ITEM != ''">
				<xsl:attribute name="TotalOccurrences"><xsl:value-of select="FIELD.14/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:for-each select="FIELD.4/FIELD_ITEM">
				<xsl:element name="ExplicitTime">
					<xsl:value-of select="."/>
				</xsl:element>
			</xsl:for-each>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.2/FIELD_ITEM">
					<xsl:element name="Quantity">
						<xsl:call-template name="CQ"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="RepeatPattern">
						<xsl:call-template name="RPT"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.5/FIELD_ITEM">
					<xsl:element name="RelativeTimeAndUnits">
						<xsl:call-template name="CQ"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="ServiceDuration">
						<xsl:call-template name="CQ"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.9/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.9/FIELD_ITEM">
					<xsl:element name="Priority">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.13/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.13/FIELD_ITEM">
					<xsl:element name="OccurrenceDuration">
						<xsl:call-template name="CQ"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</TQ1>
	</xsl:template>
</xsl:stylesheet>
