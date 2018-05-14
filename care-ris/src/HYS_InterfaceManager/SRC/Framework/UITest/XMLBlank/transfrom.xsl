<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!--<xsl:output method="xml" encoding="UTF-8"/>-->
  <xsl:template match="/">
    <root>
      <Node_A>The Value of A:<xsl:value-of select="/r/a" />;</Node_A><br/>
      <Node_B>The Value of A:<xsl:value-of select="/r/b" />;</Node_B>
      <Node_C>The Value of A:<xsl:value-of select="/r/c" />;</Node_C>
      <Node_D>The Value of A:<xsl:value-of select="/r/d" />;</Node_D>
    </root>
  </xsl:template >
</xsl:stylesheet>