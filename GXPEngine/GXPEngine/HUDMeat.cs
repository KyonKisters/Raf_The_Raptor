using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;


public class HUDMeat : Sprite
{
    Canvas meatCounter = new Canvas (200,100,false);
    int smallMeat = 0;
    int hugeMeat=0;
    Font meatFont = new Font("Prehistoric Caveman", 15);
    Sprite HudSmallmeat = new Sprite("HUDSmallMeat.png", false, false);
    public bool BigMeat=false;
    bool once = false;
    Sprite BigMeatsprite = new Sprite("BigMeat.png",false,false);
    public HUDMeat() : base("HUDSmallMeat.png", false, false)
    {
        x += 295;
        y -= 290;
        scale = 0.5f;
    }
    public void Update(int smallMeat, int hugeMeat)
    {
        AddChild(meatCounter);
        this.smallMeat = smallMeat;
        this.hugeMeat = hugeMeat;
        alpha = smallMeat > 0 ? 1 : 0;
        if (smallMeat > 0 & !BigMeat)
        {
            meatCounter.graphics.Clear(Color.Transparent);
            meatCounter.graphics.DrawString( smallMeat+"/3", meatFont, Brushes.White,70,15);
        }
        if (BigMeat & hugeMeat>0)
        {
            meatCounter.graphics.Clear(Color.Transparent);
            AddChild(BigMeatsprite);
            if (!once)
            {
                x = x + 40;
                once = true;
            }
        }
        if (BigMeat & hugeMeat == 0)
        {
            BigMeatsprite.alpha = 0;
        }else BigMeatsprite.alpha = 1;

    }
    public void giveSmallMeat(int smallMeat)
    {
        this.smallMeat = smallMeat;
    }
}

