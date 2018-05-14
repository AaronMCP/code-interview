using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Forms
{
    public class FileComboBoxControler
    {
        private ComboBox _cb;

        private void _cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemSelected == null) return;
            FileComboBoxItem item = _cb.SelectedItem as FileComboBoxItem;
            if (item != null) ItemSelected(item);
        }

        public event FileComboBoxItemSelectedHanlder ItemSelected;

        public readonly string FolderPath;

        public FileComboBoxControler(ComboBox cb, string folderPath, bool loadRightNow)
        {
            _cb = cb;
            FolderPath = folderPath;

            if (_cb == null) throw new ArgumentNullException();
            if (FolderPath == null) throw new ArgumentNullException();

            if (loadRightNow) Load();

            _cb.SelectedIndexChanged += new EventHandler(_cb_SelectedIndexChanged);
        }

        public void Load()
        {
            try
            {
                _cb.Items.Clear();
                string[] flist = Directory.GetFiles(FolderPath);

                SortedList<string, string> slist = new SortedList<string, string>();
                foreach (string f in flist)
                {
                    slist.Add(f, f);
                }

                foreach (KeyValuePair<string, string> p in slist)
                {
                    FileComboBoxItem i = new FileComboBoxItem(p.Value);
                    _cb.Items.Add(i);
                }
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
            }
        }

        public void SelectTheFirstItem()
        {
            if (_cb.Items.Count > 0) _cb.SelectedIndex = 0;
        }

        public FileComboBoxItem GetSelectedItem()
        {
            return _cb.SelectedItem as FileComboBoxItem;
        }
    }

    public delegate void FileComboBoxItemSelectedHanlder(FileComboBoxItem item);

    public class FileComboBoxItem
    {
        public readonly string FilePath;
        public readonly string FileName;

        public FileComboBoxItem(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
        }

        public override string ToString()
        {
            return FileName;
        }

        private string _fileContent;
        public string FileContent
        {
            get
            {
                if (_fileContent == null)
                {
                    try
                    {
                        _fileContent = File.ReadAllText(FilePath);
                    }
                    catch (Exception err)
                    {
                        Program.Context.Log.Write(err);
                    }
                }
                return _fileContent;
            }
        }
    }
}
