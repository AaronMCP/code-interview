using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.Utilities.Oam
{
    public class Utilities
    {
        public static DataTable CreataCustomDataTable()
        {
            DataTable customedTable = new DataTable();

            //ID
            DataColumn idColumn = new DataColumn();
            idColumn.ColumnName = "Id";
            customedTable.Columns.Add(idColumn);

            //FieldName
            DataColumn fieldNameColumn = new DataColumn();
            fieldNameColumn.ColumnName = "FieldName";
            customedTable.Columns.Add(fieldNameColumn);

            //FieldValue
            DataColumn fieldValueColumn = new DataColumn();
            fieldValueColumn.ColumnName = "FieldValue";
            customedTable.Columns.Add(fieldValueColumn);

            //FieldDescription
            DataColumn fieldDescriptionColumn = new DataColumn();
            fieldDescriptionColumn.ColumnName = "FieldDescription";
            customedTable.Columns.Add(fieldDescriptionColumn);

            //ShortcutCode
            //DataColumn shortcutCodeColumn = new DataColumn();
            //shortcutCodeColumn.ColumnName = "ShortcutCode";
            DataColumn shortcutCodeColumn = new DataColumn("ShortcutCode",typeof(System.Object));
            customedTable.Columns.Add(shortcutCodeColumn);

            //DefaultValue
            DataColumn defaultValueColumn = new DataColumn();
            defaultValueColumn.ColumnName = "DefaultValue";
            customedTable.Columns.Add(defaultValueColumn);

            //CategoryName
            DataColumn categoryNameColumn = new DataColumn();
            categoryNameColumn.ColumnName = "CategoryName";
            customedTable.Columns.Add(categoryNameColumn);

            //FieldType
            DataColumn fieldTypeColumn = new DataColumn();
            fieldTypeColumn.ColumnName = "FieldType";
            customedTable.Columns.Add(fieldTypeColumn);

            //RegularExpress
            DataColumn regularExpressColumn = new DataColumn();
            regularExpressColumn.ColumnName = "RegularExpress";
            customedTable.Columns.Add(regularExpressColumn);

            //Description
            DataColumn descriptionColumn = new DataColumn();
            descriptionColumn.ColumnName = "Description";
            customedTable.Columns.Add(descriptionColumn);

            DataColumn defaultDescriptionColumn = new DataColumn();
            defaultDescriptionColumn.ColumnName = "DefaultDescription";
            customedTable.Columns.Add(defaultDescriptionColumn);

            DataColumn orderIDColumn = new DataColumn();
            orderIDColumn.ColumnName = "OrderID";
            customedTable.Columns.Add(orderIDColumn);

            DataColumn siteColumn = new DataColumn();
            siteColumn.ColumnName = "Site";
            customedTable.Columns.Add(siteColumn);
            return customedTable;
        }

        public static string[] ParametersParse(string parameters)
        {
            string[] str = { "@$@" };
            string[] strArry = parameters.Split(str, StringSplitOptions.RemoveEmptyEntries);
            return strArry;
        }

        public static int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch(dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
            }

            return 0;
        }

        public static DataTable CreataCustomRoleDataTable()
        {
            DataTable customedTable = new DataTable();

            //Role name
            DataColumn roleNameColumn = new DataColumn();
            roleNameColumn.ColumnName = "RoleName";
            customedTable.Columns.Add(roleNameColumn);

            //Role Description
            DataColumn roleDescriptionColumn = new DataColumn();
            roleDescriptionColumn.ColumnName = "RoleDescription";
            customedTable.Columns.Add(roleDescriptionColumn);

            DataColumn isSystemColumn = new DataColumn();
            isSystemColumn.ColumnName = "IsSystem";
            customedTable.Columns.Add(isSystemColumn);
            return customedTable;
        }


        public static DataTable CreateClientConfigDataTable()
        {
            DataTable customedTable = new DataTable("ConfigDic");
            //ConfigName
            customedTable.Columns.Add("ConfigName", typeof(string));
            //ModuleID
            customedTable.Columns.Add("ModuleID", typeof(string));
            //ModuleName
            customedTable.Columns.Add("ModuleName", typeof(string));

            customedTable.Columns.Add("Value", typeof(string));
            customedTable.Columns.Add("Exportable", typeof(int));
            customedTable.Columns.Add("PropertyDesc", typeof(string));
            customedTable.Columns.Add("PropertyOptions", typeof(string));
            customedTable.Columns.Add("Inheritance", typeof(int));
            customedTable.Columns.Add("PropertyType", typeof(int));
            customedTable.Columns.Add("IsHidden", typeof(int));
            customedTable.Columns.Add("OrderingPos", typeof(int));
            customedTable.Columns.Add("OrderNo", typeof(int));
            customedTable.Columns.Add("Domain", typeof(string));
            customedTable.Columns.Add("Type", typeof(int));
            return customedTable;



            ////ConfigName
            //DataColumn configNameColumn = new DataColumn();
            //configNameColumn.ColumnName = "ConfigName";
            //configNameColumn.DataType
            //customedTable.Columns.Add(configNameColumn);

            ////ModuleID
            //DataColumn moduleIDColumn = new DataColumn();
            //moduleIDColumn.ColumnName = "ModuleID";
            //customedTable.Columns.Add(moduleIDColumn);

            ////ModuleName
            //DataColumn moduleNameColumn = new DataColumn();
            //moduleNameColumn.ColumnName = "ModuleName";
            //customedTable.Columns.Add(moduleNameColumn);

            ////Value
            //DataColumn valueColumn = new DataColumn();
            //valueColumn.ColumnName = "Value";
            //customedTable.Columns.Add(valueColumn);

            ////Exportable
            //DataColumn exportableColumn = new DataColumn();
            //exportableColumn.ColumnName = "Exportable";
            //customedTable.Columns.Add(exportableColumn);

            ////PropertyDesc
            //DataColumn propertyDescColumn = new DataColumn();
            //propertyDescColumn.ColumnName = "PropertyDesc";
            //customedTable.Columns.Add(propertyDescColumn);

            ////PropertyOptions
            //DataColumn propertyOptionsColumn = new DataColumn();
            //propertyOptionsColumn.ColumnName = "PropertyOptions";
            //customedTable.Columns.Add(propertyOptionsColumn);

            ////Inheritance
            //DataColumn inheritanceColumn = new DataColumn();
            //inheritanceColumn.ColumnName = "Inheritance";
            //customedTable.Columns.Add(inheritanceColumn);

            ////PropertyType
            //DataColumn propertyTypeColumn = new DataColumn();
            //propertyTypeColumn.ColumnName = "PropertyType";
            //customedTable.Columns.Add(propertyTypeColumn);

            ////IsHidden
            //DataColumn isHiddenColumn = new DataColumn();
            //isHiddenColumn.ColumnName = "IsHidden";
            //customedTable.Columns.Add(isHiddenColumn);

            ////OrderingPos
            //DataColumn orderingPosColumn = new DataColumn();
            //orderingPosColumn.ColumnName = "OrderingPos";
            //customedTable.Columns.Add(orderingPosColumn);

            ////Domain
            //DataColumn DomainColumn = new DataColumn();
            //DomainColumn.ColumnName = "Domain";
            //customedTable.Columns.Add(DomainColumn);

            ////Type
            //DataColumn typeColumn = new DataColumn();
            //typeColumn.ColumnName = "Type";
            //customedTable.Columns.Add(typeColumn);

            //return customedTable;
        }
    }
}
