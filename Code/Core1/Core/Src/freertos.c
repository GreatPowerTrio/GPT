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
#include "queue.h"
#include "pic.h"

/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
typedef StaticTask_t osStaticThreadDef_t;
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

/* �ⲿ���� */
extern uint16_t rec_data;

extern bool key1_flag;
extern bool key2_flag;
extern bool key3_flag;


// �����Ƿ���ס����������ʱʹ������Ч
bool key_locked = false;



static uint32_t color = BLUE;

/* USER CODE END Variables */
/* Definitions for DebugTask */
osThreadId_t DebugTaskHandle;
const osThreadAttr_t DebugTask_attributes = {
  .name = "DebugTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for InterfaceTask */
osThreadId_t InterfaceTaskHandle;
const osThreadAttr_t InterfaceTask_attributes = {
  .name = "InterfaceTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for MenuTask */
osThreadId_t MenuTaskHandle;
const osThreadAttr_t MenuTask_attributes = {
  .name = "MenuTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for ECGTask */
osThreadId_t ECGTaskHandle;
uint32_t ECGTaskBuffer[ 128 ];
osStaticThreadDef_t ECGTaskControlBlock;
const osThreadAttr_t ECGTask_attributes = {
  .name = "ECGTask",
  .cb_mem = &ECGTaskControlBlock,
  .cb_size = sizeof(ECGTaskControlBlock),
  .stack_mem = &ECGTaskBuffer[0],
  .stack_size = sizeof(ECGTaskBuffer),
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for WifiTask */
osThreadId_t WifiTaskHandle;
uint32_t WifiTaskBuffer[ 128 ];
osStaticThreadDef_t WifiTaskControlBlock;
const osThreadAttr_t WifiTask_attributes = {
  .name = "WifiTask",
  .cb_mem = &WifiTaskControlBlock,
  .cb_size = sizeof(WifiTaskControlBlock),
  .stack_mem = &WifiTaskBuffer[0],
  .stack_size = sizeof(WifiTaskBuffer),
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for SettingTask */
osThreadId_t SettingTaskHandle;
uint32_t SettingTaskBuffer[ 128 ];
osStaticThreadDef_t SettingTaskControlBlock;
const osThreadAttr_t SettingTask_attributes = {
  .name = "SettingTask",
  .cb_mem = &SettingTaskControlBlock,
  .cb_size = sizeof(SettingTaskControlBlock),
  .stack_mem = &SettingTaskBuffer[0],
  .stack_size = sizeof(SettingTaskBuffer),
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for LCDMutex */
osMutexId_t LCDMutexHandle;
const osMutexAttr_t LCDMutex_attributes = {
  .name = "LCDMutex"
};

/* Private function prototypes -----------------------------------------------*/
/* USER CODE BEGIN FunctionPrototypes */

uint32_t EasingFunction(uint32_t t);
void Decode(uint16_t rec, uint16_t *data, uint16_t *state);

/* USER CODE END FunctionPrototypes */

void StartDebugTask(void *argument);
void StartInterfaceTask(void *argument);
void StartMenuTask(void *argument);
void StartECGTask(void *argument);
void StartWifiTask(void *argument);
void StartSettingTask(void *argument);

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
  /* creation of LCDMutex */
  LCDMutexHandle = osMutexNew(&LCDMutex_attributes);

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
  /* creation of DebugTask */
  // DebugTaskHandle = osThreadNew(StartDebugTask, NULL, &DebugTask_attributes);

  /* creation of InterfaceTask */
  InterfaceTaskHandle = osThreadNew(StartInterfaceTask, NULL, &InterfaceTask_attributes);

  /* creation of MenuTask */
  // MenuTaskHandle = osThreadNew(StartMenuTask, NULL, &MenuTask_attributes);

  /* creation of ECGTask */
  // ECGTaskHandle = osThreadNew(StartECGTask, NULL, &ECGTask_attributes);

  /* creation of WifiTask */
  // WifiTaskHandle = osThreadNew(StartWifiTask, NULL, &WifiTask_attributes);

  /* creation of SettingTask */
  // SettingTaskHandle = osThreadNew(StartSettingTask, NULL, &SettingTask_attributes);

  /* USER CODE BEGIN RTOS_THREADS */
  /* add threads, ... */
  /* USER CODE END RTOS_THREADS */

  /* USER CODE BEGIN RTOS_EVENTS */
  /* add events, ... */
  /* USER CODE END RTOS_EVENTS */

}

/* USER CODE BEGIN Header_StartDebugTask */
/**
  * @brief  ��������һ�㲻����
  * @param  argument: Not used
  * @retval None
  */
/* USER CODE END Header_StartDebugTask */
void StartDebugTask(void *argument)
{
  /* USER CODE BEGIN StartDebugTask */
  /* Infinite loop */
  for(;;)
  {
    osDelay(1);
  }
  /* USER CODE END StartDebugTask */
}

/* USER CODE BEGIN Header_StartInterfaceTask */
/**
* @brief �������Ŀ�������
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartInterfaceTask */
void StartInterfaceTask(void *argument)
{
  /* USER CODE BEGIN StartInterfaceTask */
  
  // ����������׼���꣬-20 �ǿɵ��ڵ�yƫ����
  static uint8_t X, Y;
  X = LCD_W / 2 - GTP_W / 2;
  Y = LCD_H / 2 - GTP_H / 2 - 20;

  // ���ߵĺ��
  static uint8_t thick = 3;

  // ����ֻ����һ�εı�־λ
  static bool once_flag = false;

  // �������ڲ��ţ���ס����
  key_locked = true;

  /* Infinite loop */
  for(;;)
  {
    // ���м�����
    if(key2_flag)
    {
      // ��־λ���㡢����������˵�������ɱ
      key2_flag = key1_flag = key3_flag = false;
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);
      MenuTaskHandle = osThreadNew(StartMenuTask, NULL, &MenuTask_attributes);
      vTaskDelete(NULL);
    }

    if(!once_flag)
    {
      // ��һ�ν���

      // ����һ�׶Σ���ˮƽ����
      for(uint8_t i = 0; i < GTP_W / 2; i++)
      {
        LCD_Fill(X + GTP_W / 2 - i, Y + GTP_H, X + GTP_W / 2 + i, Y + GTP_H + thick, YELLOW);
        osDelay(i);
      }
      // �������׶Σ�����GTPͼ��
      for(uint8_t i = 0; i < GTP_H; i++)
        LCD_ShowPicture(X, Y + GTP_H - i, GTP_W, i, img_GPT);

      // �������׶Σ���ʾ�ײ���ʾͼ��
      LCD_ShowPicture(LCD_W - BOTTOM_W, LCD_H - BOTTOM_H, BOTTOM_W, BOTTOM_H, img_bottom);
      
      once_flag     = true;
      // ��������
      key_locked    = false;
    }
    else
    {
      // �ڶ��ν���ʱ���Ქ�Ŷ�����ֱ����ʾͼ��
      LCD_Fill(X, Y + GTP_H, X + GTP_W, Y + GTP_H + thick, YELLOW);
      LCD_ShowPicture(X, Y, GTP_W, GTP_H, img_GPT);
      LCD_ShowPicture(LCD_W - BOTTOM_W, LCD_H - BOTTOM_H, BOTTOM_W, BOTTOM_H, img_bottom);
      // ��������
      key_locked   = false;
    }

    osDelay(1);
  }

  /* USER CODE END StartInterfaceTask */
}

/* USER CODE BEGIN Header_StartMenuTask */
/**
* @brief �������Ĳ˵�����
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartMenuTask */
void StartMenuTask(void *argument)
{
  /* USER CODE BEGIN StartMenuTask */

  // �����������Ա���
  static uint8_t tick = 0;
  // ������������һ�ν�������ν��
  static uint32_t last_move, move;

  // ����һ�׶κͶ������׶�
  static bool is_first_stage, is_second_stage; 

  // ��׼���꣺(x1, x2) �������ϣ�(x2, y2) ��������
  static uint8_t x1, y1, x2, y2;
  x1 = LCD_W / 2 - PIC_W / 2;
  y1 = LCD_H / 2 - PIC_H / 2;
  x2 = LCD_W / 2 + PIC_W / 2;
  y2 = LCD_H / 2 - PIC_H / 2;  

  /* Infinite loop */
  for(;;)
  {
    // ����
    if(!key_locked && key1_flag)
    {
      key1_flag = false;
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);
      if(img_pointer == 0)
        ECGTaskHandle = osThreadNew(StartECGTask, NULL, &ECGTask_attributes);
      else if(img_pointer == 1)
        WifiTaskHandle = osThreadNew(StartWifiTask, NULL, &WifiTask_attributes);
      else if(img_pointer == 2)
        SettingTaskHandle = osThreadNew(StartSettingTask, NULL, &SettingTask_attributes);

      vTaskDelete(NULL);
    }

    // ��һ��
    if(key2_flag)
    {
      key2_flag       = false;
      key_locked      = true;

      is_first_stage  = true;
      is_second_stage = false;
      tick = 0;
    }

    // �˳�
    if(key3_flag)
    {
      // ��־λ���㡢���������뿪������������ɱ
      key3_flag = false;
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);
      InterfaceTaskHandle = osThreadNew(StartInterfaceTask, NULL, &InterfaceTask_attributes);
      vTaskDelete(NULL);
    }


    if(key_locked)
    {
      // ����һ�׶Σ�ͼ������Ƴ�
      if(is_first_stage)
      { 
        last_move = EasingFunction(tick - 1);
        move      = EasingFunction(tick);

        // May overflow
        if(move <= LCD_H)
        {
          /*
            x1 = LCD_W / 2 - PIC_W / 2;
            y1 = LCD_H / 2 - PIC_H / 2;
            x2 = LCD_W / 2 + PIC_W / 2;
            y2 = LCD_H / 2 - PIC_H / 2;
          */
          LCD_Fill(x1, y1 + last_move, x2, y2 + move, BLACK);
          LCD_ShowPicture(x1, y1 + move, PIC_W, PIC_H, imgs[img_pointer]);
          tick += 1;
        }
        else
        {
          tick = 0;
          is_first_stage  = false;
          is_second_stage = true;
        }
      }
      // �������׶Σ���������
      if(is_second_stage)
      {
        last_move = EasingFunction(tick - 1);
        move      = EasingFunction(tick);

        if(move + PIC_H <= LCD_H / 2)
        {
          /*
            x1 = LCD_W / 2 - PIC_W / 2;
            y1 = 0;
            x2 = LCD_W / 2 + PIC_W / 2;
            y2 = 0;          
          */
          LCD_Fill(x1, 0 + last_move, x2, 0 + move, BLACK);
          LCD_ShowPicture(x1, move, PIC_W, PIC_H, imgs[img_pointer + 1]);
          tick += 1;
        }
        else
        {
          LCD_Fill(x1, last_move, x2, y2, BLACK);

          tick            = 0;
          is_first_stage  = false;
          is_second_stage = false;
          key_locked      = false;
          img_pointer     += 1;
          img_pointer     %= MENU_SUM;
        }        
      }
    }
    if(!key_locked)
    {
      /*
        x1 = LCD_W / 2 - PIC_W / 2;
        y1 = LCD_H / 2 - PIC_H / 2;
      */
      LCD_ShowPicture(x1, y1, PIC_W, PIC_H, imgs[img_pointer]);
    }

    osDelay(1);
  }
  /* USER CODE END StartMenuTask */
}

/* USER CODE BEGIN Header_StartECGTask */
/**
* @brief Function implementing the ECGTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartECGTask */
void StartECGTask(void *argument)
{
  /* USER CODE BEGIN StartECGTask */
  // ����״̬
  uint16_t lead_state = false;

  // �������ݡ��������ݺ���������
  uint16_t data, wave_data, rate_data;

  // ���ߵ�����
  static uint16_t x = 1, y = 40, last_y = 40;

  // ���ʳ�ʼͼ��
  LCD_ShowPicture(35, 120, 20, 20, img_heart);
  // ���ʳ�ʼΪ0
  LCD_ShowIntNum(55, 120, 0, 3, CYAN, BLACK, 16);
  // ����״̬��ʾ��
  LCD_ShowPicture(5, 5, 69, 20, img_word);
  // ��ʼ����״̬Ϊ�Ͽ�
  LCD_ShowPicture(80, 5, 20, 20, img_lead_off);  

  /* Infinite loop */
  for(;;)
  {
    if(key3_flag)
    {
      key2_flag = key1_flag = key3_flag = false;
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);
      MenuTaskHandle = osThreadNew(StartMenuTask, NULL, &MenuTask_attributes);
      vTaskDelete(NULL);    
    }

    // �����ݽ���Ϊ12����λ��1λ��״̬λ
    Decode(rec_data, &data, &lead_state);

    // ����λ�ֿɷ�Ϊ�������ݺ���������
    if(data < 350 && data > 20)
    {
      rate_data = data;
      if(lead_state)
        LCD_ShowIntNum(55, 120, rate_data, 3, CYAN, BLACK, 16);
      else  
        LCD_ShowIntNum(55, 120, 0,         3, CYAN, BLACK, 16);
    }
    else 
      wave_data  = data; 

    // ��ʾ����״̬
    if(lead_state)
      LCD_ShowPicture(80, 5, 20, 20, img_lead_on);
    else
      LCD_ShowPicture(80, 5, 20, 20, img_lead_off);

    // ��ʾ�������������½�
    // LCD_ShowIntNum(0, 144, wave_data, 4, BLACK, BLACK, 16);

    // ���������ݴ���ӳ�����ʾ
    y = 40 + (wave_data - 1780) * 80 / 520;
    if(y < 40)  y = 40;
    if(y > 120) y = 120;
    if(!lead_state) y = 80;
    
    /* ���ߣ���������Ӵ����� */
    LCD_DrawLine(x - 1, LCD_H - last_y, x, LCD_H - y, color);
    LCD_DrawLine(x, LCD_H - y, x + 1, LCD_H - last_y, color);
    LCD_DrawLine(x, LCD_H - last_y, x + 1, LCD_H - y, color);
    LCD_DrawLine(x + 1, LCD_H - y, x + 2, LCD_H - last_y, color);

    // ����һ��������
    x++;
    if(x == LCD_W)
    {
      x = 1;
      LCD_Fill(0, 40, LCD_W, 120 + 1, BLACK);
    }
    last_y = y;
  }
  /* USER CODE END StartECGTask */
}

/* USER CODE BEGIN Header_StartWifiTask */
/**
* @brief Function implementing the WifiTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartWifiTask */
void StartWifiTask(void *argument)
{
  /* USER CODE BEGIN StartWifiTask */
  /* Infinite loop */
  for(;;)
  {
    if(key3_flag)
    {
      key2_flag = key1_flag = key3_flag = false;

      HAL_GPIO_WritePin(C2C_WIFI_GPIO_Port, C2C_WIFI_Pin, GPIO_PIN_RESET);
      HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_RESET);
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);

      MenuTaskHandle = osThreadNew(StartMenuTask, NULL, &MenuTask_attributes);
      vTaskDelete(NULL); 
    } 
    LCD_ShowString(0, 0, "WIFI", BLUE, BLACK, 16, 0);  

    HAL_GPIO_WritePin(LED_GPIO_Port, LED_Pin, GPIO_PIN_SET);
    HAL_GPIO_WritePin(C2C_WIFI_GPIO_Port, C2C_WIFI_Pin, GPIO_PIN_SET);
    
    osDelay(1);
  }
  /* USER CODE END StartWifiTask */
}

/* USER CODE BEGIN Header_StartSettingTask */
/**
* @brief Function implementing the SettingTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartSettingTask */
void StartSettingTask(void *argument)
{
  /* USER CODE BEGIN StartSettingTask */
  /* Infinite loop */
  for(;;)
  {
    if(key3_flag)
    {
      key2_flag = key1_flag = key3_flag = false;
      LCD_Fill(0, 0, LCD_W, LCD_H, BLACK);
      MenuTaskHandle = osThreadNew(StartMenuTask, NULL, &MenuTask_attributes);
      vTaskDelete(NULL);   
    }   
    LCD_ShowString(0, 0, "SETTING", BLUE, BLACK, 16, 0);   
    osDelay(1);
  }
  /* USER CODE END StartSettingTask */
}

/* Private application code --------------------------------------------------*/
/* USER CODE BEGIN Application */

/* ���λ������������ڲ˵��л����� */
uint32_t EasingFunction(uint32_t t)
{
  return t * t * t;
}

/* ���ݽ���Ϊadc���ݺ͵���״̬ */
void Decode(uint16_t rec, uint16_t *data, uint16_t *state)
{
    *state = rec & 0x8000;
    *data  = rec & 0x7FFF;
}

/* USER CODE END Application */

