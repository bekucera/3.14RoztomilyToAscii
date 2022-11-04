using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PicuteToAscii
{
    internal class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image (.jpg,png)|*.png;*.jpg";
            openFileDialog.ShowDialog();
            var imageURL = openFileDialog.FileName;
            Bitmap image = new Bitmap(imageURL);

            StringBuilder stringBuilder = new StringBuilder();
            string dark = "██";
            string mediumDark = "▓▓";
            string medium = "▒▒";
            string mediumLight = "░░";
            string light = "  ";

            int scale = image.Width / 50;

            for (int y = 0; y < image.Height; y += scale)
            {
                for (int x = 0; x < image.Width; x += scale)
                {
                    int b = Convert.ToInt32(image.GetPixel(x, y).GetBrightness() * 100F);

                    if (b <= 20)
                        stringBuilder.Append(dark);
                    else
                    if (b <= 40)
                        stringBuilder.Append(mediumDark);
                    else
                    if (b <= 60)
                        stringBuilder.Append(medium);
                    else
                    if (b < 80)
                        stringBuilder.Append(mediumLight);
                    else
                        stringBuilder.Append(light);
                }
                stringBuilder.AppendLine();
            }

            File.WriteAllText("file.txt",stringBuilder.ToString());
            Process.Start("notepad.exe","file.txt");
        }
    }
}
