#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Andy Bu                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace CommonGlobalSettings
{
    public partial class ButtonMenu : CheckBox
    {
        /// <summary>
        /// MenuStrip to handle
        /// </summary>
        private ContextMenuStrip menu = null;
        private ButtonMenuDirection direction = ButtonMenuDirection.AboveLeft;
        private ToolStripDropDownDirection preferredDirection = ToolStripDropDownDirection.AboveLeft;
        private ButtonMenuStyle style = ButtonMenuStyle.Flat;

        /// <summary>
        /// Switch between Flat and button appearence
        /// </summary>
        public ButtonMenuStyle Style
        {

            get
            {
                return style;
            }
            set
            {
                style = value;
                updateStyle();
            }

        }
        /*
         * Standard getter and setter
         */
        public ContextMenuStrip Menu
        {
            get
            {
                return menu;
            }
            set
            {
                /*
                 * If menu != null then we have a menu to clean before to change
                 */
                if (menu != null)
                    menu.Closed -= menu_Closed;

                menu = value;
                if (menu != null)
                    menu.Closed += menu_Closed;
            }

        }

        public ButtonMenuDirection Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public ToolStripDropDownDirection PreferredDirection
        {
            get
            {
                return preferredDirection;
            }
            set
            {
                preferredDirection = value;
            }
        }

        public ButtonMenu()
        {
            /*
             * Setting button style properties
             */
            // this.Appearance = Appearance.Button;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.FlatStyle = FlatStyle.Popup;
            //this.AutoSize = false;
            //base.AutoSize = false;
            this.CheckedChanged += new EventHandler(ButtonMenu_CheckedChanged);
            updateStyle();
        }

        void menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            /*
             * Popup Menu closing... raise the button
             */
            if (!frombutton)
                Checked = false;

        }
        /// <summary>
        /// Closing menu from button ? 
        /// </summary>
        private bool frombutton = false;
        private void ButtonMenu_CheckedChanged(object sender, EventArgs e)
        {
            // No menu...
            if (menu == null)
                return;
            frombutton = true;
            if (Checked && !Menu.Visible)
                DisplayMenu();
            if (!Checked && Menu.Visible)
                HideMenu();
            Refresh();
            frombutton = false;
        }

        /// <summary>
        /// Method used to handle graphics subsystem when menu must be showed
        /// </summary>
        private void DisplayMenu()
        {
            if (menu == null)
                return;
            ToolStripDropDownDirection menuDirection = ToolStripDropDownDirection.Default;
            Point offset = new Point();
            Point p = PointToScreen(this.Location);
            switch (direction)
            {
                case ButtonMenuDirection.AboveLeft:
                    menuDirection = ToolStripDropDownDirection.AboveLeft;
                    break;
                case ButtonMenuDirection.AboveRight:
                    menuDirection = ToolStripDropDownDirection.AboveRight;
                    break;
                case ButtonMenuDirection.BelowLeft:
                    menuDirection = ToolStripDropDownDirection.BelowLeft;
                    break;
                case ButtonMenuDirection.BelowRight:
                    menuDirection = ToolStripDropDownDirection.BelowRight;
                    break;
                case ButtonMenuDirection.Default:
                    menuDirection = ToolStripDropDownDirection.Default;
                    break;
                case ButtonMenuDirection.Left:
                    menuDirection = ToolStripDropDownDirection.Left;
                    break;
                case ButtonMenuDirection.Right:
                    menuDirection = ToolStripDropDownDirection.Right;
                    break;
                case ButtonMenuDirection.Automatic:
                    menuDirection = preferredDirection;
                    break;
            }
            offset = CalculateMenuOffset(menuDirection);
            p.Offset(offset);
            Point menuUpperLeft = new Point();
            Point menuBottomRight = new Point();
            switch (menuDirection)
            {
                case ToolStripDropDownDirection.AboveLeft:
                    menuUpperLeft.X = p.X;
                    menuUpperLeft.Y = p.Y - menu.Height;
                    menuBottomRight.X = p.X + menu.Width;
                    menuBottomRight.Y = p.Y;
                    break;
                case ToolStripDropDownDirection.AboveRight:
                    menuUpperLeft.X = p.X;
                    menuUpperLeft.Y = p.Y - menu.Height;
                    menuBottomRight.X = p.X + menu.Width;
                    menuBottomRight.Y = p.Y;
                    break;
                case ToolStripDropDownDirection.BelowLeft:
                    menuUpperLeft.X = p.X;
                    menuUpperLeft.Y = p.Y + menu.Height;
                    menuBottomRight.X = p.X + menu.Width;
                    menuBottomRight.Y = p.Y;
                    break;
                case ToolStripDropDownDirection.BelowRight:
                    menuUpperLeft.X = p.X;
                    menuUpperLeft.Y = p.Y + menu.Height;
                    menuBottomRight.X = p.X + menu.Width;
                    menuBottomRight.Y = p.Y;
                    break;
                case ToolStripDropDownDirection.Default:
                    menuUpperLeft.X = p.X;
                    menuUpperLeft.Y = p.Y + menu.Height;
                    menuBottomRight.X = p.X + menu.Width;
                    menuBottomRight.Y = p.Y;
                    break;
                case ToolStripDropDownDirection.Left:
                    menuUpperLeft.X = p.X - menu.Width;
                    menuUpperLeft.Y = p.Y;
                    menuBottomRight.X = p.X;
                    menuBottomRight.Y = p.Y + menu.Height;
                    break;
                case ToolStripDropDownDirection.Right:
                    menuUpperLeft.X = p.X - menu.Width;
                    menuUpperLeft.Y = p.Y;
                    menuBottomRight.X = p.X + this.Width;
                    menuBottomRight.Y = p.Y + menu.Height;
                    break;
            }
            Screen s = Screen.FromControl(this);
            int sw = s.WorkingArea.Width;
            int sh = s.WorkingArea.Height;
            if (menuUpperLeft.X < 0 || menuUpperLeft.Y < 0 || menuBottomRight.X > sw || menuBottomRight.Y > sh)
            {
                //Reverse direction
                switch (menuDirection)
                {
                    case ToolStripDropDownDirection.AboveLeft:
                        menuDirection = ToolStripDropDownDirection.BelowLeft;
                        break;
                    case ToolStripDropDownDirection.AboveRight:
                        menuDirection = ToolStripDropDownDirection.BelowRight;
                        break;
                    case ToolStripDropDownDirection.BelowLeft:
                        menuDirection = ToolStripDropDownDirection.AboveLeft;
                        break;
                    case ToolStripDropDownDirection.BelowRight:
                        menuDirection = ToolStripDropDownDirection.AboveRight;
                        break;
                    case ToolStripDropDownDirection.Default:
                        menuDirection = ToolStripDropDownDirection.Default;
                        break;
                    case ToolStripDropDownDirection.Left:
                        menuDirection = ToolStripDropDownDirection.Right;
                        break;
                    case ToolStripDropDownDirection.Right:
                        menuDirection = ToolStripDropDownDirection.Left;
                        break;
                }
                offset = CalculateMenuOffset(menuDirection);
            }
            menu.Show(this, offset, menuDirection);
        }

        private Point CalculateMenuOffset(ToolStripDropDownDirection direction)
        {
            Point offset = new Point();
            switch (direction)
            {
                case ToolStripDropDownDirection.AboveLeft:
                    offset.X = menu.Width;
                    offset.Y = 0;
                    break;
                case ToolStripDropDownDirection.AboveRight:
                    offset.X = this.Width - menu.Width;
                    offset.Y = 0;
                    break;
                case ToolStripDropDownDirection.BelowLeft:
                    offset.X = menu.Width;
                    offset.Y = this.Height;
                    break;
                case ToolStripDropDownDirection.BelowRight:
                    offset.X = this.Width - menu.Width;
                    offset.Y = this.Height;
                    break;
                case ToolStripDropDownDirection.Default:
                    offset.X = (this.Width - menu.Width) / 2;
                    offset.Y = this.Height;
                    break;
                case ToolStripDropDownDirection.Left:
                    offset.X = 0;
                    offset.Y = 0;
                    break;
                case ToolStripDropDownDirection.Right:
                    offset.X = this.Width;
                    offset.Y = 0;
                    break;
            }
            return offset;
        }
        /// <summary>
        /// Unique method to update Style
        /// </summary>
        private void updateStyle()
        {
            switch (style)
            {
                case ButtonMenuStyle.Flat:
                    this.FlatStyle = FlatStyle.Popup;
                    break;
                case ButtonMenuStyle.Button:
                    this.FlatStyle = FlatStyle.Standard;
                    break;
            }
        }
        /// <summary>
        /// Method used to handle graphics subsystem when menu must be hided
        /// </summary>
        private void HideMenu()
        {
            menu.Hide();
        }

    }
    public enum ButtonMenuStyle
    {
        Flat = 0,
        Button = 1,
    }
    public enum ButtonMenuDirection
    {
        AboveLeft = 0,
        AboveRight = 1,
        BelowLeft = 2,
        BelowRight = 3,
        Left = 4,
        Right = 5,
        Default = 6,
        Automatic = 7
    }
}
