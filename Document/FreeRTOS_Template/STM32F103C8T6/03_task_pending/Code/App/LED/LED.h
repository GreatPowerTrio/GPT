/*********************************************************************************************************
* 模块名称：LED.h
* 摘    要：LED模块
* 当前版本：1.0.0
* 作    者：
* 完成日期：2023 年 08 月 01 日 
* 内    容：LED测试程序
* 注    意： 
**********************************************************************************************************
* 取代版本：
* 作    者：
* 完成日期：
* 修改内容：
* 修改文件：
*********************************************************************************************************/
#ifndef _LED_H_
#define _LED_H_

/*********************************************************************************************************
*                                              包含头文件
*********************************************************************************************************/
#include "stm32f10x.h"                  // Device header


/*********************************************************************************************************
*                                              宏定义
*********************************************************************************************************/

/*********************************************************************************************************
*                                              枚举结构体定义
*********************************************************************************************************/

/*********************************************************************************************************
*                                              API函数声明
*********************************************************************************************************/
void LED_Init(void);
void LED_Flicker(void);
void LED1_Flicker(void);
#endif