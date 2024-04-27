/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * File Name          : freertos.c
  * Description        : Code for freertos applications
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

/* Includes ------------------------------------------------------------------*/
#include "FreeRTOS.h"
#include "task.h"
#include "main.h"
#include "cmsis_os.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */

/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
typedef StaticTask_t osStaticThreadDef_t;
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
#define BUFFER_SIZE 200
#define K           10
#define ABS(x)      (((x) < (0)) ? (-(x)) : (x))
#define MAX(a, b)   (a) > (b) ? (a) : (b)
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
/* USER CODE BEGIN Variables */


extern char u_buf[PRINT_BUF_SIZE];
extern uint8_t rec_data;
extern uint32_t adc_data;


uint16_t S1[BUFFER_SIZE], S2[BUFFER_SIZE], S3[BUFFER_SIZE];
uint8_t H = 100;

extern float Coef[5];                   //滤波器系数

extern float x1[2];                    //前两个RED输入值
extern float y1[2];                    //前两个RED输出值

/* USER CODE END Variables */
/* Definitions for AlgorithmTask */
osThreadId_t AlgorithmTaskHandle;
uint32_t AlgorithmTaskBuffer[ 512 ];
osStaticThreadDef_t AlgorithmTaskControlBlock;
const osThreadAttr_t AlgorithmTask_attributes = {
  .name = "AlgorithmTask",
  .cb_mem = &AlgorithmTaskControlBlock,
  .cb_size = sizeof(AlgorithmTaskControlBlock),
  .stack_mem = &AlgorithmTaskBuffer[0],
  .stack_size = sizeof(AlgorithmTaskBuffer),
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for C2CTask */
osThreadId_t C2CTaskHandle;
const osThreadAttr_t C2CTask_attributes = {
  .name = "C2CTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for LEDDebugTask */
osThreadId_t LEDDebugTaskHandle;
const osThreadAttr_t LEDDebugTask_attributes = {
  .name = "LEDDebugTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for InitWifiTask */
osThreadId_t InitWifiTaskHandle;
const osThreadAttr_t InitWifiTask_attributes = {
  .name = "InitWifiTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityAboveNormal,
};
/* Definitions for C2CMutex */
osMutexId_t C2CMutexHandle;
const osMutexAttr_t C2CMutex_attributes = {
  .name = "C2CMutex"
};

/* Private function prototypes -----------------------------------------------*/
/* USER CODE BEGIN FunctionPrototypes */

/* USER CODE END FunctionPrototypes */

void StartAlgorithmTask(void *argument);
void StartC2CTask(void *argument);
void StartLEDDebugTask(void *argument);
void StartInitWifiTask(void *argument);

void MX_FREERTOS_Init(void); /* (MISRA C 2004 rule 8.1) */

/* Hook prototypes */
void vApplicationStackOverflowHook(xTaskHandle xTask, signed char *pcTaskName);

/* USER CODE BEGIN 4 */
void vApplicationStackOverflowHook(xTaskHandle xTask, signed char *pcTaskName)
{
   /* Run time stack overflow checking is performed if
   configCHECK_FOR_STACK_OVERFLOW is defined to 1 or 2. This hook function is
   called if a stack overflow is detected. */
}
/* USER CODE END 4 */

/**
  * @brief  FreeRTOS initialization
  * @param  None
  * @retval None
  */
void MX_FREERTOS_Init(void) {
  /* USER CODE BEGIN Init */

  /* USER CODE END Init */
  /* Create the mutex(es) */
  /* creation of C2CMutex */
  C2CMutexHandle = osMutexNew(&C2CMutex_attributes);

  /* USER CODE BEGIN RTOS_MUTEX */
  /* add mutexes, ... */
  /* USER CODE END RTOS_MUTEX */

  /* USER CODE BEGIN RTOS_SEMAPHORES */
  /* add semaphores, ... */
  /* USER CODE END RTOS_SEMAPHORES */

  /* USER CODE BEGIN RTOS_TIMERS */
  /* start timers, add new ones, ... */
  /* USER CODE END RTOS_TIMERS */

  /* USER CODE BEGIN RTOS_QUEUES */
  /* add queues, ... */
  /* USER CODE END RTOS_QUEUES */

  /* Create the thread(s) */
  /* creation of AlgorithmTask */
  // AlgorithmTaskHandle = osThreadNew(StartAlgorithmTask, NULL, &AlgorithmTask_attributes);

  /* creation of C2CTask */
  C2CTaskHandle = osThreadNew(StartC2CTask, NULL, &C2CTask_attributes);

  /* creation of LEDDebugTask */
  LEDDebugTaskHandle = osThreadNew(StartLEDDebugTask, NULL, &LEDDebugTask_attributes);

  /* creation of InitWifiTask */
  InitWifiTaskHandle = osThreadNew(StartInitWifiTask, NULL, &InitWifiTask_attributes);

  /* USER CODE BEGIN RTOS_THREADS */
  /* add threads, ... */
  /* USER CODE END RTOS_THREADS */

  /* USER CODE BEGIN RTOS_EVENTS */
  /* add events, ... */
  /* USER CODE END RTOS_EVENTS */

}

/* USER CODE BEGIN Header_StartAlgorithmTask */
/**
  * @brief  Function implementing the AlgorithmTask thread.
  * @param  argument: Not used
  * @retval None
  */
/* USER CODE END Header_StartAlgorithmTask */
void StartAlgorithmTask(void *argument)
{
  /* USER CODE BEGIN StartAlgorithmTask */
  Filter_Init();
  uint8_t i;
  static float filter_result;
  static uint16_t data, rate;
  static uint16_t time, tick, last_tick;
  /* Infinite loop */
  for(;;)
  { 
    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    
    data = adc_data;
    filter_result = ButterworthFliter((float)data, Coef, x1, y1);

    if(S3[K + 1] > S3[K] && S3[K + 1] > S3[K + 2])
    {
      tick = HAL_GetTick();
      time = tick - last_tick;
      last_tick = tick;
    }
    rate = 60 * 1000 / time; 
    // print("%f\r\n", (float)time);
    // print("%f\r\n", (float)S3[K + 2]);
    print("%f\r\n", (float)rate);

    osMutexRelease(C2CMutexHandle);

    for(i = BUFFER_SIZE - 1; i >= 1; i--)
      S1[i] = S1[i - 1]; 
    S1[0] = (uint16_t)filter_result;

    H = 200;
    for(i = K; i < BUFFER_SIZE; i++)
    {
      if(S1[i] > S1[i - K])
        S2[i] = 0;
      else
        S2[i] = ABS(S1[i - K] - S1[i]);

      H = MAX(H, S2[i]);


      if(S2[i] > H / 2)
        S3[i] = S2[i] - H / 2;
      else 
        S3[i] = 0;     
    }
    osDelay(2);
  }
  /* USER CODE END StartAlgorithmTask */
}

/* USER CODE BEGIN Header_StartC2CTask */
/**
* @brief Function implementing the C2CTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartC2CTask */
void StartC2CTask(void *argument)
{
  /* USER CODE BEGIN StartC2CTask */
  /* Infinite loop */
  for(;;)
  {

    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    
    C2C_Write_Uint16(adc_data);

    osMutexRelease(C2CMutexHandle);

    osDelay(4);  
  }
  /* USER CODE END StartC2CTask */
}

/* USER CODE BEGIN Header_StartLEDDebugTask */
/**
* @brief Function implementing the LEDDebugTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartLEDDebugTask */
void StartLEDDebugTask(void *argument)
{
  /* USER CODE BEGIN StartLEDDebugTask */


  /* Infinite loop */
  for(;;)
  {
		HAL_GPIO_TogglePin(LED_GPIO_Port, LED_Pin);

    osDelay(500);
  }
  /* USER CODE END StartLEDDebugTask */
}

/* USER CODE BEGIN Header_StartInitWifiTask */
/**
* @brief Function implementing the InitWifiTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartInitWifiTask */
void StartInitWifiTask(void *argument)
{
  /* USER CODE BEGIN StartInitWifiTask */
  /* Infinite loop */
  for(;;)
  {
    wifi_AP_init();
    vTaskDelete(NULL);
  }
  /* USER CODE END StartInitWifiTask */
}

/* Private application code --------------------------------------------------*/
/* USER CODE BEGIN Application */

/* USER CODE END Application */

