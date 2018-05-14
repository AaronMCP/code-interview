using System;
using System.Collections.Generic;

namespace HYS.Common.Objects.Config
{
    /// <summary>
    /// These parameter are use by adapter developer to write scripts.
    /// When adapter device is used to install an interface on IM GUI,
    /// IM will replace these parameter by up-to-date value of the interface
    /// in the script files.
    /// </summary>
    public class IMParameter
    {
        public const string InterfaceID = "[%InterfaceID%]";
        public const string InterfaceName = "[%InterfaceName%]";
        public const string InterfaceDescription = "[%InterfaceDescription%]";
        public const string InterfaceDirectory = "[%InterfaceDirectory%]";
        public const string ReferenceDeviceName = "[%ReferenceDeviceName%]";
        public const string ReferenceDeviceID = "[%ReferenceDeviceID%]";
        public const string ConfigDBConnection = "[%ConfigDBConnection%]";
        public const string DataDBConnection = "[%DataDBConnection%]";
        public const string ServiceName = "[%ServiceName%]";
        public const string IMCaption = "[%IMCaption%]";

        private static string[] _list;
        public static string[] List
        {
            get
            {
                if (_list == null)
                {
                    _list = new string[]
                    {
                        InterfaceID,
                        InterfaceName,
                        InterfaceDescription,
                        InterfaceDirectory,
                        ReferenceDeviceName,
                        ReferenceDeviceID,
                        //
                        //database connection may be changed after installing interface,
                        //after installation, interface will receive database connnection from IAdapterBase.Initialize().
                        //
                        //ConfigDBConnection,
                        //DataDBConnection,
                        ServiceName,
                        IMCaption,
                    };
                }
                return _list;
            }
        }
    }
}