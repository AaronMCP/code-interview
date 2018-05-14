<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:myTrans="urn:xdsgw:XmlNodeTransformer" 
                exclude-result-prefixes="myTrans">
  <xsl:template match="/">
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"
                   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                   xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <soap:Header>
        <xsl:copy-of select="/soap:Envelope/soap:Header/node()" 
                     xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"/>
      </soap:Header>
      <soap:Body>
        <MessageComResponse xmlns="http://www.carestreamhealth.com/">
          <MessageComResult>
            <xsl:value-of select="/soap:Envelope/soap:Body/csh:MessageComResponse/csh:MessageComResult"
                          xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"
                          xmlns:csh="http://www.carestreamhealth.com/" />
          </MessageComResult>
          <ReturnMessage>
            <xsl:value-of select="myTrans:GetDescapingInnerXml(
              '/soap:Envelope/soap:Body/csh:MessageComResponse/csh:ReturnMessage',
              'soap|http://schemas.xmlsoap.org/soap/envelope/|csh|http://www.carestreamhealth.com/')"
              disable-output-escaping="yes"/>
          </ReturnMessage>
      </MessageComResponse>
    </soap:Body>
   </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>