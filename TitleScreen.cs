using System;
using SplashKitSDK;
using System.Collections.Generic;


public class TitleScreen
{
    //Loads bitmaps for titlescreen.
    private Bitmap _title = new Bitmap("Title", "title.png");
    private Bitmap _start = new Bitmap("Start", "start.png");
    private Bitmap _instructions = new Bitmap("Instructions", "instructions.png");
    private Bitmap _sandBitmap = new Bitmap("Sand", "seafloor.png");
    private Bitmap _readMe = new Bitmap("readme", "readme.png");

    //Circles for titlescreen buttons.
    public Circle StartButton
    {
        get { return SplashKit.CircleAt(250, 650, 50); }
    }
    public Circle Instructions
    {
        get { return SplashKit.CircleAt(550, 650, 50); }
    }

    public bool _startGame = false;

    private SoundEffect _pirateMusic = new SoundEffect("piratemusic", "piratemusic.wav");

    public TitleScreen()
    {
        _pirateMusic.Play();
    }

    public void Draw()
    {
        SplashKit.CurrentWindow().DrawBitmap(_sandBitmap, 0, 0);
        SplashKit.CurrentWindow().DrawBitmap(_title, 150, 100);
        SplashKit.CurrentWindow().DrawBitmap(_start, 200, 600);
        SplashKit.CurrentWindow().DrawBitmap(_instructions, 500, 600);
        SplashKit.CurrentWindow().Refresh(60);
    }

    public void HandleInput()
    {
        if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseOverStart)
        {
            _startGame = true;
            _pirateMusic.Stop();
        }

        if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseOverInstructions)
        {
            while (!SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                SplashKit.ProcessEvents();
                SplashKit.CurrentWindow().DrawBitmap(_readMe, 0, 0);
                SplashKit.CurrentWindow().Refresh(60);
            }
        }
    }
    public bool IsMouseOverStart
    {
        get
        {
            return SplashKit.PointInCircle(SplashKit.MousePosition(), StartButton);
        }
    }
    public bool IsMouseOverInstructions
    {
        get
        {
            return SplashKit.PointInCircle(SplashKit.MousePosition(), Instructions);
        }
    }
}
