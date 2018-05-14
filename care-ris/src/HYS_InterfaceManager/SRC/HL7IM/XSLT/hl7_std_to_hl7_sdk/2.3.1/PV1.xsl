<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="PV1">
  
  <PV1>
     <FIELD.1>
         <FIELD_ITEM><xsl:value-of select="@SetID-PV1"/></FIELD_ITEM>
     </FIELD.1>
     
     <FIELD.2>
         <FIELD_ITEM><xsl:value-of select="@PatientClass"/></FIELD_ITEM>
     </FIELD.2>   
     
     <FIELD.3>
         <xsl:for-each select="AssignedPatientLocation">
             <xsl:call-template name="PL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>
     
     <FIELD.4>
         <FIELD_ITEM><xsl:value-of select="@AdmissionType"/></FIELD_ITEM>
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
         <FIELD_ITEM><xsl:value-of select="@HospitalService"/></FIELD_ITEM>
     </FIELD.10>
     
     <FIELD.11>
         <xsl:for-each select="TemporaryLocation">
             <xsl:call-template name="PL"></xsl:call-template>
         </xsl:for-each>
     </FIELD.11>
     
     <FIELD.12>
         <FIELD_ITEM><xsl:value-of select="@PreadmitTestIndicator"/></FIELD_ITEM>
     </FIELD.12>
     
     <FIELD.13>
         <FIELD_ITEM><xsl:value-of select="@ReadmissionIndicator"/></FIELD_ITEM>
     </FIELD.13>
     
     <FIELD.14>
         <FIELD_ITEM><xsl:value-of select="@AdmitSource"/></FIELD_ITEM>
     </FIELD.14>
     
     <FIELD.15>
         <xsl:for-each select="AmbulatoryStatus">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.15>
     
     <FIELD.16>
         <FIELD_ITEM><xsl:value-of select="@VIPIndicator"/></FIELD_ITEM>
     </FIELD.16>
     
     <FIELD.17>
         <xsl:for-each select="AdmittingDoctor">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.17>
     
     <FIELD.18>
         <FIELD_ITEM><xsl:value-of select="@PatientType"/></FIELD_ITEM>
     </FIELD.18>
     
     <FIELD.19>
         <xsl:for-each select="VisitNumber">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.19>
       
     <FIELD.20>
         <xsl:for-each select="FinancialClass">
            <xsl:call-template name="FC"></xsl:call-template>
         </xsl:for-each>
     </FIELD.20>
     
     <FIELD.21>
         <FIELD_ITEM><xsl:value-of select="@ChargePriceIndicator"/></FIELD_ITEM>
     </FIELD.21>
     
     <FIELD.22>
         <FIELD_ITEM><xsl:value-of select="@CourtesyCode"/></FIELD_ITEM>
     </FIELD.22>
     
     <FIELD.23>
         <FIELD_ITEM><xsl:value-of select="@CreditRating"/></FIELD_ITEM>
     </FIELD.23>
     
     <FIELD.24>
         <xsl:for-each select="ContractCode">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.24>
     
     <FIELD.25>
         <xsl:for-each select="ContractEffectiveDate">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.25>
     
     <FIELD.26>
         <xsl:for-each select="ContractAmount">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.26>
     
     <FIELD.27>
         <xsl:for-each select="ContractPeriod">
             <FIELD_ITEM><xsl:value-of select="."/></FIELD_ITEM>
         </xsl:for-each>
     </FIELD.27>
     
     <FIELD.28>
         <FIELD_ITEM><xsl:value-of select="@InterestCode"/></FIELD_ITEM>
     </FIELD.28>
     
     <FIELD.29>
         <FIELD_ITEM><xsl:value-of select="@TransfertoBedDebtCode"/></FIELD_ITEM>
     </FIELD.29>
     
     <FIELD.30>
         <FIELD_ITEM><xsl:value-of select="@TransfertoBadDebtDate"/></FIELD_ITEM>
     </FIELD.30>
     
     <FIELD.31>
         <FIELD_ITEM><xsl:value-of select="@BadDebtAgencyCode"/></FIELD_ITEM>
     </FIELD.31>
     
     <FIELD.32>
         <FIELD_ITEM><xsl:value-of select="@BadDebtTransferAmount"/></FIELD_ITEM>
     </FIELD.32>
     
     <FIELD.33>
         <FIELD_ITEM><xsl:value-of select="@BadDebtRecoveryAmount"/></FIELD_ITEM>
     </FIELD.33>
     
     <FIELD.34>
         <FIELD_ITEM><xsl:value-of select="@DeleteAccountIndicator"/></FIELD_ITEM>
     </FIELD.34>
     
     <FIELD.35>
         <FIELD_ITEM><xsl:value-of select="@DeleteAccountDate"/></FIELD_ITEM>
     </FIELD.35>
     
     <FIELD.36>
         <FIELD_ITEM><xsl:value-of select="@DischargeDisposition"/></FIELD_ITEM>
     </FIELD.36>
     
     <FIELD.37>
         <xsl:if test="count(DischargedToLocation/@* ) > 0">
            <xsl:call-template name="DLD"></xsl:call-template>
         </xsl:if>
     </FIELD.37>
     
     <FIELD.38>
         <xsl:for-each select="DietType">
             <xsl:call-template name="CE"></xsl:call-template>
         </xsl:for-each>
     </FIELD.38>
     
     <FIELD.39>
         <FIELD_ITEM><xsl:value-of select="@ServicingFacility"/></FIELD_ITEM>
     </FIELD.39>
     
     <FIELD.40>
         <FIELD_ITEM><xsl:value-of select="@BedStatus"/></FIELD_ITEM>
     </FIELD.40>
     
     <FIELD.41>
         <FIELD_ITEM><xsl:value-of select="@AccountStatus"/></FIELD_ITEM>
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
         <FIELD_ITEM><xsl:value-of select="@AdmitDT"/></FIELD_ITEM>
     </FIELD.44>
     
     <FIELD.45>
         <FIELD_ITEM><xsl:value-of select="@DischargeDT"/></FIELD_ITEM>
     </FIELD.45>
     
     <FIELD.46>
         <FIELD_ITEM><xsl:value-of select="@CurrentPatientBalance"/></FIELD_ITEM>
     </FIELD.46>
     
     <FIELD.47>
         <FIELD_ITEM><xsl:value-of select="@TotalCharges"/></FIELD_ITEM>
     </FIELD.47>
     
     <FIELD.48>
         <FIELD_ITEM><xsl:value-of select="@TotalAdjustment"/></FIELD_ITEM>
     </FIELD.48>
     
     <FIELD.49>
         <FIELD_ITEM><xsl:value-of select="@TotalPayment"/></FIELD_ITEM>
     </FIELD.49>
     
     <FIELD.50>
         <xsl:for-each select="AlternateVisitID">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.50>
     
     <FIELD.51>
         <FIELD_ITEM><xsl:value-of select="@VisitIndicator"/></FIELD_ITEM>
     </FIELD.51>
     
     <FIELD.52>
		 <xsl:for-each select="OtherHealthcareProvider">
             <xsl:call-template name="XCN"></xsl:call-template>
         </xsl:for-each>
        
     </FIELD.52>
  </PV1>
  
  </xsl:template>

</xsl:stylesheet>

