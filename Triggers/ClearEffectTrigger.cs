using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.ShaderHelper
{
    [CustomEntity("ShaderHelper/ClearEffectTrigger")]
    class ClearEffectTrigger : Trigger
    {
        string shaderName;
        public ClearEffectTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shaders");
        }
        public override void OnEnter(Player player)
        {
            ShaderHelperModule.Instance.ClearEffects();
            if (String.IsNullOrEmpty(shaderName))
                return;
            string[] shaders = shaderName.Split(',');
            foreach (string shaderv in shaders)
            {
                if (ShaderHelperModule.Instance.FX.ContainsKey(shaderv))
                {
                    Effect shader = ShaderHelperModule.Instance.FX[shaderv];
                    ShaderHelperModule.Instance.AddGlobalEffect(new DefaultEffectManager(shader));
                }
                else
                    Logger.Log(LogLevel.Warn, "ShaderHelper", "ClearEffectTrigger could not find shader " + shaderName + " in the FX.\n");
            }
            RemoveSelf();
        }
    }
}
