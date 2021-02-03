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
        protected string shaderName;
        public ClearEffectTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shaders");
        }
        public override void OnEnter(Player player)
        {
            ShaderHelperModule.Instance.ClearEffects();
            ShaderHelperModule.Instance.AddEffect(shaderName);
            RemoveSelf();
        }
    }
}
