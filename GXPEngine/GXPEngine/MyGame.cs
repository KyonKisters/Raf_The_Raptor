using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine
using TiledMapParser;
//Cleaning up code = CTRL + K + D
//Commenting = CTRL + K + C
//Uncommenting = CTRL + K + U

public class MyGame : Game
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
        //enemy = new Enemy();
        //enemy = new Enemy(player);
        //AddChild(enemy);
    }

    void Update()
	{
	}
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