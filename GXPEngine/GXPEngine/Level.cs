using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


public class Level : GameObject
{
    BigMeatObject hugeMeat;
    MummyKiller mummykiller;
    Player player;
    List<Enemy> enemies = new List<Enemy>();
    List<TRex> trexs = new List<TRex>();
    EnemyBoss enemyBoss;
    Camera camera;
    PAAProjectile Pprojectile;
    EAAProjectile Eprojectile;
    MyGame _game;
    DugHole dughole;
    Check check;
    Sound nextLevel = new Sound("NextLevel.wav");
    List<Enemypack> enemypacks = new List<Enemypack>();
    int levelNumber;
    bool delete=false;
    bool dead = false;
    public int LVL;
    public int enemyDefeatedCounter;
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

        //Layer without collider
        loader.addColliders = false;
        loader.LoadTileLayers(0);
        //Layer with collider
        loader.addColliders = true;
        loader.LoadTileLayers(1);
        //Object layer connect with classes 
        loader.autoInstance = true;
        loader.LoadObjectGroups(0);

        if (levelNumber==2)
        {
            enemyBoss.createPlayer(player);
        }
        foreach (Enemy enemy in enemies)
        { 
        enemy.createPlayer(player);
        }
        check = new Check(player, levelNumber);

            foreach (TRex trex in trexs)
            {
                trex.createPlayer(player);   
            }
        foreach (Enemypack enemypack in enemypacks)
        {
            enemypack.createLevelInst(this);
            enemypack.getPlayer(player);
        }
        if (levelNumber == 3)
        {
            mummykiller.createLevelInst(this);
            mummykiller.createPlayer(player);
        }
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
            enemies.Add(enemy);
            enemy.createLevelInst(this);
        }
        if (sprite is EnemyBoss enemyBoss)
        {
            this.enemyBoss = enemyBoss;
            AddChild(enemyBoss);
            enemyBoss.createLevelInst(this);
        }
        if (sprite is TRex trex)
        {
            trexs.Add(trex);
            trex.createLevelInst(this);
        }
        if (sprite is Enemypack enemypack)
        {
            enemypacks.Add(enemypack);
        }
        if (sprite is MummyKiller mummyKiller)
        {
            this.mummykiller = mummyKiller;
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Load Level
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Load level
    /// </summary>
    #region Load Level
    public void LoadLevel()
    {
        levelNumber++;
        delete = true;
        nextLevel.Play(false, 0, 0.3f);
        _game.LoadLevel("Level" + levelNumber + ".tmx", levelNumber, delete);
        delete = false;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Create Projectiles
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Creates the projectiles so that this Game is the parent
    /// </summary>
    #region create projectiles
    public void Attack(string facing, float x, float y, bool PlayerAttack, bool EnemyAttack,bool stonethrow)
    {
        if (PlayerAttack)
        {
            if (stonethrow)
            {
                Pprojectile = new PAAProjectile(facing, x, y);
                AddChild(Pprojectile);
                Pprojectile.lifetime = 60;
                Pprojectile.alpha = 1;
                Pprojectile.speed = 6;
                Pprojectile.creatLevel(this);
            }
            if (!stonethrow)
            {
                Pprojectile = new PAAProjectile(facing, x, y);
                AddChild(Pprojectile);
                Pprojectile.lifetime = 3;
                Pprojectile.speed = 20;
                Pprojectile.alpha=0;
                Pprojectile.creatLevel(this);
            }
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
    public void placeMeat(string facing, float x, float y)
    {
        hugeMeat = new BigMeatObject(facing, x, y);
        AddChild(hugeMeat);
        foreach (TRex trex in trexs)
        {
            trex.createMeat(hugeMeat);
        }

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
                _game.LoadLevel("Level" + levelNumber + ".tmx", levelNumber,player.delete);
                gameover.LateDestroy();
            }
        }
        if (enemyDefeatedCounter == 5)
        {
            nextLevel.Play();
            foreach (Enemypack enemypack in enemypacks)
            {
                enemypack.EnemiesDefeated = true;
            }
            enemyDefeatedCounter = 6;
        }
    }
}
