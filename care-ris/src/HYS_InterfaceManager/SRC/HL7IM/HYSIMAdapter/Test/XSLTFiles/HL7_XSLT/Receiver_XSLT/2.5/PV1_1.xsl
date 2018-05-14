<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="PV1">

  <PV1>
         <xsl:if test="FIELD.1/FIELD_ITEM != ''">
			 <xsl:attribute name="SetID-PV1"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
			 <xsl:attribute name="PatientClass"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.4/FIELD_ITEM != ''">
			 <xsl:attribute name="AdmissionType"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.10/FIELD_ITEM != ''">
			 <xsl:attribute name="HospitalService"><xsl:value-of select="FIELD.10/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.12/FIELD_ITEM != ''">
			 <xsl:attribute name="PreadmitTestIndicator"><xsl:value-of select="FIELD.12/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.13/FIELD_ITEM != ''">
			 <xsl:attribute name="ReadmissionIndicator"><xsl:value-of select="FIELD.13/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.14/FIELD_ITEM != ''">
			 <xsl:attribute name="AdmitSource"><xsl:value-of select="FIELD.14/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.16/FIELD_ITEM != ''">
			 <xsl:attribute name="VIPIndicator"><xsl:value-of select="FIELD.16/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.18/FIELD_ITEM != ''">
			 <xsl:attribute name="PatientType"><xsl:value-of select="FIELD.18/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.21/FIELD_ITEM != ''">
			 <xsl:attribute name="ChargePriceIndicator"><xsl:value-of select="FIELD.21/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.22/FIELD_ITEM != ''">
			 <xsl:attribute name="CourtesyCode"><xsl:value-of select="FIELD.22/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.23/FIELD_ITEM != ''">
			 <xsl:attribute name="CreditRating"><xsl:value-of select="FIELD.23/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.24/FIELD_ITEM != ''">
			 <xsl:attribute name="ContractCode"><xsl:value-of select="FIELD.24/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.25/FIELD_ITEM != ''">
			 <xsl:attribute name="ContractEffectiveDate"><xsl:value-of select="FIELD.25/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.26/FIELD_ITEM != ''">
			 <xsl:attribute name="ContractAmount"><xsl:value-of select="FIELD.26/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.27/FIELD_ITEM != ''">
			 <xsl:attribute name="ContractPeriod"><xsl:value-of select="FIELD.27/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.28/FIELD_ITEM != ''">
			 <xsl:attribute name="InterestCode"><xsl:value-of select="FIELD.28/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.29/FIELD_ITEM != ''">
			 <xsl:attribute name="TransfertoBadDebtCode"><xsl:value-of select="FIELD.29/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.30/FIELD_ITEM != ''">
	         <xsl:attribute name="TransfertoBadDebtDate"><xsl:value-of select="FIELD.30/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.31/FIELD_ITEM != ''">
			 <xsl:attribute name="BadDebtAgencyCode"><xsl:value-of select="FIELD.31/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.32/FIELD_ITEM != ''">
	         <xsl:attribute name="BadDebtTransferAmount"><xsl:value-of select="FIELD.32/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.33/FIELD_ITEM != ''">
	         <xsl:attribute name="BadDebtRecoveryAmount"><xsl:value-of select="FIELD.33/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.34/FIELD_ITEM != ''">
			 <xsl:attribute name="DeleteAcountIndicator"><xsl:value-of select="FIELD.34/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.35/FIELD_ITEM != ''">
	         <xsl:attribute name="DeleteAccountDate"><xsl:value-of select="FIELD.35/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.36/FIELD_ITEM != ''">
			 <xsl:attribute name="DischargeDisposition"><xsl:value-of select="FIELD.36/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.39/FIELD_ITEM != ''">
			 <xsl:attribute name="ServicingFacility"><xsl:value-of select="FIELD.39/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.40/FIELD_ITEM != ''">
			 <xsl:attribute name="BedStatus"><xsl:value-of select="FIELD.40/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.41/FIELD_ITEM != ''">
			 <xsl:attribute name="AcountStatus"><xsl:value-of select="FIELD.41/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.44/FIELD_ITEM != ''">
			 <xsl:attribute name="AdmitDT"><xsl:value-of select="FIELD.44/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.45/FIELD_ITEM != ''">
	             <xsl:attribute name="DischargeDT"><xsl:value-of select="FIELD.45/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.46/FIELD_ITEM != ''">
			 <xsl:attribute name="CurrentPatientBalance"><xsl:value-of select="FIELD.46/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.47/FIELD_ITEM != ''">
			 <xsl:attribute name="TotalCharges"><xsl:value-of select="FIELD.47/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.48/FIELD_ITEM != ''">
			 <xsl:attribute name="TotalAdjustments"><xsl:value-of select="FIELD.48/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.49/FIELD_ITEM != ''">
			 <xsl:attribute name="TotalPayments"><xsl:value-of select="FIELD.49/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:if test="FIELD.51/FIELD_ITEM != ''">
			 <xsl:attribute name="VisitIndicator"><xsl:value-of select="FIELD.51/FIELD_ITEM"/></xsl:attribute>
	     </xsl:if>
	     
	     <xsl:for-each select="FIELD.15/FIELD_ITEM">
			 <xsl:element name="AmbulatoryStatus"><xsl:value-of select="."/></xsl:element>
	     </xsl:for-each>
	     
	     <xsl:if test="FIELD.3/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.3/FIELD_ITEM">
		             <xsl:element name="AssignedPatientLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.5/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.5/FIELD_ITEM">
		             <xsl:element name="PreadmitNumber">
		                 <xsl:call-template name="CX"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		     
		 <xsl:if test="FIELD.6/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.6/FIELD_ITEM">
		             <xsl:element name="PriorPatientLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
         
         <xsl:if test="FIELD.7/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.7/FIELD_ITEM">
		             <xsl:element name="AttendingDoctor">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.8/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.8/FIELD_ITEM">
		             <xsl:element name="ReferringDoctor">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.9/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.9/FIELD_ITEM">
					 <xsl:element name="ConsultingDoctor">
		             <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
         
         <xsl:call-template name="PV1_2"/>
         
  </PV1>
  
  </xsl:template>
  
  <xsl:include href = "./PV1_2.xsl"/>

</xsl:stylesheet>

