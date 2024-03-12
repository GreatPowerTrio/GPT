/*********************************************************************************************************
* 模块名称：ProcKeyOne.h
* 摘    要：
* 当前版本：1.0.0
* 作    者：
* 完成日期：20xx年xx月xx日 
* 内    容：
* 注    意：                                                                  
**********************************************************************************************************
* 取代版本：
* 作    者：
* 完成日期：
* 修改内容：
* 修改文件：
*********************************************************************************************************/
#ifndef _PROCKEYONE_H_
#define _PROCKEYONE_H_

/*********************************************************************************************************
*                                              包含头文件
*********************************************************************************************************/
#include "DataType.h"
#include "stdio.h"
#include "freertos_demo.h"

/*********************************************************************************************************
*                                              宏定义
*********************************************************************************************************/

/*********************************************************************************************************
*                                              枚举结构体定义
*********************************************************************************************************/

/*********************************************************************************************************
*                                              API函数声明
*********************************************************************************************************/

void InitProcKeyOne(void);
void ProcKeyDownKey1(void);
void ProcKeyUpKey1(void);
void ProcKeyDownKey2(void);
void ProcKeyUpKey2(void);
void ProcKeyDownKey3(void);
void ProcKeyUpKey3(void);

#endif
