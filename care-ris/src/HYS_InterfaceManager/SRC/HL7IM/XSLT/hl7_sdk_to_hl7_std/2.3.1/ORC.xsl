<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="ORC">

  <ORC>
         <xsl:if test="FIELD.1/FIELD_ITEM != ''">
			 <xsl:attribute name="OrderControl"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.5/FIELD_ITEM != ''">
			 <xsl:attribute name="OrderStatus"><xsl:value-of select="FIELD.5/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.6/FIELD_ITEM != ''">
			 <xsl:attribute name="ResponseFlag"><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.9/FIELD_ITEM != ''">
			 <xsl:attribute name="DTOfTransaction"><xsl:value-of select="FIELD.9/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.15/FIELD_ITEM != ''">
			 <xsl:attribute name="OrderEffectiveDT"><xsl:value-of select="FIELD.15/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
		     <xsl:for-each select="FIELD.2/FIELD_ITEM">
                 <xsl:element name="PlacerOrderNumber">
                     <xsl:call-template name="EI"></xsl:call-template>
                 </xsl:element>
             </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.3/FIELD_ITEM != ''">
		     <xsl:for-each select="FIELD.3/FIELD_ITEM">
                 <xsl:element name="FillerOrderNumber">
                     <xsl:call-template name="EI"></xsl:call-template>
                 </xsl:element>
             </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.4/FIELD_ITEM != ''">
		     <xsl:for-each select="FIELD.4/FIELD_ITEM">
                 <xsl:element name="PlacerGroupNumber">
                     <xsl:call-template name="EI"></xsl:call-template>
                 </xsl:element>
             </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.7/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.7/FIELD_ITEM">
		             <xsl:element name="QuantityTiming">
		                 <xsl:call-template name="TQ"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.8/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.8/FIELD_ITEM">
		             <xsl:element name="Parent">
		                <xsl:call-template name="EIP"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.10/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.10/FIELD_ITEM">
		             <xsl:element name="EnteredBy">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.11/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.11/FIELD_ITEM">
		             <xsl:element name="VerifiedBy">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.12/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.12/FIELD_ITEM">
		             <xsl:element name="OrderingProvider">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.13/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.13/FIELD_ITEM">
		             <xsl:element name="EnterersLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.14/FIELD_ITEM != ''">
                 <xsl:for-each select="OBR/FIELD.14/FIELD_ITEM">
                     <xsl:element name="CallBackPhoneNumber">
                         <xsl:call-template name="XTN"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
             </xsl:if>
             
             <xsl:if test="FIELD.16/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.16/FIELD_ITEM">
                     <xsl:element name="OrderControlCodeReason">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.17/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.17/FIELD_ITEM">
                     <xsl:element name="EnteringOrganization">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.18/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.18/FIELD_ITEM">
                     <xsl:element name="EnteringDevice">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
             <xsl:if test="FIELD.19/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.19/FIELD_ITEM">
		             <xsl:element name="ActionBy">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.20/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.20/FIELD_ITEM">
                     <xsl:element name="AdvancedBeneficiaryNoticeCode">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
  </ORC>
  
  </xsl:template>


</xsl:stylesheet>

