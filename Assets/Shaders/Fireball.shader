// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/Fireball"
{
	Properties
	{
		_NoiseMovement("Noise Movement", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_NoiseEffect("Noise Effect", Float) = 0.1
		_NoiseScale("Noise Scale", Float) = 3
		_ScaleTime("Scale Time", Float) = 1
		_Scaletime("Scale time", Float) = 0.5
		[HDR]_ExteriorColorr("Exterior Colorr", Color) = (1,0.3030913,0,0)
		_Tessellation("Tessellation", Float) = 5
		[HDR]_InteriorColor("Interior Color", Color) = (1,0.8624264,0,0)
		_TextureScale("Texture Scale", Float) = 0.5
		_Speed("Speed", Vector) = (0.5,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NoiseMovement;
		uniform float _Scaletime;
		uniform float _NoiseScale;
		uniform float _NoiseEffect;
		uniform float4 _InteriorColor;
		uniform float4 _ExteriorColorr;
		uniform sampler2D _Noise;
		uniform float _ScaleTime;
		uniform float2 _Speed;
		uniform float _TextureScale;
		uniform float _Tessellation;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_0 = (_Tessellation).xxxx;
			return temp_cast_0;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime41 = _Time.y * _Scaletime;
			float2 panner44 = ( mulTime41 * float2( 0,0 ) + ( v.texcoord.xy * _NoiseScale ));
			float4 tex2DNode46 = tex2Dlod( _NoiseMovement, float4( panner44, 0, 0.0) );
			v.vertex.xyz += ( ase_vertexNormal * tex2DNode46.r * tex2DNode46.g * tex2DNode46.b * _NoiseEffect );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime8 = _Time.y * _ScaleTime;
			float2 panner6 = ( mulTime8 * _Speed + ( i.uv_texcoord * _TextureScale ));
			float4 tex2DNode4 = tex2D( _Noise, panner6 );
			float4 lerpResult53 = lerp( _InteriorColor , _ExteriorColorr , ( tex2DNode4.r + tex2DNode4.r ));
			o.Emission = lerpResult53.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
887;73;1031;927;2246.367;730.2819;2.528324;False;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-2017.307,-70.13776;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-2017.307,101.8622;Float;False;Property;_TextureScale;Texture Scale;9;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1892.307,379.8622;Float;False;Property;_ScaleTime;Scale Time;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1712.308,-61.13777;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-1727.308,362.8622;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;10;-1697.308,164.8622;Float;False;Property;_Speed;Speed;10;0;Create;True;0;0;False;0;0.5,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;38;-2133.863,892.3976;Float;False;Property;_NoiseScale;Noise Scale;3;0;Create;True;0;0;False;0;3;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-2182.602,634.677;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;-1957.36,1133.527;Float;False;Property;_Scaletime;Scale time;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1895.973,685.7745;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;41;-1754.551,1114.584;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;6;-1481.308,22.86215;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;42;-1887.16,968.6078;Float;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;4;-1278.195,-128.8131;Float;True;Property;_Noise;Noise;1;0;Create;True;0;0;False;0;e28dc97a9541e3642a48c0e3886688c5;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;44;-1606.254,862.764;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-871.8636,958.3988;Float;False;Property;_NoiseEffect;Noise Effect;2;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;-1032.069,-418.8924;Float;False;Property;_ExteriorColorr;Exterior Colorr;6;1;[HDR];Create;True;0;0;False;0;1,0.3030913,0,0;1,0.4405404,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;54;-1049.97,-612.4916;Float;False;Property;_InteriorColor;Interior Color;8;1;[HDR];Create;True;0;0;False;0;1,0.8624264,0,0;1,0.8624264,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;45;-1677.831,594.4242;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;-1228.916,774.3505;Float;True;Property;_NoiseMovement;Noise Movement;0;0;Create;True;0;0;False;0;e28dc97a9541e3642a48c0e3886688c5;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-972.9815,55.09154;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-623.0347,726.4893;Float;True;5;5;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-677.4815,386.6435;Float;False;Property;_Tessellation;Tessellation;7;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;53;-678.4744,-60.39189;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-343.5671,-72.4524;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Robogods/Fireball;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;11;0
WireConnection;12;1;13;0
WireConnection;8;0;9;0
WireConnection;43;0;39;0
WireConnection;43;1;38;0
WireConnection;41;0;40;0
WireConnection;6;0;12;0
WireConnection;6;2;10;0
WireConnection;6;1;8;0
WireConnection;4;1;6;0
WireConnection;44;0;43;0
WireConnection;44;2;42;0
WireConnection;44;1;41;0
WireConnection;46;1;44;0
WireConnection;60;0;4;1
WireConnection;60;1;4;1
WireConnection;47;0;45;0
WireConnection;47;1;46;1
WireConnection;47;2;46;2
WireConnection;47;3;46;3
WireConnection;47;4;49;0
WireConnection;53;0;54;0
WireConnection;53;1;55;0
WireConnection;53;2;60;0
WireConnection;0;2;53;0
WireConnection;0;11;47;0
WireConnection;0;14;64;0
ASEEND*/
//CHKSM=87B04F807F483A52B1D3B84596A2086BF99367E2