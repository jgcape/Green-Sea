using System;
using SplashKitSDK;

public class Bubble      //Information for bubble projectiles.
{
    private Bitmap _bubbleBitmap;
    private SoundEffect bubble = new SoundEffect("Bubble", "bubble.wav");
    private double X { get; set; }
    private double Y { get; set; }
    private Vector2D Velocity { get; set; }

    private int Width
    {
        get { return _bubbleBitmap.Width; }
    }
    private int Height
    {
        get { return _bubbleBitmap.Height; }
    }
    public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X, Y, 20); }
    }

    public Bubble(Turtle turtle)
    {
        _bubbleBitmap = new Bitmap("Bubble", "bubble.png");

        bubble.Play();
        X = turtle.X + 36;
        Y = turtle.Y;         
    }

    public void Update()
    {
        Y -= 8;
    }
    public void Draw()
    {
        SplashKit.DrawBitmap(_bubbleBitmap, X, Y);
    }

    public bool IsOffScreen()
    {
        bool offscreen;
        offscreen = X < -Width || X > SplashKit.CurrentWindowWidth() || Y < -Height || Y > SplashKit.CurrentWindowHeight();
        return offscreen;
    }
    public bool CollidedWith(Enemy enemy)
    {
        { return SplashKit.CirclesIntersect(CollisionCircle, enemy.CollisionCircle); }
    }
     public bool CollidedWith(Boss boss)
    {
        { return SplashKit.CirclesIntersect(CollisionCircle, boss.CollisionCircle); }
    }
}


