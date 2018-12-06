using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_P3_ColorReduction
{
    public partial class Form1 : Form
    {
        Image MainImage;

        static double[,] D2 = new double[2, 2]
            {
                { 0, 2d/4 },
                { 3d/4, 1d/4 }
            };
        static double[,] D3 = new double[3, 3]
            {
                { 6/9, 8/9, 4/9 },
                { 1/9, 0/9, 3/9 },
                { 5/9, 2/9, 7/9 }
            };
        
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
        
        
        public int DoNotOverflow(int n)
        {
            if (n > 255) return 255;
            if (n < 0) return 0;
            return n;
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            ResultForm rf = new ResultForm();
            Image image = WorkImagePB.Image;

            switch (AlgorithmCB.Text)
            {
                case "Rozproszenie średnie":
                    image = AverageDithering(WorkImagePB.Image, (int)KNUD.Value);
                    KNUD.Value = 2;
                    break;

                case "Uporządkowane drżenie (losowe)":
                    image = OrderedDitheringRandom(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value, true);
                    break;

                case "Uporządkowane drżenie (pozycja piksela)":
                    image = OrderedDitheringRandom(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value, false);
                    break;

                case "Propagacja błędu":
                    image = ErrorPropagation(WorkImagePB.Image, (int)KNUD.Value);
                    break;

                case "Algorytm popularnościowy":
                    image = Populistic(WorkImagePB.Image, (int)KNUD.Value);
                    break;

                default:
                    MessageBox.Show("Nierozpoznana nazwa algorytmu.", "Czy nastanie znów świt?", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                    
            }

            rf.SetImage(ScaleImage(image, rf.ResultPB.Width, rf.ResultPB.Height));
            if(AlgorithmCB.Text == "Rozproszenie średnie")
                rf.Text = AlgorithmCB.Text + ", K: " + ((int)KNUD.Value).ToString();
            else
            rf.Text = AlgorithmCB.Text + ", Kr: " + ((int)KRNUD.Value).ToString() + ", Kg: " + ((int)KGNUD.Value).ToString() + ", Kb: " + ((int)KBNUD.Value).ToString();
            rf.Show();
        }
        
        public Image AverageDithering(Image image, int K)
        {
            Bitmap bitmap = new Bitmap(image);
            float brightness = 0, treshold;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    brightness += bitmap.GetPixel(i, j).GetBrightness();
                }
            }
            treshold = brightness / (bitmap.Width * bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    brightness = bitmap.GetPixel(i, j).GetBrightness();
                    if (brightness < treshold)
                        bitmap.SetPixel(i, j, Color.Black);
                    else
                        bitmap.SetPixel(i, j, Color.White);
                }
            }

            return bitmap;
        }
        public Image OrderedDitheringRandom(Image image, int Kr, int Kg, int Kb, bool isRandom)
        {
            Bitmap bitmap = new Bitmap(image);
            Random r = new Random();
            int xR, xG, xB, yR, yG, yB;

            double nRTemp = 16 * Math.Pow(Kr - 1, -1d / 2);
            int nR = (int)nRTemp + 1;
            double[,] DR = CalcD(nR);
            nR = DR.GetLength(0);

            double nGTemp = 16 * Math.Pow(Kg - 1, -1d / 2);
            int nG = (int)nGTemp + 1;
            double[,] DG = CalcD(nG);
            nG = DG.GetLength(0);

            double nBTemp = 16 * Math.Pow(Kb - 1, -1d / 2);
            int nB = (int)nBTemp + 1;
            double[,] DB = CalcD(nB);
            nB = DB.GetLength(0);

            Color color;
            
            //double  reR, reG, reB;
            double coef = 256d / 255;
            int R, G, B;

            int[] tresholdsR = new int[Kr];
            int[] tresholdsG = new int[Kg];
            int[] tresholdsB = new int[Kb];

            tresholdsR[0] = 0;
            tresholdsG[0] = 0;
            tresholdsB[0] = 0;

            for (int i = 1; i < Kr; i++)
                tresholdsR[i] = (int)(i * (256d / (Kr - 1)) - 1);

            for (int i = 1; i < Kg; i++)
                tresholdsG[i] = (int)(i * (256d / (Kg - 1)) - 1);

            for (int i = 1; i < Kb; i++)
                tresholdsB[i] = (int)(i * (256d / (Kb - 1)) - 1);


            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);
                    if (isRandom)
                    {
                        xR = r.Next(nR);
                        yR = r.Next(nR);
                        xG = r.Next(nG);
                        yG = r.Next(nG);
                        xB = r.Next(nB);
                        yB = r.Next(nB);
                    }
                    else
                    {
                        xR = i % nR;
                        yR = j % nR;
                        xG = i % nG;
                        yG = j % nG;
                        xB = i % nB;
                        yB = j % nB;
                    }

                    // To na pewno uporządkowane drżenia?
                    //R = (int)(color.R + coef * 256 / (Kr - 1) * (DR[xR, yR] - 1d / 2));
                    //G = (int)(color.G + coef * 256 / (Kg - 1) * (DG[xG, yG] - 1d / 2));
                    //B = (int)(color.B + coef * 256 / (Kb - 1) * (DB[xB, yB] - 1d / 2));

                    R = DoNotOverflow((int)(color.R + coef * 256 / (Kr - 1) * (DR[xR, yR])));
                    G = DoNotOverflow((int)(color.G + coef * 256 / (Kg - 1) * (DG[xG, yG])));
                    B = DoNotOverflow((int)(color.B + coef * 256 / (Kb - 1) * (DB[xB, yB])));


                    R = ClosestColor(R, tresholdsR);
                    G = ClosestColor(G, tresholdsG);
                    B = ClosestColor(B, tresholdsB);
                    
                    //B = (int)((double)(K - 1) * color.B / (n * n));

                    //reR = color.R % (n * n);
                    //reG = color.G % (n * n);
                    //reB = color.B % (n * n);

                    //// To tylko dla K = 2
                    //if (reR > D[x, y] && R < K - 1) R += 1;
                    //if (reG > D[x, y] && G < K - 1) G += 1;
                    //if (reB > D[x, y] && B < K - 1) B += 1;

                    bitmap.SetPixel(i, j, Color.FromArgb(R,G,B));

                }
            }
            return bitmap;
        }
        public Image OrderedDitheringPixelPosition(Image image, int K)
        {
            Bitmap bitmap = new Bitmap(image);

            //DO_YOUR_JOB

            return bitmap;
        }
        public Image ErrorPropagation(Image image, int K)
        {
            Bitmap bitmap = new Bitmap(image);

            //DO_YOUR_JOB

            return bitmap;
        }
        public Image Populistic(Image image, int K)
        {
            Bitmap bitmap = new Bitmap(image);

            //DO_YOUR_JOB

            return bitmap;
        }

        public double[,] CalcOrdMat(double[,] D)
        {
            int n = D.GetLength(0);
            double[,] D2 = new double[2*n, 2*n];

            //Pierwsza ćwiartka
            for (int i = 0; i < 2 * n; i++)
                for (int j = 0; j < 2 * n; j++)
                {
                    D2[i, j] = 1d / (4 * n * n) * (4 * n * n * D[i % n, j % n]);
                }

            //Druga ćwiartka
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    D2[i + n, j] = 1d / (4 * n * n) * (4 * n * n * D[i % n, j % n] + 2);
                }

            //Trzecia ćwiartka
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    D2[i, j + n] = 1d / (4 * n * n) * (4 * n * n * D[i % n, j % n] + 3);
                }

            //Czwarta ćwiartka
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    D2[i + n, j + n] = 1d / (4 * n * n) * (4 * n * n * D[i % n, j % n] + 1);
                }

            return D2;
        }

        public int ClosestColor(int color, int[] tresholds)
        {
            float x = color / (tresholds[1]-tresholds[0]);

            return tresholds[(int)Math.Round(x)];
        }

        public Bitmap OpenImage()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "All files (*.*)|*.*";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return new Bitmap(dlg.FileName);
                }
            }
            return new Bitmap(WorkImagePB.Image);
        }

        public double[,] CalcD(int n)
        {
            double[,] D;

            if (n < 2)
            {
                n = 2;
                D = D2;
            }
            else if (n == 3)
            {
                D = D3;
            }
            else if (n == 4)
            {
                D = CalcOrdMat(D2);
            }
            else if (n < 6)
            {
                n = 6;
                D = CalcOrdMat(D3);
            }
            else if (n < 8)
            {
                n = 8;
                D = CalcOrdMat(CalcOrdMat(D2));
            }
            else if (n < 12)
            {
                n = 12;
                D = CalcOrdMat(CalcOrdMat(D3));
            }
            else
            {
                n = 16;
                D = CalcOrdMat(CalcOrdMat(CalcOrdMat(D2)));
            }
            return D;
        }


        private void LoadImageBtn_Click(object sender, EventArgs e)
        {
            WorkImagePB.Image = ScaleImage(OpenImage(), WorkImagePB.Width, WorkImagePB.Height);
        }
    }


}
