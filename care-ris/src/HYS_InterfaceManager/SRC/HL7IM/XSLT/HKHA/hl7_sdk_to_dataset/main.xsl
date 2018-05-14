<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:template match="/">
		<Message>
			<xsl:copy-of select="/Message/Header"/>
			<!--<xsl:apply-templates select="//Header"/>		-->
			<xsl:apply-templates select="//Body"/>
		</Message>
	</xsl:template>
	<xsl:template match="//Header">
		<xsl:copy/>
	</xsl:template>
	<xsl:template match="//Body">
		<Body>
			<!-- 
    For each type of HL7v2 message, create a xsl template for it.
    Then include the template into this file, and call it in the following choose..when statement.
    
    Note: According to HL7v3 Web Services Profile (http://www.hl7.org/v3ballot/html/infrastructure/transport/transport-wsprofiles.html),
    under the Body element there should be a single element, which is the top-level element of HL7 XML message.
    -->
			<xsl:for-each select="//Body/*">
				<xsl:choose>
					<xsl:when test="name()= 'ORM_O01'">
						<csb:Order>
							<xsl:call-template name="ORM_O01"/>
						</csb:Order>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A01'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A04'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A05'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A08'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A31'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A40'">
						<csb:Patient>
							<xsl:call-template name="ADT_A40"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A47'">
						<csb:Patient>
							<xsl:call-template name="ADT_A40"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ORU_R01'">
						<csb:Report>
							<xsl:call-template name="ORU_R01"/>
						</csb:Report>
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</Body>
	</xsl:template>
	<xsl:template name="ADT_A01">
		<xsl:for-each select="PID">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				<xsl:call-template name="PATIENT"/>
			</csb:table>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="ADT_A40">
		<xsl:for-each select="PID">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				<xsl:call-template name="PATIENT"/>
			</csb:table>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="ORM_O01">
		<xsl:for-each select="OBR">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				
				<xsl:call-template name="PATIENT"/>
				
				<xsl:call-template name="ORDER"/>
			</csb:table>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="ORU_R01">
	<xsl:for-each select="OBR">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				
				<xsl:call-template name="PATIENT"/>
				<xsl:call-template name="ORDER"></xsl:call-template>				
				
				<xsl:call-template name="REPORT"></xsl:call-template>
			</csb:table>
		</xsl:for-each>
</xsl:template>
	<xsl:include href="./DATAINDEX.xsl"/>
	<xsl:include href="./PATIENT.xsl"/>
	<xsl:include href="./ORDER.xsl"/>
	<xsl:include href="./REPORT.xsl"/>
</xsl:stylesheet>
