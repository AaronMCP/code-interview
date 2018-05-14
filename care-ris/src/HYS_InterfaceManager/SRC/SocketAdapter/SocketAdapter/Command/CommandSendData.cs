using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HYS.SocketAdapter.Command
{


    public class CommandSendData
    {

        public CommandSendData()
        {
            _PacketHead.PacketType = PacketType;
        }

        public CommandSendData(CommandBase.CommandTypeEnum cmdType)
        {
            _CommandType = cmdType;
            _PacketHead.PacketType = PacketType;
        }


        #region Calss Property
        public  const CommandBase.CommandPacketTypeEnum PacketType = CommandBase.CommandPacketTypeEnum.cptSendData;
        PacketHead _PacketHead;
        public PacketHead PacketHead
        {
            get { return _PacketHead; }
            set { _PacketHead = value;}
        }

        CommandBase.CommandTypeEnum _CommandType;
        public CommandBase.CommandTypeEnum CommandType
        {
            get { return _CommandType; }
            set { _CommandType = value; }
        }

       

        string _CommandGUID = "";
        public string CommandGUID
        {
            get { return _CommandGUID; }
            set { _CommandGUID = value; }
        }

        //Hashtable _Params;
        Dictionary<string, string> _Params = new Dictionary<string, string>();


        //System.Collections.
        public Dictionary<string, string> Params
        {
            get { return _Params; }
        }
        #endregion

        public bool AddParameter(string Name, string Value)
        {
            _Params.Add(Name, Value);
            return true;
        }

        public void DeleteParameter(string Name)
        {
            _Params.Remove(Name);
        }

        public void ClearParameters()
        {
            _Params.Clear();
        }


        #region Most Importment Method   
        public string EncodePackage()
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("<>");

            #region Packet Head 
            sbMsg.Append(CommandBase.EncodePacketHead( PacketHead ));            
            #endregion

            #region Command Head
            sbMsg.Append( "<" );
            sbMsg.Append( "Command type=" + ((int)CommandType).ToString() );
            sbMsg.Append( "%CommandGUID=" + CommandGUID );            
            sbMsg.Append( ">" );
            #endregion

            #region Parameters
            foreach (string name in Params.Keys)
            {
                sbMsg.Append("<Paramname="+name.Trim());
                sbMsg.Append("%ParamValue=" + CommandBase.FilterParamValue(Params[name].ToString().Trim()));
                sbMsg.Append(">");
            }
            #endregion

            string sMsg = sbMsg.ToString();

            return sMsg;

        }

        public bool DecodePackage( string sMsg )
        {
           
            int bPos;
            // Decode Packet Head
            PacketHead = CommandBase.DecodePacketHead(sMsg);
            if (PacketHead.PacketType != PacketType) return false;

            // Decode Command Type
            bPos = sMsg.IndexOf("Command type");
            string sCommandType = CommandBase.ExtractValue(sMsg, bPos).Trim();
            _CommandType = (CommandBase.CommandTypeEnum)Convert.ToInt32(sCommandType);

            bPos = sMsg.IndexOf("CommandGUID");
            _CommandGUID = CommandBase.ExtractValue(sMsg, bPos);
            
            // Decode Parameters
            bPos = 0;
            this.ClearParameters();
            while (true)
            {
                bPos = sMsg.IndexOf("Paramname", bPos);
                if (bPos < 0) break;

                string name = CommandBase.ExtractValue(sMsg, bPos);
                bPos = sMsg.IndexOf("ParamValue", bPos);
                string value = CommandBase.AntiFilterParameterValue(CommandBase.ExtractValue(sMsg, bPos));
                _Params.Add(name, value);

            }
            return true;
        }

        #endregion
    }

    

    /*
    int m_nType;
    int m_nResult;

    public Command( : m_nType  (CMD_UNKNOWN),
                       m_nResult(RESULT_UNKNOWN)
{
}

CCommand::CCommand(short          nType,
				   const tstring& strGUID,
				   const tstring& strFrom,
				   const tstring& strTo) : m_nType  (nType),
				                           m_nResult(RESULT_UNKNOWN),
										   m_strGUID(strGUID),
										   m_strFrom(strFrom),
										   m_strTo  (strTo)
{
}

bool CCommand::AddParameter(const tstring& strName,
							const tstring& strValue)
{
	return m_params.insert(make_pair(strName, strValue)).second;
}

bool CCommand::SerialInCommand(const tstring& strCmd)
{
	size_t nPos = strCmd.find(_T('>'));

	return tstring::npos != nPos &&
		   SerialInHead(strCmd.substr(0, nPos+1)) &&
		   SerialInParams(strCmd.substr(nPos+1, strCmd.size()-nPos-1));
}


tstring CCommand::SerialOutCommand()
{
	return SerialOutHead()+SerialOutParams();
}

tstring CCommand::SerialOutResult()
{
	tstring strResult;

	strResult += _T("<Command come from=");
	strResult += m_strFrom;
	strResult += _T("><CommandGUID=");
	strResult += m_strGUID;
	strResult += _T("%Result=");
	strResult += CommonFunction::LongToStr(m_nResult);
	strResult += _T(">");

	return strResult;
}

tstring CCommand::SerialOutHead()
{
	tstring strHead;

	strHead += _T("<Command type=");
	strHead += IntToStr(m_nType);
	strHead += _T("%CommandGUID=");
	strHead += m_strGUID;
	strHead += _T(">");

	return strHead;
}

tstring CCommand::SerialOutParams()
{
	tstring strParam;
	Params::iterator it;
	tstring strParamValue;

	for (it = m_params.begin(); it != m_params.end(); ++it)
	{
		strParam += _T("<Paramname=");
		strParam += it->first;
		strParam += _T("%ParamValue=");

		//Transfer '&', '=', '%' which exists in parameter value.
		//add by quinn 2005-06-23
		strParamValue = it->second;
		FilterParamValue(strParamValue);
		strParam += strParamValue;

//		strParam += it->second;
		strParam += _T(">");
	}

	return strParam;
}

bool CCommand::SerialInHead(const tstring& strHead)
{
	size_t bPos, ePos;

	bPos      = strHead.find(_T('='));
	ePos      = strHead.find(_T('%'));
	if (tstring::npos == bPos || tstring::npos == ePos) return false;
	m_nType   = atoi(strHead.substr(bPos+1, ePos-bPos-1).data());
	
	bPos      = strHead.rfind(_T('='));
	ePos      = strHead.rfind(_T('>'));
	if (tstring::npos == bPos || tstring::npos == ePos) return false;
	m_strGUID = strHead.substr(bPos+1, ePos-bPos-1);

	return true;
}

bool CCommand::SerialInParams(tstring& strParams)
{
	EmptyParams();

	size_t  pos, pos1, pos2, pos3, pos4;
	tstring strParam, strName, strValue;

	for (; !strParams.empty();)
	{
		pos      = strParams.find(_T('>'));
		if (tstring::npos == pos) return false;
		strParam = strParams.substr(0, pos+1);
		pos1     = strParam.find(_T('='));
		pos2     = strParam.find(_T('%'));
		pos3     = strParam.rfind(_T('='));
		pos4     = strParam.rfind(_T('>'));
		if (tstring::npos == pos1 || tstring::npos == pos2 ||
			tstring::npos == pos3 || tstring::npos == pos4) return false;
		strName  = strParam.substr(pos1+1, pos2-pos1-1);
		strValue = strParam.substr(pos3+1, pos4-pos3-1);

		AntiFilterParameterValue(strValue); //add by quinn 2005-06-23
		
		m_params.insert(make_pair(strName, strValue));
		strParams.erase(0, pos+1);
	}

	return true;
}

void CCommand::EmptyParams()
{
	m_params.erase(m_params.begin(), m_params.end());
}

//filter &, =, % which exists in paramvalues
void CCommand::FilterParamValue(tstring &str)
{
	tstring strOld;
	tstring strNew;

	//replace '&' with "&38;"
	strOld = "&";
	strNew = "&38;";
	Replace(str,strOld,strNew);

	//replace '=' with "&61;"
	strOld = "=";
	strNew = "&61;";
	Replace(str,strOld,strNew);

	//replace '%' with "&37;"
	strOld = "%";
	strNew = "&37;";
	Replace(str,strOld,strNew);

	//replace '<' with "&60;"
	strOld = "<";
	strNew = "&60;";
	Replace(str,strOld,strNew);

	//replace '>' with "&62;"
	strOld = ">";
	strNew = "&62;";
	Replace(str,strOld,strNew);
}

//Transfer "&37;", "&61;", "&38;" back to '%', '=', '&'
//and change character from "'" to "''"
void CCommand::AntiFilterParameterValue(tstring &str)
{
	tstring strOld;
	tstring strNew;

	//replace "&37;" with '%'
	strOld = "&37;";
	strNew = "%";
	Replace(str,strOld,strNew);

	//replace "&61;" with '='
	strOld = "&61;";
	strNew = "=";
	Replace(str,strOld,strNew);

	//replace "&38;" with '&'
	strOld = "&38;";
	strNew = "&";
	Replace(str,strOld,strNew);

	//replace ' with ''
	strOld = "'";
	strNew = "''";
	Replace(str,strOld,strNew);

	//replace "&60;" with '<'
	strOld = "&60;";
	strNew = "<";
	Replace(str,strOld,strNew);

	//replace "&62;" with '>'
	strOld = "&62;";
	strNew = ">";
	Replace(str,strOld,strNew);
}

void CCommand::CFunction::GetParams(Params &paramsOut, Params &paramsIn)
{
//Get params from fnPACSQCNotify to fnImageArrival
	tstring strFirst, strSecond;
	Params::iterator it = paramsIn.find(GS_PARAM_PATIENTID);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);
	}
	else
	{
		strFirst = GS_PARAM_PATIENTID;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}

	it = paramsIn.find(GS_PARAM_ACCESSIONNUMBER);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);
	}
	else
	{
		strFirst = GS_PARAM_ACCESSIONNUMBER;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}

	it = paramsIn.find(GS_PARAM_MODALITYNAME);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);
	}
	else
	{
		strFirst = GS_PARAM_MODALITYNAME;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}

	it = paramsIn.find(GS_PARAM_OPERATORNAME);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);
	}
	else
	{
		strFirst = GS_PARAM_OPERATORNAME;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}

	it = paramsIn.find(GS_PARAM_PERFORMED_START_DT);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);
	}
	else
	{
		strFirst = GS_PARAM_PERFORMED_START_DT;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}

	it = paramsIn.find(GS_PARAM_PERFORMED_END_DT);
	if(it != paramsIn.end())
	{
		paramsOut.insert(*it);	
	}
	else
	{
		strFirst = GS_PARAM_PERFORMED_END_DT;
		strSecond = "";
		paramsOut.insert(make_pair(strFirst,strSecond));
	}
}

*/
}
   



