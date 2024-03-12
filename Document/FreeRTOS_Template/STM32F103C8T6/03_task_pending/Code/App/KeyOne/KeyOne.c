/*********************************************************************************************************
* 模块名称: keyOne.c
* 摘 要: 按键扫描模块
* 当前版本: 1.0.0
* 作 者: XXX
* 完成日期: 20XX 年 XX 月 XX 日
* 内 容:
* 注 意: none
**********************************************************************************************************
* 取代版本:
* 作 者:
* 完成日期:
* 修改内容:
* 修改文件:
*********************************************************************************************************/
 
 
/*********************************************************************************************************
* 包含头文件
*********************************************************************************************************/
#include "KeyOne.h"

/*********************************************************************************************************
* 宏定义
*********************************************************************************************************/
#define KEY1 (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_2)) //读取pA2的引脚电平
#define KEY2 (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_4)) //读取pA4的引脚电平
#define KEY3 (GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_5)) //读取pA6的引脚电平
/*********************************************************************************************************
* 枚举结构体定义
*********************************************************************************************************/


/*********************************************************************************************************
* 内部变量
*********************************************************************************************************/
static u8 s_arrKeyDown[MAX_NAME_VAL];

/*********************************************************************************************************
* 内部函数声明
*********************************************************************************************************/
static void KeyOne_GPIO_Config(void);

/*********************************************************************************************************
* 内部函数实现
*********************************************************************************************************/

static void KeyOne_GPIO_Config(void){
  
  EXTI_InitTypeDef EXTI_InitStructure;  //EXTI_InitStructure用于存放EXTI的参数
  NVIC_InitTypeDef NVIC_InitStructure;  //NVIC_InitStructure用于存放NVIC的参数
  GPIO_InitTypeDef GPIO_InitStructure;//定义GPIO_InitStructure结构体
  //使能RCC相关时钟
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);  //使能AFIO的时钟
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);//使能gpioa时钟
  
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2 | GPIO_Pin_4 | GPIO_Pin_6;//设置gpio引脚
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;//设置gpio速度
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;//设置gpio模式
  GPIO_Init(GPIOA, &GPIO_InitStructure);//应用gpio初始化函数
  
  //配置PC1的EXTI和NVIC 
  GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource6);   //选择引脚作为中断线
  EXTI_InitStructure.EXTI_Line = EXTI_Line6;                    //选择中断线
  EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;           //开放中断请求
  EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Falling;        //设置为下降沿触发
  EXTI_InitStructure.EXTI_LineCmd = ENABLE;                     //使能中断线 
  EXTI_Init(&EXTI_InitStructure);                               //根据参数初始化EXTI
  
  NVIC_InitStructure.NVIC_IRQChannel = EXTI9_5_IRQn;              //中断通道号
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;     //设置抢占优先级
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;            //设置子优先级
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;               //使能中断
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_4);
  NVIC_Init(&NVIC_InitStructure);                               //根据参数初始化NVIC
}

/*********************************************************************************************************
* API 函数实现
*********************************************************************************************************/

void KeyOne_Init(){
  KeyOne_GPIO_Config();//配置gpio
  s_arrKeyDown[KEY1_NAME_VAL] = KEY_DOWN_LEVEL_KEY1;//按键按下时为低电平
  s_arrKeyDown[KEY2_NAME_VAL] = KEY_DOWN_LEVEL_KEY2;
  s_arrKeyDown[KEY3_NAME_VAL] = KEY_DOWN_LEVEL_KEY3;
}


void KeyOne_Scan(u8 keyname, void(*OnKeyOneUp)(void), void(*OnKeyOneDown)(void)){
  static u8 s_arrKeyVal[MAX_NAME_VAL];
  static u8 s_arrKeyFlag[MAX_NAME_VAL];
  s_arrKeyVal[keyname] <<= 1;//左移一位 实现一次10ms的检测 总共存八次
  switch(keyname){
    case KEY1_NAME_VAL: 
      s_arrKeyVal[keyname] = s_arrKeyVal[keyname] | KEY1;//读取key1的值赋入数组 0按下 1松开
      break;
    case KEY2_NAME_VAL: 
      s_arrKeyVal[keyname] = s_arrKeyVal[keyname] | KEY2;
      break;
    case KEY3_NAME_VAL: 
      s_arrKeyVal[keyname] = s_arrKeyVal[keyname] | KEY3;
      break;
    default:
      break;
  }//检查key80ms内是否一直按下 80ms标志位是否置TURE 只有重复了八次才有可能执行
  if(s_arrKeyVal[keyname] == s_arrKeyDown[keyname] && s_arrKeyFlag[keyname] == TRUE){
    (*OnKeyOneDown)();//执行按下程序
    s_arrKeyFlag[keyname] = FALSE;//执行完成标志位置
  }else if(s_arrKeyVal[keyname] == (u8)(~s_arrKeyDown[keyname]) && s_arrKeyFlag[keyname] == FALSE){
    (*OnKeyOneUp)();//执行松开程序
    s_arrKeyFlag[keyname] = TRUE;
  }
}
