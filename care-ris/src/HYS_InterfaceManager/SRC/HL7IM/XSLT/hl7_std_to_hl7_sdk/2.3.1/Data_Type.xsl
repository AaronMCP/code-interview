<!-- edited with XMLSpy v2008 rel. 2 (http://www.altova.com) by adam (Rayco (Shanghai) Medical Products Company Limited) -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="HD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@EntityIdentifier"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@NameSpaceID"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@UniversalID"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@UniversalIDType"/>
			</COMPONENT.4>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="MSG">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@MessageCode"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@TriggerEvent"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@MessageStructure"/>
			</COMPONENT.3>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="PT">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@ProcessingID"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@ProcessingMode"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="VID">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@VersionID"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(InternationalizationCode ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="InternationalizationCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="InternationalizationCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="InternationalizationCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="InternationalizationCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="InternationalizationCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="InternationalizationCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:if test="count(InternalVersionID ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="InternalVersionID/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="InternalVersionID/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="InternalVersionID/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="InternalVersionID/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="InternalVersionID/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="InternalVersionID/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.3>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CE">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@Identifier"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Text"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@NameOfCodingSystem"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@AlternateIdentifier"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@AlternateText"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@NameOfAlternateCodingSystem"/>
			</COMPONENT.6>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CX">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@ID"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@CheckDigit"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@CodeIdentifyingTheCheckDigitSchemeEmployed"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(AssigningAuthority/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAuthority/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAuthority/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAuthority/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@IdentifierTypeCodeID"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:if test="count(AssigningFacility/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningFacility/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningFacility/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningFacility/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.6>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XPN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@FamilyName"/>
			</COMPONENT.1>
			<COMPONENT.2>
        <xsl:value-of select="@GivenName"/>
      </COMPONENT.2>
      <COMPONENT.3>
        <xsl:value-of select="@LastNamePrefix"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@MiddleInitialOrName"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@Suffix"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Prefix"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@Degree"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@NameTypeCode"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@NameRepresentationCode"/>
			</COMPONENT.9>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XAD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@StreetAddress"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@OtherDestination"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@City"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@StateOrProvince"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@ZipOrPostalCode"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Country"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@AddressType"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@OtherGeographicDesignation"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@CountyParishCode"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@CensusTract"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@AddressRepresentationCode"/>
			</COMPONENT.11>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XTN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@CAnyText"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@TelecommunicationUseCode"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@TelecommunicationEquipmentType"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@EmailAddress"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@CountryCode"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@AreaCityCode"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@PhoneNumber"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@Extension"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@AnyText"/>
			</COMPONENT.9>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="DLN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@LicenseNumber"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@IssuingStateProvinceCountry"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@ExpirationDate"/>
			</COMPONENT.3>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="PL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="AssignedPatientLocation/@PointOfCare"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="AssignedPatientLocation/@Room"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="AssignedPatientLocation/@Bed"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(AssignedPatientLocation/Facility/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssignedPatientLocation/Facility/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssignedPatientLocation/Facility/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssignedPatientLocation/Facility/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="AssignedPatientLocation/@LocationStatus"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="AssignedPatientLocation/@PersonLocationType"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="AssignedPatientLocation/@Building"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="AssignedPatientLocation/@Floor"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="AssignedPatientLocation/@LocationDecription"/>
			</COMPONENT.9>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XCN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@IDNumber"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@FamilyName"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@LastNamePrefix"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@GivenName"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@MiddleInitialOrName"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Surfix"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@Prefix"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@Degree"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@SourceTable"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:if test="count(AssigningAuthority/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAuthority/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAuthority/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAuthority/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@NameTypeCode"/>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:value-of select="@IdentifierCheckDigit"/>
			</COMPONENT.12>
			<COMPONENT.13>
				<xsl:value-of select="@CodeIdentifyingTheCheckDigitSchemeEmployed"/>
			</COMPONENT.13>
			<COMPONENT.14>
				<xsl:value-of select="@IdentifierTypeCode"/>
			</COMPONENT.14>
			<COMPONENT.15>
				<xsl:if test="count(AssigningFacility/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningFacility/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningFacility/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningFacility/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.15>
			<COMPONENT.16>
				<xsl:value-of select="@NameRepresentationCode"/>
			</COMPONENT.16>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="FC">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@FinancialClass"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="DLD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="DischargedToLocation/@DischargeLocation"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="DischargedToLocation/@EffectiveDate"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="ELD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@SegmentID"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@SegmentSequence"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@FieldLocated"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(ErrorCode/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ErrorCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ErrorCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ErrorCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ErrorCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="ErrorCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="ErrorCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.4>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="ERL">
		<FIELD_ITEM>
			<xsl:if test="count(ErrorPosition/@* ) > 0">
				<COMPONENT.1>
					<xsl:value-of select="ErrorPosition/@SegmentID"/>
				</COMPONENT.1>
				<COMPONENT.2>
					<xsl:value-of select="ErrorPosition/@SegmentSequence"/>
				</COMPONENT.2>
				<COMPONENT.3>
					<xsl:value-of select="ErrorPosition/@FieldLocated"/>
				</COMPONENT.3>
				<COMPONENT.4>
					<xsl:value-of select="ErrorPosition/@FieldRepetitionNo"/>
				</COMPONENT.4>
				<COMPONENT.5>
					<xsl:value-of select="ErrorPosition/@MainIngredientsNo"/>
				</COMPONENT.5>
				<COMPONENT.6>
					<xsl:value-of select="ErrorPosition/@SubIngredientsNo"/>
				</COMPONENT.6>
			</xsl:if>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CWE">
		<FIELD_ITEM>
			<xsl:if test="count(HL7ErrorCode/@* ) > 0">
				<COMPONENT.1>
					<xsl:value-of select="HL7ErrorCode/@Identifier"/>
				</COMPONENT.1>
				<COMPONENT.2>
					<xsl:value-of select="HL7ErrorCode/@Text"/>
				</COMPONENT.2>
				<COMPONENT.3>
					<xsl:value-of select="HL7ErrorCode/@NameOfCodingSystem"/>
				</COMPONENT.3>
				<COMPONENT.4>
					<xsl:value-of select="HL7ErrorCode/@AlternateIdentifier"/>
				</COMPONENT.4>
				<COMPONENT.5>
					<xsl:value-of select="HL7ErrorCode/@AlternateText"/>
				</COMPONENT.5>
				<COMPONENT.6>
					<xsl:value-of select="HL7ErrorCode/@NameOfAlternateCodingSystem"/>
				</COMPONENT.6>
				<COMPONENT.7>
					<xsl:value-of select="HL7ErrorCode/@CodingSystemVersionID"/>
				</COMPONENT.7>
				<COMPONENT.8>
					<xsl:value-of select="HL7ErrorCode/@AlternateCodingSystemVersionID"/>
				</COMPONENT.8>
				<COMPONENT.9>
					<xsl:value-of select="HL7ErrorCode/@OriginalText"/>
				</COMPONENT.9>
			</xsl:if>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="EI">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@EntityIdentifier"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@NamespaceID"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@UniversalID"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@UniversalIDType"/>
			</COMPONENT.4>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CQ">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@Quantity"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(Units) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Units/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Units/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Units/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="Units/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="Units/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="Units/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="SPS">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(SpecimenSourceNameOrCode/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="SpecimenSourceNameOrCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="SpecimenSourceNameOrCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="SpecimenSourceNameOrCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="SpecimenSourceNameOrCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="SpecimenSourceNameOrCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="SpecimenSourceNameOrCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Additives"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@FreeText"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(BodySite/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="BodySite/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="BodySite/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="BodySite/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="BodySite/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="BodySite/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="BodySite/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:if test="count(SiteModifier/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="SiteModifier/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="SiteModifier/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="SiteModifier/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="SiteModifier/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="SiteModifier/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="SiteModifier/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:if test="count(CollectionMethodModifierCode/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="CollectionMethodModifierCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="CollectionMethodModifierCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="CollectionMethodModifierCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="CollectionMethodModifierCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="CollectionMethodModifierCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="CollectionMethodModifierCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.6>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="MOC">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(DollarAmount/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="DollarAmount/@Quantity"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="DollarAmount/@Denomination"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(ChargeCode/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ChargeCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ChargeCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ChargeCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ChargeCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="ChargeCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="ChargeCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="PRL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(OBX-3-ObservationIdentifierofParentResult/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="OBX-3-ObservationIdentifierofParentResult/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@OBX-4-Sub-IDofParentResult"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@PartofOBX-5ObservationResultFromParent"/>
			</COMPONENT.3>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="TQ">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(Quantity/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Quantity/@Quantity"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Quantity/Unit/@Identifier"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Quantity/Unit/@Text"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="Quantity/Unit/@NameOfCodingSystem"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="Quantity/Unit/@AlternateIdentifier"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="Quantity/Unit/@AlternateText"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="Quantity/Unit/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.7>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(Interval/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Interval/@RepeatPattern"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Interval/@ExplicitTimeInterval"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@Duration"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@StartDT"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@EndDT"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Priority"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@Condition"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@Text"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@Conjunction"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@OrderSequencing"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:if test="count(PerformanceDuration/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="PerformanceDuration/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="PerformanceDuration/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="PerformanceDuration/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="PerformanceDuration/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="PerformanceDuration/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="PerformanceDuration/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:value-of select="@TotalOccurances"/>
			</COMPONENT.12>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CM">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(ParentsPlacerOrderNumber/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ParentsPlacerOrderNumber/@EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ParentsPlacerOrderNumber/@NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ParentsPlacerOrderNumber/@UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ParentsPlacerOrderNumber/@UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(ParentsFillerOrderNumber/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ParentsPlacerOrderNumber/@EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ParentsPlacerOrderNumber/@NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ParentsPlacerOrderNumber/@UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ParentsPlacerOrderNumber/@UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="NDL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(Name/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Name/@IDNumber"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Name/@FamilyName"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Name/@GivenName"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="Name/@MiddleInitialName"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="Name/@Suffix"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="Name/@Prefix"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="Name/@Degree"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="Name/@SourceTable"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="Name/@AssigningAuthority"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@StartDT"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@EndDT"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@PointOfCare"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@Room"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Bed"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:if test="count(Facility/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Facility/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Facility/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Facility/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@LocationStatus"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@PatientLocationType"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@Building"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@Floor"/>
			</COMPONENT.11>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XON">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@OrganizationName"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@OrganizationNameTypeCode"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@IDNumber"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@CheckDigit"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@CodeIdentifyingTheCheckDigitSchemeEmployed"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:if test="count(AssigningAuthority/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAuthority/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAuthority/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAuthority/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@IdentifierTypeCode"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:if test="count(AssigningFacilityID/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningFacilityID/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningFacilityID/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningFacilityID/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@NameRepresentationCode"/>
			</COMPONENT.9>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="EIP">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(ParentsPlacerOrderNumber/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ParentsPlacerOrderNumber/EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ParentsPlacerOrderNumber/NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ParentsPlacerOrderNumber/UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ParentsPlacerOrderNumber/UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(ParentsFillerOrderNumber/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ParentsPlacerOrderNumber/EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ParentsPlacerOrderNumber/NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ParentsPlacerOrderNumber/UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="/ParentsPlacerOrderNumber/UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	
	<xsl:template name="RPT">
		<FIELD_ITEM>
                     <COMPONENT.1>
                         <xsl:if test="count(RepeatPatternCode/@* ) > 0">
                             <SUBCOMPONENT.1><xsl:value-of select="RepeatPatternCode/@Identifier"/></SUBCOMPONENT.1>
                             <SUBCOMPONENT.2><xsl:value-of select="RepeatPatternCode/@Text"/></SUBCOMPONENT.2>
                             <SUBCOMPONENT.3><xsl:value-of select="RepeatPatternCode/@NameOfCodingSystem"/></SUBCOMPONENT.3>
                             <SUBCOMPONENT.4><xsl:value-of select="RepeatPatternCode/@AlternateIdentifier"/></SUBCOMPONENT.4>
                             <SUBCOMPONENT.5><xsl:value-of select="RepeatPatternCode/@AlternateText"/></SUBCOMPONENT.5>
                             <SUBCOMPONENT.6><xsl:value-of select="RepeatPatternCode/@NameOfAlternateCodingSystem"/></SUBCOMPONENT.6>
                             <SUBCOMPONENT.7><xsl:value-of select="RepeatPatternCode/@CodingSystemVersionID"/></SUBCOMPONENT.7>
                             <SUBCOMPONENT.8><xsl:value-of select="RepeatPatternCode/@AlternateCodingSystemVersionID"/></SUBCOMPONENT.8>
                             <SUBCOMPONENT.9><xsl:value-of select="RepeatPatternCode/@OriginalText"/></SUBCOMPONENT.9>
                         </xsl:if>
                     </COMPONENT.1>                 
                     <COMPONENT.2><xsl:value-of select="@CalendarAlignment"/></COMPONENT.2>
                     <COMPONENT.3><xsl:value-of select="@PhaseRangeBeginValue"/></COMPONENT.3>
                     <COMPONENT.4><xsl:value-of select="@PhaseRangeEndValue"/></COMPONENT.4>
                     <COMPONENT.5><xsl:value-of select="@PeriodQuantity"/></COMPONENT.5>
                     <COMPONENT.6><xsl:value-of select="@PeriodUnits"/></COMPONENT.6>
                     <COMPONENT.7><xsl:value-of select="@InstitutionSpecifiedTime"/></COMPONENT.7>
                     <COMPONENT.8><xsl:value-of select="@Event"/></COMPONENT.8>  
                     <COMPONENT.9><xsl:value-of select="@EventOffsetQuantity"/></COMPONENT.9>
                     <COMPONENT.10><xsl:value-of select="@EventOffsetUnits"/></COMPONENT.10>
                     <COMPONENT.11><xsl:value-of select="@GeneralTimingSpecification"/></COMPONENT.11>  
             </FIELD_ITEM>
	</xsl:template>
	<xsl:template name="JCC">
		<FIELD_ITEM>
                 <COMPONENT.1><xsl:value-of select="@JobCode"/></COMPONENT.1>                 
                 <COMPONENT.2><xsl:value-of select="@JobClass"/></COMPONENT.2>
             </FIELD_ITEM>
	</xsl:template>
	<xsl:template name="ZRD">
		<FIELD_ITEM>
                     <COMPONENT.1><xsl:value-of select="@Identifier"/></COMPONENT.1>                 
                     <COMPONENT.2><xsl:value-of select="@Text"/></COMPONENT.2>
                     <COMPONENT.3><xsl:value-of select="@CodingMethodName"/></COMPONENT.3>
                     <COMPONENT.4><xsl:value-of select="@Value"/></COMPONENT.4>
                     <COMPONENT.5>
                         <xsl:if test="count(Units/@* ) > 0">
                             <SUBCOMPONENT.1><xsl:value-of select="NameContext/@Identifier"/></SUBCOMPONENT.1>
                             <SUBCOMPONENT.2><xsl:value-of select="NameContext/@Text"/></SUBCOMPONENT.2>
                             <SUBCOMPONENT.3><xsl:value-of select="NameContext/@NameOfCodingSystem"/></SUBCOMPONENT.3>
                             <SUBCOMPONENT.4><xsl:value-of select="NameContext/@AlternateIdentifier"/></SUBCOMPONENT.4>
                             <SUBCOMPONENT.5><xsl:value-of select="NameContext/@AlternateText"/></SUBCOMPONENT.5>
                             <SUBCOMPONENT.6><xsl:value-of select="NameContext/@NameOfAlternateCodingSystem"/></SUBCOMPONENT.6>
                         </xsl:if>
                     </COMPONENT.5>
                     <COMPONENT.6><xsl:value-of select="@FilmNumberOfPartitions"/></COMPONENT.6>
             </FIELD_ITEM>
	</xsl:template>
</xsl:stylesheet>
