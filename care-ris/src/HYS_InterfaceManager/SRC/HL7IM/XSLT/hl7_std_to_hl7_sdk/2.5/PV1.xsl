<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="PV1">

  <PV1>
     <FIELD.1>
         <xsl:if test="count(@SetIDPatientID ) > 0">
             <FIELD_ITEM><xsl:value-of select="@SetID-PV1"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.1>
     
     <FIELD.2>
         <xsl:if test="count(@PatientClass ) > 0">
             <FIELD_ITEM><xsl:value-of select="@PatientClass"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.2>
    
     <FIELD.3>
		 <xsl:for-each select="AssignedPatientLocation">
			 <xsl:call-template name="PL"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.3>
     
     <FIELD.4>
         <xsl:if test="count(@AdmissionType ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AdmissionType"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.4>
     
     <FIELD.5>
		 <xsl:for-each select="PreadmitNumber">
			 <xsl:call-template name="CX"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.5>
     
     <FIELD.6>
		 <xsl:for-each select="PriorPatientLocation">
			 <xsl:call-template name="PL"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.6>
     
     <FIELD.7>
         <xsl:for-each select="AttendingDoctor">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.7>
     
     <FIELD.8>
         <xsl:for-each select="ReferringDoctor">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.8>
     
     <FIELD.9>
         <xsl:for-each select="ConsultingDoctor">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.9>
    
     <FIELD.10>
         <xsl:if test="count(@HospitalService ) > 0">
             <FIELD_ITEM><xsl:value-of select="@HospitalService"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.10>
     
     <FIELD.11>
		 <xsl:for-each select="TemporaryLocation">
			 <xsl:call-template name="PL"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.11>
     
     <FIELD.12>
         <xsl:if test="count(@PreadmitTestIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@PreadmitTestIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.12>
     
     <FIELD.13>
         <xsl:if test="count(@Re-admissionIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@Re-admissionIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.13>
     
     <FIELD.14>
         <xsl:if test="count(@AdmitSource ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AdmitSource"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.14>
     
     <FIELD.15>
         <xsl:for-each select="AmbulatoryStatus">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.15>
     
     <FIELD.16>
         <xsl:if test="count(@VIPIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@VIPIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.16>
     
     <FIELD.17>
         <xsl:for-each select="AdmittingDoctor">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.17>
     
     <FIELD.18>
         <xsl:if test="count(@PatientType ) > 0">
             <FIELD_ITEM><xsl:value-of select="@PatientType"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.18>
     
     <FIELD.19>
		 <xsl:for-each select="VisitNumber">
			 <xsl:call-template name="CX"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.19>
     
     <FIELD.20>
         <xsl:for-each select="FinacialClass">
             <xsl:call-template name="FC"></xsl:call-template>
         </xsl:for-each>
     </FIELD.20>
     
     <FIELD.21>
         <xsl:if test="count(@ChargePriceIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ChargePriceIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.21>
     
     <FIELD.22>
         <xsl:if test="count(@CourtesyCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@CourtesyCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.22>
     
     <FIELD.23>
         <xsl:if test="count(@CreditRating ) > 0">
             <FIELD_ITEM><xsl:value-of select="@CreditRating"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.23>
     
     <FIELD.24>
         <xsl:for-each select="@ContractCode">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.24>
     
     <FIELD.25>
         <xsl:for-each select="@ContractEffectiveDate">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.25>
     
     <FIELD.26>
         <xsl:for-each select="@ContractAmount">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.26>
     
     <FIELD.27>
         <xsl:for-each select="@ContractPeriod">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.27>
     
     <FIELD.28>
         <xsl:if test="count(@InterestCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@InterestCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.28>
          
     <FIELD.29>
         <xsl:if test="count(@TransfertoBadDebtCode) > 0">
             <FIELD_ITEM><xsl:value-of select="@TransfertoBadDebtCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.29>
     
     <FIELD.30>
         <xsl:if test="count(@TrasfertoBadDebtDate ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TrasfertoBadDebtDate"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.30>
     
     <FIELD.31>
         <xsl:if test="count(@BadDebtAgencyCode ) > 0">
             <FIELD_ITEM><xsl:value-of select="@BadDebtAgencyCode"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.31>
     
     <FIELD.32>
         <xsl:if test="count(@BadDebtTransferAmount ) > 0">
             <FIELD_ITEM><xsl:value-of select="@PatientClass"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.32>
     
     <FIELD.33>
         <xsl:if test="count(@BadDebtRecoveryAmount ) > 0">
             <FIELD_ITEM><xsl:value-of select="@BadDebtRecoveryAmount"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.33>
     
     <FIELD.34>
         <xsl:if test="count(@DeleteAccountIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DeleteAccountIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.34>
     
     <FIELD.35>
         <xsl:if test="count(@DeleteAccountDate ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DeleteAccountDate"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.35>
     
     <FIELD.36>
         <xsl:if test="count(@DischargeDisposition ) > 0">
             <FIELD_ITEM><xsl:value-of select="@DischargeDisposition"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.36>
     
     <FIELD.37>
		 <xsl:for-each select="DischargedtoLocation">
			 <xsl:call-template name="DLD"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.37>
     
     <FIELD.38>
         <xsl:for-each select="DietType">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.38>
         
     <FIELD.39>
         <xsl:if test="count(@ServicingFacility ) > 0">
             <FIELD_ITEM><xsl:value-of select="@ServicingFacility"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.39>
     
     <FIELD.40>
         <xsl:if test="count(@BedStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@BedStatus"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.40>
     
     <FIELD.41>
         <xsl:if test="count(@AccountStatus ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AccountStatus"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.41>
     
     <FIELD.42>
		 <xsl:for-each select="PendingLocation">
			 <xsl:call-template name="PL"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.42>
     
     <FIELD.43>
			<xsl:for-each select="PriorTemporaryLocation">
				<xsl:call-template name="PL"></xsl:call-template>
			</xsl:for-each>
         
     </FIELD.43>
     
     <FIELD.44>
         <xsl:if test="count(@AdmitDT ) > 0">
             <FIELD_ITEM><xsl:value-of select="@AdmitDT"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.44>
     
     <FIELD.45>
         <xsl:for-each select="@DischargeDT">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.45>
     
     <FIELD.46>
         <xsl:if test="count(@CurrentPatientBalance ) > 0">
             <FIELD_ITEM><xsl:value-of select="@CurrentPatientBalance"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.46>
     
     <FIELD.47>
         <xsl:if test="count(@TotalCharges ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TotalCharges"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.47>
     
     <FIELD.48>
         <xsl:if test="count(@TotalAdjustments ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TotalAdjustments"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.48>
     
     <FIELD.49>
         <xsl:if test="count(@TotalPayments ) > 0">
             <FIELD_ITEM><xsl:value-of select="@TotalPayments"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.49>
     
     <FIELD.50>
		 <xsl:for-each select="AlternateVisitID">
			 <xsl:call-template name="CX"></xsl:call-template>
		 </xsl:for-each>
         
     </FIELD.50>
     
     <FIELD.51>
         <xsl:if test="count(@VisitIndicator ) > 0">
             <FIELD_ITEM><xsl:value-of select="@VisitIndicator"/></FIELD_ITEM>
         </xsl:if>
     </FIELD.51>
     
     <FIELD.52>
         <xsl:for-each select="OtherHealthcareProvider">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.52>
     
  </PV1>
  
  </xsl:template>

</xsl:stylesheet>

