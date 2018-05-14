<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="ZDS">
		<ZDS>
			<xsl:if test="FIELD.1/FIELD_ITEM != ''">
         <xsl:for-each select="FIELD.1/FIELD_ITEM">
             <xsl:element name="ReferencePointer">
                <xsl:call-template name="RP"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>			
		</ZDS>
	</xsl:template>
</xsl:stylesheet>
