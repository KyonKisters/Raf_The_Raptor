﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Enemy : AnimationSprite
{
    public float speed = 1f;
    int attacktimer;
    public bool attack = false;
    public float life = 2;
    string facing;
    float distance = 250;
    Level level;
    Player player;
    TriggerBox triggerBox = new TriggerBox ();

    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Enemy
    /// </summary>
    #region Constructor
    public Enemy(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        SetOrigin(this.width/2,this.height/2);
        triggerBox.scale = this.distance/40;
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
                    level.Attack(facing,this.x,this.y,false,true);
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
    public bool HitArea(float distance)
    {
        this.distance = distance;
        Boolean hitarea;
        float distX = player.x - this.x;
        float distY = player.y - this.y;
        float DistBetwThisAndObj = Mathf.Sqrt(distX * distX + distY * distY);

        if (DistBetwThisAndObj < distance)
        {
            hitarea = true;
        }
        else hitarea = false;

        return hitarea;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                         Movement
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Movement of the object
    /// </summary>
    #region Movement
    public void Movement()
    {
        float moveX = 0;
        float moveY = 0;
        if (HitArea(distance))
        {
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
        Collision collision = MoveUntilCollision(moveX, moveY);
        if (collision != null)
        {
            handleCollision(collision);
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
    }
}



