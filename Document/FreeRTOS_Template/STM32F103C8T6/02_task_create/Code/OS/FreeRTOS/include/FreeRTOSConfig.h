/*
 * FreeRTOS V202212.01
 * Copyright (C) 2020 Amazon.com, Inc. or its affiliates.  All Rights Reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 * https://www.FreeRTOS.org
 * https://github.com/FreeRTOS
 *
 */

#ifndef FREERTOS_CONFIG_H
#define FREERTOS_CONFIG_H

/*-----------------------------------------------------------
 * Application specific definitions.
 *
 * These definitions should be adjusted for your particular hardware and
 * application requirements.
 *
 * THESE PARAMETERS ARE DESCRIBED WITHIN THE 'CONFIGURATION' SECTION OF THE
 * FreeRTOS API DOCUMENTATION AVAILABLE ON THE FreeRTOS.org WEB SITE.
 *
 * See http://www.freertos.org/a00110.html
 *----------------------------------------------------------*/

#define configUSE_PREEMPTION		1   //使用抢占式内核
//#define configUSE_TIME_SLICING  1   //使用时间片调度（默认是使能的）
#define configUSE_IDLE_HOOK			0   //使用模式的钩子函数
#define configUSE_TICK_HOOK			0   //使用TICK的钩子函数
#define configUSS_PORT_OPTIMISED_SELECTION  1  //1使用硬件选择下一个执行任务 （需要架构拥有计算前导零[CLZ]的指令）
#define configUSE_TICKLESS_IDLE 0   //1使用低功耗模式
#define configUSE_QUEUE_SETS    1   //1使用队列集
#define configCPU_CLOCK_HZ			( ( unsigned long ) 72000000 )  //CPU主频
//#define configSYSTICK_CLOCK_HZ  (configCPU_CLOCK_HZ)  //定义systick的时钟频率 默认与系统时钟相同。
#define configTICK_RATE_HZ			( ( TickType_t ) 1000 )
#define configMAX_PRIORITIES		( 32 )  //可使用最大优先级
#define configMINIMAL_STACK_SIZE	( ( unsigned short ) 128 )

#define configMAX_TASK_NAME_LEN		( 16 )

#define configUSE_16_BIT_TICKS		0   //设置系统节拍计数器的数据类型 0为32位
#define configIDLE_SHOULD_YIELD		1   //1同优先级的任务可以抢占空闲任务
#define configUSE_TASK_NOTIFCATIONS 1 //1 使能任务间的消息传递
#define configENABLE_BACKWARD_COMPATIBILITY 0 //1 兼容老版本API函数

/*Memory Allocation*/
#define configTOTAL_HEAP_SIZE		( ( size_t ) ( 17 * 1024 ) )  //17k总堆栈
#define configSUPPORT_STATIC_ALLOCATION   0   //1 支持静态申请分配内存，默认0
#define configSUPPORT_DYNAMIC_ALLOCATION  1   //1 支持动态申请分配内存，默认1

/*Debug*/
#define configUSE_TRACE_FACILITY	1   //使能可视化追踪调试
#define configUSE_STATS_FORMATTING_FUNCTION 1

/*Software Timer*/
#define configUSE_TIMERS  1 //1 使能软件定时器
#define configTIMER_TASK_PRIORITY (configMAX_PRIORITIES - 1)
#define configTIMER_QUEUE_LENGTH  5
#define configTIMER_TASK_STACK_DEPTH  (configMINIMAL_STACK_SIZE * 2)


/* Set the following definitions to 1 to include the API function, or zero
to exclude the API function. */

#define INCLUDE_vTaskPrioritySet		    1   //设置任务优先级 
#define INCLUDE_uxTaskPriorityGet		    1   //获取任务优先级
#define INCLUDE_vTaskDelete				      1   //删除任务
#define INCLUDE_vTaskCleanUpResources	  1   //
#define INCLUDE_vTaskSuspend			      1   //挂起任务
#define INCLUDE_xResumeFromISR          1   //恢复在中断中挂起的任务
#define INCLUDE_vTaskDelayUntil			    1   //任务绝对延时
#define INCLUDE_vTaskDelay				      1   //任务延时
#define INCLUDE_xTaskGetSchedulerState  1   //
#define INCLUDE_xTaskGetCurrentTaskHandle 1 //
//#define INCLUDE_xTimerPendFunctionCall  1   //
#define INCLUDE_eTaskGetState           1   //

/* This is the raw value as per the Cortex-M3 NVIC.  Values can be 255
(lowest) to 0 (1?) (highest). */
#define configKERNEL_INTERRUPT_PRIORITY 		255
/* !!!! configMAX_SYSCALL_INTERRUPT_PRIORITY must not be set to zero !!!!
See http://www.FreeRTOS.org/RTOS-Cortex-M3-M4.html. */
#define configMAX_SYSCALL_INTERRUPT_PRIORITY 	191 /* equivalent to 0xb0, or priority 11. */

/*FreeRTOS interrupt relative define*/
#define xPortPendSVHandler    PendSV_Handler
#define vPortSVCHandler       SVC_Handler

/* This is the value being used as per the ST library which permits 16
priority values, 0 to 15.  This must correspond to the
configKERNEL_INTERRUPT_PRIORITY setting.  Here 15 corresponds to the lowest
NVIC value of 255. */
#define configLIBRARY_KERNEL_INTERRUPT_PRIORITY	15

/*Assert*/
//#define vAssertCalled(char, int) printf("Error: %s, %d\r\n", char, int)
//#define configASSERT(x) if((x) == 0) vAssertCalled(__FILE__,__LINE__)


#endif /* FREERTOS_CONFIG_H */

