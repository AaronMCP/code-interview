<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1" 
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">

<xsl:template match="/">
	<Message>		
		<xsl:apply-templates select="//Header"/>		
		<xsl:apply-templates select="//Body"/>
	</Message>
</xsl:template>
 
 <xsl:template match="//Header">	 
		 <xsl:copy></xsl:copy>	 
 </xsl:template>

  <xsl:template match="//Body">   
	<Body>
    <!-- 
    For each type of HL7v2 message, create a xsl template for it.
    Then include the template into this file, and call it in the following choose..when statement.
    
    Note: According to HL7v3 Web Services Profile (http://www.hl7.org/v3ballot/html/infrastructure/transport/transport-wsprofiles.html),
    under the Body element there should be a single element, which is the top-level element of HL7 XML message.
    -->
		<xsl:for-each select="//Body/*">
				<xsl:choose>
					<xsl:when test="name()= 'ris:ORM_O01'">
            <csb:Order>
						<xsl:call-template name="ORM_O01"/>
            </csb:Order>
					</xsl:when>
          <xsl:when test="name()= 'ADT_A01'">
            <csb:Patient>
              <xsl:call-template name="ADT_A01"/>
            </csb:Patient>
          </xsl:when>
				</xsl:choose>
		</xsl:for-each>
  </Body>
 </xsl:template>   

<xsl:include href = "./ORM_O01.xsl"/>
<xsl:include href = "./ADT_A01.xsl"/>

</xsl:stylesheet>