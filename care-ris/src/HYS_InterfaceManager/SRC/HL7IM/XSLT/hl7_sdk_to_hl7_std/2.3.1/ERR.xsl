<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="ERR">
		<ERR>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:attribute name="InfluenceDegree"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:attribute name="DiagnosisInformation"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.8/FIELD_ITEM != ''">
				<xsl:attribute name="UserMessage"><xsl:value-of select="FIELD.8/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.1/FIELD_ITEM">
					<xsl:element name="ErrorCodeAndPosition">
						<xsl:call-template name="ELD"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.2/FIELD_ITEM">
					<xsl:element name="ErrorPosition">
						<xsl:call-template name="ERL"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="HL7ErrorCode">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.5/FIELD_ITEM">
					<xsl:element name="AppErrorCode">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
				<xsl:element name="AppErrorParam" ><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:element>
				</xsl:for-each>				
			</xsl:if>
			
			<xsl:if test="FIELD.9/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.9/FIELD_ITEM">
				<xsl:element name="NoticeIndextoPerson" ><xsl:value-of select="FIELD.9/FIELD_ITEM"/></xsl:element>
				</xsl:for-each>				
			</xsl:if>
			
			<xsl:if test="FIELD.10/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.10/FIELD_ITEM">
					<xsl:element name="InvalidModel">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			
			<xsl:if test="FIELD.11/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.11/FIELD_ITEM">
					<xsl:element name="InvalidModelCode">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			
			<xsl:if test="FIELD.12/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.12/FIELD_ITEM">
					<xsl:element name="HelpDeskContactPoint">
						<xsl:call-template name="XTN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			
		</ERR>
	</xsl:template>

</xsl:stylesheet>
