//
// @author: ZJ
//


#include "filter.h"


#define SAMPLE_RATE 250.0f           // 采样频率
#define CUTOFF_FREQ 30.0f            // 截止频率
#define PI          3.1415926f      // 圆周率PI


float Coef[5];                   //滤波器系数

float x1[2];                    //前两个RED输入值
float y1[2];                    //前两个RED输出值




void CalcButterworthCoef(float *coef, float Q)
{

    float K = tanf(PI * CUTOFF_FREQ / SAMPLE_RATE);

    float K2 = K * K;
    float norm = 1.0f / (1.0f + K / Q + K2);

    coef[0] = K2 * norm;
    coef[1] = 2.0f * coef[0];
    coef[2] = coef[0];
    coef[3] = 2.0f * (K2 - 1.0f) * norm;
    coef[4] = (1.0f - K / Q + K2) * norm;
}


float ButterworthFliter(float input, float *coef, float *x, float *y)
{

    float output = coef[0] * input + coef[1] * x[0] + coef[2] * x[1] - coef[3] * y[0] - coef[4] * y[1];

    x[1] = x[0];
    x[0] = input;
    y[1] = y[0];
    y[0] = output;

    return output;
}





void Filter_Init(void)
{
    CalcButterworthCoef(Coef, 0.707f);
}




