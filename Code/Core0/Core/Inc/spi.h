#ifndef _SPI_H_
#define _SPI_H_

#include "main.h"

// SCLK
#define C2C_SCLK_Clr()    HAL_GPIO_WritePin(C2C_SCLK_GPIO_Port, C2C_SCLK_Pin, GPIO_PIN_RESET)
#define C2C_SCLK_Set()    HAL_GPIO_WritePin(C2C_SCLK_GPIO_Port, C2C_SCLK_Pin, GPIO_PIN_SET)
          
// MOSI 
#define C2C_MOSI_Clr()    HAL_GPIO_WritePin(C2C_MOSI_GPIO_Port, C2C_MOSI_Pin, GPIO_PIN_RESET)
#define C2C_MOSI_Set()    HAL_GPIO_WritePin(C2C_MOSI_GPIO_Port, C2C_MOSI_Pin, GPIO_PIN_SET)


// CS 		     
#define C2C_CS_Clr()      HAL_GPIO_WritePin(C2C_CS_GPIO_Port, C2C_CS_Pin, GPIO_PIN_RESET)
#define C2C_CS_Set()      HAL_GPIO_WritePin(C2C_CS_GPIO_Port, C2C_CS_Pin, GPIO_PIN_SET)


void C2C_Write_Uint16(uint16_t dat);

#endif
