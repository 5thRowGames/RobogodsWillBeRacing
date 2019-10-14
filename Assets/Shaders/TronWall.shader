// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/TronWall"
{
	Properties
	{
		_BandasVerticales("Bandas Verticales", 2D) = "white" {}
		_Degradadovertical("Degradado vertical", 2D) = "white" {}
		_BandaVerticalColor("Banda Vertical Color", Color) = (0,0.2271905,1,0)
		_Degradadospeed("Degradado speed", Vector) = (0,-0.5,0,0)
		[HDR]_DegradadoColor("Degradado Color", Color) = (0,0.5863873,4,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Vector0("Vector 0", Vector) = (0,-0.5,0,0)
		_Tiling("Tiling", Vector) = (1,1,0,0)
		[HDR]_BandaSolaColor("BandaSolaColor", Color) = (0,0.03912401,1,0)
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
		uniform sampler2D _BandasVerticales;
		uniform float4 _BandasVerticales_ST;
		uniform sampler2D _Degradadovertical;
		uniform float2 _Degradadospeed;
		uniform float4 _DegradadoColor;
		uniform sampler2D _TextureSample0;
		uniform float2 _Vector0;
		uniform float2 _Tiling;
		uniform float4 _BandaSolaColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BandasVerticales = i.uv_texcoord * _BandasVerticales_ST.xy + _BandasVerticales_ST.zw;
			float4 tex2DNode7 = tex2D( _BandasVerticales, uv_BandasVerticales );
			float4 temp_output_18_0 = ( 1.0 - tex2DNode7 );
			float2 panner12 = ( _Time.y * _Degradadospeed + i.uv_texcoord);
			float2 uv_TexCoord23 = i.uv_texcoord * _Tiling;
			float2 panner22 = ( _Time.y * _Vector0 + uv_TexCoord23);
			float4 BandaSolaVertical29 = tex2D( _TextureSample0, panner22 );
			o.Emission = ( ( _BandaVerticalColor * temp_output_18_0 ) + ( temp_output_18_0 * ( ( 1.0 - tex2D( _Degradadovertical, panner12 ) ) * _DegradadoColor ) ) + ( BandaSolaVertical29 * _BandaSolaColor ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
638;73;1280;927;2455.644;675.5575;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;2;-2362.594,597.7178;Float;False;1488.834;606.9524;Base degradado;8;17;15;14;13;12;11;10;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;27;-2643.056,-680.0071;Float;False;Property;_Tiling;Tiling;7;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-2391.056,-688.0071;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;25;-2319.056,-558.0071;Float;False;Property;_Vector0;Vector 0;6;0;Create;True;0;0;False;0;0,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;26;-2315.056,-414.0071;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;11;-2294.594,929.7182;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;9;-2283.594,785.7184;Float;False;Property;_Degradadospeed;Degradado speed;3;0;Create;True;0;0;False;0;0,-0.5;0,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-2312.594,647.718;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;22;-1932.056,-676.0071;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;12;-1921.594,685.7181;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;13;-1722.21,747.7358;Float;True;Property;_Degradadovertical;Degradado vertical;1;0;Create;True;0;0;False;0;420d83c7d344b374d9dac1893b476af5;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;1;-1983.09,-212.171;Float;False;964.4934;681.9998;Banda Verticales;4;19;18;16;7;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;21;-1613.763,-645.369;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;10eed37a74334d144ba4e059816c0b22;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-1264.127,-611.473;Float;False;BandaSolaVertical;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;14;-1414.082,997.6708;Float;False;Property;_DegradadoColor;Degradado Color;4;1;[HDR];Create;True;0;0;False;0;0,0.5863873,4,0;0,0.5863873,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;15;-1375.873,749.873;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-1933.09,206.0676;Float;True;Property;_BandasVerticales;Bandas Verticales;0;0;Create;True;0;0;False;0;9c8e1aa1caab632468e211716a4bc610;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;30;-940.6443,-382.5575;Float;True;29;BandaSolaVertical;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1108.76,824.0206;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-1594.296,-69.07153;Float;False;Property;_BandaVerticalColor;Banda Vertical Color;2;0;Create;True;0;0;False;0;0,0.2271905,1,0;0,0.2271905,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;32;-937.6443,-180.5575;Float;False;Property;_BandaSolaColor;BandaSolaColor;8;1;[HDR];Create;True;0;0;False;0;0,0.03912401,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;18;-1571.596,216.8293;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-670.5933,313.0432;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1253.598,3.829133;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-646.6443,-245.5575;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-430.4155,90.82922;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;8;-1598.299,498.1875;Float;False;BandaVerticales;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-127,15;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/TronWall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;27;0
WireConnection;22;0;23;0
WireConnection;22;2;25;0
WireConnection;22;1;26;0
WireConnection;12;0;10;0
WireConnection;12;2;9;0
WireConnection;12;1;11;0
WireConnection;13;1;12;0
WireConnection;21;1;22;0
WireConnection;29;0;21;0
WireConnection;15;0;13;0
WireConnection;17;0;15;0
WireConnection;17;1;14;0
WireConnection;18;0;7;0
WireConnection;5;0;18;0
WireConnection;5;1;17;0
WireConnection;19;0;16;0
WireConnection;19;1;18;0
WireConnection;31;0;30;0
WireConnection;31;1;32;0
WireConnection;20;0;19;0
WireConnection;20;1;5;0
WireConnection;20;2;31;0
WireConnection;8;0;7;0
WireConnection;0;2;20;0
ASEEND*/
//CHKSM=46943DB7227A9D7B0A258971E290A9D0DD7A3F35