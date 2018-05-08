using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    public class gameObject
    {
        protected Image theImage = null;
        protected double lPosition;
        protected double tPosition;
        public gameObject(Canvas canvas, String source, double lPosition, double tPosition, double width, double heigth)
        {
            this.theImage = new Image { Source = new BitmapImage(new Uri(source, UriKind.Relative)) };
            this.theImage.Width = width;
            this.theImage.Height = heigth;
            this.lPosition = lPosition;
            this.tPosition = tPosition;
            canvas.Children.Add(theImage);
            Canvas.SetLeft(theImage, this.lPosition);
            Canvas.SetTop(theImage, this.tPosition);
        }
        public double getHeigth()
        {
            return this.theImage.Height;
        }

        public double getWidth()
        {
            return this.theImage.Width;
        }
        public void setPositionLeft(double left)
        {
            Canvas.SetLeft(theImage, left);
        }
        public void setPositionTop(double top)
        {
            Canvas.SetTop(theImage, top);
        }
        public void moveLeft(double i)
        {
            this.lPosition -= i;
            Canvas.SetLeft(theImage, lPosition);
        }
        public void moveRight(double i)
        {
            this.lPosition += i;
            Canvas.SetLeft(theImage, lPosition);
        }

        public double getPositionLeft()
        {
            return Canvas.GetLeft(theImage);
        }

        public double getPositionTop()
        {
            return Canvas.GetTop(theImage);
        }

        public void resetPlayer(Canvas canvas)
        {
            canvas.Children.Remove(this.theImage);
        }

        public Image getImage()
        {
            return this.theImage;
        }

    }

}
