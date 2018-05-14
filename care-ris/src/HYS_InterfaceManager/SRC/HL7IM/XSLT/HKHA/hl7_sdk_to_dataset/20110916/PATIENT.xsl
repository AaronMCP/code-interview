<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
	
<!--<xsl:template name="GetPatName">
		<xsl:param name="PatName" select="$PatName"></xsl:param>
	
		<xsl:choose>
			<xsl:when test="count(following-sibling::*)>0">
				<xsl:for-each select="following-sibling::*[1]">
				<xsl:call-template name="GetPatName">
					<xsl:with-param name="PatName" select="concat($PatName,'^',.)"/>
				</xsl:call-template>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test=".!=''"><xsl:value-of select="concat($PatName,'^',.)"></xsl:value-of></xsl:when>
					<xsl:otherwise><xsl:value-of select="$PatName"></xsl:value-of></xsl:otherwise>
				</xsl:choose>
				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->

	<xsl:template name="PATIENT">
		
<!--		<xsl:param name="PatName"></xsl:param>
			<xsl:for-each select="../PID/FIELD.5/FIELD_ITEM[1]/*[1]">
				<xsl:call-template name="GetPatName">
				<xsl:with-param name="PatName" select="concat($PatName,.)"/>
			</xsl:call-template>
			</xsl:for-each>
				-->
				
		<xsl:element name="csb:PATIENT_ACCOUNT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ADDRESS">
			<xsl:value-of select="../PID/FIELD.11/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BED_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTH_PLACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTHDATE">
			<xsl:value-of select="../PID/FIELD.7/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CITIZENSHIP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_COUNTRY_CODE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_1">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_2">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_3">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_4">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_DRIVERLIC_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ETHNIC_GROUP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MARITAL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MOTHER_MAIDEN_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_NATIONALITY">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_OTHER_PID">
		<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_ALIAS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCAL_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCATION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_NAME">
		
			<xsl:value-of select="../PID/FIELD.5/FIELD_ITEM/*"></xsl:value-of>
<!--			<xsl:value-of select="$PatName"/>-->
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_TYPE">
			<xsl:value-of select="../PV1/FIELD.2/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENTID">
			
			<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
			<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
			<xsl:value-of select="../PID/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
			<xsl:value-of select="../PID/FIELD.13/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIMARY_LANGUAGE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_ID">
			<xsl:value-of select="../MRG/FIELD.1/FIELD_ITEM/COMPONENT.1"/>,
			<xsl:value-of select="../MRG/FIELD.1/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_VISIT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RELIGION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SEX">
			<xsl:value-of select="../PID/FIELD.8/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SSN_NUMBER">
			<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VISIT_NUMBER">
			<xsl:value-of select="../PV1/FIELD.19/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
