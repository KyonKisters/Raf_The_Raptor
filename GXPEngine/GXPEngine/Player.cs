using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


public class Player : AnimationSprite
{
    bool walkSoundON;
    float walkSoundTimer;
    float speed = 6f;
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
    public bool delete = false;
    HealthBar healthbar= new HealthBar();
    HUDSmallMeat hudsmallmeat = new HUDSmallMeat();
    Sound collect = new Sound("item-collect.wav");
    Sound collectStone = new Sound("Rock.wav");
    Sound stonethrow = new Sound("throwingrock.wav");
    Sound nextLevel = new Sound("NextLevel.wav");
    Sound eat = new Sound("eat.wav");
    //Sound walking = new Sound("walk-sound.wav");
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
        walkSoundTimer++;

        if (walkSoundON & walkSoundTimer > 50)
        {
            walkSoundTimer = 0;
        }
        if (Input.GetKey(Key.A) || Input.GetKey(Key.LEFT))
        {
            walkSoundON = true;
            stopOtherAnimation = false;
            Facing = Direction.LEFT;
            moveX = -speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
        {
            walkSoundON = true;
            stopOtherAnimation = false;
            Facing = Direction.RIGHT;
            moveX = speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.W) || Input.GetKey(Key.UP))
        {
            walkSoundON = true;
            stopOtherAnimation = false;
            Facing = Direction.TOP;
            moveX = 0;
            moveY = -speed;
        }
        if (Input.GetKey(Key.S) || Input.GetKey(Key.DOWN))
        {
            walkSoundON = true;
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
        if (!stopOtherAnimation)
        {
            if (facing == "LEFT")
            {
                if (levelnumber == 1)
                {
                    Mirror(true, false);
                    SetCycle(1, 3);
                }
                if (levelnumber == 2 | levelnumber == 3)
                {
                    Mirror(false, false);
                    SetCycle(1, 4);
                }
            }

            if (facing == "RIGHT")
            {
                if (levelnumber == 1)
                {
                    Mirror(false, false);
                    SetCycle(1, 4);
                }
                if (levelnumber == 2 | levelnumber == 3)
                {
                    Mirror(true, false);
                    SetCycle(1, 4);
                }
            }

            if (facing == "TOP")
            {
                if (levelnumber == 1)
                {
                    SetCycle(15, 3);
                }
                if (levelnumber == 2)
                {
                    SetCycle(11, 2);
                }
                if (levelnumber == 3)
                {
                    SetCycle(11, 3);
                }
            }
            if (facing == "DOWN")
            {
                if (levelnumber == 1)
                {
                    SetCycle(8, 3);
                }
                if (levelnumber == 2)
                {
                    SetCycle(6, 2);
                }
                if (levelnumber == 3)
                {
                    SetCycle(6, 3);
                }
            }
        }
        if (levelnumber == 1)  //Idle Animation
        {
            if (Input.GetKeyUp(Key.A) | Input.GetKeyUp(Key.D))
            {
                stopOtherAnimation = true;
                SetCycle(5, 2);
            }
            if (Input.GetKeyUp(Key.W))
            {
                stopOtherAnimation = true;
                SetCycle(18, 2);
            }
            if (Input.GetKeyUp(Key.S))
            {
                stopOtherAnimation = true;
                SetCycle(12, 2);
            }
        }
        if (levelnumber == 2)  //Idle Animation
        {
            if (Input.GetKeyUp(Key.A) | Input.GetKeyUp(Key.D))
            {
                stopOtherAnimation = true;
                SetCycle(14,2);
            }
            if (Input.GetKeyUp(Key.W))
            {
                stopOtherAnimation = true;
                SetCycle(10, 1);
            }
            if (Input.GetKeyUp(Key.S))
            {
                stopOtherAnimation = true;
                SetCycle(6, 1);
            }
        }

            if (animationtimer > 12 & !stopOtherAnimation)// Walk Animation time
        {
            NextFrame();
            animationtimer = 0;
        }
        if (animationtimer > 30 & stopOtherAnimation)// Idle Animation time
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
            delete = true;
            nextLevel.Play(false,0,0.3f);
            _game.LoadLevel("Level" + levelnumber + ".tmx", levelnumber,delete);
            delete = false;
        }
        if (col.other is SmallMeat)
        {
            if (levelnumber == 1)
            {
                collect.Play();
            }
            else eat.Play();
            smallmeat++;
            
            col.other.LateDestroy();
            if (smallmeat == 3)
            {

            }
        }
        if (col.other is LittleStone)
        {
            collectStone.Play();
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
            stonethrow.Play();
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
