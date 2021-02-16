using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class HUDSmallMeat : Sprite
{
    public HUDSmallMeat() : base("HUDSmallMeat.png",false,false)
    {
        x += 300;
        y -= 290;
        scale = 0.5f;
    }
}

