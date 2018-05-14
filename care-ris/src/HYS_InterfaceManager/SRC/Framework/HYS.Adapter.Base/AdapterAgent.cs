using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Base
{
    public class AdapterAgent<T,A> 
        where T : IAdapterBase
        where A : AdapterEntryAttributeBase
    {
        private Logging Log;
        public AdapterAgent(string assemblyFileName, Logging log)
        {
            _fileName = assemblyFileName;
            Log = log;
        }

        private string _fileName;
        public string FileName
        {
            get{ return _fileName; }
        }

        private Type _adapterType;
        public Type Type
        {
            get
            {
                if (_adapterType == null)
                {
                    _adapterType = AssemblyHelper.FindAdapter<A>(_fileName);
                    if (_adapterType == null)
                    {
                        Log.Write(LogType.Error, "Cannot find adapter in file: " + _fileName);
                        Log.Write(AssemblyHelper.LastError);
                    }
                }
                return _adapterType;
            }
        }

        private A _adapterAttribute;
        public A Attribute
        {
            get
            {
                if (_adapterAttribute == null)
                {
                    Type t = Type;
                    _adapterAttribute = AssemblyHelper.GetApdaterEntryAttribute<A>(t);
                    if (_adapterAttribute == null)
                    {
                        Log.Write(LogType.Error, "Cannot find adapter attribute of type: " + t);
                        Log.Write(AssemblyHelper.LastError);
                    }
                }
                return _adapterAttribute;
            }
        }

        private T _adapterInstance;
        public T Instance
        {
            get
            {
                if (_adapterInstance == null)
                {
                    Type t = Type;
                    _adapterInstance = AssemblyHelper.CreateAdapter<T>(t);
                    if (_adapterInstance == null)
                    {
                        Log.Write(LogType.Error, "Cannot create adapter instance of type: " + t);
                        Log.Write(AssemblyHelper.LastError);
                    }
                }
                return _adapterInstance;
            }
        }
    }
}
