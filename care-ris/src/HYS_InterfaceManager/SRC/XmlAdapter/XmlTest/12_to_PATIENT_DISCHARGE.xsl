<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href = "XIMOutTemplate.xsl"/>
  <xsl:template match="/">
<XMLRequestMessage><XISVersion>3.0</XISVersion><Name><xsl:value-of select="/NewDataSet/Table/DATAINDEX_RECORD_INDEX_2" /></Name><Qualifier></Qualifier><OriginatingDevice>XIM(HL7) Adapter of GC Gateway 2.0</OriginatingDevice><TransactionID><xsl:value-of select="/NewDataSet/Table/DATAINDEX_RECORD_INDEX_3" /></TransactionID><TargetDevice>XIS</TargetDevice><XIM><ITEM><SECURITY_AND_PRIVACY><ENCRYPTED_DATA><TRANSFER_SYNTAX_IDENTIFIER><xsl:value-of select="/NewDataSet/Table/PATIENT_BIRTH_PLACE" /></TRANSFER_SYNTAX_IDENTIFIER><CONTENT><xsl:value-of select="/NewDataSet/Table/PATIENT_CUSTOMER_3" /></CONTENT></ENCRYPTED_DATA></SECURITY_AND_PRIVACY><PATIENT><IDENTIFICATION><NAME><xsl:for-each select="/NewDataSet/Table/PATIENT_PATIENT_NAME"><xsl:call-template name="TOut_PNM"></xsl:call-template></xsl:for-each></NAME><ID><xsl:for-each select="/NewDataSet/Table/PATIENT_PATIENTID"><xsl:call-template name="TOut_ID"></xsl:call-template></xsl:for-each></ID></IDENTIFICATION></PATIENT><SCHEDULED_PROCEDURE_STEP><TRANSACTION_UID><xsl:value-of select="/NewDataSet/Table/DATAINDEX_RECORD_INDEX_3" /></TRANSACTION_UID></SCHEDULED_PROCEDURE_STEP></ITEM></XIM></XMLRequestMessage>
  </xsl:template>
</xsl:stylesheet>
