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
    float speed = 4f;
    bool attack;
    bool sprint = true;
    public bool discoveredenemypack = false;
    string facing;
    int tutorialProgress=0;
    int tutorialTimer=0;
    int dashtimer = 0;
    int dashspeed;
    bool AAttack;
    int attacktimer;
    public bool discoveredEnemy = false;
    public bool discoveredTRex = false;
    public bool cantPlaceMeat = false;
    bool canDash = true;
    bool dashes = false;
    Level level;
    MyGame _game;
    public bool collectedMeat = false;
    public int levelnumber;
    bool change;
    int smallmeat;
    public int hugeMeat;
    public int life = 4;
    public bool cantDigAHole=false;
    int animationtimer=0;
    bool stopOtherAnimation = true;
    public bool delete = false;
    HealthBar healthbar= new HealthBar();
    HUDMeat hudsmallmeat = new HUDMeat();
    Sound collect = new Sound("item-collect.wav");
    Sound collectStone = new Sound("Rock.wav");
    Sound stonethrow = new Sound("throwingrock.wav");
    Sound nextLevel = new Sound("NextLevel.wav");
    Sound eat = new Sound("eat.wav");
    Sound dash = new Sound("dash.wav");
    Sound rafattack = new Sound("RAFATTACK.wav");
    Sprite imscared = new Sprite("imscared.png",false,false);
    Sprite pressWASD = new Sprite("WASD.png", false, false);
    Sprite eek = new Sprite("eekchat.png", false, false);
    Sprite hideOrRun = new Sprite("hidorrun.png", false, false);
    Sprite pressQ = new Sprite("PressQ.png", false, false);
    Sprite or = new Sprite("or.png", false, false);
    Sprite pressShift = new Sprite("shift.png", false, false);
    Sprite meat = new Sprite("meat.png", false, false);
    Sprite lure = new Sprite("lure.png", false, false);
    Sprite pressE = new Sprite("pressE.png", false, false);
    Sprite canhelpmeout = new Sprite("canhelpmeout.png",false,false);
    Sprite throwStone = new Sprite("throwStone.png",false,false);
    Sprite dontletmethrough = new Sprite("dontletmethrough.png",false,false);
    Sprite defeat5 = new Sprite("defeat5.png",false,false);
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
        imscared.x -= 50;
        imscared.y -= 100;
        pressWASD.x -= 50;
        pressWASD.y -= 100;
        eek.x -= 50;
        eek.y -= 100;
        hideOrRun.x -= 50;
        hideOrRun.y -= 100;
        pressQ.x -= 50;
        pressQ.y -= 100;
        or.x -= 50;
        or.y -= 100;
        pressShift.x -= 50;
        pressShift.y -= 100;
        meat.x -= 50;
        meat.y -= 100;
        lure.x -= 50;
        lure.y -= 100;
        pressE.x -= 50;
        pressE.y -= 100;
        canhelpmeout.x -= 50;
        canhelpmeout.y -= 100;
        throwStone.x -= 50;
        throwStone.y -= 100;
        dontletmethrough.x -= 50;
        dontletmethrough.y -= 100;
        defeat5.x -= 50;
        defeat5.y -= 100;
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
    //                                        Tutorial boxes
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Tutorial
    /// </summary>
    #region Tutorial
    void TutorialBoxes()
    {
        Console.WriteLine(tutorialProgress);
        if (levelnumber == 1)
        {
            if (tutorialProgress == 0)
            {
                AddChild(imscared);
                tutorialTimer++;
                if (tutorialTimer > 80)
                {
                    imscared.LateDestroy();
                    AddChild(pressWASD);
                    tutorialTimer = 0;
                    tutorialProgress++;
                }
            }
            if ((Input.GetKey(Key.W) | Input.GetKey(Key.A) | Input.GetKey(Key.S) | Input.GetKey(Key.D)) & tutorialProgress == 1)
            {
                pressWASD.LateDestroy();
                tutorialProgress++;
            }
            if (tutorialProgress == 2 & discoveredEnemy)
            {
                tutorialTimer++;
                if (tutorialTimer > 0 & tutorialTimer < 2)
                {
                    AddChild(eek);
                }
                if (tutorialTimer > 80 & tutorialTimer < 82)
                {
                    eek.LateDestroy();
                    AddChild(hideOrRun);
                }
                if (tutorialTimer > 200 & tutorialTimer < 202)
                {
                    hideOrRun.LateDestroy();
                    AddChild(pressQ);
                }
                if (tutorialTimer > 280 & tutorialTimer < 282)
                {
                    pressQ.LateDestroy();
                    AddChild(or);
                }
                if (tutorialTimer > 310 & tutorialTimer < 312)
                {
                    or.LateDestroy();
                    AddChild(pressShift);
                    tutorialTimer = 0;
                    tutorialProgress++;
                }
            }
            if ((Input.GetKey(Key.Q) | Input.GetKey(Key.LEFT_SHIFT) | Input.GetKey(Key.RIGHT_SHIFT)) & tutorialProgress == 3)
            {
                pressShift.LateDestroy();
                tutorialProgress++;
            }
            if (tutorialProgress == 4 & discoveredTRex)
            {
                tutorialTimer++;
                if (tutorialTimer > 0 & tutorialTimer < 2)
                {
                    AddChild(meat);
                }
                if (tutorialTimer > 80 & tutorialTimer < 82)
                {
                    meat.LateDestroy();
                    AddChild(lure);
                }
                if (tutorialTimer > 140 & tutorialTimer < 142)
                {
                    lure.LateDestroy();
                    tutorialProgress++;
                }
            }
            if (smallmeat == 3 & tutorialProgress == 5)
            {
                AddChild(pressE);
            }
            if (tutorialProgress == 5 & Input.GetKey(Key.E) & smallmeat == 3)
            {
                pressE.LateDestroy();
                tutorialProgress++;
                tutorialTimer = 0;
            }
        }
        if (levelnumber==2)
        {
            if (tutorialProgress == 6)
            {
                tutorialTimer++;
                if (tutorialTimer > 0 & tutorialTimer<2)
                {
                    AddChild(canhelpmeout);
                }
                if (tutorialTimer > 80 & tutorialTimer <82)
                {
                    canhelpmeout.LateDestroy();
                    AddChild(throwStone);
                }
                if (Input.GetKey(Key.E))
                {
                    tutorialProgress++;
                    tutorialTimer = 0;
                }
            }
            if (tutorialProgress==7)
            {
                tutorialTimer++;
                if (tutorialTimer>0 & tutorialTimer<2)
                {
                    throwStone.LateDestroy();
                    tutorialTimer = 0;
                    tutorialProgress++;
                }
            }
            if (tutorialProgress==8)
            {
                if (discoveredenemypack)
                {
                    tutorialTimer++;
                    if (tutorialTimer > 0 & tutorialTimer < 2)
                    {
                        AddChild(dontletmethrough);
                    }
                    if (tutorialTimer > 100 & tutorialTimer < 102)
                    {
                        dontletmethrough.LateDestroy();
                        AddChild(defeat5);
                    }
                    if (tutorialTimer > 280 & tutorialTimer < 282)
                    {
                        defeat5.LateDestroy();
                        tutorialProgress++;
                        tutorialTimer = 0;
                    }
                }
            }

        }
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
        if (levelnumber==1)
        {
            speed = 4f;
        }
        if (levelnumber == 2)
        {
            speed = 6f;
        }
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
        if (dashes)
        {
            if (sprint)
            {
                if (levelnumber == 1)
                {
                    dashspeed += 45;
                }
                if (levelnumber == 2)
                {
                    dashspeed += 25;
                }
                if (levelnumber == 3)
                {
                    dashspeed += 25;
                }
            }
            if (facing == "TOP")
            {
                moveY -= dashspeed;
            }
            if (facing == "DOWN")
            {
                moveY += dashspeed;
            }
            if (facing == "RIGHT")
            {
                moveX += dashspeed;
            }
            if (facing == "LEFT")
            {
                moveX -= dashspeed;
            }
            if (dashspeed >=90 & levelnumber==1)
            {
                sprint = false;
            }
            if (dashspeed >= 45 & levelnumber==2)
            {
                sprint = false;
            }
            if (dashspeed >= 45 & levelnumber == 3)
            {
                sprint = false;
            }
            if (!sprint)
            {
                dashspeed-=5;
                if (dashspeed<=0)
                {
                    sprint = true;
                    dashes = false;
                }
            }
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
                smallmeat = 0;
                hudsmallmeat.BigMeat = true;
                hugeMeat = 1;
            }
        }
        if (col.other is LittleStone)
        {
            collectStone.Play();
            col.other.LateDestroy();
            attack = true;
        }
        if (col.other is BigMeatObject)
        {
           hugeMeat++;
           collectedMeat = true;
           collect.Play();
           col.other.LateDestroy();
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
        dashtimer++;
        attacktimer++;
        if (levelnumber == 2 | levelnumber==3)
        {
            if (attack)
            {
                if (Input.GetKey(Key.E))
                {
                    stonethrow.Play();
                    facing = Facing.ToString();
                    level.Attack(facing, this.x, this.y, true, false, true);
                    attack = false;
                }
            }
            if (attacktimer >= 60)
            {
                AAttack = true;
                if (Input.GetKey(Key.SPACE)& AAttack)
                {
                    rafattack.Play();
                    facing = Facing.ToString();
                    level.Attack(facing, this.x, this.y, true, false, false);
                    AAttack = false;
                    attacktimer = 0;
                }
            }
            if (AAttack)
            {
                attacktimer = 60;
            }
        }

        if (Input.GetKey(Key.E) & levelnumber == 1 & hudsmallmeat.BigMeat & !cantPlaceMeat & hugeMeat>0)
        {
            facing = Facing.ToString();
            level.placeMeat(facing,x,y);
            hugeMeat--;
            collectedMeat = false;
        }
        if (dashtimer>50)
        {
            dashtimer = 0;
            canDash = true;
        }
        if ((Input.GetKey(Key.LEFT_SHIFT) | Input.GetKey(Key.RIGHT_SHIFT)) & canDash)
        {
            dash.Play();
            canDash = false;
            dashes = true;
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
        TutorialBoxes();
        hudsmallmeat.Update(smallmeat,hugeMeat);
        healthbar.Update(life);
    }
}
