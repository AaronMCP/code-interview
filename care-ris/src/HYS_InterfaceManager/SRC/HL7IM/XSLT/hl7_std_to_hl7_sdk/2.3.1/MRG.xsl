<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  
  <xsl:template name="MRG">
  
  <MRG>
    <FIELD.1>
         <xsl:for-each select="PriorPatientIdentifierList">
              <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.1>   
     
     <FIELD.2>
         <xsl:for-each select="PriorAlternativePatientID">
            <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.2>
     
     <FIELD.3>
         <xsl:for-each select="PriorPatientAccountNumber">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.3>
    
<FIELD.4>
         <xsl:for-each select="PriorPatientID">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.4>
	
	<FIELD.5>
         <xsl:for-each select="PriorVisitNumber">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.5>
     
     <FIELD.6>
         <xsl:for-each select="PriorAlternativeVisitID">
             <xsl:call-template name="CX"></xsl:call-template>
         </xsl:for-each>
     </FIELD.6>
 
     <FIELD.7>
         <xsl:for-each select="PriorPatientName">
             <xsl:call-template name="XPN"></xsl:call-template>
         </xsl:for-each>
     </FIELD.7>
     
       </MRG>
  
  </xsl:template>

</xsl:stylesheet>

