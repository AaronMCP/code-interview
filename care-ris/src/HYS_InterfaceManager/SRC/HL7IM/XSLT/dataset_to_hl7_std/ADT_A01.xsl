<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1"
                xmlns:ris="urn:hl7-org:v2xml" xmlns:csb="http://www.carestream.com/csbroker">
  <xsl:strip-space elements="*"/>
  <xsl:template name="ADT_A01">
    <HL7>
      <xsl:call-template name="MSH"/>
      <xsl:call-template name="PID"/>
      <xsl:call-template name="EVN"/>
      <xsl:call-template name="PV1"/>
    </HL7>
  </xsl:template>

  <xsl:template name="MSH">
    <MSH FieldSeparator="|" EncodingCharacters="^~\&amp;" DTOfMessage="" Security="SecurityMessage" MessageControlID="1" SequenceNumber="10001" ContinuationPointer="ContinuationPointer" AcceptAcknowledgmentType="AL" ApplicationAcknowledgmentType="AL" CountryCode="/PRC" AlternateCharacterSetHandlingScheme="ISO 2022-1994">
      <SendingApplication NameSpaceID="CareStream"/>
      <SendingFacility NameSpaceID="HIS"/>
      <ReceivingApplication NameSpaceID="HL7Gateway"/>
      <ReceivingFacility NameSpaceID="HL7Gateway"/>
      <MessageType MessageCode="ADT" TriggerEvent="A01" MessageStructure="ADT_A01"/>
      <ProcessingID ProcessingID="P"/>
      <VersionID VersionID="2.5"/>
      <PrincipalLanguageofMessage Identifier="PRC"/>
      <MessageProfileIdentifier EntityIdentifier=""/>
    </MSH>
  </xsl:template>
  <xsl:template name="PID">
    <PID  MultipleBirthIndicator="" BirthOrder="" PatientDeathDateandTime="" PatientDeathIndicator="" IdentityUnknownIndicator="AL" LastUpdateDT="" Strain="">
      <xsl:attribute name="SetIDPatientID">
        <xsl:value-of select="csb:Table/csb:PATIENT_PATIENTID"/>
      </xsl:attribute>
      <xsl:attribute name="DTOfBirth">
        <xsl:value-of select="csb:Table/csb:PATIENT_BIRTHDATE"/>
      </xsl:attribute>
      <xsl:attribute name="Sex">
        <xsl:value-of select="csb:Table/csb:PATIENT_SEX"/>
      </xsl:attribute>
      <xsl:attribute name="SSNNumber-Patient">
        <xsl:value-of select="csb:Table/csb:PATIENT_SSN_NUMBER"/>
      </xsl:attribute>
      <xsl:attribute name="CountryCode">
        <xsl:value-of select="csb:Table/csb:PATIENT_COUNTRY_CODE"/>
      </xsl:attribute>
      <xsl:attribute name="BirthPlace">
        <xsl:value-of select="csb:Table/csb:PATIENT_BIRTH_PLACE"/>
      </xsl:attribute>
      <IdentityReliabilityCode></IdentityReliabilityCode>
      <PatientID_External >
        <xsl:attribute name="ID">
          <xsl:value-of select="csb:Table/csb:PATIENT_PATIENTID"/>
        </xsl:attribute>
      </PatientID_External>
      <PatientID_InternalID >
        <xsl:attribute name="ID">
          <xsl:value-of select="csb:Table/csb:PATIENT_PATIENTID"/>
        </xsl:attribute>
      </PatientID_InternalID>
      <AlternatePatientID-PID CheckDigit="">
        <xsl:attribute name="ID">
          <xsl:value-of select="csb:Table/csb:PATIENT_OTHER_PID"/>
        </xsl:attribute>
      </AlternatePatientID-PID>
      <PatientName>
        <FamilyName >
          <xsl:attribute name="Surname">
            <xsl:value-of select="csb:Table/csb:PATIENT_PATIENT_NAME"/>
          </xsl:attribute>
        </FamilyName>
      </PatientName>
      <MothersMaidenName>
        <FamilyName>
          <xsl:attribute name="Surname">
            <xsl:value-of select="csb:Table/csb:PATIENT_MOTHER_MAIDEN_NAME"/>
          </xsl:attribute>
        </FamilyName>
      </MothersMaidenName>
      <PatientAlias>
        <FamilyName >
          <xsl:attribute name="Surname">
            <xsl:value-of select="csb:Table/csb:PATIENT_PATIENT_ALIAS"/>
          </xsl:attribute>
        </FamilyName>
      </PatientAlias>
      <Race>
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_RACE"/>
        </xsl:attribute>
      </Race>
      <PatientAddress>
        <StreetAddress>
          <xsl:attribute name="StreetOrMailingAddress">
            <xsl:value-of select="csb:Table/csb:PATIENT_ADDRESS"/>
          </xsl:attribute>
        </StreetAddress>
      </PatientAddress>
      <PhoneNumber-Home>
        <xsl:attribute name="TelephoneNumber">
          <xsl:value-of select="csb:Table/csb:PATIENT_PHONENUMBER_HOME"/>
        </xsl:attribute>
      </PhoneNumber-Home>
      <PhoneNumber-Business >
        <xsl:attribute name="TelephoneNumber">
          <xsl:value-of select="csb:Table/csb:PATIENT_PHONENUMBER_BUSINESS"/>
        </xsl:attribute>
      </PhoneNumber-Business>
      <PrimaryLanguage >
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_PRIMARY_LANGUAGE"/>
        </xsl:attribute>
      </PrimaryLanguage>
      <MaritalStatus >
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_MARITAL_STATUS"/>
        </xsl:attribute>
      </MaritalStatus>
      <Religion>
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_RELIGION"/>
        </xsl:attribute>
      </Religion>
      <PatientAccountNumber>
        <xsl:attribute name="ID">
          <xsl:value-of select="csb:Table/csb:PATIENT_ACCOUNT_NUMBER"/>
        </xsl:attribute>
      </PatientAccountNumber>
      <DriversLicenseNumber-Patient >
        <xsl:attribute name="LicenseNumber">
          <xsl:value-of select="csb:Table/csb:PATIENT_DRIVERLIC_NUMBER"/>
        </xsl:attribute>
      </DriversLicenseNumber-Patient>
      <MothersIdentifier ID="N"/>
      <EthnicGroup >
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_ETHNIC_GROUP"/>
        </xsl:attribute>
      </EthnicGroup>
      <Citizenship >
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_CITIZENSHIP"/>
        </xsl:attribute>
      </Citizenship>
      <VeteransMilitaryStatus Identifier="2028-9"/>
      <Nationality Identifier="20110101">
        <xsl:attribute name="Identifier">
          <xsl:value-of select="csb:Table/csb:PATIENT_NATIONALITY"/>
        </xsl:attribute>
      </Nationality>
      <LastUpdateFacility NameSpaceID="SNOMED"/>
      <SpeciesCode Identifier="O"/>
      <BreedCode Identifier="DXL"/>
      <ProductionClassCode Identifier="201206"/>
    </PID>
  </xsl:template>
  <xsl:template name="EVN">
    <EVN EventTypeCode="" RecordDT="" DTPlannedEvent="" EventReasonCode="" EventOccurred="">
      <OperatorID IDNumber=""/>
      <EventFacility NameSpaceID=""/>
    </EVN>
  </xsl:template>
  <xsl:template name="PV1">
    <PV1 SetID-PV1="0001" PatientClass="I" AdmissionType="R" HospitalService="MED" PreadmitTestIndicator="Preadmit001" ReadmissionIndicator="R" AdmitSource="1" VIPIndicator="N" PatientType="N" ChargePriceIndicator="ChargePriceIndicator" CourtesyCode="2028-9" CreditRating="CreditRating" ContractCode="ContractCode" ContractEffectiveDate="201003051559" ContractAmount="4" ContractPeriod="30" InterestCode="InterestCode" TransfertoBadDebtCode="BadBebtCode" TransfertoBadDebtDate="201003051600" BadDebtAgencyCode="BadBevtAgebctCode" BadDebtTransferAmount="5" BadDebtRecoveryAmount="5" DeleteAcountIndicator="SelectAccountIndicator" DeleteAccountDate="201003051602" DischargeDisposition="01" ServicingFacility="HIS" BedStatus="C" AcountStatus="valid" AdmitDT="201003051605" DischargeDT="201003051606" CurrentPatientBalance="5" TotalCharges="5" TotalAdjustments="5" TotalPayments="5" VisitIndicator="A">
      <AmbulatoryStatus>A0</AmbulatoryStatus>
      <AssignedPatientLocation PointOfCare="" Room="" Bed=""/>
      <PreadmitNumber ID="Preadmit001"/>
      <PriorPatientLocation PointOfCare="" Room="" Bed=""/>
      <AttendingDoctor IDNumber="D0001" GivenName="">
        <FamilyName Surname=""/>
      </AttendingDoctor>
      <ReferringDoctor IDNumber="D0002" GivenName="">
        <FamilyName Surname=""/>
      </ReferringDoctor>
      <ConsultingDoctor IDNumber="D0003" GivenName="">
        <FamilyName Surname=""/>
      </ConsultingDoctor>
      <TemporaryLocation PointOfCare="" Room="" Bed=""/>
      <AdmittingDoctor IDNumber="D0003" GivenName="">
        <FamilyName Surname=""/>
      </AdmittingDoctor>
      <VisitNumber >
        <xsl:attribute name="ID">
          <xsl:value-of select="csb:Table/csb:PATIENT_VISIT_NUMBER"/>
        </xsl:attribute>
      </VisitNumber>
      <FinancialClass FinancialClassCode="Financial"/>
      <DicchargedtoLocation DischargeLocation=""/>
      <DietType Identifier=""/>
      <PendingLocation PointOfCare=""/>
      <PriorTemporaryLocation PointOfCare="" Room="" Bed=""/>
      <AlternateVisitID ID=""/>
      <OtherHealthcareProvider IDNumber=""/>
    </PV1>
  </xsl:template>

</xsl:stylesheet>
