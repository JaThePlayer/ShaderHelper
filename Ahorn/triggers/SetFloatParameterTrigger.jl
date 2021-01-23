module SetFloatParameterTrigger

using ..Ahorn, Maple

@mapdef Trigger "ShaderHelper/SetFloatParameterTrigger" SetFloatParameterTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, shader::String="shaderhelper/testshader", value::Float=0, key::String="key")

const placements = Ahorn.PlacementDict(
    "Set Float Parameter Trigger (Shader Helper)" => Ahorn.EntityPlacement(
        SetFloatParameterTrigger,
        "rectangle"
    )
)

end