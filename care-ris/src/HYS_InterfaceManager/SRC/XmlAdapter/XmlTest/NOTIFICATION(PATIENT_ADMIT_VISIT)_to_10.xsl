<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href = "XIMInTemplate.xsl"/>
  <xsl:template match="/">
    <NewDataSet>
        <Table>
          <DATAINDEX_RECORD_INDEX_1>
            <xsl:value-of select="/XMLRequestMessage/Name"/>
          </DATAINDEX_RECORD_INDEX_1>
          <PATIENT_PATIENTID>
            <xsl:for-each select ="/XMLRequestMessage/XIM/ITEM">
              <xsl:for-each select ="PATIENT/IDENTIFICATION/ID">
                <xsl:call-template name ="TIn_ID">
                </xsl:call-template>
              </xsl:for-each>
            </xsl:for-each>
          </PATIENT_PATIENTID>
          <ORDER_FILLER_NO>
            <xsl:for-each select ="/XMLRequestMessage/XIM/ITEM">
              <xsl:for-each select ="ORDER/IDENTIFICATION/ACCESSION_NUMBER">
                <xsl:call-template name ="TIn_ID">
                </xsl:call-template>
              </xsl:for-each>
            </xsl:for-each>
          </ORDER_FILLER_NO>
          <DATAINDEX_RECORD_INDEX_3>
            <xsl:for-each select ="/XMLRequestMessage/XIM/ITEM">
              <xsl:for-each select ="SCHEDULED_PROCEDURE_STEP">
                <xsl:value-of select="TRANSACTION_UID"/>
              </xsl:for-each>
            </xsl:for-each>
          </DATAINDEX_RECORD_INDEX_3>
          <DATAINDEX_EVENT_TYPE>10</DATAINDEX_EVENT_TYPE>
        </Table>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>
