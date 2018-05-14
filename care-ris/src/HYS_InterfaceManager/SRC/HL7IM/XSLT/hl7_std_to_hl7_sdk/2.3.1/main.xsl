<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
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
					<xsl:when test="MSH/MessageType/@MessageCode='ACK'  ">
						<xsl:call-template name="ACK"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A01' ">
						<xsl:call-template name="ADT_A01"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A02' ">
						<xsl:call-template name="ADT_A02"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A03' ">
						<xsl:call-template name="ADT_A03"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A04' ">
						<xsl:call-template name="ADT_A04"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A05' ">
						<xsl:call-template name="ADT_A05"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A07' ">
						<xsl:call-template name="ADT_A07"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A08' ">
						<xsl:call-template name="ADT_A08"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A11' ">
						<xsl:call-template name="ADT_A11"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A12' ">
						<xsl:call-template name="ADT_A12"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A13' ">
						<xsl:call-template name="ADT_A13"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A31' ">
						<xsl:call-template name="ADT_A31"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ADT' and MSH/MessageType/@TriggerEvent='A40' ">
						<xsl:call-template name="ADT_A40"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ORM' and MSH/MessageType/@TriggerEvent='O01' ">
						<xsl:call-template name="ORM_O01"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='OMG' and MSH/MessageType/@TriggerEvent='O09' ">
						<xsl:call-template name="OMG_O09"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='OMI' and MSH/MessageType/@TriggerEvent='O23' ">
						<xsl:call-template name="OMI_O23"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ORU' and MSH/MessageType/@TriggerEvent='R01' ">
						<xsl:call-template name="ORU_R01"/>
					</xsl:when>
					<xsl:when test="MSH/MessageType/@MessageCode='ORR' and MSH/MessageType/@TriggerEvent='O02' ">
						<xsl:call-template name="ORR_O02"/>
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</Body>
	</xsl:template>
	<xsl:include href="./messages.xsl"/>
</xsl:stylesheet>
