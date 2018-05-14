using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;
using System.ComponentModel;

namespace Hys.CommonControls
{
    public class CSTabStrip : RadTabStrip
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadTabStrip"; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            TabItem ti = this.SelectedTab as TabItem;
            if (ti != null)
            {
                //ti.BackColor = ti.ContentPanel.BackColor = this.BackColor;
                if (ti.ContentPanel.BackColor != this.BackColor)
                    ti.ContentPanel.BackColor = this.BackColor;
            }

            base.OnPaint(e);

            //System.Diagnostics.Debug.WriteLine("CSTabStrip::OnPaint, " + System.DateTime.Now.ToString("mm:ss fff"));
        }

        public int SelectedIndex
        {
            get
            {
                if (base.SelectedTab == null)
                    return -1;

                int i = 0;
                RadItemCollection.RadItemEnumerator itor = base.Items.GetEnumerator();
                while (itor.MoveNext())
                {
                    if (itor.Current == base.SelectedTab)
                    {
                        return i;
                    }
                    ++i;
                }

                return -1;
            }
            set
            {
                if (value >= 0 && value < base.Items.Count)
                {
                    RadItem ri = base.Items[value];
                    base.SelectedTab = ri;
                }
            }
        }

        public System.Windows.Forms.TabAlignment Alignment
        {
            get
            {
                switch (base.TabsPosition)
                {
                    case TabPositions.Bottom:
                        return TabAlignment.Bottom;
                    case TabPositions.Left:
                        return TabAlignment.Left;
                    case TabPositions.Right:
                        return TabAlignment.Right;
                    case TabPositions.Top:
                        return TabAlignment.Top;
                    default:
                        break;
                }

                return TabAlignment.Top;
            }
            set
            {
                switch (value)
                {
                    case TabAlignment.Bottom:
                        base.TabsPosition = TabPositions.Bottom;
                        break;
                    case TabAlignment.Left:
                        base.TabsPosition = TabPositions.Left;
                        break;
                    case TabAlignment.Right:
                        base.TabsPosition = TabPositions.Right;
                        break;
                    case TabAlignment.Top:
                        base.TabsPosition = TabPositions.Top;
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Multiline
        {
            get { return false; }
            set { }
        }

        public event TabControlCancelEventHandler Selecting;

        public event System.EventHandler SelectedIndexChanged;

        protected override void OnTabSelecting(TabCancelEventArgs args)
        {
            if (Selecting != null)
            {
                TabControlCancelEventArgs e = new TabControlCancelEventArgs(
                    null, 0, args.Cancel, TabControlAction.Selecting);

                Selecting(this, e);

                args.Cancel = e.Cancel;
            }

            base.OnTabSelecting(args);
        }

        protected override void OnTabSelected(TabEventArgs args)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, new EventArgs());
            }

            base.OnTabSelected(args);
        }

        //protected override void OnBackColorChanged(EventArgs e)
        //{
        //    foreach(TabItem ti in this.Items)
        //    {
        //        if (ti == null)
        //            continue;

        //        ti.BackColor = ti.ContentPanel.BackColor = this.BackColor;
        //    }

        //    base.OnBackColorChanged(e);
        //}
    }

    public class CSTabItem : TabItem
    {
        public CSTabItem()
            : base()
        {
            base.ContentPanel.Resize += new EventHandler(ContentPanel_Resize);
        }

        public CSTabItem(string text)
            : base(text)
        {
            base.ContentPanel.Resize += new EventHandler(ContentPanel_Resize);
        }

        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(TabItem);
            }
        }

        public int Height
        {
            get { return base.ContentPanel.Height; }
            set { base.ContentPanel.Height = value; }
        }

        public int Width
        {
            get { return base.ContentPanel.Width; }
            set { base.ContentPanel.Width = value; }
        }

        public bool UseVisualStyleBackColor
        {
            get { return false; }
            set { }
        }

        public int TabIndex
        {
            get { return base.ZIndex; }
            set { base.ZIndex = value; }
        }

        public System.Windows.Forms.Cursor Cursor
        {
            get { return System.Windows.Forms.Cursors.Default; }
            set { }
        }

        public Point PointToClient(Point pt)
        {
            return base.ContentPanel.PointToClient(pt);
        }

        public event System.EventHandler Resize;

        void ContentPanel_Resize(object sender, EventArgs e)
        {
            if (Resize != null)
            {
                Resize(this, e);
            }
        }

        //public RadTabStripContentPanel RISContentPanel
        //{
        //    //get { return base.; }
        //}
    }

    public class CSButton : RadButton
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadButton"; }
        }

        public bool UseVisualStyleBackColor
        {
            get { return false; }
            set { }
        }

        public System.Drawing.ContentAlignment TextAlign
        {
            set { this.TextAlignment = value; }
            get { return this.TextAlignment; }
        }
    }

    public class CSLabel : RadLabel
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadLabel"; }
        }

        public System.Drawing.ContentAlignment TextAlign
        {
            set { base.TextAlignment = value; }
            get { return base.TextAlignment; }
        }
    }

    public class CSCheckBox : RadCheckBox
    {
        public CSCheckBox()
        {
            this.ToggleStateChanged += new StateChangedEventHandler(CSCheckBox_ToggleStateChanged);
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadCheckBox"; }
        }

        public bool UseVisualStyleBackColor
        {
            get { return false; }
            set { }
        }

        public event EventHandler CheckedChanged;

        /// <summary>
        /// 居然重载不响应,还是用事件吧
        /// </summary>
        /// <param name="e"></param>
        protected override void OnToggleStateChanged(StateChangedEventArgs e) 
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, new EventArgs());
            }

            base.OnToggleStateChanged(e);
        }

        void CSCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, new EventArgs());
            }
        }
    }

    public class CSRadioButton : RadRadioButton
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadRadioButton"; }
        }

        public event EventHandler CheckedChanged;

        public bool Checked
        {
            get { return this.ToggleState == ToggleState.On; }
            set { this.ToggleState = value ? ToggleState.On : ToggleState.Off; }
        }

        public bool UseVisualStyleBackColor
        {
            get { return false; }
            set { }
        }

        public System.Drawing.ContentAlignment TextAlign
        {
            set { this.TextAlignment = value; }
            get { return this.TextAlignment; }
        }

        protected override void OnToggleStateChanging(StateChangingEventArgs e)
        {
            base.OnToggleStateChanging(e);
        }

        protected override void OnToggleStateChanged(StateChangedEventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, new EventArgs());
            }

            base.OnToggleStateChanged(e);
        }
    }    

    public class CSGroupBox : RadGroupBox
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadGroupBox"; }
        }

        public CSGroupBox()
            : base()
        {
            //The first Children[0] means the GroupBoxContent element. The second Children[0] means the FillPrimitive of GroupBoxContent element.
            ((FillPrimitive)this.GroupBoxElement.Children[0].Children[0]).BackColor = Color.Transparent;

            //((BorderPrimitive)this.GroupBoxElement.Children[1].Children[1]).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            //this.ThemeName = "Desert";
        }
    }

    public class CSLayoutPanel : Telerik.WinControls.Layouts.StackLayoutPanel
    {
        //public override string ThemeClassName
        //{
        //    get { return base.GetType().FullName; }
        //}
    }

    public class CSPanel : RadPanel
    {
        public CSPanel()
        {
            // Hide the Border by default
            this.PanelElement.Children[1].Visibility = ElementVisibility.Hidden;
        }

        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadPanel";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }

        public event EventHandler Load;

        public SizeF AutoScaleDimensions
        {
            get { return new System.Drawing.SizeF(6F, 13F); }
            set { }
        }

        public System.Windows.Forms.AutoSizeMode AutoSizeMode
        {
            get
            {
                return AutoSizeMode.GrowAndShrink;
                //return base.PanelElement.AutoSizeMode == RadAutoSizeMode.Auto;
            }
            set { }
        }

        public System.Windows.Forms.AutoScaleMode AutoScaleMode
        {
            get { return AutoScaleMode.Font; }
            set { }
        }

        public Control ActiveControl
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.Focused)
                        return ctrl;
                }

                return null;
            }
        }

        protected override void OnLoad(Size desiredSize)
        {
            if (Load != null)
            {
                Load(this, new EventArgs());
            }

            base.OnLoad(desiredSize);
        }
    }

    public class CSUserControl : RadControl
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.RadControl"; }
        }

        public event EventHandler Load;

        public SizeF AutoScaleDimensions
        {
            get { return new System.Drawing.SizeF(6F, 13F); }
            set { }
        }

        public System.Windows.Forms.AutoScaleMode AutoScaleMode
        {
            get { return AutoScaleMode.Font; }
            set { }
        }

        public Control ActiveControl
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.Focused)
                        return ctrl;
                }

                return null;
            }
        }

        protected override void OnLoad(Size desiredSize)
        {
            if (Load != null)
            {
                Load(this, new EventArgs());
            }

            base.OnLoad(desiredSize);
        }
    }

    public class CSPanelBar : RadPanelBar
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadPanelBar"; }
            set { base.ThemeClassName = value; }
        }
    }

    public class CSToggleButton : RadToggleButton
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadToggleButton"; }
            set { base.ThemeClassName = value; }
        }
    }

    public class CSToggleButton2 : RadToggleButton
    {
        public CSToggleButton2()
        {
            Telerik.WinControls.Primitives.FillPrimitive prmFill = (Telerik.WinControls.Primitives.FillPrimitive)this.ButtonElement.Children[0];
            ImageAndTextLayoutPanel imgTxtPanel = (ImageAndTextLayoutPanel)this.ButtonElement.Children[1];
            Telerik.WinControls.Primitives.BorderPrimitive prmBorder = (Telerik.WinControls.Primitives.BorderPrimitive)this.ButtonElement.Children[2];
            Telerik.WinControls.Primitives.FocusPrimitive prmFocus = (Telerik.WinControls.Primitives.FocusPrimitive)this.ButtonElement.Children[3];

            prmFill.GradientStyle = GradientStyles.OfficeGlassRect;
            prmFill.BackColor = prmFill.BackColor2 = prmFill.BackColor3 = prmFill.BackColor4 = Color.Transparent;

            prmBorder.Visibility = ElementVisibility.Hidden;
            prmBorder.Margin = new System.Windows.Forms.Padding(4);

            prmFocus.Visibility = ElementVisibility.Hidden;

            this.ToggleStateChanged += new StateChangedEventHandler(CSToggleButton2_ToggleStateChanged);
            this.MouseEnter += new EventHandler(CSToggleButton2_MouseEnter);
            this.MouseLeave += new EventHandler(CSToggleButton2_MouseLeave);
        }

        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadToggleButton";
            }
        }

        #region Private functions

        private void PerformStyle_NotActive()
        {
            Telerik.WinControls.Primitives.FillPrimitive prmFill = (Telerik.WinControls.Primitives.FillPrimitive)this.ButtonElement.Children[0];
            Telerik.WinControls.Primitives.BorderPrimitive prmBorder = (Telerik.WinControls.Primitives.BorderPrimitive)this.ButtonElement.Children[2];

            prmBorder.Visibility = ElementVisibility.Hidden;

            prmFill.GradientStyle = GradientStyles.OfficeGlassRect;
            prmFill.BackColor = prmFill.BackColor2 = prmFill.BackColor3 = prmFill.BackColor4 = Color.Transparent;
        }

        private void PerformStyle_Active()
        {
            Telerik.WinControls.Primitives.FillPrimitive prmFill = (Telerik.WinControls.Primitives.FillPrimitive)this.ButtonElement.Children[0];
            Telerik.WinControls.Primitives.BorderPrimitive prmBorder = (Telerik.WinControls.Primitives.BorderPrimitive)this.ButtonElement.Children[2];

            //prmBorder.Visibility = ElementVisibility.Visible;

            prmFill.GradientStyle = GradientStyles.Linear;
            prmFill.ResetValue(FillPrimitive.BackColorProperty);
            prmFill.ResetValue(FillPrimitive.BackColor2Property);
            prmFill.ResetValue(FillPrimitive.BackColor3Property);
            prmFill.ResetValue(FillPrimitive.BackColor4Property);
        }

        void CSToggleButton2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if(this.ToggleState == ToggleState.On)
            {
                PerformStyle_Active();
            }
            else
            {
                PerformStyle_NotActive();
            }
        }

        void CSToggleButton2_MouseLeave(object sender, EventArgs e)
        {
            if (this.ToggleState == ToggleState.On)
                return;

            PerformStyle_NotActive();
        }

        void CSToggleButton2_MouseEnter(object sender, EventArgs e)
        {
            PerformStyle_Active();
        }

        #endregion
    }

    public class CSDropDownButton : RadDropDownButton
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadDropDownButton"; }
            set { base.ThemeClassName = value; }
        }
    }

    public class CSStatusStrip : RadStatusStrip
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadStatusStrip"; }
            set { base.ThemeClassName = value; }
        }

        public ToolStripGripStyle GripStyle
        {
            get { return base.SizingGrip ? ToolStripGripStyle.Visible : ToolStripGripStyle.Hidden; }
            set { base.SizingGrip = value == ToolStripGripStyle.Visible ? true : false; }
        }

        //        protected override 
    }

    public class CSStatusStripLabel : RadLabelElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadLabelElement);
            }
        }

        public System.Drawing.ContentAlignment TextAlign
        {
            set { base.TextAlignment = value; }
            get { return base.TextAlignment; }
        }

        public new System.Windows.Forms.RightToLeft RightToLeft
        {
            get { return (base.RightToLeft ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No); }
            set { base.RightToLeft = ((value == System.Windows.Forms.RightToLeft.Yes) ? true : false); }
        }

        public bool Spring
        {
            get
            {
                return false; // System.Convert.ToBoolean(base.GetValue(StatusBarBoxLayout.SpringProperty));
            }
            set
            {
                //base.Parent.SetValue(StatusBarBoxLayout.SpringProperty, value);
            }
        }
    }

    public class CSMaskedTextBox : RadMaskedEditBox
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadMaskedEditBox";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }
    }

    public class CSMenuItem : RadMenuItem
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadMenuItem);
            }
        }
    }

    public class CSSplitter : RadSplitter
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadSplitter";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }
    }    

    public class CSChart : RadChart
    {
        public CSChart()
            : base()
        {
            this.PlotArea.EmptySeriesMessage.TextBlock.Text = string.Empty;
            this.PlotArea.XAxis.AxisLabel.Visible = true;
            this.PlotArea.YAxis.AxisLabel.Visible = true;
        }

        private object _originalDataSource;

        public object OriginalDataSource
        {
            get { return _originalDataSource; }
            set { _originalDataSource = value; }
        }
        private List<string> _originalFieldList = new List<string>();

        public List<string> OriginalFieldList
        {
            get { return _originalFieldList; }
            set { _originalFieldList = value; }
        }
        private string _originalLabels;

        public string OriginalLabels
        {
            get { return _originalLabels; }
            set { _originalLabels = value; }
        }

        private RadHScrollBar _hScrollBar;
        public RadHScrollBar HScrollBar
        {
            get { return _hScrollBar; }
            set { _hScrollBar = value; }
        }

        private RadSpinEditor _spinEditor;
        public RadSpinEditor SpinEditor
        {
            get { return _spinEditor; }
            set { _spinEditor = value; }
        }

    }

    public class CSSplitContainer : RadSplitContainer
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadSplitContainer";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }
    }

    public class CSSplitPanel : SplitPanel
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.SplitPanel";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }
    }

    public class CSContextMenu : RadContextMenu
    {
        
    }

    //this customized tablelayout panel is for telerik tabitem selection too slow bug only!
    public class QuickTableLayoutPanel : TableLayoutPanel
    {
        public QuickTableLayoutPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles(); 
        }
        
        protected override void OnVisibleChanged(EventArgs e)
        {
            ;//why telerik fire to all tabitem the visibility changed event?
        }

    }
}
