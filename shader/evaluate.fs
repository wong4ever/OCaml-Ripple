#version 120
#extension GL_ARB_draw_buffers : enable

//uniform sampler1D source; // ��Դ���棬�ɺ���PrepareWaveSourceTex�����һά����
//uniform float lambda;
//uniform float sized;
uniform float Lambda1;
uniform float Lambda2;
uniform float source[500];
uniform int n;  // ��Դ�ĸ���
uniform float InvN;
uniform float poolSize;
uniform float time;



const float PI = 3.1415926;
const int SourceSize = 2;


void main()
{
	vec2 cpos = gl_TexCoord[0].xy * poolSize;
	int i;
	float Amplitude = 0.0;
	for (i=0; i<n-1; i++)
	{

		int base = i * 8;
		//vec4 v1 = texture1D(source, (base * InvN)*0.9998+0.0001);
		//vec4 v2 = texture1D(source, ((base + 1) * InvN)*0.9998+0.0001);
		float InitT		= source[base];
		float PosX		= source[base+1];
		float PosY		= source[base+2];
		float A			= source[base+3];
		float Lambda	= source[base+4];
		float Omega		= source[base+5];
		float Velocity	= source[base+6];
		float MaxPhase  = source[base+7];
		float dTime = time - InitT;
		float dist = distance(cpos, vec2(PosX,PosY));
		float x = dTime - dist / Velocity;
		float phase = Omega/(1.0+Lambda2*dTime) * x - PI/2;
		Amplitude += A /(1+Lambda*dist*dist+Lambda1*dTime*dTime) * (cos(phase)) * (x>0?1:0)*(phase<MaxPhase?1:0);       //  ��ĳһ������в�������ĵ���
	}

	gl_FragData[0] = vec4(Amplitude,Amplitude,Amplitude,1.0) ; 	
}