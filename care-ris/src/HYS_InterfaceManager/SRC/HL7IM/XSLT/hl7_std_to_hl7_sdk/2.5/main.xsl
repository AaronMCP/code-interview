<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
  <xsl:template match="/">
    <Message>
      <xsl:copy-of select="Message/Header"/>
      <xsl:apply-templates select="//Body"/>
      <xsl:copy-of select="//Body/following-sibling::*"/>
    </Message>
  </xsl:template>
  <xsl:template match="//Body">
    <Body>
      <xsl:for-each select="child::*">
        <xsl:choose>
          <xsl:when test="MSH/MessageType/@MessageCode='ACK' or MSH/MessageType/@MessageCode='ORR' ">
            <xsl:call-template name="ACK"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A01' ">
            <xsl:call-template name="ADT_A01"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A02' ">
            <xsl:call-template name="ADT_A02"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A03' ">
            <xsl:call-template name="ADT_A03"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A04' ">
            <xsl:call-template name="ADT_A04"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A05' ">
            <xsl:call-template name="ADT_A05"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A07' ">
            <xsl:call-template name="ADT_A07"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A08' ">
            <xsl:call-template name="ADT_A08"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A11' ">
            <xsl:call-template name="ADT_A11"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A12' ">
            <xsl:call-template name="ADT_A12"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A13' ">
            <xsl:call-template name="ADT_A13"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A31' ">
            <xsl:call-template name="ADT_A31"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A40' ">
            <xsl:call-template name="ADT_A40"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ORM' and MSH/MessageType/@TriggerEvent='O01' ">
            <xsl:call-template name="ORM_O01"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='OMG' and MSH/MessageType/@TriggerEvent='O09' ">
            <xsl:call-template name="OMG_O09"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='OMI' and MSH/MessageType/@TriggerEvent='O23' ">
            <xsl:call-template name="OMI_O23"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='ORU' and MSH/MessageType/@TriggerEvent='R01' ">
            <xsl:call-template name="ORU_R01"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='QBP' and MSH/MessageType/@TriggerEvent='Q23' ">
            <xsl:call-template name="QBP_Q23"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='RSP' and MSH/MessageType/@TriggerEvent='K23' ">
            <xsl:call-template name="RSP_K23"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='QBP' and MSH/MessageType/@TriggerEvent='Q22' ">
            <xsl:call-template name="QBP_Q22"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='RSP' and MSH/MessageType/@TriggerEvent='K22' ">
            <xsl:call-template name="RSP_K22"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='QBP' and MSH/MessageType/@TriggerEvent='ZV1' ">
            <xsl:call-template name="QBP_ZV1"></xsl:call-template>
          </xsl:when>
          <xsl:when test="MSH/MessageType/@MessageCode='RSP' and MSH/MessageType/@TriggerEvent='ZV2' ">
            <xsl:call-template name="RSP_ZV2"></xsl:call-template>
          </xsl:when>
    
        </xsl:choose>
      </xsl:for-each>
    </Body>
  </xsl:template>

  <xsl:include href = "./messages.xsl"/>

</xsl:stylesheet>

