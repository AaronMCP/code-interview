using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UITest.MappingViewer
{
    public class MPanelControler : IPanel
    {
        private Panel _panel;
        private List<IControler> _controlerLists;

        private List<IRelation> _relationList;
        public List<IRelation> RelationList
        {
            get { return _relationList; }
            set { _relationList = value; }
        }

        public MPanelControler(Panel pnl)
        {
            _panel = pnl;
            _panel.Paint += new PaintEventHandler(_panel_Paint);
            _controlerLists = new List<IControler>();
            _relationList = new List<IRelation>();
            _selectedRelations = new List<IRelation>();
        }

        public void AddList(IControler list)
        {
            if (list == null) return;

            list.Panel = this;
            list.ItemSelected += new ItemSelectedHandler(list_ItemSelected);

            _controlerLists.Add(list);

            Control ctrl = list.GetControl();
            if (ctrl != null)
            {
                _panel.Controls.Add(ctrl);
                ctrl.Visible = true;
            }
        }
        public void RemoveList(IControler list)
        {
            if (_controlerLists.Contains(list))
            {
                list.Panel = null;
                list.ItemSelected -= new ItemSelectedHandler(list_ItemSelected);

                _controlerLists.Remove(list);

                Control ctrl = list.GetControl();
                if (!_panel.Controls.Contains(ctrl))
                {
                    _panel.Controls.Add(ctrl);
                }
            }
        }
        public void ClearList()
        {
            foreach (IControler item in _controlerLists)
            {
                item.Panel = null;
                item.ItemSelected -= new ItemSelectedHandler(list_ItemSelected);
            }
            _controlerLists.Clear();
            _panel.Controls.Clear();
            _relationList.Clear();
        }

        public void Redraw()
        {
            _panel.Refresh();
        }
        public void Reposition()
        {
            _panel.SuspendLayout();
            
            int count = _controlerLists.Count;
            int width = _panel.ClientSize.Width;
            int height = _panel.ClientSize.Height;

            List<Rectangle> rlist = new List<Rectangle>();
            int xInterval = width / (2 * count + 1);
            for (int i = 0; i < count; i++)
            {
                IControler ctl = _controlerLists[i];
                Control ctrl = ctl.GetControl();
                if (ctrl == null) continue;
                ctrl.Top = (height - ctrl.Height) / 2;
                ctrl.Left = xInterval * (i * 2 + 1);
                ctrl.Width = xInterval;
            }

            _panel.ResumeLayout();
        }

        private List<IRelation> _selectedRelations;
        private void RefreshSelectedRelations(IItem item)
        {
            _selectedRelations.Clear();
            if (item == null) return;
            foreach (IRelation r in _relationList)
            {
                if (r.Sources.Contains(item) || r.Targets.Contains(item))
                {
                    _selectedRelations.Add(r);
                }
            }
        }

        private void list_ItemSelected(IControler ctrl, IItem item)
        {
            RefreshSelectedRelations(item);
            Redraw();
        }

        private void _panel_Paint(object sender, PaintEventArgs e)
        {
            DrawRelation(e.Graphics, e.ClipRectangle);
        }
        
        private void DrawRelation(Graphics g, Rectangle r)
        {
            if (_relationList.Count < 1)
            {
                g.Clear(_panel.BackColor);
                return;
            }

            foreach (IRelation rel in _relationList)
            {
                if(rel.Sources.Count < 1 || rel.Targets.Count < 1 ) continue;

                float penWidth = 1;
                if (_selectedRelations.Contains(rel)) penWidth = 2;

                switch (rel.Type)
                {
                    case MRelationType.OneToOne:
                        {
                            IItem sItem = rel.Sources[0];
                            IItem tItem = rel.Targets[0];
                            DrawLine(g,
                                sItem.Controler.GetListRectangle(),
                                sItem.Controler.GetItemRectangle(sItem),
                                tItem.Controler.GetListRectangle(),
                                tItem.Controler.GetItemRectangle(tItem),
                                penWidth);
                            break;
                        }
                    case MRelationType.OneToMulti:
                        {
                            IItem sItem = rel.Sources[0];
                            foreach (IItem tItem in rel.Targets)
                            {
                                DrawLine(g,
                                    sItem.Controler.GetListRectangle(),
                                    sItem.Controler.GetItemRectangle(sItem),
                                    tItem.Controler.GetListRectangle(),
                                    tItem.Controler.GetItemRectangle(tItem),
                                    penWidth);
                            }
                            break;
                        }
                    case MRelationType.MultiToOne:
                        {
                            IItem tItem = rel.Targets[0];
                            foreach (IItem sItem in rel.Sources)
                            {
                                DrawLine(g,
                                    sItem.Controler.GetListRectangle(),
                                    sItem.Controler.GetItemRectangle(sItem),
                                    tItem.Controler.GetListRectangle(),
                                    tItem.Controler.GetItemRectangle(tItem),
                                    penWidth);
                            }
                            break;
                        }
                }
            }
        }
        private void DrawLine(Graphics g, Rectangle sList, Rectangle sItem, Rectangle eList, Rectangle eItem, float penWidth)
        {
            if (sList.IsEmpty || sItem.IsEmpty || eList.IsEmpty || eItem.IsEmpty) return;

            Rectangle lList, lItem, rList, rItem;
            if (sList.Left < eList.Left)
            {
                lList = sList;
                lItem = sItem;
                rList = eList;
                rItem = eItem;
            }
            else
            {
                lList = eList;
                lItem = eItem;
                rList = sList;
                rItem = sItem;
            }

            Point sPoint = new Point(lList.Left + lList.Width, lList.Top + lItem.Top + lItem.Height / 2);
            Point ePoint = new Point(rList.Left, rList.Top + rItem.Top + rItem.Height / 2);

            using (SolidBrush b = new SolidBrush(Color.Black))
            {
                using (Pen p = new Pen(b))
                {
                    p.Width = penWidth;
                    g.DrawLine(p, sPoint, ePoint);
                }
            }
        }
    }
}
