<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="IPC">

  <IPC>
     <FIELD.1>
         <xsl:for-each select="AccessionIdentifier">
             <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:for-each select="RequestedProcedureID">
             <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="StudyInstanceUID">
             <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>   
     
     <FIELD.4>
         <xsl:for-each select="ScheduledProcedureID">
            <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.4>
     
     <FIELD.5>
         <xsl:for-each select="Modality">
                <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:for-each select="ProtocolCode">
                  <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:for-each select="ScheduledStationName">
             <xsl:call-template name="EI"></xsl:call-template>
         </xsl:for-each>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:for-each select="ScheduledProcedureStepLocation">
                  <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.8>
     
     <FIELD.9>
         <xsl:if test="count(@ScheduledAETitle ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ScheduledAETitle"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.9>
     
  </IPC>
  
  </xsl:template>

</xsl:stylesheet>

