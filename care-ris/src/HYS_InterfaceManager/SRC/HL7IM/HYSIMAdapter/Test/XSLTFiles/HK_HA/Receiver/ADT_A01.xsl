<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" 
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="ADT_A01">
    <xsl:if test="ris:MSH !=''">
      <xsl:for-each select="ris:PV1">
        <csb:Table>

          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
            <xsl:choose>
              <xsl:when test="../ris:MSH/ris:MSH.9/ris:MSG.2 = 'A04'">
                <xsl:text>00</xsl:text>
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
              <xsl:value-of select="../ris:PID/ris:PID.7/ris:TS.1"></xsl:value-of>
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
              <xsl:value-of select="../ris:PID/ris:PID.3[ris:CX.5='SS']/ris:CX.1"></xsl:value-of>
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
              <xsl:value-of select="../ris:PID/ris:PID.5/ris:XPN.1/ris:FN.1"></xsl:value-of>
            </xsl:element>
            <xsl:element name="csb:PATIENT_PATIENT_STATUS">
              <xsl:text></xsl:text>
            </xsl:element>
            <xsl:element name="csb:PATIENT_PATIENT_TYPE">
              <xsl:text></xsl:text>
            </xsl:element>
            <xsl:element name="csb:PATIENT_PATIENTID">
              <xsl:value-of select="../ris:PID/ris:PID.3[ris:CX.5='MR']/ris:CX.1"></xsl:value-of>
            </xsl:element>
            <xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
              <xsl:if test="count(../ris:PID/ris:PID.14)>0">
                <xsl:value-of select="../ris:PID/ris:PID.14/ris:XTN.1"></xsl:value-of>
              </xsl:if>
            </xsl:element>
            <xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
              <xsl:if test="count(../ris:PID/ris:PID.13)>0">
                <xsl:value-of select="../ris:PID/ris:PID.13/ris:XTN.1"></xsl:value-of>
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
              <xsl:value-of select="../ris:PID/ris:PID.8"></xsl:value-of>
            </xsl:element>
            <xsl:element name="csb:PATIENT_SSN_NUMBER">
              <xsl:text></xsl:text>
            </xsl:element>
            <xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
              <xsl:text></xsl:text>
            </xsl:element>
            <xsl:element name="csb:PATIENT_VISIT_NUMBER">
              <xsl:value-of select="ris:PV1.19/ris:CX.1"></xsl:value-of>
            </xsl:element>

        </csb:Table>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
