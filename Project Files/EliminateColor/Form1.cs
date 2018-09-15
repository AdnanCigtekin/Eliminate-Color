/*
    This project is made by Adnan Çığtekin and published to GitHub under MIT License.
 */


using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;



namespace EliminateColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path1 = "";
        Bitmap myBmp;
   
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        void ProcessFiles(string path,bool flag,string outputPath)
        {
           
            string[] files;
            string[] directories;

            if (path[path.Length - 1] != '/')
            {
                path += "/";
            }

            files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string Tempfile = Path.GetFileName(file);

                myBmp = new Bitmap(path + Tempfile);
                pictureBox1.Image = myBmp;
                myBmp.MakeTransparent(Color.FromArgb(255, 0, 255));
                Rectangle rect = new Rectangle(0, 0, myBmp.Width, myBmp.Height);
                
                Bitmap output = myBmp.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                pictureBox1.Image = myBmp;

                string newoutputPath;
                newoutputPath = outputPath +"/";                
                string newFileName = newoutputPath + Tempfile;

                myBmp.Save(newFileName, ImageFormat.Png);
            }

            directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                // Process each directory recursively
                string folderName = "";
                for(int i = directory.Length -1; directory[i] != '/'; i--)
                {
                    folderName += directory[i];
                }
                folderName = Reverse(folderName);
                System.IO.Directory.CreateDirectory(outputPath + folderName);
                ProcessFiles(directory,true, outputPath + folderName);
            }
        }

        string GlobalOutputPath = "";

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (path1.Equals(""))
            {
                MessageBox.Show("Please select an input folder");
                return;
            }
            if (GlobalOutputPath.Equals(""))
            {
                MessageBox.Show("Please select an output folder");
                return;
            }

            ProcessFiles(path1,false, GlobalOutputPath);
            Console.WriteLine("FINISHED");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = myBmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string sr = folderBrowserDialog1.SelectedPath.ToString();
                
                textBox1.Text = sr;
                //MessageBox.Show("Output has been entered by the user");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string sr = folderBrowserDialog1.SelectedPath.ToString();
                textBox2.Text = sr;
                path1 = sr;
                //MessageBox.Show("Input has been entered by the user");

            }
        }

        Color colorToBeRemoved= new Color();
        Bitmap ImcolorToBeRemoved;
        private void button4_Click(object sender, EventArgs e)
        {
            ImcolorToBeRemoved = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colorToBeRemoved = colorDialog1.Color;
                for (int x = 0; x < ImcolorToBeRemoved.Width; x++)
                {
                    for (int y = 0; y < ImcolorToBeRemoved.Height; y++)
                    {
                        ImcolorToBeRemoved.SetPixel(x, y, colorToBeRemoved);
                    }
                }
                label4.Visible = false;
            }
           
            pictureBox2.Image = ImcolorToBeRemoved;
        }
    }
}
