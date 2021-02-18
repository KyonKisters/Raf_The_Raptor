using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;


public class HUDSmallMeat : Sprite
{
    Canvas meatCounter = new Canvas (200,100,false);
    int smallMeat = 0;
    Font meatFont = new Font("Prehistoric Caveman", 20);
    Sprite HudSmallmeat = new Sprite("HUDSmallMeat.png", false, false);
    public HUDSmallMeat() : base("HUDSmallMeat.png", false, false)
    {
        x += 300;
        y -= 290;
        scale = 0.5f;
    }
    public void Update(int smallMeat)
    {
        AddChild(meatCounter);
        this.smallMeat = smallMeat;
        alpha = smallMeat > 0 ? 1 : 0;
        if (smallMeat > 0)
        {
            meatCounter.graphics.Clear(Color.Transparent);
            meatCounter.graphics.DrawString("x" + smallMeat, meatFont, Brushes.White,80,0);
        }
    }
    public void giveSmallMeat(int smallMeat)
    {
        this.smallMeat = smallMeat;
    }
}

