// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Splash_Shader"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Main("Main", 2D) = "white" {}
		_EmisionTexture("EmisionTexture", 2D) = "white" {}
		_Opacity("Opacity", Float) = 20
		_Disolve("Disolve", 2D) = "white" {}
		_TextureSpeedUV("Texture Speed UV", Vector) = (0,0,0,0)
		_EmissionValue("EmissionValue", Float) = 0
		_Color("Color", Color) = (0,0,0,0)
		_Desaturate("Desaturate", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _Color;
				uniform sampler2D _EmisionTexture;
				uniform float4 _EmisionTexture_ST;
				uniform sampler2D _Disolve;
				uniform float4 _Disolve_ST;
				uniform float _Desaturate;
				uniform float _EmissionValue;
				uniform sampler2D _Main;
				uniform float4 _Main_ST;
				uniform float _Opacity;
				uniform float2 _TextureSpeedUV;
				float3 HSVToRGB( float3 c )
				{
					float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
					float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
					return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
				}
				
				float3 RGBToHSV(float3 c)
				{
					float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
					float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
					float d = q.x - min( q.w, q.y );
					float e = 1.0e-10;
					return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 uv_EmisionTexture = i.texcoord.xy * _EmisionTexture_ST.xy + _EmisionTexture_ST.zw;
					float3 hsvTorgb52 = RGBToHSV( tex2D( _EmisionTexture, uv_EmisionTexture ).rgb );
					float4 uvs4_Disolve = i.texcoord;
					uvs4_Disolve.xy = i.texcoord.xy * _Disolve_ST.xy + _Disolve_ST.zw;
					float3 hsvTorgb53 = HSVToRGB( float3(( hsvTorgb52.x + uvs4_Disolve.z ),hsvTorgb52.y,hsvTorgb52.z) );
					float3 desaturateInitialColor55 = hsvTorgb53;
					float desaturateDot55 = dot( desaturateInitialColor55, float3( 0.299, 0.587, 0.114 ));
					float3 desaturateVar55 = lerp( desaturateInitialColor55, desaturateDot55.xxx, _Desaturate );
					float4 _Vector0 = float4(-0.3,1,-2,1);
					float3 temp_cast_1 = (_Vector0.x).xxx;
					float3 temp_cast_2 = (_Vector0.y).xxx;
					float3 temp_cast_3 = (_Vector0.z).xxx;
					float3 temp_cast_4 = (_Vector0.w).xxx;
					float3 clampResult46 = clamp( (temp_cast_3 + (desaturateVar55 - temp_cast_1) * (temp_cast_4 - temp_cast_3) / (temp_cast_2 - temp_cast_1)) , float3( 0,0,0 ) , float3( 1,1,1 ) );
					float2 uv_Main = i.texcoord.xy * _Main_ST.xy + _Main_ST.zw;
					float clampResult5 = clamp( ( tex2D( _Main, uv_Main ).b * _Opacity ) , 0.0 , 1.0 );
					float2 appendResult28 = (float2(_TextureSpeedUV.x , _TextureSpeedUV.y));
					float2 panner29 = ( 1.0 * _Time.y * appendResult28 + uvs4_Disolve.xy);
					float2 break32 = panner29;
					float2 appendResult33 = (float2(break32.x , ( uvs4_Disolve.w + break32.y )));
					float T22 = uvs4_Disolve.w;
					float W21 = uvs4_Disolve.z;
					float2 _BoolData = float2(0,1);
					float ifLocalVar18 = 0;
					if( ( tex2D( _Disolve, appendResult33 ).r * T22 ) >= W21 )
					ifLocalVar18 = _BoolData.x;
					else
					ifLocalVar18 = _BoolData.y;
					float4 appendResult6 = (float4(( ( _Color * i.color ) + ( float4( clampResult46 , 0.0 ) * _EmissionValue * i.color ) ).rgb , ( i.color.a * clampResult5 * ifLocalVar18 )));
					

					fixed4 col = appendResult6;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18800
19;306;1358;1017;3476.197;1268.152;2.795423;True;True
Node;AmplifyShaderEditor.Vector2Node;31;-2663.659,466.5092;Inherit;False;Property;_TextureSpeedUV;Texture Speed UV;4;0;Create;True;0;0;0;False;0;False;0,0;-0.8,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-2381.352,333.9547;Inherit;False;0;15;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;43;-2438.436,-1002.455;Inherit;True;Property;_EmisionTexture;EmisionTexture;1;0;Create;True;0;0;0;False;0;False;-1;None;e1fc2976df825407d8644808a60f8918;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;28;-2381.92,525.461;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RGBToHSVNode;52;-2117.345,-895.1669;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PannerNode;29;-1996.154,588.187;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;32;-1672.164,708.5607;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-1857.044,-741.9381;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-1505.447,598.7919;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;53;-1661.677,-872.7759;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;56;-1577.595,-672.7239;Inherit;False;Property;_Desaturate;Desaturate;7;0;Create;True;0;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;55;-1376.314,-781.0608;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;45;-1258.347,-655.9821;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;0;False;0;False;-0.3,1,-2,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;33;-1373.471,710.484;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-1991.882,422.9878;Inherit;False;T;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-903.3759,130.9378;Inherit;False;Property;_Opacity;Opacity;2;0;Create;True;0;0;0;False;0;False;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-1168.046,393.2307;Inherit;True;Property;_Disolve;Disolve;3;0;Create;True;0;0;0;False;0;False;-1;None;02cd7093de51d7a4d9fae92028a1c14b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;44;-880.3522,-759.5669;Inherit;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;1,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-1995.478,330.7389;Inherit;False;W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1040.376,-123.0622;Inherit;True;Property;_Main;Main;0;0;Create;True;0;0;0;False;0;False;-1;None;ae29c741c06cc7b40bb280a2eb53c6e1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;24;-1055.25,642.8412;Inherit;False;22;T;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;49;-465.8304,-813.4208;Inherit;False;Property;_Color;Color;6;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;46;-599.0489,-647.334;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;7;-538.7288,-153.2958;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;39;-791.9062,742.8712;Inherit;False;Constant;_BoolData;Bool Data;4;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-695.3758,40.9378;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-761.9662,514.6829;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-609.2131,-460.3542;Inherit;False;Property;_EmissionValue;EmissionValue;5;0;Create;True;0;0;0;False;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-799.1282,645.9866;Inherit;False;21;W;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;18;-583.379,638.686;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-180.3787,-614.4175;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;5;-507.8922,128.4748;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-172.1,-476.982;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-319.5963,1.78167;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;51;60.3016,-482.2799;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;579.8113,-15.10952;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;14;834.8056,20.58535;Float;False;True;-1;2;ASEMaterialInspector;0;9;Splash_Shader;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;28;0;31;1
WireConnection;28;1;31;2
WireConnection;52;0;43;0
WireConnection;29;0;20;0
WireConnection;29;2;28;0
WireConnection;32;0;29;0
WireConnection;54;0;52;1
WireConnection;54;1;20;3
WireConnection;42;0;20;4
WireConnection;42;1;32;1
WireConnection;53;0;54;0
WireConnection;53;1;52;2
WireConnection;53;2;52;3
WireConnection;55;0;53;0
WireConnection;55;1;56;0
WireConnection;33;0;32;0
WireConnection;33;1;42;0
WireConnection;22;0;20;4
WireConnection;15;1;33;0
WireConnection;44;0;55;0
WireConnection;44;1;45;1
WireConnection;44;2;45;2
WireConnection;44;3;45;3
WireConnection;44;4;45;4
WireConnection;21;0;20;3
WireConnection;46;0;44;0
WireConnection;3;0;2;3
WireConnection;3;1;4;0
WireConnection;17;0;15;1
WireConnection;17;1;24;0
WireConnection;18;0;17;0
WireConnection;18;1;23;0
WireConnection;18;2;39;1
WireConnection;18;3;39;1
WireConnection;18;4;39;2
WireConnection;50;0;49;0
WireConnection;50;1;7;0
WireConnection;5;0;3;0
WireConnection;47;0;46;0
WireConnection;47;1;48;0
WireConnection;47;2;7;0
WireConnection;8;0;7;4
WireConnection;8;1;5;0
WireConnection;8;2;18;0
WireConnection;51;0;50;0
WireConnection;51;1;47;0
WireConnection;6;0;51;0
WireConnection;6;3;8;0
WireConnection;14;0;6;0
ASEEND*/
//CHKSM=B975D69565D112BBDD8F20E0EB6C9FD9B260760D