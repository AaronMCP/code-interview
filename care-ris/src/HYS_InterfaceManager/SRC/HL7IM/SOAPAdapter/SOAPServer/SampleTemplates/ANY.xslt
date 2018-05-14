<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <Message>
      <Header/>
      <Body>
        <HL7>
	        <MSH FieldSeparator="|" EncodingCharacters="^~\&amp;" MessageControlID="102108">
            <SendingApplication>
              <xsl:attribute name="EntityIdentifier">
                <xsl:value-of select="/Message/Body/HL7/MSH/ReceivingApplication/@EntityIdentifier"/>
              </xsl:attribute>
            </SendingApplication>
            <SendingFacility>
              <xsl:attribute name="EntityIdentifier">
                <xsl:value-of select="/Message/Body/HL7/MSH/ReceivingFacility/@EntityIdentifier"/>
              </xsl:attribute>
            </SendingFacility>
            <ReceivingApplication>
              <xsl:attribute name="EntityIdentifier">
                <xsl:value-of select="/Message/Body/HL7/MSH/SendingApplication/@EntityIdentifier"/>
              </xsl:attribute>
            </ReceivingApplication>
            <ReceivingFacility>
              <xsl:attribute name="EntityIdentifier">
                <xsl:value-of select="/Message/Body/HL7/MSH/SendingFacility/@EntityIdentifier"/>
              </xsl:attribute>
            </ReceivingFacility>
		        <MessageType MessageCode="ACK" TriggerEvent="A01" MessageStructure="ACK"/>
		        <ProcessingID ProcessingID="P"/>
		        <VersionID VersionID="2.3.1"/>
	        </MSH>
	        <MSA TextMessage="" AcknowledgmentCode="AA" MessageControlID="00019">
	        </MSA>
        </HL7>
      </Body>
    </Message>
  </xsl:template>
  <!--<xsl:template match="/">
    <Message>
      <Header/>
      <Body>
        <S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
          <S:Header>
            <WorkContext xmlns="http://oracle.com/weblogic/soap/workarea/">rO0ABXdbACd3ZWJsb2dpYy5hcHAuRUFJX0NPTU1PTl9FU0JfRHVtbXlXU19XU1MAAADWAAAAI3dlYmxvZ2ljLndvcmthcmVhLlN0cmluZ1dvcmtDb250ZXh0AAMxLjAAAA==</WorkContext>
          </S:Header>
          <S:Body>
            <ns2:receiveWithSequenceResponse xmlns:ns2="http://test.eai.ha.org.hk/"/>
          </S:Body>
        </S:Envelope>
      </Body>
    </Message>
  </xsl:template>-->
</xsl:stylesheet>
