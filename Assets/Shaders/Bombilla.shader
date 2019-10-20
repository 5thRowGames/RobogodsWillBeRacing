// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/BombillaBase"
{
	Properties
	{
		_Paloluz("Palo luz", 2D) = "white" {}
		_Albedobombilla("Albedo bombilla", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,0.5059125,0,0)
		_Intensify("Intensify", Float) = 3
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Background+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Albedobombilla;
		uniform float4 _Albedobombilla_ST;
		uniform float4 _Color0;
		uniform sampler2D _Paloluz;
		uniform float4 _Paloluz_ST;
		uniform float _Intensify;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedobombilla = i.uv_texcoord * _Albedobombilla_ST.xy + _Albedobombilla_ST.zw;
			o.Albedo = tex2D( _Albedobombilla, uv_Albedobombilla ).rgb;
			float2 uv_Paloluz = i.uv_texcoord * _Paloluz_ST.xy + _Paloluz_ST.zw;
			o.Emission = ( ( _Color0 * tex2D( _Paloluz, uv_Paloluz ) ) * _Intensify ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
885;73;1033;927;721.032;275.9613;1;False;False
Node;AmplifyShaderEditor.ColorNode;8;-895.642,-80.10838;Float;False;Property;_Color0;Color 0;3;1;[HDR];Create;True;0;0;False;0;1,0.5059125,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-942.0093,99.5358;Float;True;Property;_Paloluz;Palo luz;1;0;Create;True;0;0;False;0;c38dc9677694552468fb61a4edd4d2bd;c38dc9677694552468fb61a4edd4d2bd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-543.5021,53.02297;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-479.032,300.0387;Float;False;Property;_Intensify;Intensify;4;0;Create;True;0;0;False;0;3;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-481.6086,-168.7643;Float;True;Property;_Albedobombilla;Albedo bombilla;2;0;Create;True;0;0;False;0;3b6660653ad22ee45ad0d2c09e189d48;3b6660653ad22ee45ad0d2c09e189d48;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-318.032,144.0387;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-102,-34;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/BombillaBase;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Background;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;8;0
WireConnection;9;1;6;0
WireConnection;12;0;9;0
WireConnection;12;1;14;0
WireConnection;0;0;7;0
WireConnection;0;2;12;0
ASEEND*/
//CHKSM=F95C30CBD5CB115AF016ABD4D110EF2086AB87D1