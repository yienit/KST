using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace NuiLib
{
    /// <summary>
    /// 控件渲染核心类
    /// </summary>
    public class Render
    {
        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            IntPtr hRgn = Win32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            Win32.SetWindowRgn(form.Handle, hRgn, true);
            Win32.DeleteObject(hRgn);
        }

        /// <summary>
        /// 利用九宫图绘制图像
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="img">所需绘制的图片</param>
        /// <param name="destRect">目标矩形</param>
        /// <param name="srcRect">来源矩形</param>
        public static void DrawNineRectEx(Graphics g, Image img, Rectangle destRect, Rectangle srcRect)
        {
            int offset = 5;
            Rectangle NineRect = new Rectangle(offset, offset, srcRect.Width - 2 * offset, srcRect.Height - 2 * offset);

            int destX = 0, destY = 0, destWidth, destHeight;
            int srcX = 0, srcY = 0, srcWidth, srcHeight;

            // 左上-------------------------------------;
            destX = destRect.Left; destY = destRect.Top; destWidth = offset; destHeight = offset;
            srcX = srcRect.Left; srcY = srcRect.Top; srcWidth = offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 右上-------------------------------------;
            destX = destRect.Right - offset; destY = destRect.Top; destWidth = offset; destHeight = offset;
            srcX = srcRect.Right - offset; srcY = srcRect.Top; srcWidth = offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 右下-------------------------------------;
            destX = destRect.Right - offset; destY = destRect.Bottom - offset; destWidth = offset; destHeight = offset;
            srcX = srcRect.Right - offset; srcY = srcRect.Bottom - offset; srcWidth = offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 左下-------------------------------------;
            destX = destRect.Left; destY = destRect.Bottom - offset; destWidth = offset; destHeight = offset;
            srcX = srcRect.Left; srcY = srcRect.Bottom - offset; srcWidth = offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 上-------------------------------------;
            destX = destRect.Left + offset; destY = destRect.Top; destWidth = destRect.Width - 2 * offset; destHeight = offset;
            srcX = srcRect.Left + offset; srcY = srcRect.Top; srcWidth = srcRect.Width - 2 * offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 右-------------------------------------;
            destX = destRect.Right - offset; destY = destRect.Top + offset; destWidth = offset; destHeight = destRect.Height - 2 * offset;
            srcX = srcRect.Right - offset; srcY = srcRect.Top + offset; srcWidth = offset; srcHeight = srcRect.Height - 2 * offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 右-------------------------------------;
            destX = destRect.Left + offset; destY = destRect.Bottom - offset; destWidth = destRect.Width - 2 * offset; destHeight = offset;
            srcX = srcRect.Left + offset; srcY = srcRect.Bottom - offset; srcWidth = srcRect.Width - 2 * offset; srcHeight = offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 左-------------------------------------;
            destX = destRect.Left; destY = offset; destWidth = offset; destHeight = destRect.Height - 2 * offset;
            srcX = srcRect.Left; srcY = offset; srcWidth = offset; srcHeight = srcRect.Height - 2 * offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

            // 中间-------------------------------------;
            destX = destRect.Left + offset; destY = destRect.Top + offset; destWidth = destRect.Width - 2 * offset; destHeight = destRect.Height - 2 * offset;
            srcX = srcRect.Left + offset; srcY = srcRect.Top + offset; srcWidth = srcRect.Width - 2 * offset; srcHeight = srcRect.Height - 2 * offset;
            g.DrawImage(img, new Rectangle(destX, destY, destWidth, destHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);

        }

        /// <summary>
        /// 利用九宫图绘制图像
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="img">所需绘制的图片</param>
        /// <param name="destRect">目标矩形</param>
        /// <param name="srcRect">来源矩形</param>
        public static void DrawNineRect(Graphics g, Image img, Rectangle destRect, Rectangle srcRect)
        {
            int offset = 5;
            Rectangle NineRect = new Rectangle(offset, offset, srcRect.Width - 2 * offset, srcRect.Height - 2 * offset);

            int xDest = 0, yDest = 0, widthDest, heightDest;
            int xSrc = 0, ySrc = 0, widthSrc, heightSrc;

            int nDestWidth = destRect.Width;
            int nDestHeight = destRect.Height; ;


            // 左上-------------------------------------;
            xDest = destRect.Left;
            yDest = destRect.Top;
            widthDest = NineRect.Left - srcRect.Left;
            heightDest = NineRect.Top - srcRect.Top;
            xSrc = srcRect.Left;
            ySrc = srcRect.Top;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthDest, heightDest, GraphicsUnit.Pixel);

            // 上-------------------------------------;
            xDest = destRect.Left + NineRect.Left - srcRect.Left;
            widthDest = nDestWidth - widthDest - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            widthSrc = NineRect.Right - NineRect.Left;
            heightSrc = NineRect.Top - srcRect.Top;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthSrc, heightSrc, GraphicsUnit.Pixel);

            // 右上-------------------------------------;
            xDest = destRect.Right - (srcRect.Right - NineRect.Right);
            widthDest = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthDest, heightDest, GraphicsUnit.Pixel);

            // 左-------------------------------------;
            xDest = destRect.Left;
            yDest = destRect.Top + NineRect.Top - srcRect.Top;
            widthDest = NineRect.Left - srcRect.Left;
            heightDest = destRect.Bottom - yDest - (srcRect.Bottom - NineRect.Bottom);
            xSrc = srcRect.Left;
            ySrc = NineRect.Top;
            widthSrc = NineRect.Left - srcRect.Left;
            heightSrc = NineRect.Bottom - NineRect.Top;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthSrc, heightSrc, GraphicsUnit.Pixel);

            // 中-------------------------------------;
            xDest = destRect.Left + NineRect.Left - srcRect.Left;
            widthDest = nDestWidth - widthDest - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            widthSrc = NineRect.Right - NineRect.Left;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthSrc, heightSrc, GraphicsUnit.Pixel);

            // 右-------------------------------------;
            xDest = destRect.Right - (srcRect.Right - NineRect.Right);
            widthDest = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            widthSrc = srcRect.Right - NineRect.Right;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthSrc, heightSrc, GraphicsUnit.Pixel);

            // 左下-------------------------------------;
            xDest = destRect.Left;
            yDest = destRect.Bottom - (srcRect.Bottom - NineRect.Bottom);
            widthDest = NineRect.Left - srcRect.Left;
            heightDest = srcRect.Bottom - NineRect.Bottom;
            xSrc = srcRect.Left;
            ySrc = NineRect.Bottom;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthDest, heightDest, GraphicsUnit.Pixel);

            // 下-------------------------------------;
            xDest = destRect.Left + NineRect.Left - srcRect.Left;
            widthDest = nDestWidth - widthDest - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            widthSrc = NineRect.Right - NineRect.Left;
            heightSrc = srcRect.Bottom - NineRect.Bottom;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthSrc, heightSrc, GraphicsUnit.Pixel);

            // 右下-------------------------------------;
            xDest = destRect.Right - (srcRect.Right - NineRect.Right);
            widthDest = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            g.DrawImage(img, new Rectangle(xDest, yDest, widthDest, heightDest), xSrc, ySrc, widthDest, heightDest, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g">绘图对像</param>
        /// <param name="img">图片</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="lr">绘置的图片边界</param>
        /// <param name="index">当前状态</param> 
        /// <param name="Totalindex">状态总数</param>
        public static void DrawNineRect(Graphics g, Bitmap img, Rectangle r, Rectangle lr, int index, int Totalindex)
        {
            if (img == null) { return; }

            Rectangle r1, r2;
            int x = (index - 1) * img.Width / Totalindex;
            int y = 0;
            int x1 = r.Left;
            int y1 = r.Top;

            if (r.Height > img.Height && r.Width <= img.Width / Totalindex)
            {
                r1 = new Rectangle(x, y, img.Width / Totalindex, lr.Top);
                r2 = new Rectangle(x1, y1, r.Width, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + lr.Top, img.Width / Totalindex, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, r.Width, r.Height - lr.Top - lr.Bottom);
                if ((lr.Top + lr.Bottom) == 0) { r1.Height = r1.Height - 1; }
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + img.Height - lr.Bottom, img.Width / Totalindex, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, r.Width, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else
            {
                if (r.Height <= img.Height && r.Width > img.Width / Totalindex)
                {
                    r1 = new Rectangle(x, y, lr.Left, img.Height);
                    r2 = new Rectangle(x1, y1, lr.Left, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    r1 = new Rectangle(x + lr.Left, y, img.Width / Totalindex - lr.Left - lr.Right, img.Height);
                    r2 = new Rectangle(x1 + lr.Left, y1, r.Width - lr.Left - lr.Right, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, img.Height);
                    r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                }
                else
                {
                    if (r.Height <= img.Height && r.Width <= img.Width / Totalindex)
                    {
                        r1 = new Rectangle((index - 1) * img.Width / Totalindex, 0, img.Width / Totalindex, img.Height - 1);
                        g.DrawImage(img, new Rectangle(x1, y1, r.Width, r.Height), r1, GraphicsUnit.Pixel);
                    }
                    else if (r.Height > img.Height && r.Width > img.Width / Totalindex)
                    {
                        //top-left
                        r1 = new Rectangle(x, y, lr.Left, lr.Top);
                        r2 = new Rectangle(x1, y1, lr.Left, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //top-bottom
                        r1 = new Rectangle(x, y + img.Height - lr.Bottom, lr.Left, lr.Bottom);
                        r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, lr.Left, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //left
                        r1 = new Rectangle(x, y + lr.Top, lr.Left, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1, y1 + lr.Top, lr.Left, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //top
                        r1 = new Rectangle(x + lr.Left, y,
                            img.Width / Totalindex - lr.Left - lr.Right, lr.Top);
                        r2 = new Rectangle(x1 + lr.Left, y1,
                            r.Width - lr.Left - lr.Right, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //right-top
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, lr.Top);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //Right
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + lr.Top,
                            lr.Right, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + lr.Top,
                            lr.Right, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //right-bottom
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + img.Height - lr.Bottom,
                            lr.Right, lr.Bottom);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + r.Height - lr.Bottom,
                            lr.Right, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //bottom
                        r1 = new Rectangle(x + lr.Left, y + img.Height - lr.Bottom,
                            img.Width / Totalindex - lr.Left - lr.Right, lr.Bottom);
                        r2 = new Rectangle(x1 + lr.Left, y1 + r.Height - lr.Bottom,
                            r.Width - lr.Left - lr.Right, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //Center
                        r1 = new Rectangle(x + lr.Left, y + lr.Top,
                            img.Width / Totalindex - lr.Left - lr.Right, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1 + lr.Left, y1 + lr.Top,
                            r.Width - lr.Left - lr.Right, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        /// <summary>
        /// 获取绘制带有阴影的字符串的图像
        /// </summary>
        /// <param name="str">需要绘制的字符串</param>
        /// <param name="font">显示字符串的字体</param>
        /// <param name="foreColor">字符串的颜色</param>
        /// <param name="shadowColor">字符串阴影颜色</param>
        /// <param name="shadowWidth">阴影的宽度</param>
        /// <returns>绘有发光字符串的Image对象</returns>
        public static Image GetStringImgWithShadowEffect(string str, Font font, Color foreColor, Color shadowColor, int shadowWidth)
        {
            Bitmap bitmap = null;//实例化Bitmap类
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))//实例化Graphics类
            {
                SizeF size = g.MeasureString(str, font);//对字符串进行测量
                using (Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height))//通过文字的大小实例化Bitmap类
                using (Graphics Var_G_Bmp = Graphics.FromImage(bmp))//实例化Bitmap类
                using (SolidBrush Var_BrushBack = new SolidBrush(Color.FromArgb(16, shadowColor.R, shadowColor.G, shadowColor.B)))//根据RGB的值定义画刷
                using (SolidBrush Var_BrushFore = new SolidBrush(foreColor))//定义画刷
                {
                    Var_G_Bmp.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                    Var_G_Bmp.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                    Var_G_Bmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                    Var_G_Bmp.DrawString(str, font, Var_BrushBack, 0, 0);//给制文字
                    bitmap = new Bitmap(bmp.Width + shadowWidth, bmp.Height + shadowWidth);//根据辉光文字的大小实例化Bitmap类
                    using (Graphics Var_G_Bitmap = Graphics.FromImage(bitmap))//实例化Graphics类
                    {
                        Var_G_Bitmap.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                        Var_G_Bitmap.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                        Var_G_Bitmap.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                        //遍历辉光文字的各象素点
                        for (int x = 0; x <= shadowWidth; x++)
                        {
                            for (int y = 0; y <= shadowWidth; y++)
                            {
                                Var_G_Bitmap.DrawImageUnscaled(bmp, x, y);//绘制辉光文字的点
                            }
                        }
                        Var_G_Bitmap.DrawString(str, font, Var_BrushFore, shadowWidth / 2, shadowWidth / 2);//绘制文字
                    }
                }
            }

            return bitmap;

        }

        /// <summary>
        /// 建立带有圆角样式的路径。
        /// </summary>
        /// <param name="rect">用来建立路径的矩形。</param>
        /// <param name="radius">圆角的大小。</param>
        /// <returns>建立的路径。</returns>
        public static GraphicsPath CreateRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int radiusCorrection = 1;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(
                rect.Right - radius - radiusCorrection,
                rect.Y,
                radius,
                radius,
                270,
                90);
            path.AddArc(
                rect.Right - radius - radiusCorrection,
                rect.Bottom - radius - radiusCorrection,
                radius,
                radius, 0, 90);
            path.AddArc(
                rect.X,
                rect.Bottom - radius - radiusCorrection,
                radius,
                radius,
                90,
                90);
            path.CloseFigure();
            return path;
        }
    }
}
