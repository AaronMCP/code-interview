<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="ZE1">

  <ZE1>
     <FIELD.1>
         <xsl:if test="count(@SetID-ZE1 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetID-ZE1"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@ControlCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ControlCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="Technical">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>   
     
     <FIELD.4>
         <xsl:if test="count(@TechnicalCount ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TechnicalCount"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="MedicalAddtion">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:for-each select="MedicalWorkClass">
             <xsl:call-template name="JCC"></xsl:call-template>
         </xsl:for-each>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:for-each select="MedicalWork">
				<xsl:call-template name="XCN"></xsl:call-template>
             
         </xsl:for-each>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:if test="count(@UserCommodityClass ) > 0">
             <FIELD_ITEM><xsl:value-of select="@UserCommodityClass"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.8>
     
     <FIELD.9>
         <xsl:for-each select="UserCommodity">
             <xsl:call-template name="ZRD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="Contact">
                 <xsl:call-template name="XTN"></xsl:call-template>
             </xsl:for-each>
     </FIELD.10>
     
     <FIELD.11>
         <xsl:if test="count(@OperationField ) > 0">
             <FIELD_ITEM><xsl:value-of select="@OperationField"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.11>
     
     <FIELD.12>
         <xsl:if test="count(@AccountField ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AccountField"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.12>
     
  </ZE1>
  
  </xsl:template>

</xsl:stylesheet>

