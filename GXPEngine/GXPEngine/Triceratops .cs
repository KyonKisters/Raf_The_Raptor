using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Triceratops : AnimationSprite
{
    int timer=0;
    public Triceratops(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
        SetCycle(1, 3);
    }

    void Update()
    {
        timer++;
        if (timer>=12)
        {
            NextFrame();
            timer = 0;
        }
    }
}

