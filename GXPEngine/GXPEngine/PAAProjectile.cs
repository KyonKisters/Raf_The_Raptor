using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;

class PAAProjectile : Sprite
{
    string facing;
    public float speed = 3;
    public int lifetime;
    Level level;
    //----------------------------------------------------------------------------------------
    //                                        Constructor
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor of the Projectile
    /// </summary>
    #region Constructor
    public PAAProjectile(string facing, float x, float y) : base("Rock.png")
    {
        SetOrigin(width / 2, height / 2);
        this.facing = facing;
        this.x = x;
        this.y = y;

        if (this.facing == "TOP")
        {
            this.y -= 24;
        }
        if (this.facing == "DOWN")
        {
            this.y += 24;
        }
        if (this.facing == "RIGHT")
        {
            this.x += 24;
        }
        if (this.facing == "LEFT")
        {
            this.x -= 24;
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Instances
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Instances of the Projectile
    /// </summary>
    #region Instances
    public void creatLevel(Level level)
    {
        this.level = level;
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Movement
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Movement of the Projectile
    /// </summary>
    #region Movement 
    void Movement()
    { 
        lifetime--;
        float moveX = 0;
        float moveY = 0;
        if (lifetime <= 0)
        {
            LateDestroy();
        }
        if (facing == "LEFT")
        {
            moveX -= speed;
        }

        if (facing == "RIGHT")
        {
            moveX += speed;
        }
        if (facing == "TOP")
        {
            moveY -= speed;
        }
        if (facing == "DOWN")
        {
            moveY += speed;
        }
        Move(moveX, moveY);
        Collision collision = MoveUntilCollision(moveX, moveY);
        if (collision != null)
        {
            handleCollision(collision);
        }
    }
    #endregion
    //----------------------------------------------------------------------------------------
    //                                        Collision
    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Collision of the Projectile
    /// </summary>
    #region Collision
    void handleCollision(Collision col)
    {
        if (!(col.other is Player) & !(col.other is Check) & !(col.other is DugHole))
        {
            LateDestroy();
        }
        if (col.other is Enemy newEnemy)
        {
            newEnemy.life--;
            LateDestroy();
            if (newEnemy.life <= 0)
            {
                level.enemyDefeatedCounter++;
                col.other.LateDestroy();
            }
        }
        if (col.other is EnemyBoss newEnemyBoss)
        {
            newEnemyBoss.life--;
            LateDestroy();
            if (newEnemyBoss.life <= 0)
            {
                level.enemyDefeatedCounter++;
                col.other.LateDestroy();
                level.LoadLevel();
            }
        }
        if (col.other is MummyKiller mummyKiller)
        {
            mummyKiller.life--;
            LateDestroy();
            if (mummyKiller.life <= 0)
            {
                level.enemyDefeatedCounter++;
                col.other.LateDestroy();
                level.LoadLevel();
            }
        }
    }

    #endregion
    void Update()
    {
        Movement();
    }
}

