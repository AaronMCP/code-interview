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
/*                        Author : Bruce Deng  2011-03-03
/****************************************************************************/


using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
//Reference the DShowNet lib(Dotnet version of direct show) 
using DShowNET;
using DShowNET.Device;

//test

namespace Hys.CareAgent.WcfService
{

    /// <summary> Build preview and capture filter</summary>
    public class CameraCpature : ISampleGrabberCB, IDisposable
    {

        // Wait for the async job to finish 
        private ManualResetEvent _reset = new ManualResetEvent(false);
        // the _image - needs to be global because it is fetched with an async method call
        public Bitmap _image;
        /// <summary> base filter of the actually used video devices. </summary>
        private IBaseFilter _capFilter;
        /// <summary> graph builder interface to used to preview. </summary>
        private IGraphBuilder _graphBuilder;

        /// <summary> capture graph builder interface. </summary>
        private ICaptureGraphBuilder2 _capGraphBuilder2;
        private ISampleGrabber _sampGrabber;

        /// <summary> control interface. </summary>
        private IMediaControl _mediaCtrl;

        /// <summary> grabber filter interface. </summary>
        private IBaseFilter _baseGrabFilter;

        // DShow Filter: Control preview window -> copy of _graphBuilder
        protected IVideoWindow _videoWindow;

        /// <summary> structure describing the bitmap to grab. </summary>
        private VideoInfoHeader _videoInfoHeader;


        /// <summary> buffer for bitmap data. </summary>
        private byte[] _bitmapData;

        /// <summary> list of installed video devices. </summary>
        private ArrayList _capDevices;
        public Control _viewControl = null;				// Property Backer: Owner control for preview
        //alsace
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");


        private bool disposed = false;

        public CameraCpature(Control preview)
        {
            _viewControl = preview;
        }

        ~CameraCpature()
        {
            Dispose();
        }


        public virtual void Dispose()
        {
            if (!this.disposed)
            {
                try
                {
                    // release scarce resource here
                    if (_capDevices != null)
                    {
                        foreach (DsDevice d in _capDevices)
                            d.Dispose();

                        _capDevices = null;
                    }
                }
                finally
                {
                    this.disposed = true;
                    GC.SuppressFinalize(this);
                }
            }
        }

        public void ReleaseDevice()
        {
            try
            {
                if (_capDevices != null)
                {
                    foreach (DsDevice d in _capDevices)
                        d.Dispose();

                    _capDevices = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ResetEvent()
        {

        }

        public void WaitForEvent()
        {
            if (!_reset.WaitOne(100000, false))
            {
                throw new Exception("Timeout waiting to get picture");
            }
        }

        public void StartPreview(DsDevice device)
        {
            try
            {
                _reset.Reset();
                Init(device);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _reset.Set();
            }

        }

        public Bitmap GetImage()
        {
            try
            {
                CaptureBmpData();

                _image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                return _image;
            }
            catch (Exception ex)
            {
                // CloseInterfaces();
                throw new Exception(ex.Message);
            }
        }



        public static DsDevice[] GetDevices()
        {
            ArrayList devices;


            if (!DsUtils.IsCorrectDirectXVersion())
            {
                _logger.Error("DirectX 8.1 NOT installed!");
                devices = new ArrayList();
                // throw new Exception("DirectX 8.1 NOT installed!");
            }

            if (!DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out devices))
            {
                //throw new Exception("No video capture devices found!");
                _logger.Error("No video capture devices found!");
                devices = new ArrayList();
            }
            return (DsDevice[])devices.ToArray(typeof(DsDevice));
        }

        private void CaptureBmpData()
        {


            if (_sampGrabber == null)
            {

                return;
            }

            if (_bitmapData == null)
            {
                int size = _videoInfoHeader.BmiHeader.ImageSize;
                // sanity check
                if ((size < 1000) || (size > 16000000))
                {

                    return;
                }
                _bitmapData = new byte[size + 64000];
            }

            int hr = _sampGrabber.SetCallback(this, 1);
            _reset.Reset();
            if (!_reset.WaitOne(10000, false))
            {
                throw new Exception("Timeout waiting to get picture");
            }
        }


        private void Init(DsDevice device)
        {
            CloseInterfaces();
            // store it for clean up.            
            _capDevices = new ArrayList();
            _capDevices.Add(device);

            StartupVideo(device.Mon);
        }

        /// <summary> capture event, triggered by buffer callback. </summary>
        private void OnCaptureDone()
        {
            if (_sampGrabber == null)
            {

                return;
            }

            int w = _videoInfoHeader.BmiHeader.Width;
            int h = _videoInfoHeader.BmiHeader.Height;
            if (((w & 0x03) != 0) || (w < 32) || (w > 4096) || (h < 32) || (h > 4096))
            {

                return;
            }

            int stride = w * 3;


            GCHandle handle = GCHandle.Alloc(_bitmapData, GCHandleType.Pinned);
            int scan0 = (int)handle.AddrOfPinnedObject();
            scan0 += (h - 1) * stride;
            _image = new Bitmap(w, h, -stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);
            //.Format24bppRgb
            handle.Free();
            _bitmapData = null;
            Thread.Sleep(1000);
            _reset.Set();

        }



        /// <summary> start all the interfaces, graphs and preview window. </summary>
        private bool StartupVideo(UCOMIMoniker mon)
        {
            int hr;
            if (!CreateCaptureDevice(mon))
                return false;

            if (!GetInterfaces())
                return false;

            if (!SetupGraph())
                return false;

            hr = _mediaCtrl.Run();
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            // will thow up input for tuner settings.
            //bool hasTuner = DsUtils.ShowTunerPinDialog( _capGraphBuilder2, _capFilter,this._viewControl.Handle );

            return true;
        }

        /// <summary> build the capture graph for grabber. </summary>
        private bool SetupGraph()
        {
            const int WS_CHILD = 0x40000000;
            const int WS_CLIPCHILDREN = 0x02000000;
            const int WS_CLIPSIBLINGS = 0x04000000;

            int hr;
            hr = _capGraphBuilder2.SetFiltergraph(_graphBuilder);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            hr = _graphBuilder.AddFilter(_capFilter, "Ds.NET Video Capture Device");
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);


            // will thow up user input for quality
            //DsUtils.ShowCapPinDialog(_capGraphBuilder2, _capFilter, IntPtr.Zero);

            AMMediaType media = new AMMediaType();
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;		// 
            hr = _sampGrabber.SetMediaType(media);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            hr = _graphBuilder.AddFilter(_baseGrabFilter, "Ds.NET Grabber");
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            Guid cat;
            Guid med;

            cat = PinCategory.Capture;
            med = MediaType.Video;
            hr = _capGraphBuilder2.RenderStream(ref cat, ref med, _capFilter, null, _baseGrabFilter); // _baseGrabFilter 

            media = new AMMediaType();
            hr = _sampGrabber.GetConnectedMediaType(media);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                throw new NotSupportedException("Unknown Grabber Media Format");

            _videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            Marshal.FreeCoTaskMem(media.formatPtr); media.formatPtr = IntPtr.Zero;

            hr = _sampGrabber.SetBufferSamples(false);
            if (hr == 0)
                hr = _sampGrabber.SetOneShot(false);
            if (hr == 0)
                hr = _sampGrabber.SetCallback(null, 0);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);



            // Render preview (video -> renderer)           


            hr = _capGraphBuilder2.RenderStream(PinCategory.Preview, ref med, _capFilter, null, null);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);


            // Get the IVideoWindow interface
            _videoWindow = (IVideoWindow)_graphBuilder;
            // Set the video window to be a child of the main window
            hr = _videoWindow.put_Owner(this._viewControl.Handle);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            // Set video window style
            hr = _videoWindow.put_WindowStyle(WS_CHILD | WS_CLIPCHILDREN | WS_CLIPSIBLINGS);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
            // Position video window in client rect of owner window
            _viewControl.Resize += new EventHandler(onPreviewWindowResize);
            onPreviewWindowResize(this, null);

            //Make the video window visible, now that it is properly positioned
            hr = _videoWindow.put_Visible(DsHlp.OATRUE);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            hr = _mediaCtrl.Run();
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
            return true;
        }

        /// <summary> Resize the preview when the PreviewWindow is resized </summary>
        protected void onPreviewWindowResize(object sender, EventArgs e)
        {
            if (_videoWindow != null)
            {
                // Position video window in client rect of owner window
                Rectangle rc = _viewControl.ClientRectangle;
                _videoWindow.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
            }
        }


        /// <summary> create the used COM components and get the interfaces. </summary>
        private bool GetInterfaces()
        {
            Type comType = null;
            object comObj = null;
            try
            {
                comType = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow FilterGraph not installed or registered!");
                comObj = Activator.CreateInstance(comType);
                _graphBuilder = (IGraphBuilder)comObj; comObj = null;

                Guid clsid = Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(ICaptureGraphBuilder2).GUID;
                comObj = DsBugWO.CreateDsInstance(ref clsid, ref riid);
                _capGraphBuilder2 = (ICaptureGraphBuilder2)comObj; comObj = null;

                comType = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow SampleGrabber not installed or registered!");
                comObj = Activator.CreateInstance(comType);
                _sampGrabber = (ISampleGrabber)comObj; comObj = null;

                _mediaCtrl = (IMediaControl)_graphBuilder;
                _baseGrabFilter = (IBaseFilter)_sampGrabber;
                return true;
            }
            catch (Exception ee)
            {
                if (comObj != null)
                    Marshal.ReleaseComObject(comObj); comObj = null;

                throw ee;
            }
        }

        /// <summary> create the user selected capture device. </summary>
        private bool CreateCaptureDevice(UCOMIMoniker mon)
        {
            object capObj = null;
            try
            {
                Guid gbf = typeof(IBaseFilter).GUID;
                mon.BindToObject(null, null, ref gbf, out capObj);
                _capFilter = (IBaseFilter)capObj; capObj = null;
                return true;
            }
            catch (Exception ee)
            {
                if (capObj != null)
                    Marshal.ReleaseComObject(capObj); capObj = null;
                throw ee;
            }
        }



        /// <summary>
        /// MUST do this. 
        /// Notice alot of crap in here - I've had some problems over extending the bandwidth of
        /// the USB bas.
        /// </summary>
        public void CloseInterfaces()
        {
            int hr;
            try
            {
                if (_mediaCtrl != null)
                {
                    hr = _mediaCtrl.Stop();
                    _mediaCtrl = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _baseGrabFilter = null;

            try
            {
                if (_sampGrabber != null)
                    Marshal.ReleaseComObject(_sampGrabber);
                _sampGrabber = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                if (_capGraphBuilder2 != null)
                    Marshal.ReleaseComObject(_capGraphBuilder2);
                _capGraphBuilder2 = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                if (_graphBuilder != null) Marshal.ReleaseComObject(_graphBuilder);
                _graphBuilder = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                if (_capFilter != null) Marshal.ReleaseComObject(_capFilter);
                _capFilter = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                //if (_capDevices != null)
                //{
                //    foreach (DsDevice d in _capDevices)
                //        d.Dispose();

                //    _capDevices = null;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary> sample callback, not used. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            return 0;
        }

        /// <summary> buffer callback, could be from foreign thread.</summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (_bitmapData == null)
            {
                return 0;
            }

            if ((pBuffer != IntPtr.Zero) && (BufferLen > 1000) && (BufferLen <= _bitmapData.Length))
                Marshal.Copy(pBuffer, _bitmapData, 0, BufferLen);

            this.OnCaptureDone();
            return 0;
        }

        //
        public bool ShowCapPinDialog()
        {
            ICaptureGraphBuilder2 bld = _capGraphBuilder2;
            IBaseFilter flt = _baseGrabFilter;

            int hr;
            object comObj = null;
            IAMStreamConfig pamsc = null;

            try
            {
                Guid cat = PinCategory.Capture;
                Guid type = MediaType.Interleaved;
                Guid iid = typeof(IAMStreamConfig).GUID;
                hr = bld.FindInterface(ref cat, ref type, flt, ref iid, out comObj);

                if (hr != 0)
                {
                    type = MediaType.Video;
                    hr = bld.FindInterface(ref cat, ref type, flt, ref iid, out comObj);
                    if (hr != 0)
                        return false;
                }

                pamsc = comObj as IAMStreamConfig;
                if (pamsc == null)
                    return false;

                int count;
                int size;
                hr = pamsc.GetNumberOfCapabilities(out count, out size);
                IntPtr pt = Marshal.AllocHGlobal(128);

                for (int iFormat = 0; iFormat < count; iFormat++)
                {
                    AMMediaType ammtp = new AMMediaType();
                    hr = pamsc.GetStreamCaps(iFormat, out ammtp, pt);

                    if (hr == 0)
                    {
                        VideoInfoHeader pvihdr = new VideoInfoHeader();
                        Marshal.PtrToStructure(ammtp.formatPtr, pvihdr);
                        if (pvihdr.BmiHeader.Width == 176)
                        {
                            pvihdr.AvgTimePerFrame = 10000000 / 15;
                            hr = pamsc.SetFormat(ammtp);
                            break;
                        }
                    }
                }

                return true;
            }
            catch (Exception ee)
            {
                Trace.WriteLine("!Ds.NET: ShowCapPinDialog " + ee.Message);
                return false;
            }
            finally
            {
                pamsc = null;
                if (comObj != null)
                    Marshal.ReleaseComObject(comObj);
                comObj = null;
            }
        }
    }
}
