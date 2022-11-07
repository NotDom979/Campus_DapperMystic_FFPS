Shader "Zoom/ZoomShader"
{
    Properties
    {
        [HideInInspector]_MainTex("_MainTex", 2D) = "white" {}
        _Factor("Factor", Float) = 1
        _Color ("Tint", Color) = (1,1,1,1)
        _UVCenterOffset("UVCenterOffset", Vector) = (0,0,0,1)
    }
 
        SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
 
        GrabPass{ "_GrabTexture" }
 
        Pass
            {
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float4 uv2 : TEXCOORD1;
            };
 
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };
            fixed4 _Color;
            float4 _MainTex_ST;
            float _useTemperatureMask;
            sampler2D _GrabTexture;
            sampler2D _MainTex;
            sampler2D _TemperatureColourRamp;
            half _Factor;
            float4 _UVCenterOffset;
 
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                if(_Factor < 1) _Factor = 1;
                float4 center = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0, 0, 0, 1))); 
                center += _UVCenterOffset;
                float4 diff = ComputeGrabScreenPos(o.vertex) - center;
                diff /= _Factor;
                o.uv = center + diff;
                o.uv2 = v.uv;
                return o;
            }
 
            fixed4 frag(v2f i) : COLOR
            {
                fixed4 albedo = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv)) * _Color;
                fixed4 mask = tex2D(_MainTex, i.uv2);
                albedo *= mask;
                return albedo;
            }
            ENDCG
        }
    }
}
