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
    int levelnumber = 1;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        level = new Level("Level" + levelnumber + ".tmx",this ,levelnumber);
        AddChild(level);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Load Levels
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Load the levels 
    /// </summary>
    #region Load Levels
    public void LoadLevel(string filename,int levelnumber)
    {
        Game.main.GetChildren().ForEach(DestroyGame);
        level = new Level(filename, this, levelnumber);
        AddChild(level);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Destroys Levels
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Destroys the levels
    /// </summary>
    #region Level destroying
    void DestroyGame(GameObject other)
    {
        other.LateRemove();
        other.LateDestroy();
    }
    #endregion
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
{
    Player player;
    Camera camera;
    Enemy enemy;

	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
        LoadMap("map.tmx");
        player = new Player();
        AddChild(player);
        camera = new Camera(0, 0, 800, 600);
        player.AddChild(camera);
    }

    void Update()
	{
	}
    //----------------------------------------------------------------------------------------
    //                                         Load Tiledmap
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Loading all parts of a Tiled map
    /// </summary>
    #region load Tiledmap
    void LoadMap(string filename) //LoadMap function, add colliders to layers in Tiled
    {
        TiledLoader loader = new TiledLoader(filename);
        //Layer without collider
        loader.addColliders = false;
        loader.LoadTileLayers(0);
        //Layer with collider
        loader.addColliders = true;
        loader.LoadTileLayers(1);
        //Object layer connect with classes 
        loader.autoInstance = true;
        loader.LoadObjectGroups(0);
    }

    static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}