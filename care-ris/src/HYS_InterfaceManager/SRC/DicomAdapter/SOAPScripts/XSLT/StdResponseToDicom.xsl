<xsl:stylesheet version="1.1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"
                xmlns:c="http://www.carestreamhealth.com/"
                xmlns:h="http://www.carestream.com/HL7_STD"
                exclude-result-prefixes="s c h">
  <xsl:template match="/">
      <xsl:copy-of select="/s:Envelope/s:Body/c:MessageComResponse/c:ReturnMessage/node()"/>
  </xsl:template>
</xsl:stylesheet>
