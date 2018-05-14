<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="QAK">

  <QAK>
     <FIELD.1>
         <xsl:if test="count(@QueryTag ) > 0">
             <FIELD_ITEM><xsl:value-of select="@QueryTag"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@QueryResponseStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@QueryResponseStatus"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>
     
     <FIELD.3>
             <xsl:for-each select="MessageQueryName">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.3>
         
    <FIELD.4>
         <xsl:if test="count(@HitCount ) > 0">
             <FIELD_ITEM><xsl:value-of select="@HitCount"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
          <FIELD.5>
         <xsl:if test="count(@thisPayload ) > 0">
             <FIELD_ITEM><xsl:value-of select="@thisPayload"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.5>


     <FIELD.6>
         <xsl:if test="count(@HitsRemaining ) > 0">
             <FIELD_ITEM><xsl:value-of select="@HitsRemaining"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.6>


  </QAK>
  
  </xsl:template>

</xsl:stylesheet>

