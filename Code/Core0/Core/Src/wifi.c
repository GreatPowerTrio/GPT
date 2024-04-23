#include "wifi.h"
#include "AT_cmdset.h"
#include "main.h"

extern UART_HandleTypeDef huart2;
// 延时ms
void delay_ms(uint32_t ms)
{
    for (uint32_t i = 0; i < ms; i++)
    {
        for (uint32_t j = 0; j < 1000; j++)
        {
            // Software delay loop
        }
    }
}

/************************************************
 * name: wifi_server_init
 * function: 初始化WIFI模块为服务器模式
 * input: void
 * output: void
 * notice: 需要配合串口使用
 ************************************************/
extern char u_buf[100];
void wifi_server_init(void)
{
    // Initialize the wifi module
    print_wifi(AT_Test);             // 检测设备是否在线
    print_wifi(AT_RST);              // 让Wifi模块重启的命令
    print_wifi(AT_GMR);              // 获取固件版本号
    print_wifi(AT_SYSMSG);           // 获取系统信息
    print_wifi(AT_WIFI_MODE_SOFTAP); // 设置WIFI模式
    print_wifi(AT_RST);              // 让Wifi模块重启的命令
    print_wifi(AT_WIFI_SET);         // 设置WIFI
    print_wifi(AT_WIFI_SERVER);      // 开启服务器
}

/************************************************
 * name: wifi_client_init
 * function: 初始化WIFI模块为客户端模式
 * input: void
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_client_init(void)
{
    // Initialize the wifi module
    print_wifi("AT+RESTORE\r\n");
    delay_ms(100);
  print_wifi("AT+RST\r\n");
  delay_ms(100);
  print_wifi("AT+CWMODE=1\r\n");
  delay_ms(100);
  print_wifi("AT+RST\r\n");
  delay_ms(100);
  print_wifi("AT+UART=115200,8,1,0,0\r\n");
  delay_ms(100);
   //print_wifi("AT+CWSAP=\"esp8266\",\"123456789999\",1,3\r\n");
  delay_ms(100);
   print_wifi("AT+CWJAP=\"GPTT\",\"12345678999\"\r\n");
   delay_ms(100);
  print_wifi("AT+CIPMUX=0\r\n");
  delay_ms(100);
  //print_wifi("AT+CIPSERVER=1,8080\r\n");
  delay_ms(100);
  // print_wifi("AT+CIPSTART=\"TCP\",\"172.27.16.1\",8080\r\n");
  delay_ms(100);
  print_wifi("AT+CIPSTO=500\r\n");
  delay_ms(100);      
  print_wifi("AT+CIFSR\r\n");
}

/************************************************
 * name: wifi_client_send
 * function: 发送数据
 * input: data: 发送的数据
 *        len: 数据长度
 * output: void
 * notice: 需要配合串口使用
 ************************************************/
void wifi_client_send(uint16_t *data, uint8_t len)
{
    // Connect to the wifi network
    print_wifi("AT+CIPSTART=\"TCP\",\"192.168.66.28\", 8080\r\n"); // 连接TCP
    delay_ms(100);
    print_wifi("AT+CIPMODE=1\r\n");
    delay_ms(100);
    print_wifi("AT+CIPSEND\r\n");
    delay_ms(100);
    // print_wifi("AT+CIPSEND=0,%d\r\n", len); // 发送数据
    delay_ms(100);
    print_wifi("HELLO\r\n ");
    for(int i = 0; i < len; i++)
    {
        print_wifi("%u", data[i]); // 发送数据
    }
    print_wifi("\r\n"); // 发送结束符
    delay_ms(100);
    print_wifi("AT+CIPCLOSE\r\n");
}
/************************************************
 * name: wifi_AP_init
 * function: 初始化WIFI模块为AP模式(热点模式)
 * input: void
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_AP_init(void)
{
    // Initialize the wifi module
    print_wifi("AT\r\n"); // 检测设备是否在线
    delay_ms(100);
    print_wifi("AT+RST\r\n"); // 让Wifi模块重启的命令
    delay_ms(100);
    print_wifi("AT+GMR\r\n"); // 获取固件版本号
    delay_ms(100);
    print_wifi("AT+SYSMSG\r\n"); // 获取系统信息
    delay_ms(100);
    print_wifi("AT+CWMODE=2\r\n"); // 设置WIFI模式
    delay_ms(100);
    print_wifi("AT+CWSAP=\"ESP8266\",\"123456789\"\r\n"); // 设置WIFI
    delay_ms(100);
    print_wifi("AT+CIPMUX=1\r\n"); // 设置多连接
    delay_ms(100);
    print_wifi("AT+CIPSERVER=1,8080\r\n"); // 开启服务器
    delay_ms(100);
    print_wifi("AT+CIPSTO=500\r\n"); // 设置超时时间
    delay_ms(100);
    print_wifi("AT+UART=115200,8,1,0,0\r\n"); // 设置波特率
    delay_ms(100);

    // print_wifi(AT_RST); //让Wifi模块重启的命令
    // print_wifi(AT_WIFI_SET); //设置WIFI
    // print_wifi(AT_WIFI_SERVER); //开启服务器
}

/************************************************
 * name: wifi_SCAN
 * function: 扫描WIFI
 * input: void
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_SCAN(void)
{
    // Connect to the wifi network
    print_wifi(AT_WIFI_SCAN); // 扫描WIFI
}

/************************************************
 * name: wifi_connect
 * function: 连接WIFI
 * input: ssid: WIFI名称
 *        password: WIFI密码
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_connect(char *ssid, char *password)
{
    // Connect to the wifi network
    print_wifi("AT+CWJAP=\"%s\",\"%s\"\r\n", ssid, password); // 连接WIFI
}

/************************************************
 * name: wifi_TCP_connect
 * function: 连接TCP服务器
 * input: ip: 服务器IP地址
 *        port: 服务器端口
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_TCP_connect(char *ip, char *port)
{
    // Connect to the wifi network
    print_wifi("AT+CIPSTART=\"TCP\",\"%s\",%s\r\n", ip, port); // 连接TCP
}

/************************************************
 * name: wifi_TCP_send
 * function: 发送数据
 * input: data: 发送的数据
 *        len: 数据长度
 * output: void
 * notice: 需要配合串口使用
 ************************************************/

void wifi_send_data(unsigned char *data, unsigned int len)
{
    // Send data over wifi
    print_wifi("AT+CIPSEND=0,%d\r\n", len); // 发送数据
    // 可能需要延迟
    for (int i = 0; i < len; i++)
    {
        print_wifi("%u", data[i]); // 发送数据
    }
    print_wifi("\r\n"); // 发送结束符
}

