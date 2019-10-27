// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/TronRoad"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_BandaVerticalColor("Banda Vertical Color", Color) = (0,0.2271905,1,0)
		_Degradadospeed("Degradado speed", Vector) = (0,-0.5,0,0)
		[HDR]_DegradadoColor("Degradado Color", Color) = (0,0.5863873,4,0)
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

		uniform float4 _BandaVerticalColor;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;
		uniform float2 _Degradadospeed;
		uniform float4 _DegradadoColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			float4 temp_output_2_0 = ( 1.0 - tex2DNode1 );
			float2 panner8 = ( _Time.y * _Degradadospeed + i.uv_texcoord);
			o.Emission = ( ( _BandaVerticalColor * temp_output_2_0 ) + ( temp_output_2_0 * ( ( 1.0 - tex2D( _TextureSample1, panner8 ) ) * _DegradadoColor ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;73.6;761;575;442.6328;318.2393;1.443738;False;False
Node;AmplifyShaderEditor.CommentaryNode;19;-2079.452,251.4115;Float;False;1488.834;606.9524;Base degradado;8;10;11;12;8;7;13;15;16;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;11;-2000.452,439.4123;Float;False;Property;_Degradadospeed;Degradado speed;3;0;Create;True;0;0;False;0;0,-0.5;0,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;12;-2011.452,583.4124;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-2029.452,301.4118;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;8;-1638.452,339.4121;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;20;-1699.949,-558.4767;Float;False;964.4934;681.9998;Banda Verticales;4;1;2;3;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;7;-1439.069,401.4298;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;420d83c7d344b374d9dac1893b476af5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1649.949,-140.2382;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;13;-1092.732,403.567;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;15;-1130.941,651.3646;Float;False;Property;_DegradadoColor;Degradado Color;4;1;[HDR];Create;True;0;0;False;0;0,0.5863873,4,0;0,0.5863873,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-1311.155,-415.3773;Float;False;Property;_BandaVerticalColor;Banda Vertical Color;2;0;Create;True;0;0;False;0;0,0.2271905,1,0;0,0.2271905,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-825.6194,477.7146;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;2;-1288.455,-129.4768;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-970.4573,-342.4769;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-218.4526,140.9374;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;40;-2152.518,-1064.904;Float;False;1665.535;388.9066;Cuadrado;7;31;29;27;25;33;34;35;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;39;-432.6331,-511.9189;Float;False;Property;_CuadradoColor;Cuadrado Color;6;1;[HDR];Create;True;0;0;False;0;0,1,0.8947759,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;35;-729.9888,-932.4762;Float;False;Cuadrado;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-220.6331,-287.9189;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;36;-473.6331,-299.9189;Float;False;35;Cuadrado;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;42.54736,83.93744;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;31;-2102.518,-1008.091;Float;False;Property;_TilingSquare;Tiling Square;7;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;33;-1291.766,-790.9958;Float;False;32;BandaVerticales;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-1315.158,151.8819;Float;False;BandaVerticales;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;27;-1578.027,-1002.415;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1843.525,-1011.091;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1002.766,-972.9967;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;25;-1374.592,-1014.904;Float;True;Property;_Cuadrado;Cuadrado;5;0;Create;True;0;0;False;0;5b0cf7dd4cf97694e95750e891a5d5e6;5b0cf7dd4cf97694e95750e891a5d5e6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;461,-64;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/TronRoad;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;10;0
WireConnection;8;2;11;0
WireConnection;8;1;12;0
WireConnection;7;1;8;0
WireConnection;13;0;7;0
WireConnection;16;0;13;0
WireConnection;16;1;15;0
WireConnection;2;0;1;0
WireConnection;3;0;6;0
WireConnection;3;1;2;0
WireConnection;17;0;2;0
WireConnection;17;1;16;0
WireConnection;35;0;34;0
WireConnection;37;0;39;0
WireConnection;37;1;36;0
WireConnection;18;0;3;0
WireConnection;18;1;17;0
WireConnection;32;0;1;0
WireConnection;27;0;29;0
WireConnection;29;0;31;0
WireConnection;34;0;25;0
WireConnection;34;1;33;0
WireConnection;25;1;27;0
WireConnection;0;2;18;0
ASEEND*/
//CHKSM=10C589E73DB61827FC28876B4742DB6E32C87FD8