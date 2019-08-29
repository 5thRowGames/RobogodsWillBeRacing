// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/EnergyShield"
{
	Properties
	{
		_Fresnelopacitypower("Fresnel opacity power", Range( 0 , 4)) = 4
		_Adding("Adding", Range( -3 , 3)) = 0.16
		[HDR]_MainColor("Main Color", Color) = (0.4455402,0,0.5943396,0)
		_ExteriorRim("Exterior Rim", Float) = 3
		_GlowPositionController("Glow Position Controller", Range( -5 , 5)) = -0.2352941
		_AddGlowPosition("Add Glow Position", Range( 0 , 0.3)) = 0.1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _MainColor;
		uniform float _ExteriorRim;
		uniform float _Fresnelopacitypower;
		uniform float _Adding;
		uniform float _GlowPositionController;
		uniform float _AddGlowPosition;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV91 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode91 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV91, _Fresnelopacitypower ) );
			float temp_output_110_0 = ( fresnelNode91 + _Adding );
			float4 temp_output_120_0 = ( _MainColor * ( ( _ExteriorRim * fresnelNode91 ) + temp_output_110_0 ) );
			float4 color142 = IsGammaSpace() ? float4(3.576471,0,4.768628,0) : float4(16.50442,0,31.07889,0);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_147_0 = ( ase_vertex3Pos.x + ase_vertex3Pos.z );
			o.Albedo = ( temp_output_120_0 + ( color142 * ( step( temp_output_147_0 , ( _GlowPositionController + _AddGlowPosition ) ) - step( temp_output_147_0 , _GlowPositionController ) ) ) ).rgb;
			o.Emission = temp_output_120_0.rgb;
			o.Alpha = temp_output_110_0;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
984;73;934;927;-525.1776;-594.7291;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;92;880.0261,462.0998;Float;False;Property;_Fresnelopacitypower;Fresnel opacity power;0;0;Create;True;0;0;False;0;4;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;145;799.457,910.6426;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;134;891.1617,1152.421;Float;False;Property;_GlowPositionController;Glow Position Controller;4;0;Create;True;0;0;False;0;-0.2352941;0;-5;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;138;841.0403,1268.154;Float;False;Property;_AddGlowPosition;Add Glow Position;5;0;Create;True;0;0;False;0;0.1;0.1;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;126;1311.212,12.61978;Float;False;Property;_ExteriorRim;Exterior Rim;3;0;Create;True;0;0;False;0;3;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;91;1168.572,373.566;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;1198.736,599.6456;Float;False;Property;_Adding;Adding;1;0;Create;True;0;0;False;0;0.16;0.16;-3;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;137;1207.04,1254.154;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;147;1046.421,860.4518;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;146;1344.021,910.6512;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;148;1451.241,1212.009;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;110;1600.463,521.1693;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;1552.212,14.61978;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;119;1962.212,313.6198;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;114;1974.582,66.84168;Float;False;Property;_MainColor;Main Color;2;1;[HDR];Create;True;0;0;False;0;0.4455402,0,0.5943396,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;139;1835.178,1050.7;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;142;1918.501,731.5994;Float;False;Constant;_Color0;Color 0;5;1;[HDR];Create;True;0;0;False;0;3.576471,0,4.768628,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;2226.212,254.6198;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;2105.501,921.5994;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;144;2470.501,712.5994;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2745.844,368.0737;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/EnergyShield;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;91;3;92;0
WireConnection;137;0;134;0
WireConnection;137;1;138;0
WireConnection;147;0;145;1
WireConnection;147;1;145;3
WireConnection;146;0;147;0
WireConnection;146;1;134;0
WireConnection;148;0;147;0
WireConnection;148;1;137;0
WireConnection;110;0;91;0
WireConnection;110;1;111;0
WireConnection;125;0;126;0
WireConnection;125;1;91;0
WireConnection;119;0;125;0
WireConnection;119;1;110;0
WireConnection;139;0;148;0
WireConnection;139;1;146;0
WireConnection;120;0;114;0
WireConnection;120;1;119;0
WireConnection;143;0;142;0
WireConnection;143;1;139;0
WireConnection;144;0;120;0
WireConnection;144;1;143;0
WireConnection;0;0;144;0
WireConnection;0;2;120;0
WireConnection;0;9;110;0
ASEEND*/
//CHKSM=CD2740E2DDD47583C1AE289778926F828CAED0AC