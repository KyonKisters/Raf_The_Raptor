﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class TRex : AnimationSprite
{
    public float speed = 3f;
    int attacktimer;
    public bool attack = false;
    public float life = 2;
    string facing;
    float distance = 350;
    float distance2 = 500;
    int animationtimer;
    bool discovered=false;
    bool raw= false;
    public bool Meatplaced=false;
    Level level;
    Player player;
    TriggerBox triggerBox = new TriggerBox();
    BigMeatObject MyMeat;
    Sound TrexRaw = new Sound("TREXRAW.wav");
    int hugeMeat;

    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Enemy
    /// </summary>
    #region Constructor
    public TRex(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        SetOrigin(this.width / 2, this.height / 2);
        triggerBox.scale = this.distance / 40;
        AddChild(triggerBox);
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
    public void createPlayer(Player player)
    {
        this.player = player;
    }
    public void createMeat(BigMeatObject meat)
    {
        MyMeat = meat;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Attacks
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Attacks of the enemy
    /// </summary>
    #region Attacks
    public void Attack()
    {
        if (Input.GetKey(Key.E) & hugeMeat >= 1 & !player.collectedMeat)
        {
            Meatplaced = true;
        }
        if (player.collectedMeat)
        {
            Meatplaced = false;
        }
        if (HitArea(distance))
        {
            attacktimer++;
            if (attacktimer > 50)
            {

                Random rnd = new Random();
                float rndnumber = rnd.Next(0, 100);
                attack = rndnumber < 25 ? true : false;

                if (attack)
                {
                    level.Attack(facing, this.x, this.y, false, true,false);
                    attack = false;
                }
                attacktimer = 0;

            }
        }

    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                         Trigger Area
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Triggers when ever an object enters and this object will follow
    /// </summary>
    #region HitArea
    public bool inRange(float distance)
    {
        this.distance2 = distance;
        Boolean hitarea;

        if (DistanceTo(player) < distance)
        {
            hitarea = true;
        }
        else hitarea = false;

        return hitarea;
    }
    public bool HitArea(float distance)
    {
        this.distance = distance;
        Boolean hitarea;

        if (DistanceTo(player) < distance)
        {
            hitarea = true;
        }
        else hitarea= false;

        return hitarea;
    }
    #endregion
    public bool HitAreaMeat(float distance)
    {
        this.distance = distance;
        Boolean hitarea;

        if (DistanceTo(MyMeat) < distance)
        {
            hitarea = true;
        }
        else hitarea = false;

        return hitarea;
    }
    //----------------------------------------------------------------------------------------
    //                                         Movement
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Movement of the object
    /// </summary>
    #region Movement
    public void Movement()
    {
        if (player.levelnumber==1)
        {
            speed = 3f;
        }
        if (player.levelnumber == 2)
        {
            speed = 7.5f;
            distance = 700;
            distance2 = 700;
        }
        float moveX = 0;
        float moveY = 0;

        if (inRange(distance2))
        {
            if (!raw)
            {
                TrexRaw.Play();
                raw = true;
            }
            if (!Meatplaced)
            {
                if (HitArea(distance))
                {
                    if (!discovered)
                    {
                        player.discoveredTRex = true;
                        discovered = true;
                    }
                    if (this.x > player.x)
                    {
                        facing = "LEFT";
                        moveX = -speed;
                        moveY = 0;
                    }
                    if (this.x < player.x)
                    {
                        facing = "RIGHT";
                        moveX = speed;
                        moveY = 0;
                    }
                    if (this.y > player.y)
                    {
                        facing = "TOP";
                        moveX = 0;
                        moveY = -speed;
                    }
                    if (this.y < player.y)
                    {
                        facing = "DOWN";
                        moveX = 0;
                        moveY = speed;
                    }
                }
            }
            if (Meatplaced)
            {
                if (HitAreaMeat(distance))
                {
                    if (this.x > MyMeat.x)
                    {
                        facing = "LEFT";
                        moveX = -speed;
                        moveY = 0;
                    }
                    if (this.x < MyMeat.x)
                    {
                        facing = "RIGHT";
                        moveX = speed;
                        moveY = 0;
                    }
                    if (this.y > MyMeat.y)
                    {
                        facing = "TOP";
                        moveX = 0;
                        moveY = -speed;
                    }
                    if (this.y < MyMeat.y)
                    {
                        facing = "DOWN";
                        moveX = 0;
                        moveY = speed;
                    }
                }
            }
        } else raw = false;

        Collision collision = MoveUntilCollision(moveX, moveY);
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
        if (facing == "LEFT")
        {
            Mirror(true, false);
            SetCycle(1, 3);
        }
        if (facing == "RIGHT")
        {
            Mirror(false, false);
            SetCycle(1, 3);
        }

        if (facing == "TOP")
        {
            SetCycle(5, 3);
        }
        if (facing == "DOWN")
        {
            SetCycle(9, 3);
        }

        if (animationtimer > 12)
        {
            NextFrame();
            animationtimer = 0;
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                         Collision
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Collision of the enemy
    /// </summary>
    #region Collision
    public void handleCollision(Collision col)
    {

    }
    #endregion
    void Update()
    {
        Movement();
        Attack();
        HandleAnimation();
        this.hugeMeat = player.hugeMeat;
    }
}