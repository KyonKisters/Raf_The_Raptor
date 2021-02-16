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

    void Update()
    { 

    }
}

