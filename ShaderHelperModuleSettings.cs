using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.ShaderHelper
{
    [SettingName("modoptions_shaderhelper_title")]
    public class ShaderHelperModuleSettings : EverestModuleSettings
    {
        public bool Enabled { get; set; } = true;

        [SettingMaxLength(100)]
        public string Shaders { get; set; } = "";
    }
}
