<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	<xsl:template name="ORC">
		<ORC>
			<FIELD.1>
				<FIELD_ITEM>
					<xsl:choose>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='10'">
							<xsl:text>NW</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='11'">
							<xsl:text>SC</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='12'">
							<xsl:text>SC</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='13'">
							<xsl:text>CA</xsl:text>
						</xsl:when>
					</xsl:choose>
				</FIELD_ITEM>
			</FIELD.1>
			<FIELD.2/>
			<FIELD.3>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:ORDER_PLACER_NO"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4/>
				</FIELD_ITEM>
			</FIELD.3>
			<FIELD.4>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:ORDER_FILLER_NO"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4/>
				</FIELD_ITEM>
			</FIELD.4>
			<FIELD.5>
				<FIELD_ITEM>
					<xsl:choose>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='10'">
							<xsl:text>SC</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='11'">
							<xsl:text>SC</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='12'">
							<xsl:text>SC</xsl:text>
						</xsl:when>
						<xsl:when test="csb:DATAINDEX_EVENT_TYPE='13'">
							<xsl:text>CA</xsl:text>
						</xsl:when>
					</xsl:choose>
				</FIELD_ITEM>
			</FIELD.5>
			<FIELD.6/>
			<FIELD.7>
				<FIELD_ITEM>
					<COMPONENT.1>						
					</COMPONENT.1>
					<COMPONENT.2>						
					</COMPONENT.2>
					<COMPONENT.3>					
					</COMPONENT.3>
					<COMPONENT.4>
						<xsl:value-of select="csb:ORDER_SCHEDULED_DT"/>
					</COMPONENT.4>
				</FIELD_ITEM>
			</FIELD.7>
			<FIELD.8>				
			</FIELD.8>
			<FIELD.9/>
			<FIELD.10>				
			</FIELD.10>
			<FIELD.11>				
			</FIELD.11>
			<FIELD.12>				
			</FIELD.12>
			<FIELD.13/>
			<FIELD.14/>
			<FIELD.15/>
			<FIELD.16/>
			<FIELD.17/>
			<FIELD.18>				
			</FIELD.18>
			<FIELD.19/>
			<FIELD.20/>
		</ORC>
		
	</xsl:template>
</xsl:stylesheet>
