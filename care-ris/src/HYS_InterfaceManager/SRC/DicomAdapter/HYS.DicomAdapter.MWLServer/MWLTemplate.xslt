<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.1" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:csh="http://www.HaoYiSheng.com/DCM_SDK">
  <xsl:template match="/">
    <DElementList>
      <DElement>
        <VR>SQ</VR>
        <Tag>00400100</Tag>
        <Value />
        <Sequence>
          <DElementList>
            <DElement>
              <!--ScheduledStationAETitle-->
              <VR>AE</VR>
              <Tag>00400001</Tag>
              <Value>StationAET</Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledProcedureStepStartDate-->
              <VR>DA</VR>
              <Tag>00400002</Tag>
              <Value>20110512</Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledProcedureStepStartTime-->
              <VR>TM</VR>
              <Tag>00400003</Tag>
              <Value>190455.000</Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--Modality-->
              <VR>CS</VR>
              <Tag>00080060</Tag>
              <Value>
                <xsl:value-of select="/DElementList/DElement[Tag='00400100']/Sequence/DElementList[1]/DElement[Tag='00080060']/Value"/>
              </Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledPerformingPhysiciansName-->
              <VR>PN</VR>
              <Tag>00400006</Tag>
              <Value></Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledProcedureStepDescription-->
              <VR>LO</VR>
              <Tag>00400007</Tag>
              <Value>ScheduledProcedureStepDescription</Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledStationName-->
              <VR>SH</VR>
              <Tag>00400010</Tag>
              <Value></Value>
              <Sequence />
            </DElement>
            <DElement>
              <!--ScheduledActionItemCodeSequence-->
              <VR>SQ</VR>
              <Tag>00400008</Tag>
              <Value />
              <Sequence>
                <!--
                <DElementList>
                  <DElement>
                    <VR>SH</VR>
                    <Tag>00080100</Tag>
                    <Value />
                    <Sequence />
                  </DElement>
                  <DElement>
                    <VR>LO</VR>
                    <Tag>00080104</Tag>
                    <Value />
                    <Sequence />
                  </DElement>
                </DElementList>
                -->
              </Sequence>
            </DElement>
            <DElement>
              <!--ScheduledProcedureStepID-->
              <VR>SH</VR>
              <Tag>00400009</Tag>
              <Value>12270488</Value>
              <Sequence />
            </DElement>
          </DElementList>
        </Sequence>
      </DElement>
      <DElement>
        <!--RequestedProcedureID-->
        <VR>SH</VR>
        <Tag>00401001</Tag>
        <Value>26984439</Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--RequestedProcedureDescription-->
        <VR>LO</VR>
        <Tag>00321060</Tag>
        <Value>RequestedProcedureDescription</Value>
        <Sequence />
      </DElement>
      <!--
      <DElement>
        <VR>SQ</VR>
        <Tag>00321064</Tag>
        <Value />
        <Sequence>
          <DElementList>
            <DElement>
              <VR>SH</VR>
              <Tag>00080100</Tag>
              <Value />
              <Sequence />
            </DElement>
          </DElementList>
        </Sequence>
      </DElement>
      -->
      <DElement>
        <!--StudyInstanceUID-->
        <VR>UI</VR>
        <Tag>0020000D</Tag>
        <Value>1.2.40.0.13.0.10.36.243.242.5340867.1305198179359.32771</Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--AccessionNumber-->
        <VR>SH</VR>
        <Tag>00080050</Tag>
        <Value>37773566</Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--ReferringPhysiciansName-->
        <VR>PN</VR>
        <Tag>00080090</Tag>
        <Value></Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--PatientName-->
        <VR>PN</VR>
        <Tag>00100010</Tag>
        <Value>
          <xsl:value-of select="/DElementList/DElement[Tag='00100010']/Value"/>
        </Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--PatientID-->
        <VR>LO</VR>
        <Tag>00100020</Tag>
        <Value>51903644</Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--PatientBirthDate-->
        <VR>DA</VR>
        <Tag>00100030</Tag>
        <Value></Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--PatientSex-->
        <VR>CS</VR>
        <Tag>00100040</Tag>
        <Value></Value>
        <Sequence />
      </DElement>
      <DElement>
        <!--ReferencedStudySequence-->
        <VR>SQ</VR>
        <Tag>00081110</Tag>
        <Value/>
        <Sequence />
      </DElement>
      <DElement>
        <!--ReferencedPatientSequence-->
        <VR>SQ</VR>
        <Tag>00081120</Tag>
        <Value/>
        <Sequence />
      </DElement>
      <DElement>
        <!--DataSetType-->
        <VR>US</VR>
        <Tag>00000800</Tag>
        <Value>65278</Value>
        <Sequence />
      </DElement>
    </DElementList>
  </xsl:template>
</xsl:stylesheet>
