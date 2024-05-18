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

#define BUFFER_SIZE     100
#define K               10


#define LEAD_ADC_MAX    2400
#define LEAD_ADC_MIN    1800
#define LEAD_MAX_CNT    100


/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
/* USER CODE BEGIN Variables */


/* �ⲿ���� */
extern char     print_buf[PRINT_BUF_SIZE];
extern uint8_t  rec_data;
extern uint32_t adc_data;

extern float    Coef[5];                   //�˲���ϵ��
extern float    x1[2];                    //ǰ����adc����ֵ
extern float    y1[2];                    //ǰ����adc���ֵ


/* �ĵ��㷨���� */
uint16_t ecg_rate;
float    filter_result;
// ��ֵ��־λ�����ڼ�⵽��ֵʱ��֤���ݴ������ȷ��
bool     peak_flag;

/* ����״̬ */
bool lead_state = false;

/* Ԥ����ֵ */
uint16_t ecg_warning_level_up = 150 - 10, ecg_warning_level_down = 30 + 10;

/* ����wifi�������� */
bool wifi_flag = false;


/* USER CODE END Variables */
/* Definitions for AlgorithmTask */
osThreadId_t AlgorithmTaskHandle;
uint32_t AlgorithmTaskBuffer[ 1024 ];
osStaticThreadDef_t AlgorithmTaskControlBlock;
const osThreadAttr_t AlgorithmTask_attributes = {
  .name = "AlgorithmTask",
  .cb_mem = &AlgorithmTaskControlBlock,
  .cb_size = sizeof(AlgorithmTaskControlBlock),
  .stack_mem = &AlgorithmTaskBuffer[0],
  .stack_size = sizeof(AlgorithmTaskBuffer),
  .priority = (osPriority_t) osPriorityAboveNormal,
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
  .priority = (osPriority_t) osPriorityHigh,
};
/* Definitions for C2CMutex */
osMutexId_t C2CMutexHandle;
const osMutexAttr_t C2CMutex_attributes = {
  .name = "C2CMutex"
};

/* Private function prototypes -----------------------------------------------*/
/* USER CODE BEGIN FunctionPrototypes */

void CheckLeadState(uint16_t data);
uint16_t EncodeData(uint16_t data);
void WarningLevel(uint16_t level_up, uint16_t level_down);
void WifiAccept(void);

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
  AlgorithmTaskHandle = osThreadNew(StartAlgorithmTask, NULL, &AlgorithmTask_attributes);

  /* creation of C2CTask */
  C2CTaskHandle = osThreadNew(StartC2CTask, NULL, &C2CTask_attributes);

  /* creation of LEDDebugTask */
  // LEDDebugTaskHandle = osThreadNew(StartLEDDebugTask, NULL, &LEDDebugTask_attributes);

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

  // ��ʼ���˲���
  Filter_Init();

  // ��ֵ�����ʱ�䡢��ǰ��ֵʱ�̡���һ��ֵʱ�̡�ADC����ֵ
  static uint16_t time, tick, last_tick, data;

  // �������ݡ���ֵ����
  uint16_t S1[BUFFER_SIZE], S2[BUFFER_SIZE], S3[BUFFER_SIZE], H;

  /* Infinite loop */
  for(;;)
  { 
    // �������˲�
    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    {
      data = adc_data;
      filter_result = ButterworthFliter((float)data, Coef, x1, y1);
    }
    osMutexRelease(C2CMutexHandle);
    

    // ����״̬���
    if(!wifi_flag)
    {
      osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
      {
        CheckLeadState(filter_result);
      }
      osMutexRelease(C2CMutexHandle);
    }

    // �㷨���Ĳ���
    for(uint8_t i = BUFFER_SIZE - 1; i >= 1; i--)
      S1[i] = S1[i - 1]; 
    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    {
      S1[0] = (uint16_t)filter_result;
    }
    osMutexRelease(C2CMutexHandle);
    H = 300;
    for(uint8_t i = K; i < BUFFER_SIZE; i++)
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

    // ��ֵ��⣺��⵽��ֵ�󣬼���������ֵ֮���ʱ����������������
    if(S3[K + 1] > S3[K] && S3[K + 1] > S3[K + 2])
    {
      tick = HAL_GetTick();
      time = tick - last_tick;
      last_tick = tick;
      osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
      {
        ecg_rate = 60 * 1000 / time; 
        peak_flag = true;
      }
      osMutexRelease(C2CMutexHandle);

      // ���ޱ���
      if(!wifi_flag)
        WarningLevel(ecg_warning_level_up, ecg_warning_level_down);
    }
    else 
      peak_flag = false;


    if(!wifi_flag)
    {
      osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
      {
        print("%d\r\n", ecg_rate);
      }
      osMutexRelease(C2CMutexHandle);
    }


    // ����ƽ����������Core1�������ź�
    WifiAccept();

    vTaskDelay(3);
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
  static uint8_t cnt15;
  /* Infinite loop */
  for(;;)
  {
    // ��û�м�⵽��ֵʱ��ÿ15�η���һ����������
    if(++cnt15 == 15 && !peak_flag)
    {
      osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
      {
        // ������������
        C2C_Write_Uint16(EncodeData(ecg_rate));
      }
      osMutexRelease(C2CMutexHandle);
    }
    else
    {
      osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
      {
        // ���Ͳ�������
        C2C_Write_Uint16(EncodeData((uint16_t)filter_result));
      }
      osMutexRelease(C2CMutexHandle);
    }
    cnt15 %= 15;

    vTaskDelay(2);
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
    osMutexAcquire(C2CMutexHandle, portMAX_DELAY);
    {
      if(ecg_rate < 350 && ecg_rate > 20 && lead_state)
        wifi_send_data(ecg_rate);
      else
        wifi_send_data(0);
    }
    osMutexRelease(C2CMutexHandle);

    osDelay(1000);
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
    // �ȴ�esp8266�ϵ���Ϣ������ɣ�������ڴ����жϿ�ס
    wifi_delay(500);

    // �������ڵ���ESP8266���ж�
    HAL_UART_Receive_IT(&huart1, &rec_data, 1);
    HAL_UART_Receive_IT(&huart2, &rec_data, 1); 

    wifi_set_AP();
    vTaskDelete(NULL);
  }
  /* USER CODE END StartInitWifiTask */
}

/* Private application code --------------------------------------------------*/
/* USER CODE BEGIN Application */


// �������״̬��⣺������ֵ��һ����Χ�ڣ�������һ��ʱ�䣬�ж�Ϊ����
void CheckLeadState(uint16_t data)
{
  // �жϵ���״̬������
  static uint16_t lead_cnt = 0;
  if(LEAD_ADC_MIN < data && data < LEAD_ADC_MAX)
  {
    if(lead_cnt == LEAD_MAX_CNT)
    {
        lead_state = true;
        // HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_SET);
    }
    else
      lead_cnt++;
  }
  else
  {
    lead_state = false;
    lead_cnt = 0;
    // HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_RESET);
  }
}

// ���ݱ��룺��Ҫ���͵�Core1�������뵼��״̬��ϣ��������ݵ����λ
uint16_t EncodeData(uint16_t data)
{
  return data | (lead_state << (16 - 1));
}


// ���ޱ���
void WarningLevel(uint16_t level_up, uint16_t level_down)
{
  static uint8_t cnt5 = 0;
  
  if((ecg_rate > level_up || ecg_rate < level_down) && lead_state)
  {
    // �������ȶ���
    if(cnt5 == 1)
    {
      // ����PWM�������÷�����Ƶ��
      if(TIM2->CCR4 == 400)
      {
        TIM2->CCR4 = 600;
      }
      else
      {
        TIM2->CCR4 = 400;
      }
    }
    else
      cnt5++;
  }
  else
  {
    cnt5 = 0;
    TIM2->CCR4 = 0;
  }   
}

// ��������Core1�������ź�
void WifiAccept(void)
{
  if(!wifi_flag && HAL_GPIO_ReadPin(C2C_WIFI_GPIO_Port, C2C_WIFI_Pin) == GPIO_PIN_SET)
  {
    wifi_flag = true;
    HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_SET);
    LEDDebugTaskHandle = osThreadNew(StartLEDDebugTask, NULL, &LEDDebugTask_attributes);
    // �رշ�����
    TIM2->CCR4 = 0;
    if(C2CTaskHandle)
    {
      vTaskDelete(C2CTaskHandle);
      C2CTaskHandle = NULL;
    }  
  }
  else if(wifi_flag && HAL_GPIO_ReadPin(C2C_WIFI_GPIO_Port, C2C_WIFI_Pin) == GPIO_PIN_RESET)
  {
    wifi_flag = false;
    HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_RESET);
    C2CTaskHandle = osThreadNew(StartC2CTask, NULL, &C2CTask_attributes);

    if(LEDDebugTaskHandle)
    {
      vTaskDelete(LEDDebugTaskHandle);
      LEDDebugTaskHandle = NULL;
    }

    //���ͽ�����Ϣ
    print_wifi("AT+CIPSEND=0,10\r\n");
    print_wifi("DISCONNECT\r\n");
  }
}



/* USER CODE END Application */

