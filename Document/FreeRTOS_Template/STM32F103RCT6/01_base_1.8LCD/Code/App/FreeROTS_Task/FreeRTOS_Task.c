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
#include "freertos_task.h"
#include "config.h"
#include "stdio.h"
/*********************************************************************************************************
* 宏定义
*********************************************************************************************************/


/*********************************************
Start_Task:堆栈、优先级、句柄、回调函数
**********************************************/

#define START_TASK_STACK_SIZE 128 //设置堆栈大小
#define START_TASK_PRIO   1
TaskHandle_t Start_Task_Handler;
static void Start_Task(void * pvParameters);


/*********************************************
Task1:堆栈、优先级、句柄、回调函数
**********************************************/
#define TASK1_STACK_SIZE 128
#define TASK1_PRIO 2
TaskHandle_t Task1_Handler;
static void Task1(void * pvParameters);

/*********************************************
Task2:堆栈、优先级、句柄、回调函数
**********************************************/
#define TASK2_STACK_SIZE 128
#define TASK2_PRIO 3
TaskHandle_t Task2_Handler;
static void Task2(void * pvParameters);

/*********************************************
LCD_Init_task:堆栈、优先级、句柄、回调函数
**********************************************/
#define LCD_Init_task_STACK_SIZE 128
#define LCD_Init_task_PRIO 1
TaskHandle_t LCD_Init_task_Handler;
static void LCD_Init_task(void * pvParameters);

/*********************************************************************************************************
* 枚举结构体定义
*********************************************************************************************************/


/*********************************************************************************************************
* 内部变量
*********************************************************************************************************/



/*********************************************************************************************************
* 内部函数声明
*********************************************************************************************************/



//static void Task3(void * pvParameters);
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
  xTaskCreate((TaskFunction_t)          LCD_Init_task,  
              (char*)                   "LCD_Init_task",  
              (configSTACK_DEPTH_TYPE)  LCD_Init_task_STACK_SIZE,  
              (void*)                   NULL,
              (UBaseType_t)             LCD_Init_task_PRIO,
              (TaskHandle_t*)            &LCD_Init_task_Handler);
  vTaskDelete(NULL); //开始结束时删除自己
}

static void Task1(void * pvParameters){
  while(1){
    LED = ~LED;
    vTaskDelay(500);//FreeRTOS会在检测到这个Delay之后进行任务切换
  }
}

static void Task2(void * pvParameters){
  while(1){
    static uint8_t i = 0,j = 0;
    LCD_DrawPoint(i,j, WHITE);
    i++;
    j++;
    vTaskDelay(500);
  }
}
static void LCD_Init_task(void * pvParameters){
  LCD_Init();
  //taskENTER_CRITICAL();//进入临界段代码保护，防止时间片过了代码未执行完·
  vTaskSuspendAll();//防止任务切换导致代码运行出错
  LCD_Fill(0,0,128,160,RED);
  xTaskResumeAll();
  //taskEXIT_CRITICAL();
  vTaskDelete(NULL);
}

//static void Task3(void * pvParameters){
//  
//}



/*********************************************************************************************************
* API 函数实现
*********************************************************************************************************/

/*********************************************************************************************************
* 函数名称: FreeRTOS_Task_Init
* 函数功能: 第一个FreeRTOS程序
* 输入参数: void
* 输出参数: void
* 返 回 值: none
* 创建日期: 2023 年 10 月 09 日
* 注		意: none
*********************************************************************************************************/

void FreeRTOS_Task_Init(void){
  xTaskCreate((TaskFunction_t)          Start_Task,  
              (char*)                   "Start_Task",  
              (configSTACK_DEPTH_TYPE)  START_TASK_STACK_SIZE,  
              (void*)                   NULL,
              (UBaseType_t)             START_TASK_PRIO,
              (TaskHandle_t*)            &Start_Task_Handler);
  vTaskStartScheduler();
}



