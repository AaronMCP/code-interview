using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;


namespace Hys.CareAgent.Common
{
    /// <summary>
    /// The class for generate barcode and print barcode
    /// </summary>
    public class Barcode
    {

        public Barcode()
        {
        }
        public enum AlignType
        {
            Left, Center, Right
        }

        public enum BarCodeWeight
        {
            Small = 1, Medium, Large
        }

        private AlignType align = AlignType.Center;
        private String code = "1234567890";
        private int leftMargin = 10;
        private int topMargin = 10;
        private int height = 50;
        private bool showHeader = true;
        private bool showFooter = true;
        private String headerText = "BarCode Demo";
        private BarCodeWeight weight = BarCodeWeight.Small;
        private Font headerFont = new Font("Courier", 18);
        private Font footerFont = new Font("Courier", 8);
        private readonly int Width = 336;
        private readonly int Height = 176;


        public AlignType VertAlign
        {
            get { return align; }
            set { align = value; }
        }

        public String BarCode
        {
            get { return code; }
            set { code = value.ToUpper(); }
        }

        public int BarCodeHeight
        {
            get { return height; }
            set { height = value; }
        }

        public int LeftMargin
        {
            get { return leftMargin; }
            set { leftMargin = value; }
        }

        public int TopMargin
        {
            get { return topMargin; }
            set { topMargin = value; }
        }

        public bool ShowHeader
        {
            get { return showHeader; }
            set { showHeader = value; }
        }

        public bool ShowFooter
        {
            get { return showFooter; }
            set { showFooter = value; }
        }

        public String HeaderText
        {
            get { return headerText; }
            set { headerText = value; }
        }

        public BarCodeWeight Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public Font HeaderFont
        {
            get { return headerFont; }
            set { headerFont = value; }
        }

        public Font FooterFont
        {
            get { return footerFont; }
            set { footerFont = value; }
        }

        readonly String alphabet39 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

        readonly String[] coded39Char = 
		{
			/* 0 */ "000110100", 
			/* 1 */ "100100001", 
			/* 2 */ "001100001", 
			/* 3 */ "101100000",
			/* 4 */ "000110001", 
			/* 5 */ "100110000", 
			/* 6 */ "001110000", 
			/* 7 */ "000100101",
			/* 8 */ "100100100", 
			/* 9 */ "001100100", 
			/* A */ "100001001", 
			/* B */ "001001001",
			/* C */ "101001000", 
			/* D */ "000011001", 
			/* E */ "100011000", 
			/* F */ "001011000",
			/* G */ "000001101", 
			/* H */ "100001100", 
			/* I */ "001001100", 
			/* J */ "000011100",
			/* K */ "100000011", 
			/* L */ "001000011", 
			/* M */ "101000010", 
			/* N */ "000010011",
			/* O */ "100010010", 
			/* P */ "001010010", 
			/* Q */ "000000111", 
			/* R */ "100000110",
			/* S */ "001000110", 
			/* T */ "000010110", 
			/* U */ "110000001", 
			/* V */ "011000001",
			/* W */ "111000000", 
			/* X */ "010010001", 
			/* Y */ "110010000", 
			/* Z */ "011010000",
			/* - */ "010000101", 
			/* . */ "110000100", 
			/*' '*/ "011000100",
			/* $ */ "010101000",
			/* / */ "010100010", 
			/* + */ "010001010", 
			/* % */ "000101010", 
			/* * */ "010010100" 
		};


        /// <summary>
        /// Generate bar code image
        /// </summary>
        /// <param name="strHeader">The title for bar code</param>
        /// <param name="bShowFoot">true show footer,false not show footer</param>
        /// <param name="strCode">generate bar code by this param</param>
        /// <param name="bmp">the bmp for bar code</param>
        public void GenerateBarcode(string strHeader, bool bShowFoot, string strCode, ref Bitmap bmp)
        {
            try
            {
                HeaderText = strHeader;
                code = strCode;
                ShowFooter = bShowFoot;

                Font font = this.footerFont;
                bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Graphics g = Graphics.FromImage(bmp);
                g.FillRectangle(Brushes.White, 0, 0, Width, Height);

                String str = '*' + code.ToUpper() + '*';
                int strLength = str.Length;

                if (IsCodeError())
                {
                    g.DrawString("INVALID BAR CODE TEXT", font, Brushes.Red, 10, 10);
                    return;
                }

                String encodedString = "";

                encodedString = AddInterCharacterGap(str, strLength);

                int encodedStringLength = encodedString.Length;
                int widthOfBarCodeString = 0;
                double wideToNarrowRatio = 3;

                if (align != AlignType.Left)
                {
                    widthOfBarCodeString = GetWidthOfBarCode(encodedString, encodedStringLength, wideToNarrowRatio);
                }

                int x = 0;
                int wid = 0;
                int yTop = 0;
                SizeF hSize = g.MeasureString(headerText, headerFont);
                SizeF fSize = g.MeasureString(code, footerFont);

                int headerX = 0;
                int footerX = 0;

                GetPositionByALign(widthOfBarCodeString, ref x, ref hSize, ref fSize, ref headerX, ref footerX);
                GetTopByMargin(g, ref yTop, ref hSize, headerX);

                if (showHeader)
                {
                    g.DrawString(headerText, headerFont, Brushes.Black, headerX, topMargin);
                }

                for (int i = 0; i < encodedStringLength; i++)
                {
                    wid = GetWidthByWideToNarrowRatio(encodedString, wideToNarrowRatio, i);
                    g.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White, x, yTop, wid, height);

                    x += wid;
                }

                yTop += height;

                if (showFooter)
                    g.DrawString(code, footerFont, Brushes.Black, footerX, yTop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void GetTopByMargin(Graphics g, ref int yTop, ref SizeF hSize, int headerX)
        {
            if (showHeader)
            {
                yTop = (int)hSize.Height + topMargin;
                g.DrawString(headerText, headerFont, Brushes.Black, headerX, topMargin);
            }
            else
            {
                yTop = topMargin;
            }
        }

        private int GetWidthByWideToNarrowRatio(String encodedString, double wideToNarrowRatio, int i)
        {
            int newWid = 0;
            if (encodedString[i] == '1')
                newWid = (int)(wideToNarrowRatio * (int)weight);
            else
                newWid = (int)weight;
            return newWid;
        }

        private void GetPositionByALign(int widthOfBarCodeString, ref int x, ref SizeF hSize, ref SizeF fSize, ref int headerX, ref int footerX)
        {
            if (align == AlignType.Left)
            {
                x = leftMargin;
                headerX = leftMargin;
                footerX = leftMargin;
            }
            else if (align == AlignType.Center)
            {
                x = (Width - widthOfBarCodeString) / 2;
                headerX = (Width - (int)hSize.Width) / 2;
                footerX = (Width - (int)fSize.Width) / 2;
            }
            else
            {
                x = Width - widthOfBarCodeString - leftMargin;
                headerX = Width - (int)hSize.Width - leftMargin;
                footerX = Width - (int)fSize.Width - leftMargin;
            }
        }

        private int GetWidthOfBarCode(String encodedString, int encodedStringLength, double wideToNarrowRatio)
        {
            int widthOfBarCodeString = 0;
            for (int i = 0; i < encodedStringLength; i++)
            {
                if (encodedString[i] == '1')
                    widthOfBarCodeString += (int)(wideToNarrowRatio * (int)weight);
                else
                    widthOfBarCodeString += (int)weight;
            }
            return widthOfBarCodeString;
        }

        private string AddInterCharacterGap(String str, int strLength)
        {
            String encodedString = "";
            String intercharacterGap = "0";
            for (int i = 0; i < strLength; i++)
            {
                if (i > 0)
                    encodedString += intercharacterGap;

                encodedString += coded39Char[alphabet39.IndexOf(str[i])];
            }
            return encodedString;
        }

        private bool IsCodeError()
        {
            bool result = false;

            for (int i = 0; i < code.Length; i++)
            {
                if (alphabet39.IndexOf(code[i]) == -1 || code[i] == '*')
                {
                    return true;
                }
            }

            return result;
        }
    }
}