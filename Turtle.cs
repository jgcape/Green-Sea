using System;
using SplashKitSDK;

public class Turtle
{
    //Turtle Attributes   
    private Bitmap _turtleBitmap;
    private Bitmap _turtleBitmap1 = new Bitmap("Turtle1", "Turtle.png");
    private Bitmap _turtleBitmap2 = new Bitmap("Turtle2", "Turtle2.png");
    private Bitmap _turtleBitmap3 = new Bitmap("Turtle3", "Turtle3.png");
    public double X { get; private set; } //turtle location
    public double Y { get; private set; } //turtle location

    public int TurtleStatus {get; set;}  

    public bool Quit { get; set; }
    public int Width
    {
        get
        {
            return _turtleBitmap.Width;
        }
    }
    public int Height
    {
        get
        {
            return _turtleBitmap.Height;
        }
    }
    public Turtle() //Turtle Constructor
    {
        TurtleStatus = 1;   
        _turtleBitmap = _turtleBitmap1;

        X = (SplashKit.CurrentWindowWidth() - Width) / 2;
        Y = (SplashKit.CurrentWindowHeight() - Height) / 2;
    }

    public void Update()
    {
        
        if (TurtleStatus == 1)
        {
            _turtleBitmap = _turtleBitmap1;
        }
        if (TurtleStatus == 2)
        {
            _turtleBitmap = _turtleBitmap2;
        }

        if (TurtleStatus == 3)
        {
            _turtleBitmap = _turtleBitmap3;
        }

    }


    public void Draw() //Draw method
    {
        SplashKit.DrawBitmap(_turtleBitmap, X, Y);
    }

    public void HandleInput() // Handles input from turtle
    {
        double speed = 5;
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }
        if (SplashKit.QuitRequested())
        {
            Quit = true;
        }
        if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y -= speed;
        }
        if (SplashKit.KeyDown(KeyCode.UpKey) && SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            Y -= (2 * speed);
        }
        if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y += speed;
        }
        if (SplashKit.KeyDown(KeyCode.DownKey) && SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            Y += (2 * speed);
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X -= speed;
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey) && SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            X -= (2 * speed);
        }
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X += speed;
        }
        
        if (SplashKit.KeyDown(KeyCode.RightKey) && SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            X += (2 * speed);
        }
    }

    public void StayOnWindow()  //Method to stop the turtle going outside the screen.
    {
        const int GAP = 10;
        if (X < GAP)
        {
            X = GAP;
        }
        if (Y < GAP)
        {
            Y = GAP;
        }
        if (X > SplashKit.CurrentWindowWidth() - Width - GAP)
        {
            X = SplashKit.CurrentWindowWidth() - Width - GAP;
        }
        if (Y > SplashKit.CurrentWindowHeight() - Height - GAP)
        {
            Y = SplashKit.CurrentWindowHeight() - Height - GAP;
        }
    }
    public bool CollidedWith(Enemy enemy)         //Overloaded CollidedWith method accounts for the turtle running into any in-game objects.
    {
        { return _turtleBitmap.CircleCollision(X, Y, enemy.CollisionCircle); }
    }
     public bool CollidedWith(Boss boss)
    {
        { return _turtleBitmap.CircleCollision(X, Y, boss.CollisionCircle); }
    }
      public bool CollidedWith(Collectible collectible)
    {
        { return _turtleBitmap.CircleCollision(X, Y, collectible.CollisionCircle); }
    }
}
