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
    Camera camera;
    EAAProjectile projectile;
    MyGame _game;
    DugHole dughole;
    Check check;
    int levelNumber;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public Level(string filename, MyGame game,int levelnumber) : base (false)
        {

        _game = game;
        this.levelNumber = levelnumber;

        createLevel(filename);
        AddChild(check);

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

        Console.WriteLine();
        //Layer without collider
        loader.addColliders = false;
        loader.LoadTileLayers(0);
        //Layer with collider
        loader.addColliders = true;
        loader.LoadTileLayers(1);
        //Object layer connect with classes 
        loader.autoInstance = true;
        loader.LoadObjectGroups(0);
        
        enemy.createPlayer(player);
        check = new Check(player,levelNumber);
    }
    private void Loader_OnObjectCreated(Sprite sprite, TiledObject obj)
    {
        if (sprite is Player player)
        {
            this.player = player;
            this.player.createLevelInst(this);
            this.player.createGameInst(_game);
            this.player.giveLevelNumber(levelNumber);
            camera = new Camera(0, 0, 800, 600);
            this.player.AddChild(camera);
        }
        if (sprite is Enemy enemy)
        {
            this.enemy = enemy;
            this.enemy.createLevelInst(this);
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
        projectile = new EAAProjectile(facing, x, y,player,enemy);
        AddChild(projectile);
    }
    public void dugHoles(string facing, float x, float y)
    {
        dughole = new DugHole(facing, x, y);
        AddChild(dughole);
    }
    public void CheckBox(string facing, float x, float y)
    {
        check.checkCollision(facing,x,y);
    }
    #endregion
}
