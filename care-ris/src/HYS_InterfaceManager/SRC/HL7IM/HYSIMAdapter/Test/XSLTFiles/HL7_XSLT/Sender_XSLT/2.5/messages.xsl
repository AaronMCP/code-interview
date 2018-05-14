<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

<xsl:strip-space elements="*"/>

<xsl:template name="ACK">
     <ADT_ACK>
         <xsl:call-template name="Segments"/> 
     </ADT_ACK>
</xsl:template>

<xsl:template name="ADT_A01">
     <ADT_A01>
         <xsl:call-template name="Segments"/> 
     </ADT_A01>
</xsl:template>

<xsl:template name="ADT_A02">
     <ADT_A02>
         <xsl:call-template name="Segments"/> 
     </ADT_A02>
</xsl:template>

<xsl:template name="ADT_A03">
     <ADT_A03>
         <xsl:call-template name="Segments"/> 
     </ADT_A03>
</xsl:template>

<xsl:template name="ADT_A04">
     <ADT_A04>
         <xsl:call-template name="Segments"/> 
     </ADT_A04>
</xsl:template>

<xsl:template name="ADT_A06">
     <ADT_A06>
         <xsl:call-template name="Segments"/> 
     </ADT_A06>
</xsl:template>

<xsl:template name="ADT_A07">
     <ADT_A07>
         <xsl:call-template name="Segments"/> 
     </ADT_A07>
</xsl:template>

<xsl:template name="ADT_A08">
     <ADT_A08>
         <xsl:call-template name="Segments"/> 
     </ADT_A08>
</xsl:template>

<xsl:template name="ADT_A11">
     <ADT_A11>
         <xsl:call-template name="Segments"/> 
     </ADT_A11>
</xsl:template>

<xsl:template name="ADT_A12">
     <ADT_A12>
         <xsl:call-template name="Segments"/> 
     </ADT_A12>
</xsl:template>

<xsl:template name="ADT_A13">
     <ADT_A13>
         <xsl:call-template name="Segments"/> 
     </ADT_A13>
</xsl:template>

<xsl:template name="ADT_A31">
     <ADT_A31>
         <xsl:call-template name="Segments"/> 
     </ADT_A31>
</xsl:template>

<xsl:template name="ADT_A40">
     <ADT_A40>
         <xsl:call-template name="Segments"/> 
     </ADT_A40>
</xsl:template>

<xsl:template name="ORM_O01">
     <ORM_O01>
         <xsl:call-template name="Segments"/>  
     </ORM_O01>
</xsl:template>

<xsl:template name="OMG_O09">
     <OMG_O09>
         <xsl:call-template name="Segments"/>  
     </OMG_O09>
</xsl:template>

<xsl:template name="OMI_O23">
     <OMI_O23>
         <xsl:call-template name="Segments"/>  
     </OMI_O23>
</xsl:template>

<xsl:template name="ORU_R01">
     <ORU_R01>
         <xsl:call-template name="Segments"/>  
     </ORU_R01>
</xsl:template>

<xsl:template name="QBP_Q23">
     <QBP_Q23>
         <xsl:call-template name="Segments"/>  
     </QBP_Q23>
</xsl:template>

<xsl:template name="RSP_K23">
     <RSP_K23>
         <xsl:call-template name="Segments"/>  
     </RSP_K23>
</xsl:template>

<xsl:template name="QBP_Q22">
     <QBP_Q22>
         <xsl:call-template name="Segments"/>  
     </QBP_Q22>
</xsl:template>

<xsl:template name="RSP_K22">
     <RSP_K22>
         <xsl:call-template name="Segments"/>  
     </RSP_K22>
</xsl:template>

<xsl:template name="QBP_ZV1">
     <QBP_ZV1>
         <xsl:call-template name="Segments"/>  
     </QBP_ZV1>
</xsl:template>

<xsl:template name="RSP_ZV2">
     <RSP_ZV2>
         <xsl:call-template name="Segments"/>  
     </RSP_ZV2>
</xsl:template>


<xsl:template name="Segments">	
	<xsl:for-each select="MSH"><xsl:call-template name="MSH"/></xsl:for-each>
     
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
                 <xsl:when test="name() = 'MRG'">
                     <xsl:call-template name="MRG"/>
                 </xsl:when>
                 <xsl:when test="name() = 'QPD'">
					 <xsl:call-template name="QPD"></xsl:call-template>             
                 </xsl:when>                 
                 <xsl:when test="name() = 'RCP'">
                     <xsl:call-template name="RCP"/>
                 </xsl:when>
                 <xsl:when test="name() = 'QAK'">
                     <xsl:call-template name="QAK"/>
                 </xsl:when>
                 <xsl:when test="name() = 'DSC'">
                     <xsl:call-template name="DSC"/>
                 </xsl:when>
                 <xsl:when test="name() = 'MSA'">
                     <xsl:call-template name="MSA"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ERR'">
                     <xsl:call-template name="ERR"/>
                 </xsl:when>
                 <xsl:when test="name() = 'ZDS'">
                     <xsl:call-template name="ZDS"/>
                 </xsl:when>
             </xsl:choose>
     </xsl:for-each>   
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
 <xsl:include href = "./QAK.xsl"/>
 <xsl:include href = "./DSC.xsl"/>
 <xsl:include href = "./MSA.xsl"/>
 <xsl:include href = "./ERR.xsl"/>
 <xsl:include href = "./ZDS.xsl"/>
<xsl:include href = "./Data_Type.xsl"/>
</xsl:stylesheet>

