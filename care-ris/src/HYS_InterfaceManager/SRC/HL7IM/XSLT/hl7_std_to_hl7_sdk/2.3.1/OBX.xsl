<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

<!-- here is the template that does the replacement -->
<xsl:template name="replaceCharsInString">
  <xsl:param name="stringIn"/>
  <xsl:param name="charsIn"/>
  <xsl:param name="charsOut"/>
  <xsl:choose>
    <xsl:when test="contains($stringIn,$charsIn)">
      <xsl:value-of select="concat(substring-before($stringIn,$charsIn),$charsOut)"/>
      <xsl:call-template name="replaceCharsInString">
        <xsl:with-param name="stringIn" select="substring-after($stringIn,$charsIn)"/>
        <xsl:with-param name="charsIn" select="$charsIn"/>
        <xsl:with-param name="charsOut" select="$charsOut"/>
      </xsl:call-template>
    </xsl:when>
    <xsl:otherwise>
      <xsl:value-of select="$stringIn"/>
    </xsl:otherwise>
  </xsl:choose>
</xsl:template>
  
  <xsl:template name="OBX">
  
  <OBX>
     <FIELD.1>
         <FIELD_ITEM><xsl:value-of select="@SetID-OBX"/></FIELD_ITEM>
     </FIELD.1>
     
     <FIELD.2>
         <FIELD_ITEM><xsl:value-of select="@ValueType"/></FIELD_ITEM>
     </FIELD.2>
     
     <FIELD.3>
		 <xsl:for-each select="ObservationIdentifier">
			 <xsl:call-template name="CE"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.3>
     
     <FIELD.4>
         <FIELD_ITEM><xsl:value-of select="@ObservationSubID"/></FIELD_ITEM>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="ObservationValue">
             <FIELD_ITEM>
		<!--<xsl:value-of select="."/>-->

  <xsl:variable name="myString" select="."/>
  <xsl:variable name="myNewString">
    <xsl:call-template name="replaceCharsInString">
      <xsl:with-param name="stringIn" select="string($myString)"/>
      <xsl:with-param name="charsIn" select="'&amp;'"/>
      <xsl:with-param name="charsOut" select="'\T\'"/>
    </xsl:call-template>
  </xsl:variable>
  <xsl:value-of select="$myNewString"/>

	     </FIELD_ITEM>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
<xsl:for-each select="Units">
			 <xsl:call-template name="CE"></xsl:call-template>
		 </xsl:for-each>         
         
     </FIELD.6>
     
     <FIELD.7>
         <FIELD_ITEM><xsl:value-of select="@ReferencesRange"/></FIELD_ITEM>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:for-each select="AbnormalFlags">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.8>
     
     <FIELD.9>
         <FIELD_ITEM><xsl:value-of select="@Probability"/></FIELD_ITEM>
     </FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="NatureOfAbnormalTest">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.10>
     
     <FIELD.11>
         <FIELD_ITEM><xsl:value-of select="@ObservResultsStatus"/></FIELD_ITEM>
     </FIELD.11>
     
     <FIELD.12>
         <FIELD_ITEM><xsl:value-of select="@DateLastObsNormalValues"/></FIELD_ITEM>
     </FIELD.12>
     
     <FIELD.13>
         <FIELD_ITEM><xsl:value-of select="@UserDefinedAccessChecks"/></FIELD_ITEM>
     </FIELD.13>
     
     <FIELD.14>
         <FIELD_ITEM><xsl:value-of select="@DROfTheObservation"/></FIELD_ITEM>
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
       
  </OBX>
  
  </xsl:template>

</xsl:stylesheet>

