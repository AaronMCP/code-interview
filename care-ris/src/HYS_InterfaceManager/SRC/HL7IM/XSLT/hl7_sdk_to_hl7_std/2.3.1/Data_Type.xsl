<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="HD">
	<xsl:choose>
				     <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != ''">
				         <xsl:if test="COMPONENT.1 != ''">
				             <xsl:attribute name="EntityIdentifier"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
	    			     </xsl:if>
				         <xsl:if test="COMPONENT.2 != ''">
				             <xsl:attribute name="NameSpaceID"><xsl:value-of select = "COMPONENT.2" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.3 != ''">
			    	         <xsl:attribute name="UniversalID"><xsl:value-of select = "COMPONENT.3" /></xsl:attribute>
				         </xsl:if>
				         <xsl:if test="COMPONENT.4 != ''">
				             <xsl:attribute name="UniversalIDType"><xsl:value-of select = "COMPONENT.4" /></xsl:attribute>
				         </xsl:if>
				     </xsl:when>
				     <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			     </xsl:choose>
		
	</xsl:template>
	
	<xsl:template name="MSG">
	<xsl:choose>
				     <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != ''">
				         <xsl:if test="COMPONENT.1 != ''">
				             <xsl:attribute name="MessageCode"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.2 != ''">
			    	         <xsl:attribute name="TriggerEvent"><xsl:value-of select = "COMPONENT.2" /></xsl:attribute>
				         </xsl:if>
				         <xsl:if test="COMPONENT.3 != ''">
				             <xsl:attribute name="MessageStructure"><xsl:value-of select = "COMPONENT.3" /></xsl:attribute>
				         </xsl:if>
				     </xsl:when>
				     <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			     </xsl:choose>
		
	</xsl:template>
	
	<xsl:template name="PT">
		<xsl:choose>
				     <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
				         <xsl:if test="COMPONENT.1 != ''">
				             <xsl:attribute name="ProcessingID"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.2 != ''">
			    	         <xsl:attribute name="ProcessingMode"><xsl:value-of select = "COMPONENT.2" /></xsl:attribute>
				         </xsl:if>
				     </xsl:when>
				     <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			     </xsl:choose>		
	</xsl:template>
	
	<xsl:template name="VID">
		<xsl:choose>
				     <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != ''">
				         <xsl:if test="COMPONENT.1 != ''">
				             <xsl:attribute name="VersionID"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.2 != ''">
			    	         <xsl:element name="InternationalizationCode">
			    	             <xsl:choose>
									 <xsl:when test="COMPONENT.2/SUBCOMPONENT.1 != '' or COMPONENT.2/SUBCOMPONENT.2 != ''  or COMPONENT.2/SUBCOMPONENT.3 != '' or 
									                                COMPONENT.2/SUBCOMPONENT.4 != ''  or COMPONENT.2/SUBCOMPONENT.5 != ''  or COMPONENT.2/SUBCOMPONENT.6 != ''">
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.1 != ''">
									             <xsl:attribute name="Identifier"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.1" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.2 != ''">
									             <xsl:attribute name="Text"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.2" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.3 != ''">
									             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.3" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.4 != ''">
									             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.4" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.5 != ''">
									             <xsl:attribute name="AlternateText"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.5" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.2/SUBCOMPONENT.6 != ''">
									             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select = "COMPONENT.2/SUBCOMPONENT.6" /></xsl:attribute>
									         </xsl:if>
									 </xsl:when>
									 <xsl:otherwise><xsl:value-of select = "COMPONENT.2" /></xsl:otherwise>
								 </xsl:choose>
			    	         </xsl:element>
				         </xsl:if>
				         <xsl:if test="COMPONENT.3 != ''">
			    	         <xsl:element name="InternalVersionID">
			    	             <xsl:choose>
									 <xsl:when test="COMPONENT.3/SUBCOMPONENT.1 != '' or COMPONENT.3/SUBCOMPONENT.2 != ''  or COMPONENT.3/SUBCOMPONENT.3 != ''
									                           or COMPONENT.3/SUBCOMPONENT.4 != ''  or COMPONENT.3/SUBCOMPONENT.5 != ''  or COMPONENT.3/SUBCOMPONENT.6 != ''">
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.1 != ''">
									             <xsl:attribute name="Identifier"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.1" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.2 != ''">
									             <xsl:attribute name="Text"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.2" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.3 != ''">
									             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.3" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.4 != ''">
									             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.4" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.5 != ''">
									             <xsl:attribute name="AlternateText"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.5" /></xsl:attribute>
									         </xsl:if>
									         <xsl:if test="COMPONENT.3/SUBCOMPONENT.6 != ''">
									             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select = "COMPONENT.3/SUBCOMPONENT.6" /></xsl:attribute>
									         </xsl:if>
									 </xsl:when>
									 <xsl:otherwise><xsl:value-of select = "COMPONENT.2" /></xsl:otherwise>
								 </xsl:choose>
			    	         </xsl:element>
				         </xsl:if>
				     </xsl:when>
				     <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			     </xsl:choose>		
	</xsl:template>
	
		<xsl:template name="CE">
		<xsl:choose>
				     <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != ''">
				         <xsl:if test="COMPONENT.1 != ''">
				             <xsl:attribute name="Identifier"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.2 != ''">
			    	         <xsl:attribute name="Text"><xsl:value-of select = "COMPONENT.2" /></xsl:attribute>
				         </xsl:if>
				         <xsl:if test="COMPONENT.3 != ''">
				             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select = "COMPONENT.3" /></xsl:attribute>
				         </xsl:if>
				         <xsl:if test="COMPONENT.4 != ''">
				             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select = "COMPONENT.4" /></xsl:attribute>
	    			     </xsl:if>
		    		     <xsl:if test="COMPONENT.5 != ''">
			    	         <xsl:attribute name="AlternateText"><xsl:value-of select = "COMPONENT.5" /></xsl:attribute>
				         </xsl:if>
				         <xsl:if test="COMPONENT.6 != ''">
				             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select = "COMPONENT.6" /></xsl:attribute>
				         </xsl:if>
				     </xsl:when>
				     <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			     </xsl:choose>
	</xsl:template>
	
	<xsl:template name="CX">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
							                                COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="ID"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="CheckDigit"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="CodeIdentifyingTheCheckDigitSchemeEmployed"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="IdentifierTypeCode"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:element name="AssigningAuthority">
										<xsl:choose>
											<xsl:when test="COMPONENT.4/SUBCOMPONENT.1 != '' or COMPONENT.4/SUBCOMPONENT.2 != '' or COMPONENT.4/SUBCOMPONENT.3 != ''">
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:element name="AssigningFacility">
										<xsl:choose>
											<xsl:when test="COMPONENT.6/SUBCOMPONENT.1 != '' or COMPONENT.6/SUBCOMPONENT.2 != '' or COMPONENT.6/SUBCOMPONENT.3 != ''">
												<xsl:if test="COMPONENT.6/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.6/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.6/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="XPN">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
							                                COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="FamilyName"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="LastNamePrefix"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="GivenName"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="MiddleInitialOrName"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="Suffix"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="Prefix"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="Degree"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="NameTypeCode"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9!= ''">
									<xsl:attribute name="NameRepresentationCode"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="XAD">
	<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or 
				                                            COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != '' or COMPONENT.11 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="StreetAddress"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="OtherDesignation"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="City"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="StateOrProvince"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="ZipOrPostalCode"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="Country"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="AddressType"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="OtherGeographicDesignation"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9 != ''">
									<xsl:attribute name="CountyParishCode"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.10 != ''">
									<xsl:attribute name="CensusTract"><xsl:value-of select="COMPONENT.10"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.11 != ''">
									<xsl:attribute name="AddressRepresentationCode"><xsl:value-of select="COMPONENT.11"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="XTN">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
				                                            COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="CAnyText"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="TelecommunicationUseCode"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="TelecommunicationEquipmentType"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="EmailAddress"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="CountryCode"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="AreaCityCode"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="PhoneNumber"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="Extension"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9 != ''">
									<xsl:attribute name="AnyText"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="DLN">
			<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="LicenseNumber"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="IssueStateProvinceCountry"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="ExpirationDate"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="CWE">
			<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
				                                            COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="Text"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="CodingSystemVersionID"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="AlternateCodingSystemVersionID"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9 != ''">
									<xsl:attribute name="OriginalText"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="PL">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or
							                                COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="PointOfCare"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="Room"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="Bed"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="LocationStatus"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="PersonLocationType"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="Building"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="Floor"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9 != ''">
									<xsl:attribute name="LocationDescription"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:element name="Facility">
										<xsl:choose>
											<xsl:when test="COMPONENT.4/SUBCOMPONENT.1 != '' or COMPONENT.4/SUBCOMPONENT.2 != '' or COMPONENT.4/SUBCOMPONENT.3 != ''">
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="XCN">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or 
							                                COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != '' or COMPONENT.11 != '' or  COMPONENT.12 != '' or 
							                                COMPONENT.13 != '' or COMPONENT.14 != '' or COMPONENT.15 != '' or COMPONENT.16 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="IDNumber"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="FamilyName"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="LastNamePrefix"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="GivenName"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="MiddleInitialOrName"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="Surfix"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.7 != ''">
									<xsl:attribute name="Prefix"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.8 != ''">
									<xsl:attribute name="Degree"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.9 != ''">
									<xsl:attribute name="SourceTable"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.11 != ''">
									<xsl:attribute name="NameTypeCode"><xsl:value-of select="COMPONENT.11"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.12 != ''">
									<xsl:attribute name="IdentifierCheckDigit"><xsl:value-of select="COMPONENT.12"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.13 != ''">
									<xsl:attribute name="CodeIdentifyingTheCheckDigitSchemeEmployed"><xsl:value-of select="COMPONENT.13"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.14 != ''">
									<xsl:attribute name="IdentifierTypeCode"><xsl:value-of select="COMPONENT.14"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.16 != ''">
									<xsl:attribute name="NameRepresentationCode"><xsl:value-of select="COMPONENT.16"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.10 != ''">
									<xsl:element name="AssigningAuthority">
										<xsl:choose>
											<xsl:when test="COMPONENT.10/SUBCOMPONENT.1 != '' or COMPONENT.10/SUBCOMPONENT.2 != '' or COMPONENT.10/SUBCOMPONENT.3 != ''">
												<xsl:if test="COMPONENT.10/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.10/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.10/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.10/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.10/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.10/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.10"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
								<xsl:if test="COMPONENT.15 != ''">
									<xsl:element name="AssigningFacility">
										<xsl:choose>
											<xsl:when test="COMPONENT.15/SUBCOMPONENT.1 != '' or COMPONENT.15/SUBCOMPONENT.2 != '' or COMPONENT.15/SUBCOMPONENT.3 != ''">
												<xsl:if test="COMPONENT.15/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.15/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.15/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.15/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.15/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.15/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.15"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="FC">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="FinancialClassCode"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="EffectiveDate"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="DLD">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="DischargeLocation"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="EffectiveDate"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="EI">
		<xsl:choose>
				         <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != ''">
				             <xsl:if test="COMPONENT.1 != ''">
				                 <xsl:attribute name="EntityIdentifier"><xsl:value-of select = "COMPONENT.1" /></xsl:attribute>
    	    			     </xsl:if>
	    			         <xsl:if test="COMPONENT.2 != ''">
		    		             <xsl:attribute name="NameSpaceID"><xsl:value-of select = "COMPONENT.2" /></xsl:attribute>
	    	    		     </xsl:if>
		    	    	     <xsl:if test="COMPONENT.3 != ''">
			    	             <xsl:attribute name="UniversalID"><xsl:value-of select = "COMPONENT.3" /></xsl:attribute>
				             </xsl:if>
    				         <xsl:if test="COMPONENT.4 != ''">
	    			             <xsl:attribute name="UniversalIDType"><xsl:value-of select = "COMPONENT.4" /></xsl:attribute>
		    		         </xsl:if>
			    	     </xsl:when>
				         <xsl:otherwise><xsl:value-of select = "." /></xsl:otherwise>
			         </xsl:choose>
	</xsl:template>
	
	<xsl:template name="TQ">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or 
							                                COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != '' or COMPONENT.11 != '' or COMPONENT.12 != ''">
					             <xsl:if test="COMPONENT.3 != ''">
					                 <xsl:attribute name="Duration"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.4 != ''">
						             <xsl:attribute name="StartDT"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.5 != ''">
						             <xsl:attribute name="EndDT"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.6 != ''">
						             <xsl:attribute name="Priority"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.7 != ''">
						             <xsl:attribute name="Condition"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.8 != ''">
						             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.9 != ''">
						             <xsl:attribute name="Conjunction"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.10 != ''">
						             <xsl:attribute name="OrderSequencing"><xsl:value-of select="COMPONENT.10"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.12 != ''">
						             <xsl:attribute name="TotalOccurances"><xsl:value-of select="COMPONENT.12"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.1 != ''">
					                 <xsl:element name="Quantity">
					                     <xsl:choose>
											 <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
											                                COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' or
											                                COMPONENT.1/SUBCOMPONENT.7 != ''">
											     <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
											         <xsl:attribute name="Quantity"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or COMPONENT.1/SUBCOMPONENT.4 != '' or 
											     						COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' or COMPONENT.1/SUBCOMPONENT.7 != ''">
											         <xsl:element  name="Units">
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
											                 <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
											             </xsl:if>
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
											                 <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
											             </xsl:if>
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
											                 <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
											             </xsl:if>
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
											                 <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
											             </xsl:if>
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
											                 <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
											             </xsl:if>
											             <xsl:if test="COMPONENT.1/SUBCOMPONENT.7 != ''">
											                 <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.7"/></xsl:attribute>
											             </xsl:if>
											         </xsl:element>
											     </xsl:if>
											 </xsl:when>
											 <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
										 </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
					                 <xsl:element name="Interval">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.2/SUBCOMPONENT.1 != '' or COMPONENT.2/SUBCOMPONENT.2 != ''">
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="RepeatPattern"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="ExplicitTimeInterval"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.2 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.11 != ''">
					                 <xsl:element name="PerformanceDuration">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.11/SUBCOMPONENT.1 != '' or COMPONENT.11/SUBCOMPONENT.2 != '' or COMPONENT.11/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.11/SUBCOMPONENT.4 != '' or COMPONENT.11/SUBCOMPONENT.5 != '' or COMPONENT.11/SUBCOMPONENT.6 != ''">
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.11"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="EIP">
		 <xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
					             <xsl:if test="COMPONENT.1 != ''">
					                 <xsl:element name="ParentsPlacerOrderNumber">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or 
										                                    COMPONENT.1/SUBCOMPONENT.3 != '' or COMPONENT.1/SUBCOMPONENT.4 != ''">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="EntityIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
					                 <xsl:element name="ParentsFillerOrderNumber">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.2/SUBCOMPONENT.1 != '' or COMPONENT.2/SUBCOMPONENT.2 != '' or 
										                                    COMPONENT.2/SUBCOMPONENT.3 != '' or COMPONENT.2/SUBCOMPONENT.4 != ''">
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="EntityIdentifier"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="NameSpaceID"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.2 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.11 != ''">
					                 <xsl:element name="PerformanceDuration">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.11/SUBCOMPONENT.1 != '' or COMPONENT.11/SUBCOMPONENT.2 != '' or COMPONENT.11/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.11/SUBCOMPONENT.4 != '' or COMPONENT.11/SUBCOMPONENT.5 != '' or COMPONENT.11/SUBCOMPONENT.6 != ''">
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.11/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.11/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.11"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="CQ">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
							     <xsl:if test="COMPONENT.1 != ''">
						             <xsl:attribute name="Quantity"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:element name="Units">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.2/SUBCOMPONENT.1 != '' or COMPONENT.2/SUBCOMPONENT.2 != '' or COMPONENT.2/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.2/SUBCOMPONENT.4 != '' or COMPONENT.2/SUBCOMPONENT.5 != '' or COMPONENT.2/SUBCOMPONENT.6 != ''">
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.2 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="SPS">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != ''">
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:attribute name="Additives"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.3 != ''">
						               <xsl:attribute name="FreeText"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.1 != ''">
					                 <xsl:element name="SpecimenSourceNameOrCode">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' ">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									      </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.4 != ''">
					                 <xsl:element name="BodySite">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' ">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.5 != ''">
					                 <xsl:element name="SiteModifier">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' ">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.6 != ''">
					                 <xsl:element name="CollectionMethodModifierCode">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' ">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="MOC">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
							     <xsl:if test="COMPONENT.1 != ''">
						             <xsl:element name="DollarAmount">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != ''">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Quantity"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Denomination"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:element name="ChargeCode">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.2/SUBCOMPONENT.1 != '' or COMPONENT.2/SUBCOMPONENT.2 != '' or COMPONENT.2/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.2/SUBCOMPONENT.4 != '' or COMPONENT.2/SUBCOMPONENT.5 != '' or COMPONENT.2/SUBCOMPONENT.6 != ''">
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.2/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.2/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.2 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="PRL">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != ''">
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:attribute name="OBX-4-Sub-IDofParentResult"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.3 != ''">
						               <xsl:attribute name="PartofOBX-5ObservationResultFromParent"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.1 != ''">
						             <xsl:element name="OBX-3-ObservationIdentifierofParentResult">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != ''">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="NDL">
	<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or 
							                                COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != '' or COMPONENT.11 != ''">
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:attribute name="StartDT"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.3 != ''">
						               <xsl:attribute name="EndDT"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.4 != ''">
					                 <xsl:attribute name="PointOfCare"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.5 != ''">
						             <xsl:attribute name="Room"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.6 != ''">
						             <xsl:attribute name="Bed"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.8 != ''">
						             <xsl:attribute name="LocationStatus"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.9 != ''">
						             <xsl:attribute name="PatientLocationType"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.10 != ''">
					                 <xsl:attribute name="Building"><xsl:value-of select="COMPONENT.10"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.11 != ''">
						               <xsl:attribute name="Floor"><xsl:value-of select="COMPONENT.11"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.1 != ''">
					                 <xsl:element name="Name">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.7 != '' or COMPONENT.1/SUBCOMPONENT.8 != '' or COMPONENT.1/SUBCOMPONENT.9 != ''">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="IDNumber"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="FamilyName"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="GivenName"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="MiddleInitialName"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="Suffix"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="Prefix"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.7 != ''">
										             <xsl:attribute name="Degree"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.7"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.8 != ''">
										             <xsl:attribute name="SourceTable"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.8"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.9 != ''">
										             <xsl:attribute name="AssigningAuthority"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.9"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.7 != ''">
					                 <xsl:element name="Facility">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.7/SUBCOMPONENT.1 != '' or COMPONENT.7/SUBCOMPONENT.2 != '' or COMPONENT.7/SUBCOMPONENT.3 != ''">
										         <xsl:if test="COMPONENT.7/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="NamespaceID"><xsl:value-of select="COMPONENT.7/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.7/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.7/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.7/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.7/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.7 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
	
	<xsl:template name="XON">
<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or 
							                                COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != ''">
					             <xsl:if test="COMPONENT.1 != ''">
						             <xsl:attribute name="OrganizationName"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
						             <xsl:attribute name="OrganizationNameTypeCode"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.3 != ''">
						               <xsl:attribute name="IDNumber"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.4 != ''">
					                 <xsl:attribute name="CheckDigit"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.5 != ''">
						             <xsl:attribute name="CodeIdentifyingTheCheckDigitSchemeEmployed"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.7 != ''">
						             <xsl:attribute name="IdentifierTypeCode"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.9 != ''">
						             <xsl:attribute name="NameRepresentationCode"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.6 != ''">
					                 <xsl:element name="AssigningAuthority">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.6/SUBCOMPONENT.1 != '' or COMPONENT.6/SUBCOMPONENT.2 != '' or COMPONENT.6/SUBCOMPONENT.3 != ''">
										         <xsl:if test="COMPONENT.6/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="NamespaceID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.6/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.6/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.6 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
					             <xsl:if test="COMPONENT.8 != ''">
					                 <xsl:element name="AssigningFacilityID">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.8/SUBCOMPONENT.1 != '' or COMPONENT.8/SUBCOMPONENT.2 != '' or COMPONENT.8/SUBCOMPONENT.3 != ''">
										         <xsl:if test="COMPONENT.8/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="NamespaceID"><xsl:value-of select="COMPONENT.8/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.8/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.8/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.8/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.8/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.8 "/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>	
	</xsl:template>
	
	<xsl:template name="ELD">
	<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
							                                COMPONENT.6 != '' or COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != ''">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="SegmentID"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="SegmentSequence"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="FieldLocated"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>								
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:element name="ErrorCode">
										<xsl:choose>
											<xsl:when test="COMPONENT.4/SUBCOMPONENT.1 != '' or COMPONENT.4/SUBCOMPONENT.2 != '' or COMPONENT.4/SUBCOMPONENT.3 != '' or COMPONENT.4/SUBCOMPONENT.4 != '' or COMPONENT.4/SUBCOMPONENT.5!= '' or COMPONENT.4/SUBCOMPONENT.6 != ''">
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.1 != ''">
													<xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="Text"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.4 != ''">
													<xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.4"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.5 != ''">
													<xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.5"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.6 != ''">
													<xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.6"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>								
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="ERL">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or 
							                                COMPONENT.6 != '' ">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="SegmentID"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="SegmentSequence"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.3 != ''">
									<xsl:attribute name="FieldLocated"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.4 != ''">
									<xsl:attribute name="FieldRepetitionNo"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.5 != ''">
									<xsl:attribute name="MainIngredientsNo"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.6 != ''">
									<xsl:attribute name="SubIngredientsNo"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
								</xsl:if>								
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="SRT">
		<xsl:choose>
							<xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' ">
								<xsl:if test="COMPONENT.1 != ''">
									<xsl:attribute name="SortByField"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="COMPONENT.2 != ''">
									<xsl:attribute name="Sequencing"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
	</xsl:template>
	
	<xsl:template name="JCC">
<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != ''">
							     <xsl:if test="COMPONENT.1 != ''">
						             <xsl:attribute name="JobCode"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.2 != ''">
						               <xsl:attribute name="JobClass"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>	
	</xsl:template>
	
	<xsl:template name="ZRD">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != ''">
							     <xsl:if test="COMPONENT.1 != ''">
							         <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1"/></xsl:attribute>
							     </xsl:if>
							     <xsl:if test="COMPONENT.2 != ''">
							         <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
							     </xsl:if>
							     <xsl:if test="COMPONENT.3 != ''">
							         <xsl:attribute name="CodingMethodName"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
							     </xsl:if>
							     <xsl:if test="COMPONENT.4 != ''">
							         <xsl:attribute name="Value"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
							     </xsl:if>
							     <xsl:if test="COMPONENT.6 != ''">
							         <xsl:attribute name="FilmNumberOfPartitions"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
							     </xsl:if>
							     <xsl:if test="COMPONENT.5 != ''">
							         <xsl:element name="Units">
							             <xsl:choose>
											 <xsl:when test="COMPONENT.5/SUBCOMPONENT.1 != '' or COMPONENT.5/SUBCOMPONENT.2 != ''  or COMPONENT.5/SUBCOMPONENT.3 != '' or
											                                COMPONENT.5/SUBCOMPONENT.4 != ''  or COMPONENT.5/SUBCOMPONENT.5 != ''  or COMPONENT.5/SUBCOMPONENT.6 != ''">
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.1 != ''">
											         <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.1"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.2 != ''">
											         <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.2"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.3 != ''">
											         <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.3"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.4 != ''">
											         <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.4"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.5 != ''">
											         <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.5"/></xsl:attribute>
											     </xsl:if>
											     <xsl:if test="COMPONENT.5/SUBCOMPONENT.6 != ''">
											         <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.5/SUBCOMPONENT.6"/></xsl:attribute>
											     </xsl:if>
											 </xsl:when>
											 <xsl:otherwise><xsl:value-of select="COMPONENT.5"/></xsl:otherwise>
										 </xsl:choose>
							         </xsl:element>
							     </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
					     </xsl:choose>
	</xsl:template>
	
	<xsl:template name="RPT">
		<xsl:choose>
							 <xsl:when test="COMPONENT.1 != '' or COMPONENT.2 != '' or COMPONENT.3 != '' or COMPONENT.4 != '' or COMPONENT.5 != '' or COMPONENT.6 != '' or
							                                COMPONENT.7 != '' or COMPONENT.8 != '' or COMPONENT.9 != '' or COMPONENT.10 != '' or COMPONENT.11 != '' or COMPONENT.12 != ''">
							     <xsl:if test="COMPONENT.2 != ''">
						             <xsl:attribute name="CalendarAlignment"><xsl:value-of select="COMPONENT.2"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.3 != ''">
						             <xsl:attribute name="PhaseRangeBeginValue"><xsl:value-of select="COMPONENT.3"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.4 != ''">
						             <xsl:attribute name="PhaseRangeEndValue"><xsl:value-of select="COMPONENT.4"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.5 != ''">
						             <xsl:attribute name="PeriodQuantity"><xsl:value-of select="COMPONENT.5"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.6 != ''">
						             <xsl:attribute name="PeriodUnits"><xsl:value-of select="COMPONENT.6"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.7 != ''">
						             <xsl:attribute name="InstitutionSpecifiedTime"><xsl:value-of select="COMPONENT.7"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.8 != ''">
						             <xsl:attribute name="Event"><xsl:value-of select="COMPONENT.8"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.9 != ''">
						             <xsl:attribute name="EventOffsetQuantity"><xsl:value-of select="COMPONENT.9"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.10 != ''">
						             <xsl:attribute name="EventOffsetUnits"><xsl:value-of select="COMPONENT.10"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.11 != ''">
						             <xsl:attribute name="GeneralTimingSpecification"><xsl:value-of select="COMPONENT.11"/></xsl:attribute>
					             </xsl:if>
					             <xsl:if test="COMPONENT.1 != ''">
						             <xsl:element name="Units">
					                     <xsl:choose>
										     <xsl:when test="COMPONENT.1/SUBCOMPONENT.1 != '' or COMPONENT.1/SUBCOMPONENT.2 != '' or COMPONENT.1/SUBCOMPONENT.3 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.4 != '' or COMPONENT.1/SUBCOMPONENT.5 != '' or COMPONENT.1/SUBCOMPONENT.6 != '' or
										                                    COMPONENT.1/SUBCOMPONENT.7 != '' or COMPONENT.1/SUBCOMPONENT.8 != '' or COMPONENT.1/SUBCOMPONENT.9 != ''">
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.1 != ''">
										             <xsl:attribute name="Identifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.1"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.2 != ''">
										             <xsl:attribute name="Text"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.2"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.3 != ''">
										             <xsl:attribute name="NameOfCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.3"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.4 != ''">
										             <xsl:attribute name="AlternateIdentifier"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.4"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.5 != ''">
										             <xsl:attribute name="AlternateText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.5"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.6 != ''">
										             <xsl:attribute name="NameOfAlternateCodingSystem"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.6"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.7 != ''">
										             <xsl:attribute name="CodingSystemVersionID"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.7"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.8 != ''">
										             <xsl:attribute name="AlternateCodingSystemVersionID"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.8"/></xsl:attribute>
										         </xsl:if>
										         <xsl:if test="COMPONENT.1/SUBCOMPONENT.9 != ''">
										             <xsl:attribute name="OriginalText"><xsl:value-of select="COMPONENT.1/SUBCOMPONENT.9"/></xsl:attribute>
										         </xsl:if>
										     </xsl:when>
										     <xsl:otherwise><xsl:value-of select="COMPONENT.1"/></xsl:otherwise>
									     </xsl:choose>
									 </xsl:element>
					             </xsl:if>
							 </xsl:when>
							 <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
						 </xsl:choose>
	</xsl:template>
</xsl:stylesheet>
