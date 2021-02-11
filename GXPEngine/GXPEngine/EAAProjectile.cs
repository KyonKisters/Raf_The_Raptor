using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
namespace GXPEngine
{
    public class EAAProjectile : Sprite
    {
        int lifetime;
        Player player;
        Enemy enemy;
        public EAAProjectile() : base("EAAhitbox.png") 
        {
            SetOrigin(width / 2, height / 2);
        }
        void Update()
        {
            float moveX = 0;
            float moveY = 0;
            //x += 20;
            if (game.FindObjectOfType(typeof(Enemy)).x < game.FindObjectOfType(typeof(Player)).x)
            {
                moveX -=5;
            }
            if (game.FindObjectOfType(typeof(Enemy)).x > game.FindObjectOfType(typeof(Player)).x)
            {
                moveX +=5;
            }
            if (game.FindObjectOfType(typeof(Enemy)).y < game.FindObjectOfType(typeof(Player)).y)
            {
                moveY -=5;
            }
            if (game.FindObjectOfType(typeof(Enemy)).y > game.FindObjectOfType(typeof(Player)).y)
            {
                moveY +=5;
            }
            Collision collision = MoveUntilCollision(moveX, moveY);
            if (collision != null)
            {
                LateDestroy();
            }
        }
    }
}
