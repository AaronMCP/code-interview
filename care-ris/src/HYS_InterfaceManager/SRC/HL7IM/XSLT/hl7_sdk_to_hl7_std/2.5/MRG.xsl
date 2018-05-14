<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="MRG">
		<MRG>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.1/FIELD_ITEM">
					<xsl:element name="PriorPatientIdentifierList">
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
													<!-- <xsl:attribute name="NamespaceID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.1"/></xsl:attribute> -->
													<xsl:attribute name="NameSpaceID"><xsl:value-of select="concat(COMPONENT.4/SUBCOMPONENT.1,'&amp;',COMPONENT.4/SUBCOMPONENT.2,'&amp;',COMPONENT.4/SUBCOMPONENT.3)"/></xsl:attribute>
												</xsl:if>
												<!-- by LCF 20100613, for if add concat in subcom.1, if not mark subcom.2 and Subcom.3, RHIS PIX will sometimes report Error
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.4/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.4/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
												-->
											</xsl:when>
											<xsl:otherwise>
												<!-- <xsl:value-of select="COMPONENT.4 "/>-->
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
													<xsl:attribute name="NamespaceID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.1"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.6/SUBCOMPONENT.2 != ''">
													<xsl:attribute name="UniversalID"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.2"/></xsl:attribute>
												</xsl:if>
												<xsl:if test="COMPONENT.6/SUBCOMPONENT.3 != ''">
													<xsl:attribute name="UniversalIDType"><xsl:value-of select="COMPONENT.6/SUBCOMPONENT.3"/></xsl:attribute>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="COMPONENT.4 "/>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:element>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.2/FIELD_ITEM">
					<xsl:element name="PriorAlternativePatientID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.3/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.3/FIELD_ITEM">
					<xsl:element name="PriorPatientAccountNumber">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.4/FIELD_ITEM">
					<xsl:element name="PriorPatientID">
						<xsl:call-template name="CX"></xsl:call-template>									
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.5/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.5/FIELD_ITEM">
					<xsl:element name="PriorVisitNumber">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.6/FIELD_ITEM">
					<xsl:element name="PriorAlternativeVisitID">
						<xsl:call-template name="CX"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>			
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:for-each select="FIELD.7/FIELD_ITEM">
					<xsl:element name="PatientName">
						<xsl:call-template name="XPN"></xsl:call-template>
					</xsl:element>
				</xsl:for-each>
			</xsl:if>
		</MRG>
	</xsl:template>
</xsl:stylesheet>
