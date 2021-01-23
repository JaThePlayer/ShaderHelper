module ShaderHelperAddEffectTrigger

using ..Ahorn, Maple

@mapdef Trigger "ShaderHelper/AddEffectTrigger" AddEffectTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, shaders::String="shaderhelper/testshader")

const placements = Ahorn.PlacementDict(
    "Add Effect Trigger (Shader Helper)" => Ahorn.EntityPlacement(
        AddEffectTrigger,
        "rectangle"
    )
)

end