<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="PV1_2">

  <xsl:if test="FIELD.11/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.11/FIELD_ITEM">
		             <xsl:element name="TemporaryLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.17/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.17/FIELD_ITEM">
		             <xsl:element name="AdmittingDoctor">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.19/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.19/FIELD_ITEM">
		             <xsl:element name="VisitNumber">
		                 <xsl:call-template name="CX"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.20/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.20/FIELD_ITEM">
                     <xsl:element name="FinancialClass">
                         <xsl:call-template name="FC"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
  <xsl:if test="FIELD.37/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.37/FIELD_ITEM">
                     <xsl:element name="DicchargedtoLocation">
                         <xsl:call-template name="DLD"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
         
         <xsl:if test="FIELD.38/FIELD_ITEM != ''">
                 <xsl:for-each select="FIELD.38/FIELD_ITEM">
                     <xsl:element name="DietType">
                         <xsl:call-template name="CE"></xsl:call-template>
                     </xsl:element>
                 </xsl:for-each>
         </xsl:if>
             
		 <xsl:if test="FIELD.42/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.42/FIELD_ITEM">
		             <xsl:element name="PendingLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.43/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.43/FIELD_ITEM">
		             <xsl:element name="PriorTemporaryLocation">
		                 <xsl:call-template name="PL"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.50/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.50/FIELD_ITEM">
		             <xsl:element name="AlternateVisitID">
		                 <xsl:call-template name="CX"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
		 
		 <xsl:if test="FIELD.52/FIELD_ITEM != ''">
		         <xsl:for-each select="FIELD.52/FIELD_ITEM">
		             <xsl:element name="OtherHealthcareProvider">
		                 <xsl:call-template name="XCN"></xsl:call-template>
		             </xsl:element>
		         </xsl:for-each>
		 </xsl:if>
  
  </xsl:template>

</xsl:stylesheet>

