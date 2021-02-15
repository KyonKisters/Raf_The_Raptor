using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


    public class DugHole: Sprite
    {
    string facing;
    float lifetime=0.003f;
    public enum Direction { TOP, DOWN, RIGHT, LEFT };
    public Direction Facing;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Dug hole
    /// </summary>
    #region Constructor
    public DugHole(string facing, float x, float y) : base("DugHole.png")
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
    #endregion
    void Update()
    {
        alpha -= lifetime;
        if (alpha<=0)
        {
            LateDestroy();
        }
    }

}
