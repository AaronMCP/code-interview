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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Common.Controls.TimePicker
{
	/// <summary>
	/// The HourMinuteSelectorForm is used to choose an hour and a minute element of
	/// a time.  It displays a form where the user can simply click on a button that
	/// represents the time that he wants.  To exit the form, the user can do a left
	/// click outside the form or do a right click on a button.  Remember that a
	/// right click will also select the button where your mouse is currently placed.
	/// </summary>
	internal class HourMinuteSelectorForm : System.Windows.Forms.Form
	{
		#region Constants
		private static String H = "H";
		private static String M = "M";
		#endregion

		#region Properties
		public TimeSpan SelectedTime
		{
			get { return m_sSelectedTime; }
			set 
			{ 
				m_sSelectedTime = value; 
				SetHour(value.Hours);
				SetMinute(value.Minutes);
			}
		}
		private TimeSpan m_sSelectedTime;

		public Color ButtonColor
		{
			get { return m_oButtonColor; }
			set 
			{ 
				m_oButtonColor = value; 
				foreach(Control c in this.Controls)
				{
					if (c is Button)
					{
						c.BackColor = value;
					}
				}
			}
		}
		private Color m_oButtonColor;

		public Color SelectedColor
		{
			get { return m_oSelectedColor; }
			set { m_oSelectedColor = value; }
		}
		private Color m_oSelectedColor;

		public Color SeparatorColor
		{
			get { return m_oLblSeparator.BackColor; }
			set { m_oLblSeparator.BackColor = value; }
		}
		#endregion

		private Button m_oOldHourButton;
		private Button m_oOldMinuteButton;

		private System.Windows.Forms.Button m_oBtn0Hour;
		private System.Windows.Forms.Button m_oBtn1Hour;
		private System.Windows.Forms.Button m_oBtn2Hour;
		private System.Windows.Forms.Button m_oBtn3Hour;
		private System.Windows.Forms.Button m_oBtn4Hour;
		private System.Windows.Forms.Button m_oBtn5Hour;
		private System.Windows.Forms.Button m_oBtn6Hour;
		private System.Windows.Forms.Button m_oBtn7Hour;
		private System.Windows.Forms.Button m_oBtn8Hour;
		private System.Windows.Forms.Button m_oBtn9Hour;
		private System.Windows.Forms.Button m_oBtn10Hour;
		private System.Windows.Forms.Button m_oBtn11Hour;
		private System.Windows.Forms.Button m_oBtn12Hour;
		private System.Windows.Forms.Button m_oBtn13Hour;
		private System.Windows.Forms.Button m_oBtn14Hour;
		private System.Windows.Forms.Button m_oBtn15Hour;
		private System.Windows.Forms.Button m_oBtn16Hour;
		private System.Windows.Forms.Button m_oBtn17Hour;
		private System.Windows.Forms.Button m_oBtn18Hour;
		private System.Windows.Forms.Button m_oBtn19Hour;
		private System.Windows.Forms.Button m_oBtn20Hour;
		private System.Windows.Forms.Button m_oBtn21Hour;
		private System.Windows.Forms.Button m_oBtn22Hour;
		private System.Windows.Forms.Button m_oBtn23Hour;
		private System.Windows.Forms.Button m_oBtn0Minute;
		private System.Windows.Forms.Button m_oBtn1Minute;
		private System.Windows.Forms.Button m_oBtn2Minute;
		private System.Windows.Forms.Button m_oBtn3Minute;
		private System.Windows.Forms.Button m_oBtn4Minute;
		private System.Windows.Forms.Button m_oBtn5Minute;
		private System.Windows.Forms.Button m_oBtn6Minute;
		private System.Windows.Forms.Button m_oBtn7Minute;
		private System.Windows.Forms.Button m_oBtn8Minute;
		private System.Windows.Forms.Button m_oBtn9Minute;
		private System.Windows.Forms.Button m_oBtn10Minute;
		private System.Windows.Forms.Button m_oBtn11Minute;
		private System.Windows.Forms.Button m_oBtn12Minute;
		private System.Windows.Forms.Button m_oBtn13Minute;
		private System.Windows.Forms.Button m_oBtn14Minute;
		private System.Windows.Forms.Button m_oBtn15Minute;
		private System.Windows.Forms.Button m_oBtn16Minute;
		private System.Windows.Forms.Button m_oBtn17Minute;
		private System.Windows.Forms.Button m_oBtn18Minute;
		private System.Windows.Forms.Button m_oBtn19Minute;
		private System.Windows.Forms.Button m_oBtn20Minute;
		private System.Windows.Forms.Button m_oBtn21Minute;
		private System.Windows.Forms.Button m_oBtn22Minute;
		private System.Windows.Forms.Button m_oBtn23Minute;
		private System.Windows.Forms.Button m_oBtn24Minute;
		private System.Windows.Forms.Button m_oBtn25Minute;
		private System.Windows.Forms.Button m_oBtn26Minute;
		private System.Windows.Forms.Button m_oBtn27Minute;		
		private System.Windows.Forms.Button m_oBtn28Minute;
		private System.Windows.Forms.Button m_oBtn29Minute;		
		private System.Windows.Forms.Button m_oBtn30Minute;
		private System.Windows.Forms.Button m_oBtn31Minute;
		private System.Windows.Forms.Button m_oBtn32Minute;
		private System.Windows.Forms.Button m_oBtn33Minute;
		private System.Windows.Forms.Button m_oBtn34Minute;
		private System.Windows.Forms.Button m_oBtn35Minute;		
		private System.Windows.Forms.Button m_oBtn36Minute;
		private System.Windows.Forms.Button m_oBtn37Minute;
		private System.Windows.Forms.Button m_oBtn38Minute;
		private System.Windows.Forms.Button m_oBtn39Minute;
		private System.Windows.Forms.Button m_oBtn40Minute;
		private System.Windows.Forms.Button m_oBtn41Minute;
		private System.Windows.Forms.Button m_oBtn42Minute;
		private System.Windows.Forms.Button m_oBtn43Minute;
		private System.Windows.Forms.Button m_oBtn44Minute;
		private System.Windows.Forms.Button m_oBtn45Minute;
		private System.Windows.Forms.Button m_oBtn46Minute;
		private System.Windows.Forms.Button m_oBtn47Minute;
		private System.Windows.Forms.Button m_oBtn48Minute;
		private System.Windows.Forms.Button m_oBtn49Minute;
		private System.Windows.Forms.Button m_oBtn50Minute;
		private System.Windows.Forms.Button m_oBtn51Minute;
		private System.Windows.Forms.Button m_oBtn52Minute;
		private System.Windows.Forms.Button m_oBtn53Minute;
		private System.Windows.Forms.Button m_oBtn54Minute;
		private System.Windows.Forms.Button m_oBtn55Minute;
		private System.Windows.Forms.Button m_oBtn56Minute;
		private System.Windows.Forms.Button m_oBtn57Minute;		
		private System.Windows.Forms.Button m_oBtn58Minute;
		private System.Windows.Forms.Button m_oBtn59Minute;
		private System.Windows.Forms.Label m_oLblSeparator;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A constructor that instanciate its data members.
		/// </summary>
		public HourMinuteSelectorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_oOldHourButton = new Button();
			m_oOldMinuteButton = new Button();

			m_sSelectedTime = TimeSpan.MinValue;
			m_oSelectedColor = Color.White;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_oBtn0Hour = new System.Windows.Forms.Button();
            this.m_oBtn1Hour = new System.Windows.Forms.Button();
            this.m_oBtn2Hour = new System.Windows.Forms.Button();
            this.m_oBtn3Hour = new System.Windows.Forms.Button();
            this.m_oBtn4Hour = new System.Windows.Forms.Button();
            this.m_oBtn5Hour = new System.Windows.Forms.Button();
            this.m_oBtn6Hour = new System.Windows.Forms.Button();
            this.m_oBtn7Hour = new System.Windows.Forms.Button();
            this.m_oBtn8Hour = new System.Windows.Forms.Button();
            this.m_oBtn9Hour = new System.Windows.Forms.Button();
            this.m_oBtn10Hour = new System.Windows.Forms.Button();
            this.m_oBtn11Hour = new System.Windows.Forms.Button();
            this.m_oBtn12Hour = new System.Windows.Forms.Button();
            this.m_oBtn13Hour = new System.Windows.Forms.Button();
            this.m_oBtn14Hour = new System.Windows.Forms.Button();
            this.m_oBtn15Hour = new System.Windows.Forms.Button();
            this.m_oBtn16Hour = new System.Windows.Forms.Button();
            this.m_oBtn17Hour = new System.Windows.Forms.Button();
            this.m_oBtn18Hour = new System.Windows.Forms.Button();
            this.m_oBtn19Hour = new System.Windows.Forms.Button();
            this.m_oBtn20Hour = new System.Windows.Forms.Button();
            this.m_oBtn21Hour = new System.Windows.Forms.Button();
            this.m_oBtn22Hour = new System.Windows.Forms.Button();
            this.m_oBtn23Hour = new System.Windows.Forms.Button();
            this.m_oLblSeparator = new System.Windows.Forms.Label();
            this.m_oBtn52Minute = new System.Windows.Forms.Button();
            this.m_oBtn53Minute = new System.Windows.Forms.Button();
            this.m_oBtn51Minute = new System.Windows.Forms.Button();
            this.m_oBtn50Minute = new System.Windows.Forms.Button();
            this.m_oBtn42Minute = new System.Windows.Forms.Button();
            this.m_oBtn43Minute = new System.Windows.Forms.Button();
            this.m_oBtn31Minute = new System.Windows.Forms.Button();
            this.m_oBtn40Minute = new System.Windows.Forms.Button();
            this.m_oBtn32Minute = new System.Windows.Forms.Button();
            this.m_oBtn33Minute = new System.Windows.Forms.Button();
            this.m_oBtn21Minute = new System.Windows.Forms.Button();
            this.m_oBtn30Minute = new System.Windows.Forms.Button();
            this.m_oBtn22Minute = new System.Windows.Forms.Button();
            this.m_oBtn23Minute = new System.Windows.Forms.Button();
            this.m_oBtn41Minute = new System.Windows.Forms.Button();
            this.m_oBtn20Minute = new System.Windows.Forms.Button();
            this.m_oBtn12Minute = new System.Windows.Forms.Button();
            this.m_oBtn13Minute = new System.Windows.Forms.Button();
            this.m_oBtn11Minute = new System.Windows.Forms.Button();
            this.m_oBtn10Minute = new System.Windows.Forms.Button();
            this.m_oBtn2Minute = new System.Windows.Forms.Button();
            this.m_oBtn3Minute = new System.Windows.Forms.Button();
            this.m_oBtn1Minute = new System.Windows.Forms.Button();
            this.m_oBtn0Minute = new System.Windows.Forms.Button();
            this.m_oBtn54Minute = new System.Windows.Forms.Button();
            this.m_oBtn44Minute = new System.Windows.Forms.Button();
            this.m_oBtn34Minute = new System.Windows.Forms.Button();
            this.m_oBtn24Minute = new System.Windows.Forms.Button();
            this.m_oBtn14Minute = new System.Windows.Forms.Button();
            this.m_oBtn4Minute = new System.Windows.Forms.Button();
            this.m_oBtn55Minute = new System.Windows.Forms.Button();
            this.m_oBtn45Minute = new System.Windows.Forms.Button();
            this.m_oBtn35Minute = new System.Windows.Forms.Button();
            this.m_oBtn25Minute = new System.Windows.Forms.Button();
            this.m_oBtn15Minute = new System.Windows.Forms.Button();
            this.m_oBtn5Minute = new System.Windows.Forms.Button();
            this.m_oBtn56Minute = new System.Windows.Forms.Button();
            this.m_oBtn46Minute = new System.Windows.Forms.Button();
            this.m_oBtn36Minute = new System.Windows.Forms.Button();
            this.m_oBtn26Minute = new System.Windows.Forms.Button();
            this.m_oBtn16Minute = new System.Windows.Forms.Button();
            this.m_oBtn6Minute = new System.Windows.Forms.Button();
            this.m_oBtn57Minute = new System.Windows.Forms.Button();
            this.m_oBtn47Minute = new System.Windows.Forms.Button();
            this.m_oBtn37Minute = new System.Windows.Forms.Button();
            this.m_oBtn27Minute = new System.Windows.Forms.Button();
            this.m_oBtn17Minute = new System.Windows.Forms.Button();
            this.m_oBtn7Minute = new System.Windows.Forms.Button();
            this.m_oBtn58Minute = new System.Windows.Forms.Button();
            this.m_oBtn48Minute = new System.Windows.Forms.Button();
            this.m_oBtn38Minute = new System.Windows.Forms.Button();
            this.m_oBtn28Minute = new System.Windows.Forms.Button();
            this.m_oBtn18Minute = new System.Windows.Forms.Button();
            this.m_oBtn8Minute = new System.Windows.Forms.Button();
            this.m_oBtn59Minute = new System.Windows.Forms.Button();
            this.m_oBtn49Minute = new System.Windows.Forms.Button();
            this.m_oBtn39Minute = new System.Windows.Forms.Button();
            this.m_oBtn29Minute = new System.Windows.Forms.Button();
            this.m_oBtn19Minute = new System.Windows.Forms.Button();
            this.m_oBtn9Minute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_oBtn0Hour
            // 
            this.m_oBtn0Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn0Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn0Hour.Location = new System.Drawing.Point(0, 0);
            this.m_oBtn0Hour.Name = "m_oBtn0Hour";
            this.m_oBtn0Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn0Hour.TabIndex = 0;
            this.m_oBtn0Hour.Tag = "H";
            this.m_oBtn0Hour.Text = "0";
            this.m_oBtn0Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn0Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn1Hour
            // 
            this.m_oBtn1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn1Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn1Hour.Location = new System.Drawing.Point(24, 0);
            this.m_oBtn1Hour.Name = "m_oBtn1Hour";
            this.m_oBtn1Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn1Hour.TabIndex = 2;
            this.m_oBtn1Hour.Tag = "H";
            this.m_oBtn1Hour.Text = "1";
            this.m_oBtn1Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn1Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn2Hour
            // 
            this.m_oBtn2Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn2Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn2Hour.Location = new System.Drawing.Point(48, 0);
            this.m_oBtn2Hour.Name = "m_oBtn2Hour";
            this.m_oBtn2Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn2Hour.TabIndex = 3;
            this.m_oBtn2Hour.Tag = "H";
            this.m_oBtn2Hour.Text = "2";
            this.m_oBtn2Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn2Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn3Hour
            // 
            this.m_oBtn3Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn3Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn3Hour.Location = new System.Drawing.Point(72, 0);
            this.m_oBtn3Hour.Name = "m_oBtn3Hour";
            this.m_oBtn3Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn3Hour.TabIndex = 4;
            this.m_oBtn3Hour.Tag = "H";
            this.m_oBtn3Hour.Text = "3";
            this.m_oBtn3Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn3Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn4Hour
            // 
            this.m_oBtn4Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn4Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn4Hour.Location = new System.Drawing.Point(0, 21);
            this.m_oBtn4Hour.Name = "m_oBtn4Hour";
            this.m_oBtn4Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn4Hour.TabIndex = 5;
            this.m_oBtn4Hour.Tag = "H";
            this.m_oBtn4Hour.Text = "4";
            this.m_oBtn4Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn4Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn5Hour
            // 
            this.m_oBtn5Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn5Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn5Hour.Location = new System.Drawing.Point(24, 21);
            this.m_oBtn5Hour.Name = "m_oBtn5Hour";
            this.m_oBtn5Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn5Hour.TabIndex = 6;
            this.m_oBtn5Hour.Tag = "H";
            this.m_oBtn5Hour.Text = "5";
            this.m_oBtn5Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn5Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn6Hour
            // 
            this.m_oBtn6Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn6Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn6Hour.Location = new System.Drawing.Point(48, 21);
            this.m_oBtn6Hour.Name = "m_oBtn6Hour";
            this.m_oBtn6Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn6Hour.TabIndex = 7;
            this.m_oBtn6Hour.Tag = "H";
            this.m_oBtn6Hour.Text = "6";
            this.m_oBtn6Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn6Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn7Hour
            // 
            this.m_oBtn7Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn7Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn7Hour.Location = new System.Drawing.Point(72, 21);
            this.m_oBtn7Hour.Name = "m_oBtn7Hour";
            this.m_oBtn7Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn7Hour.TabIndex = 8;
            this.m_oBtn7Hour.Tag = "H";
            this.m_oBtn7Hour.Text = "7";
            this.m_oBtn7Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn7Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn8Hour
            // 
            this.m_oBtn8Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn8Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn8Hour.Location = new System.Drawing.Point(0, 43);
            this.m_oBtn8Hour.Name = "m_oBtn8Hour";
            this.m_oBtn8Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn8Hour.TabIndex = 9;
            this.m_oBtn8Hour.Tag = "H";
            this.m_oBtn8Hour.Text = "8";
            this.m_oBtn8Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn8Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn9Hour
            // 
            this.m_oBtn9Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn9Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn9Hour.Location = new System.Drawing.Point(24, 43);
            this.m_oBtn9Hour.Name = "m_oBtn9Hour";
            this.m_oBtn9Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn9Hour.TabIndex = 10;
            this.m_oBtn9Hour.Tag = "H";
            this.m_oBtn9Hour.Text = "9";
            this.m_oBtn9Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn9Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn10Hour
            // 
            this.m_oBtn10Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn10Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn10Hour.Location = new System.Drawing.Point(48, 43);
            this.m_oBtn10Hour.Name = "m_oBtn10Hour";
            this.m_oBtn10Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn10Hour.TabIndex = 11;
            this.m_oBtn10Hour.Tag = "H";
            this.m_oBtn10Hour.Text = "10";
            this.m_oBtn10Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn10Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn11Hour
            // 
            this.m_oBtn11Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn11Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn11Hour.Location = new System.Drawing.Point(72, 43);
            this.m_oBtn11Hour.Name = "m_oBtn11Hour";
            this.m_oBtn11Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn11Hour.TabIndex = 12;
            this.m_oBtn11Hour.Tag = "H";
            this.m_oBtn11Hour.Text = "11";
            this.m_oBtn11Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn11Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn12Hour
            // 
            this.m_oBtn12Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn12Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn12Hour.Location = new System.Drawing.Point(0, 65);
            this.m_oBtn12Hour.Name = "m_oBtn12Hour";
            this.m_oBtn12Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn12Hour.TabIndex = 13;
            this.m_oBtn12Hour.Tag = "H";
            this.m_oBtn12Hour.Text = "12";
            this.m_oBtn12Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn12Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn13Hour
            // 
            this.m_oBtn13Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn13Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn13Hour.Location = new System.Drawing.Point(24, 65);
            this.m_oBtn13Hour.Name = "m_oBtn13Hour";
            this.m_oBtn13Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn13Hour.TabIndex = 14;
            this.m_oBtn13Hour.Tag = "H";
            this.m_oBtn13Hour.Text = "13";
            this.m_oBtn13Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn13Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn14Hour
            // 
            this.m_oBtn14Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn14Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn14Hour.Location = new System.Drawing.Point(48, 65);
            this.m_oBtn14Hour.Name = "m_oBtn14Hour";
            this.m_oBtn14Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn14Hour.TabIndex = 15;
            this.m_oBtn14Hour.Tag = "H";
            this.m_oBtn14Hour.Text = "14";
            this.m_oBtn14Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn14Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn15Hour
            // 
            this.m_oBtn15Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn15Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn15Hour.Location = new System.Drawing.Point(72, 65);
            this.m_oBtn15Hour.Name = "m_oBtn15Hour";
            this.m_oBtn15Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn15Hour.TabIndex = 16;
            this.m_oBtn15Hour.Tag = "H";
            this.m_oBtn15Hour.Text = "15";
            this.m_oBtn15Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn15Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn16Hour
            // 
            this.m_oBtn16Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn16Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn16Hour.Location = new System.Drawing.Point(0, 87);
            this.m_oBtn16Hour.Name = "m_oBtn16Hour";
            this.m_oBtn16Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn16Hour.TabIndex = 17;
            this.m_oBtn16Hour.Tag = "H";
            this.m_oBtn16Hour.Text = "16";
            this.m_oBtn16Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn16Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn17Hour
            // 
            this.m_oBtn17Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn17Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn17Hour.Location = new System.Drawing.Point(24, 87);
            this.m_oBtn17Hour.Name = "m_oBtn17Hour";
            this.m_oBtn17Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn17Hour.TabIndex = 18;
            this.m_oBtn17Hour.Tag = "H";
            this.m_oBtn17Hour.Text = "17";
            this.m_oBtn17Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn17Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn18Hour
            // 
            this.m_oBtn18Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn18Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn18Hour.Location = new System.Drawing.Point(48, 87);
            this.m_oBtn18Hour.Name = "m_oBtn18Hour";
            this.m_oBtn18Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn18Hour.TabIndex = 19;
            this.m_oBtn18Hour.Tag = "H";
            this.m_oBtn18Hour.Text = "18";
            this.m_oBtn18Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn18Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn19Hour
            // 
            this.m_oBtn19Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn19Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn19Hour.Location = new System.Drawing.Point(72, 87);
            this.m_oBtn19Hour.Name = "m_oBtn19Hour";
            this.m_oBtn19Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn19Hour.TabIndex = 20;
            this.m_oBtn19Hour.Tag = "H";
            this.m_oBtn19Hour.Text = "19";
            this.m_oBtn19Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn19Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn20Hour
            // 
            this.m_oBtn20Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn20Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn20Hour.Location = new System.Drawing.Point(0, 109);
            this.m_oBtn20Hour.Name = "m_oBtn20Hour";
            this.m_oBtn20Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn20Hour.TabIndex = 21;
            this.m_oBtn20Hour.Tag = "H";
            this.m_oBtn20Hour.Text = "20";
            this.m_oBtn20Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn20Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn21Hour
            // 
            this.m_oBtn21Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn21Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn21Hour.Location = new System.Drawing.Point(24, 109);
            this.m_oBtn21Hour.Name = "m_oBtn21Hour";
            this.m_oBtn21Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn21Hour.TabIndex = 22;
            this.m_oBtn21Hour.Tag = "H";
            this.m_oBtn21Hour.Text = "21";
            this.m_oBtn21Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn21Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn22Hour
            // 
            this.m_oBtn22Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn22Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn22Hour.Location = new System.Drawing.Point(48, 109);
            this.m_oBtn22Hour.Name = "m_oBtn22Hour";
            this.m_oBtn22Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn22Hour.TabIndex = 23;
            this.m_oBtn22Hour.Tag = "H";
            this.m_oBtn22Hour.Text = "22";
            this.m_oBtn22Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn22Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn23Hour
            // 
            this.m_oBtn23Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn23Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn23Hour.Location = new System.Drawing.Point(72, 109);
            this.m_oBtn23Hour.Name = "m_oBtn23Hour";
            this.m_oBtn23Hour.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn23Hour.TabIndex = 24;
            this.m_oBtn23Hour.Tag = "H";
            this.m_oBtn23Hour.Text = "23";
            this.m_oBtn23Hour.Click += new System.EventHandler(this.LeftHourClick);
            this.m_oBtn23Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oLblSeparator
            // 
            this.m_oLblSeparator.BackColor = System.Drawing.Color.RosyBrown;
            this.m_oLblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_oLblSeparator.Location = new System.Drawing.Point(96, 0);
            this.m_oLblSeparator.Name = "m_oLblSeparator";
            this.m_oLblSeparator.Size = new System.Drawing.Size(8, 135);
            this.m_oLblSeparator.TabIndex = 46;
            this.m_oLblSeparator.Click += new System.EventHandler(this.m_oLblSeparator_Click);
            // 
            // m_oBtn52Minute
            // 
            this.m_oBtn52Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn52Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn52Minute.Location = new System.Drawing.Point(150, 109);
            this.m_oBtn52Minute.Name = "m_oBtn52Minute";
            this.m_oBtn52Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn52Minute.TabIndex = 77;
            this.m_oBtn52Minute.Tag = "M";
            this.m_oBtn52Minute.Text = "52";
            this.m_oBtn52Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn52Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn53Minute
            // 
            this.m_oBtn53Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn53Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn53Minute.Location = new System.Drawing.Point(174, 109);
            this.m_oBtn53Minute.Name = "m_oBtn53Minute";
            this.m_oBtn53Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn53Minute.TabIndex = 78;
            this.m_oBtn53Minute.Tag = "M";
            this.m_oBtn53Minute.Text = "53";
            this.m_oBtn53Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn53Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn51Minute
            // 
            this.m_oBtn51Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn51Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn51Minute.Location = new System.Drawing.Point(126, 109);
            this.m_oBtn51Minute.Name = "m_oBtn51Minute";
            this.m_oBtn51Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn51Minute.TabIndex = 76;
            this.m_oBtn51Minute.Tag = "M";
            this.m_oBtn51Minute.Text = "51";
            this.m_oBtn51Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn51Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn50Minute
            // 
            this.m_oBtn50Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn50Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn50Minute.Location = new System.Drawing.Point(102, 109);
            this.m_oBtn50Minute.Name = "m_oBtn50Minute";
            this.m_oBtn50Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn50Minute.TabIndex = 75;
            this.m_oBtn50Minute.Tag = "M";
            this.m_oBtn50Minute.Text = "50";
            this.m_oBtn50Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn50Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn42Minute
            // 
            this.m_oBtn42Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn42Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn42Minute.Location = new System.Drawing.Point(150, 87);
            this.m_oBtn42Minute.Name = "m_oBtn42Minute";
            this.m_oBtn42Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn42Minute.TabIndex = 67;
            this.m_oBtn42Minute.Tag = "M";
            this.m_oBtn42Minute.Text = "42";
            this.m_oBtn42Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn42Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn43Minute
            // 
            this.m_oBtn43Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn43Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn43Minute.Location = new System.Drawing.Point(174, 87);
            this.m_oBtn43Minute.Name = "m_oBtn43Minute";
            this.m_oBtn43Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn43Minute.TabIndex = 68;
            this.m_oBtn43Minute.Tag = "M";
            this.m_oBtn43Minute.Text = "43";
            this.m_oBtn43Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn43Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn31Minute
            // 
            this.m_oBtn31Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn31Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn31Minute.Location = new System.Drawing.Point(126, 65);
            this.m_oBtn31Minute.Name = "m_oBtn31Minute";
            this.m_oBtn31Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn31Minute.TabIndex = 56;
            this.m_oBtn31Minute.Tag = "M";
            this.m_oBtn31Minute.Text = "31";
            this.m_oBtn31Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn31Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn40Minute
            // 
            this.m_oBtn40Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn40Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn40Minute.Location = new System.Drawing.Point(102, 87);
            this.m_oBtn40Minute.Name = "m_oBtn40Minute";
            this.m_oBtn40Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn40Minute.TabIndex = 65;
            this.m_oBtn40Minute.Tag = "M";
            this.m_oBtn40Minute.Text = "40";
            this.m_oBtn40Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn40Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn32Minute
            // 
            this.m_oBtn32Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn32Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn32Minute.Location = new System.Drawing.Point(150, 65);
            this.m_oBtn32Minute.Name = "m_oBtn32Minute";
            this.m_oBtn32Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn32Minute.TabIndex = 57;
            this.m_oBtn32Minute.Tag = "M";
            this.m_oBtn32Minute.Text = "32";
            this.m_oBtn32Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn32Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn33Minute
            // 
            this.m_oBtn33Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn33Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn33Minute.Location = new System.Drawing.Point(174, 65);
            this.m_oBtn33Minute.Name = "m_oBtn33Minute";
            this.m_oBtn33Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn33Minute.TabIndex = 58;
            this.m_oBtn33Minute.Tag = "M";
            this.m_oBtn33Minute.Text = "33";
            this.m_oBtn33Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn33Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn21Minute
            // 
            this.m_oBtn21Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn21Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn21Minute.Location = new System.Drawing.Point(126, 43);
            this.m_oBtn21Minute.Name = "m_oBtn21Minute";
            this.m_oBtn21Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn21Minute.TabIndex = 46;
            this.m_oBtn21Minute.Tag = "M";
            this.m_oBtn21Minute.Text = "21";
            this.m_oBtn21Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn21Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn30Minute
            // 
            this.m_oBtn30Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn30Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn30Minute.Location = new System.Drawing.Point(102, 65);
            this.m_oBtn30Minute.Name = "m_oBtn30Minute";
            this.m_oBtn30Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn30Minute.TabIndex = 55;
            this.m_oBtn30Minute.Tag = "M";
            this.m_oBtn30Minute.Text = "30";
            this.m_oBtn30Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn30Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn22Minute
            // 
            this.m_oBtn22Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn22Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn22Minute.Location = new System.Drawing.Point(150, 43);
            this.m_oBtn22Minute.Name = "m_oBtn22Minute";
            this.m_oBtn22Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn22Minute.TabIndex = 47;
            this.m_oBtn22Minute.Tag = "M";
            this.m_oBtn22Minute.Text = "22";
            this.m_oBtn22Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn22Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn23Minute
            // 
            this.m_oBtn23Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn23Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn23Minute.Location = new System.Drawing.Point(174, 43);
            this.m_oBtn23Minute.Name = "m_oBtn23Minute";
            this.m_oBtn23Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn23Minute.TabIndex = 48;
            this.m_oBtn23Minute.Tag = "M";
            this.m_oBtn23Minute.Text = "23";
            this.m_oBtn23Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn23Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn41Minute
            // 
            this.m_oBtn41Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn41Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn41Minute.Location = new System.Drawing.Point(126, 87);
            this.m_oBtn41Minute.Name = "m_oBtn41Minute";
            this.m_oBtn41Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn41Minute.TabIndex = 66;
            this.m_oBtn41Minute.Tag = "M";
            this.m_oBtn41Minute.Text = "41";
            this.m_oBtn41Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn41Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn20Minute
            // 
            this.m_oBtn20Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn20Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn20Minute.Location = new System.Drawing.Point(102, 43);
            this.m_oBtn20Minute.Name = "m_oBtn20Minute";
            this.m_oBtn20Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn20Minute.TabIndex = 45;
            this.m_oBtn20Minute.Tag = "M";
            this.m_oBtn20Minute.Text = "20";
            this.m_oBtn20Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn20Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn12Minute
            // 
            this.m_oBtn12Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn12Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn12Minute.Location = new System.Drawing.Point(150, 21);
            this.m_oBtn12Minute.Name = "m_oBtn12Minute";
            this.m_oBtn12Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn12Minute.TabIndex = 37;
            this.m_oBtn12Minute.Tag = "M";
            this.m_oBtn12Minute.Text = "12";
            this.m_oBtn12Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn12Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn13Minute
            // 
            this.m_oBtn13Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn13Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn13Minute.Location = new System.Drawing.Point(174, 21);
            this.m_oBtn13Minute.Name = "m_oBtn13Minute";
            this.m_oBtn13Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn13Minute.TabIndex = 38;
            this.m_oBtn13Minute.Tag = "M";
            this.m_oBtn13Minute.Text = "13";
            this.m_oBtn13Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn13Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn11Minute
            // 
            this.m_oBtn11Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn11Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn11Minute.Location = new System.Drawing.Point(126, 21);
            this.m_oBtn11Minute.Name = "m_oBtn11Minute";
            this.m_oBtn11Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn11Minute.TabIndex = 36;
            this.m_oBtn11Minute.Tag = "M";
            this.m_oBtn11Minute.Text = "11";
            this.m_oBtn11Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn11Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn10Minute
            // 
            this.m_oBtn10Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn10Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn10Minute.Location = new System.Drawing.Point(102, 21);
            this.m_oBtn10Minute.Name = "m_oBtn10Minute";
            this.m_oBtn10Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn10Minute.TabIndex = 35;
            this.m_oBtn10Minute.Tag = "M";
            this.m_oBtn10Minute.Text = "10";
            this.m_oBtn10Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn10Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn2Minute
            // 
            this.m_oBtn2Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn2Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn2Minute.Location = new System.Drawing.Point(150, 0);
            this.m_oBtn2Minute.Name = "m_oBtn2Minute";
            this.m_oBtn2Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn2Minute.TabIndex = 27;
            this.m_oBtn2Minute.Tag = "M";
            this.m_oBtn2Minute.Text = "2";
            this.m_oBtn2Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn2Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn3Minute
            // 
            this.m_oBtn3Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn3Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn3Minute.Location = new System.Drawing.Point(174, 0);
            this.m_oBtn3Minute.Name = "m_oBtn3Minute";
            this.m_oBtn3Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn3Minute.TabIndex = 28;
            this.m_oBtn3Minute.Tag = "M";
            this.m_oBtn3Minute.Text = "3";
            this.m_oBtn3Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn3Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn1Minute
            // 
            this.m_oBtn1Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn1Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn1Minute.Location = new System.Drawing.Point(126, 0);
            this.m_oBtn1Minute.Name = "m_oBtn1Minute";
            this.m_oBtn1Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn1Minute.TabIndex = 26;
            this.m_oBtn1Minute.Tag = "M";
            this.m_oBtn1Minute.Text = "1";
            this.m_oBtn1Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn1Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn0Minute
            // 
            this.m_oBtn0Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn0Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn0Minute.Location = new System.Drawing.Point(102, 0);
            this.m_oBtn0Minute.Name = "m_oBtn0Minute";
            this.m_oBtn0Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn0Minute.TabIndex = 25;
            this.m_oBtn0Minute.Tag = "M";
            this.m_oBtn0Minute.Text = "0";
            this.m_oBtn0Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn0Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn54Minute
            // 
            this.m_oBtn54Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn54Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn54Minute.Location = new System.Drawing.Point(198, 109);
            this.m_oBtn54Minute.Name = "m_oBtn54Minute";
            this.m_oBtn54Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn54Minute.TabIndex = 79;
            this.m_oBtn54Minute.Tag = "M";
            this.m_oBtn54Minute.Text = "54";
            this.m_oBtn54Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn54Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn44Minute
            // 
            this.m_oBtn44Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn44Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn44Minute.Location = new System.Drawing.Point(198, 87);
            this.m_oBtn44Minute.Name = "m_oBtn44Minute";
            this.m_oBtn44Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn44Minute.TabIndex = 69;
            this.m_oBtn44Minute.Tag = "M";
            this.m_oBtn44Minute.Text = "44";
            this.m_oBtn44Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn44Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn34Minute
            // 
            this.m_oBtn34Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn34Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn34Minute.Location = new System.Drawing.Point(198, 65);
            this.m_oBtn34Minute.Name = "m_oBtn34Minute";
            this.m_oBtn34Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn34Minute.TabIndex = 59;
            this.m_oBtn34Minute.Tag = "M";
            this.m_oBtn34Minute.Text = "34";
            this.m_oBtn34Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn34Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn24Minute
            // 
            this.m_oBtn24Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn24Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn24Minute.Location = new System.Drawing.Point(198, 43);
            this.m_oBtn24Minute.Name = "m_oBtn24Minute";
            this.m_oBtn24Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn24Minute.TabIndex = 49;
            this.m_oBtn24Minute.Tag = "M";
            this.m_oBtn24Minute.Text = "24";
            this.m_oBtn24Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn24Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn14Minute
            // 
            this.m_oBtn14Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn14Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn14Minute.Location = new System.Drawing.Point(198, 21);
            this.m_oBtn14Minute.Name = "m_oBtn14Minute";
            this.m_oBtn14Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn14Minute.TabIndex = 39;
            this.m_oBtn14Minute.Tag = "M";
            this.m_oBtn14Minute.Text = "14";
            this.m_oBtn14Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn14Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn4Minute
            // 
            this.m_oBtn4Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn4Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn4Minute.Location = new System.Drawing.Point(198, 0);
            this.m_oBtn4Minute.Name = "m_oBtn4Minute";
            this.m_oBtn4Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn4Minute.TabIndex = 29;
            this.m_oBtn4Minute.Tag = "M";
            this.m_oBtn4Minute.Text = "4";
            this.m_oBtn4Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn4Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn55Minute
            // 
            this.m_oBtn55Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn55Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn55Minute.Location = new System.Drawing.Point(223, 109);
            this.m_oBtn55Minute.Name = "m_oBtn55Minute";
            this.m_oBtn55Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn55Minute.TabIndex = 80;
            this.m_oBtn55Minute.Tag = "M";
            this.m_oBtn55Minute.Text = "55";
            this.m_oBtn55Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn55Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn45Minute
            // 
            this.m_oBtn45Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn45Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn45Minute.Location = new System.Drawing.Point(223, 87);
            this.m_oBtn45Minute.Name = "m_oBtn45Minute";
            this.m_oBtn45Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn45Minute.TabIndex = 70;
            this.m_oBtn45Minute.Tag = "M";
            this.m_oBtn45Minute.Text = "45";
            this.m_oBtn45Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn45Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn35Minute
            // 
            this.m_oBtn35Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn35Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn35Minute.Location = new System.Drawing.Point(223, 65);
            this.m_oBtn35Minute.Name = "m_oBtn35Minute";
            this.m_oBtn35Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn35Minute.TabIndex = 60;
            this.m_oBtn35Minute.Tag = "M";
            this.m_oBtn35Minute.Text = "35";
            this.m_oBtn35Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn35Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn25Minute
            // 
            this.m_oBtn25Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn25Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn25Minute.Location = new System.Drawing.Point(223, 43);
            this.m_oBtn25Minute.Name = "m_oBtn25Minute";
            this.m_oBtn25Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn25Minute.TabIndex = 50;
            this.m_oBtn25Minute.Tag = "M";
            this.m_oBtn25Minute.Text = "25";
            this.m_oBtn25Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn25Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn15Minute
            // 
            this.m_oBtn15Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn15Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn15Minute.Location = new System.Drawing.Point(223, 21);
            this.m_oBtn15Minute.Name = "m_oBtn15Minute";
            this.m_oBtn15Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn15Minute.TabIndex = 40;
            this.m_oBtn15Minute.Tag = "M";
            this.m_oBtn15Minute.Text = "15";
            this.m_oBtn15Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn15Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn5Minute
            // 
            this.m_oBtn5Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn5Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn5Minute.Location = new System.Drawing.Point(223, 0);
            this.m_oBtn5Minute.Name = "m_oBtn5Minute";
            this.m_oBtn5Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn5Minute.TabIndex = 30;
            this.m_oBtn5Minute.Tag = "M";
            this.m_oBtn5Minute.Text = "5";
            this.m_oBtn5Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn5Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn56Minute
            // 
            this.m_oBtn56Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn56Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn56Minute.Location = new System.Drawing.Point(248, 109);
            this.m_oBtn56Minute.Name = "m_oBtn56Minute";
            this.m_oBtn56Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn56Minute.TabIndex = 81;
            this.m_oBtn56Minute.Tag = "M";
            this.m_oBtn56Minute.Text = "56";
            this.m_oBtn56Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn56Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn46Minute
            // 
            this.m_oBtn46Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn46Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn46Minute.Location = new System.Drawing.Point(248, 87);
            this.m_oBtn46Minute.Name = "m_oBtn46Minute";
            this.m_oBtn46Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn46Minute.TabIndex = 71;
            this.m_oBtn46Minute.Tag = "M";
            this.m_oBtn46Minute.Text = "46";
            this.m_oBtn46Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn46Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn36Minute
            // 
            this.m_oBtn36Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn36Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn36Minute.Location = new System.Drawing.Point(248, 65);
            this.m_oBtn36Minute.Name = "m_oBtn36Minute";
            this.m_oBtn36Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn36Minute.TabIndex = 61;
            this.m_oBtn36Minute.Tag = "M";
            this.m_oBtn36Minute.Text = "36";
            this.m_oBtn36Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn36Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn26Minute
            // 
            this.m_oBtn26Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn26Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn26Minute.Location = new System.Drawing.Point(248, 43);
            this.m_oBtn26Minute.Name = "m_oBtn26Minute";
            this.m_oBtn26Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn26Minute.TabIndex = 51;
            this.m_oBtn26Minute.Tag = "M";
            this.m_oBtn26Minute.Text = "26";
            this.m_oBtn26Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn26Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn16Minute
            // 
            this.m_oBtn16Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn16Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn16Minute.Location = new System.Drawing.Point(248, 21);
            this.m_oBtn16Minute.Name = "m_oBtn16Minute";
            this.m_oBtn16Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn16Minute.TabIndex = 41;
            this.m_oBtn16Minute.Tag = "M";
            this.m_oBtn16Minute.Text = "16";
            this.m_oBtn16Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn16Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn6Minute
            // 
            this.m_oBtn6Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn6Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn6Minute.Location = new System.Drawing.Point(248, 0);
            this.m_oBtn6Minute.Name = "m_oBtn6Minute";
            this.m_oBtn6Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn6Minute.TabIndex = 31;
            this.m_oBtn6Minute.Tag = "M";
            this.m_oBtn6Minute.Text = "6";
            this.m_oBtn6Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn6Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn57Minute
            // 
            this.m_oBtn57Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn57Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn57Minute.Location = new System.Drawing.Point(272, 109);
            this.m_oBtn57Minute.Name = "m_oBtn57Minute";
            this.m_oBtn57Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn57Minute.TabIndex = 82;
            this.m_oBtn57Minute.Tag = "M";
            this.m_oBtn57Minute.Text = "57";
            this.m_oBtn57Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            // 
            // m_oBtn47Minute
            // 
            this.m_oBtn47Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn47Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn47Minute.Location = new System.Drawing.Point(272, 87);
            this.m_oBtn47Minute.Name = "m_oBtn47Minute";
            this.m_oBtn47Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn47Minute.TabIndex = 72;
            this.m_oBtn47Minute.Tag = "M";
            this.m_oBtn47Minute.Text = "47";
            this.m_oBtn47Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn47Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn37Minute
            // 
            this.m_oBtn37Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn37Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn37Minute.Location = new System.Drawing.Point(272, 65);
            this.m_oBtn37Minute.Name = "m_oBtn37Minute";
            this.m_oBtn37Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn37Minute.TabIndex = 62;
            this.m_oBtn37Minute.Tag = "M";
            this.m_oBtn37Minute.Text = "37";
            this.m_oBtn37Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn37Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn27Minute
            // 
            this.m_oBtn27Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn27Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn27Minute.Location = new System.Drawing.Point(272, 43);
            this.m_oBtn27Minute.Name = "m_oBtn27Minute";
            this.m_oBtn27Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn27Minute.TabIndex = 52;
            this.m_oBtn27Minute.Tag = "M";
            this.m_oBtn27Minute.Text = "27";
            this.m_oBtn27Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn27Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn17Minute
            // 
            this.m_oBtn17Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn17Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn17Minute.Location = new System.Drawing.Point(272, 21);
            this.m_oBtn17Minute.Name = "m_oBtn17Minute";
            this.m_oBtn17Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn17Minute.TabIndex = 42;
            this.m_oBtn17Minute.Tag = "M";
            this.m_oBtn17Minute.Text = "17";
            this.m_oBtn17Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn17Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn7Minute
            // 
            this.m_oBtn7Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn7Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn7Minute.Location = new System.Drawing.Point(272, 0);
            this.m_oBtn7Minute.Name = "m_oBtn7Minute";
            this.m_oBtn7Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn7Minute.TabIndex = 32;
            this.m_oBtn7Minute.Tag = "M";
            this.m_oBtn7Minute.Text = "7";
            this.m_oBtn7Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn7Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn58Minute
            // 
            this.m_oBtn58Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn58Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn58Minute.Location = new System.Drawing.Point(296, 109);
            this.m_oBtn58Minute.Name = "m_oBtn58Minute";
            this.m_oBtn58Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn58Minute.TabIndex = 83;
            this.m_oBtn58Minute.Tag = "M";
            this.m_oBtn58Minute.Text = "58";
            this.m_oBtn58Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn58Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn48Minute
            // 
            this.m_oBtn48Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn48Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn48Minute.Location = new System.Drawing.Point(296, 87);
            this.m_oBtn48Minute.Name = "m_oBtn48Minute";
            this.m_oBtn48Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn48Minute.TabIndex = 73;
            this.m_oBtn48Minute.Tag = "M";
            this.m_oBtn48Minute.Text = "48";
            this.m_oBtn48Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn48Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn38Minute
            // 
            this.m_oBtn38Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn38Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn38Minute.Location = new System.Drawing.Point(296, 65);
            this.m_oBtn38Minute.Name = "m_oBtn38Minute";
            this.m_oBtn38Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn38Minute.TabIndex = 63;
            this.m_oBtn38Minute.Tag = "M";
            this.m_oBtn38Minute.Text = "38";
            this.m_oBtn38Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn38Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn28Minute
            // 
            this.m_oBtn28Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn28Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn28Minute.Location = new System.Drawing.Point(296, 43);
            this.m_oBtn28Minute.Name = "m_oBtn28Minute";
            this.m_oBtn28Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn28Minute.TabIndex = 53;
            this.m_oBtn28Minute.Tag = "M";
            this.m_oBtn28Minute.Text = "28";
            this.m_oBtn28Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn28Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn18Minute
            // 
            this.m_oBtn18Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn18Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn18Minute.Location = new System.Drawing.Point(296, 21);
            this.m_oBtn18Minute.Name = "m_oBtn18Minute";
            this.m_oBtn18Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn18Minute.TabIndex = 43;
            this.m_oBtn18Minute.Tag = "M";
            this.m_oBtn18Minute.Text = "18";
            this.m_oBtn18Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn18Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn8Minute
            // 
            this.m_oBtn8Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn8Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn8Minute.Location = new System.Drawing.Point(296, 0);
            this.m_oBtn8Minute.Name = "m_oBtn8Minute";
            this.m_oBtn8Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn8Minute.TabIndex = 33;
            this.m_oBtn8Minute.Tag = "M";
            this.m_oBtn8Minute.Text = "8";
            this.m_oBtn8Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn8Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn59Minute
            // 
            this.m_oBtn59Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn59Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn59Minute.Location = new System.Drawing.Point(320, 109);
            this.m_oBtn59Minute.Name = "m_oBtn59Minute";
            this.m_oBtn59Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn59Minute.TabIndex = 84;
            this.m_oBtn59Minute.Tag = "M";
            this.m_oBtn59Minute.Text = "59";
            this.m_oBtn59Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn59Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn49Minute
            // 
            this.m_oBtn49Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn49Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn49Minute.Location = new System.Drawing.Point(320, 87);
            this.m_oBtn49Minute.Name = "m_oBtn49Minute";
            this.m_oBtn49Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn49Minute.TabIndex = 74;
            this.m_oBtn49Minute.Tag = "M";
            this.m_oBtn49Minute.Text = "49";
            this.m_oBtn49Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn49Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn39Minute
            // 
            this.m_oBtn39Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn39Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn39Minute.Location = new System.Drawing.Point(320, 65);
            this.m_oBtn39Minute.Name = "m_oBtn39Minute";
            this.m_oBtn39Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn39Minute.TabIndex = 64;
            this.m_oBtn39Minute.Tag = "M";
            this.m_oBtn39Minute.Text = "39";
            this.m_oBtn39Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn39Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn29Minute
            // 
            this.m_oBtn29Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn29Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn29Minute.Location = new System.Drawing.Point(320, 43);
            this.m_oBtn29Minute.Name = "m_oBtn29Minute";
            this.m_oBtn29Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn29Minute.TabIndex = 54;
            this.m_oBtn29Minute.Tag = "M";
            this.m_oBtn29Minute.Text = "29";
            this.m_oBtn29Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn29Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn19Minute
            // 
            this.m_oBtn19Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn19Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn19Minute.Location = new System.Drawing.Point(320, 21);
            this.m_oBtn19Minute.Name = "m_oBtn19Minute";
            this.m_oBtn19Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn19Minute.TabIndex = 44;
            this.m_oBtn19Minute.Tag = "M";
            this.m_oBtn19Minute.Text = "19";
            this.m_oBtn19Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn19Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // m_oBtn9Minute
            // 
            this.m_oBtn9Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_oBtn9Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.m_oBtn9Minute.Location = new System.Drawing.Point(320, 0);
            this.m_oBtn9Minute.Name = "m_oBtn9Minute";
            this.m_oBtn9Minute.Size = new System.Drawing.Size(26, 26);
            this.m_oBtn9Minute.TabIndex = 34;
            this.m_oBtn9Minute.Tag = "M";
            this.m_oBtn9Minute.Text = "9";
            this.m_oBtn9Minute.Click += new System.EventHandler(this.LeftMinuteClick);
            this.m_oBtn9Minute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
            // 
            // HourMinuteSelectorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(346, 135);
            this.Controls.Add(this.m_oLblSeparator);
            this.Controls.Add(this.m_oBtn23Hour);
            this.Controls.Add(this.m_oBtn19Hour);
            this.Controls.Add(this.m_oBtn15Hour);
            this.Controls.Add(this.m_oBtn11Hour);
            this.Controls.Add(this.m_oBtn7Hour);
            this.Controls.Add(this.m_oBtn3Hour);
            this.Controls.Add(this.m_oBtn22Hour);
            this.Controls.Add(this.m_oBtn18Hour);
            this.Controls.Add(this.m_oBtn14Hour);
            this.Controls.Add(this.m_oBtn10Hour);
            this.Controls.Add(this.m_oBtn6Hour);
            this.Controls.Add(this.m_oBtn2Hour);
            this.Controls.Add(this.m_oBtn21Hour);
            this.Controls.Add(this.m_oBtn17Hour);
            this.Controls.Add(this.m_oBtn13Hour);
            this.Controls.Add(this.m_oBtn9Hour);
            this.Controls.Add(this.m_oBtn5Hour);
            this.Controls.Add(this.m_oBtn1Hour);
            this.Controls.Add(this.m_oBtn20Hour);
            this.Controls.Add(this.m_oBtn16Hour);
            this.Controls.Add(this.m_oBtn12Hour);
            this.Controls.Add(this.m_oBtn8Hour);
            this.Controls.Add(this.m_oBtn4Hour);
            this.Controls.Add(this.m_oBtn59Minute);
            this.Controls.Add(this.m_oBtn49Minute);
            this.Controls.Add(this.m_oBtn39Minute);
            this.Controls.Add(this.m_oBtn29Minute);
            this.Controls.Add(this.m_oBtn19Minute);
            this.Controls.Add(this.m_oBtn9Minute);
            this.Controls.Add(this.m_oBtn58Minute);
            this.Controls.Add(this.m_oBtn48Minute);
            this.Controls.Add(this.m_oBtn38Minute);
            this.Controls.Add(this.m_oBtn28Minute);
            this.Controls.Add(this.m_oBtn18Minute);
            this.Controls.Add(this.m_oBtn8Minute);
            this.Controls.Add(this.m_oBtn57Minute);
            this.Controls.Add(this.m_oBtn47Minute);
            this.Controls.Add(this.m_oBtn37Minute);
            this.Controls.Add(this.m_oBtn27Minute);
            this.Controls.Add(this.m_oBtn17Minute);
            this.Controls.Add(this.m_oBtn7Minute);
            this.Controls.Add(this.m_oBtn56Minute);
            this.Controls.Add(this.m_oBtn46Minute);
            this.Controls.Add(this.m_oBtn36Minute);
            this.Controls.Add(this.m_oBtn26Minute);
            this.Controls.Add(this.m_oBtn16Minute);
            this.Controls.Add(this.m_oBtn6Minute);
            this.Controls.Add(this.m_oBtn55Minute);
            this.Controls.Add(this.m_oBtn45Minute);
            this.Controls.Add(this.m_oBtn35Minute);
            this.Controls.Add(this.m_oBtn25Minute);
            this.Controls.Add(this.m_oBtn15Minute);
            this.Controls.Add(this.m_oBtn5Minute);
            this.Controls.Add(this.m_oBtn54Minute);
            this.Controls.Add(this.m_oBtn44Minute);
            this.Controls.Add(this.m_oBtn34Minute);
            this.Controls.Add(this.m_oBtn24Minute);
            this.Controls.Add(this.m_oBtn14Minute);
            this.Controls.Add(this.m_oBtn4Minute);
            this.Controls.Add(this.m_oBtn53Minute);
            this.Controls.Add(this.m_oBtn43Minute);
            this.Controls.Add(this.m_oBtn33Minute);
            this.Controls.Add(this.m_oBtn23Minute);
            this.Controls.Add(this.m_oBtn13Minute);
            this.Controls.Add(this.m_oBtn3Minute);
            this.Controls.Add(this.m_oBtn52Minute);
            this.Controls.Add(this.m_oBtn42Minute);
            this.Controls.Add(this.m_oBtn32Minute);
            this.Controls.Add(this.m_oBtn51Minute);
            this.Controls.Add(this.m_oBtn41Minute);
            this.Controls.Add(this.m_oBtn50Minute);
            this.Controls.Add(this.m_oBtn31Minute);
            this.Controls.Add(this.m_oBtn40Minute);
            this.Controls.Add(this.m_oBtn30Minute);
            this.Controls.Add(this.m_oBtn22Minute);
            this.Controls.Add(this.m_oBtn21Minute);
            this.Controls.Add(this.m_oBtn20Minute);
            this.Controls.Add(this.m_oBtn12Minute);
            this.Controls.Add(this.m_oBtn11Minute);
            this.Controls.Add(this.m_oBtn10Minute);
            this.Controls.Add(this.m_oBtn2Minute);
            this.Controls.Add(this.m_oBtn1Minute);
            this.Controls.Add(this.m_oBtn0Minute);
            this.Controls.Add(this.m_oBtn0Hour);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HourMinuteSelectorForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TimeSelectorForm";
            this.Deactivate += new System.EventHandler(this.TimeSelectorForm_Deactivate);
            this.ResumeLayout(false);

		}
		#endregion

		public new void Show()
		{
			if (m_sSelectedTime != TimeSpan.MinValue)
			{
				SetHour(m_sSelectedTime.Hours);
				SetMinute(m_sSelectedTime.Minutes);
			}
			else
			{
				SetHour(DateTime.Now.TimeOfDay.Hours);
				SetMinute(DateTime.Now.TimeOfDay.Minutes);
			}

			base.Show();
		}

		#region Handlers for the events in the HourMinuteSelectorForm
		/// <summary>
		/// Method that handles the left click on a hour button.
		/// </summary>
		/// <param name="sender">The button on which the user clicked</param>
		/// <param name="e">Information about the event</param>
		private void LeftHourClick(object sender, System.EventArgs e)
		{
			Button l_oClickedButton = (Button) sender;

			m_oOldHourButton.BackColor = l_oClickedButton.BackColor;
			m_oOldHourButton = l_oClickedButton;

			l_oClickedButton.BackColor = m_oSelectedColor;
			l_oClickedButton.Select();

			m_sSelectedTime = new TimeSpan( Convert.ToInt32(l_oClickedButton.Text), m_sSelectedTime.Minutes, 0);
		}

		/// <summary>
		/// Method that handle the right click on any button.  To know if the button
		/// clicked was an hour or a minute, we check the value of the Tag property.
		/// </summary>
		/// <param name="sender">The button on which the user clicked</param>
		/// <param name="e">Information about the event</param>
		private void RightClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Button l_oClickedButton = (Button) sender;
				if (l_oClickedButton.Tag.Equals(H))
				{
					LeftHourClick(sender, e);
				}
				else if (l_oClickedButton.Tag.Equals(M))
				{
					LeftMinuteClick(sender, e);
				}
				this.Hide();
			}
		}

		/// <summary>
		/// Method that handles the left click on a minute button.
		/// </summary>
		/// <param name="sender">The button on which the user clicked</param>
		/// <param name="e">Information about the event</param>
		private void LeftMinuteClick(object sender, System.EventArgs e)
		{
			Button l_oClickedButton = (Button) sender;

			m_oOldMinuteButton.BackColor = l_oClickedButton.BackColor;
			m_oOldMinuteButton = l_oClickedButton;

			l_oClickedButton.BackColor = m_oSelectedColor;
			l_oClickedButton.Select();

			m_sSelectedTime = new TimeSpan( m_sSelectedTime.Hours, Convert.ToInt32(l_oClickedButton.Text), 0);
		}

		/// <summary>
		/// Method to handle the click on the separator (which is a label control) that
		/// is between the hours and the minute.  
		/// </summary>
		/// <param name="sender">The label that got clicked</param>
		/// <param name="e">Information about the event</param>
		private void m_oLblSeparator_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		private void TimeSelectorForm_Deactivate(object sender, System.EventArgs e)
		{
			this.Hide();
		}
		#endregion

		private void SetHour(int nHour)
		{
			switch (nHour)
			{
				case 0:
					LeftHourClick(m_oBtn0Hour, null);
					break;
				case 1:
					LeftHourClick(m_oBtn1Hour, null);
					break;
				case 2:
					LeftHourClick(m_oBtn2Hour, null);
					break;
				case 3:
					LeftHourClick(m_oBtn3Hour, null);
					break;
				case 4:
					LeftHourClick(m_oBtn4Hour, null);
					break;
				case 5:
					LeftHourClick(m_oBtn5Hour, null);
					break;
				case 6:
					LeftHourClick(m_oBtn6Hour, null);
					break;
				case 7:
					LeftHourClick(m_oBtn7Hour, null);
					break;
				case 8:
					LeftHourClick(m_oBtn8Hour, null);
					break;
				case 9:
					LeftHourClick(m_oBtn9Hour, null);
					break;
				case 10:
					LeftHourClick(m_oBtn10Hour, null);
					break;
				case 11:
					LeftHourClick(m_oBtn11Hour, null);
					break;
				case 12:
					LeftHourClick(m_oBtn12Hour, null);
					break;
				case 13:
					LeftHourClick(m_oBtn13Hour, null);
					break;
				case 14:
					LeftHourClick(m_oBtn14Hour, null);
					break;
				case 15:
					LeftHourClick(m_oBtn15Hour, null);
					break;
				case 16:
					LeftHourClick(m_oBtn16Hour, null);
					break;
				case 17:
					LeftHourClick(m_oBtn17Hour, null);
					break;
				case 18:
					LeftHourClick(m_oBtn18Hour, null);
					break;
				case 19:
					LeftHourClick(m_oBtn19Hour, null);
					break;
				case 20:
					LeftHourClick(m_oBtn20Hour, null);
					break;
				case 21:
					LeftHourClick(m_oBtn21Hour, null);
					break;
				case 22:
					LeftHourClick(m_oBtn22Hour, null);
					break;
				case 23:
					LeftHourClick(m_oBtn23Hour, null);
					break;
				default:
					break;
			}
		}

		private void SetMinute(int nMinute)
		{
			switch (nMinute)
			{
				case 0:
					LeftMinuteClick(m_oBtn0Minute, null);
					break;
				case 1:
					LeftMinuteClick(m_oBtn1Minute, null);
					break;
				case 2:
					LeftMinuteClick(m_oBtn2Minute, null);
					break;
				case 3:
					LeftMinuteClick(m_oBtn3Minute, null);
					break;
				case 4:
					LeftMinuteClick(m_oBtn4Minute, null);
					break;
				case 5:
					LeftMinuteClick(m_oBtn5Minute, null);
					break;
				case 6:
					LeftMinuteClick(m_oBtn6Minute, null);
					break;
				case 7:
					LeftMinuteClick(m_oBtn7Minute, null);
					break;
				case 8:
					LeftMinuteClick(m_oBtn8Minute, null);
					break;
				case 9:
					LeftMinuteClick(m_oBtn9Minute, null);
					break;
				case 10:
					LeftMinuteClick(m_oBtn10Minute, null);
					break;
				case 11:
					LeftMinuteClick(m_oBtn11Minute, null);
					break;
				case 12:
					LeftMinuteClick(m_oBtn12Minute, null);
					break;
				case 13:
					LeftMinuteClick(m_oBtn13Minute, null);
					break;
				case 14:
					LeftMinuteClick(m_oBtn14Minute, null);
					break;
				case 15:
					LeftMinuteClick(m_oBtn15Minute, null);
					break;
				case 16:
					LeftMinuteClick(m_oBtn16Minute, null);
					break;
				case 17:
					LeftMinuteClick(m_oBtn17Minute, null);
					break;
				case 18:
					LeftMinuteClick(m_oBtn18Minute, null);
					break;
				case 19:
					LeftMinuteClick(m_oBtn19Minute, null);
					break;
				case 20:
					LeftMinuteClick(m_oBtn20Minute, null);
					break;
				case 21:
					LeftMinuteClick(m_oBtn21Minute, null);
					break;
				case 22:
					LeftMinuteClick(m_oBtn22Minute, null);
					break;
				case 23:
					LeftMinuteClick(m_oBtn23Minute, null);
					break;
				case 24:
					LeftMinuteClick(m_oBtn24Minute, null);
					break;
				case 25:
					LeftMinuteClick(m_oBtn25Minute, null);
					break;
				case 26:
					LeftMinuteClick(m_oBtn26Minute, null);
					break;
				case 27:
					LeftMinuteClick(m_oBtn27Minute, null);
					break;
				case 28:
					LeftMinuteClick(m_oBtn28Minute, null);
					break;
				case 29:
					LeftMinuteClick(m_oBtn29Minute, null);
					break;
				case 30:
					LeftMinuteClick(m_oBtn30Minute, null);
					break;
				case 31:
					LeftMinuteClick(m_oBtn31Minute, null);
					break;
				case 32:
					LeftMinuteClick(m_oBtn32Minute, null);
					break;
				case 33:
					LeftMinuteClick(m_oBtn33Minute, null);
					break;
				case 34:
					LeftMinuteClick(m_oBtn34Minute, null);
					break;
				case 35:
					LeftMinuteClick(m_oBtn35Minute, null);
					break;
				case 36:
					LeftMinuteClick(m_oBtn36Minute, null);
					break;
				case 37:
					LeftMinuteClick(m_oBtn37Minute, null);
					break;
				case 38:
					LeftMinuteClick(m_oBtn38Minute, null);
					break;
				case 39:
					LeftMinuteClick(m_oBtn39Minute, null);
					break;
				case 40:
					LeftMinuteClick(m_oBtn40Minute, null);
					break;
				case 41:
					LeftMinuteClick(m_oBtn41Minute, null);
					break;
				case 42:
					LeftMinuteClick(m_oBtn42Minute, null);
					break;
				case 43:
					LeftMinuteClick(m_oBtn43Minute, null);
					break;
				case 44:
					LeftMinuteClick(m_oBtn44Minute, null);
					break;
				case 45:
					LeftMinuteClick(m_oBtn45Minute, null);
					break;
				case 46:
					LeftMinuteClick(m_oBtn46Minute, null);
					break;
				case 47:
					LeftMinuteClick(m_oBtn47Minute, null);
					break;
				case 48:
					LeftMinuteClick(m_oBtn48Minute, null);
					break;
				case 49:
					LeftMinuteClick(m_oBtn49Minute, null);
					break;
				case 50:
					LeftMinuteClick(m_oBtn50Minute, null);
					break;
				case 51:
					LeftMinuteClick(m_oBtn51Minute, null);
					break;
				case 52:
					LeftMinuteClick(m_oBtn52Minute, null);
					break;
				case 53:
					LeftMinuteClick(m_oBtn53Minute, null);
					break;
				case 54:
					LeftMinuteClick(m_oBtn54Minute, null);
					break;
				case 55:
					LeftMinuteClick(m_oBtn55Minute, null);
					break;
				case 56:
					LeftMinuteClick(m_oBtn56Minute, null);
					break;
				case 57:
					LeftMinuteClick(m_oBtn57Minute, null);
					break;
				case 58:
					LeftMinuteClick(m_oBtn58Minute, null);
					break;
				case 59:
					LeftMinuteClick(m_oBtn59Minute, null);
					break;
				default:
					break;
			}
		}		
	}
}
