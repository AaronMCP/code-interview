<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="OBX">

  <OBX>
     <FIELD.1>
         <xsl:if test="count(@SetIDObservational ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetIDObservational"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@ValueType ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ValueType"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="ObservationIdentifier">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>   
     
     <FIELD.4>
         <xsl:if test="count(@ObservationSub-ID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ObservationSub-ID"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="ObservationValue">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:for-each select="Units">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:if test="count(@ReferencesRange ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ReferencesRange"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:for-each select="AbnormalFlags">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.8>
     
     <FIELD.9>
         <xsl:if test="count(@Probability ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Probability"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="NatureofAbnormalTest">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.10>
     
     <FIELD.11>
         <xsl:if test="count(@ObservResultStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ObservResultStatus"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.11>
     
     <FIELD.12>
         <xsl:if test="count(@EffectiveDateofReferenceRange ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EffectiveDateofReferenceRange"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.12>
     
     <FIELD.13>
         <xsl:if test="count(@UserDefinedAccessChecks ) > 0">
             <FIELD_ITEM><xsl:value-of select="@UserDefinedAccessChecks"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.13>
     
     <FIELD.14>
         <xsl:if test="count(@DateTimeoftheObservation ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DateTimeoftheObservation"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.14>
     
     <FIELD.15>
         <xsl:for-each select="ProducersID">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.15>
     
     <FIELD.16>
         <xsl:for-each select="ResponsibleObserver">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.16>
     
     <FIELD.17>
         <xsl:for-each select="ObservationMethod">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.17>
     
     <FIELD.18>
         <xsl:for-each select="EquipmentInstanceIdentifier">
             <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.18>
     
     <FIELD.19>
         <xsl:if test="count(@DTOftheAnalysis ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DTOftheAnalysis"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.19>     
  </OBX>
  
  </xsl:template>

</xsl:stylesheet>

