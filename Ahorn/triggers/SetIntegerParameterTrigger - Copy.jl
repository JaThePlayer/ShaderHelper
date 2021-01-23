module SetIntegerParameterTrigger

using ..Ahorn, Maple

@mapdef Trigger "ShaderHelper/SetIntegerParameterTrigger" SetIntegerParameterTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, shader::String="shaderhelper/testshader", value::Int=0, key::String="key")

const placements = Ahorn.PlacementDict(
    "Set Integer Parameter Trigger (Shader Helper)" => Ahorn.EntityPlacement(
        SetIntegerParameterTrigger,
        "rectangle"
    )
)

end