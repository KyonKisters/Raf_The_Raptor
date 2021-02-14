using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine
using TiledMapParser;

//Cleaning up code = CTRL + K + D
//Commenting = CTRL + K + C
//Uncommenting = CTRL + K + U

public class MyGame : Game
{
    Level level;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        level = new Level("Level2.tmx", this);
        AddChild(level);
    }
    #endregion
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}