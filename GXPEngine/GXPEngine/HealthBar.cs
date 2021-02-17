using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class HealthBar : AnimationSprite
{
    public HealthBar() : base("HealthBar.png", 5, 1,-1,false,false)
    {
        x -= 400;
        y -= 325;
    }

    public void Update(int life)
    {
        if (life ==4)
        {
            SetFrame(0);
        }
        if (life == 3)
        {
            SetFrame(1);
        }
        if (life == 2)
        {
            SetFrame(2);
        }
        if (life == 1)
        {
            SetFrame(3);
        }
        if (life == 0)
        {
            SetFrame(4);
        }
    }
}

