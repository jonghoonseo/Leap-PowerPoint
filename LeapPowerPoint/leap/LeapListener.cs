using System;
using Leap;

namespace LeapSlideShow
{
    class LeapListener: Listener
    {
        private Microsoft.Office.Interop.PowerPoint.SlideShowWindow _showWindow;
        public Microsoft.Office.Interop.PowerPoint.SlideShowWindow ShowWindow 
        {
            //set the slide show window
            set { this._showWindow = value; }
            //get the slide show window
            get { return this._showWindow; }
        }


        public override void OnInit(Controller cntrlr)
        {
            //Console.WriteLine("Initialized");
            cntrlr.EnableGesture(Gesture.GestureType.TYPESWIPE);

            if (cntrlr.Config.SetFloat("Gesture.Swipe.MinLength", 90) &&              // default is 150
                cntrlr.Config.SetFloat("Gesture.Swipe.MinVelocity", 200))              // default is 1000
                cntrlr.Config.Save();

        }

        public override void OnConnect(Controller cntrlr)
        {
            //Console.WriteLine("Connected");
        }

        public override void OnDisconnect(Controller cntrlr)
        {
            //Console.WriteLine("Disconnected");
        }

        public override void OnExit(Controller cntrlr)
        {
            //Console.WriteLine("Exited");
        }

        private long currentTime;
        private long previousTime;
        private long timeChange;

        private int lastGestureID;

        private uint twoFingerCounter=0;

        // variables for Fist interaction
        //private Hand hand0;
        //private bool moved = false;

        public override void OnFrame(Controller cntrlr)
        {
            // Get the current frame.
            Frame currentFrame = cntrlr.Frame();

            currentTime = currentFrame.Timestamp;
            timeChange = currentTime - previousTime;

            //////////////////////////////////////////////////////////////////////////
            //  제스처 인식 적용 구간 - 제스처 성능 개판임
            GestureList gestures = currentFrame.Gestures();
            if (gestures[0].IsValid)
            {
                if (lastGestureID != currentFrame.Gestures()[0].Id)
                {
                    //System.Windows.Forms.MessageBox.Show("gesture");
                    lastGestureID = currentFrame.Gestures()[0].Id;

                    SwipeGesture aGesture = new SwipeGesture(gestures[0]);

                    if (aGesture.Direction.x > 0)
                        ShowWindow.View.Next();
                    else
                        ShowWindow.View.Previous();
                }

                return;
            }
            //////////////////////////////////////////////////////////////////////////

            bool cont = true;
            if (timeChange > 500)
            {
                if (!currentFrame.Hands.IsEmpty)
                {
                ////    //////////////////////////////////////////////////////////////////////////
                ////    //  주먹으로 드로우 할라 그랬는데 성능이 턱없이 부족함
                ////    //      주먹을 1로 인식하는 경우가 너무 많음
                ////    ////// 주먹이면
                ////    if (currentFrame.Pointables.Count == 0 ||
                ////        (currentFrame.Pointables.Count == 1 && (currentFrame.Pointables[0].Length < 55 || currentFrame.Pointables[0].Length > 75)))
                ////    {
                ////        Hand handCurr = currentFrame.Hands[0];

                ////        // 저장되지 않은 주먹이면 새로 저장
                ////        if (hand0 == null || hand0.Id != handCurr.Id)
                ////        {
                ////            hand0 = handCurr;
                ////            System.Diagnostics.Debug.WriteLine(hand0.Id);
                ////        }
                ////        else
                ////        {
                ////            float diff = hand0.StabilizedPalmPosition.x - handCurr.StabilizedPalmPosition.x;
                ////            //if (diff < 0)
                ////            //{
                ////            //    ShowWindow.Left += diff;
                ////            //    //Microsoft.Office.Interop.PowerPoint.SlideShowWindow newWindow;
                ////            //    //////ShowWindow.View.PointerType = Microsoft.Office.Interop.PowerPoint.PpSlideShowPointerType.ppSlideShowPointerPen;
                ////            //    //////ShowWindow.View.PointerColor.RGB = System.Drawing.Color.Green.ToArgb();
                ////            //    //////MouseCursor.Draw(700 - (int)diff, (int)handCurr.StabilizedPalmPosition.y);

                ////            //} else
                ////            ShowWindow.Left += diff * (float)-0.03;
                ////            Microsoft.Office.Interop.PowerPoint.Slide currentSlide = ShowWindow.View.Slide;
                ////            //Microsoft.Office.Interop.PowerPoint.Slide nextSlide = ShowWindow.Presentation.Slides._Index(currentSlide.SlideIndex + 1);
                ////            //Microsoft.Office.Interop.PowerPoint.SlideShowWindow newWindow;
                            

                ////            //ShowWindow.View.GotoSlide(currentSlide.SlideIndex + 1);     // <TODO> 여기서 에러 남 - +1 이 인덱스를 넘을 가능성


                ////            moved = true;
                ////        }
                ////        cont = false;
                ////    }
                    //////////////////////////////////////////////////////////////////////////

                    
                    // Get the first finger in the list of fingers
                    Finger finger = currentFrame.Fingers[0];
                    
                    // Get the closest screen intercepting a ray projecting from the finger
                    Screen screen = cntrlr.LocatedScreens.ClosestScreenHit(finger);


                    if (cont && screen != null && screen.IsValid)
                    {
                        // Get the velocity of the finger tip
                        var tipVelocity = (int)finger.TipVelocity.Magnitude;

                        // Use tipVelocity to reduce jitters when attempting to hold
                        // the cursor steady
                        if (tipVelocity > 25)
                        {
                            var xScreenIntersect = screen.Intersect(finger, true).x;
                            var yScreenIntersect = screen.Intersect(finger, true).y;
                            
                            if (xScreenIntersect.ToString() != "NaN")
                            {
                                var x = (int)(xScreenIntersect * screen.WidthPixels);
                                var y = (int)(screen.HeightPixels - (yScreenIntersect * screen.HeightPixels));


                                if (currentFrame.Fingers.Count == 2)
                                {
                                    if (twoFingerCounter++ >= 15 && currentFrame.Fingers[1].TouchDistance > 0.2)
                                    {
                                        ShowWindow.View.PointerType = Microsoft.Office.Interop.PowerPoint.PpSlideShowPointerType.ppSlideShowPointerPen;
                                        MouseCursor.Draw(x, y);
                                    } else 
                                    {
                                        ShowWindow.View.PointerType = Microsoft.Office.Interop.PowerPoint.PpSlideShowPointerType.ppSlideShowPointerAutoArrow;
                                        MouseCursor.MoveCursor(x, y);
                                    }
                                }
                                else if (currentFrame.Fingers.Count == 1)
                                {
                                    ShowWindow.View.PointerType = Microsoft.Office.Interop.PowerPoint.PpSlideShowPointerType.ppSlideShowPointerAutoArrow;
                                    MouseCursor.MoveCursor(x, y);

                                    twoFingerCounter = 0;
                                }
                            }
                        }
                    }
                }
                ////else if(moved)      // 손이 없는데 화면이 움직여 있으면 바로 잡기 <-- Fist Interaction
                ////    ShowWindow.Left = 0;

                previousTime = currentTime;
            }
        }
    }
}
