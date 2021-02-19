using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class BigMeatObject : Sprite
{
    string facing;
    public enum Direction { TOP, DOWN, RIGHT, LEFT };
    public Direction Facing;
    public BigMeatObject(string facing, float x, float y) : base("BigMeat.png")
    {

        SetOrigin(width / 2, height / 2);
        this.facing = facing;
        this.x = x;
        this.y = y;

        if (this.facing == "TOP")
        {
            this.y -= 48;
        }
        if (this.facing == "DOWN")
        {
            this.y += 48;
        }
        if (this.facing == "RIGHT")
        {
            this.x += 48;
        }
        if (this.facing == "LEFT")
        {
            this.x -= 48;
        }
    }

}

