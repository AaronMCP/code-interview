<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="OBX">

  <OBX>
         <xsl:if test="FIELD.1/FIELD_ITEM != ''">
			 <xsl:attribute name="SetIDObservational"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
			 <xsl:attribute name="ValueType"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.4/FIELD_ITEM != ''">
			 <xsl:attribute name="ObservationSub-ID"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     	     
	     <xsl:if test="FIELD.7/FIELD_ITEM != ''">
			 <xsl:attribute name="ReferencesRange"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     	     
	     <xsl:if test="FIELD.9/FIELD_ITEM != ''">
			 <xsl:attribute name="Probability"><xsl:value-of select="FIELD.9/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.11/FIELD_ITEM != ''">
			 <xsl:attribute name="ObservResultStatus"><xsl:value-of select="FIELD.11/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.12/FIELD_ITEM != ''">
			 <xsl:attribute name="EffectiveDateOfReferenceRange"><xsl:value-of select="FIELD.12/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.13/FIELD_ITEM != ''">
			 <xsl:attribute name="UserDefinedAccessChecks"><xsl:value-of select="FIELD.13/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.14/FIELD_ITEM != ''">
			 <xsl:attribute name="DateTimeoftheObservation"><xsl:value-of select="FIELD.14/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.19/FIELD_ITEM != ''">
			 <xsl:attribute name="DTOftheAnalysis"><xsl:value-of select="FIELD.19/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.3/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.3/FIELD_ITEM">
                     <xsl:element name="ObservationIdentifier">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:for-each select="FIELD.5/FIELD_ITEM">
             <xsl:element name="ObservationValue">
                 <!--<xsl:for-each select="child::*">
                     <xsl:if test="count(*) > 0">
                         <xsl:for-each select="child::*">
                             <xsl:if test="position() != last()"><xsl:value-of select="."/>&amp;</xsl:if>
                             <xsl:if test="position() = last()"><xsl:value-of select="."/>^</xsl:if>
                         </xsl:for-each>
                     </xsl:if>
                     <xsl:if test="count(*) = 0">
                         <xsl:if test="position() != last()"><xsl:value-of select="."/>^</xsl:if>
                         <xsl:if test="position() = last()"><xsl:value-of select="."/></xsl:if>
                     </xsl:if>
                 </xsl:for-each>-->
                 <xsl:value-of select="."></xsl:value-of>
             </xsl:element>
	     </xsl:for-each>
	     
         <xsl:if test="FIELD.6/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.6/FIELD_ITEM">
                     <xsl:element name="Units">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
	     
	     <xsl:for-each select="FIELD.8/FIELD_ITEM">
			 <xsl:element name="AbnormalFlags"><xsl:value-of select="."/></xsl:element>
	     </xsl:for-each>
	     
	     <xsl:for-each select="FIELD.10/FIELD_ITEM">
			 <xsl:element name="NatureOfAbnormalTest"><xsl:value-of select="."/></xsl:element>
	     </xsl:for-each>
	     
         <xsl:if test="FIELD.15/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.15/FIELD_ITEM">
                     <xsl:element name="ProducersID">
                        <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.16/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.16/FIELD_ITEM">
		             <xsl:element name="ResponsibleObserver">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
         <xsl:if test="FIELD.17/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.17/FIELD_ITEM">
                     <xsl:element name="ObservationMethod">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>	 
         
         <xsl:if test="FIELD.18/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.18/FIELD_ITEM">
		             <xsl:element name="EquipmentInstanceIdentifier">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		        
  </OBX>
  
  </xsl:template>

</xsl:stylesheet>

