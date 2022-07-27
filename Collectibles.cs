using System;
using SplashKitSDK;

public abstract class Collectible  //Contains information for two types (child classes) of collectible.
{
    protected double X { get; set; }
    protected double Y { get; set; }
    protected Vector2D Velocity { get; set; }

    protected int Width
    {
        get { return 50; }
    }
    protected int Height
    {
        get { return 50; }
    }
    public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X, Y, 15); }
    }
    public Collectible() 
    {       
    }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }
    public abstract void Draw();
    public bool IsOffScreen()
    {
        bool offscreen;

        offscreen = X < -Width || X > SplashKit.CurrentWindowWidth() || Y < -Height || Y > SplashKit.CurrentWindowHeight();

        return offscreen;
    }

}
public class Urchin : Collectible //Urchin child class of Collectible.
{
    private Bitmap _urchinBitmap = new Bitmap("urchin", "urchin.png");

    public Urchin()
    {
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(SplashKit.CurrentWindowWidth());

            if (SplashKit.Rnd() < 0.5)
                Y = -Height;
            else
                Y = SplashKit.CurrentWindowHeight();
        }
        else
        {
            Y = SplashKit.Rnd(SplashKit.CurrentWindowHeight());

            if (SplashKit.Rnd() < 0.5)
                X = -Width;
            else
                X = SplashKit.CurrentWindowWidth();
        }

        const int SPEED = 5;     //Urchins move faster than goldies.

        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = SplashKit.Rnd(SplashKit.CurrentWindowWidth()),
            Y = SplashKit.Rnd(SplashKit.CurrentWindowHeight())
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);

    }
    public override void Draw()
    {
        SplashKit.DrawBitmap(_urchinBitmap, X - 18, Y - 18);


    }
}
public class Goldie : Collectible //Goldie child class of collectible.
{

    private Bitmap _goldieBitmap = new Bitmap("Goldie", "goldfish.png");
    public Goldie()
    {
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(SplashKit.CurrentWindowWidth());

            if (SplashKit.Rnd() < 0.5)
                Y = -Height;
            else
                Y = SplashKit.CurrentWindowHeight();
        }
        else
        {
            Y = SplashKit.Rnd(SplashKit.CurrentWindowHeight());

            if (SplashKit.Rnd() < 0.5)
                X = -Width;
            else
                X = SplashKit.CurrentWindowWidth();
        }

        const int SPEED = 1;

        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = SplashKit.Rnd(SplashKit.CurrentWindowWidth()),
            Y = SplashKit.Rnd(SplashKit.CurrentWindowHeight())
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }
    public override void Draw()
    {
        SplashKit.DrawBitmap(_goldieBitmap, X - 30, Y - 20);
    }
}
