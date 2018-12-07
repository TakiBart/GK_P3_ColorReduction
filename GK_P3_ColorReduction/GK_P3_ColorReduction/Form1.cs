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
                { 6d/9, 8d/9, 4d/9 },
                { 1d/9, 0d/9, 3d/9 },
                { 5d/9, 2d/9, 7d/9 }
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
                    image = AverageDithering(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value);
                    KNUD.Value = 2;
                    break;

                case "Uporządkowane drżenie (losowe)":
                    image = OrderedDithering(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value, true);
                    break;

                case "Uporządkowane drżenie (pozycja piksela)":
                    image = OrderedDithering(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value, false);
                    break;

                case "Propagacja błędu":
                    image = ErrorPropagation(WorkImagePB.Image, (int)KRNUD.Value, (int)KGNUD.Value, (int)KBNUD.Value);
                    break;

                case "Algorytm popularnościowy":
                    image = Populistic(WorkImagePB.Image, (int)KNUD.Value);
                    break;

                default:
                    MessageBox.Show("Nierozpoznana nazwa algorytmu.", "Czy nastanie znów świt?", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                    
            }

            rf.SetImage(ScaleImage(image, rf.ResultPB.Width, rf.ResultPB.Height));
            if(AlgorithmCB.Text == "Algorytm popularnościowy")
                rf.Text = AlgorithmCB.Text + ", K: " + ((int)KNUD.Value).ToString();
            else
            rf.Text = AlgorithmCB.Text + ", Kr: " + ((int)KRNUD.Value).ToString() + ", Kg: " + ((int)KGNUD.Value).ToString() + ", Kb: " + ((int)KBNUD.Value).ToString();
            rf.Show();
        }
        
        public Image AverageDithering(Image image, int Kr, int Kg, int Kb)
        {
            Bitmap bitmap = new Bitmap(image);

            //float brightness = 0, treshold;
            //for (int i = 0; i < bitmap.Width; i++)
            //{
            //    for (int j = 0; j < bitmap.Height; j++)
            //    {
            //        brightness += bitmap.GetPixel(i, j).GetBrightness();
            //    }
            //}
            //treshold = brightness / (bitmap.Width * bitmap.Height);

            //for (int i = 0; i < bitmap.Width; i++)
            //{
            //    for (int j = 0; j < bitmap.Height; j++)
            //    {
            //        brightness = bitmap.GetPixel(i, j).GetBrightness();
            //        if (brightness < treshold)
            //            bitmap.SetPixel(i, j, Color.Black);
            //        else
            //            bitmap.SetPixel(i, j, Color.White);
            //    }
            //}

            Color color;
            int iR, iG, iB;
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

                    iR = (int)Math.Round((double)color.R / (tresholdsR[1] - tresholdsR[0]));
                    iG = (int)Math.Round((double)color.G / (tresholdsG[1] - tresholdsG[0]));
                    iB = (int)Math.Round((double)color.B / (tresholdsB[1] - tresholdsB[0]));

                    bitmap.SetPixel(i, j, Color.FromArgb(tresholdsR[iR], tresholdsG[iG], tresholdsB[iB]));
                }
            }
            
            return bitmap;
        }

        public Image OrderedDithering(Image image, int Kr, int Kg, int Kb, bool isRandom)
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
            
            int iR, iG, iB;

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

                    // iR, iG, iB przetrzymują indeks koloru w tresholds
                    iR = color.R / (tresholdsR[1] - tresholdsR[0]);
                    iG = color.G / (tresholdsG[1] - tresholdsG[0]);
                    iB = color.B / (tresholdsB[1] - tresholdsB[0]);

                    if ((color.R - tresholdsR[iR]) / (256d / (Kr - 1)) > DR[xR, yR])
                        iR++;
                    if ((color.G - tresholdsG[iG]) / (256d / (Kg - 1d)) > DG[xG, yG])
                        iG++;
                    if ((color.B - tresholdsB[iB]) / (256d / (Kb - 1d)) > DB[xB, yB])
                        iB++;

                    bitmap.SetPixel(i, j, Color.FromArgb(tresholdsR[iR], tresholdsG[iG], tresholdsB[iB]));
                }
            }
            return bitmap;
        }
        
        public Image ErrorPropagation(Image image, int Kr, int Kg, int Kb)
        {
            Bitmap bitmap = new Bitmap(image);

            double[,] FloydSteinberg = new double[,]
            {
                {0,     0,     0     },
                {0,     0,     7d/16 },
                {3d/16, 5d/16, 1d/16 }
            };

            double[,] Burkes = new double[,]
            {
                {0,     0,     0,     0,     0     },
                {0,     0,     0,     8d/32, 4d/32 },
                {2d/32, 4d/32, 8d/32, 4d/32, 2d/32 }
            };

            double[,] Stucky = new double[,]
            {
                {0,     0,     0,     0,     0     },
                {0,     0,     0,     0,     0     },
                {0,     0,     0,     8d/42, 4d/42 },
                {2d/42, 4d/42, 8d/42, 4d/42, 2d/42 },
                {1d/42, 2d/42, 4d/42, 2d/42, 1d/42 }
            };

            double[,] filter;

            if (FloydSteinbergRB.Checked)
                filter = FloydSteinberg;
            else if (BurkesRB.Checked)
                filter = Burkes;
            else
                filter = Stucky;

            double[,] propagationR = new double[bitmap.Width, bitmap.Height];
            double[,] propagationG = new double[bitmap.Width, bitmap.Height];
            double[,] propagationB = new double[bitmap.Width, bitmap.Height];

            for (int i = 0; i < propagationR.GetLength(0); i++)
                for (int j = 0; j < propagationR.GetLength(1); j++)
                {
                    propagationR[i, j] = 0;
                    propagationG[i, j] = 0;
                    propagationB[i, j] = 0;
                }

            Color color;
            double erR, erG, erB;

            int iR, iG, iB;
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

                    iR = (int)Math.Round((double)(color.R + propagationR[i, j]) / (tresholdsR[1] - tresholdsR[0]));
                    iG = (int)Math.Round((double)(color.G + propagationG[i, j]) / (tresholdsG[1] - tresholdsG[0]));
                    iB = (int)Math.Round((double)(color.B + propagationB[i, j]) / (tresholdsB[1] - tresholdsB[0]));

                    if (iR >= tresholdsR.Length)
                        iR = tresholdsR.Length - 1;
                    else if (iR < 0)
                        iR = 0;

                    if (iG >= tresholdsG.Length)
                        iG = tresholdsG.Length - 1;
                    else if (iG < 0)
                        iG = 0;

                    if (iB >= tresholdsB.Length)
                        iB = tresholdsB.Length - 1;
                    else if (iB < 0)
                        iB = 0;


                    erR = color.R + propagationR[i,j] - tresholdsR[iR];
                    erG = color.G + propagationG[i,j] - tresholdsG[iG];
                    erB = color.B + propagationB[i,j] - tresholdsB[iB];

                    for (int x = 0; x < filter.GetLength(0) && i + x < bitmap.Width; x++)
                        for (int y = 0; y < filter.GetLength(1) && j + y < bitmap.Height; y++)
                        {
                            propagationR[i + x, j + y] += filter[x, y] * erR;
                            propagationG[i + x, j + y] += filter[x, y] * erG;
                            propagationB[i + x, j + y] += filter[x, y] * erB;

                        }
                    bitmap.SetPixel(i, j, Color.FromArgb(tresholdsR[iR], tresholdsG[iG], tresholdsB[iB]));
                }
            }

            return bitmap;
        }

        public Image Populistic(Image image, int K)
        {
            Bitmap bitmap = new Bitmap(image);
            Color color;

            Dictionary<Color, int> colors = new Dictionary<Color, int>(256 * 256 * 256);

            for (int r = 0; r < 256; r++)
                for (int g = 0; g < 256; g++)
                    for (int b = 0; b < 256; b++)
                        colors.Add(Color.FromArgb(r, g, b), 0);

            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);
                    colors[Color.FromArgb(color.R, color.G, color.B)]++;
                }
            
            Color[] tresholds = new Color[K];
            
            for (int i = 0; i < K; i++)
            {
                tresholds[i] = colors.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; 
                colors[tresholds[i]] = 0;
            }

            int iMin;
            double min, temp;

            for(int i =0;i<bitmap.Width;i++)
                for(int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);
                    iMin = 0;
                    min = Math.Sqrt((color.R - tresholds[0].R) * (color.R - tresholds[0].R) + (color.G - tresholds[0].G) * (color.G - tresholds[0].G) + (color.B - tresholds[0].B) * (color.B - tresholds[0].B));
                    for(int k = 1; k< K; k++)
                    {
                        temp = Math.Sqrt((color.R - tresholds[k].R) * (color.R - tresholds[k].R) + (color.G - tresholds[k].G) * (color.G - tresholds[k].G) + (color.B - tresholds[k].B) * (color.B - tresholds[k].B));
                        if (temp < min)
                        {
                            min = temp;
                            iMin = k;
                        }
                    }
                    bitmap.SetPixel(i, j, tresholds[iMin]);
                }

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

        private void LoadImageBtn_Click(object sender, EventArgs e)
        {
            WorkImagePB.Image = ScaleImage(OpenImage(), WorkImagePB.Width, WorkImagePB.Height);
        }
    }


}
