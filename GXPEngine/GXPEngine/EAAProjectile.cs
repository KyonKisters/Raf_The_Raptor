using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EAAProjectile : Sprite
    {
        public EAAProjectile() : base("EAAhitbox.png") 
        { 
       
        }
        void Update()
        {
            x++;
        }
    }
}
