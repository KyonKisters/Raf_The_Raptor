using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


    public class Level : GameObject
    {
    Player player;
    Enemy enemy;
    MyGame _game;
    Camera camera;
    EAAProjectile projectile;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public Level(string filename, MyGame game,int levelnumber) : base (false)
        {

        createLevel(filename);
        this._game = game;

        string currentLevel = filename;

        player = new Player(game,currentLevel,levelnumber);
        player.createGame(this);

        AddChild(player);
        this.enemy.createPlayer(player);


        camera = new Camera(0, 0, 800, 600);
        player.AddChild(camera);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Create Level
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Create level
    /// </summary>
    #region Create Level
    void createLevel(string filename)
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
        projectile = new EAAProjectile(facing, x, y);
        AddChild(projectile);
    }
    #endregion
}
