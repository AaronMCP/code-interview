using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CommonGlobalSettings
{
    /// <summary>
    /// For binding model field to DB field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseFieldAttribute : Attribute
    {
        string dbFieldName;

        protected BaseFieldAttribute(string dbFieldName)
        {
            this.dbFieldName = dbFieldName;
        }

        public string DBFieldName
        {
            get { return dbFieldName; }
            set { dbFieldName = value; }
        }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldAttribute : BaseFieldAttribute
    {
        DbType dbType = DbType.String;
        int size = 0;

        public DataFieldAttribute(string dbFieldName)
            : base(dbFieldName)
        {

        }

        public DbType Type
        {
            get { return dbType; }
            set { dbType = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
