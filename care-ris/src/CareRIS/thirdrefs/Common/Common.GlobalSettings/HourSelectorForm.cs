// C# TimePicker Class v1.0
// by Louis-Philippe Carignan - 10 July 2003
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyTimePicker
{
	/// <summary>
	/// The HourSelectorForm is used to choose an hour element of a time.  It displays
	/// a form where the user can simply click on a button that represents the hour 
	/// that he wants.  To exit the form, the user can do a left click outside the form 
	/// or do a right click on a button.  Remember that a right click will also 
	/// select the button where your mouse is currently placed.
	/// </summary>
	internal class HourSelectorForm : System.Windows.Forms.Form
	{
		#region Properties
		public TimeSpan SelectedTime
		{
			get { return m_sSelectedTime; }
			set 
			{ 
				m_sSelectedTime = value; 
				SetHour(value.Hours);
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

		public bool ResetMinute
		{
			get { return m_bResetMinute; }
			set { m_bResetMinute = value; }
		}
		private bool m_bResetMinute;
		#endregion

		private Button m_oOldHourButton;
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
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A constructor that instanciate its data members.
		/// </summary>
		public HourSelectorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_oOldHourButton = new Button();

			m_sSelectedTime = TimeSpan.MinValue;
			m_oSelectedColor = Color.White;
			m_bResetMinute = true;
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
			this.m_oBtn4Hour = new System.Windows.Forms.Button();
			this.m_oBtn2Hour = new System.Windows.Forms.Button();
			this.m_oBtn1Hour = new System.Windows.Forms.Button();
			this.m_oBtn3Hour = new System.Windows.Forms.Button();
			this.m_oBtn7Hour = new System.Windows.Forms.Button();
			this.m_oBtn8Hour = new System.Windows.Forms.Button();
			this.m_oBtn6Hour = new System.Windows.Forms.Button();
			this.m_oBtn5Hour = new System.Windows.Forms.Button();
			this.m_oBtn11Hour = new System.Windows.Forms.Button();
			this.m_oBtn12Hour = new System.Windows.Forms.Button();
			this.m_oBtn10Hour = new System.Windows.Forms.Button();
			this.m_oBtn9Hour = new System.Windows.Forms.Button();
			this.m_oBtn15Hour = new System.Windows.Forms.Button();
			this.m_oBtn16Hour = new System.Windows.Forms.Button();
			this.m_oBtn14Hour = new System.Windows.Forms.Button();
			this.m_oBtn13Hour = new System.Windows.Forms.Button();
			this.m_oBtn19Hour = new System.Windows.Forms.Button();
			this.m_oBtn20Hour = new System.Windows.Forms.Button();
			this.m_oBtn18Hour = new System.Windows.Forms.Button();
			this.m_oBtn17Hour = new System.Windows.Forms.Button();
			this.m_oBtn23Hour = new System.Windows.Forms.Button();
			this.m_oBtn0Hour = new System.Windows.Forms.Button();
			this.m_oBtn22Hour = new System.Windows.Forms.Button();
			this.m_oBtn21Hour = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_oBtn4Hour
			// 
			this.m_oBtn4Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn4Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn4Hour.Location = new System.Drawing.Point(97, 0);
			this.m_oBtn4Hour.Name = "m_oBtn4Hour";
			this.m_oBtn4Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn4Hour.TabIndex = 4;
			this.m_oBtn4Hour.Text = "4";
			this.m_oBtn4Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn4Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn2Hour
			// 
			this.m_oBtn2Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn2Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn2Hour.Location = new System.Drawing.Point(48, 0);
			this.m_oBtn2Hour.Name = "m_oBtn2Hour";
			this.m_oBtn2Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn2Hour.TabIndex = 2;
			this.m_oBtn2Hour.Text = "2";
			this.m_oBtn2Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn2Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn1Hour
			// 
			this.m_oBtn1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn1Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn1Hour.Location = new System.Drawing.Point(24, 0);
			this.m_oBtn1Hour.Name = "m_oBtn1Hour";
			this.m_oBtn1Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn1Hour.TabIndex = 0;
			this.m_oBtn1Hour.Tag = "1";
			this.m_oBtn1Hour.Text = "1";
			this.m_oBtn1Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn1Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn3Hour
			// 
			this.m_oBtn3Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn3Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn3Hour.Location = new System.Drawing.Point(72, 0);
			this.m_oBtn3Hour.Name = "m_oBtn3Hour";
			this.m_oBtn3Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn3Hour.TabIndex = 3;
			this.m_oBtn3Hour.Text = "3";
			this.m_oBtn3Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn3Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn7Hour
			// 
			this.m_oBtn7Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn7Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn7Hour.Location = new System.Drawing.Point(169, 0);
			this.m_oBtn7Hour.Name = "m_oBtn7Hour";
			this.m_oBtn7Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn7Hour.TabIndex = 7;
			this.m_oBtn7Hour.Text = "7";
			this.m_oBtn7Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn7Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn8Hour
			// 
			this.m_oBtn8Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn8Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn8Hour.Location = new System.Drawing.Point(194, 0);
			this.m_oBtn8Hour.Name = "m_oBtn8Hour";
			this.m_oBtn8Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn8Hour.TabIndex = 8;
			this.m_oBtn8Hour.Text = "8";
			this.m_oBtn8Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn8Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn6Hour
			// 
			this.m_oBtn6Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn6Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn6Hour.Location = new System.Drawing.Point(145, 0);
			this.m_oBtn6Hour.Name = "m_oBtn6Hour";
			this.m_oBtn6Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn6Hour.TabIndex = 6;
			this.m_oBtn6Hour.Text = "6";
			this.m_oBtn6Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn6Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn5Hour
			// 
			this.m_oBtn5Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn5Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn5Hour.Location = new System.Drawing.Point(121, 0);
			this.m_oBtn5Hour.Name = "m_oBtn5Hour";
			this.m_oBtn5Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn5Hour.TabIndex = 5;
			this.m_oBtn5Hour.Text = "5";
			this.m_oBtn5Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn5Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn11Hour
			// 
			this.m_oBtn11Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn11Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn11Hour.Location = new System.Drawing.Point(266, 0);
			this.m_oBtn11Hour.Name = "m_oBtn11Hour";
			this.m_oBtn11Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn11Hour.TabIndex = 11;
			this.m_oBtn11Hour.Text = "11";
			this.m_oBtn11Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn11Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn12Hour
			// 
			this.m_oBtn12Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn12Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn12Hour.Location = new System.Drawing.Point(0, 21);
			this.m_oBtn12Hour.Name = "m_oBtn12Hour";
			this.m_oBtn12Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn12Hour.TabIndex = 12;
			this.m_oBtn12Hour.Text = "12";
			this.m_oBtn12Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn12Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn10Hour
			// 
			this.m_oBtn10Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn10Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn10Hour.Location = new System.Drawing.Point(242, 0);
			this.m_oBtn10Hour.Name = "m_oBtn10Hour";
			this.m_oBtn10Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn10Hour.TabIndex = 10;
			this.m_oBtn10Hour.Text = "10";
			this.m_oBtn10Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn10Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn9Hour
			// 
			this.m_oBtn9Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn9Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn9Hour.Location = new System.Drawing.Point(218, 0);
			this.m_oBtn9Hour.Name = "m_oBtn9Hour";
			this.m_oBtn9Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn9Hour.TabIndex = 9;
			this.m_oBtn9Hour.Text = "9";
			this.m_oBtn9Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn9Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn15Hour
			// 
			this.m_oBtn15Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn15Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn15Hour.Location = new System.Drawing.Point(72, 21);
			this.m_oBtn15Hour.Name = "m_oBtn15Hour";
			this.m_oBtn15Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn15Hour.TabIndex = 15;
			this.m_oBtn15Hour.Text = "15";
			this.m_oBtn15Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn15Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn16Hour
			// 
			this.m_oBtn16Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn16Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn16Hour.Location = new System.Drawing.Point(97, 21);
			this.m_oBtn16Hour.Name = "m_oBtn16Hour";
			this.m_oBtn16Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn16Hour.TabIndex = 16;
			this.m_oBtn16Hour.Text = "16";
			this.m_oBtn16Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn16Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn14Hour
			// 
			this.m_oBtn14Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn14Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn14Hour.Location = new System.Drawing.Point(48, 21);
			this.m_oBtn14Hour.Name = "m_oBtn14Hour";
			this.m_oBtn14Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn14Hour.TabIndex = 14;
			this.m_oBtn14Hour.Text = "14";
			this.m_oBtn14Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn14Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn13Hour
			// 
			this.m_oBtn13Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn13Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn13Hour.Location = new System.Drawing.Point(24, 21);
			this.m_oBtn13Hour.Name = "m_oBtn13Hour";
			this.m_oBtn13Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn13Hour.TabIndex = 13;
			this.m_oBtn13Hour.Text = "13";
			this.m_oBtn13Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn13Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn19Hour
			// 
			this.m_oBtn19Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn19Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn19Hour.Location = new System.Drawing.Point(169, 21);
			this.m_oBtn19Hour.Name = "m_oBtn19Hour";
			this.m_oBtn19Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn19Hour.TabIndex = 19;
			this.m_oBtn19Hour.Text = "19";
			this.m_oBtn19Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn19Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn20Hour
			// 
			this.m_oBtn20Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn20Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn20Hour.Location = new System.Drawing.Point(194, 21);
			this.m_oBtn20Hour.Name = "m_oBtn20Hour";
			this.m_oBtn20Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn20Hour.TabIndex = 20;
			this.m_oBtn20Hour.Text = "20";
			this.m_oBtn20Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn20Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn18Hour
			// 
			this.m_oBtn18Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn18Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn18Hour.Location = new System.Drawing.Point(145, 21);
			this.m_oBtn18Hour.Name = "m_oBtn18Hour";
			this.m_oBtn18Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn18Hour.TabIndex = 18;
			this.m_oBtn18Hour.Text = "18";
			this.m_oBtn18Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn18Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn17Hour
			// 
			this.m_oBtn17Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn17Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn17Hour.Location = new System.Drawing.Point(121, 21);
			this.m_oBtn17Hour.Name = "m_oBtn17Hour";
			this.m_oBtn17Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn17Hour.TabIndex = 17;
			this.m_oBtn17Hour.Text = "17";
			this.m_oBtn17Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn17Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn23Hour
			// 
			this.m_oBtn23Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn23Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn23Hour.Location = new System.Drawing.Point(266, 21);
			this.m_oBtn23Hour.Name = "m_oBtn23Hour";
			this.m_oBtn23Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn23Hour.TabIndex = 23;
			this.m_oBtn23Hour.Text = "23";
			this.m_oBtn23Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn23Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn0Hour
			// 
			this.m_oBtn0Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn0Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn0Hour.Name = "m_oBtn0Hour";
			this.m_oBtn0Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn0Hour.TabIndex = 24;
			this.m_oBtn0Hour.Text = "0";
			this.m_oBtn0Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn0Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn22Hour
			// 
			this.m_oBtn22Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn22Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn22Hour.Location = new System.Drawing.Point(242, 21);
			this.m_oBtn22Hour.Name = "m_oBtn22Hour";
			this.m_oBtn22Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn22Hour.TabIndex = 22;
			this.m_oBtn22Hour.Text = "22";
			this.m_oBtn22Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn22Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// m_oBtn21Hour
			// 
			this.m_oBtn21Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_oBtn21Hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_oBtn21Hour.Location = new System.Drawing.Point(218, 21);
			this.m_oBtn21Hour.Name = "m_oBtn21Hour";
			this.m_oBtn21Hour.Size = new System.Drawing.Size(26, 26);
			this.m_oBtn21Hour.TabIndex = 21;
			this.m_oBtn21Hour.Text = "21";
			this.m_oBtn21Hour.Click += new System.EventHandler(this.LeftClick);
			this.m_oBtn21Hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightClick);
			// 
			// HourSelectorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 47);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_oBtn23Hour,
																		  this.m_oBtn22Hour,
																		  this.m_oBtn21Hour,
																		  this.m_oBtn20Hour,
																		  this.m_oBtn19Hour,
																		  this.m_oBtn18Hour,
																		  this.m_oBtn17Hour,
																		  this.m_oBtn16Hour,
																		  this.m_oBtn15Hour,
																		  this.m_oBtn14Hour,
																		  this.m_oBtn13Hour,
																		  this.m_oBtn12Hour,
																		  this.m_oBtn11Hour,
																		  this.m_oBtn10Hour,
																		  this.m_oBtn9Hour,
																		  this.m_oBtn8Hour,
																		  this.m_oBtn7Hour,
																		  this.m_oBtn6Hour,
																		  this.m_oBtn5Hour,
																		  this.m_oBtn4Hour,
																		  this.m_oBtn3Hour,
																		  this.m_oBtn2Hour,
																		  this.m_oBtn1Hour,
																		  this.m_oBtn0Hour});
			this.DockPadding.All = 10;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "HourSelectorForm";
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
			}
			else
			{
				SetHour(DateTime.Now.TimeOfDay.Hours);
			}

			base.Show();
		}

		#region Handlers for the events in the HourSelectorForm
		/// <summary>
		/// Method that handles the left click on a hour button.
		/// </summary>
		/// <param name="sender">The button on which the user clicked</param>
		/// <param name="e">Information about the event</param>
		private void LeftClick(object sender, System.EventArgs e)
		{
			Button l_oClickedButton = (Button) sender;

			m_oOldHourButton.BackColor = l_oClickedButton.BackColor;
			m_oOldHourButton = l_oClickedButton;

			l_oClickedButton.BackColor = m_oSelectedColor;
			l_oClickedButton.Select();

			m_sSelectedTime = new TimeSpan( Convert.ToInt32(l_oClickedButton.Text), m_sSelectedTime.Minutes, 0);
		}

		/// <summary>
		/// Method that handle the right click on a hour button.
		/// </summary>
		/// <param name="sender">The button on which the user clicked</param>
		/// <param name="e">Information about the event</param>
		private void RightClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				LeftClick(sender, e);
				this.Hide();
			}
		}

		private void TimeSelectorForm_Deactivate(object sender, System.EventArgs e)
		{
			if (m_bResetMinute == true)
			{
				m_sSelectedTime = new TimeSpan(m_sSelectedTime.Hours, 0, 0);
			}
			else
			{
				m_sSelectedTime = new TimeSpan(m_sSelectedTime.Hours, m_sSelectedTime.Minutes, 0);
				m_bResetMinute = true;
			}

			this.Hide();
		}
		#endregion

		private void SetHour(int nHour)
		{
			switch (nHour)
			{
				case 0:
					LeftClick(m_oBtn0Hour, null);
					break;
				case 1:
					LeftClick(m_oBtn1Hour, null);
					break;
				case 2:
					LeftClick(m_oBtn2Hour, null);
					break;
				case 3:
					LeftClick(m_oBtn3Hour, null);
					break;
				case 4:
					LeftClick(m_oBtn4Hour, null);
					break;
				case 5:
					LeftClick(m_oBtn5Hour, null);
					break;
				case 6:
					LeftClick(m_oBtn6Hour, null);
					break;
				case 7:
					LeftClick(m_oBtn7Hour, null);
					break;
				case 8:
					LeftClick(m_oBtn8Hour, null);
					break;
				case 9:
					LeftClick(m_oBtn9Hour, null);
					break;
				case 10:
					LeftClick(m_oBtn10Hour, null);
					break;
				case 11:
					LeftClick(m_oBtn11Hour, null);
					break;
				case 12:
					LeftClick(m_oBtn12Hour, null);
					break;
				case 13:
					LeftClick(m_oBtn13Hour, null);
					break;
				case 14:
					LeftClick(m_oBtn14Hour, null);
					break;
				case 15:
					LeftClick(m_oBtn15Hour, null);
					break;
				case 16:
					LeftClick(m_oBtn16Hour, null);
					break;
				case 17:
					LeftClick(m_oBtn17Hour, null);
					break;
				case 18:
					LeftClick(m_oBtn18Hour, null);
					break;
				case 19:
					LeftClick(m_oBtn19Hour, null);
					break;
				case 20:
					LeftClick(m_oBtn20Hour, null);
					break;
				case 21:
					LeftClick(m_oBtn21Hour, null);
					break;
				case 22:
					LeftClick(m_oBtn22Hour, null);
					break;
				case 23:
					LeftClick(m_oBtn23Hour, null);
					break;
				default:
					break;
			}
		}
	}
}
