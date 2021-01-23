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
    //for effects with no parameters/engine timing only
    public class DefaultEffectManager: IEffectManager
    {
        private Effect effect;
        public DefaultEffectManager(Effect eff)
        {
            effect = eff;
        }

        void ApplyParameters()
        {
            EffectParameter deltaParam = effect.Parameters["DeltaTime"];
            if (deltaParam != null)
                deltaParam.SetValue(Engine.DeltaTime);
            EffectParameter timeParam = effect.Parameters["Time"];
            if (timeParam != null)
                timeParam.SetValue(ShaderHelperModule.Instance.Time);
        }

        public override void Render(Texture2D source, Texture2D map)
        {
            ApplyParameters();
            Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, effect);
            Draw.SpriteBatch.Draw(source, Vector2.Zero, Color.White);
            Draw.SpriteBatch.End();
        }

        public override void Apply(VirtualRenderTarget source)
        {
            ApplyParameters();
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
