<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:template match="/">
		<Message>
			<xsl:copy-of select="/Message/Header"/>
			<xsl:apply-templates select="//Body"/>
		</Message>
	</xsl:template>
	<xsl:template match="//Body/node()">
		<Body>
			<!-- 
    For each type of HL7v2 message, create a xsl template for it.
    Then include the template into this file, and call it in the following choose..when statement.
    
    Note: According to HL7v3 Web Services Profile (http://www.hl7.org/v3ballot/html/infrastructure/transport/transport-wsprofiles.html),
    under the Body element there should be a single element, which is the top-level element of HL7 XML message.
    -->
			<xsl:for-each select="csb:Table">
				<xsl:choose>
					<xsl:when test="csb:DATAINDEX_EVENT_TYPE = '00'">
						<xsl:call-template name="ADT_A01"/>
					</xsl:when>
					<xsl:when test="csb:DATAINDEX_EVENT_TYPE = '01'">
						<xsl:call-template name="ADT_A08"/>
					</xsl:when>
					<xsl:when test="csb:DATAINDEX_EVENT_TYPE = '10' or csb:DATAINDEX_EVENT_TYPE = '11' or csb:DATAINDEX_EVENT_TYPE = '13' or csb:DATAINDEX_EVENT_TYPE = '14'">
						<xsl:call-template name="ORM_O01"/>
					</xsl:when>
					<xsl:when test="csb:DATAINDEX_EVENT_TYPE='32' and csb:REPORT_REPORT_STATUS='206'">
						<xsl:call-template name="ORU_R01"/>
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</Body>
	</xsl:template>
	<xsl:template name="ADT_A01">
		<ADT_A01>
			<xsl:call-template name="MSH"/>
			<xsl:call-template name="EVN"/>
			<xsl:call-template name="PID"/>
		</ADT_A01>
	</xsl:template>
	<xsl:template name="ADT_A08">
		<ADT_A08>
			<xsl:call-template name="MSH"/>
			<xsl:call-template name="EVN"/>
			<xsl:call-template name="PID"/>
		</ADT_A08>
	</xsl:template>
	<xsl:template name="ORM_O01">
		<ORM_O01>
			<xsl:call-template name="MSH"/>
			<xsl:call-template name="EVN"/>
			<xsl:call-template name="PID"/>
			<xsl:call-template name="ORC"/>
			<xsl:call-template name="OBR"/>
		</ORM_O01>
	</xsl:template>
	<xsl:template name="ORU_R01">
		<ORU_R01>
			<xsl:call-template name="MSH"/>
			<xsl:call-template name="EVN"/>
			<xsl:call-template name="PID"/>
			<xsl:call-template name="OBR"/>
			<xsl:call-template name="OBX"/>
		</ORU_R01>
	</xsl:template>
	<xsl:include href="./MSH.xsl"/>
	<xsl:include href="./EVN.xsl"/>
	<xsl:include href="./PID.xsl"/>
	<xsl:include href="./ORC.xsl"/>
	<xsl:include href="./OBR.xsl"/>
	<xsl:include href="./OBX.xsl"/>
</xsl:stylesheet>
