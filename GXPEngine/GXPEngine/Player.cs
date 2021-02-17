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
    public int life = 4;
    public bool cantDigAHole=false;
    int animationtimer=0;
    bool stopOtherAnimation = true;
    HealthBar healthbar= new HealthBar();
    HUDSmallMeat hudsmallmeat = new HUDSmallMeat();
    Enemy enemy;

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
        AddChild(healthbar);
        AddChild(hudsmallmeat);
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
    public void createEnemyInst(Enemy enemy)
    {
        this.enemy = enemy;
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

        if (Input.GetKey(Key.A) || Input.GetKey(Key.LEFT))
        {
            stopOtherAnimation = false;
            Facing = Direction.LEFT;
            moveX = -speed;
            moveY = 0;

        }
        if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
        {
            stopOtherAnimation = false;
            Facing = Direction.RIGHT;
            moveX = speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.W) || Input.GetKey(Key.UP))
        {
            stopOtherAnimation = false;
            Facing = Direction.TOP;
            moveX = 0;
            moveY = -speed;

        }
        if (Input.GetKey(Key.S) || Input.GetKey(Key.DOWN))
        {
            stopOtherAnimation = false;
            Facing = Direction.DOWN;
            moveX = 0;
            moveY = speed;
        }

        facing = Facing.ToString();
        level.CheckBox(facing, x, y);
        Collision collision = MoveUntilCollision(moveX, moveY); //You can move until collision, for example with Tiled Map
        if (collision != null)
        {
            handleCollision(collision);
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Animations
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Animation of the player
    /// </summary>
    #region Animations
    void HandleAnimation()
    {
        animationtimer++;
        if (levelnumber == 1 & facing == "LEFT" & !stopOtherAnimation)
        {
            Mirror(true, false);
            SetCycle(1, 3);
        }
        if ( (levelnumber == 2 | levelnumber == 3) & facing == "LEFT" & !stopOtherAnimation)
        {
            Mirror(false, false);
            SetCycle(1, 4);
        }
        if (levelnumber == 1 & facing == "RIGHT" & !stopOtherAnimation)
        {
            Mirror(false, false);
            SetCycle(1, 4);
        }
        if ( (levelnumber == 2 | levelnumber == 3) & facing == "RIGHT" & !stopOtherAnimation)
        {
            Mirror(true, false);
            SetCycle(1, 4);
        }
        if (levelnumber == 1 & facing == "TOP" & !stopOtherAnimation)
        {
            SetCycle(15, 3);
        }
        if (levelnumber == 2 & facing == "TOP" & !stopOtherAnimation)
        {
            SetCycle(11, 2);
        }
        if (levelnumber == 3 & facing == "TOP" & !stopOtherAnimation)
        {
            SetCycle(11, 3);
        }
        if (levelnumber == 1 & facing == "DOWN" & !stopOtherAnimation)
        {
            SetCycle(8, 3);
        }
        if (levelnumber == 2 & facing == "DOWN" & !stopOtherAnimation)
        {
            SetCycle(6, 2);
        }
        if (levelnumber == 3 & facing == "DOWN" & !stopOtherAnimation)
        {
            SetCycle(6, 3);
        }
        if ( (Input.GetKeyUp(Key.A) | Input.GetKeyUp(Key.D)) & levelnumber==1)
        {
            stopOtherAnimation = true;
            SetCycle(5, 2);
        }
        if (Input.GetKeyUp(Key.W) & levelnumber == 1)
        {
            stopOtherAnimation = true;
            SetCycle(18, 2);
        }
        if ( Input.GetKeyUp(Key.S) & levelnumber == 1)
        {
            stopOtherAnimation = true;
            SetCycle(12, 2);
        }

        if (animationtimer > 12 & !stopOtherAnimation)
        {
            NextFrame();
            animationtimer = 0;
        }
        if (animationtimer > 24 & stopOtherAnimation)
        {
            NextFrame();
            animationtimer = 0;
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
            level.Attack(facing, this.x, this.y,true,false);
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

        if (Input.GetKey(Key.Q) & !cantDigAHole)
        {
            facing = Facing.ToString();
            level.dugHoles(facing, x,y);
        }
    }


    #endregion
    void Update()
    {
        Movement();
        Attack();
        dugHole();
        HandleAnimation();
        hudsmallmeat.Update(smallmeat);
        healthbar.Update(life);
    }
}
