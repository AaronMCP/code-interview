using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HYS.RdetAdapter.Common
{
    #region Base class
    public class CommandToken
    {
        public const string CommandHeadToken = "Command";
        public const string ErrorCodeHeadToken = "ErrorCode";

        public const string StudyInstanceUID = "StudyInstanceUID";

        static public bool IsErrorCodeToken(string sToken)
        {
            return sToken.Trim().ToLower() == ErrorCodeHeadToken.ToLower();
        }

        static public bool IsCommandToken(string sToken)
        {
            return sToken.Trim().ToLower() == CommandHeadToken.ToLower();
        }
                

        public const string NewPatient = "NewPatient";
        public const string UpdatePatient = "UpdatePatient";
        public const string NewImage = "NewImage";
        public const string GetScannerStatus = "GetScannerStatus";
        public const string GetBodyParts = "GetBodyParts";
        public const string GetProjections = "GetProjections";
        public const string GetLocale = "GetLocale";

        static public bool IsNewPatient( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == NewPatient.ToLower();
        }

        static public bool IsUpdatePatient( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == UpdatePatient.ToLower();
        }

        static public bool IsNewImage( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == NewImage.ToLower();
        }

        static public bool IsGetScannerStatus( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == GetScannerStatus.ToLower();
        }

        static public bool IsGetBodyParts(string sCmdToken)
        {
            return sCmdToken.Trim().ToLower() == GetBodyParts.ToLower();
        }


        static public bool IsGetProjections( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == GetProjections.ToLower();
        }

         static public bool IsGetLocale( string sCmdToken )
        {
            return sCmdToken.Trim().ToLower() == GetLocale.ToLower();
        }

        
    }

    public class CmdReqBase
    {
        public  CmdReqBase()
        { }
        public  CmdReqBase(string sCmd)
        {
            _Command = sCmd;
        }

        //Hashtable ht = new Hashtable();
        List<System.Collections.Generic.KeyValuePair<string,string>> ht = new List<System.Collections.Generic.KeyValuePair<string,string>>();
      
        string _Command ="";
        public string Command 
        {
            get { return _Command;}
            set { _Command = value;}
        }

        

        public void AddParameter(string PName, string PValue)
        {
            if (ValidParm(PName, PValue))
            {
                //ht.Add(new KeyValuePName, PValue);
                KeyValuePair<string,string> item = new KeyValuePair<string,string>(PName,PValue);                
                ht.Add(item);
            }
        }

        public int GetParamCount()
        {
            return ht.Count;
        }

        public string GetParamValue(int index)
        {
            if (index >= ht.Count)
            {
                throw new Exception("Index Exceed border!");
            }
            KeyValuePair<string, string> item = ht[index];
            return item.Value.ToString();
        }

        public string GetParamName(int index)
        {
            if (index >= ht.Count)
            {
                throw new Exception("Index Exceed border!");
            }
            KeyValuePair<string, string> item = ht[index];
            return item.Key.ToString();
        }

        

        public void ClearParameters()
        {
            ht.Clear();
        }

        

        public void DeleteParam(int index)
        {
            ht.RemoveAt(index);
        }

        virtual public bool ValidParm(string PName, string PValue)
        {
            return true;
        }
    }

    public class CmdRespBase
    {
        //System.Collections.Hashtable ht = new System.Collections.Hashtable();        
        List<System.Collections.Generic.KeyValuePair<string, string>> ht = new List<System.Collections.Generic.KeyValuePair<string,string>>();

        string _ErrorCode = "";
        public string ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        public void AddParameter(string PName, string PValue)
        {
            if (ValidParm(PName, PValue))
            {
                //ht.Add(new KeyValuePName, PValue);
                KeyValuePair<string, string> item = new KeyValuePair<string, string>(PName, PValue);
                ht.Add(item);
            }
        }

        public int GetParamCount()
        {
            return ht.Count;
        }

        public string GetParamValue(string sPName)
        {
            foreach (KeyValuePair<string, string> item in ht)
            {
                if (item.Key.ToUpper() == sPName.ToUpper())
                    return item.Value;
            }
            return "";
        }

        public string GetParamValue(int index)
        {
            if (index >= ht.Count)
            {
                throw new Exception("Index Exceed border!");
            }
            KeyValuePair<string, string> item = ht[index];
            return item.Value.ToString();
        }

        public string GetParamName(int index)
        {
            if (index >= ht.Count)
            {
                throw new Exception("Index Exceed border!");
            }
            KeyValuePair<string, string> item = ht[index];
            return item.Key.ToString();
        }



        public void ClearParameters()
        {
            ht.Clear();
        }



        public void DeleteParam(int index)
        {
            ht.RemoveAt(index);
        }

        virtual public bool ValidParm(string PName, string PValue)
        {
            return true;
        }
        
    }
    #endregion

   

    #region NewPatient
    public class CmdReqNewPatient :CmdReqBase
    {
        public CmdReqNewPatient() : base( CommandToken.NewPatient )
        {
            
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }

        
    }

    public class CmdRespNewPatient :CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
              

    }

    #endregion

    #region UpdatePatient
    public class CmdReqUpdatePatient : CmdReqBase
    {
        public CmdReqUpdatePatient() : base( CommandToken.UpdatePatient )
        {
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespUpdatePatient : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion


    #region NewImage
    public class CmdReqNewImage : CmdReqBase
    {
        public CmdReqNewImage() : base( CommandToken.NewImage )
        {
        }
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespNewImage : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion

    #region GetScannerStatus
    public class CmdReqGetScannerStatus : CmdReqBase
    {
        public CmdReqGetScannerStatus()
            : base(CommandToken.GetScannerStatus)
        {
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespGetScannerStatus : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion

    #region GetBodyParts
    public class CmdReqGetBodyParts : CmdReqBase
    {
        public CmdReqGetBodyParts()
            : base(CommandToken.GetBodyParts)
        {
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespGetBodyParts : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion

    #region GetProjections
    public class CmdReqGetProjections : CmdReqBase
    {
        public CmdReqGetProjections()
            : base(CommandToken.GetProjections)
        {
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespGetProjections : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion

    #region GetLocale
    public class CmdReqGetLocale : CmdReqBase
    {
        public CmdReqGetLocale()
            : base(CommandToken.GetLocale)
        {
        }

        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }

    public class CmdRespGetLocale : CmdRespBase
    {
        public override bool ValidParm(string PName, string PValue)
        {
            //TODO: ADD VALID CODE HERE
            return base.ValidParm(PName, PValue);
        }
    }
    #endregion
}
