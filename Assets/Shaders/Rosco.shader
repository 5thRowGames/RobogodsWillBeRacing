// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/Rosco"
{
	Properties
	{
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_TextureSample4("Texture Sample 4", 2D) = "white" {}
		_Tiling3("Tiling 3", Vector) = (3,3,0,0)
		_Tiling2("Tiling 2", Vector) = (3,3,0,0)
		_Tiling1("Tiling 1", Vector) = (3,3,0,0)
		_Speed3("Speed 3", Vector) = (0,1,0,0)
		_Speed2("Speed 2", Vector) = (0,1,0,0)
		_Speed1("Speed 1", Vector) = (0,1,0,0)
		_AlbedoColor("Albedo Color", Color) = (0,0,0,0)
		[HDR]_StarColor("Star Color", Color) = (0,0,0,0)
		[HDR]_StarColor2("Star Color 2", Color) = (0,0,0,0)
		[HDR]_StarColor3("Star Color 3", Color) = (0,0,0,0)
		_Slider3("Slider 3", Range( -12 , 100)) = 0
		_Slider2("Slider 2", Range( -12 , 200)) = 0
		_Slider1("Slider 1", Range( -12 , 500)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float4 _AlbedoColor;
		uniform float4 _StarColor;
		uniform sampler2D _TextureSample4;
		uniform float2 _Speed1;
		uniform float2 _Tiling1;
		uniform float _Slider1;
		uniform float4 _StarColor2;
		uniform sampler2D _TextureSample3;
		uniform float2 _Speed2;
		uniform float2 _Tiling2;
		uniform float _Slider2;
		uniform float4 _StarColor3;
		uniform sampler2D _TextureSample2;
		uniform float2 _Speed3;
		uniform float2 _Tiling3;
		uniform float _Slider3;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _AlbedoColor.rgb;
			float2 uv_TexCoord47 = i.uv_texcoord * _Tiling1;
			float2 panner48 = ( _Time.y * _Speed1 + uv_TexCoord47);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float2 uv_TexCoord36 = i.uv_texcoord * _Tiling2;
			float2 panner37 = ( _Time.y * _Speed2 + uv_TexCoord36);
			float2 uv_TexCoord20 = i.uv_texcoord * _Tiling3;
			float2 panner15 = ( _Time.y * _Speed3 + uv_TexCoord20);
			o.Emission = ( ( _StarColor * ( tex2D( _TextureSample4, panner48 ) * step( ase_vertex3Pos.y , _Slider1 ) ) ) + ( _StarColor2 * ( tex2D( _TextureSample3, panner37 ) * step( ase_vertex3Pos.y , _Slider2 ) ) ) + ( _StarColor3 * ( tex2D( _TextureSample2, panner15 ) * step( ase_vertex3Pos.y , _Slider3 ) ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
633;73;1285;927;1950.497;763.8721;1;True;False
Node;AmplifyShaderEditor.Vector2Node;17;-1643.001,591.4855;Float;False;Property;_Tiling3;Tiling 3;3;0;Create;True;0;0;False;0;3,3;3,3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;33;-1803.561,-23.71402;Float;False;Property;_Tiling2;Tiling 2;4;0;Create;True;0;0;False;0;3,3;3,3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;44;-2001.962,-692.6664;Float;False;Property;_Tiling1;Tiling 1;5;0;Create;True;0;0;False;0;3,3;3,3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1767.961,-751.6666;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;46;-1628.96,-446.6667;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;35;-1431.923,222.2856;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;45;-1743.961,-610.6666;Float;False;Property;_Speed1;Speed 1;8;0;Create;True;0;0;False;0;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;19;-1270,837.4855;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;18;-1385.001,673.4855;Float;False;Property;_Speed3;Speed 3;6;0;Create;True;0;0;False;0;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-1570.924,-82.71407;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;34;-1546.924,58.28588;Float;False;Property;_Speed2;Speed 2;7;0;Create;True;0;0;False;0;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1409.001,532.4855;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;39;-1188.445,200.7967;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;49;-1420.313,-308.4544;Float;False;Property;_Slider1;Slider 1;15;0;Create;True;0;0;False;0;0;0;-12;500;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;32;-1026.522,815.9965;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;15;-1118.001,553.4855;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1223.275,360.4979;Float;False;Property;_Slider2;Slider 2;14;0;Create;True;0;0;False;0;0;0;-12;200;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;37;-1279.924,-61.71402;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1062.406,974.644;Float;False;Property;_Slider3;Slider 3;13;0;Create;True;0;0;False;0;0;0;-12;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;50;-1385.482,-468.1556;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;48;-1476.961,-730.6665;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;16;-897.0015,516.4855;Float;True;Property;_TextureSample2;Texture Sample 2;0;0;Create;True;0;0;False;0;00468596807cf4b4bb91787c92efab31;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-1255.962,-767.6666;Float;True;Property;_TextureSample4;Texture Sample 4;2;0;Create;True;0;0;False;0;00468596807cf4b4bb91787c92efab31;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;51;-1035.232,-501.5804;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;22;-676.2744,782.5717;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;-1058.924,-98.7141;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;False;0;00468596807cf4b4bb91787c92efab31;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;40;-838.1949,167.372;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-580.722,-28.09842;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-777.7588,-697.0508;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-745.7177,-914.2565;Float;False;Property;_StarColor;Star Color;10;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;60;-385.8057,268.9044;Float;False;Property;_StarColor3;Star Color 3;12;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-418.8012,587.1011;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;59;-544.1053,-339.7343;Float;False;Property;_StarColor2;Star Color 2;11;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-97.3471,517.2814;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-242.7719,-247.4903;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-447.3247,-693.7825;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;640.3575,193.6829;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;12;596.7391,-184.7268;Float;False;Property;_AlbedoColor;Albedo Color;9;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;846.4135,67.5424;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/Rosco;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;47;0;44;0
WireConnection;36;0;33;0
WireConnection;20;0;17;0
WireConnection;15;0;20;0
WireConnection;15;2;18;0
WireConnection;15;1;19;0
WireConnection;37;0;36;0
WireConnection;37;2;34;0
WireConnection;37;1;35;0
WireConnection;48;0;47;0
WireConnection;48;2;45;0
WireConnection;48;1;46;0
WireConnection;16;1;15;0
WireConnection;52;1;48;0
WireConnection;51;0;50;2
WireConnection;51;1;49;0
WireConnection;22;0;32;2
WireConnection;22;1;23;0
WireConnection;41;1;37;0
WireConnection;40;0;39;2
WireConnection;40;1;38;0
WireConnection;42;0;41;0
WireConnection;42;1;40;0
WireConnection;53;0;52;0
WireConnection;53;1;51;0
WireConnection;24;0;16;0
WireConnection;24;1;22;0
WireConnection;25;0;60;0
WireConnection;25;1;24;0
WireConnection;43;0;59;0
WireConnection;43;1;42;0
WireConnection;54;0;11;0
WireConnection;54;1;53;0
WireConnection;55;0;54;0
WireConnection;55;1;43;0
WireConnection;55;2;25;0
WireConnection;0;0;12;0
WireConnection;0;2;55;0
ASEEND*/
//CHKSM=7930F94192ABDB913CA6EBF32CB27B0693AB98C8