<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	<xsl:template name="PID">
		<PID>
			<FIELD.1/>
			<FIELD.2/>
			<FIELD.3>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_PATIENTID"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4>ADT1</COMPONENT.4>
				</FIELD_ITEM>
			</FIELD.3>
			<FIELD.4/>
			<FIELD.5>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_PATIENT_NAME"/>
					</COMPONENT.1>
					<COMPONENT.2>Monday</COMPONENT.2>
				</FIELD_ITEM>
			</FIELD.5>
			<FIELD.6/>
			<FIELD.7>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_BIRTHDATE"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.7>
			<FIELD.8>
				<FIELD_ITEM>
					<COMPONENT.1>><xsl:value-of select="csb:PATIENT_SEX"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.8>
			<FIELD.9/>
			<FIELD.10>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_RACE"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.10>
			<FIELD.11>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_ADDRESS"/>
					</COMPONENT.1>
					<COMPONENT.2/>
					<COMPONENT.3/>
					<COMPONENT.4/>
					<COMPONENT.5/>
				</FIELD_ITEM>
			</FIELD.11>
			<FIELD.12>
				<FIELD_ITEM>
					<xsl:value-of select="csb:PATIENT_COUNTRY_CODE"/>
				</FIELD_ITEM>
			</FIELD.12>
			<FIELD.13/>
			<FIELD.14/>
			<FIELD.15/>
			<FIELD.16/>
			<FIELD.17/>
			<FIELD.18>
				<FIELD_ITEM>
					<COMPONENT.1>><xsl:value-of select="csb:PATIENT_ACCOUNT_NUMBER"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.18>
			<FIELD.19/>
			<FIELD.20/>
			<FIELD.21/>
			<FIELD.22/>
			<FIELD.23/>
			<FIELD.24/>
			<FIELD.25/>
			<FIELD.26/>
			<FIELD.27/>
			<FIELD.28/>
			<FIELD.29/>
			<FIELD.30/>
			<FIELD.31/>
			<FIELD.32/>
			<FIELD.33/>
			<FIELD.34/>
			<FIELD.35/>
			<FIELD.36/>
			<FIELD.37/>
			<FIELD.38/>
			<FIELD.39/>
		</PID>
		<PV1>
			<FIELD.1/>
			<FIELD.2>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:PATIENT_PATIENT_TYPE"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.2>
			<FIELD.3>				
			</FIELD.3>
			<FIELD.4/>
			<FIELD.5/>
			<FIELD.6/>
			<FIELD.7/>
			<FIELD.8>
				<FIELD_ITEM>
					<COMPONENT.1>
						<xsl:value-of select="csb:ORDER_REF_PHYSICIAN"/>
					</COMPONENT.1>
				</FIELD_ITEM>
			</FIELD.8>
			<FIELD.9/>
			<FIELD.10>				
			</FIELD.10>
			<FIELD.11/>
			<FIELD.12/>
			<FIELD.13/>
			<FIELD.14/>
			<FIELD.15/>
			<FIELD.16/>
			<FIELD.17>
				
			</FIELD.17>
			<FIELD.18/>
			<FIELD.19>
				
			</FIELD.19>
			<FIELD.20/>
			<FIELD.21/>
			<FIELD.22/>
			<FIELD.23/>
			<FIELD.24/>
			<FIELD.25/>
			<FIELD.26/>
			<FIELD.27/>
			<FIELD.28/>
			<FIELD.29/>
			<FIELD.30/>
			<FIELD.31/>
			<FIELD.32/>
			<FIELD.33/>
			<FIELD.34/>
			<FIELD.35/>
			<FIELD.36/>
			<FIELD.37/>
			<FIELD.38/>
			<FIELD.39/>
			<FIELD.40/>
			<FIELD.41/>
			<FIELD.42/>
			<FIELD.43/>
			<FIELD.44>
				
			</FIELD.44>
			<FIELD.45/>
			<FIELD.46/>
			<FIELD.47/>
			<FIELD.48/>
			<FIELD.49/>
			<FIELD.50/>
			<FIELD.51>
				
			</FIELD.51>
			<FIELD.52/>
		</PV1>
	</xsl:template>
</xsl:stylesheet>
