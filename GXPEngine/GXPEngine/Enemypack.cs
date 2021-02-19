using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;


public class Enemypack : AnimationSprite
{
    public bool EnemiesDefeated = false;
    Level level;
    int distance = 300;
    Player player;
    public Enemypack(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    { 
    
    }
    public void createLevelInst(Level level)
    {
        this.level = level;
    }
    public void getPlayer(Player player)
    {
        this.player = player;
    }
    //----------------------------------------------------------------------------------------
    //                                         Trigger Area
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Triggers when ever an object enters and this object will follow
    /// </summary>
    #region HitArea
    public bool HitArea()
    {
        Boolean hitarea;

        if (DistanceTo(player) < distance)
        {
            hitarea = true;
        }
        else hitarea = false;

        return hitarea;
    }
    #endregion
    void Update()
    {
        if (EnemiesDefeated)
        {
            LateDestroy();
        }
        if (HitArea())
        {
            player.discoveredenemypack=true;
        }
    }
}

