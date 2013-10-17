using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;

namespace LeapSlideShow
{
    public partial class RibbonLeapSlideShow
    {
        Microsoft.Office.Interop.PowerPoint.Application thisApplication;
        Microsoft.Office.Interop.PowerPoint.SlideShowWindow showWindow;
        bool isLeapConnected;
        Leap.Controller controller;
        LeapListener listener;

        private void RibbonLeapSlideShow_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void buttonSlideShowFullScreen_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                thisApplication = Globals.ThisAddIn.Application;
                showWindow = thisApplication.ActivePresentation.SlideShowSettings.Run();

                // If leap is not available, just run slideshow and quit
                while (true)
                {
                    // LeapMotion이 꽂혀 있으면 밖으로 나감
                    if (setupLeapMotion())
                        break;

                    // 만약에 Retry를 No 하면, 걍 종료
                    if (MessageBox.Show("Leap Motion is not connected. Check the connection.", "Leap Motion is missing.", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                        return;
                }
                thisApplication.SlideShowEnd += new Microsoft.Office.Interop.PowerPoint.EApplication_SlideShowEndEventHandler(thisApplication_SlideShowEnd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        void thisApplication_SlideShowEnd(Microsoft.Office.Interop.PowerPoint.Presentation Pres)
        {
            if (!isLeapConnected)
                return;
            isLeapConnected = false;

            showWindow.View.PointerType = Microsoft.Office.Interop.PowerPoint.PpSlideShowPointerType.ppSlideShowPointerAutoArrow;
            MouseCursor.MouseUp();

            controller.RemoveListener(listener);
            controller.Dispose();
        }

        private bool setupLeapMotion()
        {
            controller = new Leap.Controller();
            for (int i = 0; !controller.IsConnected && i < 5; ++i)
                System.Threading.Thread.Sleep(100);

            // if Leap is not available, quit
            if (!controller.IsConnected)
                return isLeapConnected = false;
            isLeapConnected = true;

            listener = new LeapListener();
            listener.ShowWindow = showWindow;
            controller.AddListener(listener);

            

            return true;
        }
    }
}
