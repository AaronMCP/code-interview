<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>

  <xsl:template name="QPD">

  <QPD>
	  <FIELD.1>
             <xsl:for-each select="MessageQueryName">
                 <xsl:call-template name="CE"></xsl:call-template>
             </xsl:for-each>
         </FIELD.1>        
         
     <FIELD.2>
             <xsl:if test="count(@QueryTag ) > 0">
                 <FIELD_ITEM><xsl:value-of select="@QueryTag"/></FIELD_ITEM>
             </xsl:if>
         </FIELD.2>
     
     <xsl:choose>
						<xsl:when test="count(PersonalIdentifier)>0">
						<FIELD.3>
								 <xsl:for-each select="PersonalIdentifier">
									 <xsl:call-template name="CX"></xsl:call-template>
								 </xsl:for-each>
							 </FIELD.3>
						</xsl:when>
						<xsl:otherwise>
						<FIELD.3>
							 <xsl:for-each select="DemographicsFields">
								 <xsl:call-template name="QIP"></xsl:call-template>
							 </xsl:for-each>
						 </FIELD.3>
						</xsl:otherwise>
					</xsl:choose>     
     
         <FIELD.4>
             <xsl:for-each select="WhatDomainReturned">
                 <xsl:call-template name="CX"></xsl:call-template>
             </xsl:for-each>
         </FIELD.4>         
         
  </QPD>
  
  </xsl:template>

</xsl:stylesheet>

