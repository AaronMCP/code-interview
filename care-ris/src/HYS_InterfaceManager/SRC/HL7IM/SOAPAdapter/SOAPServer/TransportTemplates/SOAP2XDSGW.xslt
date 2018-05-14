<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:myTrans="urn:xdsgw:XmlNodeTransformer" exclude-result-prefixes="myTrans">
<!--<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">-->
  <xsl:template match="/">
    <Message>
      <Header/>
      <Body>
        <xsl:value-of select="myTrans:GetDescapingInnerXml(
          '/soap:Envelope/soap:Body/csh:MessageCom/csh:RequestMessage',
          'soap|http://schemas.xmlsoap.org/soap/envelope/|csh|http://www.carestreamhealth.com/')"
          disable-output-escaping="yes"/>
        <!--<xsl:copy-of select="/soap:Envelope/soap:Body/node()" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"/>-->
      </Body>
    </Message>
  </xsl:template>
</xsl:stylesheet>