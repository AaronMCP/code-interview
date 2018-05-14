using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Hys.CommonControls
{
    public class CSToolStrip : RadToolStrip
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadToolStrip";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }
    }

    public class CSToolStripElement : RadToolStripElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToolStripElement);
            }
        }
    }

    public class CSToolStripItem : RadToolStripItem
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToolStripItem);
            }
        }
    }

    /// <summary>
    /// CSToolStripItem2 with the customized overflow menu.
    /// Notes:
    ///     1> ToolStrip should set ShowOverFlowButton as false
    ///     2> ToolStrip should NOT contain the duplicated Text items.
    ///     3> DropDownMenuAlignmentRight should be invoked on Form's Loading and ToolStrip's Resizing
    /// </summary>
    public class CSToolStripItem2 : RadToolStripItem
    {
        RadDropDownButtonElement _ddbeOthers = null;
        RadItem _currentItem = null;

        public CSToolStripItem2()
        {
            OverflowManager.ShowRootItem = false;
            OverflowManager.ShowResetItem = false;
            OverflowManager.ShowCustomizeItem = false;

            _ddbeOthers = new RadDropDownButtonElement();

            _ddbeOthers.Name = "ddbeOthers";
            _ddbeOthers.Text = "Other";
            _ddbeOthers.HasTwoColumnDropDown = true;

            _ddbeOthers.Click += new EventHandler(_ddbeOthers_Click);
            _ddbeOthers.DropDownOpening += new CancelEventHandler(_ddbeOthers_DropDownOpening);

            _ddbeOthers.DropDownMenu.Click += new EventHandler(DropDownMenu_Click);
            _ddbeOthers.DropDownMenu.ItemSelected += new ItemSelectedEventHandler(DropDownMenu_ItemSelected);
            _ddbeOthers.DropDownMenu.ItemDeselected += new ItemSelectedEventHandler(DropDownMenu_ItemDeselected);

            this.Items.Add(_ddbeOthers);
        }

        /// <summary>
        /// Note: invoke me on Form's Loading and ToolStrip's Resizing.
        /// </summary>
        public void DropDownMenuAlignmentRight()
        {
            /*
            int i = 0;
            for (i = 0; i < this.Items.Count; ++i)
            {
                if (this.Items[i].Visibility == ElementVisibility.Hidden)
                {
                    break;
                }
            }

            if (i >= this.Items.Count)
            {
                this.Items.Remove(_ddbeOthers);
                return;
            }

            if (i < 0) i = 0;

            // move left
            do
            {
                if (this.Items.Contains(_ddbeOthers))
                    this.Items.Remove(_ddbeOthers);

                this.Items.Insert(i, _ddbeOthers);
            }
            while (_ddbeOthers.Visibility != ElementVisibility.Visible && --i >= 0);

            ++i;

            // move right
            while (i >= 0 && i < this.Items.Count && this.Items[i].Visibility == ElementVisibility.Visible)
            {
                if (this.Items.Contains(_ddbeOthers))
                    this.Items.Remove(_ddbeOthers);

                this.Items.Insert(i, _ddbeOthers);

                ++i;
            }
            */
            if (this.Items.Count > 0 && this.Items.Count > 14)//max is 12 + 2(the first two fixed button)
            {
                    if (this.Items.Contains(_ddbeOthers))
                    {
                        this.Items.Remove(_ddbeOthers);
                    }
                    this.Items.Insert(14, _ddbeOthers);
                    for (int i = this.Items.Count - 1; i > 15; i--)
                    {
                        this.Items[i].Visibility = ElementVisibility.Hidden;
                    }
                    _ddbeOthers.Visibility = ElementVisibility.Visible;
            }
        }

        public RadDropDownButtonElement getDropDownMenu()
        {
            return _ddbeOthers;
        }

        #region private functions

        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToolStripItem);
            }
        }

        private void DropDownMenu_ItemDeselected(object sender, ItemSelectedEventArgs args)
        {
            _currentItem = null;
        }

        private void DropDownMenu_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DropDownMenu_Click, " + System.DateTime.Now.ToString("mm:ss fff") + ", Current=" + (_currentItem == null ? "NULL" : _currentItem.Text));

            if (_currentItem == null)
                return;

            foreach (RadItem ri in this.Items)
            {
                if (string.IsNullOrEmpty(ri.Text))
                    continue;

                if (ri.Text == _currentItem.Text)
                {
                    _ddbeOthers.HideDropDown();

                    ri.PerformClick();

                    break;
                }
            }
        }

        private void DropDownMenu_ItemSelected(object sender, ItemSelectedEventArgs args)
        {
            _currentItem = args.Item;

            //System.Diagnostics.Debug.WriteLine("DropDownMenu_ItemSelected, " + System.DateTime.Now.ToString("mm:ss fff"));
        }

        private void _ddbeOthers_Click(object sender, EventArgs e)
        {
            PrepareDropDownMenu();

            if (!_ddbeOthers.IsDropDownShown)
                _ddbeOthers.ShowDropDown();
        }

        private void _ddbeOthers_DropDownOpening(object sender, CancelEventArgs e)
        {
            RemoveVisibleOnes();
        }

        private RadItem ContainText(RadDropDownButtonElement ddbe, string txt)
        {
            foreach (RadItem ri in ddbe.Items)
            {
                if (ri.Text == txt)
                    return ri;
            }

            return null;
        }

        private void RemoveItemByText(RadDropDownButtonElement ddbe, string txt)
        {
            for (int i = ddbe.Items.Count - 1; i >= 0; --i)
            {
                RadItem ri = ddbe.Items[i];
                if (ri.Text == txt)
                {
                    ddbe.Items.RemoveAt(i);
                }
            }
        }

        private void RemoveVisibleOnes()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Visibility == ElementVisibility.Visible)
                {
                    RadButtonElement rbe = this.Items[i] as RadButtonElement;
                    if (rbe == null)
                        continue;

                    RemoveItemByText(_ddbeOthers, rbe.Text);
                }
            }
        }

        private void PrepareDropDownMenu()
        {
            _ddbeOthers.Items.Clear();
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Visibility == ElementVisibility.Hidden)
                {
                    RadButtonElement rbe = null;

                    ToolStripItemsOverFlow.RadFakeElement fakeElem = this.Items[i] as ToolStripItemsOverFlow.RadFakeElement;
                    if (fakeElem == null)
                    {
                        rbe = this.Items[i] as RadButtonElement;
                        if (rbe == null)
                            continue;
                    }
                    else
                    {
                        rbe = fakeElem.AssociatedItem as RadButtonElement;
                        if (rbe == null)
                            continue;
                    }

                    RadItem riExisted = ContainText(_ddbeOthers, rbe.Text);
                    if (riExisted != null)
                    {
                        riExisted.Enabled = rbe.Enabled;

                        continue;
                    }

                    RadMenuItem rmi = new RadMenuItem();
                    rmi.Text = rbe.Text;
                    rmi.Enabled = rbe.Enabled;

                    System.Diagnostics.Debug.WriteLine("Add item of radDropdownbuttonElment, " + System.DateTime.Now.ToString("mm:ss fff") + "," + rbe.Text);

                    //int myidx = FindInsertPosition(i);
                    //if (myidx >= 0 && myidx < _ddbeOthers.Items.Count)
                    //{
                    //    _ddbeOthers.Items.Insert(myidx, rmi);
                    //}
                    //else
                    //{
                    //    _ddbeOthers.Items.Add(rmi);
                    //}
                    _ddbeOthers.Items.Add(rmi);
                }
                else if (this.Items[i].Visibility == ElementVisibility.Collapsed)
                {
                    continue;
                }
                else if (this.Items[i].Visibility == ElementVisibility.Visible)
                {
                    RadButtonElement rbe = this.Items[i] as RadButtonElement;
                    if (rbe == null)
                        continue;

                    RemoveItemByText(_ddbeOthers, rbe.Text);
                }
            }
        }

        private int FindInsertPosition(int idx)
        {
            int i = 0, m;
            for (i = idx + 1; i < this.Items.Count; ++i)
            {
                RadItem nextItem = this.Items[i];

                m = 0;

                for (m = 0; m < _ddbeOthers.Items.Count; ++m)
                {
                    if (nextItem.Text == _ddbeOthers.Items[m].Text)
                    {
                        return m;
                    }
                }
            }

            return -1;
        }

        // BUG, it happens before radToolStrip1_Resize.
        //protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnBoundsChanged, " + System.DateTime.Now.ToString("mm:ss fff"));

        //    base.OnBoundsChanged(e);

        //    DropDownMenuAlignmentRight();
        //}

        #endregion
    }

    public class CSToolStripLabel : RadToolStripLabelElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToolStripLabelElement);
            }
        }
    }

    public class CSToolStripButton : RadButtonElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadButtonElement);
            }
        }

        public new System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                switch (base.DisplayStyle)
                {
                    case Telerik.WinControls.DisplayStyle.Image:
                        return ToolStripItemDisplayStyle.Image;
                        break;
                    case Telerik.WinControls.DisplayStyle.ImageAndText:
                        return ToolStripItemDisplayStyle.ImageAndText;
                        break;
                    case Telerik.WinControls.DisplayStyle.None:
                        return ToolStripItemDisplayStyle.None;
                        break;
                    case Telerik.WinControls.DisplayStyle.Text:
                        return ToolStripItemDisplayStyle.Text;
                        break;
                    default:
                        break;
                }

                return ToolStripItemDisplayStyle.Text;
            }
            set
            {
                switch (value)
                {
                    case ToolStripItemDisplayStyle.Image:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
                        break;
                    case ToolStripItemDisplayStyle.ImageAndText:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
                        break;
                    case ToolStripItemDisplayStyle.None:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.None;
                        break;
                    case ToolStripItemDisplayStyle.Text:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
                        break;
                }
            }
        }

        public Color ImageTransparentColor
        {
            get { return Color.Black; }
            set { }
        }

        public bool Visible
        {
            get { return base.Visibility == ElementVisibility.Visible; }
            set
            {
                if (value)
                {
                    base.Visibility = ElementVisibility.Visible;
                }
                else
                {
                    base.Visibility = ElementVisibility.Hidden;
                }
            }
        }
    }

    public class CSToolStripToggleButton : RadToggleButtonElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToggleButtonElement);
            }
        }

        public new System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                switch (base.DisplayStyle)
                {
                    case Telerik.WinControls.DisplayStyle.Image:
                        return ToolStripItemDisplayStyle.Image;
                        break;
                    case Telerik.WinControls.DisplayStyle.ImageAndText:
                        return ToolStripItemDisplayStyle.ImageAndText;
                        break;
                    case Telerik.WinControls.DisplayStyle.None:
                        return ToolStripItemDisplayStyle.None;
                        break;
                    case Telerik.WinControls.DisplayStyle.Text:
                        return ToolStripItemDisplayStyle.Text;
                        break;
                    default:
                        break;
                }

                return ToolStripItemDisplayStyle.Text;
            }
            set
            {
                switch (value)
                {
                    case ToolStripItemDisplayStyle.Image:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
                        break;
                    case ToolStripItemDisplayStyle.ImageAndText:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
                        break;
                    case ToolStripItemDisplayStyle.None:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.None;
                        break;
                    case ToolStripItemDisplayStyle.Text:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
                        break;
                }
            }
        }

        public Color ImageTransparentColor
        {
            get { return Color.Black; }
            set { }
        }

        public bool Visible
        {
            get { return base.Visibility == ElementVisibility.Visible; }
            set
            {
                if (value)
                {
                    base.Visibility = ElementVisibility.Visible;
                }
                else
                {
                    base.Visibility = ElementVisibility.Hidden;
                }
            }
        }
    }

    public class CSToolStripComboBox : RadComboBoxElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadComboBoxElement);
            }
        }
    }

    public class CSToolStripSeparator : RadToolStripSeparatorItem
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadToolStripSeparatorItem);
            }
        }
    }

    public class CSToolStripDropDownButton : RadDropDownButtonElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadDropDownButtonElement);
            }
        }

        public new System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                switch (base.DisplayStyle)
                {
                    case Telerik.WinControls.DisplayStyle.Image:
                        return ToolStripItemDisplayStyle.Image;
                        break;
                    case Telerik.WinControls.DisplayStyle.ImageAndText:
                        return ToolStripItemDisplayStyle.ImageAndText;
                        break;
                    case Telerik.WinControls.DisplayStyle.None:
                        return ToolStripItemDisplayStyle.None;
                        break;
                    case Telerik.WinControls.DisplayStyle.Text:
                        return ToolStripItemDisplayStyle.Text;
                        break;
                    default:
                        break;
                }

                return ToolStripItemDisplayStyle.Text;
            }
            set
            {
                switch (value)
                {
                    case ToolStripItemDisplayStyle.Image:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
                        break;
                    case ToolStripItemDisplayStyle.ImageAndText:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
                        break;
                    case ToolStripItemDisplayStyle.None:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.None;
                        break;
                    case ToolStripItemDisplayStyle.Text:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
                        break;
                }
            }
        }

        public Color ImageTransparentColor
        {
            get { return Color.Black; }
            set { }
        }
    }

    public class CSToolStripCheckBox : RadCheckBoxElement
    {
        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadCheckBoxElement);
            }
        }

        public new System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                switch (base.DisplayStyle)
                {
                    case Telerik.WinControls.DisplayStyle.Image:
                        return ToolStripItemDisplayStyle.Image;
                        break;
                    case Telerik.WinControls.DisplayStyle.ImageAndText:
                        return ToolStripItemDisplayStyle.ImageAndText;
                        break;
                    case Telerik.WinControls.DisplayStyle.None:
                        return ToolStripItemDisplayStyle.None;
                        break;
                    case Telerik.WinControls.DisplayStyle.Text:
                        return ToolStripItemDisplayStyle.Text;
                        break;
                    default:
                        break;
                }

                return ToolStripItemDisplayStyle.Text;
            }
            set
            {
                switch (value)
                {
                    case ToolStripItemDisplayStyle.Image:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
                        break;
                    case ToolStripItemDisplayStyle.ImageAndText:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
                        break;
                    case ToolStripItemDisplayStyle.None:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.None;
                        break;
                    case ToolStripItemDisplayStyle.Text:
                        base.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
                        break;
                }
            }
        }

        public Color ImageTransparentColor
        {
            get { return Color.Black; }
            set { }
        }
    }
}
