<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
	<xsl:strip-space elements="*"/>
  
  <!--
	<xsl:template name="ORM_O01">
		<xsl:if test="ris:MSH !=''">
			<xsl:choose>
				<xsl:when test="count(ris:ORM_O01.ORDER)>0">
					<xsl:for-each select="ris:ORM_O01.ORDER">
						<Table>
              <xsl:call-template name="DATAINDEX_ORDER" ></xsl:call-template>
							<xsl:for-each select="../ris:ORM_O01.PATIENT">
								<xsl:call-template name="PATIENT"/>
							</xsl:for-each>
              <xsl:call-template name="ORDER" ></xsl:call-template>
						</Table>
					</xsl:for-each>
				</xsl:when>
        <xsl:when test="count(ris:ORM_O01.PATIENT)>0">
					<xsl:for-each select="ris:ORM_O01.PATIENT">
						<Table>
              <xsl:call-template name="DATAINDEX_PATIENT" ></xsl:call-template>
							<xsl:for-each select="../ris:ORM_O01.PATIENT">
								<xsl:call-template name="PATIENT"/>
							</xsl:for-each>
              <xsl:call-template name="ORDER" ></xsl:call-template>
						</Table>
					</xsl:for-each>
				</xsl:when>
			</xsl:choose>
		</xsl:if>
	</xsl:template>
	
	<xsl:include href = "./Common.xsl"/>
  <xsl:include href = "./DATAINDEX.xsl"/>
	<xsl:include href = "./PATIENT.xsl"/>
	<xsl:include href = "./ORDER.xsl"/>
  -->

  <xsl:template name="ORM_O01">
    <xsl:if test="ris:MSH !=''">
      <xsl:for-each select="ris:ORM_O01.ORDER">
        <csb:Table>
      
    <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
      <xsl:choose>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-SCH' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-SCH' ">
          <xsl:text>10</xsl:text>
        </xsl:when>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-MOD' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-CHG' ">
          <xsl:text>11</xsl:text>
        </xsl:when>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-CAN' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-CAN' ">
          <xsl:text>13</xsl:text>
        </xsl:when>
      </xsl:choose>
    </xsl:element>
    <xsl:element name="csb:DATAINDEX_PROCESS_FLAG">
      <xsl:text></xsl:text>
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

    <xsl:for-each select="../ris:ORM_O01.PATIENT">
    <xsl:element name="csb:PATIENT_ACCOUNT_NUMBER">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_ADDRESS">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_BED_NUMBER">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_BIRTH_PLACE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_BIRTHDATE">
      <xsl:value-of select="ris:PID/ris:PID.7/ris:TS.1"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_CITIZENSHIP">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_COUNTRY_CODE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_CUSTOMER_1">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_CUSTOMER_2">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_CUSTOMER_3">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_CUSTOMER_4">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_DRIVERLIC_NUMBER">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_ETHNIC_GROUP">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_MARITAL_STATUS">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_MOTHER_MAIDEN_NAME">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_NATIONALITY">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_OTHER_PID">
      <xsl:value-of select="ris:PID/ris:PID.3[ris:CX.5='9']/ris:CX.1"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_ALIAS">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_LOCAL_NAME">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_LOCATION">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_NAME">
      <xsl:value-of select="ris:PID/ris:PID.5/ris:XPN.1/ris:FN.1"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_STATUS">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENT_TYPE">
      <xsl:value-of select="ris:ORM_O01.PATIENT_VISIT/ris:PV1/ris:PV1.2"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PATIENTID">
      <xsl:value-of select="ris:PID/ris:PID.3[ris:CX.5='PATKEY']/ris:CX.1"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
      <xsl:if test="count(ris:PID/ris:PID.14)>0">
        <xsl:value-of select="ris:PID/ris:PID.14/ris:XTN.1"></xsl:value-of>
      </xsl:if>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
      <xsl:if test="count(ris:PID/ris:PID.13)>0">
        <xsl:value-of select="ris:PID/ris:PID.13/ris:XTN.1"></xsl:value-of>
      </xsl:if>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PRIMARY_LANGUAGE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PRIOR_PATIENT_ID">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PRIOR_PATIENT_NAME">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_PRIOR_VISIT_NUMBER">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_RACE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_RELIGION">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_SEX">
      <xsl:value-of select="ris:PID/ris:PID.8"></xsl:value-of>
    </xsl:element>
    <xsl:element name="csb:PATIENT_SSN_NUMBER">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:PATIENT_VISIT_NUMBER">
      <xsl:value-of select="ris:ORM_O01.PATIENT_VISIT/ris:PV1/ris:PV1.19/ris:CX.1"></xsl:value-of>
    </xsl:element>
          </xsl:for-each>

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
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.47"></xsl:value-of>
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
    <xsl:value-of select="ris:ORC/ris:ORC.3/ris:EI.1"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_MODALITY">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_ORDER_NO">
    <xsl:value-of select="ris:ORC/ris:ORC.2/ris:EI.1"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_PATIENT_ID">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_PLACER">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_PLACER_CONTACT">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_PLACER_DEPARTMENT">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_PLACER_NO">
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.18"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_PROCEDURE_CODE">
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.4/ris:CE.1"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_PROCEDURE_DESC">
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.4/ris:CE.2"></xsl:value-of>
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
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.16/ris:XCN.14/ris:HD.1"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_REF_PHYSICIAN">
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.16/ris:XCN.2/ris:FN.1"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_REQUEST_REASON">
    <xsl:text></xsl:text>
  </xsl:element>
  <xsl:element name="csb:ORDER_REUQEST_COMMENTS">
    <xsl:value-of select="ris:ORM_O01.ORDER_DETAIL/ris:ORM_O01.CHOICE/ris:OBR/ris:OBR.13"></xsl:value-of>
  </xsl:element>
  <xsl:element name="csb:ORDER_SCHEDULED_DT">
    <xsl:value-of select="ris:ORC/ris:ORC.7/ris:TQ.4/ris:TS.1"></xsl:value-of>
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

        </csb:Table>
      </xsl:for-each>
    </xsl:if>
 </xsl:template>
  
</xsl:stylesheet>
