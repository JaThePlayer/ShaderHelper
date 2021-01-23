# ShaderHelper

A small helper thrown together over the course of a couple days. Shaders are automatically loaded from other mods, and are expected to be in the compiled shader object (cso) format. These can be made using a tool like [EFB](http://github.com/GlaireDaggers/Effect-Build) (thanks jade for finding this tool and generally helping).

Shaders are expected to be within the Effects/ folder within the mod's zip, and will be auto loaded and stored in the FX dictionary (ShaderHelper.Instance.FX). Shaders are mapped to the FX dictionary with the key being the filename with the Effects/ path and the file extension cut off. For example, Effects/shaderhelper/testshader.cso maps to the key shaderhelper/testshader .

Should be updated over time, and over time some better documentation should be added. Some triggers are provided, so map makers can add some basic shaders. Expect updates.