using System;
using SplashKitSDK;

public class Boss
{
    //Boss Attributes, bitmaps and coordinate data.  

    public Bitmap _bossBitmap {get; set;}
    public Bitmap _bossHealthBitmap {get; set;}
    public Bitmap _bossHealth1 = new Bitmap("bosshealth1", "brucehealth1.png");
    public Bitmap _bossHealth2 = new Bitmap("bosshealth2", "brucehealth2.png");
    public Bitmap _bossHealth3 = new Bitmap("bosshealth3", "brucehealth3.png");
    public Bitmap _bossHealth4 = new Bitmap("bosshealth4", "brucehealth4.png");
    public Bitmap _bossHealthDead = new Bitmap("bossdead", "brucedead.png");
    public Bitmap _bossDead = new Bitmap("deadboss", "sharkbossdead.png"); 
    
    
    

    private double X { get; set; }
    private double Y { get; set; }

    const int SPEED = 5;    

    private Vector2D Velocity { get; set; }
   
    private int Width
    {
        get
        {
            return _bossBitmap.Width;
        }
    }
    private int Height
    {
        get
        {
            return _bossBitmap.Height;
        }
    }
    public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X, Y, 80); }
    }
    public Boss(Turtle turtle) //Boss Constructor, initialise boss location, direction, and bitmaps for health bar and boss character.
    {
        _bossHealthBitmap = new Bitmap("bossfullhealth", "brucehealth5.png");
        _bossBitmap = new Bitmap("boss", "sharkboss.png");

        
        X = 400;
        Y = 200;

        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = turtle.X,
            Y = turtle.Y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public void Update(Turtle turtle)
    {
        const int GAP = 10;             //Conditions to keep the boss on screen, heads back at turtle when hits the edge.

        if (X < GAP || Y < GAP || X > SplashKit.CurrentWindowWidth() - Width - GAP || Y > SplashKit.CurrentWindowHeight() - Height - GAP)  
        {
            Point2D fromPt = new Point2D()
            {
                X = X,
                Y = Y
            };

            Point2D toPt = new Point2D()
            {
                X = turtle.X,
                Y = turtle.Y
            };

            Vector2D dir;
            dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            Velocity = SplashKit.VectorMultiply(dir, SPEED);
        }       

        X += Velocity.X;
        Y += Velocity.Y;
    }


    public void Draw() //Draws the boss and the boss health bar to the window.
    {
        SplashKit.DrawBitmap(_bossHealthBitmap, 100, 50);
        SplashKit.DrawBitmap(_bossBitmap, X - 180, Y - 120);
    }  
  
}

