<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" 
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="DATAINDEX">
   
          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
            <xsl:choose>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A01' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A04' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A05'">
                <xsl:text>00</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A08' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A31'">
                <xsl:text>01</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'NW'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'CH'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'SN'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'RO'">
                <xsl:text>10</xsl:text>
              </xsl:when>
               <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'XO'">
                <xsl:text>11</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'DC'">
                <xsl:text>12</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'CA'">
                <xsl:text>13</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'S12' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'S13' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'S15'">
                <xsl:text>20</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'R01' ">
                <xsl:text>30</xsl:text>
              </xsl:when>
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

           
  </xsl:template>

</xsl:stylesheet>
