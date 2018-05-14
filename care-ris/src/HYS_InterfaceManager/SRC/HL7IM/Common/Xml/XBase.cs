using System;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace HYS.HL7IM.Common.Xml
{
	/// <summary>
	/// 可映射到XML的对象基类
	/// </summary>
	public abstract class XBase
	{
		public string ToXMLString()
		{
			return ToFullString();
		}
        public string ToXMLString(Type t)
        {
            if (t == null) return ToXMLString();
            string nodeName = XObjectHelper.GetCleanName(t.Name);
            return ToFullString(nodeName, t);
        }
        public string ToXMLString(string defaultXmlNamespace)
        {
            string str = ToXMLString();
            return InsertXmlNamespace(str, defaultXmlNamespace);
        }
        public string ToXMLString(Type t, string defaultXmlNamespace)
        {
            string str = ToXMLString(t);
            return InsertXmlNamespace(str, defaultXmlNamespace);
        }
        private string InsertXmlNamespace(string xmlstr, string xmlnamespace)
        {
            string str = xmlstr;

            int index1, index2, index;
            index1 = str.IndexOf("/>");
            index = index2 = str.IndexOf('>');
            if (index1 < index) index = index1;
            if (index < 0) return str;

            string xmlns = string.Format(" xmlns=\"{0}\"", xmlnamespace);
            str = str.Insert(index, xmlns);
            return str;
        }
		
		internal virtual string XMLNodeName
		{
			get{ return "XBase"; }
		}

        internal virtual string ToNakedString(Type t)
		{
			return "";
		}
		internal virtual string ToAttributeString(Type t)
		{
			StringBuilder sb = new StringBuilder();

			string[] memlist = GetFieldNames(t);
			foreach( string str in memlist )
				sb.Append( GetAttributeXMLString( str ) );

			return sb.ToString().TrimEnd();
		}

        internal string ToFullString(string root, Type t)
		{
			return XObjectHelper.GetXMLElementWithAttribute
				( root, ToAttributeString(t), ToNakedString(t) );
		}
        internal string ToFullString(string root)
        {
            return ToFullString(root, this.GetType());
        }
		internal string ToFullString(Type t)
		{
            return ToFullString(XMLNodeName, t);
		}
        internal string ToFullString()
        {
            return ToFullString(XMLNodeName, this.GetType());
        }
		

		internal abstract void InnerLoad( XmlNodeReader reader, string endpoint );
		internal void Load( XmlNode node )
		{
			if( node == null ) return;
			XmlNodeReader reader = new XmlNodeReader (node);
			
			while (reader.Read())
			{
				InnerLoad( reader, reader.Name );
			}
		}


        protected static string[] GetPropertyNames(Type t)
		{
			ArrayList strlist = new ArrayList();

			try
			{
				//PropertyInfo[] list = this.GetType().GetProperties( BindingFlags.Instance | BindingFlags.Public );
                //PropertyInfo[] list = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                IOrderedEnumerable<PropertyInfo> list = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(item => item.MetadataToken);
				if( list != null )
				{
					foreach( PropertyInfo prop in list )
					{
						if( prop.DeclaringType == XObjectHelper.XObjectType ||
							prop.DeclaringType == XObjectHelper.XObjectCollectionType )
							continue;

                        object[] olist = prop.GetCustomAttributes(XObjectHelper.XNodeAttributeType, false);
                        if (olist != null && olist.Length > 0)
                        {
                            XNodeAttribute attr = olist[0] as XNodeAttribute;
                            if (attr != null || attr.VisibleInSerialization == false) continue;
                        }

						strlist.Add( prop.Name );
					}
				}
			}
			catch( Exception e )
			{
				System.Diagnostics.Debug.WriteLine( e.ToString() );
				XObjectManager.NotifyException( t, e );
			}

			return strlist.ToArray( typeof(string) ) as string[];
		}

        protected static string[] GetFieldNames(Type t)
		{
			ArrayList strlist = new ArrayList();

			try
			{
				//MemberInfo[] list = this.GetType().GetFields( BindingFlags.Instance | BindingFlags.Public );
                //MemberInfo[] list = t.GetFields(BindingFlags.Instance | BindingFlags.Public);
                IOrderedEnumerable<FieldInfo> list = t.GetFields(BindingFlags.Instance | BindingFlags.Public).OrderBy(item => item.MetadataToken);
				if( list != null )
				{
					foreach( MemberInfo mem in list )
					{
						if( mem.DeclaringType == XObjectHelper.XObjectCollectionType )
							continue;

                        object[] olist = mem.GetCustomAttributes(XObjectHelper.XNodeAttributeType, false);
                        if (olist != null && olist.Length > 0)
                        {
                            XNodeAttribute attr = olist[0] as XNodeAttribute;
                            if (attr != null || attr.VisibleInSerialization == false) continue;
                        }

						strlist.Add( mem.Name );
					}
				}
			}
			catch( Exception e )
			{
				System.Diagnostics.Debug.WriteLine( e.ToString() );
				XObjectManager.NotifyException( t, e );
			}

			return strlist.ToArray( typeof(string) ) as string[];
		}
		
		
		protected string GetNodeXMLString( string propName )
		{
			return GetNodeXMLString( propName, false );
		}
		protected string GetNodeXMLString( string propName, bool isdata )
		{
			if( propName == null || propName.Length < 1 ) return "";

			object o = GetValueEx( propName );
			Type type = o.GetType();

			if( XObjectHelper.IsXObjectType( type ) )
			{
                XObject item = o as XObject;
                //XObject item = GetValueEx( propName ) as XObject;
				if( item == null ) return XObjectHelper.GetEmptyElement( propName );
                return item.ToFullString(propName);
			}

			if( XObjectHelper.IsXObjectCollectionType( type ) )
			{
                XObjectCollection col = o as XObjectCollection;
				//XObjectCollection col = GetValueEx( propName ) as XObjectCollection;
				if( col == null ) return XObjectHelper.GetEmptyElement( propName );
				return col.ToFullString( propName );
			}

			//return XObjectHelper.GetXMLElement( propName, GetValueEx( propName ), isdata );
            return XObjectHelper.GetXMLElement(propName, o, isdata);
		}
		
		protected string GetAttributeXMLString( string attributeName )
		{
			if( attributeName == null || attributeName.Length < 1 ) return "";
			return attributeName + "=\"" + GetValueEx( attributeName ) + "\" ";
		}


		protected virtual object GetValueEx( string name )
		{
			if( name == null || name.Length < 1 ) return "";

			try
			{
				object result = this.GetType().InvokeMember( name,
					/*BindingFlags.DeclaredOnly |*/	BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.GetProperty |
					BindingFlags.GetField,
					null, this, new object[]{} );

				if( result == null ) return "";

                MemberInfo[] pilist = this.GetType().GetMember(name);
                if (pilist.Length < 1) return "";
					
				Type type = null;
                MemberInfo minfo = pilist[0];
				if( minfo is PropertyInfo ) type = ((PropertyInfo)minfo).PropertyType;
				if( minfo is FieldInfo ) type = ((FieldInfo)minfo).FieldType;
				if( type == null ) return "";

				return ProcessGetValue( type, minfo, result );
			}
			catch( Exception e )
			{
				XObjectManager.NotifyException( this, e );
				return e;
			}
		}
        protected virtual bool SetValueEx(string name, string newvalue)
		{
			if( name == null || name.Length < 1 ) return false;

			try
			{
				MemberInfo[] pilist = this.GetType().GetMember( name );
				if( pilist.Length < 1 ) return false;
					
				Type type = null;
				MemberInfo minfo = pilist[0];
				if( minfo is PropertyInfo ) type = ((PropertyInfo)minfo).PropertyType;
				if( minfo is FieldInfo ) type = ((FieldInfo)minfo).FieldType;
				if( type == null ) return false;

				return ProcessSetValue( type, name, newvalue );
			}
			catch( Exception e )
			{
				System.Diagnostics.Debug.WriteLine( e.ToString() );
				XObjectManager.NotifyException( this, e );
				return false;
			}
		}
		protected bool SetValue( string name, XmlNodeReader reader, ref bool hasAlreadyMoveToNext )
		{
            hasAlreadyMoveToNext = false;
			if( name == null || name.Length < 1 ) return false;

			try
			{
				PropertyInfo pi = this.GetType().GetProperty( name );
				Type type = pi.PropertyType;

				if( XObjectHelper.IsXObjectType( type ) )
				{
					object theproperty = this.GetType().InvokeMember( name,
						/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.GetProperty,
						null, this, new object[]{} );

                    if (theproperty == null && reader.IsEmptyElement == false)
                    {
                        theproperty = type.Assembly.CreateInstance(type.FullName);
                        this.GetType().InvokeMember(name,
                            /*BindingFlags.DeclaredOnly |*/ BindingFlags.Public |
                        BindingFlags.Instance | BindingFlags.SetProperty,
                        null, this, new object[] { theproperty });
                    }

					if( theproperty != null )
					{
						XObjectHelper.XBaseType.InvokeMember( "InnerLoad", 
							BindingFlags.DeclaredOnly | 
							BindingFlags.Public | BindingFlags.NonPublic | 
							BindingFlags.Instance | BindingFlags.InvokeMethod,
							null, theproperty, new object[]{reader,name} );

						return true;
					}
				}
				
				if( XObjectHelper.IsXObjectCollectionType( type ) )
				{
					object thecollection = this.GetType().InvokeMember( name,
						/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.GetProperty,
						null, this, new object[]{} );

                    if (thecollection == null && reader.IsEmptyElement == false)
                    {
                        thecollection = type.Assembly.CreateInstance(type.FullName);
                        this.GetType().InvokeMember(name,
                            /*BindingFlags.DeclaredOnly |*/ BindingFlags.Public |
                        BindingFlags.Instance | BindingFlags.SetProperty,
                        null, this, new object[] { thecollection });
                    }

					if( thecollection != null )
					{
						XObjectHelper.XBaseType.InvokeMember( "InnerLoad", 
							BindingFlags.DeclaredOnly |
							BindingFlags.Public | BindingFlags.NonPublic | 
							BindingFlags.Instance | BindingFlags.InvokeMethod,
							null, thecollection, new object[]{reader,name} );

						return true;
					}
				}

                if (type == typeof(string))
                {
                    object[] olist = pi.GetCustomAttributes(XObjectHelper.XRawXmlStringAttributeType, false);
                    if (olist != null && olist.Length > 0)
                    {
                        XRawXmlStringAttribute a = olist[0] as XRawXmlStringAttribute;
                        if (a != null && a.EnableRawXmlString)
                        {
                            string rawXmlString = reader.ReadInnerXml();
                            hasAlreadyMoveToNext = true;

                            return SetValueEx(name, rawXmlString);
                        }
                    }
                }
			}
			catch( Exception e )
			{
				System.Diagnostics.Debug.WriteLine( "(" + name + ") " + e.ToString() );
				XObjectManager.NotifyException( this, e );
				return false;
			}

			return SetValueEx( name, reader.ReadString() );
		}

		private object ProcessGetValue( Type type, MemberInfo memInfo, object result )
		{
			if( type == typeof( string ) )
			{
				if( memInfo == null ) return result;
				object[] olist = memInfo.GetCustomAttributes( XObjectHelper.XCDataAttributeType, false );
				if( olist == null || olist.Length < 1 || result == null ) return result;
				
				XCDataAttribute attr = olist[0] as XCDataAttribute;
				if( attr == null || attr.EnableCData == false ) return result;
				return "<![CDATA[" + result.ToString() + "]]>";
			}
			
			if( type == typeof( Color ) )
			{
				ColorConverter cc = new ColorConverter();
				return ( cc.ConvertToInvariantString( result ) );
			}
					
			if( type == typeof( Font ) )
			{
				FontConverter cc = new FontConverter();
				return ( cc.ConvertToInvariantString( result ) );
			}

			if( type == typeof( Point ) )
			{
				PointConverter cc = new PointConverter();
				return ( cc.ConvertToInvariantString( result ) );
			}

			if( type == typeof( Rectangle ) )
			{
				RectangleConverter cc = new RectangleConverter();
				return ( cc.ConvertToInvariantString( result ) );
			}

			if( type == typeof( Size ) )
			{
				SizeConverter cc = new SizeConverter();
				return ( cc.ConvertToInvariantString( result ) );
			}

            //if (type == typeof(Type))
            //{
            //    if (result == null) return "";
            //    return result.ToString();
            //}

			if( ! XObjectHelper.IsXBaseType( type ) )
			{
				TypeConverter tc = TypeDescriptor.GetConverter( type );
				if( tc != null ) return ( tc.ConvertToInvariantString( result ) );
			}

			return result;
		}
		private bool ProcessSetValue( Type type, string name, string newvalue )
		{
			if( type == typeof( string ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{newvalue} );
				return true;
			}

			if( type == typeof( bool ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ bool.Parse(newvalue) } );
				return true;
			}
				
			if( type == typeof( int ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ int.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( long ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ long.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( decimal ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ decimal.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( float ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ float.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( double ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ double.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( char ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ char.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Enum ) || type.BaseType == typeof( Enum ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Enum.Parse(type,newvalue) } );
				return true;
			}

			if( type == typeof( Single ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Single.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Byte ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Byte.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( SByte ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ SByte.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Int16 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Int16.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Int32 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Int32.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Int64 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ Int64.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( UInt16 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ UInt16.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( UInt32 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ UInt32.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( UInt64 ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ UInt64.Parse(newvalue) } );
				return true;
			}

            // 20080827 : DateTimeConvertor.ConvertToInvariantString(DateTime.MinValue) return empty string ""
            //            which cannot be parsed by following culture specific method.

            //if( type == typeof( DateTime ) )
            //{
            //    this.GetType().InvokeMember( name, 
            //        /*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
            //        BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
            //        null, this, new object[]{ DateTime.Parse(newvalue) } );
            //    return true;
            //}

			if( type == typeof( TimeSpan ) )
			{
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ TimeSpan.Parse(newvalue) } );
				return true;
			}

			if( type == typeof( Color ) )
			{
				ColorConverter cc = new ColorConverter();
					
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ cc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

			if( type == typeof( Font ) )
			{
				FontConverter cc = new FontConverter();
					
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ cc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

			if( type == typeof( Point ) )
			{
				PointConverter cc = new PointConverter();
					
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ cc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

			if( type == typeof( Rectangle ) )
			{
				RectangleConverter cc = new RectangleConverter();
					
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ cc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

			if( type == typeof( Size ) )
			{
				SizeConverter cc = new SizeConverter();
					
				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ cc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

            //if (type == typeof(Type))
            //{
            //    Type newType = null;
            //    if (newvalue != null && newvalue.Length > 0)
            //    {
            //        newType = Type.GetType(newvalue, true);
            //    }
                
            //    this.GetType().InvokeMember(name,
            //        /*BindingFlags.DeclaredOnly |*/ BindingFlags.Public |
            //        BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField,
            //        null, this, new object[] { newType });
            //    return true;
            //}

			if( ! XObjectHelper.IsXBaseType( type ) )
			{
				TypeConverter tc = TypeDescriptor.GetConverter( type );
				if( tc == null ) return true;

				this.GetType().InvokeMember( name, 
					/*BindingFlags.DeclaredOnly |*/ BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField ,
					null, this, new object[]{ tc.ConvertFromInvariantString(newvalue) } );
				return true;
			}

			return true;
		}
	}
}
