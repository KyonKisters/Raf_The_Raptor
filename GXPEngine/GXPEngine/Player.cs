using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


public class Player : AnimationSprite
{
    float speed = 4f;
    bool attack;
    string facing;
    Level level;
    MyGame _game;
    int levelnumber;
    bool change;
    int smallmeat;
    public int life = 5;

    public enum Direction { TOP, DOWN, RIGHT, LEFT };
    public Direction Facing;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the player
    /// </summary>
    #region Constructor
    public Player(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Instances
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Instances of other classes
    /// </summary>
    #region Instances
    public void createLevelInst(Level level)
    {
        this.level = level;
    }
    public void createGameInst(MyGame game)
    {
        this._game = game;
    }
    public void giveLevelNumber(int levelnumber)
    {
        this.levelnumber = levelnumber;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Movement
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Movement of the player
    /// </summary>
    #region Movement

    void Movement()
    {
        float moveX = 0;
        float moveY = 0;
        Console.WriteLine(life);
        if (Input.GetKey(Key.A) || Input.GetKey(Key.LEFT))
        {
            Facing = Direction.LEFT;
            moveX = -speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
        {
            Facing = Direction.RIGHT;
            moveX = speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.W) || Input.GetKey(Key.UP))
        {
            Facing = Direction.TOP;
            moveX = 0;
            moveY = -speed;
        }
        if (Input.GetKey(Key.S) || Input.GetKey(Key.DOWN))
        {
            Facing = Direction.DOWN;
            moveX = 0;
            moveY = speed;
        }
        Collision collision = MoveUntilCollision(moveX, moveY); //You can move until collision, for example with Tiled Map
        if (collision != null)
        {
            handleCollision(collision);
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Collision
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Collision of the player
    /// </summary>
    #region Collision

    void handleCollision(Collision col)
    {
        Console.WriteLine(col.other.name);
        if (col.other is Tunnel)
        {
            if (levelnumber <= 3 && !change)
            {
                levelnumber++;
            }

            if (levelnumber == 4)
            {
                change = true;
            }

            if (change)
            {
                levelnumber = 1;
            }
            _game.LoadLevel("Level" + levelnumber + ".tmx", levelnumber);
        }
        if (col.other is SmallMeat)
        {
            smallmeat++;
            col.other.LateDestroy();
            if (smallmeat == 3)
            {

            }
        }
        if (col.other is LittleStone)
        {
            col.other.LateDestroy();
            attack = true;
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Attacks
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Attacks of the player
    /// </summary>
    #region Attack
    void Attack()
    {
        if (Input.GetKey(Key.E) & attack)
        {
            facing = Facing.ToString();
            level.Attack(facing, this.x, this.y);
            attack = false;
        }
    }

    #endregion
    //----------------------------------------------------------------------------------------
    //                                        DugHole
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Dugs a hole in front thats not passable
    /// </summary>
    #region Dug hole
    void dugHole()
    {
        if (Input.GetKey(Key.Q))
        {
            facing = Facing.ToString();
            level.dugHoles(facing, this.x, this.y);
        }
    }


    #endregion
    void Update()
    {
        Movement();
        Attack();
        dugHole();
    }
}
