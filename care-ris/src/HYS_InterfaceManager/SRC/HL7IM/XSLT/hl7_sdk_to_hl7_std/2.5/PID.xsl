<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="PID">
		<PID>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="SetIDPatientID"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:attribute name="DTOfBirth"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.8/FIELD_ITEM != ''">
				<xsl:attribute name="Sex"><xsl:value-of select="FIELD.8/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.12/FIELD_ITEM != ''">
				<xsl:attribute name="CountryCode"><xsl:value-of select="FIELD.12/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.19/FIELD_ITEM != ''">
				<xsl:attribute name="SSNNumber-Patient"><xsl:value-of select="FIELD.19/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.23/FIELD_ITEM != ''">
				<xsl:attribute name="BirthPlace"><xsl:value-of select="FIELD.23/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.24/FIELD_ITEM != ''">
				<xsl:attribute name="MultipleBirthIndicator"><xsl:value-of select="FIELD.24/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.25/FIELD_ITEM != ''">
				<xsl:attribute name="BirthOrder"><xsl:value-of select="FIELD.25/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.29/FIELD_ITEM != ''">
				<xsl:attribute name="PatientDeathDateandTime"><xsl:value-of select="FIELD.29/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.30/FIELD_ITEM != ''">
				<xsl:attribute name="PatientDeathIndicator"><xsl:value-of select="FIELD.30/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.31/FIELD_ITEM != ''">
				<xsl:attribute name="IdentityUnknownIndicator"><xsl:value-of select="FIELD.31/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.33/FIELD_ITEM != ''">
				<xsl:attribute name="LastUpdateDT"><xsl:value-of select="FIELD.33/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			<xsl:if test="FIELD.37/FIELD_ITEM != ''">
				<xsl:attribute name="Strain"><xsl:value-of select="FIELD.37/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
                        <xsl:if test="FIELD.32/FIELD_ITEM != ''">
				<xsl:element name="IdentityReliabilityCode">
					<xsl:value-of select="FIELD.32/FIELD_ITEM"/>
				</xsl:element>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.2/FIELD_ITEM">
					<xsl:element name="PatientID_External">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="PatientID_InternalID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.4/FIELD_ITEM">
					<xsl:element name="AlternatePatientID-PID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.5/FIELD_ITEM">
					<xsl:element name="PatientName">
						<xsl:call-template name="XPN"></xsl:call-template>						
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="MothersMaidenName">
						<xsl:call-template name="XPN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.9/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.9/FIELD_ITEM">
					<xsl:element name="PatientAlias">
						<xsl:call-template name="XPN"></xsl:call-template>
									
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.10/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.10/FIELD_ITEM">
					<xsl:element name="Race">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.11/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.11/FIELD_ITEM">
					<xsl:element name="PatientAddress">
						<xsl:call-template name="XAD"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.13/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.13/FIELD_ITEM">
					<xsl:element name="PhoneNumber-Home">
						<xsl:call-template name="XTN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.14/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.14/FIELD_ITEM">
					<xsl:element name="PhoneNumber-Business">
						<xsl:call-template name="XTN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.15/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.15/FIELD_ITEM">
					<xsl:element name="PrimaryLanguage">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.16/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.16/FIELD_ITEM">
					<xsl:element name="MaritalStatus">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.17/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.17/FIELD_ITEM">
					<xsl:element name="Religion">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.18/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.18/FIELD_ITEM">
					<xsl:element name="PatientAccountNumber">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.20/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.20/FIELD_ITEM">
					<xsl:element name="DriversLicenseNumber-Patient">
						<xsl:call-template name="DLN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.21/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.21/FIELD_ITEM">
					<xsl:element name="MothersIdentifier">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.22/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.22/FIELD_ITEM">
					<xsl:element name="EthnicGroup">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.26/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.26/FIELD_ITEM">
					<xsl:element name="Citizenship">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.27/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.27/FIELD_ITEM">
					<xsl:element name="VeteransMilitaryStatus">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.28/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.28/FIELD_ITEM">
					<xsl:element name="Nationality">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.34/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.34/FIELD_ITEM">
					<xsl:element name="LastUpdateFacility">
						<xsl:call-template name="HD"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.35/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.35/FIELD_ITEM">
					<xsl:element name="SpeciesCode">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.36/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.36/FIELD_ITEM">
					<xsl:element name="BreedCode">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.38/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.38/FIELD_ITEM">
					<xsl:element name="ProductionClassCode">
						<xsl:call-template name="CE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.39/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.39/FIELD_ITEM">
					<xsl:element name="TribalCitizenship">
						<xsl:call-template name="CWE"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</PID>
	</xsl:template>
</xsl:stylesheet>
