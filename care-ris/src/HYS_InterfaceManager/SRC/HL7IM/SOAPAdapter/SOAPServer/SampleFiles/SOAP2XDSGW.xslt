<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:myTrans="urn:xdsgw:XmlNodeTransformer" exclude-result-prefixes="myTrans">
<xsl:template match="/">
 <Message>
  <Header/>
    <Body><xsl:value-of select="myTrans:GetDescapingInnerXml(
    '/soap:Envelope/soap:Body/csh:MessageCom/csh:RequestMessage',
    'soap|http://schemas.xmlsoap.org/soap/envelope/|csh|http://www.carestreamhealth.com/')" 
    disable-output-escaping="yes"/>
  </Body>
 </Message>
</xsl:template>
</xsl:stylesheet>
