// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/GoThrougPortal"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Speed("Speed", Vector) = (0.5,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,0.6190026,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform sampler2D _TextureSample0;
		uniform float2 _Speed;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord2 = i.uv_texcoord * float2( 0.3,0.3 );
			float2 panner3 = ( 1.0 * _Time.y * _Speed + uv_TexCoord2);
			float4 temp_output_16_0 = ( tex2D( _TextureSample0, panner3 ) / 0.7 );
			o.Albedo = ( _Color0 * temp_output_16_0 ).rgb;
			o.Alpha = 1;
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			clip( ( temp_output_16_0 * tex2D( _TextureSample1, uv_TextureSample1 ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;73.6;825;587;1594.462;230.223;1.687487;False;False
Node;AmplifyShaderEditor.Vector2Node;22;-1642.406,44.84097;Float;False;Constant;_Vector2;Vector 2;4;0;Create;True;0;0;False;0;0.3,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1374.566,4.689703;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;4;-1276.666,133.2897;Float;False;Property;_Speed;Speed;2;0;Create;True;0;0;False;0;0.5,0;0.5,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;3;-1039.617,56.5648;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;13;-749.1863,-30.50768;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;bdbe94d7623ec3940947b62544306f1c;ee34453d880644e44806eb9461c77325;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-690.8147,192.6662;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-530.9156,214.5415;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;75b53abac5b2dcf4a8af9b4a89242d9e;75b53abac5b2dcf4a8af9b4a89242d9e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-334.8388,-227.9373;Float;False;Property;_Color0;Color 0;4;1;[HDR];Create;True;0;0;False;0;1,0.6190026,0,0;1,0.6190026,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-394.4147,-20.53381;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-77.40576,177.841;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-59.40576,-87.15903;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;237.9001,-6.199999;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/GoThrougPortal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;22;0
WireConnection;3;0;2;0
WireConnection;3;2;4;0
WireConnection;13;1;3;0
WireConnection;16;0;13;0
WireConnection;16;1;17;0
WireConnection;19;0;16;0
WireConnection;19;1;8;0
WireConnection;20;0;11;0
WireConnection;20;1;16;0
WireConnection;0;0;20;0
WireConnection;0;10;19;0
ASEEND*/
//CHKSM=B8615CFD0AA6E2B31AD1777D75A82FF9571831A9