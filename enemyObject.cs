using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfApplication1
{
   public class enemyObject
    {
        protected Image theImage = null;
        protected int leftP; //100;
        protected int topP; // = 45;
        protected bool direction = false; //checks position the enemy is moving 
        protected bool hitEdge = false; //checks if the enemy has hit the edge of the screen
        protected bool resetterTop = false;// = 0; to know when to go down

        public enemyObject(Canvas canvas,String source, int leftP, int topP, double width, double heigth)
        {
            this.theImage = new Image { Source = new BitmapImage(new Uri(source, UriKind.Relative)) };
            this.theImage.Width = width;
            this.theImage.Height = heigth;
            this.leftP = leftP;
            this.topP = topP;
            canvas.Children.Add(theImage);
            Canvas.SetLeft(theImage, this.leftP);
            Canvas.SetTop(theImage, this.topP);
        }
        public void setTop(int pos)
        {
            Canvas.SetTop(theImage, pos);
        }
        public void setLeft(int pos)
        {
            Canvas.SetLeft(theImage, pos);
        }
        public Image getImage()
        {
            return this.theImage;
        }
        public int getLeft()
        {
            return leftP;
        }
        public int getTop()
        {
            return topP;
        }


    }
}
