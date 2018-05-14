<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

<xsl:template match="/">
     <xsl:apply-templates/>
</xsl:template>
 
 <xsl:template match="/*">     
     <!--<xsl:strip-space elements="*"/>-->     
     <HL7>
         <xsl:call-template name="MSH"/>
         <xsl:for-each select="MSH/following-sibling::*">  
             <xsl:choose>
                 <xsl:when test="name() = 'EVN'">
                     <xsl:call-template name="EVN"/>
                 </xsl:when>
                 <xsl:when test="name() = 'NTE'">
                     <xsl:call-template name="NTE"/>
                 </xsl:when>
                 <xsl:when test="name() = 'PID'">
                     <xsl:call-template name="PID"/>
                 </xsl:when>
                 <xsl:when test="name() = 'PV1'">
                     <xsl:call-template name="PV1"/>
                 </xsl:when>
                 <xsl:when test="name() = 'MRG'">
                     <xsl:call-template name="MRG"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ORC'">
                     <xsl:call-template name="ORC"/>
                 </xsl:when>
                 <xsl:when test="name() = 'OBR'">
                     <xsl:call-template name="OBR"/>
                 </xsl:when>
                 <xsl:when test="name() = 'OBX'">
                     <xsl:call-template name="OBX"/>
                 </xsl:when>
                 <xsl:when test="name() = 'TQ1'">
                     <xsl:call-template name="TQ1"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ZE1'">
                     <xsl:call-template name="ZE1"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ZE2'">
                     <xsl:call-template name="ZE2"/>
                 </xsl:when>
                 <xsl:when test="name() = 'IPC'">
                     <xsl:call-template name="IPC"/>
                 </xsl:when>
                 <xsl:when test="name() = 'QPD'">
                     <xsl:call-template name="QPD"/>
                 </xsl:when>
                 <xsl:when test="name() = 'RCP'">
                     <xsl:call-template name="RCP"/>
                 </xsl:when>
                 <xsl:when test="name() = 'MSA'">
                     <xsl:call-template name="MSA"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ERR'">
                     <xsl:call-template name="ERR"/>
                 </xsl:when>
             </xsl:choose>
         </xsl:for-each>   
     </HL7> 
     
 </xsl:template>
  
<xsl:include href = "./MSH.xsl"/>
 <xsl:include href = "./PID.xsl"/>
 <xsl:include href = "./PV1.xsl"/>
 <xsl:include href = "./EVN.xsl"/>
 <xsl:include href = "./NTE.xsl"/>
 <xsl:include href = "./ORC.xsl"/>
 <xsl:include href = "./OBR.xsl"/>
 <xsl:include href = "./OBX.xsl"/>
 <xsl:include href = "./TQ1.xsl"/>
 <xsl:include href = "./ZE1.xsl"/>
 <xsl:include href = "./ZE2.xsl"/>
 <xsl:include href = "./IPC.xsl"/>
 <xsl:include href = "./MRG.xsl"/>
 <xsl:include href = "./QPD.xsl"/>
 <xsl:include href = "./RCP.xsl"/>
 <xsl:include href = "./MSA.xsl"/>
 <xsl:include href = "./ERR.xsl"/>
<xsl:include href="./Data_Type.xsl"></xsl:include>

</xsl:stylesheet>