using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    class collision
    {
        public static bool detectCollsion(Canvas game,enemyObject[] enemy, Rectangle bullet, double height, double width, int i)
        {
            try
            {
                //BULLET
                double leftB = Convert.ToDouble(bullet.GetValue(Canvas.LeftProperty));
                double topB = Convert.ToDouble(bullet.GetValue(Canvas.TopProperty));

                Rect bulletPosition = new Rect(leftB, topB, 30, 10);
                //Rect bulletPosition = new Rect(bulletPoint, bulletPoint2);

                
                    //ENEMY
                    double leftE = Canvas.GetLeft(enemy[i].getImage());
                    double topE = Canvas.GetTop(enemy[i].getImage());
                    


                    Rect enemyPosition = new Rect(leftE, topE, height, width);

                    //HITBOX
                    Rectangle rec1 = new Rectangle();
                    rec1.Width = width;
                    rec1.Height = height;
                    rec1.Stroke = new SolidColorBrush(Colors.Red);
                    //rec1.Fill = new SolidColorBrush(Colors.Red);
                    Canvas.SetLeft(rec1, leftE);
                    Canvas.SetTop(rec1, topE);
                    //game.Children.Add(rec1);

                if (game.Children.Contains(enemy[i].getImage()))
                {
                    enemyPosition.Intersect(bulletPosition);
                    if (!(enemyPosition.IsEmpty))
                    {
                        return true;

                    }

                }
                
            }
            catch (Exception)
            {

            }
            return false;

            
        }


        public static bool EDetectCollision(Canvas game, Rectangle bullet, gameObject player)
        {

            //BULLET

            double leftB = Convert.ToDouble(bullet.GetValue(Canvas.LeftProperty));
            double topB = Convert.ToDouble(bullet.GetValue(Canvas.TopProperty));

            Rect bulletPosition = new Rect(leftB, topB, 7, 25);

            //PLAYER
            double leftP = player.getPositionLeft() + 15;
            double topP = player.getPositionTop();

            Rect playerPosition = new Rect(leftP, topP, 50, 58);


            Rectangle rec1 = new Rectangle();
            rec1.Width = 50;
            rec1.Height = 58;
            rec1.Stroke = new SolidColorBrush(Colors.Red);
           // rec1.Fill = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(rec1, leftP);
            Canvas.SetTop(rec1, topP);
           // game.Children.Add(rec1);


            bulletPosition.Intersect(playerPosition);
            if (!(bulletPosition.IsEmpty))
            {
                return true;


            }

            return false;
        }



        public static bool BossdetectCollsion(Canvas game, enemyObject enemy, Rectangle bullet)
        {
            try
            {
                //BULLET
                double leftB = Convert.ToDouble(bullet.GetValue(Canvas.LeftProperty));
                double topB = Convert.ToDouble(bullet.GetValue(Canvas.TopProperty));

                Rect bulletPosition = new Rect(leftB, topB, 30, 10);
                //Rect bulletPosition = new Rect(bulletPoint, bulletPoint2);


                //ENEMY
                double leftE = Canvas.GetLeft(enemy.getImage());
                double topE = Canvas.GetTop(enemy.getImage());

                Rect enemyPosition = new Rect(leftE + 18, topE + 20, 160, 160);

                //HITBOX
                Rectangle rec1 = new Rectangle();
                rec1.Width = 160;
                rec1.Height = 160;
                rec1.Stroke = new SolidColorBrush(Colors.Red);
                //rec1.Fill = new SolidColorBrush(Colors.Red);
                Canvas.SetLeft(rec1, leftE + 18);
                Canvas.SetTop(rec1, topE + 20);
                //game.Children.Add(rec1);

                if (game.Children.Contains(enemy.getImage()))
                {
                    enemyPosition.Intersect(bulletPosition);
                    if (!(enemyPosition.IsEmpty))
                    {
                        return true;

                    }

                }

            }
            catch (Exception)
            {

            }
            return false;


        }


    }
}
