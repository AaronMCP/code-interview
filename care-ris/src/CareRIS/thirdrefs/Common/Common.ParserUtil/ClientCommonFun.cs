using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LogServer;

namespace Common.ParserUtil
{
    public class ClientCommonFun
    {
        static LogManager _logger = new LogManager();



        public static System.Windows.Forms.DialogResult ShowMessageBox_Error(string msg)
        {
            return MessageBox.Show(msg);
        }
        
      
        public static void SetDataRowValue(System.Data.DataRow dr, string columnName, string szValue)
        {
            try
            {
                if (!dr.Table.Columns.Contains(columnName))
                {
                    string msg = dr.Table.TableName + " dose not contain the column " + columnName;
                    System.Diagnostics.Debug.Assert(false, msg);
                    return;
                }

                Type datatyp = dr.Table.Columns[columnName].DataType;

                if (datatyp == typeof(System.Int32) || datatyp == typeof(System.Int16) || datatyp == typeof(System.Int64))
                {
                    int tmp;
                    if (szValue == null || szValue == string.Empty)
                    {
                        dr[columnName] = System.DBNull.Value;
                    }
                    else if (int.TryParse(szValue, out tmp))
                    {
                        dr[columnName] = tmp;
                    }
                }
                else if (datatyp == typeof(System.Double) || datatyp == typeof(System.Decimal))
                {
                    float tmp;
                    if (szValue == null || szValue == string.Empty)
                    {
                        dr[columnName] = System.DBNull.Value;
                    }
                    else if (float.TryParse(szValue, out tmp))
                    {
                        dr[columnName] = tmp;
                    }
                }
                else if (datatyp == typeof(System.DateTime))
                {
                    DateTime dtTmp;
                    if (szValue == null || szValue == string.Empty)
                    {
                        dr[columnName] = System.DBNull.Value;
                    }
                    else if (DateTime.TryParse(szValue, out dtTmp))
                    {
                        dr[columnName] = dtTmp;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid DateTime string, " + dr.Table.TableName + ", " + columnName + ", value=" + szValue);
                    }
                }
                else if (datatyp == typeof(System.Guid))
                {
                    if (string.IsNullOrEmpty(szValue))
                    {
                        dr[columnName] = System.DBNull.Value;
                    }
                    else
                    {
                        dr[columnName] = szValue;
                    }
                }
                else
                {
                    dr[columnName] = szValue;
                }
            }
            catch (System.Exception ex)
            {
                RISLog_Error("ERROR on updating Column=" + columnName + ", value=" + szValue + ", msg=" + ex.Message);
            }
        }

        public static void RISLog_Error(string errDesc)
        {
            System.Diagnostics.Debug.WriteLine("error=" + errDesc);

            _logger.Error(
                0x0400,
                "Common",
                0,
                errDesc,
                "",
                "",
                0
                );
        }

        public static void RISLog_Info(string errDesc)
        {
            _logger.Info(
                0x0400,
                "Common",
                0,
                errDesc,
                "",
                "",
                0
                );
        }

        public static string RunPath
        {
            get
            {
                string str = System.Windows.Forms.Application.ExecutablePath;
                int pos = str.LastIndexOf('\\');
                if (pos > 0)
                {
                    str = str.Substring(0, pos);
                }

                return str;
            }
        }
    }

    public class ImageUtil
    {
        public static Image FromFile(string filepath, int fixedwidth)
        {
            Image img = Image.FromFile(filepath);
            if (img == null || img.Width == 0)
                return null;

            Image newimg = new Bitmap(img, fixedwidth, img.Height * fixedwidth / img.Width);

            return newimg;
        }
    }
}
