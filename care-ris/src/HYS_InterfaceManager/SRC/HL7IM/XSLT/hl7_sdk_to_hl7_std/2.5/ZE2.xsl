<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="ZE2">
		<ZE2>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
				<xsl:attribute name="SetID"><xsl:value-of select="FIELD.1/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.6/FIELD_ITEM != ''">
				<xsl:attribute name="Count"><xsl:value-of select="FIELD.6/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			<xsl:if test="FIELD.7/FIELD_ITEM != ''">
				<xsl:attribute name="ShootField"><xsl:value-of select="FIELD.7/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			
			 <xsl:if test="FIELD.2/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.2/FIELD_ITEM">
			         <xsl:element name="Voltage">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
			 </xsl:if>
			 
			 <xsl:if test="FIELD.3/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.3/FIELD_ITEM">
			         <xsl:element name="ElectricCurrent">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
			 </xsl:if>
			 
			 <xsl:if test="FIELD.4/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.4/FIELD_ITEM">
			         <xsl:element name="Distance">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
			 </xsl:if>
			 
			 <xsl:if test="FIELD.5/FIELD_ITEM != ''">
			     <xsl:for-each select="FIELD.5/FIELD_ITEM">
			         <xsl:element name="Time">
		                 <xsl:call-template name="CQ"></xsl:call-template>
		             </xsl:element>
			     </xsl:for-each>
			 </xsl:if>		     
		</ZE2>
	</xsl:template>
</xsl:stylesheet>
