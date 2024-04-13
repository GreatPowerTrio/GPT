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
#include "lcd.h"
#include "lcd_init.h"
#include "stdbool.h"
#include "cmsis_os2.h"

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
#define LED_Pin GPIO_PIN_13
#define LED_GPIO_Port GPIOC
#define BLK_Pin GPIO_PIN_4
#define BLK_GPIO_Port GPIOA
#define CS_Pin GPIO_PIN_5
#define CS_GPIO_Port GPIOA
#define DC_Pin GPIO_PIN_6
#define DC_GPIO_Port GPIOA
#define RST_Pin GPIO_PIN_7
#define RST_GPIO_Port GPIOA
#define SDA_Pin GPIO_PIN_4
#define SDA_GPIO_Port GPIOC
#define SCL_Pin GPIO_PIN_5
#define SCL_GPIO_Port GPIOC
#define C2C_SCLK_Pin GPIO_PIN_6
#define C2C_SCLK_GPIO_Port GPIOC
#define C2C_SCLK_EXTI_IRQn EXTI9_5_IRQn
#define C2C_MISO_Pin GPIO_PIN_7
#define C2C_MISO_GPIO_Port GPIOC
#define C2C_MOSI_Pin GPIO_PIN_8
#define C2C_MOSI_GPIO_Port GPIOC
#define C2C_CS_Pin GPIO_PIN_9
#define C2C_CS_GPIO_Port GPIOC
#define C2C_CS_EXTI_IRQn EXTI9_5_IRQn

/* USER CODE BEGIN Private defines */

/* USER CODE END Private defines */

#ifdef __cplusplus
}
#endif

#endif /* __MAIN_H */
