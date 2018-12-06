using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_P3_ColorReduction
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();


            this.Text = "Hello there";
        }

        public ResultForm(Image image, int K, string algName)
        {
            InitializeComponent();

            ResultPB.Image = image;
            this.Text = algName + ", K = " + K.ToString();
        }

        public void SetImage(Image image)
        {
            ResultPB.Image = image;
        }
        
    }
}
