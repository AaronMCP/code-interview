<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="MSH">
  
  <MSH>
     <FIELD.1>
         <!--<xsl:value-of select="MSH/@FieldSeparator"/>-->
         <xsl:text>|</xsl:text>
     </FIELD.1>
     <FIELD.2>
<xsl:text>^~\&amp;</xsl:text>
         <!--<xsl:value-of select="MSH/@EncodingCharacters"/>-->
	 
     </FIELD.2>   
     
     <FIELD.3>
		 <xsl:for-each select="SendingApplication">
			 <xsl:call-template name="HD"></xsl:call-template>
		 </xsl:for-each>         
     </FIELD.3>
     
     <FIELD.4>
		 <xsl:for-each select="SendingFacility">
			 <xsl:call-template name="HD"></xsl:call-template>
		 </xsl:for-each>               
     </FIELD.4>
     
     <FIELD.5>
          <xsl:for-each select="ReceivingApplication">
			 <xsl:call-template name="HD"></xsl:call-template>
		 </xsl:for-each>           
     </FIELD.5>
     
     <FIELD.6>
		<xsl:for-each select="ReceivingFacility">
			 <xsl:call-template name="HD"></xsl:call-template>
		 </xsl:for-each>         
     </FIELD.6>
    
     <FIELD.7>
         <xsl:if test="count(@DTOfMessage ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DTOfMessage"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
    
     <FIELD.8>
         <xsl:if test="count(@Security ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Security"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.8>
    
     <FIELD.9>
		 <xsl:for-each select="MessageType">
			 <xsl:call-template name="MSG"></xsl:call-template>
		 </xsl:for-each>          
     </FIELD.9>
     
     <FIELD.10>
         <xsl:if test="count(@MessageControlID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@MessageControlID"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.10>
    
     <FIELD.11>
		<xsl:for-each select="ProcessingID">
			 <xsl:call-template name="PT"></xsl:call-template>
		 </xsl:for-each>         
     </FIELD.11>
     
     <FIELD.12>
         <xsl:for-each select="VersionID">
			 <xsl:call-template name="VID"></xsl:call-template>
		 </xsl:for-each>        
     </FIELD.12>
     
     <FIELD.13>
         <xsl:if test="count(@SequenceNumber ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SequenceNumber"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.13>
    
     <FIELD.14>
         <xsl:if test="count(@ContinuationPointer ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ContinuationPointer"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.14>
    
     <FIELD.15>                    
         <xsl:if test="count(@AcceptAcknowledgmentType ) > 0">      
             <FIELD_ITEM><xsl:value-of select="@AcceptAcknowledgmentType"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.15>
    
     <FIELD.16>
         <xsl:if test="count(@ApplicationAcknowledgmentType ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ApplicationAcknowledgmentType"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.16>
    
     <FIELD.17>      
         <xsl:if test="count(@CountryCode ) > 0">   
             <FIELD_ITEM><xsl:value-of select="@CountryCode"/></FIELD_ITEM>         
         </xsl:if>
     </FIELD.17>
    
    <FIELD.18>
         <xsl:for-each select="CharacterSet">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>  
         </xsl:for-each>
    </FIELD.18>
    
     <FIELD.19>
		 <xsl:for-each select="PrincipleLanguageOfMessage">
            <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
         
     </FIELD.19>
    
     <FIELD.20>         
         <xsl:if test="count(@AlternateCharacterSetHandlingScheme ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AlternateCharacterSetHandlingScheme"/></FIELD_ITEM>       
         </xsl:if> 
     </FIELD.20>
    
    <FIELD.21>                                    
         <xsl:for-each select="MessageProfileIdentifier">
             <xsl:call-template name="HD"></xsl:call-template>
         </xsl:for-each>
    </FIELD.21>
  </MSH>
  
  </xsl:template>

</xsl:stylesheet>

