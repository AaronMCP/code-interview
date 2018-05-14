<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="HD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@NameSpaceID"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@UniversalID"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@UniversalIDType"/>
			</COMPONENT.3>
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
			<COMPONENT.7>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@ExpirationDate"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:if test="count(AssigningJurisdiction/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningJurisdiction/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningJurisdiction/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningJurisdiction/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="AssigningJurisdiction/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="AssigningJurisdiction/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="AssigningJurisdiction/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="AssigningJurisdiction/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="AssigningJurisdiction/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="AssigningJurisdiction/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:if test="count(AssigningAgencyOrDepartment/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAgencyOrDepartment/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAgencyOrDepartment/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAgencyOrDepartment/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="AssigningAgencyOrDepartment/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="AssigningAgencyOrDepartment/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="AssigningAgencyOrDepartment/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="AssigningAgencyOrDepartment/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="AssigningAgencyOrDepartment/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="AssigningAgencyOrDepartment/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.10>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XPN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(FamilyName/@Surname ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="FamilyName/@Surname"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="FamilyName/@OwnSurnamePrefix"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="FamilyName/@OwnSurname"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="FamilyName/@SurnamePrefixFromPartnerSpouse"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="FamilyName/@SurnameFromPartnerSpouse"/>
					</SUBCOMPONENT.5>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@GivenName"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@SecondAndFurtherGivenNamesOrInitialsThereof"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@Suffix"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@Prefix"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Degree"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@NameTypeCode"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@NameRepresentationCode"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:if test="count(NameContext/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="NameContext/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="NameContext/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="NameContext/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="NameContext/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="NameContext/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="NameContext/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:if test="count(NameValidityRange/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="NameValidityRange/@RangeStartDateTime"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="NameValidityRange/@RangeEndDateTime"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@NameAssemblyOrder"/>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.12>
			<COMPONENT.13>
				<xsl:value-of select="@ExpirationDate"/>
			</COMPONENT.13>
			<COMPONENT.14>
				<xsl:value-of select="@ProfessionalSuffix"/>
			</COMPONENT.14>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XAD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(StreetAddress/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="StreetAddress/@StreetOrMailingAddress"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="StreetAddress/@StreetName"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="StreetAddress/@DwellingNumber"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@OtherDesignation"/>
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
			<COMPONENT.12>
				<xsl:if test="count(AddressValidityRange/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AddressValidityRange/@RangeStartDateTime"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AddressValidityRange/@RangeEndDateTime"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.12>
			<COMPONENT.13>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.13>
			<COMPONENT.14>
				<xsl:value-of select="@ExpirationDate"/>
			</COMPONENT.14>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XTN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@TelephoneNumber"/>
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
			<COMPONENT.10>
				<xsl:value-of select="@ExtensionPrefix"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@SpeedDialCode"/>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:value-of select="@UnformattedTelephoneNumber"/>
			</COMPONENT.12>
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
				<xsl:value-of select="@FieldRepetitionNo"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@MainIngredientsNo"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@SubIngredientsNo"/>
			</COMPONENT.6>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CWE">
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
			<COMPONENT.7>
				<xsl:value-of select="@CodingSystemVersionID"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@AlternateCodingSystemVersionID"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@OriginalText"/>
			</COMPONENT.9>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="EI">
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
	<xsl:template name="TQ">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(Quantity/@* ) > 0">
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
				<xsl:value-of select="@Interval"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@Duration"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@StartDateTime"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@EndDateTime"/>
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
				<xsl:if test="count(OccurrenceDuration/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="OccurrenceDuration/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="OccurrenceDuration/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="OccurrenceDuration/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="OccurrenceDuration/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="OccurrenceDuration/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="OccurrenceDuration/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:value-of select="@TotalOccurrences"/>
			</COMPONENT.12>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="EIP">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(RequestAllocationID/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="RequestAllocationID/@EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="RequestAllocationID/@NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="RequestAllocationID/@UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="RequestAllocationID/@UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(ImplementAllocationID/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ImplementAllocationID/@EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ImplementAllocationID/@NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ImplementAllocationID/@UniversalID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ImplementAllocationID/@UniversalIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="XCN">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@IDNumber"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(FamilyName/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="FamilyName/@Surname"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="FamilyName/@OwnSurnamePrefix"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="FamilyName/@OwnSurname"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="FamilyName/@SurnamePrefixFromPartnerSpouse"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="FamilyName/@SurnameFromPartnerSpouse"/>
					</SUBCOMPONENT.5>
				</xsl:if>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@GivenName"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@SecondAndFurtherGivenNamesOrInitialsThereof"/>
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
				<xsl:value-of select="@SourceTable"/>
			</COMPONENT.8>
			<COMPONENT.9>
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
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@NameTypeCode"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@IdentifierCheckDigit"/>
			</COMPONENT.11>
			<COMPONENT.12>
				<xsl:if test="count(@CheckDigitScheme) > 0">
					<xsl:value-of select="@CheckDigitScheme"/>
				</xsl:if>
				<xsl:if test="count(@CodeIdentifyingTheCheckDigitSchemeEmployed) > 0">
					<xsl:value-of select="@CodeIdentifyingTheCheckDigitSchemeEmployed"/>
				</xsl:if>
			</COMPONENT.12>
			<COMPONENT.13>
				<xsl:value-of select="@IdentifierTypeCode"/>
			</COMPONENT.13>
			<COMPONENT.14>
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
			</COMPONENT.14>
			<COMPONENT.15>
				<xsl:value-of select="@NameRepresentationCode"/>
			</COMPONENT.15>
			<COMPONENT.16>
				<xsl:if test="count(NameContext/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="NameContext/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="NameContext/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="NameContext/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="NameContext/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="NameContext/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="NameContext/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.16>
			<COMPONENT.17>
				<xsl:if test="count(NameValidityRange/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="NameValidityRange/@RangeStartDateTime"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="NameValidityRange/@RangeEndDateTime"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.17>
			<COMPONENT.18>
				<xsl:value-of select="@NameAssemblyOrder"/>
			</COMPONENT.18>
			<COMPONENT.19>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.19>
			<COMPONENT.20>
				<xsl:value-of select="@ExpirationDate"/>
			</COMPONENT.20>
			<COMPONENT.21>
				<xsl:value-of select="@ProfessionalSuffix"/>
			</COMPONENT.21>
			<COMPONENT.22>
				<xsl:if test="count(AssigningJurisdiction/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningJurisdiction/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningJurisdiction/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningJurisdiction/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="AssigningJurisdiction/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="AssigningJurisdiction/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="AssigningJurisdiction/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="AssigningJurisdiction/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="AssigningJurisdiction/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="AssigningJurisdiction/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.22>
			<COMPONENT.23>
				<xsl:if test="count(AssigningAgencyorDepartment/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAgencyorDepartment/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAgencyorDepartment/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAgencyorDepartment/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="AssigningAgencyorDepartment/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="AssigningAgencyorDepartment/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="AssigningAgencyorDepartment/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="AssigningAgencyorDepartment/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="AssigningAgencyorDepartment/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="AssigningAgencyorDepartment/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.23>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="PL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@PointOfCare"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Room"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@Bed"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(Facility/@* ) > 0">
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
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@LocationStatus"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@PersonLocationType"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@Building"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@Floor"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@LocationDescription"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:if test="count(ComprehensiveLocationIdentifier/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ComprehensiveLocationIdentifier/@EntityIdentifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ComprehensiveLocationIdentifier/@NameSpaceID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ComprehensiveLocationIdentifier/@UniveralID"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ComprehensiveLocationIdentifier/@UniveralIDType"/>
					</SUBCOMPONENT.4>
				</xsl:if>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:if test="count(AssigningAuthorityforLocation/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="AssigningAuthorityforLocation/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="AssigningAuthorityforLocation/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="AssigningAuthorityforLocation/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
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
			<COMPONENT.10>
				<xsl:value-of select="@OrginizationIdentifier"/>
			</COMPONENT.10>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="FC">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@FinacialClassCode"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@EffectiveDate"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="DLD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="DischargeLocation"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="EffectiveDate"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="CQ">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@Quantity"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(Units/@* ) > 0">
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
				<xsl:if test="count(SpecimenSourceNameorCode/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="SpecimenSourceNameorCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="SpecimenSourceNameorCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="SpecimenSourceNameorCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="SpecimenSourceNameorCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="SpecimenSourceNameorCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="SpecimenSourceNameorCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="SpecimenSourceNameorCode/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="SpecimenSourceNameorCode/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="SpecimenSourceNameorCode/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(Additives/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Additives/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Additives/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Additives/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="Additives/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="Additives/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="Additives/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="Additives/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="Additives/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="Additives/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@SpecimenCollectionMethod"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:if test="count(BodySite/@* ) > 0">
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
					<SUBCOMPONENT.7>
						<xsl:value-of select="BodySite/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="BodySite/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="BodySite/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:if test="count(SiteModifier/@* ) > 0">
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
					<SUBCOMPONENT.7>
						<xsl:value-of select="SiteModifier/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="SiteModifier/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="SiteModifier/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:if test="count(CollectionMethodModifierCode/@* ) > 0">
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
					<SUBCOMPONENT.7>
						<xsl:value-of select="CollectionMethodModifierCode/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="CollectionMethodModifierCode/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="CollectionMethodModifierCode/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:if test="count(SpecimenRole/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="SpecimenRole/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="SpecimenRole/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="SpecimenRole/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="SpecimenRole/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="SpecimenRole/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="SpecimenRole/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="SpecimenRole/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="SpecimenRole/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="SpecimenRole/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.7>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="MOC">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(Money/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Money/@Quantity"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Money/@Denomination"/>
					</SUBCOMPONENT.2>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:if test="count(DemandCode/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="DemandCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="DemandCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="DemandCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="DemandCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="DemandCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="DemandCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="PRL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(ParentExamID/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="ParentExamID/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="ParentExamID/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="ParentExamID/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="ParentExamID/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="ParentExamID/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="ParentExamID/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@ParentExamSubID"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@ParentExamNote"/>
			</COMPONENT.3>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="NDL">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(Name/@* ) > 0">
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
						<xsl:value-of select="Name/@SecondAndFurtherGivenNamesOrInitialsThereof"/>
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
						<xsl:value-of select="Name/@AssigningAuthority-NameSpaceID"/>
					</SUBCOMPONENT.9>
					<SUBCOMPONENT.10>
						<xsl:value-of select="Name/@AssigningAuthority-UniversalID"/>
					</SUBCOMPONENT.10>
					<SUBCOMPONENT.11>
						<xsl:value-of select="Name/@AssigningAuthority-UniversalIDType"/>
					</SUBCOMPONENT.11>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@StartDateTime"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@EndDateTime"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@CarePlaces"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@Room"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@Sickbed"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:if test="count(Facilities/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Facilities/@NameSpaceID"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Facilities/@UniversalID"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Facilities/@UniversalIDType"/>
					</SUBCOMPONENT.3>
				</xsl:if>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@LocatedCondition"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@PatientLocatedType"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@Building"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:value-of select="@Floor"/>
			</COMPONENT.11>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="RPT">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:if test="count(RepeatPatternCode/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="RepeatPatternCode/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="RepeatPatternCode/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="RepeatPatternCode/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="RepeatPatternCode/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="RepeatPatternCode/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="RepeatPatternCode/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
					<SUBCOMPONENT.7>
						<xsl:value-of select="RepeatPatternCode/@CodingSystemVersionID"/>
					</SUBCOMPONENT.7>
					<SUBCOMPONENT.8>
						<xsl:value-of select="RepeatPatternCode/@AlternateCodingSystemVersionID"/>
					</SUBCOMPONENT.8>
					<SUBCOMPONENT.9>
						<xsl:value-of select="RepeatPatternCode/@OriginalText"/>
					</SUBCOMPONENT.9>
				</xsl:if>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@CalendarAlignment"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@PhaseRangeBeginValue"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@PhaseRangeEndValue"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:value-of select="@PeriodQuantity"/>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@PeriodUnits"/>
			</COMPONENT.6>
			<COMPONENT.7>
				<xsl:value-of select="@InstitutionSpecifiedTime"/>
			</COMPONENT.7>
			<COMPONENT.8>
				<xsl:value-of select="@Event"/>
			</COMPONENT.8>
			<COMPONENT.9>
				<xsl:value-of select="@EventOffsetQuantity"/>
			</COMPONENT.9>
			<COMPONENT.10>
				<xsl:value-of select="@EventOffsetUnits"/>
			</COMPONENT.10>
			<COMPONENT.11>
				<xsl:if test="count(GeneralTimingSpecification/@*) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="GeneralTimingSpecification/@GeneralTimingSpecification"/>
					</SUBCOMPONENT.1>
				</xsl:if>
			</COMPONENT.11>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="JCC">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@JobCode"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@JobClass"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="ZRD">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@Identifier"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Text"/>
			</COMPONENT.2>
			<COMPONENT.3>
				<xsl:value-of select="@CodingMethodName"/>
			</COMPONENT.3>
			<COMPONENT.4>
				<xsl:value-of select="@Value"/>
			</COMPONENT.4>
			<COMPONENT.5>
				<xsl:if test="count(Unit/@* ) > 0">
					<SUBCOMPONENT.1>
						<xsl:value-of select="Unit/@Identifier"/>
					</SUBCOMPONENT.1>
					<SUBCOMPONENT.2>
						<xsl:value-of select="Unit/@Text"/>
					</SUBCOMPONENT.2>
					<SUBCOMPONENT.3>
						<xsl:value-of select="Unit/@NameOfCodingSystem"/>
					</SUBCOMPONENT.3>
					<SUBCOMPONENT.4>
						<xsl:value-of select="Unit/@AlternateIdentifier"/>
					</SUBCOMPONENT.4>
					<SUBCOMPONENT.5>
						<xsl:value-of select="Unit/@AlternateText"/>
					</SUBCOMPONENT.5>
					<SUBCOMPONENT.6>
						<xsl:value-of select="Unit/@NameOfAlternateCodingSystem"/>
					</SUBCOMPONENT.6>
				</xsl:if>
			</COMPONENT.5>
			<COMPONENT.6>
				<xsl:value-of select="@FilmNumberOfPartitions"/>
			</COMPONENT.6>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="QIP">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@SegmentFieldName"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Value"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="SRT">
		<FIELD_ITEM>
			<COMPONENT.1>
				<xsl:value-of select="@SortByField"/>
			</COMPONENT.1>
			<COMPONENT.2>
				<xsl:value-of select="@Sequencing"/>
			</COMPONENT.2>
		</FIELD_ITEM>
	</xsl:template>
	<xsl:template name="RP">
		<FIELD_ITEM>
                     <COMPONENT.1><xsl:value-of select="@Pointer"/></COMPONENT.1>
                     <COMPONENT.2>
                         <xsl:if test="count(ApplicationID/@* ) > 0">
                             <SUBCOMPONENT.1><xsl:value-of select="ApplicationID/@NameSpaceID"/></SUBCOMPONENT.1>
                             <SUBCOMPONENT.2><xsl:value-of select="ApplicationID/@UniversalID"/></SUBCOMPONENT.2>
                             <SUBCOMPONENT.3><xsl:value-of select="ApplicationID/@UniversalIDType"/></SUBCOMPONENT.3>                             
                         </xsl:if>
                     </COMPONENT.2>
                     <COMPONENT.3><xsl:value-of select="@DataOfType"/> </COMPONENT.3>
                     <COMPONENT.4><xsl:value-of select="@SubType"/> </COMPONENT.4>
             </FIELD_ITEM>           
	</xsl:template>
</xsl:stylesheet>
