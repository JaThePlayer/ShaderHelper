using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Celeste.Mod.ShaderHelper
{
    [CustomEntity("ShaderHelper/AddEffectTrigger")]
    public class AddEffectTrigger : Trigger
    {
        string shaderName;

        public AddEffectTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shaders");
        }

        public override void OnEnter(Player player)
        {
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
                    Logger.Log(LogLevel.Warn, "ShaderHelper", "AddEffectTrigger could not find shader " + shaderName + " in the FX.\n");
            }
            RemoveSelf();
        }
    }
}
