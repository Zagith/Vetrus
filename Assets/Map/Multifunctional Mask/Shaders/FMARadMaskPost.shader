﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'

// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "MultyFuncMask/Post/RadialMask" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_MaskTex("Mask texture", 2D) = "gray" {}
		_BigMaskTex("Offset mask map", 2D) = "white" {}
		
		_offset("Offset", Float) = 0.05
		_nX("QuialityX", int) = 3
	    _nY("QuialityY", int) = 3

		_iterMin("Iteration Min", Float) = 0
		_iterMax("Iteration Max", Float) = 1
		_min("min", Float) = 0
		_max("max", Float) = 1
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		Pass{
		//Fog{ Mode Off }

		CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _MaskTex;
			uniform sampler2D _BigMaskTex;
			uniform float4   _CameraDepthTexture_ST;
			uniform sampler2D _CameraDepthTexture;
			float4 _Color;

			float _offset;
			int _nX;
			int _nY;
			float _iterMin;
			float _iterMax;
			float _min;
			float _max;

			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert(v2f v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _CameraDepthTexture);

				return o;
			}

			half4 frag(v2f input) : COLOR
			{
				fixed4 c = fixed4(0, 0, 0, 1);

				fixed sum = 0;
				fixed bKoef = tex2D(_BigMaskTex, fixed2(input.texcoord.x, 1 - input.texcoord.y)).r * tex2D(_BigMaskTex, fixed2(input.texcoord.x, 1 - input.texcoord.y)).a;
				fixed2 radial = bKoef * normalize(fixed2(input.texcoord.x - 0.5, input.texcoord.y - 0.5)) * _offset / max(_nX, _nY);
				fixed2 tangent = fixed2(-radial.y, radial.x);
				fixed widthValue = _iterMax - _iterMin;
				fixed halfNX = _nX / 2;
				fixed halfNY = _nY / 2;

				for (int x = 0; x < _nX; x++) 
				{
					fixed minCoordX = (x + 0.5) / _nX;

					for (int y = 0; y < _nY; y++)
					{
						fixed4 value = tex2D(_MaskTex, fixed2(minCoordX, (y + 0.5) / _nY));//_maskArr[x][y];//tex2D(_MaskTex, fixed2(minCoordX, (y + 0.5) / _nY));
						sum += value;
						value = widthValue * value + _iterMin;
						c += value *  tex2D(_MainTex, input.texcoord + radial * (y - halfNY) + tangent * (x - halfNX)) * _Color;
					}
				}
				c /= sum;
				return (_max - _min) * c + _min;
			}
		ENDCG
		}
	}
	FallBack "Off"
}