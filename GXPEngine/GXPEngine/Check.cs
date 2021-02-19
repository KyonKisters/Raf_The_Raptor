using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


public class Check : Sprite
{
        Player player;
        float speed = 4f;
        string facing;
        int levelnumber;
        public enum Direction { TOP, DOWN, RIGHT, LEFT };
        public Direction Facing;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Checkbox
    /// </summary>
    #region Constructor
    public Check(Player player,int levelnumber) : base("CheckBox.png")
        {
        SetOrigin(width / 2, height / 2);
        alpha = 0.0f;
        this.player = player;
        this.levelnumber = levelnumber;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Collision + Position
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Collision and Position of the Checkbox
    /// </summary>
    #region Collision + Position
    public void checkCollision(string facing, float x, float y)
    {
        this.facing = facing;
        this.x = x;
        this.y = y;
        float moveX = 0;
        float moveY = 0;

        if (this.facing == "TOP")
        {
            if (levelnumber == 1)
            {
                this.y -= 48;
            }
            if (levelnumber == 2)
            {
                this.y -= 96;
            }
            if (levelnumber == 3)
            {
                this.y -= 144;
            }
            moveX = 0;
            moveY = -speed;
        }
        if (this.facing == "DOWN")
        {
            if (levelnumber == 1)
            {
                this.y += 48;
            }
            if (levelnumber == 2)
            {
                this.y += 96;
            }
            if (levelnumber == 3)
            {
                this.y += 144;
            }
            moveX = 0;
            moveY = speed;
        }
        if (this.facing == "RIGHT")
        {
            if (levelnumber == 1)
            {
                this.x += 48;
            }
            if (levelnumber == 2)
            {
                this.x += 144;
            }
            if (levelnumber == 3)
            {
                this.x += 192;
            }
            moveX = speed;
            moveY = 0;
        }
        if (this.facing == "LEFT")
        {
            moveX = -speed;
            moveY = 0;
            if (levelnumber == 1)
            {
                this.x -= 48;
            }
            if (levelnumber == 2)
            {
                this.x -= 144;
            }
            if (levelnumber == 3)
            {
                this.x -= 192;
            }
        }

        Move(moveX, moveY);
        Collision col = MoveUntilCollision(moveX, moveY);

        if (col != null)
        {
            player.cantDigAHole = true;
            player.cantPlaceMeat = true;
        }
        else {
            player.cantDigAHole = false;
            player.cantPlaceMeat = false;
        } 
    }
    #endregion
}
