using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace ASLKeyboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Custom label font
            InitCustomLabelFont();
        }

        private void InitCustomLabelFont()
        {
            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.ASLWrite1.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.ASLWrite1;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            System.Drawing.Font aslWriteFontImg = new System.Drawing.Font(pfc.Families[0], 80);
            System.Drawing.Font aslWriteFontNormal = new System.Drawing.Font(pfc.Families[0], 20);

            label2.Font = aslWriteFontNormal;
            pictureBox1.Parent = pictureBox2;
            //pictureBox1.Size = label2.Size;
            //pictureBox1.Location = new Point(label2.Location.X + 200, label2.Location.Y + 10);

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                //float w = pictureBox1.Image.Width / 2f + 50;
                //float h = pictureBox1.Image.Height / 2f;
                float w = 0;
                float h = 0;

                g.TranslateTransform(w, h);
                g.RotateTransform(0);
                g.ScaleTransform(1, 1);

                SizeF size = g.MeasureString("A", aslWriteFontImg);
                //PointF drawPoint = new PointF(-size.Width / 2f, -size.Height / 2f);
                PointF drawPoint = new PointF(0, 0);
                g.DrawString("A", aslWriteFontImg, Brushes.Black, drawPoint);
            }
            pictureBox1.Refresh();
            //pictureBox1.Parent = label2;
            
            pictureBox2.Image = MergePictureboxes(pictureBox2, pictureBox1);
            pictureBox1.Location = new Point(100, 50);
        }

        private Bitmap MergePictureboxes(PictureBox pb1, PictureBox pb2)
        {
            Bitmap bmp1 = new Bitmap(pb1.Image);
            Bitmap bmp2 = new Bitmap(pb2.Image);

            Bitmap result = new Bitmap(Math.Max(bmp1.Width, bmp2.Width),
                                       Math.Max(bmp1.Height, bmp2.Height));
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp2, Point.Empty);
                g.DrawImage(bmp1, Point.Empty);
            }
            return result;
        }

    }
}
