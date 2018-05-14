<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="OBR">
  
  <OBR>
     <FIELD.1>
         <FIELD_ITEM><xsl:value-of select="@SetID-OBR"/></FIELD_ITEM>
     </FIELD.1>
     
     <FIELD.2>
		<xsl:for-each select="PlacerOrderNumber">
			<xsl:call-template name="EI"></xsl:call-template>
		</xsl:for-each>
        
     </FIELD.2>
     
     <FIELD.3>
     <xsl:for-each select="FillerOrderNumber">
			<xsl:call-template name="EI"></xsl:call-template>
		</xsl:for-each>
        
     </FIELD.3>
     
     <FIELD.4>
     <xsl:for-each select="UniversalServiceID">
			<xsl:call-template name="CE"></xsl:call-template>
		</xsl:for-each>
     </FIELD.4>
     
     <FIELD.5>
         <FIELD_ITEM><xsl:value-of select="@Priority"/></FIELD_ITEM>
     </FIELD.5>
     
     <FIELD.6>
         <FIELD_ITEM><xsl:value-of select="@RequestedDT"/></FIELD_ITEM>
     </FIELD.6>
     
     <FIELD.7>
         <FIELD_ITEM><xsl:value-of select="@ObservationDT"/></FIELD_ITEM>
     </FIELD.7>
     
     <FIELD.8>
         <FIELD_ITEM><xsl:value-of select="@ObservationEndDT"/></FIELD_ITEM>
     </FIELD.8>
     
     <FIELD.9>
     <xsl:for-each select="CollectionVolume">
			<xsl:call-template name="CQ"></xsl:call-template>
		</xsl:for-each>
         
     </FIELD.9>
     
     <FIELD.10>
         <xsl:for-each select="CollectorIdentifier">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.10>
     
     <FIELD.11>
         <FIELD_ITEM><xsl:value-of select="@SpecimenActionCode"/></FIELD_ITEM>
     </FIELD.11>
     
     <FIELD.12>
     <xsl:for-each select="DangerCode">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
         
     </FIELD.12>
     
     <FIELD.13>
         <FIELD_ITEM><xsl:value-of select="@RelevantClinicalInfo"/></FIELD_ITEM>
     </FIELD.13>
     
     <FIELD.14>
         <FIELD_ITEM><xsl:value-of select="@SpecimenReceivedDT"/></FIELD_ITEM>
     </FIELD.14>
     
     <FIELD.15>
         <xsl:for-each select="SpecimenSource">
             <xsl:call-template name="SPS"></xsl:call-template>
         </xsl:for-each>
     </FIELD.15>
     
     <FIELD.16>
         <xsl:for-each select="OrderingProvider">
		 <xsl:call-template name="XCN"></xsl:call-template>           
             
         </xsl:for-each>
     </FIELD.16>
     
     <FIELD.17>
         <xsl:for-each select="OrderCallbackPhoneNumber">
              <xsl:call-template name="XTN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.17>
     
     <FIELD.18>
         <FIELD_ITEM><xsl:value-of select="@PlacerField1"/></FIELD_ITEM>
     </FIELD.18>
     
     <FIELD.19>
         <FIELD_ITEM><xsl:value-of select="@PlacerField2"/></FIELD_ITEM>
     </FIELD.19>
     
     <FIELD.20>
         <FIELD_ITEM><xsl:value-of select="@FillerField1"/></FIELD_ITEM>
     </FIELD.20>
     
     <FIELD.21>
         <FIELD_ITEM><xsl:value-of select="@FillerField2"/></FIELD_ITEM>
     </FIELD.21>
     
     <FIELD.22>
         <FIELD_ITEM><xsl:value-of select="@ResultsRptStatusChng-DT"/></FIELD_ITEM>
     </FIELD.22>
     
     <FIELD.23>
         <xsl:for-each select="ChargeToPractice">
             <xsl:call-template name="MOC"></xsl:call-template>
         </xsl:for-each>
     </FIELD.23>
     
     <FIELD.24>
         <FIELD_ITEM><xsl:value-of select="@DiagnosticServSectID"/></FIELD_ITEM>
     </FIELD.24>
     
     <FIELD.25>
         <FIELD_ITEM><xsl:value-of select="@ResultStatus"/></FIELD_ITEM>
     </FIELD.25>
     
     <FIELD.26>
         <xsl:for-each select="ParentResult">
             <xsl:call-template name="PRL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.26>
     
     <FIELD.27>
         <xsl:for-each select="QuantityTiming">
         <xsl:call-template name="TQ"></xsl:call-template>
         </xsl:for-each>
     </FIELD.27>
     
     <FIELD.28>
         <xsl:for-each select="ResultCopiesTo">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.28>
     
     <FIELD.29>
         <xsl:for-each select="Parent">
			 <xsl:call-template name="CM"></xsl:call-template>
         </xsl:for-each>
     </FIELD.29>
     
     <FIELD.30>
         <FIELD_ITEM><xsl:value-of select="@TransportationMode"/></FIELD_ITEM>
     </FIELD.30>
     
     <FIELD.31>
         <xsl:for-each select="ReasonForStudy">
         <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.31>
     
     <FIELD.32>
         <xsl:for-each select="PrincipleResultInterpreter">
             <xsl:call-template name="NDL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.32>
     
     <FIELD.33>
         <xsl:for-each select="AssistantResultInterpreter">
             <xsl:call-template name="NDL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.33>
     
     <FIELD.34>
         <xsl:for-each select="Technician">
             <xsl:call-template name="NDL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.34>
     
     <FIELD.35>
         <xsl:for-each select="Transcriptionist">
             <xsl:call-template name="NDL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.35>
     
     <FIELD.36>
         <FIELD_ITEM><xsl:value-of select="@ScheduledDT"/></FIELD_ITEM>
     </FIELD.36>
     
     <FIELD.37>
         <FIELD_ITEM><xsl:value-of select="@NumberofSampleContainers"/></FIELD_ITEM>
     </FIELD.37>
     
     <FIELD.38>
         <xsl:for-each select="TransportLogisticsOfCollectedSample">
         <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.38>
     
     <FIELD.39>
         <xsl:for-each select="CollectorsComment">
         <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.39>
     
     <FIELD.40>
     <xsl:for-each select="TransportArrangementResponsibility">
         <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>     
     </FIELD.40>
     
     <FIELD.41>
         <FIELD_ITEM><xsl:value-of select="@TransportArranged"/></FIELD_ITEM>
     </FIELD.41>
     
     <FIELD.42>
         <FIELD_ITEM><xsl:value-of select="@EscortRequired"/></FIELD_ITEM>
     </FIELD.42>
     
     <FIELD.43>
         <xsl:for-each select="PlannedPatientTransportComment">
         <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.43>
     
     <FIELD.44>
         <xsl:for-each select="OrderingFacilityName">
             <xsl:call-template name="XON"></xsl:call-template>
         </xsl:for-each>
     </FIELD.44>
     
     <FIELD.45>
         <xsl:for-each select="OrderingFacilityAddress">
             <xsl:call-template name="XAD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.45>
     
     <FIELD.46>
         <xsl:for-each select="OrderingFacilityPhoneNumber">
             <xsl:call-template name="XTN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.46>
     
     <FIELD.47>
         <xsl:for-each select="ORC/OrderingProviderAddress">
             <xsl:call-template name="XAD"></xsl:call-template>
         </xsl:for-each>
     </FIELD.47>
     
  </OBR>
  
  </xsl:template>

</xsl:stylesheet>

