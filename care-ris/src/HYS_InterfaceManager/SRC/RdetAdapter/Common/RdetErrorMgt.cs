using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HYS.RdetAdapter.Common
{
     #region Error Code
    public struct RdetError
    {
        public int Code;
        public string ErrorDescription;
        public string Resolution;

        public RdetError(int AiCode, string AsErrorDescription, string AsResolution)
        {
            Code = AiCode;
            ErrorDescription = AsErrorDescription;
            Resolution = AsResolution;
        }
    }

    public class RdetErrorMgt
    {

        static bool IsInited = false;
        static System.Collections.Generic.Dictionary<int, RdetError> _dt = new Dictionary<int, RdetError>();
 
        static void Init()
        {
            
            _dt.Add(0, new RdetError(0, "	Success	", "		"));
            _dt.Add(-101, new RdetError(-101, "	Invalid Birth Date Format	", "	Valid format is YYYYMMDD	"));
            _dt.Add(-102, new RdetError(-102, "	Invalid Study Date Format	", "	Valid format is YYYYMMDD	"));
            _dt.Add(-103, new RdetError(-103, "	Invalid Study Time Format	", "	Valid format is HHMMSS	"));
            _dt.Add(-104, new RdetError(-104, "	Invalid Gender Format	", "	1 = Male, 2 = Female, 3 = Other	"));
            _dt.Add(-105, new RdetError(-105, "	Invalid Priority Format	", "	1 = Routine, 2 = Urgent, 3 = STAT	"));
            _dt.Add(-106, new RdetError(-106, "	Study Instance UID not supplied	", "	Supply this parameter	"));
            _dt.Add(-107, new RdetError(-107, "	No match found for Study Instance UID	", "	Patient deleted on CR800, or never saved.Recreate this patient.	"));
            _dt.Add(-108, new RdetError(-108, "	Duplicate Study Instance UID	", "	The NewPatient command failed, because a Study with that UID already exists.	"));
            _dt.Add(-110, new RdetError(-110, "	Patient Name Length	", "	Max Length = 206, including Single Byte, Ideographic, and Phonetic with ^¡¯s	"));
            _dt.Add(-111, new RdetError(-111, "	Patient ID Length	", "	Max Length = 16	"));
            _dt.Add(-112, new RdetError(-112, "	Accession Number Length	", "	Max Length = 16	"));
            _dt.Add(-113, new RdetError(-113, "	Procedure Name Length	", "	Max Length = 64	"));
            _dt.Add(-114, new RdetError(-114, "	Referring Physician Length	", "	Max Length = 206, including Single Byte, Ideographic, and Phonetic with ^¡¯s	"));
            _dt.Add(-115, new RdetError(-115, "	Department Length	", "	Max Length = 64	"));
            _dt.Add(-116, new RdetError(-116, "	Patient Location Length	", "	Max Length = 64	"));
            _dt.Add(-117, new RdetError(-117, "	Contrast Agent Length	", "	Max Length = 64	"));
            _dt.Add(-118, new RdetError(-118, "	Tech ID Length	", "	Max Length = 16	"));
            _dt.Add(-119, new RdetError(-119, "	Study Instance UID Length	", "	Max Length = 64  MinLength=24	"));
            _dt.Add(-120, new RdetError(-120, "	Procedure Code Length	", "	Max Length = 16	"));
            _dt.Add(-121, new RdetError(-121, "	Patient Comments Length	", "	Max Length = 256	"));
            _dt.Add(-122, new RdetError(-122, "	Study ID Length	", "	Max Length = 16	"));
            _dt.Add(-201, new RdetError(-201, "	Cassette ID Format	", "	10 digit number, beginning with 9	"));
            _dt.Add(-202, new RdetError(-202, "	Body Part Format	", "	Name must match a name returned by theGetBodyParts function.	"));
            _dt.Add(-203, new RdetError(-203, "	Projection Format	", "	Name must match a name returned by theGetProjections function.	"));
            _dt.Add(-204, new RdetError(-204, "	Position Format	", "	Name must match a valid Position.Currently not the case. Position must only be less than 16 characters.	"));
            _dt.Add(-205, new RdetError(-205, "	Orientation Format	", "	1 = Portrait, 2 = Landscape	"));
            _dt.Add(-206, new RdetError(-206, "	Laterality Format	", "	1 = Left, 2 = Right, 3 = Both, 4 = UnpairedUnpaired is for DX IOD only (not CR)Better to leave blank is unknown.	"));
            _dt.Add(-207, new RdetError(-207, "	Cassette ID Not Supplied	", "	Supply this parameter	"));
            _dt.Add(-208, new RdetError(-208, "	Duplicate Unavailable Cassette ID	", "	Nothing was done. Either cancel, or resubmit with the Override parameter	"));
            _dt.Add(-209, new RdetError(-209, "	Override Format	", "	0 or blank or not supplied to do nothing.1 for delete old unavailable image.	"));
            _dt.Add(-210, new RdetError(-210, "	Comments Length 	", "	Max Length = 256	"));
            _dt.Add(-211, new RdetError(-211, "	Position Length	", "	Max Length = 16	"));
            _dt.Add(-212, new RdetError(-212, "	KVP Range	", "	Max Length = 4	"));
            _dt.Add(-213, new RdetError(-213, "	mAS Range	", "	Max Length = 4	"));
            _dt.Add(-214, new RdetError(-214, "	Distance Range	", "	Max Length = 4	"));
            _dt.Add(-215, new RdetError(-215, "	Series Number Length	", "	Max Length = 12 (numeric)	"));
            _dt.Add(-216, new RdetError(-216, "	Acquisition Number Length	", "	Max Length = 12 (numeric)	"));
            _dt.Add(-217, new RdetError(-217, "	Derivation Description Length	", "	Max Length = 256	"));
            _dt.Add(-300, new RdetError(-300, "	Unexpected Parameter	", "	This parameter should not be sent with this command. The line following the ErrorCode=-300 will be of the form:InvalidParameter=ParameterName	"));
            _dt.Add(-301, new RdetError(-301, "	Command Not Supplied	", "	The first line was not Command=???	"));
            _dt.Add(-302, new RdetError(-302, "	Invalid Parameter Format	", "	The parameter was not of the formParameterName=ParameterValue	"));
            _dt.Add(-303, new RdetError(-303, "	Unknown Database error	", "	All the input parameters looked OK, but the database request returned an error.	"));
            _dt.Add(-304, new RdetError(-304, "	Could not build UID	", "	We tried to form a DICOM UID, but failed. Couldn¡¯t get the system IP Address or time.	"));
            _dt.Add(-305, new RdetError(-305, "	Blank Line	", "	A blank line was received. Were 4 consecutive NULL¡¯s sent, or were 2 NULL¡¯s sent in multi-byte mode.	"));
            _dt.Add(-306, new RdetError(-306, "	Option Not Enabled	", "	The user has not added the RDET option using the floppies.	"));
            
            IsInited = true;
        }

        static public RdetError GetRdetError(int ErrCode)
        {
            if (!IsInited)
                Init();
            return _dt[ErrCode];
        }
    }
    #endregion 
}
