//Game puzzle

//create day 29th oct 2018
//last update 01st dêc 2018

//  Trung Hảo Nguyễn

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
using System.IO;

namespace Puzzle
{
    public partial class Puzzle : Form
    {
        int n = 3;
        private int[] pic_3x3 = new int[25];    //lưu cả phần viền 
        private int[] pic_4x4 = new int[36];
        private int[] pic_5x5 = new int[49];
        //lưu vị trí các pictureBox khi đổi tọa độ
        //với tọa độ x,y sao cho pic_3x3[3x+y]la pictureBox thứ 4(x-1)+y
        private int move = 0;   //lưu lượt di chuyển
        private Point[] PicLoc_3x3 = new Point[9];        //lưu tọa độ ban đầu
        private Point[] PicLoc_4x4 = new Point[16];
        private Point[] PicLoc_5x5 = new Point[25];
        int sec = 0, min = 0;   //lưu thời gian cho time
        StreamReader help = new StreamReader("Help.txt");       //dùng để tạo hdsd
        string imgName = "Uncen/ZOE.jpg";

        public Puzzle()
        {
            InitializeComponent();

            PicLoc_3x3[0] = ptb_3_00.Location;
            PicLoc_3x3[1] = ptb_3_01.Location;
            PicLoc_3x3[2] = ptb_3_02.Location;
            PicLoc_3x3[3] = ptb_3_03.Location;
            PicLoc_3x3[4] = ptb_3_04.Location;
            PicLoc_3x3[5] = ptb_3_05.Location;
            PicLoc_3x3[6] = ptb_3_06.Location;
            PicLoc_3x3[7] = ptb_3_07.Location;
            PicLoc_3x3[8] = ptb_3_08.Location;

            PicLoc_4x4[0] = ptb_4_00.Location;
            PicLoc_4x4[1] = ptb_4_01.Location;
            PicLoc_4x4[2] = ptb_4_02.Location;
            PicLoc_4x4[3] = ptb_4_03.Location;
            PicLoc_4x4[4] = ptb_4_04.Location;
            PicLoc_4x4[5] = ptb_4_05.Location;
            PicLoc_4x4[6] = ptb_4_06.Location;
            PicLoc_4x4[7] = ptb_4_07.Location;
            PicLoc_4x4[8] = ptb_4_08.Location;
            PicLoc_4x4[9] = ptb_4_09.Location;
            PicLoc_4x4[10] = ptb_4_10.Location;
            PicLoc_4x4[11] = ptb_4_11.Location;
            PicLoc_4x4[12] = ptb_4_12.Location;
            PicLoc_4x4[13] = ptb_4_13.Location;
            PicLoc_4x4[14] = ptb_4_14.Location;
            PicLoc_4x4[15] = ptb_4_15.Location;

            PicLoc_5x5[0] = ptb_5_00.Location;
            PicLoc_5x5[1] = ptb_5_01.Location;
            PicLoc_5x5[2] = ptb_5_02.Location;
            PicLoc_5x5[3] = ptb_5_03.Location;
            PicLoc_5x5[4] = ptb_5_04.Location;
            PicLoc_5x5[5] = ptb_5_05.Location;
            PicLoc_5x5[6] = ptb_5_06.Location;
            PicLoc_5x5[7] = ptb_5_07.Location;
            PicLoc_5x5[8] = ptb_5_08.Location;
            PicLoc_5x5[9] = ptb_5_09.Location;
            PicLoc_5x5[10] = ptb_5_10.Location;
            PicLoc_5x5[11] = ptb_5_11.Location;
            PicLoc_5x5[12] = ptb_5_12.Location;
            PicLoc_5x5[13] = ptb_5_13.Location;
            PicLoc_5x5[14] = ptb_5_14.Location;
            PicLoc_5x5[15] = ptb_5_15.Location;
            PicLoc_5x5[16] = ptb_5_16.Location;
            PicLoc_5x5[17] = ptb_5_17.Location;
            PicLoc_5x5[18] = ptb_5_18.Location;
            PicLoc_5x5[19] = ptb_5_19.Location;
            PicLoc_5x5[20] = ptb_5_20.Location;
            PicLoc_5x5[21] = ptb_5_21.Location;
            PicLoc_5x5[22] = ptb_5_22.Location;
            PicLoc_5x5[23] = ptb_5_23.Location;
            PicLoc_5x5[24] = ptb_5_24.Location;

            ReSave_3x3();

            Suffle();

            btn_move.Text = move.ToString();
            time.Stop();

            ptb_original.Visible = true;
            btn_original.Text = "START";
        }

        //đặt mọi code vẽ ra form tại OnPaint
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    //tạo đối tượng ảnh lớn
        //    AnhLon anhlon = new AnhLon("D:/Visual Studio/Bài tập HDT mở rộng/Bài 2_winform/ZOE.jpg");
        //    //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
        //    Image anhnho = anhlon.AnhCat(10, 10, 100, 100);
        //    //vẽ ảnh nhỏ ra form tại tọa độ 0,0
        //    e.Graphics.DrawImage(anhnho, 0, 0);
        //}

        //lớp AnhLon dùng để cắt ảnh
        public class AnhLon
        {
            Image anh;
            public AnhLon(string duongDanAnh)
            {
                anh = Image.FromFile(duongDanAnh);
            }

            public AnhLon(Image image)
            {
                anh = image;
            }

            public Bitmap AnhCat(int X, int Y, int Width, int Height)
            {
                Bitmap bm = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bm);
                g.DrawImage(anh, -X, -Y);
                return bm;
            }
        }

        public Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);

            g.InterpolationMode = InterpolationMode.Bicubic;    // Specify here
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return (Image)b;
        }

        //
        //3x3 Paint
        //

        private void ptb_3_01_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_01.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 0, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_02_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_02.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 102, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_03_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_03.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 204, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_04_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_04.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(102, 0, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_05_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_05.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(102, 102, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_06_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_06.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(102, 204, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_07_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_07.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(204, 0, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_3_08_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_3_08.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(204, 102, 100, 100);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        //
        //4x4 Paint
        //

        private void ptb_4_01_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_01.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 0, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_02_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_02.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 75, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_03_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_03.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 150, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_04_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_04.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(0, 225, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_05_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_05.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(75, 00, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_06_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_06.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(75, 75, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_07_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_07.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(75, 150, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_08_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_08.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(75, 225, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_09_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_09.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(150, 00, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_10_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_10.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(150, 75, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_11_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_11.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(150, 150, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_12_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_12.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(150, 225, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_13_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_13.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(225, 00, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_14_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_14.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(225, 75, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_4_15_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_4_15.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 299, 299);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(225, 150, 74, 74);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        //
        //5x5 paint
        //

        private void ptb_5_01_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_01.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(00, 00, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_02_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_02.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(00, 61, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_03_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_03.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(00, 122, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_04_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_01.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(00, 183, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_05_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_05.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(00, 244, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_06_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_06.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(61, 00, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_07_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_07.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(61, 61, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_08_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_08.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(61, 122, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_09_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_09.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(61, 183, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_10_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_10.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(61, 244, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_11_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_11.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(122, 00, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_12_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_12.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(122, 61, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_13_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_13.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(122, 122, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_14_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_14.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(122, 183, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_15_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_15.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(122, 244, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_16_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_16.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(183, 00, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_17_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_17.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(183, 61, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_18_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_18.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(183, 122, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_19_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_19.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(183, 183, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_20_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_20.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(183, 244, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_21_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_21.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(244, 00, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_22_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_22.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(244, 61, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_23_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_23.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(244, 122, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        private void ptb_5_24_Paint(object sender, PaintEventArgs e)
        {
            if (ptb_5_24.Visible)
            {
                var anh = Image.FromFile(imgName);
                var anhmoi = resizeImage(anh, 304, 304);
                base.OnPaint(e);
                //tạo đối tượng ảnh lớn
                AnhLon anhlon = new AnhLon(anhmoi);
                //tạo ảnh nhỏ từ ảnh lớn theo tọa độ 10, 10 và kích thước 100,100
                Image anhnho = anhlon.AnhCat(244, 183, 60, 60);
                //vẽ ảnh nhỏ ra pictureBox
                e.Graphics.DrawImage(anhnho, 0, 0);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                sec = 0;
                min++;
            }

            btn_min.Text = min.ToString("00");
            btn_sec.Text = sec.ToString("00");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_original_Click(object sender, EventArgs e)     //bấm để xem ảnh gốc
        {
            if (ptb_original.Visible == false)
            {
                Visible_3x3(false);
                Visible_4x4(false);
                Visible_5x5(false);
                ptb_original.Visible = true;
            }
            else
            {
                if (n == 3)
                {
                    Visible_3x3(true);
                }
                else if (n == 4)
                {
                    Visible_4x4(true);
                }
                else if (n == 5)
                {
                    Visible_5x5(true);
                }

                ptb_original.Visible = false;
            }

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original.Text = "Trở lại";
            }
            else if (btn_original.Text == "START")
            {
                btn_original.Text = "Xem ảnh gốc";
                btn_start_Click(sender, e);
            }
            else btn_original.Text = "Xem ảnh gốc";
        }

        private void ptb_original_Paint(object sender, PaintEventArgs e)
        {
            var anh = Image.FromFile(imgName);
            var img = resizeImage(anh, 304, 304);
            e.Graphics.DrawImage(img, 0, 0);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        private void ReSave_3x3()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    pic_3x3[5 * i + j] = 3 * (i - 1) + j;
                }
            }

            pic_3x3[18] = 0;

            ptb_3_00.Location = PicLoc_3x3[0];
            ptb_3_01.Location = PicLoc_3x3[1];
            ptb_3_02.Location = PicLoc_3x3[2];
            ptb_3_03.Location = PicLoc_3x3[3];
            ptb_3_04.Location = PicLoc_3x3[4];
            ptb_3_05.Location = PicLoc_3x3[5];
            ptb_3_06.Location = PicLoc_3x3[6];
            ptb_3_07.Location = PicLoc_3x3[7];
            ptb_3_08.Location = PicLoc_3x3[8];
        }

        private void ReSave_4x4()
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    pic_4x4[6 * i + j] = 4 * (i - 1) + j;
                }
            }

            pic_4x4[28] = 0;

            ptb_4_00.Location = PicLoc_4x4[0];
            ptb_4_01.Location = PicLoc_4x4[1];
            ptb_4_02.Location = PicLoc_4x4[2];
            ptb_4_03.Location = PicLoc_4x4[3];
            ptb_4_04.Location = PicLoc_4x4[4];
            ptb_4_05.Location = PicLoc_4x4[5];
            ptb_4_06.Location = PicLoc_4x4[6];
            ptb_4_07.Location = PicLoc_4x4[7];
            ptb_4_08.Location = PicLoc_4x4[8];
            ptb_4_09.Location = PicLoc_4x4[9];
            ptb_4_10.Location = PicLoc_4x4[10];
            ptb_4_11.Location = PicLoc_4x4[11];
            ptb_4_12.Location = PicLoc_4x4[12];
            ptb_4_13.Location = PicLoc_4x4[13];
            ptb_4_14.Location = PicLoc_4x4[14];
            ptb_4_15.Location = PicLoc_4x4[15];
        }

        private void ReSave_5x5()
        {
            for (int i = 1; i < 6; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    pic_5x5[7 * i + j] = 5 * (i - 1) + j;
                }
            }

            pic_5x5[40] = 0;

            ptb_5_00.Location = PicLoc_5x5[0];
            ptb_5_01.Location = PicLoc_5x5[1];
            ptb_5_02.Location = PicLoc_5x5[2];
            ptb_5_03.Location = PicLoc_5x5[3];
            ptb_5_04.Location = PicLoc_5x5[4];
            ptb_5_05.Location = PicLoc_5x5[5];
            ptb_5_06.Location = PicLoc_5x5[6];
            ptb_5_07.Location = PicLoc_5x5[7];
            ptb_5_08.Location = PicLoc_5x5[8];
            ptb_5_09.Location = PicLoc_5x5[9];
            ptb_5_10.Location = PicLoc_5x5[10];
            ptb_5_11.Location = PicLoc_5x5[11];
            ptb_5_12.Location = PicLoc_5x5[12];
            ptb_5_13.Location = PicLoc_5x5[13];
            ptb_5_14.Location = PicLoc_5x5[14];
            ptb_5_15.Location = PicLoc_5x5[15];
            ptb_5_16.Location = PicLoc_5x5[16];
            ptb_5_17.Location = PicLoc_5x5[17];
            ptb_5_18.Location = PicLoc_5x5[18];
            ptb_5_19.Location = PicLoc_5x5[19];
            ptb_5_20.Location = PicLoc_5x5[20];
            ptb_5_21.Location = PicLoc_5x5[21];
            ptb_5_22.Location = PicLoc_5x5[22];
            ptb_5_23.Location = PicLoc_5x5[23];
            ptb_5_24.Location = PicLoc_5x5[24];
        }

        //hàm Swith đổi vị trí hai pic_3x3turebox
        //
        //3x3
        //
        public void SWITCH_3x3(int a)//đổi vị trí của ptb_3_00 và ptb(a)    
        {
            move++;     //tăng lượt di chuyển

            switch (a)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_01.Location;
                        ptb_3_01.Location = t;

                        break;
                    }
                case 2:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_02.Location;
                        ptb_3_02.Location = t;

                        break;
                    }
                case 3:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_03.Location;
                        ptb_3_03.Location = t;

                        break;
                    }
                case 4:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_04.Location;
                        ptb_3_04.Location = t;

                        break;
                    }
                case 5:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_05.Location;
                        ptb_3_05.Location = t;

                        break;
                    }
                case 6:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_06.Location;
                        ptb_3_06.Location = t;

                        break;
                    }
                case 7:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_07.Location;
                        ptb_3_07.Location = t;

                        break;
                    }
                case 8:
                    {
                        Point t = ptb_3_00.Location;
                        ptb_3_00.Location = ptb_3_08.Location;
                        ptb_3_08.Location = t;

                        break;
                    }
            }

            btn_move.Text = move.ToString();
        }

        //
        //4x4
        //
        public void SWITCH_4x4(int a)
        {
            move++;

            switch (a)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_01.Location;
                        ptb_4_01.Location = t;

                        break;
                    }
                case 2:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_02.Location;
                        ptb_4_02.Location = t;

                        break;
                    }
                case 3:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_03.Location;
                        ptb_4_03.Location = t;

                        break;
                    }
                case 4:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_04.Location;
                        ptb_4_04.Location = t;

                        break;
                    }
                case 5:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_05.Location;
                        ptb_4_05.Location = t;

                        break;
                    }
                case 6:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_06.Location;
                        ptb_4_06.Location = t;

                        break;
                    }
                case 7:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_07.Location;
                        ptb_4_07.Location = t;

                        break;
                    }
                case 8:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_08.Location;
                        ptb_4_08.Location = t;

                        break;
                    }
                case 9:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_09.Location;
                        ptb_4_09.Location = t;

                        break;
                    }
                case 10:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_10.Location;
                        ptb_4_10.Location = t;

                        break;
                    }
                case 11:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_11.Location;
                        ptb_4_11.Location = t;

                        break;
                    }
                case 12:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_12.Location;
                        ptb_4_12.Location = t;

                        break;
                    }
                case 13:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_13.Location;
                        ptb_4_13.Location = t;

                        break;
                    }
                case 14:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_14.Location;
                        ptb_4_14.Location = t;

                        break;
                    }
                case 15:
                    {
                        Point t = ptb_4_00.Location;
                        ptb_4_00.Location = ptb_4_15.Location;
                        ptb_4_15.Location = t;

                        break;
                    }
            }

            btn_move.Text = move.ToString();
        }

        //
        //4x4
        //
        public void SWITCH_5x5(int a)
        {
            move++;

            switch (a)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_01.Location;
                        ptb_5_01.Location = t;

                        break;
                    }
                case 2:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_02.Location;
                        ptb_5_02.Location = t;

                        break;
                    }
                case 3:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_03.Location;
                        ptb_5_03.Location = t;

                        break;
                    }
                case 4:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_04.Location;
                        ptb_5_04.Location = t;

                        break;
                    }
                case 5:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_05.Location;
                        ptb_5_05.Location = t;

                        break;
                    }
                case 6:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_06.Location;
                        ptb_5_06.Location = t;

                        break;
                    }
                case 7:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_07.Location;
                        ptb_5_07.Location = t;

                        break;
                    }
                case 8:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_08.Location;
                        ptb_5_08.Location = t;

                        break;
                    }
                case 9:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_09.Location;
                        ptb_5_09.Location = t;

                        break;
                    }
                case 10:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_10.Location;
                        ptb_5_10.Location = t;

                        break;
                    }
                case 11:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_11.Location;
                        ptb_5_11.Location = t;

                        break;
                    }
                case 12:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_12.Location;
                        ptb_5_12.Location = t;

                        break;
                    }
                case 13:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_13.Location;
                        ptb_5_13.Location = t;

                        break;
                    }
                case 14:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_14.Location;
                        ptb_5_14.Location = t;

                        break;
                    }
                case 15:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_15.Location;
                        ptb_5_15.Location = t;

                        break;
                    }
                case 16:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_16.Location;
                        ptb_5_16.Location = t;

                        break;
                    }
                case 17:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_17.Location;
                        ptb_5_17.Location = t;

                        break;
                    }
                case 18:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_18.Location;
                        ptb_5_18.Location = t;

                        break;
                    }
                case 19:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_19.Location;
                        ptb_5_19.Location = t;

                        break;
                    }
                case 20:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_20.Location;
                        ptb_5_20.Location = t;

                        break;
                    }
                case 21:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_21.Location;
                        ptb_5_21.Location = t;

                        break;
                    }
                case 22:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_22.Location;
                        ptb_5_22.Location = t;

                        break;
                    }
                case 23:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_23.Location;
                        ptb_5_23.Location = t;

                        break;
                    }
                case 24:
                    {
                        Point t = ptb_5_00.Location;
                        ptb_5_00.Location = ptb_5_24.Location;
                        ptb_5_24.Location = t;

                        break;
                    }
            }

            btn_move.Text = move.ToString();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_up_Click(object sender, EventArgs e)       //có nghĩa ô trống hay ptb_3_00 sẽ đi xuống
        {
            time.Start();

            UP();

            if (Ending())
            {
                time.Stop();

                MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                btn_close_Click(sender, e);
            }
        }

        public void UP()
        {
            if (ptb_3_01.Visible)
            {
                n = 3;
            }
            else if (ptb_4_01.Visible)
            {
                n = 4;
            }
            else if (ptb_5_01.Visible)
            {
                n = 5;
            }

            if (n == 3)
            {
                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_3x3[5 * i + j] == 0)
                        {
                            if (pic_3x3[5 * i + (j + 1)] != 0)
                            {
                                SWITCH_3x3(pic_3x3[5 * i + (j + 1)]);     //chuyển vị hai pictureBox
                                pic_3x3[5 * i + j] = pic_3x3[5 * i + (j + 1)];
                                pic_3x3[5 * i + (j + 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 4)
            {
                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_4x4[6 * i + j] == 0)
                        {
                            if (pic_4x4[6 * i + (j + 1)] != 0)
                            {
                                SWITCH_4x4(pic_4x4[6 * i + (j + 1)]);     //chuyển vị hai pictureBox
                                pic_4x4[6 * i + j] = pic_4x4[6 * i + (j + 1)];
                                pic_4x4[6 * i + (j + 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 5)
            {
                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_5x5[7 * i + j] == 0)
                        {
                            if (pic_5x5[7 * i + (j + 1)] != 0)
                            {
                                SWITCH_5x5(pic_5x5[7 * i + (j + 1)]);     //chuyển vị hai pictureBox
                                pic_5x5[7 * i + j] = pic_5x5[7 * i + (j + 1)];
                                pic_5x5[7 * i + (j + 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_right_Click(object sender, EventArgs e)        //có nghĩa là ô trống hay ptb_3_00 dịch sang trái
        {
            time.Start();

            RIGHT();

            if (Ending())
            {
                time.Stop();

                MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                btn_close_Click(sender, e);
            }
        }

        public void RIGHT()
        {
            if (ptb_3_01.Visible)
            {
                n = 3;
            }
            else if (ptb_4_01.Visible)
            {
                n = 4;
            }
            else if (ptb_5_01.Visible)
            {
                n = 5;
            }

            if (n == 3)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_3x3[5 * i + j] == 0)
                        {
                            if (pic_3x3[5 * (i - 1) + j] != 0)
                            {
                                SWITCH_3x3(pic_3x3[5 * (i - 1) + j]);       //chuyển vị hai pictureBox
                                pic_3x3[5 * i + j] = pic_3x3[5 * (i - 1) + j];
                                pic_3x3[5 * (i - 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 4)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_4x4[6 * i + j] == 0)
                        {
                            if (pic_4x4[6 * (i - 1) + j] != 0)
                            {
                                SWITCH_4x4(pic_4x4[6 * (i - 1) + j]);       //chuyển vị hai pictureBox
                                pic_4x4[6 * i + j] = pic_4x4[6 * (i - 1) + j];
                                pic_4x4[6 * (i - 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 5)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_5x5[7 * i + j] == 0)
                        {
                            if (pic_5x5[7 * (i - 1) + j] != 0)
                            {
                                SWITCH_5x5(pic_5x5[7 * (i - 1) + j]);       //chuyển vị hai pictureBox
                                pic_5x5[7 * i + j] = pic_5x5[7 * (i - 1) + j];
                                pic_5x5[7 * (i - 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_down_Click(object sender, EventArgs e)//có nghĩa ô trống hay ptb_3_00 sẽ đi lên
        {
            time.Start();

            DOWN();

            if (Ending())
            {
                time.Stop();

                MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                btn_close_Click(sender, e);
            }
        }

        public void DOWN()
        {
            if (ptb_3_01.Visible)
            {
                n = 3;
            }
            else if (ptb_4_01.Visible)
            {
                n = 4;
            }
            else if (ptb_5_01.Visible)
            {
                n = 5;
            }

            if (n == 3)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_3x3[5 * i + j] == 0)
                        {
                            if (pic_3x3[5 * i + (j - 1)] != 0)
                            {
                                SWITCH_3x3(pic_3x3[5 * i + (j - 1)]);     //chuyển vị hai pictureBox
                                pic_3x3[5 * i + j] = pic_3x3[5 * i + (j - 1)];
                                pic_3x3[5 * i + (j - 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 4)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_4x4[6 * i + j] == 0)
                        {
                            if (pic_4x4[6 * i + (j - 1)] != 0)
                            {
                                SWITCH_4x4(pic_4x4[6 * i + (j - 1)]);     //chuyển vị hai pictureBox
                                pic_4x4[6 * i + j] = pic_4x4[6 * i + (j - 1)];
                                pic_4x4[6 * i + (j - 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 5)
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (pic_5x5[7 * i + j] == 0)
                        {
                            if (pic_5x5[7 * i + (j - 1)] != 0)
                            {
                                SWITCH_5x5(pic_5x5[7 * i + (j - 1)]);     //chuyển vị hai pictureBox
                                pic_5x5[7 * i + j] = pic_5x5[7 * i + (j - 1)];
                                pic_5x5[7 * i + (j - 1)] = 0;

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_left_Click(object sender, EventArgs e)     //có nghĩa là ô trống hay ptb_3_00 dịch sang phải
        {
            time.Start();

            LEFT();

            if (Ending())
            {
                time.Stop();

                MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                btn_close_Click(sender, e);
            }
        }

        public void LEFT()
        {
            if (ptb_3_01.Visible)
            {
                n = 3;
            }
            else if (ptb_4_01.Visible)
            {
                n = 4;
            }
            else if (ptb_5_01.Visible)
            {
                n = 5;
            }

            if (n == 3)
            {
                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_3x3[5 * i + j] == 0)
                        {
                            if (pic_3x3[5 * (i + 1) + j] != 0)
                            {
                                SWITCH_3x3(pic_3x3[5 * (i + 1) + j]);       //chuyển vị hai pictureBox
                                pic_3x3[5 * i + j] = pic_3x3[5 * (i + 1) + j];
                                pic_3x3[5 * (i + 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 4)
            {
                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_4x4[6 * i + j] == 0)
                        {
                            if (pic_4x4[6 * (i + 1) + j] != 0)
                            {
                                SWITCH_4x4(pic_4x4[6 * (i + 1) + j]);       //chuyển vị hai pictureBox
                                pic_4x4[6 * i + j] = pic_4x4[6 * (i + 1) + j];
                                pic_4x4[6 * (i + 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
            else if (n == 5)
            {

                for (int i = n; i > 0; i--)
                {
                    for (int j = n; j > 0; j--)
                    {
                        if (pic_5x5[7 * i + j] == 0)
                        {
                            if (pic_5x5[7 * (i + 1) + j] != 0)
                            {
                                SWITCH_5x5(pic_5x5[7 * (i + 1) + j]);       //chuyển vị hai pictureBox
                                pic_5x5[7 * i + j] = pic_5x5[7 * (i + 1) + j];
                                pic_5x5[7 * (i + 1) + j] = 0;

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_move_TextChanged(object sender, EventArgs e)
        {
            btn_move.Text = move.ToString();
        }

        private void btn_suffle_Click(object sender, EventArgs e)
        {
            Suffle();
        }

        private void Suffle()
        {
            Random rand = new Random();

            int k = (n - 1) * 350;

            for (int i = 0; i < k; i++)
            {
                int t = rand.Next(0, 100);

                if (t % 4 == 1) UP();
                else if (t % 4 == 2) DOWN();
                else if (t % 4 == 3) LEFT();
                else if (t % 4 == 0) RIGHT();
            }

            move = 0;
            btn_move.Text = move.ToString();
            time.Stop();
            sec = min = 0;
            btn_min.Text = min.ToString("00");
            btn_sec.Text = sec.ToString("00");
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "START")
            {
                if (n == 3)
                {
                    ReSave_3x3();
                }
                else if (n == 4)
                {
                    ReSave_4x4();
                }
                else if (n == 5)
                {
                    ReSave_5x5();
                }

                Suffle();

                btn_start.Text = "AGAIN";
            }
            else
            {
                if (n == 3)
                {
                    ReSave_3x3();
                }
                else if (n == 4)
                {
                    ReSave_4x4();
                }
                else if (n == 5)
                {
                    ReSave_5x5();
                }

                Suffle();
            }

            sec = min = 0;

            move = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                time.Start();

                UP();

                if (Ending())
                {
                    time.Stop();

                    MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                }

                return true;
            }

            if (keyData == Keys.Down)
            {
                time.Start();

                DOWN();

                return true;
            }

            if (keyData == Keys.Left)
            {
                time.Start();

                LEFT();

                if (Ending())
                {
                    time.Stop();

                    MessageBox.Show("Bạn đã hoàn thành Puzzle!\nTime: " + min.ToString("00") + ":" + (sec).ToString("00") + "\nMove: " + move.ToString());
                }

                return true;
            }

            if (keyData == Keys.Right)
            {
                time.Start();

                RIGHT();

                return true;
            }

            if (keyData == Keys.F1)
            {
                if (btn_start.Text == "START")
                {
                    ReSave_3x3();
                    Suffle();

                    btn_start.Text = "AGAIN";
                }

                else Suffle();

                sec = min = 0;

                move = 0;
            }

            if (keyData == Keys.F2)
            {
                if (btn_original.Text == "Xem ảnh gốc")
                {
                    btn_original.Text = "Trở lại";
                }
                else btn_original.Text = "Xem ảnh gốc";

                if (ptb_original.Visible == false)
                {
                    ptb_original.Visible = true;
                }
                else
                {
                    ptb_original.Visible = false;
                }
            }

            if (keyData == Keys.Escape)
            {
                this.Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void toolbar_reset_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "START")
            {
                ReSave_3x3();
                Suffle();

                btn_start.Text = "AGAIN";
            }

            else Suffle();

            sec = min = 0;

            move = 0;
        }

        private void toolbar_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void seeOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original.Text = "Trở lại";
                toolbar_SeeOriginal.Text = "Back to game";
            }
            else
            {
                btn_original.Text = "Xem ảnh gốc";
                toolbar_SeeOriginal.Text = "See Original";
            }

            if (ptb_original.Visible == false)
            {
                ptb_original.Visible = true;
            }
            else
            {
                ptb_original.Visible = false;
            }
        }

        private void toolbar_howtoplay_Click(object sender, EventArgs e)
        {
            string s = help.ReadToEnd();
            MessageBox.Show(s);
        }

        private void toolbar_zoe_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_zoe.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image1_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image1.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image2_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image2.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

        }

        private void toolbar_image3_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image3.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image5_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image5.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image4_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image4.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image6_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image6.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image7_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image7.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image8_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image8.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image9_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image9.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image10_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image10.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image11_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image11.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image12_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image12.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

            //toolbar_image12.Text = Name + ".jpg";
        }

        private void toolbar_image13_Click(object sender, EventArgs e)
        {
            imgName = "Uncen/" + toolbar_image13.Text + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
        }

        private void toolbar_image_Click(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////

        private void toolbar_3x3_Click(object sender, EventArgs e)
        {
            Visible_3x3(true);

            Visible_4x4(false);

            Visible_5x5(false);

            n = 3;

            Suffle();

            btn_start.Text = "START";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

            move = 0;
            sec = min = 0;
            time.Stop();
        }

        private void toolbar_4x4_Click(object sender, EventArgs e)
        {
            Visible_3x3(false);

            Visible_4x4(true);

            Visible_5x5(false);

            n = 4;

            Suffle();

            btn_start.Text = "START";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

            move = 0;
            sec = min = 0;
            time.Stop();
        }

        private void toolbar_5x5_Click(object sender, EventArgs e)
        {
            Visible_3x3(false);

            Visible_4x4(false);

            Visible_5x5(true);

            n = 5;

            Suffle();

            btn_start.Text = "START";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

            move = 0;
            sec = min = 0;
            time.Stop();
        }

        //hàm Visiable

        protected void Visible_3x3(bool k)
        {
            ptb_3_01.Visible = ptb_3_02.Visible = ptb_3_03.Visible = ptb_3_04.Visible =
            ptb_3_05.Visible = ptb_3_06.Visible = ptb_3_07.Visible = ptb_3_08.Visible = k;
        }

        protected void Visible_4x4(bool k)
        {
            ptb_4_01.Visible = ptb_4_02.Visible = ptb_4_03.Visible = ptb_4_04.Visible =
            ptb_4_05.Visible = ptb_4_06.Visible = ptb_4_07.Visible = ptb_4_08.Visible =
            ptb_4_09.Visible = ptb_4_10.Visible = ptb_4_11.Visible = ptb_4_12.Visible =
            ptb_4_13.Visible = ptb_4_14.Visible = ptb_4_15.Visible = k;
        }

        protected void Visible_5x5(bool k)
        {
            ptb_5_01.Visible = ptb_5_02.Visible = ptb_5_03.Visible = ptb_5_04.Visible =
            ptb_5_05.Visible = ptb_5_06.Visible = ptb_5_07.Visible = ptb_5_08.Visible =
            ptb_5_09.Visible = ptb_5_10.Visible = ptb_5_11.Visible = ptb_5_12.Visible =
            ptb_5_13.Visible = ptb_5_14.Visible = ptb_5_15.Visible = ptb_5_16.Visible =
            ptb_5_17.Visible = ptb_5_18.Visible = ptb_5_19.Visible = ptb_5_20.Visible =
            ptb_5_21.Visible = ptb_5_22.Visible = ptb_5_23.Visible = ptb_5_24.Visible = k;
        }

        private void toolbar_choose_Click(object sender, EventArgs e)
        {
            grb_choose.Visible = true;
        }

        private void ptb_choose_Paint(object sender, PaintEventArgs e)
        {
            PictureBox ptb=sender as PictureBox;

            string name = "Uncen/" + ptb.Name.ToString() + ".jpg";

            var a = Image.FromFile(name);
            var b = resizeImage(a, 75, 75);

            e.Graphics.DrawImage(b, 0, 0);
        }

        private void ptb_choose_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;

            imgName = "Uncen/" + ptb.Name.ToString() + ".jpg";

            if (btn_original.Text == "Xem ảnh gốc")
            {
                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }
            else
            {
                btn_original_Click(sender, e);

                btn_original_Click(sender, e);

                btn_original.Text = "START";
            }

            grb_choose.Visible = false;
        }

        private void grb_btn_back_Click(object sender, EventArgs e)
        {
            grb_choose.Visible = false;
        }

        //hàm ending
        public bool Ending()
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (n * (i - 1) + j < n * n)
                    {
                        if (n == 3)
                        {
                            if (pic_3x3[5 * i + j] != 3 * (i - 1) + j)
                            {
                                return false;
                            }
                        }
                        else if (n == 4)
                        {
                            if (pic_4x4[6 * i + j] != n * (i - 1) + j)
                            {
                                return false;
                            }
                        }
                        else if (n == 5)
                        {
                            if (pic_5x5[7 * i + j] != n * (i - 1) + j)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
