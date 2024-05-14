/*********************************************************************************************************
* 模块名称: 
* 摘 	  要: 
* 当前版本: 1.0.0
* 作 	  者: 
* 完成日期: 20XX 年 XX 月 XX 日
* 内	  容:
* 注 	  意: none
**********************************************************************************************************
* 取代版本:
* 作 	  者:
* 完成日期:
* 修改内容:
* 修改文件:
*********************************************************************************************************/
 
 
/*********************************************************************************************************
* 包含头文件
*********************************************************************************************************/
#include "freertos_demo.h"
#include "config.h"
#include "stdio.h"
/*********************************************************************************************************
* 宏定义
*********************************************************************************************************/

//开始任务

#define START_TASK_STACK_SIZE 128 //设置堆栈大小
#define START_TASK_PRIO   1

//任务一
#define TASK1_STACK_SIZE 128
#define TASK1_PRIO 3
//任务二
#define TASK2_STACK_SIZE 128
#define TASK2_PRIO 4
//任务三
#define TASK3_STACK_SIZE 128
#define TASK3_PRIO 2

/*********************************************************************************************************
* 枚举结构体定义
*********************************************************************************************************/


/*********************************************************************************************************
* 内部变量
*********************************************************************************************************/
TaskHandle_t Start_Task_Handler;
TaskHandle_t Task1_Handler;
TaskHandle_t Task2_Handler;
TaskHandle_t Task3_Handler;
/*********************************************************************************************************
* 内部函数声明
*********************************************************************************************************/
static void Start_Task(void * pvParameters);
static void Task1(void * pvParameters);
static void Task2(void * pvParameters);
static void Task3(void * pvParameters);

static void ProcKeyDownKey1(void);
static void ProcKeyDownKey2(void);
static void ProcKeyDownKey3(void);
static void ProcKeyUpKey1(void);
static void ProcKeyUpKey2(void);
static void ProcKeyUpKey3(void);
/*********************************************************************************************************
* 内部函数实现
*********************************************************************************************************/

/*********************************************************************************************************
* 函数名称: 
* 函数功能: 
* 输入参数: 
* 输出参数: 
* 返 回 值: 
* 创建日期: 20XX 年 XX 月 XX 日
* 注		意: 
*********************************************************************************************************/

static void Start_Task(void * pvParameters){
  xTaskCreate((TaskFunction_t)          Task1, //任务对应函数 
              (char*)                   "Task1",  //任务名
              (configSTACK_DEPTH_TYPE)  TASK1_STACK_SIZE,   //分配堆栈大小  
              (void*)                   NULL,     //输入参数，目前作用不明
              (UBaseType_t)             TASK1_PRIO,   //任务优先级
              (TaskHandle_t*)            &Task1_Handler); //任务句柄，状态切换用
  xTaskCreate((TaskFunction_t)          Task2,  
              (char*)                   "Task2",  
              (configSTACK_DEPTH_TYPE)  TASK2_STACK_SIZE,  
              (void*)                   NULL,
              (UBaseType_t)             TASK2_PRIO,
              (TaskHandle_t*)            &Task2_Handler);
  xTaskCreate((TaskFunction_t)          Task3,  
              (char*)                   "Task3",  
              (configSTACK_DEPTH_TYPE)  TASK3_STACK_SIZE,  
              (void*)                   NULL,
              (UBaseType_t)             TASK3_PRIO,
              (TaskHandle_t*)            &Task3_Handler);
  vTaskDelete(NULL); //开始结束时删除自己
}

static void Task1(void * pvParameters){
  while(1){
    LED_Flicker();
    
    vTaskDelay(500);//FreeRTOS会在检测到这个Delay之后进行任务切换
  }
}

static void Task2(void * pvParameters){
  while(1){
    static uint16_t s_icnt = 0;
    char cnt[1];
    sprintf(cnt ,"%d",s_icnt);
    OLED_ShowString(1,1, cnt);
    s_icnt++;
    LED1_Flicker();
    vTaskDelay(500);
  }
}

static void Task3(void * pvParameters){
  while(1){
    KeyOne_Scan(KEY1_NAME_VAL, ProcKeyUpKey1, ProcKeyDownKey1);//检测按键1
    KeyOne_Scan(KEY2_NAME_VAL, ProcKeyUpKey2, ProcKeyDownKey2);//检测按键2
    KeyOne_Scan(KEY3_NAME_VAL, ProcKeyUpKey3, ProcKeyDownKey3);//检测按键3
    
    vTaskDelay(10);
  }
}

static void ProcKeyDownKey1(void){
  vTaskSuspend(Task1_Handler);
}
static void ProcKeyUpKey1(void){
  ;
}
static void ProcKeyDownKey2(void){
  vTaskSuspend(Task2_Handler);
}
static void ProcKeyUpKey2(void){
 vTaskResume(Task2_Handler);
}
static void ProcKeyDownKey3(void){
  ;
}
static void ProcKeyUpKey3(void){
  ;
}

void EXTI9_5_IRQHandler(void){
  BaseType_t xYieldRequired;
  if(EXTI_GetITStatus(EXTI_Line6) == SET){
    xYieldRequired = xTaskResumeFromISR(Task1_Handler);
    if(xYieldRequired == pdTRUE){
      portYIELD_FROM_ISR( xYieldRequired );//直接任务切换
    }
    EXTI_ClearITPendingBit(EXTI_Line6);
  }
}

/*********************************************************************************************************
* API 函数实现
*********************************************************************************************************/

/*********************************************************************************************************
* 函数名称: FreeRTOS_Demo
* 函数功能: 第一个FreeRTOS程序
* 输入参数: void
* 输出参数: void
* 返 回 值: none
* 创建日期: 2023 年 10 月 09 日
* 注		意: none
*********************************************************************************************************/

void FreeRTOS_Demo(void){
  xTaskCreate((TaskFunction_t)          Start_Task,  
              (char*)                   "Start_Task",  
              (configSTACK_DEPTH_TYPE)  START_TASK_STACK_SIZE,  
              (void*)                   NULL,
              (UBaseType_t)             START_TASK_PRIO,
              (TaskHandle_t*)            &Start_Task_Handler);
  vTaskStartScheduler();
}



