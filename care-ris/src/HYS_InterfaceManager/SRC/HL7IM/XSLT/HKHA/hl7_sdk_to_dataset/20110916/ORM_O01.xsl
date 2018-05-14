<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" 
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="ORM_O01">
      <csb:table>

          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
            <xsl:choose>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A01' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A04' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A05'">
                <xsl:text>00</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A08' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A31'">
                <xsl:text>01</xsl:text>
              </xsl:when>
	      <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A40'">
                <xsl:text>02</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'NW'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'CH'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'SN'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'RO'">
                <xsl:text>10</xsl:text>
              </xsl:when>
               <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'XO'">
                <xsl:text>11</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'DC'">
                <xsl:text>12</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'R01' ">
                <xsl:text>30</xsl:text>
              </xsl:when>
	      <xsl:otherwise>
                <xsl:text>01</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_PROCESS_FLAG">
            <xsl:text>0</xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_1">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_2">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_3">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_4">
            <xsl:text></xsl:text>
          </xsl:element>


		<xsl:element name="csb:PATIENT_ACCOUNT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ADDRESS">
			<xsl:value-of select="PID/FIELD.11/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BED_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTH_PLACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTHDATE">
			<xsl:value-of select="PID/FIELD.7/FIELD_ITEM/COMPONENT.1"/>
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
					<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
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
		
			<xsl:value-of select="PID/FIELD.5/FIELD_ITEM/*"></xsl:value-of>
<!--			<xsl:value-of select="$PatName"/>-->
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_TYPE">
			<xsl:value-of select="PV1/FIELD.2/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENTID">
			
			<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
			<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
			<xsl:value-of select="PID/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
			<xsl:value-of select="PID/FIELD.13/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIMARY_LANGUAGE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_ID">
			<xsl:value-of select="MRG/FIELD.4/FIELD_ITEM/COMPONENT.1"/>
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
			<xsl:value-of select="PID/FIELD.8/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SSN_NUMBER">
			<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VISIT_NUMBER">
			<xsl:value-of select="PV1/FIELD.19/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>

        <xsl:element name="csb:ORDER_BODY_PART">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_AMOUNT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_STATUS">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CNT_AGENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_1">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_2">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_3">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_4">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_DURATION">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_COMMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_DT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_LOCATION">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_REQUIREMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_STATUS">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_VOLUME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_DEPARTMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_NO">
          <xsl:value-of select="OBR/FIELD.3/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_MODALITY">
          <xsl:value-of select="OBR/FIELD.24/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_ORDER_NO">
          <xsl:value-of select="OBR/FIELD.2/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PATIENT_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_DEPARTMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_NO">
          <xsl:value-of select="OBR/FIELD.18/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_CODE">
	  <xsl:for-each select="OBR"><xsl:value-of select="FIELD.4/FIELD_ITEM/COMPONENT.1"/>/</xsl:for-each>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_DESC">
          <xsl:for-each select="OBR"><xsl:value-of select="FIELD.4/FIELD_ITEM/COMPONENT.2"/>/</xsl:for-each>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_NAME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_CLASS_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_ORGANIZATION">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.14"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_PHYSICIAN">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.2"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REQUEST_REASON">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REUQEST_COMMENTS">
          <xsl:value-of select="OBR/FIELD.13/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_SCHEDULED_DT">
          <!--<xsl:value-of select="preceding-sibling::ORC[1]/FIELD.7/FIELD_ITEM/COMPONENT.4"></xsl:value-of>-->
	  <xsl:value-of select="ORC/FIELD.9/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_SERIES_NO">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_AETITLE">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_NAME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_INSTANCE_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TECHNICIAN">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TRANSPORT_ARRANGE">
          <xsl:text></xsl:text>
        </xsl:element>


       </csb:table>
  </xsl:template>

</xsl:stylesheet>
