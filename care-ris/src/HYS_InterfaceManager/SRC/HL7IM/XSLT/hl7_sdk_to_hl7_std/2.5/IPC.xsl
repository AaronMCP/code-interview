<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:strip-space elements="*"/>
	<xsl:template name="IPC">
		<IPC>
			<xsl:if test="FIELD.9/FIELD_ITEM != ''">
				<xsl:attribute name="ScheduledAETitle"><xsl:value-of select="FIELD.9/FIELD_ITEM"/></xsl:attribute>
			</xsl:if>
			 
			 <xsl:if test="FIELD.1/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.1/FIELD_ITEM">
		             <xsl:element name="AccessionIdentifier">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     			
		     <xsl:if test="FIELD.2/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.2/FIELD_ITEM">
		             <xsl:element name="RequestedProcedureID">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.3/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.3/FIELD_ITEM">
		             <xsl:element name="StudyInstanceUID">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.4/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.4/FIELD_ITEM">
		             <xsl:element name="ScheduledProcedureStepID">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.5/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.5/FIELD_ITEM">
		             <xsl:element name="Modality">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.6/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.6/FIELD_ITEM">
		             <xsl:element name="ProtocolCode">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.7/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.7/FIELD_ITEM">
		             <xsl:element name="ScheduledStationName">
		                 <xsl:call-template name="EI"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>
		     
		     <xsl:if test="FIELD.8/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.8/FIELD_ITEM">
		             <xsl:element name="ScheduledProcedureStepLocation">
		                 <xsl:call-template name="CE"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		     </xsl:if>		     
		</IPC>
	</xsl:template>
</xsl:stylesheet>
