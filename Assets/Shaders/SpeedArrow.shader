// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/ArrowSpeed"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_ArrowSpeed("ArrowSpeed", Vector) = (0,-0.5,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_SpeedLinesSpeed("Speed Lines Speed", Vector) = (0,-0.7,0,0)
		_FirstColorSpeedLines("First Color Speed Lines", Color) = (0,0.52861,1,0)
		_HexagonalArrow("Hexagonal Arrow", 2D) = "white" {}
		_ArrowColor("Arrow Color", Color) = (1,0,0.333899,0)
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_DegradadoSpeed("Degradado Speed", Vector) = (0,-1,0,0)
		[HDR]_DegradadoColor("Degradado Color", Color) = (0,0,0,0)
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

		uniform float4 _DegradadoColor;
		uniform sampler2D _TextureSample2;
		uniform float2 _DegradadoSpeed;
		uniform sampler2D _HexagonalArrow;
		uniform float4 _HexagonalArrow_ST;
		uniform sampler2D _TextureSample0;
		uniform float2 _ArrowSpeed;
		uniform float4 _FirstColorSpeedLines;
		uniform sampler2D _TextureSample1;
		uniform float2 _SpeedLinesSpeed;
		uniform float4 _ArrowColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner85 = ( 1.0 * _Time.y * _DegradadoSpeed + i.uv_texcoord);
			float2 uv_HexagonalArrow = i.uv_texcoord * _HexagonalArrow_ST.xy + _HexagonalArrow_ST.zw;
			float4 tex2DNode37 = tex2D( _HexagonalArrow, uv_HexagonalArrow );
			float4 HexagonalOneMinus45 = ( 1.0 - tex2DNode37 );
			float2 panner5 = ( _Time.y * _ArrowSpeed + i.uv_texcoord);
			float4 tex2DNode4 = tex2D( _TextureSample0, panner5 );
			float4 Arrow11 = tex2DNode4;
			float2 panner20 = ( _Time.y * _SpeedLinesSpeed + i.uv_texcoord);
			float4 tex2DNode19 = tex2D( _TextureSample1, panner20 );
			float4 color25 = IsGammaSpace() ? float4(0,0.1812067,1,0) : float4(0,0.02754834,1,0);
			float4 SpeedLines34 = ( ( _FirstColorSpeedLines * tex2DNode19 ) + ( ( 1.0 - tex2DNode19 ) * color25 ) );
			float4 temp_cast_0 = (10.0).xxxx;
			float4 temp_output_81_0 = ( ( ( HexagonalOneMinus45 * Arrow11 ) * SpeedLines34 ) + ( _ArrowColor * pow( ( tex2DNode37 * Arrow11 ) , temp_cast_0 ) ) );
			float4 ArrowOneMinus16 = ( 1.0 - tex2DNode4 );
			o.Emission = ( ( ( _DegradadoColor * ( 1.0 - tex2D( _TextureSample2, panner85 ) ) ) * temp_output_81_0 ) + ( temp_output_81_0 + ( SpeedLines34 * ArrowOneMinus16 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
472.8;73.6;1061;711;2212.698;1723.052;1;False;False
Node;AmplifyShaderEditor.CommentaryNode;35;-2950.235,668.2935;Float;False;1907.639;787.317;Speed Lines;12;29;27;28;24;25;19;20;22;21;23;26;34;;1,0.3726415,0.3726415,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;23;-2804.235,1248.937;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-2900.235,960.9362;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;22;-2817.235,1095.936;Float;False;Property;_SpeedLinesSpeed;Speed Lines Speed;3;0;Create;True;0;0;False;0;0,-0.7;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;12;-2915.353,-5.819323;Float;False;1529.613;612.7142;Arrow;8;6;8;7;5;4;11;16;18;;0.07075471,0.4065633,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-2865.353,202.2948;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;20;-2487.234,989.9362;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;7;-2822.353,338.2947;Float;False;Property;_ArrowSpeed;ArrowSpeed;1;0;Create;True;0;0;False;0;0,-0.5;0,-1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;8;-2749.353,497.2947;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-2298.189,969.6285;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;d8d07a7791b086748923a856f879f5da;d8d07a7791b086748923a856f879f5da;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;5;-2487.353,276.2947;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;25;-1996.166,1250.61;Float;False;Constant;_SecondColorSpeedLines;Second Color Speed Lines;4;0;Create;True;0;0;False;0;0,0.1812067,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;82;-2243.556,-891.1642;Float;False;1895.804;859.6792;Transparent Arrow;14;37;66;44;42;80;73;45;48;74;76;79;38;75;81;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;26;-2209.229,718.2935;Float;False;Property;_FirstColorSpeedLines;First Color Speed Lines;4;0;Create;True;0;0;False;0;0,0.52861,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-2251.263,245.089;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;8dfe5daa378909c4fafb8652752f9444;8dfe5daa378909c4fafb8652752f9444;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;24;-1915.435,1026.512;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1852.355,728.8809;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1702.675,1084.413;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;96;-2034.459,-1584.357;Float;False;1567.203;516.7919;Degradado;7;88;87;85;84;90;95;91;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;37;-2193.556,-587.9711;Float;True;Property;_HexagonalArrow;Hexagonal Arrow;5;0;Create;True;0;0;False;0;e130dea2a58a633448b23e3e75802806;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-1863.57,266.4878;Float;False;Arrow;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;88;-1931.159,-1246.066;Float;False;Property;_DegradadoSpeed;Degradado Speed;8;0;Create;True;0;0;False;0;0,-1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1534.354,836.8809;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;-2190.692,-339.0037;Float;False;11;Arrow;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;66;-1937.116,-712.5441;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;87;-1984.459,-1394.266;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;85;-1591.859,-1296.765;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-1869.457,-193.8303;Float;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;18;-1919.564,483.3405;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;-1693.306,-705.9868;Float;True;11;Arrow;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-1714.397,-812.5497;Float;False;HexagonalOneMinus;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;-1282.599,906.4891;Float;False;SpeedLines;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1835.193,-491.4034;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-1348.147,-841.1642;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;79;-1561.358,-467.6301;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-1671.271,475.5394;Float;False;ArrowOneMinus;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;76;-1438.704,-654.804;Float;False;34;SpeedLines;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;48;-1256.219,-539.8957;Float;False;Property;_ArrowColor;Arrow Color;6;0;Create;True;0;0;False;0;1,0,0.333899,0;0.1448123,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;84;-1301.882,-1346.921;Float;True;Property;_TextureSample2;Texture Sample 2;7;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;36;-782.8069,378.4797;Float;False;34;SpeedLines;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1188.335,-771.046;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1019.891,-284.0851;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;-776.7091,546.6797;Float;False;16;ArrowOneMinus;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;90;-944.4567,-1320.165;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;95;-934.2057,-1534.357;Float;False;Property;_DegradadoColor;Degradado Color;9;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-402.6049,536.9615;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-583.152,-389.1341;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-702.6564,-1322.766;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-54.68404,-273.1308;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-83.81824,290.8282;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;94;217.4911,26.06626;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;550.4568,169.3296;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/ArrowSpeed;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;21;0
WireConnection;20;2;22;0
WireConnection;20;1;23;0
WireConnection;19;1;20;0
WireConnection;5;0;6;0
WireConnection;5;2;7;0
WireConnection;5;1;8;0
WireConnection;4;1;5;0
WireConnection;24;0;19;0
WireConnection;28;0;26;0
WireConnection;28;1;19;0
WireConnection;27;0;24;0
WireConnection;27;1;25;0
WireConnection;11;0;4;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;66;0;37;0
WireConnection;85;0;87;0
WireConnection;85;2;88;0
WireConnection;18;0;4;0
WireConnection;45;0;66;0
WireConnection;34;0;29;0
WireConnection;42;0;37;0
WireConnection;42;1;44;0
WireConnection;74;0;45;0
WireConnection;74;1;73;0
WireConnection;79;0;42;0
WireConnection;79;1;80;0
WireConnection;16;0;18;0
WireConnection;84;1;85;0
WireConnection;75;0;74;0
WireConnection;75;1;76;0
WireConnection;38;0;48;0
WireConnection;38;1;79;0
WireConnection;90;0;84;0
WireConnection;30;0;36;0
WireConnection;30;1;17;0
WireConnection;81;0;75;0
WireConnection;81;1;38;0
WireConnection;91;0;95;0
WireConnection;91;1;90;0
WireConnection;93;0;91;0
WireConnection;93;1;81;0
WireConnection;31;0;81;0
WireConnection;31;1;30;0
WireConnection;94;0;93;0
WireConnection;94;1;31;0
WireConnection;0;2;94;0
ASEEND*/
//CHKSM=F318562D9A272BE1F7DB2FBE737DDED51B030520