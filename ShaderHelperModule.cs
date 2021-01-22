using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Monocle;
using Celeste;
using System.IO;

namespace Celeste.Mod.ShaderHelper
{
    public class ShaderHelperModule : EverestModule
    {
        public static ShaderHelperModule Instance;        
        private IGraphicsDeviceService graphicsDeviceService;

        public ShaderHelperModule()
        {
            Instance = this;
            globalEffects = new List<IEffectManager>();
        }

        public Dictionary<string, Effect> FX;   // Atlas of shaders

        List<IEffectManager> globalEffects; //a list of effects to apply to the screen

        public void AddGlobalEffect(IEffectManager effect)
        {
            globalEffects.Add(effect);
        }

        public void RemoveGlobalEffect(IEffectManager effect)
        {
            globalEffects.Remove(effect);
        }

        public override void Load()
        {
            On.Celeste.Glitch.Apply += Apply;
        }
        public override void Unload()
        {
            On.Celeste.Glitch.Apply -= Apply;
        }

        public Effect LoadEffect(string path)
        {
            if (graphicsDeviceService == null)  //probably not a great method of doing this whatsoever
                graphicsDeviceService = Engine.Instance.Content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;

            ModAsset asset = Everest.Content.Get("Effects/" + path, true);
            if (asset == null)
            {
                Logger.Log(LogLevel.Warn,"ShaderHelper", "Failed to load asset " + "Effects/" + path);
                return null;
            }
            return new Effect(graphicsDeviceService.GraphicsDevice ,asset.Data);
        }
        public void Apply(On.Celeste.Glitch.orig_Apply orig, VirtualRenderTarget source, float timer, float seed, float amplitude)
        {
            orig(source, timer, seed, amplitude);
            foreach (IEffectManager manager in globalEffects)
                manager.Apply(source);
        }

        public override void LoadContent(bool firstLoad)
        {
            FX = new Dictionary<string, Effect>();
            foreach (ModContent content in Everest.Content.Mods)
                foreach(ModAsset asset in content.List)
                    //don't know everest well enough, so I doubt this is propper
                    if (Path.GetExtension(asset.PathVirtual) == ".cso" && asset.PathVirtual.StartsWith("Effects/"))
                    {
                        string shaderName = asset.PathVirtual.Substring(8);
                        shaderName = shaderName.Substring(0, shaderName.Length - 4);
                        FX[shaderName] = LoadEffect(shaderName + ".cso");
                        Logger.Log(LogLevel.Info, "ShaderHelper", "Loaded shader " + shaderName + " path " + asset.PathVirtual);
                    }

            AddGlobalEffect(new DefaultEffectManager(FX["shaderhelper/testshader"])); //just a debug shader
        }
    }
}
