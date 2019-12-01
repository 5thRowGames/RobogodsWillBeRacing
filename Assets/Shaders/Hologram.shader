// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/Hologram"
{
	Properties
	{
		_PowerFresnel("Power Fresnel", Range( -5 , 5)) = -0.2012867
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HDR]_Fresnelcolor("Fresnel color", Color) = (0.8962264,0.1733268,0.1733268,1)
		_ScanLines("ScanLines", 2D) = "white" {}
		_Speed("Speed", Vector) = (0,0,0,0)
		[HDR]_MainColor("MainColor", Color) = (0,0,0,0)
		_Tiling("Tiling", Float) = 3
		_LerpController("LerpController", Range( -15 , 15)) = 0
		_Metallic("Metallic", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		[HDR]_LineColor("Line Color", Color) = (0,0,0,0)
		_Thicknessnegativenumber("Thickness (negative number)", Float) = -0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _ScanLines;
		uniform float2 _Speed;
		uniform float _Tiling;
		uniform float4 _MainColor;
		uniform float _LerpController;
		uniform float _PowerFresnel;
		uniform float4 _Fresnelcolor;
		uniform float4 _LineColor;
		uniform float _Thicknessnegativenumber;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 panner28 = ( _Time.y * _Speed + ( ase_screenPosNorm * _Tiling ).xy);
			float4 ScanLines52 = ( 1.0 - tex2D( _ScanLines, panner28 ) );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float HologramPart93 = step( ase_vertex3Pos.y , _LerpController );
			float4 lerpResult88 = lerp( tex2D( _Albedo, uv_Albedo ) , ( ScanLines52 * _MainColor ) , HologramPart93);
			o.Albedo = lerpResult88.rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV13 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode13 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV13, _PowerFresnel ) );
			float4 Fresnel48 = ( HologramPart93 * ( fresnelNode13 * _Fresnelcolor ) );
			float temp_output_104_0 = step( ase_vertex3Pos.y , ( _LerpController + _Thicknessnegativenumber ) );
			float temp_output_98_0 = ( HologramPart93 - temp_output_104_0 );
			float4 Line114 = ( _LineColor * temp_output_98_0 );
			o.Emission = ( Fresnel48 + Line114 ).rgb;
			float temp_output_60_0 = ( 1.0 - HologramPart93 );
			float NormalPart62 = temp_output_60_0;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 break67 = ( NormalPart62 * tex2D( _Metallic, uv_Metallic ) );
			o.Metallic = break67;
			o.Smoothness = break67.a;
			o.Alpha = ( ( ScanLines52 * HologramPart93 ) + NormalPart62 ).r;
			clip( ( temp_output_60_0 + temp_output_98_0 + temp_output_104_0 ) - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float3 worldNormal : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;73;1203;509;3082.773;119.0414;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;53;-2770.084,-917.8076;Float;False;1558.193;676.6055;ScanLines;9;45;32;33;44;28;26;38;46;52;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;63;-2743.218,-108.6156;Float;False;1004.737;369.6608;Lerp Between textures;6;56;59;58;60;62;93;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;56;-2634.353,-25.87104;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;59;-2695.503,148.7308;Float;False;Property;_LerpController;LerpController;7;0;Create;True;0;0;False;0;0;0.3704835;-15;15;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;38;-2720.084,-867.8076;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;-2622.315,-640.6234;Float;False;Property;_Tiling;Tiling;6;0;Create;True;0;0;False;0;3;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;32;-2310.472,-350.8026;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;33;-2460.53,-476.3596;Float;False;Property;_Speed;Speed;4;0;Create;True;0;0;False;0;0,0;0,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;81;-2973.55,441.3658;Float;False;1631.585;503.2874;Fresnel;7;8;14;13;15;78;48;96;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StepOpNode;58;-2422.624,24.47715;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-2445.315,-659.6235;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;106;-2223.65,1246.743;Float;False;Property;_Thicknessnegativenumber;Thickness (negative number);11;0;Create;True;0;0;False;0;-0.01;-0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;103;-1860.807,1465.009;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;105;-1900.359,1202.883;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;93;-2281.16,78.15205;Float;True;HologramPart;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2923.55,582.1913;Float;False;Property;_PowerFresnel;Power Fresnel;0;0;Create;True;0;0;False;0;-0.2012867;1.86;-5;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;28;-2184.13,-587.1406;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;14;-2541.626,739.6533;Float;False;Property;_Fresnelcolor;Fresnel color;2;1;[HDR];Create;True;0;0;False;0;0.8962264,0.1733268,0.1733268,1;0,0.7634954,1.47451,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-1973.66,-531.181;Float;True;Property;_ScanLines;ScanLines;3;0;Create;True;0;0;False;0;None;85aa5e7893ad16e46885b08126f5729e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;13;-2609.261,491.3659;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;104;-1650.764,1235.166;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;108;-1528.423,1019.9;Float;True;93;HologramPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;98;-1188.687,1261.389;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;60;-2054.411,-21.60951;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;46;-1632.112,-518.3989;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2233.922,575.2752;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;96;-2058.775,497.6438;Float;False;93;HologramPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;113;-943.2792,1081.051;Float;False;Property;_LineColor;Line Color;10;1;[HDR];Create;True;0;0;False;0;0,0,0,0;2.258824,0,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1833.777,549.5101;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;52;-1451.892,-472.0941;Float;False;ScanLines;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;62;-1964.046,133.7451;Float;False;NormalPart;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-666.9898,1256.01;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-1641.845,26.41294;Float;False;93;HologramPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;74;-1636.698,-110.4729;Float;False;52;ScanLines;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;39;-1148.189,-183.557;Float;False;Property;_MainColor;MainColor;5;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0.5257993,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;89;-1113.627,-273.9577;Float;False;52;ScanLines;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-545.5883,391.1676;Float;False;62;NormalPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;48;-1581.964,542.0848;Float;False;Fresnel;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;114;-497.6017,1252.773;Float;False;Line;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;64;-748.9227,474.9547;Float;True;Property;_Metallic;Metallic;8;0;Create;True;0;0;False;0;None;404d5059ceafeef41a404f217de4cf40;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;94;-662.5079,-219.5157;Float;False;93;HologramPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;-506.901,-11.78046;Float;False;114;Line;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;50;-481.0883,-107.6031;Float;False;48;Fresnel;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-367.9694,470.6431;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;95;-1341.718,201.5599;Float;False;62;NormalPart;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1395.119,-39.09166;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-883.8299,-290.3779;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;72;-1069.472,-540.3097;Float;True;Property;_Albedo;Albedo;9;0;Create;True;0;0;False;0;None;4160b4eaa85016a4482240cdc65c4a91;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;67;-127.2522,477.3907;Float;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;124;-1098.263,387.9236;Float;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;77;-1116.672,86.79711;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;88;-435.9358,-467.802;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;129;-181.901,-63.78046;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;166.2215,-148.126;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/Hologram;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;58;0;56;2
WireConnection;58;1;59;0
WireConnection;44;0;38;0
WireConnection;44;1;45;0
WireConnection;105;0;59;0
WireConnection;105;1;106;0
WireConnection;93;0;58;0
WireConnection;28;0;44;0
WireConnection;28;2;33;0
WireConnection;28;1;32;0
WireConnection;26;1;28;0
WireConnection;13;3;8;0
WireConnection;104;0;103;2
WireConnection;104;1;105;0
WireConnection;98;0;108;0
WireConnection;98;1;104;0
WireConnection;60;0;93;0
WireConnection;46;0;26;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;78;0;96;0
WireConnection;78;1;15;0
WireConnection;52;0;46;0
WireConnection;62;0;60;0
WireConnection;109;0;113;0
WireConnection;109;1;98;0
WireConnection;48;0;78;0
WireConnection;114;0;109;0
WireConnection;65;0;66;0
WireConnection;65;1;64;0
WireConnection;73;0;74;0
WireConnection;73;1;97;0
WireConnection;90;0;89;0
WireConnection;90;1;39;0
WireConnection;67;0;65;0
WireConnection;124;0;60;0
WireConnection;124;1;98;0
WireConnection;124;2;104;0
WireConnection;77;0;73;0
WireConnection;77;1;95;0
WireConnection;88;0;72;0
WireConnection;88;1;90;0
WireConnection;88;2;94;0
WireConnection;129;0;50;0
WireConnection;129;1;130;0
WireConnection;0;0;88;0
WireConnection;0;2;129;0
WireConnection;0;3;67;0
WireConnection;0;4;67;3
WireConnection;0;9;77;0
WireConnection;0;10;124;0
ASEEND*/
//CHKSM=0A7A609728FFA5AD45C5D6FD3BE19BD32D107767