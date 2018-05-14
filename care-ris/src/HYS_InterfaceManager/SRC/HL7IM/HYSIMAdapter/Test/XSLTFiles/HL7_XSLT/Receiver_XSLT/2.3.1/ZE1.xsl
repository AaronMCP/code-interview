<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="ZE1">
		<ZE1>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="SetID-ZE1"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.2/FIELD_ITEM != ''">
			    <xsl:attribute name="ControlCode"><xsl:value-of select="FIELD.2/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.4/FIELD_ITEM != ''">
			    <xsl:attribute name="TechnicalCount"><xsl:value-of select="FIELD.4/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.8/FIELD_ITEM != ''">
				<xsl:attribute name="UseCommodityClass"><xsl:value-of select="FIELD.8/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.11/FIELD_ITEM != ''">
				<xsl:attribute name="OperationField"><xsl:value-of select="FIELD.11/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.12/FIELD_ITEM != ''">
				<xsl:attribute name="AccountField"><xsl:value-of select="FIELD.12/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			 
			 <xsl:if test="FIELD.3/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.3/FIELD_ITEM">
			         <xsl:element name="Technical">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.5/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.5/FIELD_ITEM">
			         <xsl:element name="MedicalAddition">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.6/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.6/FIELD_ITEM">
			         <xsl:element name="MedicalWorkClass">
		                 <xsl:call-template name="JCC"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.7/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.7/FIELD_ITEM">
		             <xsl:element name="MedicalWork">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.9/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.9/FIELD_ITEM">
			         <xsl:element name="UseCommodity">
			             <xsl:call-template name="ZRD"></xsl:call-template>
			         </xsl:element>
			     </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.10/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.10/FIELD_ITEM">
                     <xsl:element name="Contact">
                         <xsl:call-template name="XTN"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>		
		</ZE1>
	</xsl:template>

</xsl:stylesheet>
