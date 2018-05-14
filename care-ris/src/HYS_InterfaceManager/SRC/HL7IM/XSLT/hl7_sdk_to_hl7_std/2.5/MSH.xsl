<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="MSH">
  <MSH>
     <xsl:attribute name="FieldSeparator"><xsl:value-of select = "MSH/FIELD.1" /></xsl:attribute>
     <xsl:attribute name="EncodingCharacters"><xsl:value-of select = "MSH/FIELD.2" /></xsl:attribute>
     
     <xsl:if test="MSH/FIELD.7/FIELD_ITEM != ''">
         <xsl:attribute name="DTOfMessage"><xsl:value-of select = "MSH/FIELD.7/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.8/FIELD_ITEM != ''">
         <xsl:attribute name="Security"><xsl:value-of select = "MSH/FIELD.8/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.10/FIELD_ITEM != ''">
         <xsl:attribute name="MessageControlID"><xsl:value-of select = "MSH/FIELD.10/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.13/FIELD_ITEM != ''">
         <xsl:attribute name="SequenceNumber"><xsl:value-of select = "MSH/FIELD.13/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.14/FIELD_ITEM != ''">
         <xsl:attribute name="ContinuationPointer"><xsl:value-of select = "MSH/FIELD.14/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.15/FIELD_ITEM != ''">
         <xsl:attribute name="AcceptAcknowledgmentType"><xsl:value-of select = "MSH/FIELD.15/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.16/FIELD_ITEM != ''">
         <xsl:attribute name="ApplicationAcknowledgmentType"><xsl:value-of select = "MSH/FIELD.16/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.17/FIELD_ITEM != ''">
         <xsl:attribute name="CountryCode"><xsl:value-of select = "MSH/FIELD.17/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.20/FIELD_ITEM != ''">
         <xsl:attribute name="AlternateCharacterSetHandlingScheme"><xsl:value-of select = "MSH/FIELD.20/FIELD_ITEM" /></xsl:attribute>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.3/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.3/FIELD_ITEM">
             <xsl:element name="SendingApplication">
                 <xsl:call-template name="HD"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.4/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.4/FIELD_ITEM">
             <xsl:element name="SendingFacility">
                 <xsl:call-template name="HD"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
  
     <xsl:if test="MSH/FIELD.5/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.5/FIELD_ITEM">
             <xsl:element name="ReceivingApplication">
                 <xsl:call-template name="HD"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.6/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.6/FIELD_ITEM">
             <xsl:element name="ReceivingFacility">
                <xsl:call-template name="HD"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.9/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.9/FIELD_ITEM">
             <xsl:element name="MessageType">
                 <xsl:call-template name="MSG"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
          
     <xsl:if test="MSH/FIELD.11/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.11/FIELD_ITEM">
             <xsl:element name="ProcessingID">
                 <xsl:call-template name="PT"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.12/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.12/FIELD_ITEM">
             <xsl:element name="VersionID">
                 <xsl:call-template name="VID"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
          
     <xsl:if test="MSH/FIELD.18/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.18/FIELD_ITEM">
             <xsl:element name="CharacterSet"><xsl:value-of select = "." /></xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.19/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.19/FIELD_ITEM">
             <xsl:element name="PrincipalLanguageofMessage">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
     
     <xsl:if test="MSH/FIELD.21/FIELD_ITEM != ''">
         <xsl:for-each select="MSH/FIELD.21/FIELD_ITEM">
             <xsl:element name="MessageProfileIdentifier">
                 <xsl:call-template name="EI"></xsl:call-template>
             </xsl:element>
         </xsl:for-each>
     </xsl:if>
  </MSH>
  
  </xsl:template>

</xsl:stylesheet>

