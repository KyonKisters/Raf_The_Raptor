using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class TriggerBox : Sprite
    {
        public TriggerBox() : base("TriggerBox.png",false,false)
        {
            SetOrigin(this.width / 2,this.height/2) ;
            alpha = 0.2f;
        }
    }
}
