<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	
	<xsl:template match="OBX">
		<xsl:param name="ReportText" select="$ReportTex"/>
		<xsl:choose>
			<xsl:when test="count(following-sibling::OBX)>0">
				<xsl:apply-templates select="following-sibling::OBX">
					<xsl:with-param name="ReportText" select="concat($ReportText,' ',FIELD.5/FIELD_ITEM)"/>
				</xsl:apply-templates>
			</xsl:when>
			<xsl:otherwise><xsl:value-of select="concat($ReportText,'&#x0D;&#x0A;',FIELD.5/FIELD_ITEM)"></xsl:value-of></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REPORT">
		<xsl:param name="ReportText">
		<xsl:apply-templates select="../OBX[1]">
			<xsl:with-param name="ReportText"/>
		</xsl:apply-templates>
		</xsl:param>		


		<!--Report Table-->
		<xsl:element name="csb:REPORT_ACCESSION_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_COMMENTS">
			<xsl:value-of select="$ReportText"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_1">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_2">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_3">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_4">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_DIAGNOSE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_MODALITY">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_OBSERVATIONMETHOD">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_PATIENT_ID">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_APPROVER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_FILE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_INTEPRETER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_NO">
			<xsl:value-of select="../OBX/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_TYPE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_WRITER">
			<xsl:value-of select="../OBX/FIELD.16/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORTDT">
			<xsl:value-of select="../OBX/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>

	</xsl:template>
</xsl:stylesheet>
