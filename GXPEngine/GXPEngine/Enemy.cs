using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Enemy : AnimationSprite
    {
        float speed=2f;
        Player player;
        Boolean hitarea;
        float distX;
        float distY;
        float distance;

    public Enemy(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
        {
            SetXY(game.width - 200, game.height / 2);
        }
        void Update()
        {
            HitArea();
            Movement();
            OnCollision(this);

    }
    void HitArea()
        {
            distX = game.FindObjectOfType(typeof(Player)).x - this.x;
            distY = game.FindObjectOfType(typeof(Player)).y - this.y;
            distance = Mathf.Sqrt(distX * distX + distY * distY);
            if (distance < 300)
            {
                hitarea = true;

            }
        }
    void Movement()
    {
        float moveX = 0;
        float moveY = 0;

        if (hitarea)
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
            void OnCollision(GameObject other)
            {
                if (other is Player)
                {
                    speed = 0;
                }
                else speed = 2f;

            }
        }

