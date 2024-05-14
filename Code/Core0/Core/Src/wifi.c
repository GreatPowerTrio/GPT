#include "wifi.h"
#include "AT_cmdset.h"
#include "main.h"
#include "FreeRTOS.h"
#include "cmsis_os2.h"

extern uint8_t rec_data;
extern UART_HandleTypeDef huart2;
extern char print_buf[PRINT_BUF_SIZE];

#define SPRINTF_BUF_SIZE 20
/* 由函数 wifi_send_data( data ) 调用，将整形转化为字符串储存在 sprintf_buf 中 */
char sprintf_buf[SPRINTF_BUF_SIZE];



char *esp8266_return_code[] = { "OK", "ready", "CONNECT", "SEND OK", "CLOSED"};
uint8_t  return_code_size[] = {    2,       5,         7,         7,        6};
bool           flag_table[] = {false,   false,     false,     false,    false};
uint8_t      flag_pointer[] = {    0,       0,         0,         0,        0};



/**
  * @brief  wifi内部软件延时函数
  * @param  tick : 延时的节拍数
  * @retval 无
  * @note   没有精确延时，以从0到500的循环递增为一个节拍
  */
void wifi_delay(uint16_t tick)
{
    for (uint16_t i = 0; i < tick; i++)
    {
        for (uint16_t j = 0; j < 500; j++)
        {
            // Software delay loop
        }
    }
}



/**
  * @brief  获取指定返回代码的标志位
  * @param  code : ESP8266返回代码的枚举类型
  * @retval 指定代码的标志位
  */
bool wifi_get_flag(ESP8266_RETURN_CODE code)
{
    return flag_table[code];
}



/**
  * @brief  清除指定返回代码的标志位
  * @param  code : ESP8266返回代码的枚举类型
  * @retval 无
  */
void wifi_clear_flag(ESP8266_RETURN_CODE code)
{
    flag_table[code] = false;
    flag_pointer[code] = 0;
}


/**
  * @brief  设置指定返回代码的标志位
  * @param  code : ESP8266返回代码的枚举类型
  * @retval 无
  * @note   用于只检测一次的标志位，设置后在串口中断中不会重复检测
  */
void wifi_set_flag(ESP8266_RETURN_CODE code)
{
    flag_table[code] = true;
}




/**
  * @brief  在串口接收中断中，逐字符地检查指定标志位
  * @param  code        ESP8266返回代码的枚举类型
  * @param  rec_data    串口接收中断接收的字符
  * @retval 无
  * @note   在 stmf1xx.it.c 的 HAL_UART_RxCpltCallback() 中调用
  */
void wifi_check_flag(ESP8266_RETURN_CODE code, char rec_data)
{   
    if(flag_table[code] == false)
    {
        if(rec_data == esp8266_return_code[code][flag_pointer[code]])
            flag_pointer[code] += 1;
        else
            flag_pointer[code]  = 0;
    
        if(flag_pointer[code] == return_code_size[code])
        {
            flag_pointer[code] = 0;
            flag_table[code] = true;
        }
    }
}


/**
  * @brief  等待esp8266返回指定代码
  * @param  code  ESP8266返回代码的枚举类型
  * @retval 无
  * @note   若没有接收到指定代码，该函数会阻塞，能够实现同步响应
  */
void wifi_wait_code(ESP8266_RETURN_CODE code)
{
    while(wifi_get_flag(code) == false)
        wifi_delay(1);
    wifi_clear_flag(code);
}


void wifi_wait_code_ex(ESP8266_RETURN_CODE code)
{
    while(wifi_get_flag(code) == false)
      osDelay(1);
    wifi_clear_flag(code);
}



/**
  * @brief  设置为STATION模式，连接其他设备的wifi
  * @retval 无
  */
void wifi_set_STATION(void)
{
    // 设置为STATION模式
    print_wifi("AT+CWMODE=1\r\n");
    wifi_wait_code(OK);

    // 重启生效
    print_wifi("AT+RST\r\n");
    wifi_wait_code(READY);
    wifi_clear_flag(OK);

    // 查询当前WIFI列表，需要花费时间
    print_wifi("AT+CWLAP\r\n");
    wifi_wait_code(OK);
  
    // 设置要连接的wifi名称和密码
    print_wifi("AT+CWJAP=\"sbming\",\"1355qwer\"\r\n");
    wifi_wait_code(OK);

    print_wifi("AT+CIFSR\r\n");
    wifi_wait_code(OK);
  
    print_wifi("AT+CIPMUX=1\r\n");
    wifi_wait_code(OK);

    print_wifi("AT+CIPSERVER=1,8080\r\n");
    wifi_wait_code(OK);
}

/**
  * @brief  设置AP模式，产生热点信号
  * @retval 无
  */
void wifi_set_AP(void)
{
    // 设置为AP模式
    print_wifi("AT+CWMODE=2\r\n");
    wifi_wait_code(OK);

    // 重启生效
    print_wifi("AT+RST\r\n");
    wifi_wait_code(READY);
    wifi_clear_flag(OK);

    // 查看esp8266 IP，固定位192.168.4.1
    print_wifi("AT+CIFSR\r\n");
    wifi_wait_code(OK);

    print_wifi("AT+CIPMUX=1\r\n");
    wifi_wait_code(OK);
 
    print_wifi("AT+CIPSERVER=1,8080\r\n");
    wifi_wait_code(OK);

    // 设置热点的名称和密码
    print_wifi("AT+CWSAP=\"GPT\",\"12345678\",1,3\r\n");
    wifi_wait_code(OK);

    // ready 标志位检测一次即可
    wifi_set_flag(READY);
}


/**
  * @brief  发送单个数据
  * @param  data 要发送的16位数据
  * @retval 无
  */
void wifi_send_data(uint16_t data)
{
    // 接收到断开标志后，重启连接标志的判断
    if(wifi_get_flag(CLOSED) == true)
    {
      wifi_clear_flag(CONNECT);
      wifi_clear_flag(CLOSED);
    }

    if(wifi_get_flag(CONNECT) == true)
    {
      print_wifi("AT+CIPSEND=0,%d\r\n", (int)sprintf(sprintf_buf, "HEAD %d END", data));
      // wifi_wait_code_ex(OK);

      print_wifi("%s\r\n", sprintf_buf); 
      // wifi_wait_code_ex(SEND_OK);
    }

}


void wifi_send_package(uint16_t *buf)
{
  // len = sprintf(sprintf_buf, "");
  int len = sprintf(sprintf_buf, "HEAD ");
  for(int i = 0; i < SIZE; i++)
    len += sprintf(sprintf_buf, "%s%d ", sprintf_buf, buf[i]) - len; 
  len += sprintf(sprintf_buf, "%sEND", sprintf_buf) - len;

  if(wifi_get_flag(CONNECT) == true)
  {
    print_wifi("AT+CIPSEND=0,%d\r\n", len);
    wifi_wait_code_ex(OK);
    print_wifi("%s\r\n", sprintf_buf); 
    wifi_wait_code_ex(SEND_OK);

  }
}



void wifi_send(uint16_t dat)
{
  int len = sprintf(sprintf_buf, "HEAD %d END", (int)dat);
  if(wifi_get_flag(CONNECT) == true)
  {
    print_wifi("AT+CIPSEND=0,%d\r\n", len);
    wifi_wait_code_ex(OK);
    print_wifi("%s\r\n", sprintf_buf); 
    wifi_wait_code_ex(SEND_OK);

  }  
}
