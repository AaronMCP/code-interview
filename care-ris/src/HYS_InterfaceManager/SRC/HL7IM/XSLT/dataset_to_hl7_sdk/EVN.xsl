<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1"
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>

  <xsl:template name="EVN">


        <EVN>
        <FIELD.1 />
        <FIELD.2>
          <FIELD_ITEM>
            <COMPONENT.1><xsl:value-of select="csb:DATAINDEX_DATADT"></xsl:value-of></COMPONENT.1>
          </FIELD_ITEM>
        </FIELD.2>
        <FIELD.3 />
        <FIELD.4 />
        <FIELD.5 />
        <FIELD.6>
          <FIELD_ITEM>
            <COMPONENT.1><xsl:value-of select="csb:DATAINDEX_DATADT"></xsl:value-of></COMPONENT.1>
          </FIELD_ITEM>
        </FIELD.6>
      </EVN>
      

      

  </xsl:template>
</xsl:stylesheet>
