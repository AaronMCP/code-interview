<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" 
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="DATAINDEX_ACK">   
          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
              <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_PROCESS_FLAG">
            <xsl:text>1</xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_1">
            <xsl:value-of select="MSA/FIELD.1/FIELD_ITEM"></xsl:value-of>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_2">
            <xsl:value-of select="MSA/FIELD.3/FIELD_ITEM"></xsl:value-of>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_3">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_4">
            <xsl:text></xsl:text>
          </xsl:element>           
  </xsl:template>

</xsl:stylesheet>
