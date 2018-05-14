<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="TQ1">

  <TQ1>
     <FIELD.1>
         <xsl:if test="count(@SetID-TQ1 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetID-TQ1"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>    
     
     <FIELD.2>
         <xsl:for-each select="Quantity">
             <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="RepeatPattern">
             <xsl:call-template name="RPT"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>   
     
     <FIELD.4>
         <xsl:for-each select="ExplicitTime">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="RelativeTimeandUnits">
			 <xsl:call-template name="CQ"></xsl:call-template>
             
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:for-each select="ServiceDuration">
         <xsl:call-template name="CQ"></xsl:call-template>
            
         </xsl:for-each>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:if test="count(@StartDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@StartDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:if test="count(@EndDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EndDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.8>
     
     <FIELD.9>
             <xsl:for-each select="Priority">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.9>
     
     <FIELD.10>
         <xsl:if test="count(@ConditionText ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ConditionText"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.10>
     
     <FIELD.11>
         <xsl:if test="count(@TextInstruction ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TextInstruction"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.11>
     
     <FIELD.12>
         <xsl:if test="count(@Conjunction ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Conjunction"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.12>
     
     <FIELD.13>
         <xsl:for-each select="OccurrenceDuration">
             <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.13>
     
     <FIELD.14>
         <xsl:if test="count(@TotalOccurrences ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TotalOccurrences"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.14>     
  </TQ1>
  
  </xsl:template>

</xsl:stylesheet>

