using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Henry.Common
{
    public enum VerifyCodeType
    {
        /// <summary>
        /// 数字和字母验证码
        /// </summary>
        NumberAndCharacters,
        /// <summary>
        /// 中文验证码
        /// </summary>
        Chinese
    }
    public class ImageHelper
    {

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="type">验证码类别</param>
        /// <param name="codeLength">验证码长度</param>
        /// <returns></returns>
        private static string GetRandomStr(VerifyCodeType type, int codeLength)
        {

            string chars = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] charsArray = chars.Split(',');
            string checkCode = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                rand = new Random(unchecked((int)DateTime.Now.Ticks));//为了得到不同的随机序列
                int t = rand.Next(charsArray.Length);// The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero，下标从0开始
                checkCode += charsArray[t];
            }
            return checkCode;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static Bitmap GetVerificationCode(int length = 4, VerifyCodeType type = VerifyCodeType.NumberAndCharacters)
        {
            int step = 0;
            if (type == VerifyCodeType.Chinese)
            {
                step = 5;//中文字符比较大，所以字距要比较大
            }
            var code = GetRandomStr(type, length);
            int iWidth = (int)(code.Length * (13 + step));
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iWidth, 22);
            Graphics g = Graphics.FromImage(image);

            g.Clear(Color.White);//清除背景色

            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };//定义随机颜色

            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                int x1 = rand.Next(image.Width);
                int x2 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int y2 = rand.Next(image.Height);
                g.DrawLine(new Pen(Color.LightGray, 1), x1, y1, x2, y2);//根据坐标画线
            }

            for (int i = 0; i < code.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);

                Font f = new System.Drawing.Font(font[findex], 10, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 3 + (i * (12 + step)), ii);
            }

            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            return image;

        }
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <returns></returns>
        public static void ResizeAndSavePic(System.Drawing.Image imgPhoto, string filename, string filepath,bool dependOnHeight=true)
        {
            int maxWidth = 500;   //图片宽度最大限制
            int maxHeight = 60;  //图片高度最大限制
            int imgWidth = imgPhoto.Width;
            int imgHeight = imgPhoto.Height;
            float toImgWidth = imgWidth, toImgHeight = imgHeight;
            if (dependOnHeight)
            {

                toImgHeight =imgHeight>maxHeight ?maxHeight:imgHeight;
                toImgWidth = (float)(imgWidth * toImgHeight / imgHeight);
            }
            else {
                toImgWidth = imgWidth > maxWidth ? maxWidth : imgWidth;
                toImgHeight = (float)(imgHeight * toImgWidth / imgWidth);
            }
            
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto,(int)toImgWidth,(int)toImgHeight);
            img.Save(filepath + filename);  //保存压缩后的图片
        }

        public static Bitmap ResizePic(System.Drawing.Image imgPhoto, int size,int percent, bool dependOnHeight = true)
        {

            int width=0;
            int height=0;
            if (dependOnHeight)
            {
                height = size*percent/100;
                width = size * percent * imgPhoto.Width / (100 * imgPhoto.Height);
            }
            else
            {
                width = size*percent/100;;
                height = size * percent * imgPhoto.Height / (100 * imgPhoto.Width);
            }
            var previewImg= new System.Drawing.Bitmap(imgPhoto,width,height);
            return ImageCopy(previewImg, 0, 0, (width - size) / 2, (height - size) / 2, size, size, width, height);
        }

        public static Bitmap ImageCopy(Bitmap srcBitmap, float dst_x, float dst_y, float src_x, float src_y, float dst_width, float dst_height, float src_width, float src_height)
        {
            // Create the new bitmap and associated graphics object
            RectangleF SourceRec = new RectangleF(src_x, src_y, dst_width, dst_height);
            RectangleF DestRec = new RectangleF(dst_x, dst_y, dst_width, dst_height);
            Bitmap bmp = new Bitmap(Convert.ToInt32(dst_width), Convert.ToInt32(dst_height));
            Graphics g = Graphics.FromImage(bmp);
            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, DestRec, SourceRec, GraphicsUnit.Pixel);
            // Clean up
            g.Dispose();
            // Return the bitmap
            return bmp;

        }
    }
}
