using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.ShaderHelper
{
    public abstract class IEffectManager
    {
        //Applies the effect to a full screen
        public abstract void Apply(VirtualRenderTarget source);

        //Applies the effect as the sprite is drawn from source to map
        public abstract void Render(Texture2D source, Texture2D map);

        //Used to apply parameters every frame, check DefaultEffectManager for an example
        public abstract void ApplyParameters();

        public bool Enabled = true;
    }
}
