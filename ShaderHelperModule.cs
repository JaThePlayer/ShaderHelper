using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Monocle;
using Celeste;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Celeste.Mod.ShaderHelper
{
    public class ShaderHelperModule : EverestModule
    {
        public sealed class AssetTypeCompiledShader { private AssetTypeCompiledShader() { } }
        public sealed class AssetTypeFXFile { private AssetTypeFXFile() { } }


        public static ShaderHelperModule Instance;        
        public override Type SettingsType => typeof(ShaderHelperModuleSettings);
        public static ShaderHelperModuleSettings Settings => (ShaderHelperModuleSettings)Instance._Settings;



        private IGraphicsDeviceService graphicsDeviceService;
        public float Time=0.0f;

        public ShaderHelperModule()
        {
            Instance = this;
            globalEffects = new List<IEffectManager>();
        }

        public Dictionary<string, Effect> FX = new Dictionary<string, Effect>();   // Atlas of shaders
        List<IEffectManager> globalEffects; //a list of effects to apply to the screen


        public void AddEffect(string shaderName)
        {
            if (string.IsNullOrEmpty(shaderName))
                return;
            string[] shaders = shaderName.Split(',');
            foreach (string shaderv in shaders)
            {
                if (Instance.FX.ContainsKey(shaderv))
                {
                    Effect shader = Instance.FX[shaderv];
                    AddEffect(new DefaultEffectManager(shader));
                }
                else
                    Logger.Log(LogLevel.Warn, "ShaderHelper", "Could not find shader " + shaderv + " in the FX when adding.\n");
            }

        }

        public void AddEffect(IEffectManager effect)
        {
            globalEffects.Add(effect);
        }

        public void RemoveEffect(IEffectManager effect)
        {
            globalEffects.Remove(effect);
        }

        public void ClearEffects()
        {
            globalEffects.Clear();
            AddEffect(Settings.Shaders);
        }

        public override void Load()
        {
            On.Celeste.Glitch.Apply += Apply;
            On.Celeste.LevelEnter.Go += OnLevelEnter;
            On.Monocle.Engine.Update += Update;
            Everest.Content.OnGuessType += Content_OnGuessType;
            Everest.Content.OnUpdate += Content_OnUpdate;
        }

        private void Content_OnUpdate(ModAsset from, ModAsset to)
        {
            if (to.Type == typeof(AssetTypeCompiledShader))
            {
                string shaderName = GetShaderNameFromFilename(from.PathVirtual, false);

                Effect previousEffect = null;
                if (FX.ContainsKey(shaderName))
                {
                    previousEffect = FX[shaderName];
                }

                FX[shaderName] = LoadEffect(shaderName);

                AssetReloadHelper.ReloadLevel();

                // crashes the game!!! (but not calling it causes a memory leak D:)
                //previousEffect?.Dispose();

                Logger.Log(LogLevel.Info, "ShaderHelper", "Reloaded shader " + shaderName + " from path " + to.PathVirtual);

            }
        }

        private string Content_OnGuessType(string file, out Type type, out string format)
        {
            if (IsCompiledEffectFile(file))
            {
                type = typeof(AssetTypeCompiledShader);
                format = ".cso";
                return file.Substring(0, file.Length - 4);
            }

            type = null;
            format = null;
            return null;
        }

        public override void Unload()
        {
            On.Celeste.Glitch.Apply -= Apply;
            On.Celeste.LevelEnter.Go -= OnLevelEnter;
            On.Monocle.Engine.Update -= Update;
            Everest.Content.OnGuessType -= Content_OnGuessType;
            Everest.Content.OnUpdate -= Content_OnUpdate;
        }

        public void OnLevelEnter(On.Celeste.LevelEnter.orig_Go orig, Session session, bool fromSaveData)
        {
            orig(session, fromSaveData);
            ClearEffects(); //clear effects when a new map is used
        }

        public Effect LoadEffect(string path)
        {
            ModAsset asset = Everest.Content.Get("Effects/" + path, true);
            if (asset == null)
            {
                Logger.Log(LogLevel.Warn,"ShaderHelper", "Failed to load asset " + "Effects/" + path);
                return null;
            }

            try
            {
                Effect returnV = new Effect(Engine.Graphics.GraphicsDevice, asset.Data);
                return returnV;
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.Error, "ShaderHelper", "Failed to load the shader " + path);
                Logger.Log(LogLevel.Error, "ShaderHelper", "Exception: \n" + ex.ToString());
            }
            return null;
        }


        public void Apply(On.Celeste.Glitch.orig_Apply orig, VirtualRenderTarget source, float timer, float seed, float amplitude)
        {
            orig(source, timer, seed, amplitude);
            if (!Settings.Enabled)
                return;
            foreach (IEffectManager manager in globalEffects)
                if(manager.Enabled)
                    manager.Apply(source);
        }

        public void Update(On.Monocle.Engine.orig_Update orig, Engine self, GameTime gameTime)
        {
            orig(self, gameTime);
            Time += Engine.DeltaTime;
        }

        public static string GetShaderNameFromFilename(string virtualPath, bool stripExtension)
        {
            string shaderName = virtualPath.Substring("Effects/".Length);
            return stripExtension ? shaderName.Substring(0, shaderName.Length - 4) : shaderName;
        }

        public static bool IsCompiledEffectFile(string path)
        {
            return Path.GetExtension(path) == ".cso" && path.StartsWith("Effects/");
        }

        public override void LoadContent(bool firstLoad)
        {
            foreach (ModContent content in Everest.Content.Mods)
                foreach(ModAsset asset in content.List)
                    if (asset.Type == typeof(AssetTypeCompiledShader))
                    {
                        string shaderName = asset.PathVirtual.Substring("Effects/".Length);

                        Effect effect = LoadEffect(shaderName);
                        if(effect != null)
                            FX[shaderName] = effect;
                        Logger.Log(LogLevel.Info, "ShaderHelper", "Loaded shader " + shaderName + " path " + asset.PathVirtual);
                    }
        }
    }
}
