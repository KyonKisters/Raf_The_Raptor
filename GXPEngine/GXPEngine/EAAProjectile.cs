using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EAAProjectile : Sprite
    {
        int lifetime;
        public EAAProjectile() : base("EAAhitbox.png") 
        {
            SetOrigin(width / 2, height / 2);
        }
        void Update()
        {
            x+=20;
            lifetime++;
            if (lifetime > 2)
            {
                LateDestroy();
            }
        }
    }
}
