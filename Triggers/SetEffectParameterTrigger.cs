using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Celeste.Mod.ShaderHelper
{
    [CustomEntity("ShaderHelper/SetFloatParameterTrigger")]
    class SetFloatParameterTrigger : Trigger
    {
        string shaderName;
        string key;
        float value;
        public SetFloatParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shader");
            key = data.Attr("key");
            value = data.Float("value");
        }

        public override void OnEnter(Player player)
        {
            if (ShaderHelperModule.Instance.FX.ContainsKey(shaderName))
            {
                Effect shader = ShaderHelperModule.Instance.FX[shaderName];
                if (shader != null)
                {
                    EffectParameter parameter = shader.Parameters[key];
                    if (parameter != null)
                        parameter.SetValue(value);
                }
            }
        }
    }

    [CustomEntity("ShaderHelper/SetBoolParameterTrigger")]
    class SetBoolParameterTrigger : Trigger
    {
        string shaderName;
        string key;
        bool value;
        public SetBoolParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shader");
            key = data.Attr("key");
            value = data.Bool("value");
        }

        public override void OnEnter(Player player)
        {
            if (ShaderHelperModule.Instance.FX.ContainsKey(shaderName))
            {
                Effect shader = ShaderHelperModule.Instance.FX[shaderName];
                if (shader != null)
                {
                    EffectParameter parameter = shader.Parameters[key];
                    if (parameter != null)
                        parameter.SetValue(value);
                }
            }
        }
    }

    [CustomEntity("ShaderHelper/SetIntegerParameterTrigger")]
    class SetIntegerParameterTrigger : Trigger
    {
        string shaderName;
        string key;
        int value;
        public SetIntegerParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shader");
            key = data.Attr("key");
            value = data.Int("value");
        }

        public override void OnEnter(Player player)
        {
            if (ShaderHelperModule.Instance.FX.ContainsKey(shaderName))
            {
                Effect shader = ShaderHelperModule.Instance.FX[shaderName];
                if (shader != null)
                {
                    EffectParameter parameter = shader.Parameters[key];
                    if (parameter != null)
                        parameter.SetValue(value);
                }
            }
        }
    }


    [CustomEntity("ShaderHelper/SetVector2ParameterTrigger")]
    class SetVector2ParameterTrigger : Trigger
    {
        string shaderName;
        string key;
        Vector2 value;
        public SetVector2ParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shader");
            key = data.Attr("key");
            string[] strv = data.Attr("value").Split(',');
            if (strv.Length > 1) //we need to values
                value = new Vector2(Int32.Parse(strv[0]), Int32.Parse(strv[1]));
        }

        public override void OnEnter(Player player)
        {
            if (ShaderHelperModule.Instance.FX.ContainsKey(shaderName))
            {
                Effect shader = ShaderHelperModule.Instance.FX[shaderName];
                if (shader != null)
                {
                    EffectParameter parameter = shader.Parameters[key];
                    if (parameter != null)
                        parameter.SetValue(value);
                }
            }
        }
    }

}
