<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="PID">

  <PID>
     <FIELD.1>
         <xsl:if test="count(@SetIDPatientID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetIDPatientID"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
		 <xsl:for-each select="PatientID_External">
			 <xsl:call-template name="CX"></xsl:call-template>
		 </xsl:for-each>         
     </FIELD.2>
     
         <FIELD.3>
             <xsl:for-each select="PatientID_InternalID">
                 <xsl:call-template name="CX"></xsl:call-template>
             </xsl:for-each>
         </FIELD.3>
         
         <FIELD.4>
             <xsl:for-each select="AlternatePatientID-PID">
                 <xsl:call-template name="CX"></xsl:call-template>
             </xsl:for-each>
         </FIELD.4>
 
         <FIELD.5>
             <xsl:for-each select="PatientName">
                 <xsl:call-template name="XPN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.5>
     
         <FIELD.6>
             <xsl:for-each select="MothersMaindenName">
                  <xsl:call-template name="XPN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.6>
     
         <FIELD.7>
             <xsl:if test="count(@DTOfBirth) > 0">
                 <FIELD_ITEM><xsl:value-of select="@DTOfBirth"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.7>
     
         <FIELD.8>
             <xsl:if test="count(@Sex) > 0">
                 <FIELD_ITEM><xsl:value-of select="@Sex"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.8>
     
         <FIELD.9>
             <xsl:for-each select="PatientAlias">
                  <xsl:call-template name="XPN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.9>
     
         <FIELD.10>
             <xsl:for-each select="Race">
                  <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.10>
     
         <FIELD.11>
             <xsl:for-each select="PatientAddress">
                  <xsl:call-template name="XAD"></xsl:call-template>
             </xsl:for-each>
         </FIELD.11>
     
         <FIELD.12>
             <xsl:if test="count(@CountryCode) > 0">
                 <FIELD_ITEM><xsl:value-of select="@CountryCode"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.12>
     
         <FIELD.13>
             <xsl:for-each select="PhoneNumber-Home">
                  <xsl:call-template name="XTN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.13>
     
         <FIELD.14>
             <xsl:for-each select="PhoneNumber-Business">
                 <xsl:call-template name="XTN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.14>
     
         <FIELD.15>
             <xsl:for-each select="PrimaryLanguage">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.15>
     
         <FIELD.16>
             <xsl:for-each select="MaritalStatus">
                  <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.16>
     
         <FIELD.17>
             <xsl:for-each select="Religion">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.17>
     
         <FIELD.18>
             <xsl:for-each select="PatientAccountNumber">
                  <xsl:call-template name="CX"></xsl:call-template>
             </xsl:for-each>
         </FIELD.18>
     
         <FIELD.19>
             <xsl:if test="count(@SSNNumber-Patient ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@SSNNumber-Patient"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.19>
     
         <FIELD.20>
			 <xsl:for-each select="DriversLicenseNumber-Patient">
				 <xsl:call-template name="DLN"></xsl:call-template>
			 </xsl:for-each>            
         </FIELD.20>
     
         <FIELD.21>
             <xsl:for-each select="MothersIdentifier">
                 <xsl:call-template name="CX"></xsl:call-template>
             </xsl:for-each>
         </FIELD.21>
      
         <FIELD.22>
             <xsl:for-each select="EthnicGroup">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.22>
     
         <FIELD.23>
             <xsl:if test="count(@BirthPlace) > 0">
                 <FIELD_ITEM><xsl:value-of select="@BirthPlace"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.23>
     
         <FIELD.24>
             <xsl:if test="count(@MultipleBirthIndicator) > 0">
                 <FIELD_ITEM><xsl:value-of select="@MultipleBirthIndicator"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.24>
     
         <FIELD.25>
             <xsl:if test="count(@BirthOrder) > 0">
                 <FIELD_ITEM><xsl:value-of select="@BirthOrder"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.25>
    
         <FIELD.26>
             <xsl:for-each select="Citizenship">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.26>
     
         <FIELD.27>
             <xsl:for-each select="VeteransMilitaryStatus">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.27>
     
         <FIELD.28>
             <xsl:for-each select="Nationality">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.28>
     
         <FIELD.29>
             <xsl:if test="count(@PatientDeathDateandTime ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@PatientDeathDateandTime"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.29>
     
         <FIELD.30>
             <xsl:if test="count(@PatientDeathIndicator ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@PatientDeathIndicator"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.30>
     
         <FIELD.31>
             <xsl:if test="count(@IdentityUnknownIndicator ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@IdentityUnknownIndicator"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.31>
         
         <FIELD.32>
             <xsl:for-each select="IdentityReliabilityCode">
                 <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
             </xsl:for-each>
         </FIELD.32>
              
         <FIELD.33>
         <xsl:if test="count(@LastUpdateDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@LastUpdateDT"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.33>
     
         <FIELD.34>
			 <xsl:for-each select="LastUpdateFacility">
				 <xsl:call-template name="HD"></xsl:call-template>
			 </xsl:for-each>             
         </FIELD.34>
     
         <FIELD.35>
             <xsl:for-each select="SpeciesCode">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.35>
     
         <FIELD.36>
             <xsl:for-each select="BreedCode">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.36>
     
         <FIELD.37>
             <xsl:if test="count(@Strain ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@Strain"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.37>
     
         <FIELD.38>
             <xsl:for-each select="ProductionClassCode">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.38>
     
         <FIELD.39>
             <xsl:for-each select="TribalCitizenship">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.39>
  </PID>
  
  </xsl:template>

</xsl:stylesheet>

