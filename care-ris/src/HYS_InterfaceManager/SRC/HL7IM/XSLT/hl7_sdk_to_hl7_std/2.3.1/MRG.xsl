<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="MRG">
		<MRG>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.1/FIELD_ITEM">
					<xsl:element name="PriorPatientIdentifierList">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.2/FIELD_ITEM">
					<xsl:element name="PriorAlternativePatientID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="PriorPatientAccountNumber">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.4/FIELD_ITEM">
					<xsl:element name="PriorPatientID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.5/FIELD_ITEM">
					<xsl:element name="PriorVisitNumber">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="PriorAlternativeVisitID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>			
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.7/FIELD_ITEM">
					<xsl:element name="PatientName">
						<xsl:call-template name="XPN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</MRG>
	</xsl:template>

</xsl:stylesheet>
