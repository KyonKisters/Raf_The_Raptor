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
    PAAProjectile Pprojectile;
    EAAProjectile Eprojectile;
    MyGame _game;
    DugHole dughole;
    Check check;
    int levelNumber;
    bool dead = false;
    Sprite gameover;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Game
    /// </summary>
    #region Constructor
    public Level(string filename, MyGame game, int levelnumber) : base(false)
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
        check = new Check(player, levelNumber);
        player.createEnemyInst(enemy);
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
    public void Attack(string facing, float x, float y, bool PlayerAttack, bool EnemyAttack)
    {
        if (PlayerAttack)
        {
            Pprojectile = new PAAProjectile(facing, x, y, enemy);
            AddChild(Pprojectile);
        }
        if (EnemyAttack)
        {
            Eprojectile = new EAAProjectile(facing, x, y, player, this);
            AddChild(Eprojectile);
        }
    }
    public void dugHoles(string facing, float x, float y)
    {
        dughole = new DugHole(facing, x, y);
        AddChild(dughole);
    }
    public void CheckBox(string facing, float x, float y)
    {
        check.checkCollision(facing, x, y);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Create Game Over Screen
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Create Game Over Screen
    /// </summary>
    #region create GameOver Screen
    public void createGameOverScreen()
    {
        gameover = new Sprite("Game-over-screen.png", false, false);
        AddChild(gameover);
        dead = true;
    }
    #endregion
    void Update()
    {
        if (dead)
        {
            if (Input.GetKey(Key.SPACE))
            {
                _game.LoadLevel("Level" + levelNumber + ".tmx", levelNumber);
                gameover.LateDestroy();
            }
        }
    }
}
