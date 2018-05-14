<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="ERR">
  
  <ERR>
     <FIELD.4><FIELD_ITEM><xsl:value-of select="@InfluenceDegree"/></FIELD_ITEM></FIELD.4>
     
     <FIELD.7><FIELD_ITEM><xsl:value-of select="@DiagnosisInformation"/></FIELD_ITEM></FIELD.7>
     
     <FIELD.8><FIELD_ITEM><xsl:value-of select="@UserMessage"/></FIELD_ITEM></FIELD.8>
     
     <FIELD.1>
         <xsl:for-each select="ErrorCodeAndPosition">
             <xsl:call-template name="ELD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.1>   
     
     <FIELD.2>
         <xsl:for-each select="ErrorPosition">
             <xsl:call-template name="ERL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="HL7ErrorCode">
             <xsl:call-template name="CWE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>       
     
     
     <FIELD.5>
         <xsl:for-each select="AppErrorCode">
             <xsl:call-template name="CWE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>   
     
     <FIELD.6><FIELD_ITEM><xsl:value-of select="@AppErrorParam"/></FIELD_ITEM></FIELD.6>
     
     <FIELD.9><FIELD_ITEM><xsl:value-of select="@NoticeIndextoPerson"/></FIELD_ITEM></FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="InvalidModel">
            <xsl:call-template name="CWE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.10>   
     
     <FIELD.11>
         <xsl:for-each select="InvalidModelCode">
             <xsl:call-template name="CWE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.11>   
     
     <FIELD.12>
         <xsl:for-each select="HelpDeskContactPoint">
             <xsl:call-template name="XTN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.12>      
     
  </ERR>
  
  </xsl:template>

</xsl:stylesheet>

