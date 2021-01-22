#define DECLARE_TEXTURE(Name, index) \
    texture Name: register(t##index); \
    sampler Name##Sampler: register(s##index)

#define SAMPLE_TEXTURE(Name, texCoord) tex2D(Name##Sampler, texCoord)


DECLARE_TEXTURE(text, 0);

float4 GetTestColor(float2 pos)
{
    float4 c = SAMPLE_TEXTURE(text, pos);
    float avg = (c.r * 0.3 + c.g * 0.59 + c.b * 0.11);
    return float4(c.g, c.b,c.r,c.w);//avg, avg, c.w);
}

float4 PS_TestEffect(float4 inPosition : SV_Position, float4 inColor : COLOR0, float2 uv : TEXCOORD0) : COLOR0
{
    return GetTestColor(uv);
}

technique TestEffect
{ 
    pass pass0 
    { 
        PixelShader = compile ps_2_0 PS_TestEffect(); 
    } 
} 