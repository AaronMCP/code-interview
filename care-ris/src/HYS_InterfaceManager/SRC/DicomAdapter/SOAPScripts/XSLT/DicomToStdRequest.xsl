<xsl:stylesheet version="1.1"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:dcm="http://www.carestream.com/DCM_SDK"
                xmlns:reqXml="urn:xdsgw:AdditionalXmlDocument" 
                exclude-result-prefixes="reqXml dcm">
	<xsl:template match="/">
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
                   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
                   xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <soap:Header></soap:Header>
      <soap:Body>
        <MessageCom xmlns="http://www.carestreamhealth.com/">
          <RequestMessage>
            <xsl:copy-of select="/node()"/>
          </RequestMessage>
        </MessageCom>
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>
