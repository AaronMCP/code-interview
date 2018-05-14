using System;
using System.Xml;
using System.Reflection;

namespace HYS.Common.Xml
{
	/// <summary>
	///可映射到XML的对象的管理器
	/// </summary>
	public class XObjectManager
	{
		public static Exception LastError
		{
			get{ return _lastError; }
		}
		public static string LastErrorInfo
		{
			get
			{
				if( _lastError == null ) return "";
				return _lastError.ToString();
			}
		}

		private static Exception _lastError = null;
		public static event XObjectExceptionHandler OnError;
		internal static void NotifyException( object source, Exception error )
		{
			_lastError = error;

			if( OnError != null ) OnError( source, error );
		}

        //public static XObject CreateObject( string xmlString, Type objType )
        //{
        //    try
        //    {
        //        if( xmlString == null || 
        //            XObjectHelper.IsXObjectType( objType ) == false ) return null;

        //        //object item = objType.Assembly.CreateInstance( objType.ToString() );
        //        object item = objType.Assembly.CreateInstance(objType.FullName);

        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml( xmlString );

        //        string tname = XObjectHelper.GetCleanName(objType.Name);
        //        XmlNodeList nodelist = xmlDoc.GetElementsByTagName(tname);
        //        if( nodelist != null && nodelist.Count > 0 )
        //        {
        //            XObjectHelper.XBaseType.InvokeMember( "Load", 
        //                BindingFlags.Public | BindingFlags.NonPublic | 
        //                BindingFlags.Instance | BindingFlags.InvokeMethod,
        //                null, item, new object[]{nodelist[0]} );
        //        }

        //        _lastError = null;
        //        return item as XObject;
        //    }
        //    catch( Exception err )
        //    {
        //        XObjectManager.NotifyException( typeof(XObjectManager), err );
        //        return null;
        //    }
        //}
        public static XBase CreateObject(string xmlString, Type objType)
        {
            try
            {
                if (xmlString == null ||
                    XObjectHelper.IsXBaseType(objType) == false) return null;

                //object item = objType.Assembly.CreateInstance( objType.ToString() );
                object item = objType.Assembly.CreateInstance(objType.FullName);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                string tname = XObjectHelper.GetCleanName(objType.Name);
                XmlNodeList nodelist = xmlDoc.GetElementsByTagName(tname);
                if (nodelist != null && nodelist.Count > 0)
                {
                    XObjectHelper.XBaseType.InvokeMember("Load",
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.InvokeMethod,
                        null, item, new object[] { nodelist[0] });
                }

                _lastError = null;
                return item as XBase;
            }
            catch (Exception err)
            {
                XObjectManager.NotifyException(typeof(XObjectManager), err);
                return null;
            }
        }

        public static T CreateObject<T>(string xmlString) where T : XBase
        {
            return CreateObject(xmlString, typeof(T)) as T;
        }
     
	}

	public delegate void  XObjectExceptionHandler( object source, Exception error );
}
