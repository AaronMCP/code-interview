<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="OBR_2">
		<xsl:if test="FIELD.27/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.27/FIELD_ITEM">
				<xsl:element name="QuantityTiming">
					<xsl:call-template name="TQ"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.28/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.28/FIELD_ITEM">
				<xsl:element name="ResultCopiesTo">
					<xsl:call-template name="XCN"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.29/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.29/FIELD_ITEM">
				<xsl:element name="ParentNumber">
					<xsl:call-template name="EIP"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.31/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.31/FIELD_ITEM">
				<xsl:element name="ReasonforStudy">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.32/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.32/FIELD_ITEM">
				<xsl:element name="PrincipalResultInterpreter">
					<xsl:call-template name="NDL"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.33/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.33/FIELD_ITEM">
				<xsl:element name="AssistantResultInterpreter">
					<xsl:call-template name="NDL"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.34/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.34/FIELD_ITEM">
				<xsl:element name="Technician">
					<xsl:call-template name="NDL"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.35/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.35/FIELD_ITEM">
				<xsl:element name="Transcriptionist">
					<xsl:call-template name="NDL"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.38/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.38/FIELD_ITEM">
				<xsl:element name="TransportLogisticsOfCollectedSample">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.39/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.39/FIELD_ITEM">
				<xsl:element name="CollectorsComment">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.40/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.40/FIELD_ITEM">
				<xsl:element name="TransportArrangementResponsibility">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.43/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.43/FIELD_ITEM">
				<xsl:element name="PlannedPatientTransportComment">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.44/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.44/FIELD_ITEM">
				<xsl:element name="ProcedureCode">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.45/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.45/FIELD_ITEM">
				<xsl:element name="ProcedureCodeModifier">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.46/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.46/FIELD_ITEM">
				<xsl:element name="PlacerSupplementalServiceInformation">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.47/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.47/FIELD_ITEM">
				<xsl:element name="FillerSupplementalServiceInformation">
					<xsl:call-template name="CE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="FIELD.48/FIELD_ITEM != ''">
			<xsl:for-each select="FIELD.48/FIELD_ITEM">
				<xsl:element name="MedicallyNecessaryDuplicateProcedureReason">
					<xsl:call-template name="CWE"></xsl:call-template>
				</xsl:element>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
