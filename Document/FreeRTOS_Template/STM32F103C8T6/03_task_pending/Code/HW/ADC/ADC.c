/*********************************************************************************************************
* 模块名称: ADC.c
* 摘 	  要: ADC模块
* 当前版本: 1.0.0
* 作 	  者: xrl
* 完成日期: 2023 年 08 月 01 日
* 内	  容: ADC单通道配置
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
#include "ADC.h"

/*********************************************************************************************************
* 宏定义
*********************************************************************************************************/
#define ADC1_DR_ADDR 0x4001244C //规则数据寄存器地址 == &(uint32_t ADC1->DR) 该结构体指针是stm32标准库搞好的

/*********************************************************************************************************
* 枚举结构体定义
*********************************************************************************************************/


/*********************************************************************************************************
* 内部变量
*********************************************************************************************************/
static uint16_t AD_value[1] ; //SRAM里存储的静态变量

/*********************************************************************************************************
* 内部函数声明
*********************************************************************************************************/
static void ADC_Config(void);
static void DMA_Config(void);


/*********************************************************************************************************
* 内部函数实现
*********************************************************************************************************/
static void ADC_Config(void){
  GPIO_InitTypeDef GPIO_InitStructure;
  ADC_InitTypeDef ADC_InitStructure;
  
  RCC_ADCCLKConfig(RCC_PCLK2_Div6);       //ADCCLK 六分频 72/6 = 12MHz
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_ADC1, ENABLE);
  
  GPIO_InitStructure.GPIO_Pin   = GPIO_Pin_0;           //设置引脚
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;     //设置I/O输出速度
  GPIO_InitStructure.GPIO_Mode  = GPIO_Mode_AIN;        //设置模式
  GPIO_Init(GPIOA, &GPIO_InitStructure); 
  
  ADC_RegularChannelConfig(ADC1, ADC_Channel_0, 1, ADC_SampleTime_55Cycles5); //ADC1序列1写入通道0 采样时间55.5时钟周期
  
  ADC_InitStructure.ADC_Mode = ADC_Mode_Independent; //独立模式
  ADC_InitStructure.ADC_DataAlign = ADC_DataAlign_Right;
  ADC_InitStructure.ADC_ExternalTrigConv = ADC_ExternalTrigConv_None; //外部不触发 即软件触发
  ADC_InitStructure.ADC_ContinuousConvMode = DISABLE; //连续还是单次
  ADC_InitStructure.ADC_NbrOfChannel = 1; //通道数目，扫描模式下才有用
  ADC_InitStructure.ADC_ScanConvMode = DISABLE; //扫描还是非扫描
  ADC_Init(ADC1, &ADC_InitStructure);
  
  ADC_DMACmd(ADC1, ENABLE); //ADC1的DMA请求使能
  
  ADC_Cmd(ADC1, ENABLE);  //打开ADC1
  
  //校准
  ADC_ResetCalibration(ADC1);       //复位校准
  while(ADC_GetResetCalibrationStatus(ADC1) == SET);   //等待复位校准完成
  ADC_StartCalibration(ADC1);       //启动校准
  while(ADC_GetCalibrationStatus(ADC1) == SET);   //等待校准完成
}

static void DMA_Config(void){
  RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
  
  DMA_InitTypeDef DMA_InitStructure;
  DMA_InitStructure.DMA_BufferSize = 4;                 //DMA缓冲区大小
  DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralSRC;    //DMA传输方向 外设到存储器
  DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;                  //失能存储器到存储器
  DMA_InitStructure.DMA_MemoryBaseAddr = (uint32_t)AD_value;    //存储器地址  程序运行产生的变量是存在SRAM里直接取变量地址就是存储器里的s
  DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_HalfWord;   //存储器数据大小
  DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Disable;//存储器地址不自增
  DMA_InitStructure.DMA_Mode = DMA_Mode_Normal;         //循环发送模式
  DMA_InitStructure.DMA_PeripheralBaseAddr = ADC1_DR_ADDR;//ADC1外设地址
  DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_HalfWord;   //ADC1数据大小
  DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;//外设地址不自增
  DMA_InitStructure.DMA_Priority = DMA_Priority_High;             //高优先级
  DMA_Init(DMA1_Channel1, &DMA_InitStructure);
  
  DMA_Cmd(DMA1_Channel1, ENABLE);//使能DMA1_Channel1
  
}

/*********************************************************************************************************
* API 函数实现
*********************************************************************************************************/
void AD_Init(void){
  DMA_Config();
  ADC_Config();
}

uint16_t AD_GetValue(void){
  
  DMA_Cmd(DMA1_Channel1, DISABLE);
  DMA_SetCurrDataCounter(DMA1_Channel1, 1);
  DMA_Cmd(DMA1_Channel1, ENABLE);
  
  ADC_SoftwareStartConvCmd(ADC1, ENABLE);   //ADC1开始转换
  while(DMA_GetFlagStatus(DMA1_FLAG_TC1) == RESET){
    ;
  }
  DMA_ClearFlag(DMA1_FLAG_TC1);
//  while(ADC_GetFlagStatus(ADC1, ADC_FLAG_EOC) == RESET){  //读取结束EOC由硬件自动置1
//    ;
//  }
  
  //AD_value = ADC_GetConversionValue(ADC1);    //获取ADC转换值
  return AD_value[0];
}

float AD_GetVoltage(void){
  float voltage;
  voltage = AD_value[0] * 3.3 / 4095; //将AD值映射到电压范围 0~4095 -> 0~3.3
  return voltage;
}



