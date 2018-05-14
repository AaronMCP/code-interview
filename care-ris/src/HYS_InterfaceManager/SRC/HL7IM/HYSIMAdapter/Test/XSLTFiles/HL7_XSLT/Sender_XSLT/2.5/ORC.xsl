<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="ORC">

  <ORC>
     <FIELD.1>
         <xsl:if test="count(@OrderControl ) > 0">
             <FIELD_ITEM><xsl:value-of select="@OrderControl"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:for-each select="PlacerOrderNumber"><xsl:call-template name="EI"></xsl:call-template> </xsl:for-each>            

     </FIELD.2>   
     
     <FIELD.3>
		 <xsl:for-each select="FillerOrderNumber">
			 <xsl:call-template name="EI"></xsl:call-template> 
		 </xsl:for-each>         
     </FIELD.3>   
     
     <FIELD.4>
		<xsl:for-each select="PlacerGroupNumber">
			 <xsl:call-template name="EI"></xsl:call-template> 
		 </xsl:for-each>   
         
     </FIELD.4>   
     
     <FIELD.5>
         <xsl:if test="count(@OrderStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@OrderStatus"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:if test="count(@ResponseFlag ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ResponseFlag"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:for-each select="QuantityTiming">
             <xsl:call-template name="TQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:for-each select="Parent">
             <xsl:call-template name="EIP"></xsl:call-template>
         </xsl:for-each>
     </FIELD.8>
     
     <FIELD.9>
         <xsl:if test="count(@DTOfTransaction ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DTOfTransaction"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="EnteredBy">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.10>
     
     <FIELD.11>
         <xsl:for-each select="VerifiedBy">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.11>
     
     <FIELD.12>
         <xsl:for-each select="OrderingProvider">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.12>
     
     <FIELD.13>
		 <xsl:for-each select="EnterersLocation">
		 <xsl:call-template name="PL"></xsl:call-template>
		 </xsl:for-each>
        
     </FIELD.13>
     
     <FIELD.14>
             <xsl:for-each select="CallBackPhoneNumber">
                 <xsl:call-template name="XTN"></xsl:call-template>
             </xsl:for-each>
     </FIELD.14>
     
     <FIELD.15>
         <xsl:if test="count(@OrderEffectiveDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@OrderEffectiveDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.15>
     
     <FIELD.16>
             <xsl:for-each select="OrderControlCodeReason">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.16>
     
     <FIELD.17>
             <xsl:for-each select="EnteringOrganization">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.17>
     
     <FIELD.18>
             <xsl:for-each select="EnteringDevice">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.18>
     
     <FIELD.19>
         <xsl:for-each select="ActionBy">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.19>  
     
     <FIELD.20>
             <xsl:for-each select="AdvancedBeneficiaryNoticeCode">
               <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.20>
     
     <FIELD.21>
         <xsl:for-each select="OrderingFacilityName">
            <xsl:call-template name="XON"></xsl:call-template>
         </xsl:for-each>
     </FIELD.21>
     
     <FIELD.22>
         <xsl:for-each select="OrderingFacilityAddress">
             <xsl:call-template name="XAD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.22>
     
     <FIELD.23>
             <xsl:for-each select="OrderingFacilityPhoneNumber">
                 <xsl:call-template name="XTN"></xsl:call-template>
             </xsl:for-each>
         </FIELD.23>
     
     <FIELD.24>
         <xsl:for-each select="OrderingProviderAddress">
             <xsl:call-template name="XAD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.24>
     
     <FIELD.25>
             <xsl:for-each select="OrderStatusModifier">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.25>
     
     <FIELD.26>
             <xsl:for-each select="AdvancedBeneficiaryNoticeOverrideReason">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.26>
     
     <FIELD.27>
         <xsl:if test="count(@FillersExpectedAvailabilityDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@FillersExpectedAvailabilityDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.27>
     
     <FIELD.28>
             <xsl:for-each select="ConfidentialityCode">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.28>
     
     <FIELD.29>
             <xsl:for-each select="OrderType">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.29>
     
     <FIELD.30>
             <xsl:for-each select="EntererAuthorizationMode">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.30>
     
  </ORC>
  
  </xsl:template>

</xsl:stylesheet>

