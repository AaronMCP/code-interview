<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:js="urn:custom-javascript" exclude-result-prefixes="msxsl js">
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="*">
		<Message>
			<Header/>
			<Body>
				<xsl:for-each select="//MSH">
					<ACK>
						<MSH>
							<FIELD.1>
								<!--<xsl:value-of select="MSH/@FieldSeparator"/>-->
								<xsl:text>|</xsl:text>
							</FIELD.1>
							<FIELD.2>
								<!--<xsl:value-of select="MSH/@EncodingCharacters"/>-->
								<xsl:text>^~\&amp;</xsl:text>
							</FIELD.2>
							<FIELD.3>
								<xsl:copy-of select="FIELD.5/*"/>
							</FIELD.3>
							<FIELD.4>
								<xsl:copy-of select="FIELD.6/*"/>
							</FIELD.4>
							<FIELD.5>
								<xsl:copy-of select="FIELD.3/*"/>
							</FIELD.5>
							<FIELD.6>
								<xsl:copy-of select="FIELD.4/*"/>
							</FIELD.6>
							<FIELD.7>
								<FIELD_ITEM>
								<COMPNENT.1>
								<xsl:value-of select="js:GetCurrentDate()"/>
								</COMPNENT.1>
								</FIELD_ITEM>
							</FIELD.7>
							<FIELD.8>
								<xsl:copy-of select="FIELD.8/*"/>
							</FIELD.8>
							<FIELD.9>
								<FIELD_ITEM>
									<xsl:choose>
										<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.1 = 'ORM'">
											<xsl:element name="COMPONENT.1">
												<xsl:text>ORR</xsl:text>
											</xsl:element>
											<xsl:element name="COMPONENT.2">
												<xsl:text>O02</xsl:text>
											</xsl:element>
											<xsl:if test="FIELD.9/FIELD_ITEM/COMPONENT.3 != ''">
												<xsl:element name="COMPONENT.3">
													<xsl:text>ORR_O02</xsl:text>
												</xsl:element>
											</xsl:if>
										</xsl:when>
										<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.1 = 'OMG'">
											<xsl:element name="COMPONENT.1">
												<xsl:text>ORG</xsl:text>
											</xsl:element>
											<xsl:element name="COMPONENT.2">
												<xsl:text>O20</xsl:text>
											</xsl:element>
											<xsl:if test="FIELD.9/FIELD_ITEM/COMPONENT.3 != ''">
												<xsl:element name="COMPONENT.3">
													<xsl:text>ORG_O20</xsl:text>
												</xsl:element>
											</xsl:if>
										</xsl:when>
										<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.1 = 'OMI'">
											<xsl:element name="COMPONENT.1">
												<xsl:text>ORI</xsl:text>
											</xsl:element>
											<xsl:element name="COMPONENT.2">
												<xsl:text>O24</xsl:text>
											</xsl:element>
											<xsl:if test="FIELD.9/FIELD_ITEM/COMPONENT.3 != ''">
												<xsl:element name="COMPONENT.3">
													<xsl:text>ORI_O24</xsl:text>
												</xsl:element>
											</xsl:if>
										</xsl:when>
										<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.1 = 'OML'">
											<xsl:element name="COMPONENT.1">
												<xsl:text>ORL</xsl:text>
											</xsl:element>
											<xsl:element name="COMPONENT.2">
												<xsl:choose>
													<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O21'">
														<xsl:text>O22</xsl:text>
													</xsl:when>
													<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O33'">
														<xsl:text>O34</xsl:text>
													</xsl:when>
													<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O35'">
														<xsl:text>O36</xsl:text>
													</xsl:when>
												</xsl:choose>
											</xsl:element>
											<xsl:if test="FIELD.9/FIELD_ITEM/COMPONENT.3 != ''">
												<xsl:element name="COMPONENT.3">
													<xsl:choose>
														<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O21'">
															<xsl:text>ORL_O22</xsl:text>
														</xsl:when>
														<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O33'">
															<xsl:text>ORL_O34</xsl:text>
														</xsl:when>
														<xsl:when test="FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O35'">
															<xsl:text>ORL_O36</xsl:text>
														</xsl:when>
													</xsl:choose>
												</xsl:element>
											</xsl:if>
										</xsl:when>
										<xsl:otherwise>
											<xsl:element name="COMPONENT.1">
												<xsl:text>ACK</xsl:text>
											</xsl:element>
											<xsl:element name="COMPONENT.2">
												<xsl:copy-of select="FIELD.9/FIELD_ITEM/COMPONENT.2"/>
											</xsl:element>
											<xsl:if test="FIELD.9/FIELD_ITEM/COMPONENT.3 != ''">
												<xsl:element name="COMPONENT.3">
													<xsl:text>ACK</xsl:text>
												</xsl:element>
											</xsl:if>
										</xsl:otherwise>
									</xsl:choose>
								</FIELD_ITEM>
							</FIELD.9>
							<FIELD.10>
								<xsl:copy-of select="FIELD.10/*"/>
							</FIELD.10>
							<FIELD.11>
								<xsl:copy-of select="FIELD.11/*"/>
							</FIELD.11>
							<FIELD.12>
								<xsl:copy-of select="FIELD.12/*"/>
							</FIELD.12>
							<FIELD.13>
								<xsl:copy-of select="FIELD.13/*"/>
							</FIELD.13>
							<FIELD.14>
								<xsl:copy-of select="FIELD.14/*"/>
							</FIELD.14>
							<FIELD.15>
								<xsl:copy-of select="FIELD.15/*"/>
							</FIELD.15>
							<FIELD.16>
								<xsl:copy-of select="FIELD.16/*"/>
							</FIELD.16>
							<FIELD.17>
								<xsl:copy-of select="FIELD.17/*"/>
							</FIELD.17>
							<FIELD.18>
								<xsl:copy-of select="FIELD.18/*"/>
							</FIELD.18>
							<FIELD.19>
								<xsl:copy-of select="FIELD.19/*"/>
							</FIELD.19>
							<FIELD.20>
								<xsl:copy-of select="FIELD.20/*"/>
							</FIELD.20>
						</MSH>
						<MSA>
							<FIELD.1>
								<FIELD_ITEM>
									<xsl:text>AA</xsl:text>
								</FIELD_ITEM>
							</FIELD.1>
							<FIELD.2>
								<xsl:copy-of select="FIELD.10/*"/>
							</FIELD.2>
							<FIELD.3>
								<FIELD_ITEM>
									<xsl:text>Success</xsl:text>
								</FIELD_ITEM>
							</FIELD.3>
							<FIELD.4>
								<FIELD_ITEM/>
							</FIELD.4>
							<FIELD.5>
								<FIELD_ITEM/>
							</FIELD.5>
							<FIELD.6>
         
     </FIELD.6>
						</MSA>
					</ACK>
				</xsl:for-each>
			</Body>
		</Message>
	</xsl:template>
	<!--xsl:include href="MSH.xsl"/-->
	<msxsl:script language="JavaScript" implements-prefix="js"><![CDATA[
function GetCurrentDate()
{
	var time = new Date();

    var year = time.getFullYear().toString();

	var mon =time.getMonth()+1;
	
	if (mon>=10)
	{
		var month = mon.toString();
	}else
	{
	var month = "0"+mon.toString();
	}
    
	var d = time.getDate();
	if (d>=10)
	{
    var day = d.toString();
    }else
    {
     var day = "0"+d.toString();
    }

	var h= time.getHours();
	if (h>=10)
	{
    var hours = h.toString();
    }else
    {
    var hours = "0"+h.toString();
    }

	var m= time.getMinutes();
	if (m>=10)
	{
    var min = m.toString();
    }else
    {
    var min = "0"+m.toString();
    }
    
    
	var s= time.getSeconds();
	if (s>=10)
	{
    var sec = s.toString();
    }else
    {
    var sec = "0"+s.toString();
    }

    var mm = time.getMilliseconds();
    if (mm>=100)
    {
     var mms = mm.toString();
     }else if (mm>=10)
     {
     var mms = "0"+mm.toString();
     }else
     {
     var mms = "00"+mm.toString();
     }

    return year+month +day+hours+min+sec+"."+mms;
}
]]></msxsl:script>
</xsl:stylesheet>
