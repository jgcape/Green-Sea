using System;
using SplashKitSDK;
using System.Collections.Generic;

public class GreenSea
{
    //Fields for turtle and boss objects.
    private Turtle _Turtle;
    private Boss _Boss;


    //Lists for in-game objects.
    private List<Enemy> _Enemies = new List<Enemy>();
    private List<Bubble> _Bubbles = new List<Bubble>();
    private List<Collectible> _Collectibles = new List<Collectible>();
    private List<Background> _Background = new List<Background>();
    private List<Enemy> _RemoveEnemies = new List<Enemy>();
    private List<Bubble> _RemoveBubbles = new List<Bubble>();
    private List<Collectible> _RemoveCollectibles = new List<Collectible>();
    private Bitmap _bombsRemaining;


    //Properties and fields for music and soundeffects.
    private SoundEffect _backgroundMusic { get; set; }
    private SoundEffect _sandstorm = new SoundEffect("sandstorm", "sandstorm.wav");
    private SoundEffect _underTheSea = new SoundEffect("uts", "underthesea.wav");
    private SoundEffect _yum = new SoundEffect("yum", "Eating.wav");
    private SoundEffect _ooh = new SoundEffect("ooh", "oooh.wav");
    private SoundEffect _ouch = new SoundEffect("ouch", "ouch.wav");
    private SoundEffect _splash = new SoundEffect("splash", "splash.wav");
    private SoundEffect _roar = new SoundEffect("roar", "roar.wav");


    //Counters for game events.
    private Timer _myTimer = new Timer("My Timer");
    private uint killedEnemies = 0;
    private double Difficulty = 0;
    private int _bombStatus;
    private int _bossHealth;
    private uint ScoreCounter = 0000;
    private bool _bossPresent = false;


    //Game quit property.
    public bool Quit
    {
        get
        {
            return _Turtle.Quit;
        }
    }
    public GreenSea()    //Game constructor. Creates the turtle, background, and one squid enemy.
    {
        _backgroundMusic = _underTheSea;
        _Turtle = new Turtle();

        _bombStatus = 0;
        _myTimer.Start();

        _Enemies.Add(new Squid(_Turtle));
        _Background.Add(new Sand());
        _Background.Add(new Sand2());
    }

    public void HandleInput()        //Handles input from player.
    {
        _Turtle.HandleInput();
        _Turtle.StayOnWindow();

        if (SplashKit.KeyReleased(KeyCode.VKey))
        {
            _Bubbles.Add(new Bubble(_Turtle));
        }

        if (SplashKit.KeyReleased(KeyCode.BKey))
        {
            if (_bombStatus > 0)
            {
                _splash.Play();
                _bombStatus -= 1;
                for (int i = 0; i < _Enemies.Count; i++)
                {
                    killedEnemies += 10;
                    _RemoveEnemies.Add(_Enemies[i]);
                }
                _bossHealth -= 4;
            }
        }
    }

    public void Draw()
    {
        SplashKit.CurrentWindow().Clear(Color.SkyBlue);

        for (int i = 0; i < _Background.Count; i++)
            _Background[i].Draw();

        _Turtle.Draw();

        if (_bossPresent && _bossHealth == 0)
        {
            SplashKit.CurrentWindow().Clear(Color.SkyBlue);

            for (int i = 0; i < _Background.Count; i++)
                _Background[i].Draw();

            _Turtle.Draw();
            SplashKit.CurrentWindow().DrawText($"You win! Bruce is dead!  Final Score: " + ScoreCounter, Color.Black, 200, 40);
            _Boss._bossBitmap = _Boss._bossDead;
            _Boss._bossHealthBitmap = _Boss._bossHealthDead;
            _Boss.Draw();
            SplashKit.CurrentWindow().Refresh();
            SplashKit.Delay(8000);
            _Turtle.Quit = true;
        }

        if (_bossPresent)
        {
            _Boss.Draw();
        }

        if (_bombStatus > 0)
        {
            SplashKit.DrawBitmap(_bombsRemaining, 20, 20);
        }

        SplashKit.CurrentWindow().DrawText($"Score: " + ScoreCounter, Color.Black, x: 600, y: 40);

        for (int i = 0; i < _Enemies.Count; i++)
            _Enemies[i].Draw();

        for (int i = 0; i < _Bubbles.Count; i++)
            _Bubbles[i].Draw();

        for (int i = 0; i < _Collectibles.Count; i++)
            _Collectibles[i].Draw();

        SplashKit.CurrentWindow().Refresh(60);
    }


    public void Update()
    {
        CheckCollisions();
        if (!_backgroundMusic.IsPlaying)
        {
            _backgroundMusic.Play();
        }
        for (int i = 0; i < _Enemies.Count; i++)
            _Enemies[i].Update();

        for (int i = 0; i < _Bubbles.Count; i++)
            _Bubbles[i].Update();

        for (int i = 0; i < _Background.Count; i++)
            _Background[i].Update();

        for (int i = 0; i < _Collectibles.Count; i++)
            _Collectibles[i].Update();

        _Turtle.Update();

        if (_bossPresent)
        {
            if (_bossHealth == 16)
            {
                _Boss._bossHealthBitmap = _Boss._bossHealth4;
            }
            if (_bossHealth == 12)
            {
                _Boss._bossHealthBitmap = _Boss._bossHealth3;
            }
            if (_bossHealth == 8)
            {
                _Boss._bossHealthBitmap = _Boss._bossHealth2;
            }
            if (_bossHealth == 4)
            {
                _Boss._bossHealthBitmap = _Boss._bossHealth1;
            }
            _Boss.Update(_Turtle);
        }

        if (_Turtle.TurtleStatus == 0)
        {
            SplashKit.CurrentWindow().DrawText($"You died!  Final Score: " + ScoreCounter, Color.Black, x: 150, y: 40);
            SplashKit.CurrentWindow().Refresh(60);
            SplashKit.Delay(8000);
            _Turtle.Quit = true;
        }

        UpdateBombs();
        Score();

        if (_myTimer.Ticks % 5000 == 0)
        {
            Difficulty += 0.002;                //Difficulty field increases amount of enemies spawning every 5 seconds.
        }

        if (SplashKit.Rnd() < (0.001 + Difficulty))
        {
            _Enemies.Add(new Squid(_Turtle));
        }
        if (SplashKit.Rnd() > (0.999 - Difficulty))
        {
            _Enemies.Add(new Jelly(_Turtle));
        }
        if (SplashKit.Rnd() < 0.998 & SplashKit.Rnd() > (0.997 - Difficulty))
        {
            _Enemies.Add(new Puffer(_Turtle));
        }

        if (SplashKit.Rnd() < 0.001)
        {
            _Background.Add(new Rock());
        }
        if (SplashKit.Rnd() > 0.999)
        {
            _Background.Add(new Seaweed());
        }

        if (SplashKit.Rnd() < 0.005)
        {
            _Background.Add(new Sand2());
        }
        if (SplashKit.Rnd() < 0.0005)
        {
            _Collectibles.Add(new Goldie());
        }
        if (SplashKit.Rnd() > 0.9995)
        {
            _Collectibles.Add(new Urchin());
        }

        if (_myTimer.Ticks > 120000 && _bossPresent == false)    //Creates the boss fight after two minutes have passed.
        {
            _backgroundMusic.Stop();
            _backgroundMusic = _sandstorm;
            _roar.Play();
            _bossPresent = true;
            _Boss = new Boss(_Turtle);
            _bossHealth = 20;
            SplashKit.CurrentWindow().Refresh(60);
        }

    }

    private void CheckCollisions() //Accounts for picking up items, hitting/shooting enemies.
    {

        for (int i = 0; i < _Enemies.Count; i++)
            if (_Turtle.CollidedWith(_Enemies[i]))
            {
                _ouch.Play();
                _Turtle.TurtleStatus -= 1;

                _RemoveEnemies.Add(_Enemies[i]);
            }

        if (_bossPresent)
        {
            if (_Turtle.CollidedWith(_Boss))
            {
                _ouch.Play();
                _Turtle.TurtleStatus -= 1;

            }
        }

        if (_bossPresent)
        {
            for (int i = 0; i < _Bubbles.Count; i++)
                if (_Bubbles[i].CollidedWith(_Boss))
                {
                    _roar.Play();
                    killedEnemies += 100;
                    _RemoveBubbles.Add(_Bubbles[i]);
                    _bossHealth -= 1;
                }
        }

        for (int i = 0; i < _Collectibles.Count; i++)
            if (_Turtle.CollidedWith(_Collectibles[i]))
            {
                if (_Collectibles[i].GetType() == typeof(Goldie))
                {
                    _yum.Play();
                    killedEnemies += 50;
                    if (_Turtle.TurtleStatus<3) //Powers up turtle to a maximum of three levels.
                    {
                        _Turtle.TurtleStatus += 1;
                    }                    
                }

                else
                {
                    _ooh.Play();
                    killedEnemies += 50;
                    if (_bombStatus < 3)  //Adds bombs to a maximum of three.
                    {
                        _bombStatus += 1;
                    }
                }
                _RemoveCollectibles.Add(_Collectibles[i]);
            }

        for (int i = 0; i < _Bubbles.Count; i++)
            for (int j = 0; j < _Enemies.Count; j++)
                if (_Bubbles[i].CollidedWith(_Enemies[j]))
                {
                    killedEnemies += 10;
                    _RemoveBubbles.Add(_Bubbles[i]);
                    _RemoveEnemies.Add(_Enemies[j]);
                }

        for (int i = 0; i < _Enemies.Count; i++)
            if (_Enemies[i].IsOffScreen())
            {
                _RemoveEnemies.Add(_Enemies[i]);
            }

        for (int i = 0; i < _Collectibles.Count; i++)
            if (_Collectibles[i].IsOffScreen())
            {
                _RemoveCollectibles.Add(_Collectibles[i]);
            }

        for (int i = 0; i < _RemoveCollectibles.Count; i++)
            if (_Collectibles.Contains(_RemoveCollectibles[i]))
            {
                _Collectibles.Remove(_RemoveCollectibles[i]);
            }


        for (int i = 0; i < _RemoveEnemies.Count; i++)
            if (_Enemies.Contains(_RemoveEnemies[i]))
            {
                _Enemies.Remove(_RemoveEnemies[i]);
            }

        for (int i = 0; i < _Bubbles.Count; i++)
            if (_Bubbles[i].IsOffScreen())
            {
                _RemoveBubbles.Add(_Bubbles[i]);
            }

        for (int i = 0; i < _RemoveBubbles.Count; i++)
            if (_Bubbles.Contains(_RemoveBubbles[i]))
            {
                _Bubbles.Remove(_RemoveBubbles[i]);
            }
    }

    private void UpdateBombs()
    {
        Bitmap _1Bomb = new Bitmap("1Bomb", "1Bomb.png");
        Bitmap _2Bombs = new Bitmap("2Bombs", "2Bombs.png");
        Bitmap _3Bombs = new Bitmap("3Bombs", "3Bombs.png");

        if (_bombStatus == 3)
        {
            _bombsRemaining = _3Bombs;
        }
        if (_bombStatus == 2)
        {
            _bombsRemaining = _2Bombs;
        }
        if (_bombStatus == 1)
        {
            _bombsRemaining = _1Bomb;
        }
    }
    private void Score()
    {
        if (_myTimer.Ticks % 10 == 0)
        {
            ScoreCounter = (_myTimer.Ticks / 1000) + killedEnemies;
        }
    }

}