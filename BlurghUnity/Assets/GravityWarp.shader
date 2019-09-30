Shader "Unlit/GravityWarp"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags {
			"RenderType" = "Opaque"
			//"DisableBatching"="True"
			}


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 sphereMask :TEXCOORD1;
				float3 posWorld : TEXCOORD2;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float3 _GravityPoint;
			float _GravityMulitplier; float _GravityDistance;

			v2f vert(appdata v)
			{
				v2f o;
				//float3 localGravityPoint = mul(unity_ObjectToWorld, _GravityPoint).rgb;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
								
				
				//float3 mult = (1, 1, 1);

				// TODO clamp to center point
				//if (dir.y < 0)
				//{
				//float3 dir = normalize(o.posWorld - _GravityPoint);
				
				if (_GravityPoint.y > o.posWorld.y)
				{
					_GravityPoint.y = o.posWorld.y - (_GravityDistance / 2) + (o.posWorld.y - _GravityPoint.y);
					float3 dir = normalize(o.posWorld - _GravityPoint);
					float dist = distance(o.posWorld, _GravityPoint);
					dist = saturate(1 - dist /_GravityDistance);
					
					v.vertex.x += dir.x * _GravityMulitplier * dist;
					v.vertex.y += -abs(dir.y) / 2 * _GravityMulitplier * dist;
					v.vertex.z += dir.z * _GravityMulitplier * dist;

					//o.sphereMask = dist;

					o.vertex = UnityObjectToClipPos(v.vertex);
					//o.uv = v.uv;
					//o.normal = v.normal;	
					return o;
				}
				
				
				//v.vertex.z += dir.z * _GravityMulitplier * dist;
				else {
					_GravityPoint.y = o.posWorld.y - (_GravityDistance / 2);
					float3 dir = normalize(o.posWorld - _GravityPoint);
					float dist = distance(o.posWorld, _GravityPoint);
					//dist = saturate(1 - (pow(dist, 2) / pow(_GravityDistance, 2)));
					dist = saturate(1 - dist / _GravityDistance);
					
					v.vertex.x += dir.x * 1 * dist;
					v.vertex.y += -abs(dir.y) / 2 * _GravityMulitplier * dist;
					v.vertex.z += dir.z * 1 * dist;

					o.sphereMask = dist;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.normal = v.normal;
					return o;
				}


				//v.vertex.rgb += dir * _GravityMulitplier * dist ;
				//}
				//else 
				//{
				//	v.vertex.rgb += dir * mult * (dist - _GravityDistance) * _GravityMulitplier;
				//}
				
				
				
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
			
				//c = lerp(c, tex2D(_MainTex, i.posWorld.xy), i.normal.z * i.normal.z);
				//c = lerp(c, tex2D(_MainTex, i.posWorld.yz), i.normal.x * i.normal.x);
				//c = lerp(c, tex2D(_MainTex, i.posWorld.xz), i.normal.y * i.normal.y);

				//c.rgb -= 0.4;
					

				

				//c.rgb = saturate(c.rgb - i.sphereMask * (1 - _GravityMulitplier) * 0.333);
				return c;
			}
			ENDCG
		}
	}
}