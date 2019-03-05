using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace MarsIceEngine.Entity
{
    public struct TextureAnimationInfo
    {
        public TextureAnimationInfo(Act act, int frameCount)
        {
            if (frameCount == 0)
            {
                frameCount = 1;
            }

            Act = act;
            FrameCount = frameCount;
        }

        public Act Act { get; set; }

        public int FrameCount { get; set; }
    }
}
