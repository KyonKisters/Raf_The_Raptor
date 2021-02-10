using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Enemy : AnimationSprite
    {
        float speed=1;
        Player player;
        Boolean hitarea;
        Enemy enemy;
<<<<<<< HEAD
        float distX;
        float distY;
        float distance;
=======
>>>>>>> enemy

    public Enemy(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
        {
            SetXY(game.width - 200, game.height / 2);
        }
        void Update()
        {
            HitArea();
            Movement();
            OnCollision(this);
<<<<<<< HEAD
        }
    void HitArea()
        {
        distX = game.FindObjectOfType(typeof(Player)).x - this.x;
        distY = game.FindObjectOfType(typeof(Player)).y - this.y;
        distance = Mathf.Sqrt(distX * distX + distY * distY);
        if (distance < 200)
        {
            hitarea = true;
=======

    }
    void HitArea()
        {
            if (DistanceTo(player) < 200)
            {
                hitarea = true;
            }
            else hitarea = false;
>>>>>>> enemy
        }
        else hitarea = false;
    }

    void Movement()
        {
<<<<<<< HEAD

        if (hitarea)
            {
                if (this.x > game.FindObjectOfType(typeof(Player)).x)
                {
                    Move(-speed, 0);
                }
                if (this.x < game.FindObjectOfType(typeof(Player)).x)
                {
                    Move(speed, 0);
                }
                if (this.y > game.FindObjectOfType(typeof(Player)).y)
                {
                    Move(0, -speed);
                }
                if (this.y < game.FindObjectOfType(typeof(Player)).y)
=======
        Console.WriteLine(player.x);
        if (hitarea)
            {
                if (this.x > player.x)
                {
                    Move(-speed, 0);
                }
                if (this.x < player.x)
                {
                    Move(speed, 0);
                }
                if (this.y > player.y)
                {
                    Move(0, -speed);
                }
                if (this.y < player.y)
>>>>>>> enemy
                {
                    Move(0, speed);
                }
            }
        }
    void OnCollision(GameObject other)
    {
        if (other is Player)
        {
            speed = 0;
        } else speed = 1;

    }
    }

