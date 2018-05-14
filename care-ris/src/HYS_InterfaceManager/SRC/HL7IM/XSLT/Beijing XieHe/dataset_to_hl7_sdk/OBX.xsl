<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	<xsl:template name="OBX">
		<xsl:if test="csb:REPORT_DIAGNOSE !=''">
		<OBX>
			<FIELD.1>
				<FIELD_ITEM>1</FIELD_ITEM>
			</FIELD.1>
			<FIELD.2><FIELD_ITEM>ST</FIELD_ITEM></FIELD.2>
			<FIELD.3>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:REPORT_REPORTNO"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4/>
				</FIELD_ITEM>
			</FIELD.3>
			<FIELD.4>				
			</FIELD.4>
			<FIELD.5>
				<FIELD_ITEM>
						<xsl:value-of select="csb:REPORT_DIAGNOSE"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.5>
			<FIELD.6/>
			<FIELD.7>
				
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
			<FIELD.14>
				<FIELD_ITEM>
					<xsl:value-of select="csb:REPORT_REPORTDT"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.14>
			<FIELD.15/>
			<FIELD.16>
				<FIELD_ITEM>
					<xsl:value-of select="csb:REPORT_REPORT_WRITER"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.16>
			<FIELD.17/>
			
		</OBX>
		</xsl:if>
		<xsl:if test="csb:REPORT_COMMENTS !=''">
		<OBX>
			<FIELD.1>
			<FIELD_ITEM>
				<xsl:choose>
					<xsl:when test="csb:REPORT_DIAGNOSE !=''"><xsl:text>2</xsl:text></xsl:when>
					<xsl:otherwise><xsl:text>1</xsl:text></xsl:otherwise>
				</xsl:choose>
			</FIELD_ITEM>
			</FIELD.1>
			<FIELD.2><FIELD_ITEM>ST</FIELD_ITEM></FIELD.2>
			<FIELD.3>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:REPORT_REPORTNO"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4/>
				</FIELD_ITEM>
			</FIELD.3>
			<FIELD.4>				
			</FIELD.4>
			<FIELD.5>
				<FIELD_ITEM>
						<xsl:value-of select="csb:REPORT_COMMENTS"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.5>
			<FIELD.6/>
			<FIELD.7>
				
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
			<FIELD.14>
				<FIELD_ITEM>
					<xsl:value-of select="csb:REPORT_REPORTDT"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.14>
			<FIELD.15/>
			<FIELD.16>
				<FIELD_ITEM>
					<xsl:value-of select="csb:REPORT_REPORT_WRITER"></xsl:value-of>
				</FIELD_ITEM>
			</FIELD.16>
			<FIELD.17/>
			
		</OBX>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
