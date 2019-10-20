// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/TronAnubis"
{
	Properties
	{
		_Anubis("Anubis", 2D) = "white" {}
		_Degradadovertical("Degradado vertical", 2D) = "white" {}
		_DegradadoSpeed("Degradado Speed", Vector) = (0,-1,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		[HDR]_AlbedoColor("Albedo Color", Color) = (0,0,0,0)
		_VerticalLines("Vertical Lines", 2D) = "white" {}
		[HDR]_DegradadoColor("Degradado Color", Color) = (0,0,0,0)
		[HDR]_AnubisColor("Anubis Color", Color) = (0,0,0,0)
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

		uniform float4 _AlbedoColor;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Anubis;
		uniform float4 _Anubis_ST;
		uniform sampler2D _Degradadovertical;
		uniform float2 _DegradadoSpeed;
		uniform float4 _AnubisColor;
		uniform sampler2D _VerticalLines;
		uniform float4 _VerticalLines_ST;
		uniform float4 _DegradadoColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = ( _AlbedoColor * tex2D( _Albedo, uv_Albedo ) ).rgb;
			float2 uv_Anubis = i.uv_texcoord * _Anubis_ST.xy + _Anubis_ST.zw;
			float2 panner3 = ( _Time.y * _DegradadoSpeed + i.uv_texcoord);
			float4 Degradado18 = ( 1.0 - tex2D( _Degradadovertical, panner3 ) );
			float2 uv_VerticalLines = i.uv_texcoord * _VerticalLines_ST.xy + _VerticalLines_ST.zw;
			o.Emission = ( ( tex2D( _Anubis, uv_Anubis ) * Degradado18 * _AnubisColor ) + ( Degradado18 * tex2D( _VerticalLines, uv_VerticalLines ) * _DegradadoColor ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
535.2;73.6;999;711;238.5349;573.5844;1;False;False
Node;AmplifyShaderEditor.Vector2Node;5;-2274.883,333.8959;Float;False;Property;_DegradadoSpeed;Degradado Speed;2;0;Create;True;0;0;False;0;0,-1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;6;-2226.883,473.8959;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-2328.883,210.8958;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;3;-1899.883,234.8958;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-1676.883,223.8958;Float;True;Property;_Degradadovertical;Degradado vertical;1;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-1364.294,225.4745;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-1139.774,296.9008;Float;False;Degradado;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;16;-760.7684,677.9611;Float;True;Property;_VerticalLines;Vertical Lines;5;0;Create;True;0;0;False;0;af0459d7c278c414aadff56e68557f45;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;24;-570.5975,244.0653;Float;False;Property;_AnubisColor;Anubis Color;7;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;19;-553.7852,133.5926;Float;False;18;Degradado;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;20;-723.099,589.8946;Float;False;18;Degradado;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-651.6832,-83.36091;Float;True;Property;_Anubis;Anubis;0;0;Create;True;0;0;False;0;f2be3c75b30d9f54a889cd3eef203db4;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;-762.7251,906.9041;Float;False;Property;_DegradadoColor;Degradado Color;6;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-366.4628,616.3125;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-288.4613,103.324;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;13;-163.4415,-301.9102;Float;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;af0459d7c278c414aadff56e68557f45;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-20.14173,-486.1977;Float;False;Property;_AlbedoColor;Albedo Color;4;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;185.4881,-217.41;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-66.26278,278.8881;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;318.4997,-37.69999;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/TronAnubis;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;4;0
WireConnection;3;2;5;0
WireConnection;3;1;6;0
WireConnection;2;1;3;0
WireConnection;7;0;2;0
WireConnection;18;0;7;0
WireConnection;21;0;20;0
WireConnection;21;1;16;0
WireConnection;21;2;23;0
WireConnection;12;0;1;0
WireConnection;12;1;19;0
WireConnection;12;2;24;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;25;0;12;0
WireConnection;25;1;21;0
WireConnection;0;0;15;0
WireConnection;0;2;25;0
ASEEND*/
//CHKSM=25578754B527D7D437D46F86DBF09BBE53EA3226