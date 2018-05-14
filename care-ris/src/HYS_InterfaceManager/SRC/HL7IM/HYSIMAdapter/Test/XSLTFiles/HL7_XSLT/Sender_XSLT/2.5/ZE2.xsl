<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="ZE2">

  <ZE2>
     <FIELD.1>
         <xsl:if test="count(@SetID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetID"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:for-each select="Voltage">
             <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="ElectricCurrent">
              <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>   
     
     <FIELD.4>
         <xsl:for-each select="Distance">
              <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="Time">
              <xsl:call-template name="CQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:if test="count(@Count ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Count"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:if test="count(@ShootField ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ShootField"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
     
  </ZE2>
  
  </xsl:template>

</xsl:stylesheet>

