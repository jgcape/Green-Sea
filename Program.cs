using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Green Sea", 800, 800); //Creates a window object.        
        TitleScreen titleScreen = new TitleScreen(); //Creates the title screen.

        while (!titleScreen._startGame)
        {
            SplashKit.ProcessEvents();
            titleScreen.HandleInput();
            titleScreen.Draw();

            if (SplashKit.QuitRequested())
            {               
                break;
            }

        }       
        

        GreenSea greenSea = new GreenSea();   //Instantiating a new game object. 


        while (!greenSea.Quit)
        {
            SplashKit.ProcessEvents();
            greenSea.HandleInput();
            greenSea.Update();
            greenSea.Draw();
        }
    }
}
