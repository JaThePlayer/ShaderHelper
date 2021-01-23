module SetBoolParameterTrigger

using ..Ahorn, Maple

@mapdef Trigger "ShaderHelper/SetBoolParameterTrigger" SetBoolParameterTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, shader::String="shaderhelper/testshader", value::Bool=true, key::String="key")

const placements = Ahorn.PlacementDict(
    "Set Bool Parameter Trigger (Shader Helper)" => Ahorn.EntityPlacement(
        SetBoolParameterTrigger,
        "rectangle"
    )
)

end