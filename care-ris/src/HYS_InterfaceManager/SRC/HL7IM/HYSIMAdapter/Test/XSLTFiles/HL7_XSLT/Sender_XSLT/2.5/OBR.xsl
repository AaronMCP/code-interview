<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="OBR">

  <OBR>
     <FIELD.1>
         <xsl:if test="count(@SetIDObservationRequest ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetIDObservationRequest"/></FIELD_ITEM>
         </xsl:if>
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
         <xsl:if test="count(@Priority ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Priority"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:if test="count(@RequestedDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@RequestedDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.6>
     
     <FIELD.7>
         <xsl:if test="count(@ObservationDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ObservationDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:if test="count(@ObservationEndDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ObservationEndDT"/></FIELD_ITEM>
         </xsl:if>
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
         <xsl:if test="count(@SpecimenActionCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SpecimenActionCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.11>
     
     <FIELD.12>
             <xsl:for-each select="DangerCode">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.12>
     
     <FIELD.13>
         <xsl:if test="count(@RelevantClinicalInfo. ) > 0">
             <FIELD_ITEM><xsl:value-of select="@RelevantClinicalInfo."/></FIELD_ITEM>
         </xsl:if>
     </FIELD.13>
     
     <FIELD.14>
         <xsl:if test="count(@SpecimenReceivedDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SpecimenReceivedDT"/></FIELD_ITEM>
         </xsl:if>
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
         <xsl:if test="count(@Placerfield1 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Placerfield1"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.18>
     
     <FIELD.19>
         <xsl:if test="count(@Placerfield2 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Placerfield2"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.19>
     
     <FIELD.20>
         <xsl:if test="count(@FillerField1 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@FillerField1"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.20>
     
     <FIELD.21>
         <xsl:if test="count(@FillerField2 ) > 0">
             <FIELD_ITEM><xsl:value-of select="@FillerField2"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.21>
     
     <FIELD.22>
         <xsl:if test="count(@ResultsRptStatusChngDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ResultsRptStatusChngDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.22>
     
     <FIELD.23>
         <xsl:for-each select="ChargetoPractice">
             <xsl:call-template name="MOC"></xsl:call-template>
         </xsl:for-each>
     </FIELD.23>
     
     <FIELD.24>
         <xsl:if test="count(@DiagnosticServSectID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DiagnosticServSectID"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.24>
     
     <FIELD.25>
         <xsl:if test="count(@ResultStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ResultStatus"/></FIELD_ITEM>
         </xsl:if>
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
         <xsl:for-each select="ParentNumber">
             <xsl:call-template name="EIP"></xsl:call-template>
         </xsl:for-each>
     </FIELD.29>
     
     <FIELD.30>
         <xsl:if test="count(@TransportationMode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TransportationMode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.30>
     
     <FIELD.31>
             <xsl:for-each select="ReasonforStudy">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.31>
     
     <FIELD.32>
         <xsl:for-each select="PrincipalResultInterpreter">
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
         <xsl:if test="count(@ScheduledDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ScheduledDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.36>
     
     <FIELD.37>
         <xsl:if test="count(@NumberOfSampleContainers ) > 0">
             <FIELD_ITEM><xsl:value-of select="@NumberOfSampleContainers"/></FIELD_ITEM>
         </xsl:if>
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
         <xsl:if test="count(@TransportArranged ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TransportArranged"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.41>
     
     <FIELD.42>
         <xsl:if test="count(@EscortRequired ) > 0">
             <FIELD_ITEM><xsl:value-of select="@EscortRequired"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.42>
     
     <FIELD.43>
             <xsl:for-each select="PlannedPatientTransportComment">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.43>
     
     <FIELD.44>
             <xsl:for-each select="ProcedureCode">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.44>
     
     <FIELD.45>
             <xsl:for-each select="ProcedureCodeModifier">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.45>
     
     <FIELD.46>
             <xsl:for-each select="PlacerSupplementalServiceInformation">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.46>
     
     <FIELD.47>
             <xsl:for-each select="FillerSupplementalServiceInformation">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.47>
     
     <FIELD.48>
             <xsl:for-each select="MedicallyNecessaryDuplicateProcedureReason">
                 <xsl:call-template name="CWE"></xsl:call-template>
             </xsl:for-each>
     </FIELD.48>
     
     <FIELD.49>
         <xsl:if test="count(@ResultHandling ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ResultHandling"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.49>
  </OBR>
  
  </xsl:template>

</xsl:stylesheet>

