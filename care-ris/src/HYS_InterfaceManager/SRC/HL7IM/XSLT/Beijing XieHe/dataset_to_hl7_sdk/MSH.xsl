<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	<xsl:template name="MSH">
		<MSH>
			<FIELD.1>|</FIELD.1>
			<FIELD.2>^~\&amp;</FIELD.2>
			<FIELD.3>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:DataIndex_DataSource"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.3>
			<FIELD.4>
				<FIELD_ITEM>
					<COMPONENT.1/>
				</FIELD_ITEM>
			</FIELD.4>
			<FIELD.5>
				<FIELD_ITEM>
					<COMPONENT.1>CS Broker</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.5>
			<FIELD.6>
				<FIELD_ITEM>
					<COMPONENT.1>CSH</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.6>
			<FIELD.7/>
			<FIELD.8/>
			<FIELD.9>
				<FIELD_ITEM>
					<xsl:choose>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='00'">
							<COMPONENT.1>ADT</COMPONENT.1>
							<COMPONENT.2>A01</COMPONENT.2>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='01'">
							<COMPONENT.1>ADT</COMPONENT.1>
							<COMPONENT.2>A08</COMPONENT.2>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='10' or csb:DATAINDEX_EVENT_TYPE='11' or csb:DATAINDEX_EVENT_TYPE='13' or csb:DATAINDEX_EVENT_TYPE='14'">
							<COMPONENT.1>ORM</COMPONENT.1>
							<COMPONENT.2>O01</COMPONENT.2>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='32' and csb:REPORT_REPORT_STATUS='206'">
							<COMPONENT.1>ORU</COMPONENT.1>
							<COMPONENT.2>R01</COMPONENT.2>
						</xsl:when>
					</xsl:choose>
				</FIELD_ITEM>
			</FIELD.9>
			<FIELD.10>
				<FIELD_ITEM>
					<COMPONENT.1><xsl:value-of select="csb:DATAINDEX_DATAID"></xsl:value-of></COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.10>
			<FIELD.11>
				<FIELD_ITEM>
					<COMPONENT.1>P</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.11>
			<FIELD.12>
				<FIELD_ITEM>
					<COMPONENT.1>2.3.1</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.12>
			<FIELD.13/>
			<FIELD.14/>
			<FIELD.15/>
			<FIELD.16/>
			<FIELD.17/>
			<FIELD.18/>
			<FIELD.19/>
			<FIELD.20/>
		</MSH>
	</xsl:template>
</xsl:stylesheet>
