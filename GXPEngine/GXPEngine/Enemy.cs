using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Enemy : AnimationSprite
{
    float speed = 2f;
    Player player;
    int attacktimer;
    bool attack = false;
    EAAProjectile projectile = new EAAProjectile();

    public Enemy(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        SetXY(game.width - 200, game.height / 2);
    }
    void Update()
    {
        Movement();
        Attack();
    }
    //----------------------------------------------------------------------------------------
    //                                        Attacks
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Attacks of the enemy
    /// </summary>
    #region Attacks
    void Attack()
    {
        if (HitArea(200))
        {
            attacktimer++;
            if (attacktimer > 50)
            {
      
                Random rnd = new Random();
                float rndnumber = rnd.Next(0, 100);
                attack = rndnumber < 75 ? true : false;
                attacktimer = 0;

                if (attack)
                {
                    AddChild(projectile);
                    attack = false;
                }
                Console.WriteLine(attack);
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
    bool HitArea(float distance)
    {
        Boolean hitarea;
        float distX = game.FindObjectOfType(typeof(Player)).x - this.x;
        float distY = game.FindObjectOfType(typeof(Player)).y - this.y;
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
    void Movement()
    {
        float moveX = 0;
        float moveY = 0;
        if (HitArea(200))
        {
            if (this.x > game.FindObjectOfType(typeof(Player)).x)
            {
                moveX = -speed;
                moveY = 0;
            }
            if (this.x < game.FindObjectOfType(typeof(Player)).x)
            {
                moveX = speed;
                moveY = 0;
            }
            if (this.y > game.FindObjectOfType(typeof(Player)).y)
            {
                moveX = 0;
                moveY = -speed;
            }
            if (this.y < game.FindObjectOfType(typeof(Player)).y)
            {
                moveX = 0;
                moveY = speed;
            }
            //if (this.y < game.FindObjectOfType(typeof(Player)).y)
            //    Console.WriteLine(player.x);
        }
        Collision collision = MoveUntilCollision(moveX, moveY);
    }
    #endregion
}



