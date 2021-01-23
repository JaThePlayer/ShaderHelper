module ShaderHelperAddEffectTrigger

using ..Ahorn, Maple

@mapdef Trigger "ShaderHelper/ClearEffectTrigger" ClearEffectTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, shaders::String="shaderhelper/testshader")

const placements = Ahorn.PlacementDict(
    "Clear Effect Trigger (Shader Helper)" => Ahorn.EntityPlacement(
        ClearEffectTrigger,
        "rectangle"
    )
)

end