<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>


  <xsl:template name="ORM_O01">
    <xsl:if test="MSH !=''">
      <csb:Table>

        <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
          <xsl:choose>
            <xsl:when test="./MSH/ReceivingApplication/@NameSpaceID  = 'HIS'">
              <xsl:text>10</xsl:text>
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

        <xsl:for-each select="./PID">
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
            <xsl:value-of select="./@DTOfBirth"></xsl:value-of>
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
            <xsl:text></xsl:text>
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
            <xsl:value-of select="./PatientName/@GivenName"></xsl:value-of>
<xsl:value-of select="./PatientName/FamilyName/@Surnname"></xsl:value-of>
          </xsl:element>
          <xsl:element name="csb:PATIENT_PATIENT_STATUS">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:PATIENT_PATIENT_TYPE">
            <xsl:value-of select="./FIELD.2/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
          </xsl:element>
          <xsl:element name="csb:PATIENT_PATIENTID">
            <xsl:value-of select="./FIELD.3/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
          </xsl:element>
          <xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
            <xsl:text></xsl:text>
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
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:PATIENT_SSN_NUMBER">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:PATIENT_VISIT_NUMBER">
            <xsl:text></xsl:text>
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
          <xsl:value-of select="ris:ORC/ris:ORC.3/ris:EI.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_MODALITY">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_ORDER_NO">
          <xsl:value-of select="ris:ORC/ris:ORC.2/ris:EI.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PATIENT_ID">
          <xsl:value-of select="./PatientID_InternalID/@ID"></xsl:value-of>
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
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
