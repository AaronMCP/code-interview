<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template name ="PersonName">
    <LAST>
      <xsl:value-of select ="substring-after(current(),'^')"/>
    </LAST>
    <FIRST>
      <xsl:value-of select ="substring-before(current(),'^')"/>
    </FIRST>
  </xsl:template>
  
  <!--<xsl:include href = "XIMTypeTemplate.xsl"/>-->
  <xsl:template match="/">
    <XMLRequestMessage>
      <XISVersion>3.0</XISVersion>
      <Name>AAA</Name>
      <Qualifier></Qualifier>
      <OriginatingDevice>XIM(HL7) Adapter of GC Gateway 2.0</OriginatingDevice>
      <TransactionID>GATEWAY2_f469a92f-0b6b-4388-a5ec-a6c661c520ed</TransactionID>
      <TargetDevice>XIS</TargetDevice>
      <XIM>
        <ITEM>
          <SECURITY_AND_PRIVACY>
            <ENCRYPTED_DATA>
              <TRANSFER_SYNTAX_IDENTIFIER>
                <xsl:value-of select="/NewDataSet/Table/DataIndex_RecordID_1"/>
              </TRANSFER_SYNTAX_IDENTIFIER>
              <CONTENT>
                <xsl:for-each select ="/NewDataSet/Table/DataIndex_RecordID_2">
                  <xsl:call-template name ="PersonName">
                  </xsl:call-template>
                </xsl:for-each>
              </CONTENT>
            </ENCRYPTED_DATA>
          </SECURITY_AND_PRIVACY>
        </ITEM>
      </XIM>
    </XMLRequestMessage>
  </xsl:template>
</xsl:stylesheet>
