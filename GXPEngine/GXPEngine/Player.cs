using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : Sprite
{
    float speed = 4f;

    public Player() : base("player.png")
    {
        x = 800 / 2;
        y = 600 / 2;
        SetOrigin(width/2,height/2);
    }

    void Update()
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
        //if (collision != null)
        //{
        //    OnCollision(collision.other);
        //}

    }
}
