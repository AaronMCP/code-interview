/***************************************************************************
* SentinelKey.cs
*
* (C) Copyright 2005 SafeNet, Inc. All rights reserved.
*
* Description - This module provide defination and wrappers for 
*               Sentinel Keys Client Library APIs
*
****************************************************************************/
using System;
using Microsoft.Win32;
namespace HYS.Common.Objects.SentinelDog
{
    public class SentinelKey : IDisposable
    {
        #region Constructor / Destructor

        private uint licHandle;
        private bool disposed = false;

        private string strError = "Unable to load the required SentinelKey Library(SentinelKeyW.dll).\n" +
                                "Either the library is missing or corrupted.";

        public SentinelKey()
        {

        }

        ~SentinelKey()
        {
            Dispose(false);
        }

        /* Implement IDisposable */
        public void Dispose()
        {
            Dispose(true);
        }
        /* Private class specifically for this object.*/
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.licHandle = 0;
                    /* only MANAGED resource should be dispose     */
                    /* here that implement the IDisposable		   */
                    /* Interface 								   */
                }
                /* only UNMANAGED resource should be dispose here */
                /*												  */
            }
            disposed = true;
        }
        #endregion

        #region SentinelKey API Error Codes
        /* SentinelKey API Error codes */

        public const int SP_DRIVER_LIBRARY_ERROR_BASE = 100;
        public const int SP_DUAL_LIBRARY_ERROR_BASE = 200;
        public const int SP_SERVER_ERROR_BASE = 300;
        public const int SP_SHELL_ERROR_BASE = 400;
        public const int SP_SECURE_UPDATE_ERROR_BASE = 500;

        //Dual Library Error Codes:
        public const int SP_ERR_INVALID_PARAMETER = (SP_DUAL_LIBRARY_ERROR_BASE + 1);
        public const int SP_ERR_SOFTWARE_KEY = (SP_DUAL_LIBRARY_ERROR_BASE + 2);
        public const int SP_ERR_INVALID_LICENSE = (SP_DUAL_LIBRARY_ERROR_BASE + 3);
        public const int SP_ERR_INVALID_FEATURE = (SP_DUAL_LIBRARY_ERROR_BASE + 4);
        public const int SP_ERR_INVALID_TOKEN = (SP_DUAL_LIBRARY_ERROR_BASE + 5);
        public const int SP_ERR_NO_LICENSE = (SP_DUAL_LIBRARY_ERROR_BASE + 6);
        public const int SP_ERR_INSUFFICIENT_BUFFER = (SP_DUAL_LIBRARY_ERROR_BASE + 7);
        public const int SP_ERR_VERIFY_FAILED = (SP_DUAL_LIBRARY_ERROR_BASE + 8);
        public const int SP_ERR_CANNOT_OPEN_DRIVER = (SP_DUAL_LIBRARY_ERROR_BASE + 9);
        public const int SP_ERR_ACCESS_DENIED = (SP_DUAL_LIBRARY_ERROR_BASE + 10);
        public const int SP_ERR_INVALID_DEVICE_RESPONSE = (SP_DUAL_LIBRARY_ERROR_BASE + 11);
        public const int SP_ERR_COMMUNICATIONS_ERROR = (SP_DUAL_LIBRARY_ERROR_BASE + 12);
        public const int SP_ERR_COUNTER_LIMIT = (SP_DUAL_LIBRARY_ERROR_BASE + 13);
        public const int SP_ERR_MEM_CORRUPT = (SP_DUAL_LIBRARY_ERROR_BASE + 14);
        public const int SP_ERR_INVALID_FEATURE_TYPE = (SP_DUAL_LIBRARY_ERROR_BASE + 15);
        public const int SP_ERR_DEVICE_IN_USE = (SP_DUAL_LIBRARY_ERROR_BASE + 16);
        public const int SP_ERR_INVALID_API_VERSION = (SP_DUAL_LIBRARY_ERROR_BASE + 17);
        public const int SP_ERR_TIME_OUT_ERROR = (SP_DUAL_LIBRARY_ERROR_BASE + 18);
        public const int SP_ERR_INVALID_PACKET = (SP_DUAL_LIBRARY_ERROR_BASE + 19);
        public const int SP_ERR_KEY_NOT_ACTIVE = (SP_DUAL_LIBRARY_ERROR_BASE + 20);
        public const int SP_ERR_FUNCTION_NOT_ENABLED = (SP_DUAL_LIBRARY_ERROR_BASE + 21);
        public const int SP_ERR_DEVICE_RESET = (SP_DUAL_LIBRARY_ERROR_BASE + 22);
        public const int SP_ERR_TIME_CHEAT = (SP_DUAL_LIBRARY_ERROR_BASE + 23);
        public const int SP_ERR_INVALID_COMMAND = (SP_DUAL_LIBRARY_ERROR_BASE + 24);
        public const int SP_ERR_RESOURCE = (SP_DUAL_LIBRARY_ERROR_BASE + 25);
        public const int SP_ERR_UNIT_NOT_FOUND = (SP_DUAL_LIBRARY_ERROR_BASE + 26);
        public const int SP_ERR_DEMO_EXPIRED = (SP_DUAL_LIBRARY_ERROR_BASE + 27);
        public const int SP_ERR_QUERY_TOO_LONG = (SP_DUAL_LIBRARY_ERROR_BASE + 28);

        //Server Error Codes
        public const int SP_ERR_SERVER_PROBABLY_NOT_UP = (SP_SERVER_ERROR_BASE + 1);
        public const int SP_ERR_UNKNOWN_HOST = (SP_SERVER_ERROR_BASE + 2);
        public const int SP_ERR_BAD_SERVER_MESSAGE = (SP_SERVER_ERROR_BASE + 3);
        public const int SP_ERR_NO_LICENSE_AVAILABLE = (SP_SERVER_ERROR_BASE + 4);
        public const int SP_ERR_INVALID_OPERATION = (SP_SERVER_ERROR_BASE + 5);
        public const int SP_ERR_INTERNAL_ERROR = (SP_SERVER_ERROR_BASE + 6);
        public const int SP_ERR_PROTOCOL_NOT_INSTALLED = (SP_SERVER_ERROR_BASE + 7);
        public const int SP_ERR_BAD_CLIENT_MESSAGE = (SP_SERVER_ERROR_BASE + 8);
        public const int SP_ERR_SOCKET_OPERATION = (SP_SERVER_ERROR_BASE + 9);

        // Shell Error Codes 
        public const int SP_ERR_BAD_ALGO = (SP_SHELL_ERROR_BASE + 1);
        public const int SP_ERR_LONG_MSG = (SP_SHELL_ERROR_BASE + 2);
        public const int SP_ERR_READ_ERROR = (SP_SHELL_ERROR_BASE + 3);
        public const int SP_ERR_NOT_ENOUGH_MEMORY = (SP_SHELL_ERROR_BASE + 4);
        public const int SP_ERR_CANNOT_OPEN = (SP_SHELL_ERROR_BASE + 5);
        public const int SP_ERR_WRITE_ERROR = (SP_SHELL_ERROR_BASE + 6);
        public const int SP_ERR_CANNOT_OVERWRITE = (SP_SHELL_ERROR_BASE + 7);
        public const int SP_ERR_INVALID_HEADER = (SP_SHELL_ERROR_BASE + 8);
        public const int SP_ERR_TMP_CREATE_ERROR = (SP_SHELL_ERROR_BASE + 9);
        public const int SP_ERR_PATH_NOT_THERE = (SP_SHELL_ERROR_BASE + 10);
        public const int SP_ERR_BAD_FILE_INFO = (SP_SHELL_ERROR_BASE + 11);
        public const int SP_ERR_NOT_WIN32_FILE = (SP_SHELL_ERROR_BASE + 12);
        public const int SP_ERR_INVALID_MACHINE = (SP_SHELL_ERROR_BASE + 13);
        public const int SP_ERR_INVALID_SECTION = (SP_SHELL_ERROR_BASE + 14);
        public const int SP_ERR_INVALID_RELOC = (SP_SHELL_ERROR_BASE + 15);
        public const int SP_ERR_CRYPT_ERROR = (SP_SHELL_ERROR_BASE + 16);
        public const int SP_ERR_SMARTHEAP_ERROR = (SP_SHELL_ERROR_BASE + 17);
        public const int SP_ERR_IMPORT_OVERWRITE_ERROR = (SP_SHELL_ERROR_BASE + 18);

        //Secure Update error codes
        public const int SP_ERR_KEY_NOT_FOUND = (SP_SECURE_UPDATE_ERROR_BASE + 1);
        public const int SP_ERR_ILLEGAL_UPDATE = (SP_SECURE_UPDATE_ERROR_BASE + 2);
        public const int SP_ERR_DLL_LOAD_ERROR = (SP_SECURE_UPDATE_ERROR_BASE + 3);
        public const int SP_ERR_NO_CONFIG_FILE = (SP_SECURE_UPDATE_ERROR_BASE + 4);
        public const int SP_ERR_INVALID_CONFIG_FILE = (SP_SECURE_UPDATE_ERROR_BASE + 5);
        public const int SP_ERR_UPDATE_WIZARD_NOT_FOUND = (SP_SECURE_UPDATE_ERROR_BASE + 6);
        public const int SP_ERR_UPDATE_WIZARD_SPAWN_ERROR = (SP_SECURE_UPDATE_ERROR_BASE + 7);
        public const int SP_ERR_EXCEPTION_ERROR = (SP_SECURE_UPDATE_ERROR_BASE + 8);
        public const int SP_ERR_INVALID_CLIENT_LIB = (SP_SECURE_UPDATE_ERROR_BASE + 9);
        public const int SP_ERR_CABINET_DLL = (SP_SECURE_UPDATE_ERROR_BASE + 10);
        public const int SP_ERR_INSUFFICIENT_REQ_CODE_BUFFER = (SP_SECURE_UPDATE_ERROR_BASE + 11);
        public const int SP_ERR_UPDATE_WIZARD_USER_CANCELED = (SP_SECURE_UPDATE_ERROR_BASE + 12);

        #endregion

        #region SentinelKey Constants values used by client application
        /* Set Protocol Flags */

        //SFNTGetLicense flags
        public const int SP_TCP_PROTOCOL = 1;
        public const int SP_IPX_PROTOCOL = 2;
        public const int SP_NETBEUI_PROTOCOL = 4;
        public const int SP_STANDALONE_MODE = 32;
        public const int SP_SERVER_MODE = 64;
        public const int SP_SHARE_ON = 128;

        //Query feature flag
        public const int SP_SIMPLE_QUERY = 1;
        public const int SP_CHECK_DEMO = 0;

        //Device Capabilities
        public const int SP_CAPS_AES_128 = 0x00000001;
        public const int SP_CAPS_ECC_K163 = 0x00000002;
        public const int SP_CAPS_ECC_KEYEXCH = 0x00000004;
        public const int SP_CAPS_ECC_SIGN = 0x00000008;
        public const int SP_CAPS_TIME_SUPP = 0x00000010;
        public const int SP_CAPS_TIME_RTC = 0x00000020;

        //Feature Attributies
        public const int SP_ATTR_WRITE_ONCE = 0x0200;
        public const int SP_ATTR_ACTIVE = 0x0020;
        public const int SP_ATTR_AUTODEC = 0x0010;
        public const int SP_ATTR_SIGN = 0x0004;
        public const int SP_ATTR_DECRYPT = 0x0002;
        public const int SP_ATTR_ENCRYPT = 0x0001;
        public const int SP_ATTR_SECMSG_READ = 0x0080;

        //Feature Type
        public const int DATA_FEATURE_TYPE_BOOLEAN = 1;
        public const int DATA_FEATURE_TYPE_BYTE = 2;
        public const int DATA_FEATURE_TYPE_WORD = 3;
        public const int DATA_FEATURE_TYPE_DWORD = 4;
        public const int DATA_FEATURE_TYPE_RAW = 5;
        public const int DATA_FEATURE_TYPE_STRING = 6;
        public const int FEATURE_TYPE_COUNTER = 7;
        public const int FEATURE_TYPE_AES = 8;
        public const int FEATURE_TYPE_ECC = 9;

        //Heartbeat Interval Scope
        public const int SP_MAX_HEARTBEAT = 2592000;
        public const int SP_MIN_HEARTBEAT = 60;
        public const uint SP_INFINITE_HEARTBEAT = 0xFFFFFFFF;

        public const uint SP_PUBILC_KEY_LEN = 42;
        public const uint SP_SOFTWARE_KEY_LEN = 112;
        public const uint SP_MIN_ENCRYPT_DATA_LEN = 16;
        public const uint SP_MAX_QUERY_LEN = 112;
        public const uint SP_MAX_RAW_LEN = 128;
        public const uint SP_MAX_STRING_LEN = 128;

        #endregion

        #region  Server Info Structure Used by SFNTGetServerInfo
        public class ServerInfo
        {
            public byte[] bInfoBuffer;
            const uint MAX_SERVERINFO_LENGTH = 70;
            const int BYTE_OFFSET_NAME = 0;
            const int BYTE_OFFSET_PROTOCOLS = 64;
            const int BYTE_OFFSET_MAJORVER = 66;
            const int BYTE_OFFSET_MINORVER = 68;

            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            public ServerInfo()
            {
                bInfoBuffer = new byte[MAX_SERVERINFO_LENGTH];
            }
            ~ServerInfo()
            {
                bInfoBuffer = null;
            }
            public string ServerName
            {
                get
                {
                    return encoding.GetString(this.bInfoBuffer, BYTE_OFFSET_NAME, 64);
                }
            }
            public ushort Protocols
            {
                get
                {
                    return BitConverter.ToUInt16(this.bInfoBuffer, BYTE_OFFSET_PROTOCOLS);
                }
            }
            public ushort MajorVersion
            {
                get
                {
                    return BitConverter.ToUInt16(this.bInfoBuffer, BYTE_OFFSET_MAJORVER);
                }
            }
            public ushort MinorVersion
            {
                get
                {
                    return BitConverter.ToUInt16(this.bInfoBuffer, BYTE_OFFSET_MINORVER);
                }
            }
        }
        #endregion

        #region  License Info Structure Used by SFNTGetLicenseInfo
        public class LicenseInfo
        {
            public byte[] bInfoBuffer;
            const uint MAX_LICENSEINFO_LENGTH = 16;
            const int BYTE_OFFSET_LICENSEID = 0;
            const int BYTE_OFFSET_USERLIMIT = 4;
            const int BYTE_OFFSET_FEATURENUMS = 8;
            const int BYTE_OFFSET_LICENSESIZE = 12;
            public LicenseInfo()
            {
                bInfoBuffer = new byte[MAX_LICENSEINFO_LENGTH];
            }
            ~LicenseInfo()
            {
                bInfoBuffer = null;
            }
            public uint LicenseID
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_LICENSEID);
                }
            }
            public uint UserLimit
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_USERLIMIT);
                }
            }
            public uint FeatureNums
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FEATURENUMS);
                }
            }
            public uint LicenseSize
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_LICENSESIZE);
                }
            }
        }
        #endregion

        #region  Device Info Structure Used by SFNTGetDeviceInfo
        public class DeviceInfo
        {
            public byte[] bInfoBuffer;
            const uint MAX_DEVICEINFO_LENGTH = 49;
            const int BYTE_OFFSET_FORMFACTORTYPE = 0;
            const int BYTE_OFFSET_PRODUCTCODE = 4;
            const int BYTE_OFFSET_HARDLIMIT = 8;
            const int BYTE_OFFSET_CAPABILITIES = 12;
            const int BYTE_OFFSET_DEVID = 16;
            const int BYTE_OFFSET_DEVSN = 20;
            const int BYTE_OFFSET_DEVYEAR = 24;
            const int BYTE_OFFSET_DEVMONTH = 28;
            const int BYTE_OFFSET_DEVDAY = 29;
            const int BYTE_OFFSET_DEVHOUR = 30;
            const int BYTE_OFFSET_DEVMINUTE = 31;
            const int BYTE_OFFSET_DEVSECOND = 32;
            const int BYTE_OFFSET_MEMSIZE = 36;
            const int BYTE_OFFSET_FREESIZE = 40;
            const int BYTE_OFFSET_DRVVERSION = 44;
            public DeviceInfo()
            {
                bInfoBuffer = new byte[MAX_DEVICEINFO_LENGTH];
            }
            ~DeviceInfo()
            {
                bInfoBuffer = null;
            }
            public uint FormFactorType
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FORMFACTORTYPE);
                }
            }
            public uint ProductCode
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_PRODUCTCODE);
                }
            }
            public uint Hardlimit
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_HARDLIMIT);
                }
            }
            public uint Capabilities
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_CAPABILITIES);
                }
            }
            public uint DevID
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_DEVID);
                }
            }
            public uint DevSN
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_DEVSN);
                }
            }
            public uint Year
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_DEVYEAR);
                }
            }
            public byte Month
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_DEVMONTH];
                }
            }
            public byte Day
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_DEVDAY];
                }
            }
            public byte Hour
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_DEVHOUR];
                }
            }
            public byte Minute
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_DEVMINUTE];
                }
            }
            public byte Second
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_DEVSECOND];
                }
            }
            public uint MemorySize
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_MEMSIZE);
                }
            }
            public uint FreeSize
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FREESIZE);
                }
            }
            public uint DrvVersion
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_DRVVERSION);
                }
            }
        }
        #endregion

        #region  Feature Info Structure Used by SFNTGetFeatureInfo
        public class FeatureInfo
        {
            public byte[] bInfoBuffer;
            const int MAX_FEATUREINFO_LENGTH = 36;
            const int BYTE_OFFSET_FEATURETYPE = 0;
            const int BYTE_OFFSET_FEATURESIZE = 4;
            const int BYTE_OFFSET_FEATUREATTRIBUTES = 8;
            const int BYTE_OFFSET_ENABLECOUNTER = 12;
            const int BYTE_OFFSET_ENABLESTOPTIME = 13;
            const int BYTE_OFFSET_ENABLEDURATION = 14;
            const int BYTE_OFFSET_DURATION = 16;
            const int BYTE_OFFSET_FEATUREYEAR = 20;
            const int BYTE_OFFSET_FEATUREMONTH = 24;
            const int BYTE_OFFSET_FEATUREDAY = 25;
            const int BYTE_OFFSET_FEATUREHOUR = 26;
            const int BYTE_OFFSET_FEATUREMINUTE = 27;
            const int BYTE_OFFSET_FEATURESECOND = 28;
            const int BYTE_OFFSET_LEFTEXECUTIONNUMBER = 32;
            public FeatureInfo()
            {
                bInfoBuffer = new byte[MAX_FEATUREINFO_LENGTH];
            }
            ~FeatureInfo()
            {
                bInfoBuffer = null;
            }
            public uint FeatureType
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FEATURETYPE);
                }
            }
            public uint FeatureSize
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FEATURESIZE);
                }
            }
            public uint FeatureAttributes
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FEATUREATTRIBUTES);
                }
            }
            public byte bEnableCounter
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_ENABLECOUNTER];
                }
            }
            public byte bEnableStopTime
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_ENABLESTOPTIME];
                }
            }
            public byte bEnableDurationTime
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_ENABLEDURATION];
                }
            }
            public uint Duration
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_DURATION);
                }
            }
            public uint Year
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_FEATUREYEAR);
                }
            }
            public byte Month
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_FEATUREMONTH];
                }
            }
            public byte Day
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_FEATUREDAY];
                }
            }
            public byte Hour
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_FEATUREHOUR];
                }
            }
            public byte Minute
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_FEATUREMINUTE];
                }
            }
            public byte Second
            {
                get
                {
                    return this.bInfoBuffer[BYTE_OFFSET_FEATURESECOND];
                }
            }
            public uint LeftExecutionNumber
            {
                get
                {
                    return BitConverter.ToUInt32(this.bInfoBuffer, BYTE_OFFSET_LEFTEXECUTIONNUMBER);
                }
            }

        }
        #endregion

        #region	//////////////////////// BEGIN PUBLIC METHODS	///////////////////////////////////
        public uint SFNTGetLicense(uint devID, byte[] softwareKey, uint licID, uint flags)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTGetLicense(devID, softwareKey, licID, flags, out this.licHandle);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTReleaseLicense()
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTReleaseLicense(this.licHandle);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTCounterDecrement(uint featureID, uint decrementValue)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTCounterDecrement(this.licHandle, featureID, decrementValue);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTQueryFeature(uint featureID, uint flag, byte[] query, uint queryLength, byte[] response, uint responseLength)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTQueryFeature(this.licHandle, featureID, flag, query, queryLength, response, responseLength);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTReadString(uint featureID, byte[] stringBuffer, uint stringLength)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTReadString(this.licHandle, featureID, stringBuffer, stringLength);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else			
            }

            return status;
        }

        public uint SFNTWriteString(uint featureID, byte[] stringBuffer, uint writePassword)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTWriteString(this.licHandle, featureID, stringBuffer, writePassword);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTReadInteger(uint featureID, out uint featureValue)
        {
            uint status = 0;
            featureValue = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTReadInteger(this.licHandle, featureID, out featureValue);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTWriteInteger(uint featureID, uint featureValue, uint writePassword)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTWriteInteger(this.licHandle, featureID, featureValue, writePassword);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTReadRawData(uint featureID, byte[] rawDataBuffer, uint offset, uint length)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTReadRawData(this.licHandle, featureID, rawDataBuffer, offset, length);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTWriteRawData(uint featureID, byte[] rawDataBuffer, uint offset, uint length, uint writePassword)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTWriteRawData(this.licHandle, featureID, rawDataBuffer, offset, length, writePassword);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTEncrypt(uint featureID, byte[] plainBuffer, byte[] cipherBuffer)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTEncrypt(this.licHandle, featureID, plainBuffer, cipherBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTDecrypt(uint featureID, byte[] cipherBuffer, byte[] plainBuffer)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTDecrypt(this.licHandle, featureID, cipherBuffer, plainBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTSign(uint featureID, byte[] signBuffer, uint length, byte[] signResult)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTSign(this.licHandle, featureID, signBuffer, length, signResult);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTVerify(byte[] publicKey, byte[] signBuffer, uint length, byte[] signResult)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTVerify(this.licHandle, publicKey, signBuffer, length, signResult);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTSetHeartbeat(uint heartbeatValue)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTSetHeartbeat(this.licHandle, heartbeatValue);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else
            }

            return status;
        }

        public uint SFNTGetLicenseInfo(SentinelKey.LicenseInfo licenseInfo)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTGetLicenseInfo(this.licHandle, licenseInfo.bInfoBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else			
            }

            return status;
        }

        public uint SFNTGetFeatureInfo(uint featureID, SentinelKey.FeatureInfo featureInfo)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTGetFeatureInfo(this.licHandle, featureID, featureInfo.bInfoBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else			
            }

            return status;
        }

        public uint SFNTGetDeviceInfo(SentinelKey.DeviceInfo deviceInfo)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTGetDeviceInfo(this.licHandle, deviceInfo.bInfoBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else			
            }

            return status;
        }

        public uint SFNTGetServerInfo(SentinelKey.ServerInfo serverInfo)
        {
            uint status = 0;

            try
            {
                status = SentinelKeyNativeAPI.SFNTGetServerInfo(this.licHandle, serverInfo.bInfoBuffer);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            catch
            {
                // Anything else			
            }

            return status;
        }

        #endregion //////////////////////// END PUBLIC METHODS	///////////////////////////////////

        private const uint NotActive = 1002;
        public uint GetLicense(uint devID, uint offset, uint length)
        {
            uint result = SFNTGetLicense(devID, SentinelKeysLicense.SOFTWARE_KEY, SentinelKeysLicense.LICENSEID, 0x20);
            if (result != 0)
            {
                return result;
            }

            byte[] rawDataSys = new byte[length];
            result = SFNTReadRawData(SentinelKeysLicense.SP_SYSTEM_RAWDATA, rawDataSys, offset, length);

            if (result != 0)
            {
                return result;
            }
            string strSystemData = System.Text.Encoding.UTF8.GetString(rawDataSys);
            if (strSystemData == null)
            {
                return 1;
            }
            int dogStatus = 0;
            string strOrderNo = strSystemData.Substring(24, 10).Trim('\0');
            string strSerialNo = strSystemData.Substring(14, 10).Trim('\0');

            string strInfoInKey = "";
            foreach (char c in strOrderNo)
            {
                int temp = (int)(c ^ 'T');
                strInfoInKey += temp.ToString();
            }
            foreach (char c in strSerialNo)
            {
                int temp = (int)(c ^ 'T');
                strInfoInKey += temp.ToString();
            }
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("SOFTWARE");
            RegistryKey HaoYiSheng = software.OpenSubKey("HaoYiSheng");
            if (HaoYiSheng != null)
            {
                RegistryKey hys = HaoYiSheng.OpenSubKey("HYS");
                if (hys != null)
                {
                    SentinelKey.DeviceInfo keyInfo = new SentinelKey.DeviceInfo();
                    uint resultNum = SFNTGetDeviceInfo(keyInfo);
                    if (resultNum != 0)
                    {
                        dogStatus = 1;
                    }
                    else
                    {
                        strSerialNo = keyInfo.DevSN.ToString("X");
                        string strInfoInReg = Convert.ToString(hys.GetValue(strSerialNo));
                        if (strInfoInReg == null || strInfoInReg == "")
                        {
                            dogStatus = 1;
                        }
                        else
                        {
                            if (strInfoInReg == strInfoInKey)
                                dogStatus = 0;
                            else
                                dogStatus = 1;
                        }
                    }
                }
                else
                {
                    dogStatus = 1;
                }
            }
            else
            {
                dogStatus = 1;
            }

            if (dogStatus != 0)
            {
                return NotActive;
            }

            return 0;
        }
    }
}