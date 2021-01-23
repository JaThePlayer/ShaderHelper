# ShaderHelper

A small helper that is still fairly immature. Shaders are automatically loaded from other mods, and are expected to be in the compiled shader object (cso) format. These can be made using a tool like [EFB](http://github.com/GlaireDaggers/Effect-Build) (many thanks to jade for finding this tool and generally helping).

Shaders are expected to be within the Effects/ folder within the mod's zip, and will be auto loaded and stored in the FX dictionary (ShaderHelper.Instance.FX). Shaders are mapped to the FX dictionary with the key being the filename with the Effects/ path and the file extension cut off. For example, Effects/shaderhelper/testshader.cso maps to the key shaderhelper/testshader .

Can add shaders to maps using Ahorn triggers (AddEffectTrigger, ClearEffectTrigger). Use a comma separated list of shader names (their key names in the FX dictionary) to add shaders. Parameters can be changed by using the parameter triggers (will be likely changed out for a more ellegant system in the future). Shaders added this way will be applied globally (to the whole screen), to apply a shader to an individual object a codemod must be used.

Should be updated over time, and over time some better documentation should be added. Some triggers are provided, so map makers can add some basic shaders. Expect updates.