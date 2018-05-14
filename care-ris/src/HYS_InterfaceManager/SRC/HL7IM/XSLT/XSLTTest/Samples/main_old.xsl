<xsl:stylesheet version="1.1" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:ris="urn:hl7-org:v2xml" 
                xmlns:csb="http://www.carestream.com/csbroker"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:myscript="urn:myscript"
                exclude-result-prefixes="myscript">

  <msxsl:script language="C#" implements-prefix="myscript">
    <![CDATA[
    public string GetAge(string birthDate, string examDate)
    {
        if (birthDate == null || examDate == null) return "";
        
        string strBirth = birthDate.Trim();
        string strExam = examDate.Trim();
        if (strBirth.Length < 8) return "";
        if (strExam.Length < 8) return "";
        
        if (strBirth.Length > 8) strBirth=strBirth.Substring(0, 8);
        if (strExam.Length > 8) strExam=strExam.Substring(0, 8);

        DateTime dtBirth;
        if (!DateTime.TryParseExact(strBirth.Substring(0, 8), "yyyyMMdd",
            null, System.Globalization.DateTimeStyles.None, out dtBirth))
            return "";

        DateTime dtExam;
        if (!DateTime.TryParseExact(strExam.Substring(0, 8), "yyyyMMdd",
            null, System.Globalization.DateTimeStyles.None, out dtExam))
            return "";

        int year = dtExam.Year - dtBirth.Year;
        if (year > 0) return year.ToString() + "Y";
        else if (year < 0) return "";

        TimeSpan span = dtExam.Subtract(dtBirth);
        if (span.Days > 30) return ((int)(span.Days / 30)).ToString() + "M";
        else if (span.Days > 0) return span.Days.ToString() + "D";
        else return "";
    }
    public string Get8Date(string strDate)
    {
        if( strDate.Length < 8 )
          return( strDate );
        else
          return( strDate.Substring(0, 8) );
    }
    public string GetExamStatus(string ris_type, string reg_date)
    {
        string ret = "NONE";

        if( ris_type == "A" )
          ret = "A";
        else
        if( ris_type == "CA" )
          ret = "CANCELED";
        else
        if( ris_type == "CM" )
          ret = "READY";
        else
        if( ris_type == "DC" )
          ret = "DISCONTINUED";
        else
        if( ris_type == "ER" )
          ret = "ERROR";
        else
        if( ris_type == "HD" )
          ret = "HOLD";
        else
        if( ris_type == "IP" )
          ret = "READY";
        else
        if( ris_type == "RP" )
          ret = "READY";
        else
        if( ris_type == "SC" && reg_date != "" && reg_date != null )
          ret = "READY";
        else
        if( ris_type == "SC" && (reg_date == "" || reg_date == null) )
          ret = "SCHEDULED";
        else
          ret = "reg_date";

        if( ret == "READY" && (reg_date == "" || reg_date == null) )
          ret = "SCHEDULED";

        return( ret );
    }
    ]]>
  </msxsl:script>

	<xsl:template match="/">
		<Message>
			<xsl:copy-of select="/Message/Header"/>
			<!--<xsl:apply-templates select="//Header"/>		-->
			<xsl:apply-templates select="//Body"/>
		</Message>
	</xsl:template>
	<xsl:template match="//Header">
		<xsl:copy/>
	</xsl:template>
	<xsl:template match="//Body">
		<Body>
			<!-- 
    For each type of HL7v2 message, create a xsl template for it.
    Then include the template into this file, and call it in the following choose..when statement.
    
    Note: According to HL7v3 Web Services Profile (http://www.hl7.org/v3ballot/html/infrastructure/transport/transport-wsprofiles.html),
    under the Body element there should be a single element, which is the top-level element of HL7 XML message.
    -->
			<xsl:for-each select="//Body/*">
				<xsl:choose>
					<xsl:when test="name()= 'ORM_O01'">
						<xsl:choose>
							<xsl:when test="ORC != ''">
								<csb:Order>
									<xsl:call-template name="ORM_O01"/>
								</csb:Order>
							</xsl:when>
							<xsl:otherwise>
								<csb:Patient>
									<xsl:call-template name="ADT_A01"/>
								</csb:Patient>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A01'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A04'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A05'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A08'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A40'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A47'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ADT_A31'">
						<csb:Patient>
							<xsl:call-template name="ADT_A01"/>
						</csb:Patient>
					</xsl:when>
					<xsl:when test="name()= 'ORU_R01'">
						<!--<csb:Report>
							<xsl:call-template name="ORU_R01"/>
						</csb:Report>-->

						<xsl:choose>
							<xsl:when test="OBR != ''">
								<csb:Report>
									<xsl:call-template name="ORU_R01"/>
								</csb:Report>
							</xsl:when>
							<xsl:otherwise>
								<csb:Patient>
									<xsl:call-template name="ADT_A01"/>
								</csb:Patient>
							</xsl:otherwise>
						</xsl:choose>

					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</Body>
	</xsl:template>
	<xsl:template name="ADT_A01">
		<xsl:for-each select="PID">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				<xsl:call-template name="PATIENT"/>
			</csb:table>
		</xsl:for-each>
	</xsl:template>
	<!--<xsl:template name="ORM_O01">
		<xsl:for-each select="OBR">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				
				<xsl:call-template name="PATIENT"/>
				
				<xsl:call-template name="ORDER"/>
			</csb:table>
		</xsl:for-each>
	</xsl:template>-->
	<xsl:template name="ORU_R01">
	<xsl:for-each select="OBR">
			<csb:table>
				<xsl:call-template name="DATAINDEX"/>
				
				<xsl:call-template name="PATIENT"/>
				<xsl:call-template name="ORDER"></xsl:call-template>				
				
				<xsl:call-template name="REPORT"></xsl:call-template>
			</csb:table>
		</xsl:for-each>
</xsl:template>

  <xsl:template name="DATAINDEX">
   
          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
            <xsl:choose>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A01' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A04' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A05'">
                <xsl:text>00</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A08' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A31'">
                <xsl:text>01</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A40' or ../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A47'">
                <xsl:text>02</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'NW'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'CH'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'SN'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'RO'">
                <xsl:text>10</xsl:text>
              </xsl:when>
               <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'XO'">
                <xsl:text>11</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ../ORC/FIELD.1/FIELD_ITEM = 'DC'">
                <xsl:text>13</xsl:text>
              </xsl:when>
              <xsl:when test="../MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'R01' and ../OBR != ''">
                <xsl:text>30</xsl:text>
              </xsl:when>
		<xsl:otherwise>
    			<xsl:text>01</xsl:text>
		</xsl:otherwise>

            </xsl:choose>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_PROCESS_FLAG">
            <xsl:text>0</xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_1">
            <xsl:value-of select="../MSH/FIELD.10"/>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_2">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_3">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_4">
            <xsl:text></xsl:text>
          </xsl:element>

           
  </xsl:template>

	<xsl:template name="PATIENT">
		
<!--		<xsl:param name="PatName"></xsl:param>
			<xsl:for-each select="../PID/FIELD.5/FIELD_ITEM[1]/*[1]">
				<xsl:call-template name="GetPatName">
				<xsl:with-param name="PatName" select="concat($PatName,.)"/>
			</xsl:call-template>
			</xsl:for-each>
				-->
				
		<xsl:element name="csb:PATIENT_ACCOUNT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ADDRESS">
			<xsl:value-of select="../PID/FIELD.11/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BED_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTH_PLACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTHDATE">
			<!--<xsl:value-of select="../PID/FIELD.7/FIELD_ITEM/COMPONENT.1"/>-->
      <xsl:value-of select="myscript:Get8Date(PID/FIELD.7/FIELD_ITEM/COMPONENT.1)"/>      
		</xsl:element>
		<xsl:element name="csb:PATIENT_CITIZENSHIP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_COUNTRY_CODE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_1">
			  <!--<xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC[1]/FIELD.9/FIELD_ITEM/COMPONENT.1)"/>-->
        <xsl:choose>
            <xsl:when test="ORC/FIELD.7/FIELD_ITEM != ''">
                <xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC/FIELD.7/FIELD_ITEM)"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC/FIELD.9/FIELD_ITEM)"/>
            </xsl:otherwise>
        </xsl:choose>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_2">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_3">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_4">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_DRIVERLIC_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ETHNIC_GROUP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MARITAL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MOTHER_MAIDEN_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_NATIONALITY">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_OTHER_PID">
		<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_ALIAS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCAL_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCATION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_NAME">
		
			<xsl:value-of select="../PID/FIELD.5/FIELD_ITEM/*"></xsl:value-of>
<!--			<xsl:value-of select="$PatName"/>-->
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_TYPE">
			<xsl:value-of select="../PV1/FIELD.2/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENTID">
			
			<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
			<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
			<xsl:value-of select="../PID/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
			<xsl:value-of select="../PID/FIELD.13/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIMARY_LANGUAGE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_ID">
			<xsl:value-of select="../MRG/FIELD.1/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_VISIT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RELIGION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SEX">
			<xsl:value-of select="../PID/FIELD.8/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SSN_NUMBER">
			<xsl:value-of select="../PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VISIT_NUMBER">
			<xsl:value-of select="../PV1/FIELD.19/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
	</xsl:template>

  <xsl:template name="ORDER">   

        <xsl:element name="csb:ORDER_BODY_PART">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_AMOUNT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_STATUS">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CNT_AGENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_1">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_2">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_3">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_4">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_DURATION">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_COMMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_DT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_LOCATION">
          <xsl:value-of select="FIELD.19/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_REQUIREMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_STATUS">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_VOLUME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_DEPARTMENT">
          <xsl:value-of select="FIELD.19/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_NO">
          <xsl:value-of select="FIELD.18/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_MODALITY">
          <xsl:value-of select="FIELD.24/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_ORDER_NO">
          <xsl:value-of select="FIELD.2/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PATIENT_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER">
          <xsl:value-of select="FIELD.16/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_DEPARTMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_NO">
          <xsl:value-of select="FIELD.3/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_CODE">
          <xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_DESC">
          <!--<xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.2"></xsl:value-of>-->

      <xsl:variable name="str10" select="FIELD.45/FIELD_ITEM/COMPONENT.2"/>
      <xsl:variable name="str11">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str10)"/>
          <xsl:with-param name="charsIn" select="'\T\'"/>
          <xsl:with-param name="charsOut" select="'&amp;'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str12">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str11)"/>
          <xsl:with-param name="charsIn" select="'\R\'"/>
          <xsl:with-param name="charsOut" select="'~'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str13">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str12)"/>
          <xsl:with-param name="charsIn" select="'\S\'"/>
          <xsl:with-param name="charsOut" select="'^'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str14">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str13)"/>
          <xsl:with-param name="charsIn" select="'\F\'"/>
          <xsl:with-param name="charsOut" select="'|'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:value-of select="$str14"/>
		</xsl:element>                 
          
        <xsl:element name="csb:ORDER_PROCEDURE_NAME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_CLASS_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_ORGANIZATION">
          <xsl:value-of select="FIELD.16/FIELD_ITEM/COMPONENT.14"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_PHYSICIAN">
          <xsl:value-of select="FIELD.16/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REQUEST_REASON">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REUQEST_COMMENTS">
          <xsl:value-of select="FIELD.13/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_SCHEDULED_DT">
          <xsl:value-of select="preceding-sibling::ORC[1]/FIELD.7/FIELD_ITEM/COMPONENT.4"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_SERIES_NO">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_AETITLE">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_NAME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_INSTANCE_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TECHNICIAN">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TRANSPORT_ARRANGE">
          <xsl:text></xsl:text>
        </xsl:element>

    
  </xsl:template>

  <xsl:template name="ORM_O01">
      <csb:table>

          <xsl:element name="csb:DATAINDEX_DATA_SOURCE">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_EVENT_TYPE">
            <xsl:choose>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A01' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A04' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A05'">
                <xsl:text>00</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A08' or MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A31'">
                <xsl:text>01</xsl:text>
              </xsl:when>
	      <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'A40'">
                <xsl:text>02</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'NW'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'CH'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'SN'">
                <xsl:text>10</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'RO'">
                <xsl:text>10</xsl:text>
              </xsl:when>
               <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'XO'">
                <xsl:text>11</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'O01' and ORC/FIELD.1/FIELD_ITEM = 'DC'">
                <xsl:text>12</xsl:text>
              </xsl:when>
              <xsl:when test="MSH/FIELD.9/FIELD_ITEM/COMPONENT.2 = 'R01' ">
                <xsl:text>30</xsl:text>
              </xsl:when>
	      <xsl:otherwise>
                <xsl:text>01</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_PROCESS_FLAG">
            <xsl:text>0</xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_1">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_2">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_3">
            <xsl:text></xsl:text>
          </xsl:element>
          <xsl:element name="csb:DATAINDEX_RECORD_INDEX_4">
            <xsl:text></xsl:text>
          </xsl:element>


		<xsl:element name="csb:PATIENT_ACCOUNT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ADDRESS">
			<xsl:value-of select="PID/FIELD.11/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BED_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTH_PLACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_BIRTHDATE">
			<!--<xsl:value-of select="../PID/FIELD.7/FIELD_ITEM/COMPONENT.1"/>-->
      <xsl:value-of select="myscript:Get8Date(PID/FIELD.7/FIELD_ITEM/COMPONENT.1)"/>      
		</xsl:element>
		<xsl:element name="csb:PATIENT_CITIZENSHIP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_COUNTRY_CODE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_1">
			  <!--<xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC[1]/FIELD.9/FIELD_ITEM/COMPONENT.1)"/>-->
        <xsl:choose>
            <xsl:when test="ORC/FIELD.7/FIELD_ITEM != ''">
                <xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC/FIELD.7/FIELD_ITEM)"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="myscript:GetAge(PID/FIELD.7/FIELD_ITEM/COMPONENT.1,ORC/FIELD.9/FIELD_ITEM)"/>
            </xsl:otherwise>
        </xsl:choose>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_2">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_3">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_CUSTOMER_4">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_DRIVERLIC_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_ETHNIC_GROUP">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MARITAL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_MOTHER_MAIDEN_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_NATIONALITY">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_OTHER_PID">
		<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_ALIAS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCAL_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_LOCATION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_NAME">
		
			<xsl:value-of select="PID/FIELD.5/FIELD_ITEM/*"></xsl:value-of>
<!--			<xsl:value-of select="$PatName"/>-->
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENT_TYPE">
			<xsl:value-of select="PV1/FIELD.2/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PATIENTID">
			
			<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->	
			<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_BUSINESS">
			<xsl:value-of select="PID/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PHONENUMBER_HOME">
			<xsl:value-of select="PID/FIELD.13/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIMARY_LANGUAGE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_ID">
			<xsl:value-of select="MRG/FIELD.4/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_PATIENT_NAME">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_PRIOR_VISIT_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RACE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_RELIGION">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SEX">
			<xsl:value-of select="PID/FIELD.8/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_SSN_NUMBER">
			<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='9']/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VETERANS_MIL_STATUS">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:PATIENT_VISIT_NUMBER">
			<xsl:value-of select="PV1/FIELD.19/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>

        <xsl:element name="csb:ORDER_BODY_PART">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_AMOUNT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CHARGE_STATUS">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CNT_AGENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_1">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_2">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_3">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_CUSTOMER_4">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_DURATION">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_COMMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_DT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_LOCATION">
          <xsl:value-of select="OBR/FIELD.19/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_REQUIREMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_STATUS">
          <!--<xsl:text></xsl:text>-->
          <xsl:value-of select="myscript:GetExamStatus(ORC/FIELD.5/FIELD_ITEM,ORC/FIELD.9/FIELD_ITEM)"/>
      	<!--<xsl:choose>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'A'">
                  <xsl:text>NONE</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'CA'">
                  <xsl:text>CANCELED</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'CM'">
                  <xsl:text>READY</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'DC'">
                  <xsl:text>DISCONTINUED</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'ER'">
                  <xsl:text>ERROR</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'HD'">
                  <xsl:text>HOLD</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'IP'">
                  <xsl:text>READY</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'RP'">
                  <xsl:text>READY</xsl:text>
              </xsl:when>                                       
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'SC' and ORC/FIELD.9/FIELD_ITEM != '' ">
                  <xsl:text>READY</xsl:text>
              </xsl:when>
              <xsl:when test="ORC/FIELD.5/FIELD_ITEM = 'SC' and ORC/FIELD.9/FIELD_ITEM = '' ">
                  <xsl:text>SCHEDULED</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                  <xsl:value-of select="ORC/FIELD.9/FIELD_ITEM"></xsl:value-of>
              </xsl:otherwise>
          </xsl:choose>-->
        </xsl:element>
        <xsl:element name="csb:ORDER_EXAM_VOLUME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_DEPARTMENT">
          <xsl:value-of select="OBR/FIELD.19/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_FILLER_NO">
          <xsl:value-of select="OBR/FIELD.18/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_MODALITY">
          <xsl:value-of select="OBR/FIELD.24/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_ORDER_NO">
          <xsl:value-of select="OBR/FIELD.2/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PATIENT_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_DEPARTMENT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_PLACER_NO">
          <xsl:value-of select="OBR/FIELD.3/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_CODE">
	  <xsl:for-each select="OBR"><xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.1"/>|</xsl:for-each>
        </xsl:element>
        <xsl:element name="csb:ORDER_PROCEDURE_DESC">
          <!--<xsl:for-each select="OBR"><xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.2"/>|</xsl:for-each>-->
          
      <xsl:variable name="str20">
        <xsl:for-each select="OBR"><xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.2"/>|</xsl:for-each>
      </xsl:variable>
      <xsl:variable name="str21">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str20)"/>
          <xsl:with-param name="charsIn" select="'\T\'"/>
          <xsl:with-param name="charsOut" select="'&amp;'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str22">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str21)"/>
          <xsl:with-param name="charsIn" select="'\R\'"/>
          <xsl:with-param name="charsOut" select="'~'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str23">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str22)"/>
          <xsl:with-param name="charsIn" select="'\S\'"/>
          <xsl:with-param name="charsOut" select="'^'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str24">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str23)"/>
          <xsl:with-param name="charsIn" select="'\F\'"/>
          <xsl:with-param name="charsOut" select="'|'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:value-of select="$str24"/>
		</xsl:element>

        <xsl:element name="csb:ORDER_PROCEDURE_NAME">
          <!--<xsl:for-each select="OBR"><xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.2"/>|</xsl:for-each>-->

      <xsl:variable name="str30">
        <xsl:for-each select="OBR"><xsl:value-of select="FIELD.45/FIELD_ITEM/COMPONENT.2"/>|</xsl:for-each>
      </xsl:variable>
      <xsl:variable name="str31">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str30)"/>
          <xsl:with-param name="charsIn" select="'\T\'"/>
          <xsl:with-param name="charsOut" select="'&amp;'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str32">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str31)"/>
          <xsl:with-param name="charsIn" select="'\R\'"/>
          <xsl:with-param name="charsOut" select="'~'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str33">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str32)"/>
          <xsl:with-param name="charsIn" select="'\S\'"/>
          <xsl:with-param name="charsOut" select="'^'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str34">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str33)"/>
          <xsl:with-param name="charsIn" select="'\F\'"/>
          <xsl:with-param name="charsOut" select="'|'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:value-of select="$str34"/>
		</xsl:element>

        <xsl:element name="csb:ORDER_REF_CLASS_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_CONTACT">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_ORGANIZATION">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.14"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REF_PHYSICIAN">
          <xsl:value-of select="OBR/FIELD.16/FIELD_ITEM/COMPONENT.1"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REQUEST_REASON">
              <xsl:value-of select="ORC/FIELD.9/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_REUQEST_COMMENTS">
          <xsl:value-of select="OBR/FIELD.13/FIELD_ITEM"></xsl:value-of>
        </xsl:element>
        <xsl:element name="csb:ORDER_SCHEDULED_DT">
          <!--<xsl:value-of select="preceding-sibling::ORC[1]/FIELD.7/FIELD_ITEM/COMPONENT.4"></xsl:value-of>-->
	  <!--<xsl:value-of select="ORC/FIELD.7/FIELD_ITEM"></xsl:value-of>-->
			<!--<xsl:choose>
				<xsl:when test="./PID/FIELD.3/FIELD_ITEM/COMPONENT.5 !=''">
					<xsl:value-of select="PID/FIELD.3/FIELD_ITEM[COMPONENT.5='PATKEY']/COMPONENT.1"/>
				</xsl:when>
				<xs;xsl:otherwise>
				<xsl:value-of select="PID/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
				</xs;xsl:otherwise>
			</xsl:choose>	-->
      <!--
      	<xsl:choose>
        <xsl:when test="preceding-sibling::ORC[1]/FIELD.7/FIELD_ITEM/COMPONENT.3 != ''">
          <xsl:value-of select="preceding-sibling::ORC[1]/FIELD.7/FIELD_ITEM/COMPONENT.3"></xsl:value-of>
          </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="preceding-sibling::ORC[1]/FIELD.9/FIELD_ITEM/COMPONENT.3"></xsl:value-of>
            </xsl:otherwise>
          </xsl:choose>
      -->
      	<xsl:choose>
        <xsl:when test="ORC/FIELD.7/FIELD_ITEM != ''">
              <xsl:value-of select="ORC/FIELD.7/FIELD_ITEM"></xsl:value-of>
          </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="ORC/FIELD.9/FIELD_ITEM"></xsl:value-of>
            </xsl:otherwise>
          </xsl:choose>

        </xsl:element>
        <xsl:element name="csb:ORDER_SERIES_NO">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_AETITLE">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STATION_NAME">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_ID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_STUDY_INSTANCE_UID">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TECHNICIAN">
          <xsl:text></xsl:text>
        </xsl:element>
        <xsl:element name="csb:ORDER_TRANSPORT_ARRANGE">
          <xsl:text></xsl:text>
        </xsl:element>


       </csb:table>
  </xsl:template>

<!-- here is the template that does the replacement -->
<xsl:template name="replaceCharsInString">
  <xsl:param name="stringIn"/>
  <xsl:param name="charsIn"/>
  <xsl:param name="charsOut"/>
  <xsl:choose>
    <xsl:when test="contains($stringIn,$charsIn)">
      <xsl:value-of select="concat(substring-before($stringIn,$charsIn),$charsOut)"/>
      <xsl:call-template name="replaceCharsInString">
        <xsl:with-param name="stringIn" select="substring-after($stringIn,$charsIn)"/>
        <xsl:with-param name="charsIn" select="$charsIn"/>
        <xsl:with-param name="charsOut" select="$charsOut"/>
      </xsl:call-template>
    </xsl:when>
    <xsl:otherwise>
      <xsl:value-of select="$stringIn"/>
    </xsl:otherwise>
  </xsl:choose>
</xsl:template>

	<xsl:template match="OBX">
		<xsl:param name="ReportText" select="$ReportTex"/>
		<xsl:choose>
			<xsl:when test="count(following-sibling::OBX)>0">
				<xsl:apply-templates select="following-sibling::OBX">
					<xsl:with-param name="ReportText" select="concat($ReportText,' ',FIELD.5/FIELD_ITEM)"/>
				</xsl:apply-templates>
			</xsl:when>
			<xsl:otherwise><xsl:value-of select="concat($ReportText,'&#x0D;&#x0A;',FIELD.5/FIELD_ITEM)"></xsl:value-of></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REPORT">
		<xsl:param name="ReportText">
		<xsl:apply-templates select="../OBX[1]">
			<xsl:with-param name="ReportText"/>
		</xsl:apply-templates>
		</xsl:param>		


		<!--Report Table-->
		<xsl:element name="csb:REPORT_ACCESSION_NUMBER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_COMMENTS">
			<!--<xsl:value-of select="$ReportText"/>-->    
      <!--<xsl:value-of select="following-sibling::OBX/FIELD.5/FIELD_ITEM"/>-->

      <xsl:variable name="str0" select="$ReportText"/>
      <xsl:variable name="str1">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str0)"/>
          <xsl:with-param name="charsIn" select="'\T\'"/>
          <xsl:with-param name="charsOut" select="'&amp;'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str2">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str1)"/>
          <xsl:with-param name="charsIn" select="'\R\'"/>
          <xsl:with-param name="charsOut" select="'~'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str3">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str2)"/>
          <xsl:with-param name="charsIn" select="'\S\'"/>
          <xsl:with-param name="charsOut" select="'^'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:variable name="str4">
        <xsl:call-template name="replaceCharsInString">
          <xsl:with-param name="stringIn" select="string($str3)"/>
          <xsl:with-param name="charsIn" select="'\F\'"/>
          <xsl:with-param name="charsOut" select="'|'"/>
        </xsl:call-template>
      </xsl:variable>
      <xsl:value-of select="$str4"/>
		</xsl:element>

		<xsl:element name="csb:REPORT_CUSTOMER_1">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_2">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_3">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_CUSTOMER_4">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_DIAGNOSE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_MODALITY">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_OBSERVATIONMETHOD">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_PATIENT_ID">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_APPROVER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_FILE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_INTEPRETER">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_NO">
			<xsl:value-of select="../OBX/FIELD.3/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_STATUS">
			<xsl:value-of select="../OBX/FIELD.11/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_TYPE">
			<xsl:text/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORT_WRITER">
			<xsl:value-of select="../OBX/FIELD.16/FIELD_ITEM"/>
		</xsl:element>
		<xsl:element name="csb:REPORT_REPORTDT">
			<xsl:value-of select="../OBX/FIELD.14/FIELD_ITEM/COMPONENT.1"/>
		</xsl:element>

	</xsl:template>

</xsl:stylesheet>
