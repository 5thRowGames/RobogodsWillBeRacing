// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/FinishLine"
{
	Properties
	{
		_LineasVerticales("Lineas Verticales", 2D) = "white" {}
		_Degradado("Degradado", 2D) = "white" {}
		_BandaVerticalColor("Banda Vertical Color", Color) = (0,0.2271905,1,0)
		_Degradadospeed("Degradado speed", Vector) = (0,-0.5,0,0)
		[HDR]_DegradadoColor("Degradado Color", Color) = (0,0.5863873,4,0)
		_Ajedrez("Ajedrez", 2D) = "white" {}
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
		};

		uniform sampler2D _Ajedrez;
		uniform float4 _Ajedrez_ST;
		uniform float4 _BandaVerticalColor;
		uniform sampler2D _LineasVerticales;
		uniform float4 _LineasVerticales_ST;
		uniform sampler2D _Degradado;
		uniform float2 _Degradadospeed;
		uniform float4 _DegradadoColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Ajedrez = i.uv_texcoord * _Ajedrez_ST.xy + _Ajedrez_ST.zw;
			float2 uv_LineasVerticales = i.uv_texcoord * _LineasVerticales_ST.xy + _LineasVerticales_ST.zw;
			float4 tex2DNode3 = tex2D( _LineasVerticales, uv_LineasVerticales );
			float4 temp_output_12_0 = ( 1.0 - tex2DNode3 );
			float2 panner8 = ( _Time.y * _Degradadospeed + i.uv_texcoord);
			o.Emission = ( tex2D( _Ajedrez, uv_Ajedrez ) + ( ( _BandaVerticalColor * temp_output_12_0 ) + ( temp_output_12_0 * ( ( 1.0 - tex2D( _Degradado, panner8 ) ) * _DegradadoColor ) ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
596.8;73.6;937;711;937.1006;601.0092;1.610649;False;False
Node;AmplifyShaderEditor.CommentaryNode;2;-1158.175,147.907;Float;False;1488.834;606.9524;Base degradado;8;15;11;10;9;8;6;5;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-1090.175,479.9079;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1108.175,197.9073;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;6;-1079.175,335.9078;Float;False;Property;_Degradadospeed;Degradado speed;3;0;Create;True;0;0;False;0;0,-0.5;0,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;8;-717.175,235.9076;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;9;-517.792,297.9253;Float;True;Property;_Degradado;Degradado;1;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;420d83c7d344b374d9dac1893b476af5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;1;-778.672,-661.9812;Float;False;964.4934;681.9998;Banda Verticales;4;19;16;12;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;11;-171.4551,300.0625;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-728.672,-243.7427;Float;True;Property;_LineasVerticales;Lineas Verticales;0;0;Create;True;0;0;False;0;9c8e1aa1caab632468e211716a4bc610;9c8e1aa1caab632468e211716a4bc610;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-209.6641,547.8601;Float;False;Property;_DegradadoColor;Degradado Color;4;1;[HDR];Create;True;0;0;False;0;0,0.5863873,4,0;0,0.5863873,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;-389.8781,-518.8818;Float;False;Property;_BandaVerticalColor;Banda Vertical Color;2;0;Create;True;0;0;False;0;0,0.2271905,1,0;0,0.2271905,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;12;-367.178,-232.9813;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;95.65759,374.2101;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;702.8243,37.4329;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-49.1803,-445.9814;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;21;560.9355,-658.6833;Float;True;Property;_Ajedrez;Ajedrez;5;0;Create;True;0;0;False;0;5d63fc507d303384985c4b31391c98f2;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;20;963.8243,-19.56705;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-393.881,48.3774;Float;False;BandaVerticales;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;1146.9,-272.2341;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1423.089,-149.4977;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/FinishLine;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;5;0
WireConnection;8;2;6;0
WireConnection;8;1;4;0
WireConnection;9;1;8;0
WireConnection;11;0;9;0
WireConnection;12;0;3;0
WireConnection;15;0;11;0
WireConnection;15;1;10;0
WireConnection;17;0;12;0
WireConnection;17;1;15;0
WireConnection;19;0;16;0
WireConnection;19;1;12;0
WireConnection;20;0;19;0
WireConnection;20;1;17;0
WireConnection;7;0;3;0
WireConnection;22;0;21;0
WireConnection;22;1;20;0
WireConnection;0;2;22;0
ASEEND*/
//CHKSM=3A79763556877B572C64A7BBD7E7D3336F39053B