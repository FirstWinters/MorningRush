Shader "Cloudz"
{
	Properties 
	{
		_Color ("Ambient Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_Brightness ("Brightness", float) = 1
	}	

	SubShader 
	{
		Pass	
		{
			GLSLPROGRAM

			uniform vec4 _Time;
			uniform vec4 _Color;
			uniform sampler2D _MainTex;
			uniform vec4 _MainTex_ST;
			uniform vec4 _Brightness;

			varying vec4 texCoords;
			varying vec4 vertexColor;
			varying vec4 vertexData;
			
			varying vec3 vertexPosition;
			varying vec2 uv;
			
			#ifdef VERTEX
			void main()
			{
				vertexColor = _Color;
				
				texCoords = gl_MultiTexCoord0;
				texCoords += fract(_Time.x);
				
				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			}
			#endif

			#ifdef FRAGMENT
			void main()
			{

				gl_FragColor = texture2D(_MainTex, _MainTex_ST.xy * texCoords.xy + _MainTex_ST.zw) * vertexColor;
				
			}
			#endif

			ENDGLSL
		}
	}
}