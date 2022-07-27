using System;
using SplashKitSDK;

public abstract class Enemy  //Contains information for three types (child classes) of enemy.
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
    public virtual Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X, Y, 15); }
    }
    public Enemy(Turtle turtle)
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

        const int SPEED = 4;

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
public class Squid : Enemy
{
    private Bitmap _squidBitmap = new Bitmap("Squid", "squid.png");

    public Squid(Turtle turtle) : base(turtle)
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

        const int SPEED = 5;

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
    public override void Draw()
    {
        SplashKit.DrawBitmap(_squidBitmap, X - 36, Y - 50);


    }
}
public class Jelly : Enemy
{
    public override Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X, Y, 30); }
    }

    private Bitmap _jellyBitmap = new Bitmap("Jelly", "jelly.png");
    public Jelly(Turtle turtle) : base(turtle)
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
            X = turtle.X,
            Y = turtle.Y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }
    public override void Draw()
    {

        SplashKit.DrawBitmap(_jellyBitmap, X - 36, Y - 50);
    }
}
public class Puffer : Enemy
{
    private Bitmap _pufferBitmap = new Bitmap("Puffer", "puffer.png");

    public Puffer(Turtle turtle) : base(turtle)
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

        const int SPEED = 3;

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
    public override void Draw()
    {
        SplashKit.DrawBitmap(_pufferBitmap, X - 26, Y - 23);
    }
}
