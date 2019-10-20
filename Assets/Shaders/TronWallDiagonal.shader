// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/TronWallDiagonal"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_SpeedDegragado("Speed Degragado", Vector) = (0,-1,0,0)
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

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;
		uniform float2 _SpeedDegragado;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color13 = IsGammaSpace() ? float4(0.006182671,0,1,0) : float4(0.0004785349,0,1,0);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Albedo = ( color13 * tex2DNode1 ).rgb;
			float4 color14 = IsGammaSpace() ? float4(0.1647059,0.509804,2,0) : float4(0.02315336,0.223228,4.594794,0);
			float2 panner3 = ( _Time.y * _SpeedDegragado + i.uv_texcoord);
			o.Emission = ( tex2DNode1 * ( color14 * ( 1.0 - tex2D( _TextureSample1, panner3 ) ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
535.2;73.6;999;711;2205.743;1332.438;3.788244;False;False
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1317.173,712.8528;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;4;-1433.173,571.8528;Float;False;Property;_SpeedDegragado;Speed Degragado;2;0;Create;True;0;0;False;0;0,-1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1607.173,419.8531;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;3;-1169.812,524.1767;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-977.6351,456.3127;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;9;-623.0952,469.8201;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;14;-677.7986,156.0561;Float;False;Constant;_Color1;Color 1;3;1;[HDR];Create;True;0;0;False;0;0.1647059,0.509804,2,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-411.219,241.433;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-813.5447,-153.7145;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;c78d1d220b527f242801460561e58243;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-572.4986,-380.4438;Float;False;Constant;_Color0;Color 0;3;1;[HDR];Create;True;0;0;False;0;0.006182671,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-393.0987,-173.7437;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-78.50063,83.65704;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;292.4998,-26;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/TronWallDiagonal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;6;0
WireConnection;3;2;4;0
WireConnection;3;1;5;0
WireConnection;2;1;3;0
WireConnection;9;0;2;0
WireConnection;11;0;14;0
WireConnection;11;1;9;0
WireConnection;12;0;13;0
WireConnection;12;1;1;0
WireConnection;15;0;1;0
WireConnection;15;1;11;0
WireConnection;0;0;12;0
WireConnection;0;2;15;0
ASEEND*/
//CHKSM=A729EF398DCBBA48695A112876BCF83D515DCE1C