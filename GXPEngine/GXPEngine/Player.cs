using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : Sprite
{
    Enemy enemy;
    float speed = 4f;
    int attacktimer = 0;
    bool attack;


    public Player() : base("player.png")
    {
        x = 800 / 2;
        y = 600 / 2;
        SetOrigin(width/2,height/2);
    }

    void Update()
    {
        Movement();
        Attack();
    }
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
            moveX = -speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.D) || Input.GetKey(Key.RIGHT))
        {
            moveX = speed;
            moveY = 0;
        }
        if (Input.GetKey(Key.W) || Input.GetKey(Key.UP))
        {
            moveX = 0;
            moveY = -speed;
        }
        if (Input.GetKey(Key.S) || Input.GetKey(Key.DOWN))
        {
            moveX = 0;
            moveY = speed;
        }
        Collision collision = MoveUntilCollision(moveX, moveY); //You can move until collision, for example with Tiled Map
        if (collision != null)
        {
            OnCollision(collision.other);
        }
    }
    void OnCollision(GameObject other)
    {
        float moveX = 0;
        float moveY = 0;
        if (other is Enemy)
        {
            if (this.x > game.FindObjectOfType(typeof(Enemy)).x)
            {
                moveX = moveX + 10;
            }
            if (this.x < game.FindObjectOfType(typeof(Enemy)).x)
            {
                moveX = moveX - 10;
            }
            if (this.y > game.FindObjectOfType(typeof(Enemy)).y)
            {
                moveY = moveY + 10;
            }
            if (this.y < game.FindObjectOfType(typeof(Enemy)).y)
            {
                moveY = moveY - 10;
            }
        }
        Collision collision = MoveUntilCollision(moveX, moveY); //You can move until collision, for example with Tiled Map
        if (collision != null)
        {
            OnCollision(collision.other);
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
        attacktimer++;
        if (Input.GetKey(Key.E) & attack)
        {
            EAAProjectile projectile = new EAAProjectile();
            AddChild(projectile);
            attack = false;
        }
        if (attacktimer >50)
        {
            attacktimer = 0;
            attack = true;
        }
    }

    #endregion
}
