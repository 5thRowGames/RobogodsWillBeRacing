// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Robogods/GlowParticle"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 5
		_ParticleTexture("Particle Texture", 2D) = "white" {}
		_Intensity("Intensity", Float) = 7
		[HDR]_Color0("Color 0", Color) = (1,0.08528899,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform float _Intensity;
		uniform sampler2D _ParticleTexture;
		uniform float4 _ParticleTexture_ST;
		uniform float _Cutoff = 5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ParticleTexture = i.uv_texcoord * _ParticleTexture_ST.xy + _ParticleTexture_ST.zw;
			float4 tex2DNode1 = tex2D( _ParticleTexture, uv_ParticleTexture );
			o.Emission = ( _Color0 * ( _Intensity * tex2DNode1 ) ).rgb;
			o.Alpha = 1;
			clip( tex2DNode1.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
1017;73;901;927;805.96;512.2507;1.3;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-857.5582,31.25598;Float;True;Property;_ParticleTexture;Particle Texture;1;0;Create;True;0;0;False;0;0000000000000000f000000000000000;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-726.5582,-98.74402;Float;False;Property;_Intensity;Intensity;2;0;Create;True;0;0;False;0;7;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-471.5582,4.255981;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-201.7256,-296.0648;Float;False;Property;_Color0;Color 0;3;1;[HDR];Create;True;0;0;False;0;1,0.08528899,0,0;1,0.08528899,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-137.8476,-9.148281;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;169,5;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Robogods/GlowParticle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;5;True;True;0;False;Opaque;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;3;0
WireConnection;2;1;1;0
WireConnection;7;0;6;0
WireConnection;7;1;2;0
WireConnection;0;2;7;0
WireConnection;0;10;1;0
ASEEND*/
//CHKSM=AAADC7F778622111803EF14CEFDE6AC25BF474D6