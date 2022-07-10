using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication5.Helper;
using WindowsFormsApplication5.Model;

namespace WindowsFormsApplication5
{
    public partial class Formchinh : Form
    {
        // Khai báo 
        private bool bEBL1 = false, bEBL2 = false, bEBL3 = true, NGUON = false, TRON = false, QUAT = false;
      
        private int iDrawEllipse = 60, iDrawLine1 = 90;
        private int iDrawEllipse2 = 100, n = -135;
        private int iMoverButton = 410, iMoverButton2 = 410, iCountThread; 

        private Point _centerPoint;
        private Point currentPoint = new Point();
        private Point ToaDoChuotKhiAnButton = new Point();

        Timer t = new Timer();
        Timer t2 = new Timer();
        int u, u1,u2, u3,number, number2;
        Pen p, penRed, penYellow, z, th, k;
        Graphics g, g1;
        Bitmap bmp;
  
        private bool isArcDrawer = false;
        private List<ArcDrawerModel> arcDrawers;
        
        public Formchinh()
        {
            
            InitializeComponent();
            InitializeValue();
            setHinhTron();
            if (NGUON)
                this.BackColor = Color.Black;
            u = 0;
            u1 = 0;
            u3 = 0;
            t.Interval = 1;
            t2.Interval = 30;
            t2.Tick += new EventHandler(this.t2_Tick);
            t2.Start();
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
            u2 = 280;
            number = n * 2;
            number2 = n * 2;

        }

        private void InitializeValue()
        {
            _centerPoint = new Point(pboxRadar.Width / 2, pboxRadar.Height / 2);
            arcDrawers = new List<ArcDrawerModel>();

        }
        // thuộc tính màn hình hiển thị
        private void setHinhTron()
        {
            bmp = new Bitmap(pboxRadar.Width, pboxRadar.Height);
            g = Graphics.FromImage(bmp);
            g1 = Graphics.FromImage(bmp);
            g.TranslateTransform(-1, -1);
            g1.TranslateTransform(-1, -1);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g1.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pboxRadar.Width + 1, pboxRadar.Height + 1);
            Region region = new Region(path);
            g.SetClip(region, CombineMode.Replace);
            g1.SetClip(region, CombineMode.Replace);

        }
      // vẽ màn hình theo thời gian
        private int iSetContr = 30;
        private void t_Tick(object sender, EventArgs e)
        {
            p = new Pen(Color.Green, 1 / 2f);
            z = new Pen(Color.White, 1 / 2f);
            penRed = new Pen(Color.Yellow, 2f);
            penYellow = new Pen(Color.Red, 3f);
           
            g.Clear(Color.FromArgb(iSetContr, Color.Gray));

            ChonTrangThai();
            g.DrawClosedCurve();
            
            foreach (var arcDrawer in arcDrawers)
            {
                g.ArcDrawer(arcDrawer);

            }

            
            if (bEBL1)
            {
                g.DrawEllipse(penYellow, (iDrawEllipse + u2), (iDrawEllipse + u2), pboxRadar.Width - (iDrawEllipse + u2) * 2, pboxRadar.Height - (iDrawEllipse + u2) * 2);
            u2++;
            if (u2 == 599)
            {
                u2 = 280;
            }
            }
            p.Dispose();
           
            u++;
            
            if (u == 360)
            {
                u = 0;
                
            }
            u3++;
            if (u3 == 90)
                u3 = 0;
        }
     // thời gian mục tiêu di chuyển
        int a;
        private void t2_Tick(object sender, EventArgs e)
        {
            a++;
            th = new Pen(Color.White, 1f);
            k = new Pen(Color.Yellow, 3f);
            if (iCountThread >= 400)
            {

                if (NGUON == true)
                {
                    if (u1 % 100 == 0)
                    {
                        ButtonMove1();
                        ButtonMove();
                        if (TRON == true || bEBL1 == true)
                        {
                            pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\manhinh6.pNg");
                            mt1.Visible = true;
                            mt2.Visible = true;
                            mt3.Visible = true;
                            mt4.Visible = true;
                            mt5.Visible = true;
                        }
                        if (QUAT == true)
                        {
                            pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\manhinhquat1.pNg");
                            mt5.Visible = true;
                        }
                    }
                   
                    if (u1 % 50 == 0)
                    {
                        ButtonMove2();
                    }
                  
                }
               
            }

            pboxRadar.Image = bmp;
            k.Dispose();
            iCountThread++;
            u1++;
            if (u1 == 360)
            {
                u1 = 0;
            }
        }
        // HIEN HLF

        #region Phương thức
        // vẽ các vòng tròn cự ly cố định
        private void VeCacVongTron()
        {
            int iSizeDrawArc = 100;

            if (NGUON)
                for (int i = 1; i < 3; i++)
                {
                    g.DrawEllipse(p, iSizeDrawArc * i, iSizeDrawArc * i, pboxRadar.Width - iSizeDrawArc * i * 2, pboxRadar.Height - iSizeDrawArc * i * 2);
                    g.DrawEllipse(z, 1, 1, pboxRadar.Width - 1, pboxRadar.Height - 1);

                }
        }
        // phần mở rộng không dùng
        private void VeVongTronCoTheThayDoi()
        {
            // vẽ 2 vòng tròn
            if (bEBL1)
            {
                for (int i = 1; i < 100; i++)
                {

                  //  g.DrawEllipse(penRed, iDrawEllipse, iDrawEllipse, pboxRadar.Width - iDrawEllipse * 2, pboxRadar.Height - iDrawEllipse * 2);
                }
            }


            if (bEBL2)
            {
                if (iDrawEllipse2 >= 0 && iDrawEllipse2 <= 280)
                {
                   // g.DrawEllipse(penRed, iDrawEllipse2, iDrawEllipse2, pboxRadar.Width - iDrawEllipse2 * 2, pboxRadar.Height - iDrawEllipse2 * 2);
                }
            }
        }

        int a11 = 0;
        private void VeVongTronCoTheThayDoiTH1(Point ToaDoChuot)
        {
            // vẽ 2 vòng tròn
            if (bEBL1)
            {

                for (int i = 1; i < 100; i++)
                {
    
                    if (iDrawEllipse >= 0 && iDrawEllipse <= 280)
                    {
                        int iDrawEllipseNew = iDrawEllipse -a11;
                        Rectangle rect = new Rectangle() { Width = iDrawEllipseNew, Height = iDrawEllipseNew, X = ToaDoChuot.X - (iDrawEllipseNew / 2), Y = ToaDoChuot.Y - (iDrawEllipseNew / 2) };
                        g.DrawArc(penRed, rect, 0, 360);

                    }
                }
            }
            if (bEBL2)
            {
                if (iDrawEllipse2 >= 0 && iDrawEllipse2 <= 280)
                {
                    int iDrawEllipseNew = 325 - iDrawEllipse2;
                    Rectangle rect = new Rectangle() { Width = iDrawEllipseNew, Height = iDrawEllipseNew, X = ToaDoChuot.X - (iDrawEllipseNew / 2), Y = ToaDoChuot.Y - (iDrawEllipseNew / 2) };
                    g.DrawArc(penRed, rect, 0, 360);
                }
            }
        }

        private void VeDuongLineCoTheThayDoi(Point pTamHinhTron)
        {
            if (NGUON == true)
            {


                //vẽ 2 đường line
                if (bEBL3)
                {
                    for (int i = 0; i <= 12; i++)
                    {
                        g.DrawLine(p, pTamHinhTron, DrawHelper.getPointDrawLine(iDrawLine1 + i * 30, _centerPoint, pboxRadar.Width / 2));
                    }
                }
            }
        }


        // kiểm tra xem toa độ chuot co nam ngoai dung tron hay không 
        double iR, iX, iY;
        private bool CheckDrawEllipse(Point pValue)
        {
            if (pValue.X < _centerPoint.X) iX = _centerPoint.X - pValue.X;
            else iX = pValue.X - _centerPoint.X;
            if (pValue.Y < _centerPoint.Y) iY = _centerPoint.Y - pValue.Y;
            else iY = pValue.Y - _centerPoint.Y;
            iR = (double)Math.Sqrt(iX * iX + iY * iY);
            if (iR <= 322)  //  là bán kính của vòng tròn, 
            {
                return true;
            }
            return false;
        }

        // hien do cung
        private void ChonTrangThai()
        {
            if (iR <= 301)
            {
                lbBanKinh.Text = ((int)iR).ToString();
               
                lbDoCung.Text = DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString();
                if (lbDoCung.Text == "360")
                    lbDoCung.Text = "0";
            }
            if (bTH1)   // nếu được ấn thì chuyển sang trạng thái 
            {
                TrangThaiMot();
            }
            else TrangThaiBinhThuong();
        }


        private void TrangThaiBinhThuong()
        {
            VeCacVongTron();
            VeVongTronCoTheThayDoi();
            VeDuongLineCoTheThayDoi(_centerPoint);
            if (NGUON == true)
            {
                if (TRON)
                { // Đường quét tron
                    if (thuan == true)
                        for (int i = 0; i < 4; i++)
                        {
                            g.DrawLine(p, _centerPoint, DrawHelper.getPointDrawLine(u+i, _centerPoint, pboxRadar.Width / 2));
                        }
                    if (nguoc == true)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            g.DrawLine(p, _centerPoint, DrawHelper.getPointDrawLine(-u + i, _centerPoint, pboxRadar.Width / 2));
                        }
                    }
                }
                if (QUAT)
                {
                    if (thuan == true)
                        // Đường quét quat
                        for (int i = 0; i < 4; i++)
                        {
                            g.DrawLine(penRed, _centerPoint, DrawHelper.getPointDrawLine(u3 + i, _centerPoint, pboxRadar.Width / 2));
                        }
                    if (nguoc == true)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            g.DrawLine(penRed, _centerPoint, DrawHelper.getPointDrawLine(90-u3+i, _centerPoint, pboxRadar.Width / 2));
                        }
                    }
                }
            }
            
        }

        private void TrangThaiMot()
        {
            if (bClickMouse)
            {
                
                VeDuongLineCoTheThayDoi(ToaDoChuotKhiAnButton);
                VeVongTronCoTheThayDoiTH1(ToaDoChuotKhiAnButton);
            }
            
        }

       
        #endregion

        // mục tiêu chuyển động theo thời gian
        private void ButtonMove1()
        {


            if (NGUON == true)
            {

                iMoverButton++;
                if (iMoverButton >= 800)
                {
                    iMoverButton = 400;
                }
            }
        }
        private void ButtonMove2()
        {


            if (NGUON == true)
            {

                iMoverButton2++;
                if (iMoverButton2 >= 800)
                {
                    iMoverButton2 = 400;
                }
            }
        }
        // button di
        private void ButtonMove()
        {
            if (NGUON == true&&TRON==true)
            {
                mt1.Visible = true;
                mt1.Location = new Point(iMoverButton-100 , 300);
                mt2.Visible = true;
                mt2.Location = new Point(350, iMoverButton - 280);
                mt3.Visible = true;
                mt3.Location = new Point(iMoverButton - 150, 200);
                mt4.Visible = true;
                mt4.Location = new Point(iMoverButton - 50, 250);
                mt5.Visible = true;
                mt5.Location = new Point(700, iMoverButton - 200);
                
                
                iMoverButton++;
                if (iMoverButton >= 800)
                {
                    iMoverButton = 500;
                }
            }
        }
        #region Events

        private void button3_Click(object sender, EventArgs e)
        {
            if (NGUON == true)
                bEBL1 = !bEBL1;
        }
        // Hiển thị các phương vị cố định
        private void button12_Click(object sender, EventArgs e)
        {

            NGUON = !NGUON;
            if (NGUON == true)
            {
                
                do00.Visible = true;
                do30.Visible = true;
                do60.Visible = true;
                do90.Visible = true;
                do120.Visible = true;
                do150.Visible = true;
                do180.Visible = true;
                do210.Visible = true;
                do240.Visible = true;
                do270.Visible = true;
                do300.Visible = true;
                do330.Visible = true;

            }
            else
            {
                

                do00.Visible = false;
                do30.Visible = false;
                do60.Visible = false;
                do90.Visible = false;
                do120.Visible = false;
                do150.Visible = false;
                do180.Visible = false;
                do210.Visible = false;
                do240.Visible = false;
                do270.Visible = false;
                do300.Visible = false;
                do330.Visible = false;

            }

        }
        // thoát chương trình
        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        #endregion
        // Phần mở rộng
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            currentPoint = e.Location;
            currentPoint = e.Location;

            if (iR > 0)
            {
               
            }
            if (CheckDrawEllipse(e.Location))
            {
                pboxRadar.Cursor = Cursors.Cross;
            }
            else
            {
                pboxRadar.Cursor = Cursors.Default;
            }

        }
  
       
        bool bTH1 = false;
        private void btnTH1_Click(object sender, EventArgs e)
        {
            bTH1 = !bTH1;
        }

        
        bool bTH2 = false;
        private void btnTH2_Click(object sender, EventArgs e)
        {
            bTH2 = !bTH2;
        }

        private bool bClickMouse = false;

        private void button15_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            isArcDrawer = !isArcDrawer;
         
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            paneld.Visible = true;
            NGUON = !NGUON;
            NGUON = true;
           
            do00.Visible = true;
            do30.Visible = true;
            do60.Visible = true;
            do90.Visible = true;
            do120.Visible = true;
            do150.Visible = true;
            do180.Visible = true;
            do210.Visible = true;
            do240.Visible = true;
            do270.Visible = true;
            do300.Visible = true;
            do330.Visible = true;

        }
        // OFF màn hình
        private void button14_Click_1(object sender, EventArgs e)
        {
            paneld.Visible = false;
            NGUON = false;
         
            do00.Visible = false;
            do30.Visible = false;
            do60.Visible = false;
            do90.Visible = false;
            do120.Visible = false;
            do150.Visible = false;
            do180.Visible = false;
            do210.Visible = false;
            do240.Visible = false;
            do270.Visible = false;
            do300.Visible = false;
            do330.Visible = false;
            arcDrawers = new List<ArcDrawerModel>();
            TRON = false;
            bEBL1 = false;
            QUAT = false;
            mt1.Visible = false;
            mt2.Visible = false;
            mt3.Visible = false;
            mt4.Visible = false;
            mt5.Visible = false;
           
            pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\manhinh2.pNg");
        }
    // Cài đặt thời gian quét
        private void button2_Click_3(object sender, EventArgs e)
        {
            t.Interval = 5;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            t.Interval = 45;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            t.Interval = 100;
        }
        // Hiện mục tiêu
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (NGUON == true)
            { QUAT = !QUAT; 
            TRON=false;
            bEBL1=false;
            mt1.Visible = false;
            mt2.Visible = false;
            mt3.Visible = false;
            mt4.Visible = false;
                
            }
        }
        // chế độ quét tròn
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (NGUON == true)
            { bEBL1 = !bEBL1;
            QUAT = false;
            TRON = false;
            }
       
        }
        // chế độ quét quạt
        private void button6_Click_1(object sender, EventArgs e)
        {
            if (NGUON == true)
            { TRON = !TRON;
            bEBL1 = false;
            QUAT = false;
            
            }
        }
        // quét thuận, ngược
        private bool thuan = false, nguoc = false;
        private void button7_Click_1(object sender, EventArgs e)
        {
            thuan = true;
            nguoc = false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            thuan = false;
            nguoc = true;
        }
        private bool Blmt1 = false, Blmt2 = false;
        // mục tiêu 1
        private void mt1_Click(object sender, EventArgs e)
        {
            Blmt1 = !Blmt1;
          
           PV.Text = ""+ DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString()+" Do";
           if (iR <= 301)
           {
               Cl.Text = "" + ((int)iR).ToString() + " Km";
              
           }
           VT.Text = "20.1 Km/h";
           DC.Text = "20 km";
           mt1.BackColor = Color.Pink;
           mt2.BackColor = Color.Yellow;
           mt4.BackColor = Color.Yellow;
           mt5.BackColor = Color.Yellow;
           mt3.BackColor = Color.Yellow;
           paneld.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\hinh1.pNg");

        }
        // mục tiêu 2
        private void mt2_Click(object sender, EventArgs e)
        {
            Blmt2 = !Blmt2;

            PV.Text = "" + DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString() + " Do";
            if (iR <= 301)
            {
                Cl.Text = ""+((int)iR).ToString()+" Km";

            }
            VT.Text = "23,7 Km/h";
            DC.Text = "35 Km";
            mt2.BackColor = Color.Pink;
            mt1.BackColor = Color.Yellow;
            mt4.BackColor = Color.Yellow;
            mt5.BackColor = Color.Yellow;
            mt3.BackColor = Color.Yellow;
            paneld.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\hinh2.pNg");
        }
        // mục tiêu 3
        private void button10_Click(object sender, EventArgs e)
        {
            

            PV.Text = "" + DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString() + " Do";
            if (iR <= 301)
            {
                Cl.Text = "" + ((int)iR).ToString() + " Km";

            }
            VT.Text = "23,7 Km/h";
            DC.Text = "50 Km";
            mt2.BackColor = Color.Yellow;
            mt1.BackColor = Color.Yellow;
            mt4.BackColor = Color.Yellow;
            mt5.BackColor = Color.Yellow;
            mt3.BackColor = Color.Pink;
            paneld.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\hinh3.pNg");
        }
        // Mục tiêu 4
        private void mt4_Click(object sender, EventArgs e)
        {
            PV.Text = "" + DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString() + " Do";
            if (iR <= 301)
            {
                Cl.Text = "" + ((int)iR).ToString() + " Km";

            }
            VT.Text = "23,7 Km/h";
            DC.Text = "10 km";
            mt2.BackColor = Color.Yellow;
            mt1.BackColor = Color.Yellow;
            mt3.BackColor = Color.Yellow;
            mt5.BackColor = Color.Yellow;
            mt4.BackColor = Color.Pink;
            paneld.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\hinh4.pNg");
        }
        // Mục tiêu 5
        private void mt5_Click(object sender, EventArgs e)
        {
            PV.Text = "" + DrawHelper.dcGetAngle(_centerPoint, currentPoint).ToString() + " Do";
            if (iR <= 301)
            {
                Cl.Text = "" + ((int)iR).ToString() + " Km";

            }
            VT.Text = "23,7 Km/h";
            DC.Text = "400 m";
            mt2.BackColor = Color.Yellow;
            mt1.BackColor = Color.Yellow;
            mt3.BackColor = Color.Yellow;
            mt4.BackColor = Color.Yellow;
            mt5.BackColor = Color.Pink;
            paneld.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\hinh5.pNg");
        }

        private void Formchinh_Load(object sender, EventArgs e)
        {

        }

    }
}











