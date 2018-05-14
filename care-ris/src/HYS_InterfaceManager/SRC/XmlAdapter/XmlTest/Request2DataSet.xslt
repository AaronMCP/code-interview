<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template name ="Identifier">
    <xsl:value-of select ="ID"/>
  </xsl:template>

  <!--<xsl:include href = "XIMTypeTemplate.xsl"/>-->

  <xsl:template match="/">
    <NewDataSet>
      <!--<xsl:for-each select ="/XMLRequestMessage/XIM/ITEM">-->
        <Table>
          <DataIndex_RecordID_1>
            <xsl:value-of select="/XMLRequestMessage/Name"/>
          </DataIndex_RecordID_1>
          <DataIndex_RecordID_2>
            <xsl:for-each select ="/XMLRequestMessage/TargetDevice">
              <xsl:value-of select ="current()"/>^
              <xsl:call-template name ="Identifier">
              </xsl:call-template>
            </xsl:for-each>
          </DataIndex_RecordID_2>
          <DataIndex_RecordID_3>
          <xsl:for-each select ="/XMLRequestMessage/DeviceResponse">
            <xsl:value-of select ="Device"/>^(
            <xsl:for-each select ="Status">
              <xsl:value-of select ="current()"/>^
            </xsl:for-each>)=
          </xsl:for-each>
          </DataIndex_RecordID_3>
          <Patient_PatientName>
            <xsl:value-of select="PATIENT/IDENTIFICATION/ID"/>
          </Patient_PatientName>
        </Table>
      <!--</xsl:for-each>-->
    </NewDataSet>
  </xsl:template>
  
</xsl:stylesheet>