using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DShowNET.Device;

namespace Hys.CareAgent.WcfService
{
    public class CameraSingleton
    {
        private static CameraSingleton instance = null;

        public static CameraSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CameraSingleton();
                }
                return instance;
            }
        }

        private CameraCpature camCapture = null;

        private Panel previePanel = null;

        public CameraSingleton()
        {
            InitCamera();
        }

        /// <summary>
        /// Alsace update
        /// </summary>
        public void RestartCamera()
        {
            bStarted = false;
        }

        bool bStarted = false;
        public void StartPreview(DsDevice device)
        {
            if (!bStarted)
            {
                camCapture.StartPreview(device);
                bStarted = true;
            }
        }

        public void ChangeDevice(DsDevice device)
        {
            camCapture.StartPreview(device);
        }

        public CameraCpature CameraCpature
        {
            get { return this.camCapture; }
        }

        public bool Started
        {
            get { return this.bStarted; }
        }

        public DsDevice[] GetDevices()
        {
            return CameraCpature.GetDevices();
        }

        public Panel PreviewPanel
        {
            get { return this.previePanel; }
        }

        public void Dispose()
        {
            if (camCapture != null)
            {
                camCapture.Dispose();
            }
        }
        
        private void InitCamera()
        {
            previePanel = new Panel();
            camCapture = new CameraCpature(this.previePanel);
        }
        public void ShowCapPinDialog()
        {
             camCapture.ShowCapPinDialog();
        }
    }
}
