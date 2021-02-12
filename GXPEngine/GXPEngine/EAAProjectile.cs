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
        float speed = 5;
        //----------------------------------------------------------------------------------------
        //                                        Constructor
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the Projectile
        /// </summary>
       #region Constructor
        public EAAProjectile(string facing, float x, float y) : base("EAAhitbox.png") 
        {
            SetOrigin(width / 2, height / 2);
            this.facing = facing;
            this.x = x;
            this.y = y;

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
            //x += 20;

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
            LateDestroy();
        }
        #endregion
        void Update()
        {
            Movement();
        }
    }
}
