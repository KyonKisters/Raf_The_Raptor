using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Enemy : Sprite
    {
        float speed=1;
        Player player;
        public Enemy(Player _player) : base("enemy.png")
        {
            SetXY(game.width - 200, game.height / 2);
            player = _player;
        }
        void Update()
        {
            HitArea();
            Movement();
            StopWalk();
        }
        void HitArea()
        {
          

        }

        void Movement()
        {
            if (this.x > player.x)
            {
                Move(-speed,0);
            }
            if (this.x < player.x)
            {
                Move(speed, 0);
            }
            if (this.y > player.y)
            {
                Move(0, -speed);
            }
            if (this.y < player.y)
            {
                Move(0, speed);
            }
        }
        void StopWalk()
        {

        }
    }
}
