// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/TronAnubis"
{
	Properties
	{
		_Anubis("Anubis", 2D) = "white" {}
		_BandasVerticales("Bandas Verticales", 2D) = "white" {}
		_Degradadovertical("Degradado vertical", 2D) = "white" {}
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

		uniform sampler2D _Anubis;
		uniform float4 _Anubis_ST;
		uniform sampler2D _BandasVerticales;
		uniform float4 _BandasVerticales_ST;
		uniform sampler2D _Degradadovertical;
		uniform float2 _Degradadospeed;
		uniform float4 _DegradadoColor;
		uniform float4 _BandaVerticalColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Anubis = i.uv_texcoord * _Anubis_ST.xy + _Anubis_ST.zw;
			float2 uv_BandasVerticales = i.uv_texcoord * _BandasVerticales_ST.xy + _BandasVerticales_ST.zw;
			float4 tex2DNode45 = tex2D( _BandasVerticales, uv_BandasVerticales );
			float4 BandaVerticales43 = tex2DNode45;
			float2 panner36 = ( _Time.y * _Degradadospeed + i.uv_texcoord);
			float4 temp_output_38_0 = ( 1.0 - tex2D( _Degradadovertical, panner36 ) );
			float4 Degradado48 = temp_output_38_0;
			float4 ColorDegradado56 = _DegradadoColor;
			float4 temp_output_46_0 = ( 1.0 - tex2DNode45 );
			o.Emission = ( ( ( 1.0 - ( tex2D( _Anubis, uv_Anubis ) * BandaVerticales43 ) ) * ( Degradado48 * BandaVerticales43 ) * ColorDegradado56 ) + ( ( _BandaVerticalColor * temp_output_46_0 ) + ( temp_output_46_0 * ( temp_output_38_0 * _DegradadoColor ) ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;73;1203;927;2613.909;-878.3549;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;32;-2183.44,1201.308;Float;False;1488.834;606.9524;Base degradado;10;40;39;38;37;36;35;34;33;48;56;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-2133.441,1251.308;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;34;-2104.442,1389.308;Float;False;Property;_Degradadospeed;Degradado speed;4;0;Create;True;0;0;False;0;0,-0.5;0,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;33;-2115.441,1533.308;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-1742.443,1289.308;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;37;-1543.059,1351.326;Float;True;Property;_Degradadovertical;Degradado vertical;2;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;420d83c7d344b374d9dac1893b476af5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;45;-1753.939,809.6573;Float;True;Property;_BandasVerticales;Bandas Verticales;1;0;Create;True;0;0;False;0;1eac590d9d0388e47b3067eb6f4b34a3;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;38;-1196.722,1353.463;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;43;-1409.833,1084.478;Float;False;BandaVerticales;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1441.209,5.194211;Float;True;Property;_Anubis;Anubis;0;0;Create;True;0;0;False;0;f2be3c75b30d9f54a889cd3eef203db4;f2be3c75b30d9f54a889cd3eef203db4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;48;-994.3311,1284.124;Float;False;Degradado;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;53;-1268.725,248.2733;Float;False;43;BandaVerticales;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;39;-1574.931,1601.26;Float;False;Property;_DegradadoColor;Degradado Color;5;1;[HDR];Create;True;0;0;False;0;0,0.5863873,4,0;0,0.5863873,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;49;-750.3311,134.1178;Float;False;48;Degradado;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-983.7888,51.66657;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;56;-1186.469,1683.254;Float;False;ColorDegradado;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;46;-1331.073,821.4252;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;44;-1077.377,500.8616;Float;False;Property;_BandaVerticalColor;Banda Vertical Color;3;0;Create;True;0;0;False;0;0,0.2271905,1,0;0,0.2271905,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-929.6086,1427.61;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-487.0369,175.5422;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;58;-587.4686,331.2535;Float;False;56;ColorDegradado;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-491.4417,916.633;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-794.3765,613.4291;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;26;-624.0349,-38.38643;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-256.2491,494.5637;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-288.4613,103.324;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-1.074984,276.7464;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;318.4997,-37.69999;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/TronAnubis;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;36;0;35;0
WireConnection;36;2;34;0
WireConnection;36;1;33;0
WireConnection;37;1;36;0
WireConnection;38;0;37;0
WireConnection;43;0;45;0
WireConnection;48;0;38;0
WireConnection;50;0;1;0
WireConnection;50;1;53;0
WireConnection;56;0;39;0
WireConnection;46;0;45;0
WireConnection;40;0;38;0
WireConnection;40;1;39;0
WireConnection;51;0;49;0
WireConnection;51;1;53;0
WireConnection;41;0;46;0
WireConnection;41;1;40;0
WireConnection;42;0;44;0
WireConnection;42;1;46;0
WireConnection;26;0;50;0
WireConnection;47;0;42;0
WireConnection;47;1;41;0
WireConnection;12;0;26;0
WireConnection;12;1;51;0
WireConnection;12;2;58;0
WireConnection;54;0;12;0
WireConnection;54;1;47;0
WireConnection;0;2;54;0
ASEEND*/
//CHKSM=055A88163CB0CAA049308C4883E379E406A748E6