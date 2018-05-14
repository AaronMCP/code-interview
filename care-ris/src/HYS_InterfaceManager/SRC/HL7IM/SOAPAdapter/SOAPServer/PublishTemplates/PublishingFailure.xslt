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
            <!--<MessageType MessageCode="ACK" TriggerEvent="A01" MessageStructure="ACK"/>-->
            <xsl:copy-of select="/Message/Body/HL7/MSH/MessageType"/>
            <ProcessingID ProcessingID="P"/>
            <VersionID VersionID="2.3.1"/>
          </MSH>
          <MSA TextMessage="Message Type Unsupported" AcknowledgmentCode="AE" MessageControlID="00019">
            <ErrorCondition Identifier="200"/>
            <ERR InfluenceDegree="E" >
              <ErrorCodeAndPosition SegmentSequence="1" FieldLocated="1" SegmentID="MSH">
                <ErrorCode Identifier="200"/>
              </ErrorCodeAndPosition>
            </ERR>
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
            <xsl:copy-of select="/soap:Envelope/soap:Header/ns:WorkContext" xmlns:ns="http://oracle.com/weblogic/soap/workarea/" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"/>
          </S:Header>
          <S:Body>
            <S:Fault xmlns:ns4="http://www.w3.org/2003/05/soap-envelope">
              <faultcode>S:Server</faultcode>
              <faultstring>Message storage into CS Broker error.</faultstring>
              <detail/>
            </S:Fault>
          </S:Body>
        </S:Envelope>
      </Body>
    </Message>
  </xsl:template>-->
</xsl:stylesheet>