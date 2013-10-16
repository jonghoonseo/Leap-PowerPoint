namespace LeapSlideShow
{
    partial class RibbonLeapSlideShow : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonLeapSlideShow()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonLeapSlideShow));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonSlideShowFromFirst = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonSlideShowFromFirst);
            this.group1.Label = "Leap Slide Show";
            this.group1.Name = "group1";
            // 
            // buttonSlideShowFromFirst
            // 
            this.buttonSlideShowFromFirst.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSlideShowFromFirst.Image = ((System.Drawing.Image)(resources.GetObject("buttonSlideShowFromFirst.Image")));
            this.buttonSlideShowFromFirst.Label = "처음부터 슬라이드쇼";
            this.buttonSlideShowFromFirst.Name = "buttonSlideShowFromFirst";
            this.buttonSlideShowFromFirst.ShowImage = true;
            this.buttonSlideShowFromFirst.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSlideShowFullScreen_Click);
            // 
            // RibbonLeapSlideShow
            // 
            this.Name = "RibbonLeapSlideShow";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonLeapSlideShow_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSlideShowFromFirst;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonLeapSlideShow RibbonLeapSlideShow
        {
            get { return this.GetRibbon<RibbonLeapSlideShow>(); }
        }
    }
}
