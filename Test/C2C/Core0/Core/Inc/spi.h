#ifndef _SPI_H_
#define _SPI_H_

#include "main.h"

// SCLK
#define C2C_SCLK_Clr() HAL_GPIO_WritePin(GPIOA, GPIO_PIN_1, GPIO_PIN_RESET)
#define C2C_SCLK_Set() HAL_GPIO_WritePin(GPIOA, GPIO_PIN_1, GPIO_PIN_SET)
          
// MOSI 
#define C2C_MOSI_Clr() HAL_GPIO_WritePin(GPIOA, GPIO_PIN_2, GPIO_PIN_RESET)
#define C2C_MOSI_Set() HAL_GPIO_WritePin(GPIOA, GPIO_PIN_2, GPIO_PIN_SET)

// MISO
#define C2C_MISO_Clr()   HAL_GPIO_WritePin(GPIOA, GPIO_PIN_3, GPIO_PIN_RESET)
#define C2C_MISO_Set()   HAL_GPIO_WritePin(GPIOA, GPIO_PIN_3, GPIO_PIN_SET)

// CS 		     
#define C2C_CS_Clr()   HAL_GPIO_WritePin(GPIOA, GPIO_PIN_4, GPIO_PIN_RESET)
#define C2C_CS_Set()   HAL_GPIO_WritePin(GPIOA, GPIO_PIN_4, GPIO_PIN_SET)


void C2C_Write_Byte(uint8_t dat);
void C2C_Init(void);

#endif


