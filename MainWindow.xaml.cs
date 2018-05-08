using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApplication1
{


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //Timer to start timers and end
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += new EventHandler(timerStart);
            timer.Start();

            //GameStart
            gameOStart();
            //Spawn Enemies Level 1
            enemyOStart();

        }


        int level=1;


        //EnemyShoot
        Rectangle Eprojectile = new Rectangle();
        int EprojInPlay = 0;
        double EprojPosition = 0;
        bool EProjHasHit = false;

        //PROJECTILE
        Rectangle projectile = new Rectangle();
        int projInPlay = 0; // 0 NOT IN PLAY || 1 IN PLAY
        double projPosition = 570;
        bool projHasHit = false;


        //Boss
        enemyObject boss;
        Rectangle Bprojectile = new Rectangle();
        int BprojInPlay = 0;
        double BprojPosition = 0;
        bool BProjHasHit = false;
        int bossX = 0;
        int bossY = 45;
        int bossYdir = 1;
        int bossXdir = 1;
        //Boss Life
        int bossLife = 5;
        Rectangle bossLifeBar = new Rectangle();


        //SCORE
        int score = 0;

        //LIFE
        int lifeCount = 3;

        //Player
        gameObject player;

        //Enemy Level 1
        enemyObject[] enemyArrayEX = new enemyObject[30];
        int y1 = 45;
        int x1 = 0;
        int y2 = 85;
        int x2 = 0;
        int y3 = 125;
        int x3 = 0;
        int ydirection1 = 1;
        int xdirection1 = 1;
        int ydirection2 = 1;
        int xdirection2 = 1;
        int ydirection3 = 1;
        int xdirection3 = 1;

        //Enemy level 2
        enemyObject[] enemyArrayEX2 = new enemyObject[6];
        int lvl2X = 0;
        int lvl2Y = 130;
        int lvl2Ydir = 1;
        int lvl2Xdir = 1;

        //--GameOver--
        bool timerCalled = true;
        bool gameOver = false;

        //ChangeLevel
        bool timetoChange = false;

        //Pause
        bool pauser = false;
  
        //TIMERS
        DispatcherTimer enemyTimer1 = new DispatcherTimer(DispatcherPriority.Normal);
        DispatcherTimer shootTimer = new DispatcherTimer(DispatcherPriority.Normal);
        DispatcherTimer bossTimer1 = new DispatcherTimer(DispatcherPriority.Normal);
        DispatcherTimer EshootTimer = new DispatcherTimer(DispatcherPriority.Normal);
        DispatcherTimer messageTimer = new DispatcherTimer(DispatcherPriority.Normal);
        DispatcherTimer enemylvl2 = new DispatcherTimer(DispatcherPriority.Normal);
        

//----------------------------------------------------------------------------------------------------------------

        private void timerStart(Object e, EventArgs a)
        {
            info1.Content = gameOver;

            
            if (timerCalled)
            {
                enemyTimer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
                enemyTimer1.Tick += new EventHandler(move3Rows);
                enemyTimer1.Start();

                EshootTimer.Interval = new TimeSpan(0, 0, 0, 0, 18);
                EshootTimer.Tick += new EventHandler(enemyShoot);
                EshootTimer.Start();

                //Projectile Movement TIMER
                shootTimer.Interval = new TimeSpan(0, 0, 0, 0, 18);
                shootTimer.Tick += new EventHandler(Shoot);
                shootTimer.Tick += new EventHandler(Collision);
                shootTimer.Start();

                bossTimer1.Interval = new TimeSpan(0, 0, 0, 0, 18);
                bossTimer1.Tick += new EventHandler(moveEnemyBoss);
                bossTimer1.Tick += new EventHandler(bossShoot);

                messageTimer.Interval = new TimeSpan(0, 0, 5);
                messageTimer.Tick += new EventHandler(messages);
                messageTimer.Start();

                enemylvl2.Interval = new TimeSpan(0, 0, 0 ,0, 18);
                enemylvl2.Tick += new EventHandler(enemyLvl2Move);
                enemylvl2.Tick += new EventHandler(enemyShootlvl2);
                enemylvl2.Start();

                timerCalled = false;

            }
            else if (gameOver)
            {
                
                enemyTimer1.Stop();
                shootTimer.Stop();
                EshootTimer.Stop();
                bossTimer1.Stop();
                messageTimer.Stop();
                enemylvl2.Stop();

                gameOver = false;
                Image gameOverIMG = new Image { Source = new BitmapImage(new Uri("./gameover.png", UriKind.Relative)) };
                gameOverIMG.Width = 300;
                gameOverIMG.Height = 300;
                game.Children.Add(gameOverIMG);
                Canvas.SetLeft(gameOverIMG, 175);
                Canvas.SetTop(gameOverIMG, 150);
                gameOverLabel.Content = "PRESS R TO RESTART";
                
            }
        }
        private void gameOStart()
        {
            try
            {
                player = new gameObject(game, "./ship.png", 280, 570, 78, 58);
                bossLabel.Visibility = Visibility.Hidden;
                scoreMessage.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }
        private void enemyOStart()
        {
            try
            {
                int col = 115;
                int row = 45;
                String source = "";
                for (int i = 0; i < enemyArrayEX.Length; i++)
                {
                    if (i % 10 == 0 && i != 0)
                    {
                        row += 40;
                        col = 40;
                    }
                    if (i == 10)
                    {
                        col = 115;
                    } else if (i == 20)
                    {
                        col = 115;
                    }
                    if (i % 2 == 0)
                    {
                        source = "./enemy1.png";
                    }
                    else {
                        source = "./enemy2.png";
                    }
                    enemyArrayEX[i] = new enemyObject(game, source, col, row, 30, 30);
                    col += 40;
                }
            }
            catch (Exception)
            {
            }
        }

        void enemyOStartLvl2()
        {
            String source = "./enemy3.gif";
            int y = 130;
            int x = 500;
            try
            {
            for (int i = 0; i < enemyArrayEX2.Length; i++)
            {
                   if (i < 3)
                    {
                        enemyArrayEX2[i] = new enemyObject(game, source, x, y, 60, 60);
                        y += 60;
                    } else if (i == 3)
                    {
                        y = 130;
                        x = 100;
                        enemyArrayEX2[i] = new enemyObject(game, source, x, y, 60, 60);
                        y += 60;
                    } else if (i <= 6)
                    {
                        enemyArrayEX2[i] = new enemyObject(game, source, x, y, 60, 60);
                        y += 60;
                    }
                    
                }
            

            } catch (Exception)
            {

            }
        }

       public void enemyLvl2Move(Object sender, EventArgs e)
        {

            try
            {

                for (int i = 0; i <= 6; i++)
                {
                    enemyArrayEX2[i].setTop(lvl2Y);
                    if (lvl2X < 100 && lvl2Xdir == 1)
                    {
                        lvl2X += 5;
                        if (lvl2X >= 100)
                        {
                            lvl2Xdir = 0;
                        }
                    }
                    else if (lvl2X > -100 && lvl2Xdir == 0)
                    {
                        lvl2X -= 5;
                        if (lvl2X == -100)
                        {
                            lvl2Xdir = 1;
                        }
                    }
                    for (int j = 0; j <= 6; j++)
                    {
                        enemyArrayEX2[j].setLeft(enemyArrayEX2[j].getLeft() + lvl2X);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void move3Rows(Object sender, EventArgs e) {
            moveEnemyRow1();
            moveEnemyRow2();
            moveEnemyRow3();
        }
        public void moveEnemyRow1()
        {
            

                if (y1 < 420 && ydirection1 == 1)
                {
                    y1 += 5;
                    if (y1 == 320)
                    {
                        ydirection1 = 0;
                    }
                }
                else if (y1 > 45 && ydirection1 == 0)
                {
                    y1 -= 5;

                    if (y1 == 45)
                    {
                        ydirection1 = 1;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    enemyArrayEX[i].setTop(y1);
                    if (x1 < 100 && xdirection1 == 1)
                    {
                        x1 += 5;
                        if (x1 >= 100)
                        {
                            xdirection1 = 0;
                        }
                    }
                    else if (x1 > -100 && xdirection1 == 0)
                    {
                        x1 -= 5;
                        if (x1 == -100)
                        {
                            xdirection1 = 1;
                        }
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        enemyArrayEX[j].setLeft(enemyArrayEX[j].getLeft() + x1);
                    }
                }
            

            
        }
        public void moveEnemyRow2()
        {
            try
            {
                if (y2 < 460 && ydirection2 == 1)
                {
                    y2 += 5;
                    if (y2 == 360)
                    {
                        ydirection2 = 0;
                    }
                }
                else if (y2 > 85 && ydirection2 == 0)
                {
                    y2 -= 5;
                    if (y2 == 85)
                    {
                        ydirection2 = 1;
                    }
                }
                for (int i = 10; i < 20; i++)
                {
                    enemyArrayEX[i].setTop(y2);
                    if (x2 < 100 && xdirection2 == 1)
                    {
                        x2 += 5;
                        if (x2 >= 100)
                        {
                            xdirection2 = 0;
                        }
                    }
                    else if (x2 > -100 && xdirection2 == 0)
                    {
                        x2 -= 5;
                        if (x2 == -100)
                        {
                            xdirection2 = 1;
                        }
                    }
                    for (int j = 10; j < 20; j++)
                    {
                        enemyArrayEX[j].setLeft(enemyArrayEX[j].getLeft() + x2);
                    }
                }

            }

            catch (Exception)
            {

            }
        }
        public void moveEnemyRow3()
        {
            try
            {
                if (y3 < 500 && ydirection3 == 1)
                {
                    y3 += 5;
                    if (y3 == 400)
                    {
                        ydirection3 = 0;
                    }
                }
                else if (y3 > 125 && ydirection3 == 0)
                {
                    y3 -= 5;
                    if (y3 == 125)
                    {
                        ydirection3 = 1;
                    }
                }
                for (int i = 20; i <= 30; i++)
                {
                    enemyArrayEX[i].setTop(y3);
                    if (x3 < 100 && xdirection3 == 1)
                    {
                        x3 += 5;
                        if (x3 >= 100)
                        {
                            xdirection3 = 0;
                        }
                    }
                    else if (x3 > -100 && xdirection3 == 0)
                    {
                        x3 -= 5;
                        if (x3 == -100)
                        {
                            xdirection3 = 1;
                        }
                    }
                    for (int j = 20; j < 30; j++)
                    {
                        enemyArrayEX[j].setLeft(enemyArrayEX[j].getLeft() + x3);
                    }

                }
            }

            catch (Exception)
            {

            }
        }
        void enemyShoot(object sender, EventArgs e)
        {
            Random r = new Random();
            int rInt = r.Next(0, enemyArrayEX.Length); //for ints
            if (EprojInPlay == 0)
            {
                try //Have to handle the null expection.
                {

                        if (game.Children.Contains(enemyArrayEX[rInt].getImage())){
                            Eprojectile.Stroke = new SolidColorBrush(Colors.Green);
                            Eprojectile.Fill = new SolidColorBrush(Colors.Green);
                            Eprojectile.Width = 7;
                            Eprojectile.Height = 25;
                            Canvas.SetLeft(Eprojectile, Canvas.GetLeft(enemyArrayEX[rInt].getImage()) + 12);
                            Canvas.SetTop(Eprojectile, Canvas.GetTop(enemyArrayEX[rInt].getImage()) + 25);
                            EprojPosition = Canvas.GetTop(enemyArrayEX[rInt].getImage()) + 25;
                            game.Children.Add(Eprojectile);
                            EprojInPlay = 1;
                        }
                    
                }
                catch (Exception) //catching the null expection.
                {
                    //info.Content = "Caught";
                }
            }
            else
            {
            }

        }  

        void enemyShootlvl2(object sender, EventArgs e)
        {
            Random r2 = new Random();
            int rInt2 = r2.Next(0, enemyArrayEX2.Length); //for ints
            if (EprojInPlay == 0)
            {
                try //Have to handle the null expection.
                {

                    if (game.Children.Contains(enemyArrayEX2[rInt2].getImage()))
                    {
                        Eprojectile.Stroke = new SolidColorBrush(Colors.Green);
                        Eprojectile.Fill = new SolidColorBrush(Colors.Green);
                        Eprojectile.Width = 7;
                        Eprojectile.Height = 25;
                        Canvas.SetLeft(Eprojectile, Canvas.GetLeft(enemyArrayEX2[rInt2].getImage()) + 28);
                        Canvas.SetTop(Eprojectile, Canvas.GetTop(enemyArrayEX2[rInt2].getImage()) + 40);
                        EprojPosition = Canvas.GetTop(enemyArrayEX2[rInt2].getImage()) + 25;
                        game.Children.Add(Eprojectile);
                        EprojInPlay = 1;
                    }

                }
                catch (Exception) //catching the null expection.
                {
                    //info.Content = "Caught";
                }
            }
            else
            {
            }

        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (!(player.getPositionLeft() == 560 || player.getPositionLeft() == 0))
            {
                if (pauser == false)
                {

                    switch (e.Key) //check if outside of screen ----> size of canvas -> 559 Ship set to 280
                    {

                        case Key.Left:
                            player.moveLeft(10);
                            break;
                        case Key.Right:
                            player.moveRight(10);
                            break;
                    }
                }
            }
            else
                 if (player.getPositionLeft() == 560)
            {
                if (e.Key == Key.Left)
                {
                    player.moveLeft(10);
                }
            }
            else if (player.getPositionLeft() == 0)
            {
                if (e.Key == Key.Right)
                {
                    player.moveRight(10);
                }
            } 
            //Shooting projectiles
            if (e.Key == Key.Space && projInPlay == 0 && pauser == false)
            {
                projectile.Stroke = new SolidColorBrush(Colors.Red);
                projectile.Fill = new SolidColorBrush(Colors.Red);
                projectile.Width = 10;
                projectile.Height = 30;
                Canvas.SetLeft(projectile, (player.getPositionLeft() + 34));
                Canvas.SetTop(projectile, 570);
                game.Children.Add(projectile);
                projInPlay = 1;
            }
            if (e.Key == Key.R)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
                timerCalled = true;
            }
            if (e.Key == Key.P)
            {
                if (pauser == false)
                {
                    enemyTimer1.Stop();
                    shootTimer.Stop();
                    EshootTimer.Stop();
                    bossTimer1.Stop();
                    messageTimer.Stop();
                    enemylvl2.Stop();
                    pauser = true;
                }else if (pauser)
                {
                    enemyTimer1.Start();
                    shootTimer.Start();
                    EshootTimer.Start();
                    bossTimer1.Start();
                    messageTimer.Start();
                    enemylvl2.Start();
                    pauser = false;
                }
            }
            if (e.Key == Key.S)
            {
                save();
            }

            if (e.Key == Key.L)
            {
                load();
            }
        } 

        void Shoot(object sender, EventArgs e)
        {

            // Gotta figure out how to make the rectangle get perma-removed. 
            // It stays on the game and is being a little bitch.
            if (projInPlay == 1)
            {
                if ((projHasHit == false))
                {
                    projPosition -= 100;
                    Canvas.SetTop(projectile, projPosition);
                    if (projPosition <= 0)
                    {
                        projHasHit = true;
                    }
                }
                else
                {
                    projInPlay = 0;
                    projPosition = 570;
                    game.Children.Remove(projectile);
                    projHasHit = false;


                }
            }
            


            if (EprojInPlay == 1)
            {
                if ((EProjHasHit == false))
                {
                    EprojPosition += 10;
                    Canvas.SetTop(Eprojectile, EprojPosition);
                    if (EprojPosition >= 600)
                    {
                        EprojInPlay = 0;
                        game.Children.Remove(Eprojectile);
                    }

                }
                else
                {
                    EprojInPlay = 0;
                    EprojPosition = 0;
                    game.Children.Remove(Eprojectile);
                    EProjHasHit = false;

                }

            }


            //boss projectile
            if (BprojInPlay == 1)
            {
                if ((BProjHasHit == false))
                {
                    BprojPosition += 5;
                    Canvas.SetTop(Bprojectile, BprojPosition);
                    if (BprojPosition >= 600)
                    {
                        BprojInPlay = 0;
                        game.Children.Remove(Bprojectile);
                    }

                }
                else
                {
                    BprojInPlay = 0;
                    BprojPosition = 0;
                    game.Children.Remove(Bprojectile);
                    BProjHasHit = false;

                }

            }

        }

        void Collision(Object sender, EventArgs e)
        {


            //ENEMY COLLISION
            for (int i = 0; i <= 29; i++)
            {
                
                if (collision.detectCollsion(game, enemyArrayEX, projectile, 40, 40, i))
                {
                    game.Children.Remove(projectile);
                    game.Children.Remove(enemyArrayEX[i].getImage());
                    projPosition = 570;
                    Canvas.SetTop(projectile, projPosition);
                    projHasHit = true;
                    score += 50;
                    playerScore.Content = score;
                    if (score >= 1500)
                    {
                        score += 250;
                        startLevel2();
                        playerScore.Content = score;
                        scoreMessage.Visibility = Visibility.Visible;
                        scoreMessage.Content = "You have reached Level 2 ";
                    }
                }
            }


            //COLL: ENEMY -> PLAYER
            if (collision.EDetectCollision(game,Eprojectile,player))
            {
                game.Children.Remove(Eprojectile);
                lifeTotal();
                EprojPosition = 0;
                Canvas.SetTop(Eprojectile, EprojPosition);
                EProjHasHit = true;
            } 

            if (projPosition < 0)
                projHasHit = true;
                
            if (EprojPosition > 580)
                EProjHasHit = true;

            

            //COLL: BOSS -> PLAYER
            if (collision.EDetectCollision(game, Bprojectile, player))
            {
                game.Children.Remove(Bprojectile);
                lifeTotal();
                BprojPosition = 0;
                Canvas.SetTop(Bprojectile, BprojPosition);
                BProjHasHit = true;
            }

            if (BprojPosition > 580)
                BProjHasHit = true;


            


                //COLL: PLAYER -> lvl2Enemy
                for (int i = 0; i <= 6; i++)
                {
                    if (collision.detectCollsion(game, enemyArrayEX2, projectile, 60, 60, i))
                    {

                        game.Children.Remove(projectile);
                        game.Children.Remove(enemyArrayEX2[i].getImage());
                        projPosition = 570;
                        Canvas.SetTop(projectile, projPosition);
                        projHasHit = true;
                        score += 50;
                        playerScore.Content = score;
                    if (score == 2550) // win
                    { 
                        score += 250;
                        playerScore.Content = score;
                        scoreMessage.Visibility = Visibility.Visible;
                        scoreMessage.Content = "You Won!";

                    }
                    }
                }


            //COLL: PLAYER -> BOSS
            if (collision.BossdetectCollsion(game, boss, projectile))
            {
                game.Children.Remove(projectile);
                projPosition = 570;
                Canvas.SetTop(projectile, projPosition);
                projHasHit = true;
                bossLife--;
                if (bossLife == 4)
                {
                    bossLifeBar.Width = 120;
                }
                else if (bossLife == 3)
                {
                    bossLifeBar.Width = 90;
                }
                else if (bossLife == 2)
                {
                    bossLifeBar.Width = 60;
                }
                else if (bossLife == 1)
                {
                    bossLifeBar.Width = 30;
                }
                else if (bossLife == 0)
                {
                    bossLifeBar.Width = 0;
                    game.Children.Remove(boss.getImage());
                    score += 500;
                    playerScore.Content = score;
                    scoreMessage.Visibility = Visibility.Visible;
                    scoreMessage.Content = "+500 Boss kill";
                    bossLabel.Content = "";
                    BprojInPlay = 0;
                    BprojPosition = 0;
                    Canvas.SetTop(Bprojectile, BprojPosition);
                    game.Children.Remove(Bprojectile);
                    bossTimer1.Stop();


                }


            }
        }

        public void lifeTotal()
        {


            if (lifeCount == 3)
            {
                

                game.Children.Remove(life1);
                lifeCount--;
                
            }
            else if (lifeCount == 2)
            {
                game.Children.Remove(life2);
                lifeCount--;

            }
            else if (lifeCount == 1)
            {
                game.Children.Remove(life3);
                lifeCount--;
                bossLabel.Visibility = Visibility.Hidden;
                bossLifeBar.Visibility = Visibility.Hidden;
                gameOver = true;


            }
        }

        private void enemyBossStart()
        {
            try
            {
                int col = 115;
                int row = 45;
                String source = "./boss_enemy.png";
                row = 40;
                col = 40;

                boss = new enemyObject(game, source, col, row, 200, 200);
                bossLifeBar.Width = 150;
                bossLifeBar.Height = 25;
                bossLifeBar.Stroke = new SolidColorBrush(Colors.Red);
                bossLifeBar.Fill = new SolidColorBrush(Colors.Red);
                Canvas.SetLeft(bossLifeBar, 275);
                Canvas.SetTop(bossLifeBar, 12);
                game.Children.Add(bossLifeBar);
                bossLabel.Content = "BOSS";
                bossLabel.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
            }
        }

        public void moveEnemyBoss(Object sender, EventArgs e)
        {
         
            try
            {

                if (bossYdir == 1) // going down
                {
                    if (bossXdir == 1)
                    {
                        bossX += 6;
                        bossY += 3;
                    }
                    if (bossXdir == 0)
                    {
                        bossX -= 6;
                        bossY += 3;
                    }
                    if (bossX > 450)
                    {
                        bossXdir = 0;
                    }
                    if (bossX < 5)
                    {
                        bossXdir = 1;
                    }

                    if (bossY > 360)
                    {
                        bossYdir = 0;
                    }
                }
                if (bossYdir == 0)
                {
                    if (bossXdir == 1)
                    {
                        bossX += 5;
                        bossY -= 2;
                    }
                    if (bossXdir == 0)
                    {
                        bossX -= 5;
                        bossY -= 2;
                    }
                    if (bossX > 450)
                    {
                        bossXdir = 0;
                    }
                    if (bossX < 5)
                    {
                        bossXdir = 1;
                    }

                    if (bossY < 5)
                    {
                        bossYdir = 1;
                    }
                }


                Canvas.SetTop(boss.getImage(), bossY);

                Canvas.SetLeft(boss.getImage(), bossX);

            }

            catch (Exception)
            {

            }
        }

        void bossShoot(object sender, EventArgs e)
        {
            if (BprojInPlay == 0)
            {
                try //Have to handle the null expection.
                {
                    Bprojectile.Stroke = new SolidColorBrush(Colors.DarkGoldenrod);
                    Bprojectile.Fill = new SolidColorBrush(Colors.DarkGoldenrod);
                    Bprojectile.Width = 15;
                    Bprojectile.Height = 45;
                    Canvas.SetLeft(Bprojectile, Canvas.GetLeft(boss.getImage()) + 100);
                    Canvas.SetTop(Bprojectile, Canvas.GetTop(boss.getImage()) + 100);
                    BprojPosition = Canvas.GetTop(boss.getImage()) + 100;
                    game.Children.Add(Bprojectile);
                    BprojInPlay = 1;

                }
                catch (Exception) //catching the null expection.
                {
                    //info.Content = "Caught";
                }
            }
            else
            {
            }
        }

        public void startLevel2()
        {
                EshootTimer.Stop();
                enemyBossStart();
                enemyOStartLvl2();
                bossTimer1.Start();
                
            
            
        }
        public void messages(Object sender, EventArgs e)
        {
            if ((String)scoreMessage.Content == "+250 for next level")
            {
                scoreMessage.Content = "";
                scoreMessage.Visibility = Visibility.Hidden;

            }
            if ((String)scoreMessage.Content == "+500 Boss kill"){
                scoreMessage.Content = "";
                scoreMessage.Visibility = Visibility.Hidden;
            }
        }


        public void save()

        {
            /*
            //which level your in
            //enemy array
            //score
            //lives
            //player position
            //enemy position
            */

            info2.Content = "Saved..?";
            // Create a string array with the lines of text
            string[] lines = { "L"+level, "S"+score, "H"+lifeCount };
            
            string mydocpath = @".\";

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\save.txt"))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }


        }

        public void load()
        {

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("save.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadToEnd();

                    // level?
                    /* if (line.Contains("L1"))
                     {
                         startLevel1();
                     }
                     else if(line.Contains("L2"))
                     {
                         startLevel2();
                     }*/

                    //line.Substring(line.IndexOf("S"));
                    info2.Content = line + line.IndexOf("\n");
                    // score
                    if (line.Contains("S0"))
                    {
                        score = 0;

                        playerScore.Content = score;
                    }
                    if (line.Contains("S50"))
                    {
                        score = 50;
                        playerScore.Content = score;
                    }

                    else if (line.Contains("S100"))
                    { 
                        score = 100;
                        playerScore.Content = score;
                    }
                    
                    //lives

                    // level
                }
            }
            catch (Exception ef)
            {
                info2.Content = "The file could not be read:" + (ef.Message);
            }
        }
    

        //------------------------------------------------------------------------------------------------------------------
    } //end
} //end


        
    

