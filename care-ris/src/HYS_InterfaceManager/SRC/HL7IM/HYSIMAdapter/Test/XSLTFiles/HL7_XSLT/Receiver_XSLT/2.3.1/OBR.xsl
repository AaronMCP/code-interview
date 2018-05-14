<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="OBR">

  <OBR>
         <xsl:if test="FIELD.1/FIELD_ITEM != ''">
			 <xsl:attribute name="SetID-OBR"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.5/FIELD_ITEM != ''">
			 <xsl:attribute name="Priority"><xsl:value-of select="FIELD.5/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.6/FIELD_ITEM != ''">
			 <xsl:attribute name="RequestedDT"><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.7/FIELD_ITEM != ''">
			 <xsl:attribute name="ObservationDT"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.8/FIELD_ITEM != ''">
			 <xsl:attribute name="ObservationEndDT"><xsl:value-of select="FIELD.8/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.11/FIELD_ITEM != ''">
			 <xsl:attribute name="SpecimenActionCode"><xsl:value-of select="FIELD.11/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.13/FIELD_ITEM != ''">
			 <xsl:attribute name="RelevantClinicalInfo"><xsl:value-of select="FIELD.13/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.14/FIELD_ITEM != ''">
			 <xsl:attribute name="SpecimenReceivedDT"><xsl:value-of select="FIELD.14/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.18/FIELD_ITEM != ''">
			 <xsl:attribute name="PlacerField1"><xsl:value-of select="FIELD.18/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.19/FIELD_ITEM != ''">
			 <xsl:attribute name="PlacerField2"><xsl:value-of select="FIELD.19/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.20/FIELD_ITEM != ''">
			 <xsl:attribute name="FillerField1"><xsl:value-of select="FIELD.20/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.21/FIELD_ITEM != ''">
			 <xsl:attribute name="FillerField2"><xsl:value-of select="FIELD.21/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.22/FIELD_ITEM != ''">
			 <xsl:attribute name="ResultsRptStatusChng-DT"><xsl:value-of select="FIELD.22/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.24/FIELD_ITEM != ''">
			 <xsl:attribute name="DiagnosticServSectID"><xsl:value-of select="FIELD.24/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.25/FIELD_ITEM != ''">
			 <xsl:attribute name="ResultStatus"><xsl:value-of select="FIELD.25/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.30/FIELD_ITEM != ''">
			 <xsl:attribute name="TransportationMode"><xsl:value-of select="FIELD.30/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.36/FIELD_ITEM != ''">
			 <xsl:attribute name="ScheduledDT"><xsl:value-of select="FIELD.36/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.37/FIELD_ITEM != ''">
			 <xsl:attribute name="NumberofSampleContainers"><xsl:value-of select="FIELD.37/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.41/FIELD_ITEM != ''">
			 <xsl:attribute name="TransportArranged"><xsl:value-of select="FIELD.41/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.42/FIELD_ITEM != ''">
			 <xsl:attribute name="EscortRequired"><xsl:value-of select="FIELD.42/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
		     <xsl:for-each select="FIELD.2/FIELD_ITEM">
                 <xsl:element name="PlacerOrderNumber">
                     <xsl:call-template name="EI"></xsl:call-template>
                 </xsl:element>
             </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.3/FIELD_ITEM != ''">
		     <xsl:for-each select="FIELD.3/FIELD_ITEM">
                 <xsl:element name="FillerOrderNumber">
                     <xsl:call-template name="EI"></xsl:call-template>
                 </xsl:element>
             </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.4/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.4/FIELD_ITEM">
                     <xsl:element name="UniversalServiceID">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
		 
		 <xsl:if test="FIELD.9/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.9/FIELD_ITEM">
		             <xsl:element name="CollectionVolume">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.10/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.10/FIELD_ITEM">
		             <xsl:element name="CollectorIdentifier">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.12/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.12/FIELD_ITEM">
                     <xsl:element name="DangerCode">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.15/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.15/FIELD_ITEM">
		             <xsl:element name="SpecimenSource">
		                 <xsl:call-template name="SPS"></xsl:call-template>
								
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
         <xsl:if test="FIELD.16/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.16/FIELD_ITEM">
		             <xsl:element name="OrderingProvider">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.17/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.17/FIELD_ITEM">
                     <xsl:element name="OrderCallbackPhoneNumber">
                         <xsl:call-template name="XTN"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
             </xsl:if>
         
         <xsl:if test="FIELD.23/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.23/FIELD_ITEM">
		             <xsl:element name="ChargeToPractice">
		                 <xsl:call-template name="MOC"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.26/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.26/FIELD_ITEM">
		             <xsl:element name="ParentResult">
		                 <xsl:call-template name="PRL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.27/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.27/FIELD_ITEM">
		             <xsl:element name="QuantityTiming">
		                 <xsl:call-template name="TQ"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.28/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.28/FIELD_ITEM">
		             <xsl:element name="ResultCopiesTo">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.29/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.29/FIELD_ITEM">
		             <xsl:element name="Parent">
		                 <xsl:call-template name="EIP"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
         <xsl:if test="FIELD.31/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.31/FIELD_ITEM">
                     <xsl:element name="ReasonForStudy">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.32/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.32/FIELD_ITEM">
		             <xsl:element name="PrincipleResultInterpreter">
		                 <xsl:call-template name="NDL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.33/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.33/FIELD_ITEM">
		             <xsl:element name="AssistantResultInterpreter">
		                 <xsl:call-template name="NDL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.34/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.34/FIELD_ITEM">
		             <xsl:element name="Technician">
		                 <xsl:call-template name="NDL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.35/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.35/FIELD_ITEM">
		             <xsl:element name="Transcriptionist">
		                 <xsl:call-template name="NDL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
         <xsl:if test="FIELD.38/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.38/FIELD_ITEM">
                     <xsl:element name="TransportLogisticsOfCollectedSample">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.39/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.39/FIELD_ITEM">
                     <xsl:element name="CollectorsComment">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.40/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.40/FIELD_ITEM">
                     <xsl:element name="TransportArrangementResponsibility">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.43/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.43/FIELD_ITEM">
                     <xsl:element name="PlannedPatientTransportComment">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.44/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.44/FIELD_ITEM">
		             <xsl:element name="OrderingFacilityName">
		                 <xsl:call-template name="XON"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.45/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.45/FIELD_ITEM">
		             <xsl:element name="OrderingFacilityAddress">
		                 <xsl:call-template name="XAD"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
         <xsl:if test="FIELD.46/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.46/FIELD_ITEM">
                     <xsl:element name="OrderingFacilityPhoneNumber">
                         <xsl:call-template name="XTN"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.47/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.47/FIELD_ITEM">
		             <xsl:element name="OrderingProviderAddress">
		                 <xsl:call-template name="XAD"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
  </OBR>
  
  </xsl:template>

</xsl:stylesheet>

