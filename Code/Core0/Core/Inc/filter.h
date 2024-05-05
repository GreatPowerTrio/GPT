#ifndef _FILTER_H_
#define _FILTER_H_

#include "main.h"


void CalcButterworthCoef(float *coef, float Q);

float ButterworthFliter(float input, float *coef, float *x, float *y);

void Filter_Init(void);



#endif

