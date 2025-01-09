// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test/MyShaderName" { 
    Properties { 
        // 属性 
    } 
    SubShader { 
        // 针对显卡A的SubShader 
        Pass { 
            // 设置渲染状态和标签 
 
            // 开始Cg代码片段 
            CGPROGRAM 
            // 该代码片段的编译指令，例如： 
            #pragma vertex vert 
            #pragma fragment frag 
 
            // Cg代码写在这里 

            // Unity支持的语义有：POSITION, TANGENT，NORMAL，TEXCOORD0，TEXCOORD1，TEXCOORD2，TEXCOORD3，COLOR等
            //格式：
            // struct StructName {
            //     Type Name : Semantic;
            //     Type Name : Semantic;
            //     .......
            //    };
               
            struct a2v {
                // POSITION语义告诉Unity，用模型空间的顶点坐标填充vertex变量
                float4 vertex : POSITION;
                // NORMAL语义告诉Unity，用模型空间的法线方向填充normal变量
                float3 normal : NORMAL;
                // TEXCOORD0语义告诉Unity，用模型的第一套纹理坐标填充texcoord变量
                float4 texcoord : TEXCOORD0;
                };
               
            float4 vert(float4 v : POSITION) : SV_POSITION { 
                return UnityObjectToClipPos (v); 
            } 
            fixed4 frag() : SV_Target { 
                return fixed4(1.0, 0, 0, 1.0);
            }
            ENDCG 
 
            // 其他设置 
        } 
        // 其他需要的Pass 
           
    } 
    // 上述SubShader都失败后用于回调的Unity Shader 
    Fallback "VertexLit" 
}
