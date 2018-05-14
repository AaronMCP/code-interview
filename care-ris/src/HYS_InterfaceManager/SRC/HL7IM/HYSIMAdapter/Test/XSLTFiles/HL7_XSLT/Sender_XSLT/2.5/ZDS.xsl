<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

  <xsl:strip-space elements="*"/>
  <xsl:template name="ZDS">  
  <ZDS>     
    
     <FIELD.1>
         <xsl:if test="count(ReferencePointer/@* ) > 0">
             <FIELD_ITEM>
                     <COMPONENT.1><xsl:value-of select="ReferencePointer/@Pointer"/></COMPONENT.1>
                     <COMPONENT.2>
                         <xsl:if test="count(ReferencePointer/ApplicationID/@* ) > 0">
                             <SUBCOMPONENT.1><xsl:value-of select="ReferencePointer/ApplicationID/@NameSpaceID"/></SUBCOMPONENT.1>
                             <SUBCOMPONENT.2><xsl:value-of select="ReferencePointer/ApplicationID/@UniversalID"/></SUBCOMPONENT.2>
                             <SUBCOMPONENT.3><xsl:value-of select="ReferencePointer/ApplicationID/@UniversalIDType"/></SUBCOMPONENT.3>                             
                         </xsl:if>
                     </COMPONENT.2>
                     <COMPONENT.3><xsl:value-of select="ReferencePointer/@DataOfType"/> </COMPONENT.3>
                     <COMPONENT.4><xsl:value-of select="ReferencePointer/@SubType"/> </COMPONENT.4>
             </FIELD_ITEM>           
       </xsl:if>
     </FIELD.1>
     
     
  </ZDS>
  
  </xsl:template>

</xsl:stylesheet>

