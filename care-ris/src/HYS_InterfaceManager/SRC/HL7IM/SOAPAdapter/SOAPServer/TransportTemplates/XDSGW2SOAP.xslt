<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:myTrans="urn:xdsgw:XmlNodeTransformer" exclude-result-prefixes="myTrans">
<!--<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">-->
  <xsl:template match="/">
    <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <MessageComResponse xmlns="http://www.carestreamhealth.com/">
          <MessageComResult>0</MessageComResult>
          <ReturnMessage>
            <xsl:value-of select="myTrans:GetEscapingInnerXml('/Message/Body','')" disable-output-escaping="yes"/>
          </ReturnMessage>
        </MessageComResponse>
        <!--<xsl:copy-of select="/Message/Body/node()"/>-->
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>