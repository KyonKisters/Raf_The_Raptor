using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
namespace GXPEngine
{
    public class EAAProjectile : Sprite
    {
      
        string facing;
        float speed = 3;
        Player player;
        Enemy enemy;
        //----------------------------------------------------------------------------------------
        //                                        Constructor
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the Projectile
        /// </summary>
       #region Constructor
        public EAAProjectile(string facing, float x, float y,Player player,Enemy enemy) : base("EAAhitbox.png") 
        {
            SetOrigin(width / 2, height / 2);
            this.facing = facing;
            this.x = x;
            this.y = y;
            this.player = player;
            this.enemy = enemy;

            if (this.facing == "TOP")
            {
                this.y -=24;
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
        //                                        Movement
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Movement of the Projectile
        /// </summary>
        #region Movement 
        void Movement()
        {
            float moveX = 0;
            float moveY = 0;

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
            if (col.other is Player)
            {
                player.life--;
            }
            if (col.other is Enemy)
            {
                LateDestroy();
                enemy.life--;
                if (enemy.life <= 0)
                {
                    col.other.LateDestroy();
                }
            }
        }

        #endregion
        void Update()
        {
            Movement();
        }
    }
}
