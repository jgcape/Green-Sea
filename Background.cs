using System;
using SplashKitSDK;

public abstract class Background //Class for background with three child classes seaweed, rock and sand.
{
    protected double X { get; set; }
    protected double Y { get; set; }

    public int Height
    {
        get
        {
            return _rockBitmap.Height;
        }
    }
    protected Bitmap _seaweedBitmap;
    protected Bitmap _rockBitmap;
    protected Bitmap _sandBitmap;

    public Background()
    {
        _seaweedBitmap = new Bitmap("Seaweed", "seaweed.png");
        _rockBitmap = new Bitmap("Rock", "rock.png");
        _sandBitmap = new Bitmap("Sand", "seafloor.png");

        X = SplashKit.Rnd(SplashKit.CurrentWindowWidth());      //Spawns a background item at a random point at the top of the screen. Sand does not use this constructor.
        Y = -5;
    }

    public virtual void Update()
    {
        Y += .5;
    }
    public abstract void Draw();

    public bool IsOffScreen()   //All background objects share offscreen method.
    {
        bool offscreen;

        offscreen = Y > (SplashKit.CurrentWindowHeight() + 20);

        return offscreen;
    }
}

public class Seaweed : Background
{
    public override void Draw()
    {
        SplashKit.DrawBitmap(_seaweedBitmap, X, Y);
    }
}

public class Rock : Background
{
    public override void Draw()
    {
        SplashKit.DrawBitmap(_rockBitmap, X, Y);
    }
}

public class Sand : Background
{
    public Sand()
    {
        X = 0;
        Y = 0;
    }
    public override void Draw()
    {
        SplashKit.DrawBitmap(_sandBitmap, X, Y);
    }

}
public class Sand2 : Background
{
    public Sand2()
    {
        X = 0;
        Y = -800;
    }
    public override void Draw()
    {
        SplashKit.DrawBitmap(_sandBitmap, X, Y);
    }

}