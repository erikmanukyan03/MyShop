using System.Drawing;
using System.Drawing.Imaging;

namespace MyShop.Captcha
{
    public class CaptchaImage
    {
        private string Text { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        public Bitmap Image { get; set; }

        public CaptchaImage(string txt,int width,int height)
        {
            Text=txt;
            Width=width;
            Height=height;
            GenereteImage();
        }
        private void GenereteImage()
        {
            Bitmap bitmap = new Bitmap(Width,Height,PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawString(Text, new Font("Arial", Height / 2, FontStyle.Bold), Brushes.White, new Rectangle(0, 0, Width, Height));
            g.Dispose();
            Image = bitmap;
        }
        ~CaptchaImage() 
        {
            Dispose(false);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Image.Dispose();
            }
        }
        

    }

}
