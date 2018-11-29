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

namespace GK_P3_ColorReduction
{
    public partial class Form1 : Form
    {
        Image MainImage;
        
        public Form1()
        {
            InitializeComponent();
            
            MainImage = new Bitmap(ScaleImage(Properties.Resources.paris_eiffel, WorkImagePB.Width, WorkImagePB.Height));
            WorkImagePB.Image = new Bitmap(MainImage);
            
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        
        public Image Treshold(Image image)
        {
            Bitmap tres = new Bitmap(image);
            float brightness, treshold = 0.4f;
            for (int i = 0; i < tres.Width; i++)
            {
                for (int j = 0; j < tres.Height; j++)
                {
                    brightness = tres.GetPixel(i, j).GetBrightness();
                    if (brightness < treshold)
                        tres.SetPixel(i, j, Color.Black);
                    else
                        tres.SetPixel(i, j, Color.White);
                }
            }
            
            return tres;
        }
        
        public int DoNotOverflow(int n)
        {
            if (n > 255) return 255;
            if (n < 0) return 0;
            return n;
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            ResultForm rf = new ResultForm();
            rf.Show();
        }
    }


}
