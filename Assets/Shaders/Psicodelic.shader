// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/Psicodelic"
{
	Properties
	{
		_BPM("BPM", Float) = 124
		_TextureSample("Texture Sample", 2D) = "white" {}
		_LedtSlider("Ledt Slider", Range( -1 , 1)) = 0.682353
		_PannerSpeed("Panner Speed", Vector) = (0,0.5,0,0)
		_RightSlider("Right Slider", Range( -1 , 1)) = -0.5764706
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
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _RightSlider;
		uniform float _LedtSlider;
		uniform sampler2D _TextureSample;
		uniform float _BPM;
		uniform float2 _PannerSpeed;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_49_0 = ( step( ase_vertex3Pos.x , _RightSlider ) + ( 1.0 - step( ase_vertex3Pos.x , _LedtSlider ) ) );
			float4 color33 = IsGammaSpace() ? float4(0,0.1046919,0.2075472,0) : float4(0,0.01076646,0.03550519,0);
			o.Albedo = ( temp_output_49_0 * color33 ).rgb;
			float BPM5 = ( _BPM / 60.0 );
			float mulTime37 = _Time.y * BPM5;
			float2 panner30 = ( mulTime37 * _PannerSpeed + i.uv_texcoord);
			float4 color60 = IsGammaSpace() ? float4(0,0.5705111,1,0) : float4(0,0.2852057,1,0);
			o.Emission = ( ( 1.0 - tex2D( _TextureSample, panner30 ) ) * temp_output_49_0 * color60 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1027;73;891;927;1434.851;92.77957;1.05264;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-3532.054,180.7535;Float;False;706;286;BPM;4;5;4;3;2;;1,0,0.6797638,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3467.054,351.7528;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;60;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-3460.098,251.4326;Float;False;Property;_BPM;BPM;0;0;Create;True;0;0;False;0;124;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;3;-3226.055,249.7531;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;5;-3069.055,272.753;Float;False;BPM;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;-2179.033,379.462;Float;False;5;BPM;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-2109.284,-1.846844;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;37;-1847.033,376.462;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;38;-2099.033,198.4622;Float;False;Property;_PannerSpeed;Panner Speed;5;0;Create;True;0;0;False;0;0,0.5;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PosVertexDataNode;43;-1744.219,-390.6933;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;-1735.634,-202.1927;Float;False;Property;_LedtSlider;Ledt Slider;4;0;Create;True;0;0;False;0;0.682353;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-1763.634,-609.1927;Float;False;Property;_RightSlider;Right Slider;6;0;Create;True;0;0;False;0;-0.5764706;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;44;-1389.633,-320.1927;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;30;-1817.045,155.7312;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;46;-1408.633,-637.1927;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;32;-1337.872,121.0047;Float;True;Property;_TextureSample;Texture Sample;3;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;b038ed57d601c9449bb1dd51bfbef468;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;48;-1131.632,-321.1927;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-1297.97,362.7719;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;0,0.1046919,0.2075472,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;24;-2400.039,550.1813;Float;False;1144.461;567;(k * x) * exp(1-(k*x));7;19;20;21;22;17;18;23;;0.1367925,1,0.5356568,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;59;-952.1685,291.7698;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-844.1794,-347.7747;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;60;-1000.635,575.6476;Float;False;Constant;_Color1;Color 1;7;0;Create;True;0;0;False;0;0,0.5705111,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;16;-3960.097,586.6334;Float;False;1488.567;488.6138;mod(x,1);8;15;6;8;9;11;12;13;14;;0,0.5312119,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-2702.528,794.248;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-736.6423,592.0219;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-3275.999,915.6343;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2095.575,641.1813;Float;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;19;-1899.683,902.1347;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;9;False;1;FLOAT;0
Node;AmplifyShaderEditor.ExpOpNode;22;-1647.572,743.1812;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-2117.674,796.7102;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-3502.746,736.6526;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1492.572,743.1811;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-611.8199,273.9869;Float;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-793.9028,-17.49935;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-3711.66,787.8817;Float;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-3889.956,659.0389;Float;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;6;-3916.053,792.8862;Float;False;5;BPM;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2982.529,964.2485;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2319.038,775.2267;Float;False;Property;_Impulse;Impulse;2;0;Create;True;0;0;False;0;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;20;-1820.572,665.1813;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleRemainderNode;13;-2964.333,693.4344;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-122.5181,72.26258;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/Psicodelic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;3;1;4;0
WireConnection;5;0;3;0
WireConnection;37;0;39;0
WireConnection;44;0;43;1
WireConnection;44;1;45;0
WireConnection;30;0;31;0
WireConnection;30;2;38;0
WireConnection;30;1;37;0
WireConnection;46;0;43;1
WireConnection;46;1;47;0
WireConnection;32;1;30;0
WireConnection;48;0;44;0
WireConnection;59;0;32;0
WireConnection;49;0;46;0
WireConnection;49;1;48;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;52;1;49;0
WireConnection;19;0;18;0
WireConnection;22;0;20;0
WireConnection;18;0;17;0
WireConnection;18;1;14;0
WireConnection;9;0;11;0
WireConnection;9;1;8;0
WireConnection;23;0;22;0
WireConnection;23;1;18;0
WireConnection;53;0;59;0
WireConnection;53;1;49;0
WireConnection;53;2;60;0
WireConnection;50;0;49;0
WireConnection;50;1;33;0
WireConnection;8;0;6;0
WireConnection;20;0;21;0
WireConnection;20;1;19;0
WireConnection;13;0;9;0
WireConnection;13;1;12;0
WireConnection;0;0;50;0
WireConnection;0;2;53;0
ASEEND*/
//CHKSM=A091234BEE2AB3A67C85967974207CCEF25A9131