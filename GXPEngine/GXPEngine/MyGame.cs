using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine
using TiledMapParser;

//Cleaning up code = CTRL + K + D
//Commenting = CTRL + K + C
//Uncommenting = CTRL + K + U

public class MyGame : Game
{
    Player player;
    Camera camera;
    EAAProjectile projectile;
    Enemy enemy;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        LoadMap("Level2.tmx");
        player = new Player();
        player.createGame(this);
        AddChild(player);
        this.enemy.createPlayer(player);

        camera = new Camera(0, 0, 800, 600);
        player.AddChild(camera);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Load Tiled map
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Loading Tiled map components
    /// </summary>
    #region Tiledmap loading
    void LoadMap(string filename) //LoadMap function, add colliders to layers in Tiled
    {
        TiledLoader loader = new TiledLoader(filename);
        loader.OnObjectCreated += Loader_OnObjectCreated;

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

    private void Loader_OnObjectCreated(Sprite sprite, TiledObject obj)
    {
        if (sprite is Enemy enemy)
        {
            this.enemy = enemy;
            this.enemy.createGame(this);
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Create Projectiles
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Creates the projectiles so that this Game is the parent
    /// </summary>
  #region create projectiles
    public void Attack(string facing, float x, float y)
    {
        projectile = new EAAProjectile(facing,x,y);
        AddChild(projectile);
    }
    #endregion
    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}