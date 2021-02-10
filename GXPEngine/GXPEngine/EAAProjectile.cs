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
       
        }
        void Update()
        {
            x+=5;
            lifetime++;
            if (lifetime > 50)
            {
                LateDestroy();
            }
        }
    }
}
