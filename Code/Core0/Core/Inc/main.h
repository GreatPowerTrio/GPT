/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.h
  * @brief          : Header for main.c file.
  *                   This file contains the common defines of the application.
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2024 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __MAIN_H
#define __MAIN_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ------------------------------------------------------------------*/
#include "stm32f1xx_hal.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */


/* USER CODE END Includes */

/* Exported types ------------------------------------------------------------*/
/* USER CODE BEGIN ET */

/* USER CODE END ET */

/* Exported constants --------------------------------------------------------*/
/* USER CODE BEGIN EC */

/* USER CODE END EC */

/* Exported macro ------------------------------------------------------------*/
/* USER CODE BEGIN EM */

/* USER CODE END EM */

/* Exported functions prototypes ---------------------------------------------*/
void Error_Handler(void);

/* USER CODE BEGIN EFP */

/* USER CODE END EFP */

/* Private defines -----------------------------------------------------------*/
#define C2C_CS_Pin GPIO_PIN_6
#define C2C_CS_GPIO_Port GPIOC
#define C2C_MOSI_Pin GPIO_PIN_7
#define C2C_MOSI_GPIO_Port GPIOC
#define C2C_MISO_Pin GPIO_PIN_8
#define C2C_MISO_GPIO_Port GPIOC
#define C2C_SCLK_Pin GPIO_PIN_9
#define C2C_SCLK_GPIO_Port GPIOC
#define LED_Pin GPIO_PIN_8
#define LED_GPIO_Port GPIOA
#define ECG_LEAD_OFF_Pin GPIO_PIN_3
#define ECG_LEAD_OFF_GPIO_Port GPIOB

/* USER CODE BEGIN Private defines */
#include "stdio.h"
#include "usart.h"
#include "spi.h"
#include "wifi.h"
#define print(...)            HAL_UART_Transmit(&huart1,      (uint8_t *)u_buf, sprintf((char *)u_buf, __VA_ARGS__), 0xFFFF)
#define print_wifi(...)       HAL_UART_Transmit(&huart2,      (uint8_t *)u_buf, sprintf((char *)u_buf, __VA_ARGS__), 0xFFFF)

/* USER CODE END Private defines */

#ifdef __cplusplus
}
#endif

#endif /* __MAIN_H */
