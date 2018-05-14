<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="DATAINDEX_ORDER">
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
  </xsl:template>

  <xsl:template name="DATAINDEX_PATIENT">
    <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
      <xsl:text></xsl:text>
    </xsl:element>
    <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
      <xsl:choose>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-SCH' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-SCH' ">
          <xsl:text>00</xsl:text>
        </xsl:when>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-MOD' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-CHG' ">
          <xsl:text>01</xsl:text>
        </xsl:when>
        <xsl:when test="../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-AP-CAN' or ../ris:MSH/ris:MSH.3/ris:HD.2 = 'RAD-EP-CAN' ">
          <xsl:text>03</xsl:text>
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
  </xsl:template>
  
</xsl:stylesheet>