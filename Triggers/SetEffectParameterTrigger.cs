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

    public abstract class BaseParameterTrigger : Trigger
    {
        protected string shaderName;
        protected string key;
        public BaseParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            shaderName = data.Attr("shader");
            key = data.Attr("key");
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
                        ApplyParamter(parameter);
                }
            }
        }


        public abstract void ApplyParamter(EffectParameter parameter);
    }


    [CustomEntity("ShaderHelper/SetFloatParameterTrigger")]
    class SetFloatParameterTrigger : BaseParameterTrigger
    {
        protected float value;
        public SetFloatParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            value = data.Float("value");
        }

        public override void ApplyParamter(EffectParameter parameter)
        {
            parameter.SetValue(value);
        }
    }

    [CustomEntity("ShaderHelper/SetBoolParameterTrigger")]
    class SetBoolParameterTrigger : BaseParameterTrigger
    {
        protected bool value;
        public SetBoolParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            value = data.Bool("value");
        }

        public override void ApplyParamter(EffectParameter parameter)
        {
            parameter.SetValue(value);
        }
    }

    [CustomEntity("ShaderHelper/SetIntegerParameterTrigger")]
    class SetIntegerParameterTrigger : BaseParameterTrigger
    {
        protected int value;
        public SetIntegerParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            value = data.Int("value");
        }

        public override void ApplyParamter(EffectParameter parameter)
        {
            parameter.SetValue(value);
        }
    }


    [CustomEntity("ShaderHelper/SetVector2ParameterTrigger")]
    class SetVector2ParameterTrigger : BaseParameterTrigger
    {
        Vector2 value;
        public SetVector2ParameterTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            try
            {
                string[] strv = data.Attr("value").Split(',');
                if (strv.Length > 1) //we need two values to set the parameter
                    value = new Vector2(float.Parse(strv[0]), float.Parse(strv[1]));
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.Error, "ShaderHelper", "Exception caught while parsing trigger.\n" + ex.ToString());
            }
         }

        public override void ApplyParamter(EffectParameter parameter)
        {
            parameter.SetValue(value);
        }
    }
}
