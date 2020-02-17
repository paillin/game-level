Shader "Custom/WaterFlow"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Tint("Tint Color", Color) = (1,1,1,1)
		_Frequency("Noise Frequency", float) = 6
		_Forward("Flow Direction", Vector) = (0,1,0,0)
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200
			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows alpha:blend
			#pragma target 3.0
			#include "UnityCG.cginc"
			#define PERMS_COUNT 32
			#define MAX_PERMS_IDX ((PERMS_COUNT/2)-1)

			struct Input
			{
				float2 uv_MainTex;
			};

			sampler2D _MainTex;
			float4 _Tint;
			float _Frequency;
			float4 _Forward;


			static const int perms[PERMS_COUNT] = {
				9,12,5,12,7,9,1,4,11,4,2,15,15,3,7,14,
				9,12,5,12,7,9,1,4,11,4,2,15,15,3,7,14
			};

			static const float2 stepVec = float2(1.0f, 0.0f);

			float fade(float f)
			{
				return f * f * f * (f * (f * 6.0f - 15.0f) + 10.0f);
			}

			float hash(int h, float2 p)
			{
				switch (h & 3)
				{
				case 0:
					return p.x + p.y;
				case 1:
					return -p.x + p.y;
				case 2:
					return p.x - p.y;
				case 3:
					return -p.x - p.y;
				}
				return 0.0f;
			}

			void hash42Mit(float2 p, out int hashes[4])
			{
				float4 p4 = frac(float4(p.xyxy) * float4(0.1031f, 0.1030f, 0.0973f, 0.1099f));
				p4 += dot(p4, p4.wzxy + 33.33f);
				p4 = frac((p4.xxyz + p4.yzzw) * p4.zywx);
				hashes[0] = (int)p4.x;
				hashes[1] = (int)p4.y;
				hashes[2] = (int)p4.z;
				hashes[3] = (int)p4.w;
			}

			void getHash(float2 p, out int hashes[4])
			{
				int fpx = int(floor(p).x) & MAX_PERMS_IDX;
				int fpy = int(floor(p).y) & MAX_PERMS_IDX;
				hash42Mit(p, hashes);
				hashes[0] += perms[(perms[fpx] + fpy)];
				hashes[1] += perms[(perms[fpx + 1] + fpy)];
				hashes[2] += perms[(perms[fpx] + 1 + fpy)];
				hashes[3] += perms[(perms[fpx + 1] + 1 + fpy)];
			}

			float noise(float2 p)
			{
				int hashes[4];
				getHash(p,hashes);
				float2 i = frac(p);
				float4 grads = float4(hash(hashes[0], i),
					hash(hashes[1], i - stepVec.xy),
					hash(hashes[2], i - stepVec.yx),
					hash(hashes[3], i - stepVec.xx));
				return 0.5f * (0.5f + lerp(
					lerp(grads.x, grads.y, fade(i.x)),
					lerp(grads.z, grads.w, fade(i.x)),
					fade(i.y)));
			}

			float calcHeight(float2 uv)
			{
				float4 forward = _Time.x * normalize(_Forward).xyxy;
				forward.zw *= 2;
				return (noise(_Frequency * (uv + forward.xy)) +
					noise(_Frequency * (uv + stepVec.xy + forward.xy)) +
					noise(_Frequency * (uv + stepVec.yx + forward.xy))) / 3.0f;
			}

			float3 calcNormal(float3 oldNorm, float2 uv, float height)
			{
				float2 newNormXZ = height - float2(
						calcHeight(uv - stepVec.xy),
						calcHeight(uv - stepVec.yx)
					);
				return normalize(float3(
					oldNorm.x + 0.5f * newNormXZ.x,
					oldNorm.y,
					oldNorm.z + 0.5f * newNormXZ.y
				));
			}

			void surf(Input i, inout SurfaceOutputStandard o)
			{
				float height = calcHeight(i.uv_MainTex);
				o.Normal = calcNormal(o.Normal, i.uv_MainTex, height);
				o.Albedo = 0.75*_Tint +
					0.25*height * tex2D(_MainTex, i.uv_MainTex + 0.2 * height).rgb;
				o.Alpha = 0.8f;
			}
			ENDCG
		}
		FallBack "Diffuse"
}
