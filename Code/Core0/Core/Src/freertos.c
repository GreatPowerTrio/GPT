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
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */

/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
/* USER CODE BEGIN Variables */

extern char u_buf[100];
char rx_buf[100];
extern uint32_t adc_data;

/* USER CODE END Variables */
/* Definitions for myTask01 */
osThreadId_t myTask01Handle;
const osThreadAttr_t myTask01_attributes = {
  .name = "myTask01",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for myTask02 */
osThreadId_t myTask02Handle;
const osThreadAttr_t myTask02_attributes = {
  .name = "myTask02",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityAboveNormal,
};
/* Definitions for C2CTask */
osThreadId_t C2CTaskHandle;
const osThreadAttr_t C2CTask_attributes = {
  .name = "C2CTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityLow,
};
/* Definitions for C2CMutex */
osMutexId_t C2CMutexHandle;
const osMutexAttr_t C2CMutex_attributes = {
  .name = "C2CMutex"
};

/* Private function prototypes -----------------------------------------------*/
/* USER CODE BEGIN FunctionPrototypes */

/* USER CODE END FunctionPrototypes */

void StartTask01(void *argument);
void StartTask02(void *argument);
void StartC2CTask(void *argument);

void MX_FREERTOS_Init(void); /* (MISRA C 2004 rule 8.1) */

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
  /* creation of myTask01 */
  myTask01Handle = osThreadNew(StartTask01, NULL, &myTask01_attributes);

  /* creation of myTask02 */
  myTask02Handle = osThreadNew(StartTask02, NULL, &myTask02_attributes);

  /* creation of C2CTask */
  // C2CTaskHandle = osThreadNew(StartC2CTask, NULL, &C2CTask_attributes);

  /* USER CODE BEGIN RTOS_THREADS */
  /* add threads, ... */
  /* USER CODE END RTOS_THREADS */

  /* USER CODE BEGIN RTOS_EVENTS */
  /* add events, ... */
  /* USER CODE END RTOS_EVENTS */

}

/* USER CODE BEGIN Header_StartTask01 */
/**
  * @brief  Function implementing the myTask01 thread.
  * @param  argument: Not used
  * @retval None
  */
/* USER CODE END Header_StartTask01 */
void StartTask01(void *argument)
{
  /* USER CODE BEGIN StartTask01 */
  /* Infinite loop */
  for(;;)
  {
		HAL_GPIO_TogglePin(LED_GPIO_Port, LED_Pin);
    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    wifi_client_send((uint16_t*)"hello", 5);
    osMutexRelease(C2CMutexHandle);   
    osDelay(100);
  }                                       
  /* USER CODE END StartTask01 */
}

/* USER CODE BEGIN Header_StartTask02 */
/**
* @brief Function implementing the myTask02 thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartTask02 */
void StartTask02(void *argument)
{
  /* USER CODE BEGIN StartTask02 */
  /* Infinite loop */
  for(;;)
  {
    //wifi_server_init();
    //wifi_SCAN();
    // wifi_connect("GPTT", "12345678999");
    // wifi_AP_init();
   wifi_client_init();
  // print_wifi("AT+RST\r\n");
  // delay_ms(100);
  // print_wifi("AT+CWMODE=3\r\n");
  // delay_ms(100);
  // print_wifi("AT+RST\r\n");
  // delay_ms(100);
  // print_wifi("AT+UART=115200,8,1,0,0\r\n");
  // delay_ms(100);
  //  print_wifi("AT+CWSAP=\"esp8266\",\"123456789999\",1,3\r\n");
  // delay_ms(100);
  //  print_wifi("AT+CWJAP=\"GPTTT\",\"12345678999\"\r\n");
  //  delay_ms(100);
  // print_wifi("AT+CIPMUX=0\r\n");
  // delay_ms(100);
  // print_wifi("AT+CIPSERVER=1,8080\r\n");
  // delay_ms(100);
  // // print_wifi("AT+CIPSTART=\"TCP\",\"172.27.16.1\",8080\r\n");
  // delay_ms(100);
  // print_wifi("AT+CIPSTO=500\r\n");
  // delay_ms(100);      
  // print_wifi("AT+CIFSR\r\n");

    

  //print_wifi("AT+UART=115200,8,1,0,0\r\n");
  //print_wifi("AT+CWSAP=\"ESP8266\",\"12345678\",11,0\r\n");
    vTaskDelete(NULL);
  }
  /* USER CODE END StartTask02 */
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

    HAL_UART_Receive(&huart1, (uint8_t *)rx_buf, 2, 100);
    print("%s\r\n", rx_buf);
    osMutexRelease(C2CMutexHandle);

    osDelay(10);  
    
  }
  /* USER CODE END StartC2CTask */
}

/* Private application code --------------------------------------------------*/
/* USER CODE BEGIN Application */

/* USER CODE END Application */

