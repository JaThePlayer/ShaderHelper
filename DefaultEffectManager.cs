using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.ShaderHelper
{
    //for effects with no parameters
    public class DefaultEffectManager: IEffectManager
    {
        Effect effect;
        public DefaultEffectManager(Effect eff)
        {
            effect = eff;
        }

        public override void Apply(VirtualRenderTarget source)
        {
            //basically just stolen from the glitch effect
            //will be refined later
            VirtualRenderTarget tempA = GameplayBuffers.TempA;
            Engine.Instance.GraphicsDevice.SetRenderTarget(tempA);
            Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
            Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Draw.SpriteBatch.Draw(source, Vector2.Zero, Color.White);
            Draw.SpriteBatch.End();

            Engine.Instance.GraphicsDevice.SetRenderTarget(source);
            Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
            Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, effect);
            Draw.SpriteBatch.Draw(tempA, Vector2.Zero, Color.White);
            Draw.SpriteBatch.End();
        }
    }
}
