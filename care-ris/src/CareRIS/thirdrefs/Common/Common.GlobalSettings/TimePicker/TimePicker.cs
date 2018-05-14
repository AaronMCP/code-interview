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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Common.Controls.TimePicker
{
	/// <summary>
	/// The TimePicker class is used to select a time that is formed of an hour
	/// and a minute element.  By clicking on the drop down arrow on the right
	/// side of the component, a panel appears where the user can select his time
	/// by simply clicking on buttons.
	/// 
	/// The user also has a few important properties to customize its TimePicker
	/// object.  He can change the format of the time displayed with the Format
	/// property.  He can change the colors of the buttons with the ButtonColor
	/// and the SelectorColor.  He can also use the Value property to set a default
	/// time to the TimePicker object.  You can refer to the Design-time property
	/// region to look at the property of the TimePicker control.
	/// </summary>

    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
	public class TimePicker : System.Windows.Forms.UserControl
	{
		#region Constants
		private static String ERROR_WRONG_FORMAT_TITLE = "Time format is incorrect";
		private static String ERROR_WRONG_FORMAT_TEXT = "The format you have entered is inapropriate.  Please review your format.";
		private static TimeSpan TWELVE_HOURS = new TimeSpan(12, 0, 0);				
		private static String UNKNOWN = "??";
		private static String HHMM = "HH:MM";
		private static String ZERO = "0";
		private static String HH = "HH";
		private static String H = "H";
		private static String hh = "hh";
		private static String h = "h";
		private static String MM = "MM";
		private static String M = "M";
		private static String AM = "AM";
		private static String PM = "PM";
		private static int MIN_HEIGHT = 22;
		private char SEPARATOR = ':';
		#endregion

		#region Design-time property
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "00:00")]
		[Editor(typeof(TimePickerValueEditor), typeof(UITypeEditor))]
		[Description("The time of the time picker object when it is instanciated")]
		public TimeSpan Value
		{
			get { return m_sSelectedTime; }
			set 
			{ 
				m_sSelectedTime = value; 
				GenerateFormatedText(value);
			}
		}
		private TimeSpan m_sSelectedTime;

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(TimePickerFormat.HoursAndMinutes)]
		[Description("Choose what kind of time that you want to select")]
		public TimePickerFormat TimeSelector
		{
			get { return m_eTimeSelector; }
			set 
			{ 
				m_eTimeSelector = value; 
				if (m_eTimeSelector == TimePickerFormat.Hours)
				{
					m_oLnkHours.Visible = true;
					m_oLblSeparator.Visible = false;
					m_oLnkMinutes.Visible = false;
				}
				else if (m_eTimeSelector == TimePickerFormat.Minutes)
				{
					m_oLnkHours.Visible = false;
					m_oLblSeparator.Visible = false;
					m_oLnkMinutes.Visible = true;
					m_oLnkMinutes.Location = new Point(m_oLblSeparator.Location.X - 23, m_oLnkMinutes.Location.Y);
				}
				else if (m_eTimeSelector == TimePickerFormat.HoursAndMinutes)
				{
					m_oLnkHours.Visible = true;
					m_oLblSeparator.Visible = true;
					m_oLnkMinutes.Visible = true;
					m_oLnkMinutes.Location = new Point(m_oLblSeparator.Location.X + 1, m_oLnkMinutes.Location.Y);
				}

				RightToLeft = RightToLeft;
			}
		}
		private TimePickerFormat m_eTimeSelector;

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(String), "HH:MM")]
		[Description("Choose the format to display the time")]
		public String Format
		{
			get { return m_szTimeFormat; }
			set 
			{ 
				m_szTimeFormat = value;
				GenerateFormatedText(m_sSelectedTime);
				RightToLeft = RightToLeft;
			}
		}
		private String m_szTimeFormat;

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(':')]
		[Description("The character that separates the hour from the minute")]
		public char Separator
		{
			get { return SEPARATOR; }
			set 
			{ 
				m_szTimeFormat = m_szTimeFormat.Replace(SEPARATOR, value);
				m_oLblSeparator.Text = Convert.ToString(value);
				SEPARATOR = value; 				
			}
		}

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Place true to see the checkbox of the control being clicked")]
		public bool Checked
		{
			get { return m_oChkEnable.Checked; }
			set 
			{ 
				m_oChkEnable.Checked = value;
				m_oLnkHours.Enabled = value;
				m_oLnkMinutes.Enabled = value;
				m_oLnkAMPM.Enabled = value;
				m_oBtnShowTimeSelector.Enabled = value;
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "System.Drawing.SystemColors.Control")]
		[AmbientValue(typeof(Color), "System.Drawing.SystemColors.Control")]
		[Description("The color of the button when it is not selected")]
		public Color ButtonColor
		{
			get { return m_oButtonColor; }
			set 
			{ 
				m_oButtonColor = value; 				
				m_oHourForm.ButtonColor = value;
				m_oMinuteForm.ButtonColor = value;
				m_oHourMinuteForm.ButtonColor = value;
			}
		}
		private Color m_oButtonColor;

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Color.White")]
		[Description("The color of the button when it is selected")]
		public Color SelectedColor
		{
			get { return m_oSelectedColor; }
			set 
			{ 
				m_oSelectedColor = value; 				
				m_oHourForm.SelectedColor = value;
				m_oMinuteForm.SelectedColor = value;
				m_oHourMinuteForm.SelectedColor = value;
			}
		}
		private Color m_oSelectedColor;

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Color.RosyBrown")]
		[Description("The color of the separator in the time selector")]
		public Color SeparatorColor
		{
			get { return m_oSeparatorColor; }
			set 
			{ 
				m_oSeparatorColor = value; 
				m_oHourMinuteForm.SeparatorColor = value;
			}
		}
		private Color m_oSeparatorColor;

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Display the drop down arrow on the right side of the control")]
		public bool ShowDropDown
		{
			get { return m_oBtnShowTimeSelector.Visible; }
			set 
			{ 
				m_oBtnShowTimeSelector.Visible = value; 
				RightToLeft = RightToLeft;
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Display a checkbox to the left side of the control")]
		public bool ShowCheckBox
		{
			get { return m_oChkEnable.Visible; }
			set 
			{ 
				m_oChkEnable.Visible = value;
				Checked = Checked;

				if (value == false)
				{
					m_oLnkHours.Enabled = true;
					m_oLnkMinutes.Enabled = true;
					m_oLnkAMPM.Enabled = true;
					m_oBtnShowTimeSelector.Enabled = true;					
				}

				RightToLeft = RightToLeft;
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Tells the control if the time is on the left or right side of the control")]
		public override RightToLeft RightToLeft
		{
			get 
			{ 
				return m_eRightToLeft; 
			}
			set 
			{ 
				if (value == RightToLeft.Yes)
				{
					int l_nSpace = m_oBtnShowTimeSelector.Visible ? m_oBtnShowTimeSelector.Size.Width + 3 : 3;
					
					l_nSpace += m_oLnkAMPM.Visible ? m_oLnkAMPM.Size.Width : 0;
					m_oLnkAMPM.Location = new Point(m_oPnlBackground.Size.Width - l_nSpace, m_oLnkAMPM.Location.Y);

					l_nSpace += m_oLnkMinutes.Visible ? m_oLnkMinutes.Size.Width : 0;
					m_oLnkMinutes.Location = new Point(m_oPnlBackground.Size.Width - l_nSpace, m_oLnkMinutes.Location.Y);

					l_nSpace += m_oLblSeparator.Visible ? m_oLblSeparator.Size.Width -2 : 0;
					m_oLblSeparator.Location = new Point(m_oPnlBackground.Size.Width - l_nSpace, m_oLblSeparator.Location.Y);

					l_nSpace += m_oLnkHours.Visible ? m_oLnkHours.Size.Width : 0;
					m_oLnkHours.Location = new Point(m_oPnlBackground.Size.Width - l_nSpace, m_oLnkHours.Location.Y);

					m_oLnkAMPM.Anchor = AnchorStyles.Right;
					m_oLnkMinutes.Anchor = AnchorStyles.Right;
					m_oLblSeparator.Anchor = AnchorStyles.Right;
					m_oLnkHours.Anchor = AnchorStyles.Right;
				}
				else if (value == RightToLeft.No)
				{
					int l_nSpace = 3;

					l_nSpace = m_oChkEnable.Visible ? l_nSpace + 17 : l_nSpace;

					m_oLnkHours.Location = new Point(l_nSpace, m_oLnkHours.Location.Y);
					l_nSpace += m_oLnkHours.Visible ? m_oLnkHours.Size.Width : 0;

					m_oLblSeparator.Location = new Point(l_nSpace, m_oLblSeparator.Location.Y);
					l_nSpace += m_oLblSeparator.Visible ? m_oLblSeparator.Size.Width - 2 : 0;
					
					m_oLnkMinutes.Location = new Point(l_nSpace, m_oLnkMinutes.Location.Y);
					l_nSpace += m_oLnkMinutes.Visible ? m_oLnkMinutes.Size.Width : 0;
					
					m_oLnkAMPM.Location = new Point(l_nSpace, m_oLnkAMPM.Location.Y);
					l_nSpace += m_oLnkAMPM.Visible ? m_oLnkAMPM.Size.Width : 0;					

					m_oLnkAMPM.Anchor = AnchorStyles.Left;
					m_oLnkMinutes.Anchor = AnchorStyles.Left;
					m_oLblSeparator.Anchor = AnchorStyles.Left;
					m_oLnkHours.Anchor = AnchorStyles.Left;
				}

				m_eRightToLeft = value;
			}
		}
		private RightToLeft m_eRightToLeft;
		#endregion

		/// <summary>
		/// This private data member is used to select a time where the hours and
		/// the minutes are involved.  It is also used by the design-time property
		/// Value.
		/// </summary>
		private HourMinuteSelectorForm m_oHourMinuteForm;

		/// <summary>
		/// This private data member is used to only select the hours.  It can be
		/// used in combination with the value Hour of the TimePickerFormat enum.
		/// You can also use it by clicking on the hour in the time picker control.
		/// </summary>
		private HourSelectorForm m_oHourForm;

		/// <summary>
		/// This private data member is used to select the minutes.  It is used
		/// in the time picker when you click on the minutes.  You can also use it
		/// when the TimeSlector property is set to Minutes.
		/// </summary>
		private MinuteSelectorForm m_oMinuteForm;

		private System.Windows.Forms.Button m_oBtnShowTimeSelector;
		private System.Windows.Forms.LinkLabel m_oLnkHours;
		private System.Windows.Forms.LinkLabel m_oLnkMinutes;
		private System.Windows.Forms.LinkLabel m_oLnkAMPM;
		private System.Windows.Forms.Label m_oLblSeparator;
		private System.Windows.Forms.Panel m_oPnlBackground;
		private System.Windows.Forms.CheckBox m_oChkEnable;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor that initialize its private data members.
		/// </summary>
		public TimePicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			m_oHourMinuteForm = new HourMinuteSelectorForm();
			m_oHourMinuteForm.Size = new Size(346, 135);
			m_oHourMinuteForm.Deactivate += new EventHandler(m_oHourMinuteForm_Deactivate);

			m_oHourForm = new HourSelectorForm();
			m_oHourForm.Size = new Size(292, 47);
			m_oHourForm.Deactivate += new EventHandler(m_oHourForm_Deactivate);

			m_oMinuteForm = new MinuteSelectorForm();
			m_oMinuteForm.Size = new Size(242, 146);
			m_oMinuteForm.Deactivate += new EventHandler(m_oMinuteForm_Deactivate);

			// If you want these properties to work in the designer,
			// you have to set their default values here.  There is
			// something wrong with the DefaultValue attribute
			// Also put them after the creation of the forms since
			// they set some properties in these forms.
			ButtonColor = System.Drawing.SystemColors.Control;
			SelectedColor = Color.White;
			SeparatorColor = Color.RosyBrown;
			TimeSelector = TimePickerFormat.HoursAndMinutes;
			Format = "HH:MM";
			TimeSpan l_sTime = DateTime.Now.TimeOfDay;
			Value = new TimeSpan(l_sTime.Hours, l_sTime.Minutes, 0);
			ShowDropDown = true;
			ShowCheckBox = false;
			RightToLeft = RightToLeft.No;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TimePicker));
			this.m_oBtnShowTimeSelector = new System.Windows.Forms.Button();
			this.m_oLnkHours = new System.Windows.Forms.LinkLabel();
			this.m_oLnkMinutes = new System.Windows.Forms.LinkLabel();
			this.m_oLblSeparator = new System.Windows.Forms.Label();
			this.m_oLnkAMPM = new System.Windows.Forms.LinkLabel();
			this.m_oPnlBackground = new System.Windows.Forms.Panel();
			this.m_oChkEnable = new System.Windows.Forms.CheckBox();
			this.m_oPnlBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_oBtnShowTimeSelector
			// 
			this.m_oBtnShowTimeSelector.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.m_oBtnShowTimeSelector.Image = ((System.Drawing.Bitmap)(resources.GetObject("m_oBtnShowTimeSelector.Image")));
			this.m_oBtnShowTimeSelector.Location = new System.Drawing.Point(114, 1);
			this.m_oBtnShowTimeSelector.Name = "m_oBtnShowTimeSelector";
			this.m_oBtnShowTimeSelector.Size = new System.Drawing.Size(18, 17);
			this.m_oBtnShowTimeSelector.TabIndex = 4;
			this.m_oBtnShowTimeSelector.Click += new System.EventHandler(this.m_oBtnShowTimeSelector_Click);
			// 
			// m_oLnkHours
			// 
			this.m_oLnkHours.ActiveLinkColor = System.Drawing.Color.Black;
			this.m_oLnkHours.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.m_oLnkHours.BackColor = System.Drawing.Color.White;
			this.m_oLnkHours.DisabledLinkColor = System.Drawing.Color.Black;
			this.m_oLnkHours.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.m_oLnkHours.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.m_oLnkHours.LinkColor = System.Drawing.Color.Black;
			this.m_oLnkHours.Location = new System.Drawing.Point(29, 3);
			this.m_oLnkHours.Name = "m_oLnkHours";
			this.m_oLnkHours.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.m_oLnkHours.Size = new System.Drawing.Size(22, 14);
			this.m_oLnkHours.TabIndex = 1;
			this.m_oLnkHours.Text = "HH";
			this.m_oLnkHours.VisitedLinkColor = System.Drawing.Color.Black;
			this.m_oLnkHours.Click += new System.EventHandler(this.m_oLnkHours_Click);
			// 
			// m_oLnkMinutes
			// 
			this.m_oLnkMinutes.ActiveLinkColor = System.Drawing.Color.Black;
			this.m_oLnkMinutes.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.m_oLnkMinutes.BackColor = System.Drawing.Color.White;
			this.m_oLnkMinutes.DisabledLinkColor = System.Drawing.Color.Black;
			this.m_oLnkMinutes.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.m_oLnkMinutes.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.m_oLnkMinutes.LinkColor = System.Drawing.Color.Black;
			this.m_oLnkMinutes.Location = new System.Drawing.Point(52, 3);
			this.m_oLnkMinutes.Name = "m_oLnkMinutes";
			this.m_oLnkMinutes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.m_oLnkMinutes.Size = new System.Drawing.Size(22, 14);
			this.m_oLnkMinutes.TabIndex = 2;
			this.m_oLnkMinutes.Text = "MM";
			this.m_oLnkMinutes.VisitedLinkColor = System.Drawing.Color.Black;
			this.m_oLnkMinutes.Click += new System.EventHandler(this.m_oLnkMinutes_Click);
			// 
			// m_oLblSeparator
			// 
			this.m_oLblSeparator.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.m_oLblSeparator.BackColor = System.Drawing.Color.White;
			this.m_oLblSeparator.Location = new System.Drawing.Point(51, 2);
			this.m_oLblSeparator.Name = "m_oLblSeparator";
			this.m_oLblSeparator.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.m_oLblSeparator.Size = new System.Drawing.Size(4, 14);
			this.m_oLblSeparator.TabIndex = 9;
			this.m_oLblSeparator.Text = ":";
			// 
			// m_oLnkAMPM
			// 
			this.m_oLnkAMPM.ActiveLinkColor = System.Drawing.Color.Black;
			this.m_oLnkAMPM.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.m_oLnkAMPM.BackColor = System.Drawing.Color.White;
			this.m_oLnkAMPM.DisabledLinkColor = System.Drawing.Color.Black;
			this.m_oLnkAMPM.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.m_oLnkAMPM.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.m_oLnkAMPM.LinkColor = System.Drawing.Color.Black;
			this.m_oLnkAMPM.Location = new System.Drawing.Point(89, 3);
			this.m_oLnkAMPM.Name = "m_oLnkAMPM";
			this.m_oLnkAMPM.Size = new System.Drawing.Size(24, 14);
			this.m_oLnkAMPM.TabIndex = 3;
			this.m_oLnkAMPM.Text = "AM";
			this.m_oLnkAMPM.Visible = false;
			this.m_oLnkAMPM.VisitedLinkColor = System.Drawing.Color.Black;
			this.m_oLnkAMPM.Click += new System.EventHandler(this.m_oLnkAMPM_Click);
			// 
			// m_oPnlBackground
			// 
			this.m_oPnlBackground.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			this.m_oPnlBackground.BackColor = System.Drawing.Color.White;
			this.m_oPnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_oPnlBackground.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.m_oChkEnable});
			this.m_oPnlBackground.Location = new System.Drawing.Point(0, -1);
			this.m_oPnlBackground.Name = "m_oPnlBackground";
			this.m_oPnlBackground.Size = new System.Drawing.Size(134, 20);
			this.m_oPnlBackground.TabIndex = 100;
			// 
			// m_oChkEnable
			// 
			this.m_oChkEnable.Location = new System.Drawing.Point(5, 0);
			this.m_oChkEnable.Name = "m_oChkEnable";
			this.m_oChkEnable.Size = new System.Drawing.Size(14, 18);
			this.m_oChkEnable.TabIndex = 0;
			this.m_oChkEnable.Text = "checkBox1";
			this.m_oChkEnable.CheckedChanged += new System.EventHandler(this.m_oChkEnable_CheckedChanged);
			// 
			// TimePicker
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_oBtnShowTimeSelector,
																		  this.m_oLblSeparator,
																		  this.m_oLnkAMPM,
																		  this.m_oLnkMinutes,
																		  this.m_oLnkHours,
																		  this.m_oPnlBackground});
			this.Name = "TimePicker";
			this.Size = new System.Drawing.Size(134, 22);
			this.m_oPnlBackground.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Private method to display the correct time in the time picker control.
		/// The following formats are currently supported :
		///  HH:MM = 00:00 to 23:59
		///   H:MM =  0:00 to 23:59
		///  hh:MM = 00:00 AM to 11:59 PM
		///   h:MM =  0:00 AM to 11:59 PM
		///  HH:M  = 00:0 to 23:59
		///   H:M  =  0:0 to 23:59
		///  hh:M  = 00:0 AM to 11:59 PM
		///   h:M  =  0:0 AM to 11:59 PM
		///
		/// If the format string is incorrect, a message box will appear to indicate
		/// that you must change the format.  Question marks will also appear in the
		/// time picker component to give you a more visual hint of the error.
		/// </summary>
		/// <param name="oSelectedTime">The time to show in the time picker.</param>
		private void GenerateFormatedText(TimeSpan oSelectedTime)
		{
			bool l_bAM = true;
			int l_nHour = oSelectedTime.Hours;
			int l_nMinute = oSelectedTime.Minutes;
			
			m_oLnkHours.Text = String.Empty;
			m_oLnkMinutes.Text = String.Empty;
			String[] l_aTimes = m_szTimeFormat.Split(SEPARATOR);

			if (l_aTimes.Length == 0 || 
				l_aTimes.Length > 2)
			{
				MessageBox.Show(ERROR_WRONG_FORMAT_TEXT, ERROR_WRONG_FORMAT_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				m_oLnkHours.Text = UNKNOWN;
				m_oLnkMinutes.Text = UNKNOWN;
				m_oLnkAMPM.Visible = false;
				return;
			}

			// This section parse the hours
			if (l_aTimes[0].Equals(HH))
			{
				// The format is from 00 to 24
				if (l_nHour < 10)
				{
					m_oLnkHours.Text = ZERO;
				}

				m_oLnkHours.Text += Convert.ToString(l_nHour);
				m_oLnkAMPM.Visible = false;
			}
			else if (l_aTimes[0].Equals(H))
			{
				// The format is 0 to 24
				m_oLnkHours.Text = Convert.ToString(l_nHour);
				m_oLnkAMPM.Visible = false;
			}
			else if (l_aTimes[0].Equals(hh))
			{
				// The format is 00 AM to 12 PM
				if (l_nHour > 12)
				{
					l_nHour -= 12;
					l_bAM = false;
				}
				else if (l_nHour == 12)
				{
					l_bAM = false;
				}

				if (l_nHour < 10)
				{
					m_oLnkHours.Text = ZERO;
				}

				m_oLnkHours.Text += Convert.ToString(l_nHour);
				m_oLnkAMPM.Visible = true;
			}
			else if (l_aTimes[0].Equals(h))
			{
				// The format is 0 AM to 12 PM
				if (l_nHour > 12)
				{
					l_nHour -= 12;
					l_bAM = false;
				}
				else if (l_nHour == 12)
				{
					l_bAM = false;
				}

				m_oLnkHours.Text = Convert.ToString(l_nHour);
				m_oLnkAMPM.Visible = true;
			}


			if (l_aTimes[1].Equals(MM))
			{
                System.Diagnostics.Debug.Print(l_nMinute.ToString());
				// The format is 00 to 59
				if (l_nMinute < 10)
				{
					m_oLnkMinutes.Text = ZERO;
				}

				m_oLnkMinutes.Text += Convert.ToString(l_nMinute);
			}
			else if (l_aTimes[1].Equals(M))
			{
				// The format is 0 to 59
				m_oLnkMinutes.Text = Convert.ToString(l_nMinute);
			}

			if (l_aTimes[0].Equals(hh) ||
				l_aTimes[0].Equals(h))
			{
				m_oLnkAMPM.Text = (l_bAM ? AM : PM);				
			}
		}

		/// <summary>
		/// Private method to place a form inside the screen.  This will prevent
		/// the form of going outside the limits of the screen.
		/// </summary>
		/// <param name="sSizeOfForm">The size of the form</param>
		/// <returns>A point where the form should be place to stay in the screen</returns>
		private Point CalculateLocation(Size sSizeOfForm)
		{
			Point l_sPoint = this.PointToScreen(this.Location);
		
			l_sPoint = new Point(l_sPoint.X - this.Location.X, l_sPoint.Y + m_oPnlBackground.Height - this.Location.Y);

			if (l_sPoint.Y + sSizeOfForm.Height > Screen.PrimaryScreen.WorkingArea.Height)
			{
				// We are near the bottom of the screen.  If the form appears, it won't
				// be entirely shown on the screen.  So place the form above the 
				// Time Picker control.
				l_sPoint = new Point(l_sPoint.X, l_sPoint.Y - m_oPnlBackground.Height - sSizeOfForm.Height);
			}

			if (l_sPoint.X < 0)
			{
				// We are to much on the left side of the screen.  This causes the 
				// form to be outside the screen.  We have to push back the form 
				// on the edge of the screen.
				l_sPoint = new Point(0, l_sPoint.Y);
			}
			else if (l_sPoint.X + sSizeOfForm.Width > Screen.PrimaryScreen.WorkingArea.Width)
			{
				// This is the opposite of the previous statement.  We are now 
				// on the right side of the screen.  We have to stick the window to
				// the edge of the right border of the screen.
				l_sPoint = new Point(Screen.PrimaryScreen.WorkingArea.Width - sSizeOfForm.Width, l_sPoint.Y);
			}

			return l_sPoint;
		}

		#region Handlers for the events in the TimePicker
		private void m_oBtnShowTimeSelector_Click(object sender, System.EventArgs e)
		{
			if (m_eTimeSelector == TimePickerFormat.Hours)
			{
				m_oHourForm.SelectedTime = m_sSelectedTime;
				m_oHourForm.Location = CalculateLocation(m_oHourForm.Size);
				m_oHourForm.Show();
				m_oHourForm.BringToFront();
			}
			else if (m_eTimeSelector == TimePickerFormat.Minutes)
			{
				m_oMinuteForm.SelectedTime = m_sSelectedTime;
				m_oMinuteForm.Location = CalculateLocation(m_oMinuteForm.Size);
				m_oMinuteForm.Show();
				m_oMinuteForm.BringToFront();
			}
			else if (m_eTimeSelector == TimePickerFormat.HoursAndMinutes)
			{
				m_oHourMinuteForm.SelectedTime = m_sSelectedTime;
				m_oHourMinuteForm.Location = CalculateLocation(m_oHourMinuteForm.Size);
				m_oHourMinuteForm.Show();
				m_oHourMinuteForm.BringToFront();
			}
		}

		private void m_oHourForm_Deactivate(object sender, System.EventArgs e)
		{
			m_sSelectedTime = m_oHourForm.SelectedTime;
			GenerateFormatedText(m_sSelectedTime);
			FireEvents();
		}

		private void m_oMinuteForm_Deactivate(object sender, System.EventArgs e)
		{
			m_sSelectedTime = m_oMinuteForm.SelectedTime;
			GenerateFormatedText(m_sSelectedTime);
			FireEvents();
		}

		private void m_oHourMinuteForm_Deactivate(object sender, System.EventArgs e)
		{
			m_sSelectedTime = m_oHourMinuteForm.SelectedTime;
			GenerateFormatedText(m_sSelectedTime);
			FireEvents();
		}

		private void m_oChkEnable_CheckedChanged(object sender, System.EventArgs e)
		{
			Checked = m_oChkEnable.Checked;
		}

		private void m_oLnkHours_Click(object sender, System.EventArgs e)
		{
			m_oHourForm.SelectedTime = m_sSelectedTime;
			m_oHourForm.Location = CalculateLocation(m_oHourForm.Size);
			m_oHourForm.ResetMinute = false;
			m_oHourForm.Show();
			m_oHourForm.BringToFront();		
		}

		private void m_oLnkMinutes_Click(object sender, System.EventArgs e)
		{
			m_oMinuteForm.SelectedTime = m_sSelectedTime;
			m_oMinuteForm.Location = CalculateLocation(m_oMinuteForm.Size);
			m_oMinuteForm.ResetMinute = false;
			m_oMinuteForm.Show();
			m_oMinuteForm.BringToFront();				
		}

		private void m_oLnkAMPM_Click(object sender, System.EventArgs e)
		{
			if (m_oLnkAMPM.Text.Equals(AM))
			{
				m_oLnkAMPM.Text = PM;
				m_sSelectedTime = m_sSelectedTime.Add(TWELVE_HOURS);
				GenerateFormatedText(m_sSelectedTime);
			}
			else
			{
				m_oLnkAMPM.Text = AM;
				m_sSelectedTime = m_sSelectedTime.Subtract(TWELVE_HOURS);
				GenerateFormatedText(m_sSelectedTime);
			}
		}
		#endregion

		#region Events
		[Browsable(false)]
		public new event EventHandler TextChanged;

		[Browsable(true)]
		public event EventHandler DropDownClick;

		[Browsable(true)]
		public event EventHandler ValueChanged;

		private void FireEvents()
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, new EventArgs());
			}
		}
		#endregion						  

		/// <summary>
		/// Override of the function ToString inherited from the Object class.
		/// It returns a string in the default format HH:MM.  Use the other
		/// ToString method if you want to apply another format.
		/// </summary>
		/// <returns>Returns a formatted string with the default format HH:MM</returns>
		public override String ToString()
		{
			return ToString(HHMM);
		}

		/// <summary>
		/// If you want a string representation of your choice, you can enter your
		/// format.  The following format are currently supported :
		///  HH:MM = 00:00 to 23:59
		///   H:MM =  0:00 to 23:59
		///  hh:MM = 00:00 AM to 11:59 PM
		///   h:MM =  0:00 AM to 11:59 PM
		///  HH:M  = 00:0 to 23:59
		///   H:M  =  0:0 to 23:59
		///  hh:M  = 00:0 AM to 11:59 PM
		///   h:M  =  0:0 AM to 11:59 PM
		/// </summary>
		/// <param name="szTimeFormat">The desired format</param>
		/// <returns>Returns a formatted string based on the value of your TimePicker
		///          If incorrect format, an empty string is returned</returns>
		public String ToString(String szTimeFormat)
		{
			bool l_bAM = true;
			int l_nHour = m_sSelectedTime.Hours;
			int l_nMinute = m_sSelectedTime.Minutes;
			String l_szReturnedString = String.Empty;
			
			String[] l_aTimes = szTimeFormat.Split(SEPARATOR);

			if (l_aTimes.Length == 0 || 
				l_aTimes.Length > 2)
			{
				return l_szReturnedString;
			}

			// This section parse the hours
			if (l_aTimes[0].Equals(HH))
			{
				// The format is from 00 to 24
				if (l_nHour < 10)
				{
					l_szReturnedString = ZERO;
				}

				l_szReturnedString += Convert.ToString(l_nHour);
			}
			else if (l_aTimes[0].Equals(H))
			{
				// The format is 0 to 24
				l_szReturnedString = Convert.ToString(l_nHour);
			}
			else if (l_aTimes[0].Equals(hh))
			{
				// The format is 00 AM to 12 PM
				if (l_nHour > 12)
				{
					l_nHour -= 12;
					l_bAM = false;
				}
				else if (l_nHour == 12)
				{
					l_bAM = false;
				}

				if (l_nHour < 10)
				{
					l_szReturnedString = ZERO;
				}

				l_szReturnedString += Convert.ToString(l_nHour);
			}
			else if (l_aTimes[0].Equals(h))
			{
				// The format is 0 AM to 12 PM
				if (l_nHour > 12)
				{
					l_nHour -= 12;
					l_bAM = false;
				}
				else if (l_nHour == 12)
				{
					l_bAM = false;
				}

				l_szReturnedString = Convert.ToString(l_nHour);
			}

			l_szReturnedString += SEPARATOR;

			if (l_aTimes[1].Equals(MM))
			{
				// The format is 00 to 59
				if (l_nMinute < 10)
				{
					l_szReturnedString += ZERO;
				}

				l_szReturnedString += Convert.ToString(l_nMinute);
			}
			else if (l_aTimes[1].Equals(M))
			{
				// The format is 0 to 59
				l_szReturnedString += Convert.ToString(l_nMinute);
			}

			if (l_aTimes[0].Equals(hh) ||
				l_aTimes[0].Equals(h))
			{
				l_szReturnedString += (l_bAM ? " " + AM : " " + PM);				
			}

			return l_szReturnedString;
		}

		/// <summary>
		/// SetBoundsCore is used to set the height of the TimePicker control.
		/// Since we don't want the control to be resized in height, we must
		/// override the SetBoundsCore method and modify the height of the control.
		/// The impact of this override is in design-mode.  The developper won't be
		/// able to change the height of the control.
		/// </summary>
		/// <param name="nX">The new Left property value of the control</param>
		/// <param name="nY">The new Right property value of the control</param>
		/// <param name="nWidth">The new Width property value of the control</param>
		/// <param name="nHeight">The new Height property value of the control</param>
		/// <param name="eSpecifiedBound">A bitwise combination of the BoundsSpecified values</param>
		protected override void SetBoundsCore(int nX, int nY, int nWidth, int nHeight, BoundsSpecified eSpecifiedBound)
		{
			BoundsSpecified t = eSpecifiedBound & BoundsSpecified.Size;

			if (eSpecifiedBound == BoundsSpecified.Size || 
				eSpecifiedBound == BoundsSpecified.All ||
				eSpecifiedBound == BoundsSpecified.None ||
				eSpecifiedBound == BoundsSpecified.Height)
			{
				nHeight = MIN_HEIGHT;
			}

			base.SetBoundsCore(nX, nY, nWidth, nHeight, eSpecifiedBound);
		}
	}

	/// <summary>
	/// The TimePickerFormat enum is used with the TimeSelector property of the
	/// TimePicker.  TimePickerFormat tells the TimePicker what kind of selection
	/// to show.
	/// </summary>
	public enum TimePickerFormat
	{
		Hours,
		Minutes,
		HoursAndMinutes
	}
}
