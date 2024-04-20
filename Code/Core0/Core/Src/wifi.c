#include "wifi.h"
#include "AT_cmdset.h"
#include "main.h"

/************************************************
 * name: wifi_server_init
 * function: 初始化WIFI模块为服务器模式
 * input: void
 * output: void
 * notice: 需要配合串口使用
************************************************/

void wifi_server_init(void)
{
    // Initialize the wifi module
    printf(AT_Test); //检测设备是否在线
    printf(AT_RST); //让Wifi模块重启的命令
    printf(AT_GMR); //获取固件版本号
    printf(AT_SYSMSG); //获取系统信息
    printf(AT_WIFI_MODE_SOFTAP); //设置WIFI模式
    printf(AT_RST); //让Wifi模块重启的命令
    printf(AT_WIFI_SET); //设置WIFI
    printf(AT_WIFI_SERVER); //开启服务器
    
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
    printf(AT_Test); //检测设备是否在线
    printf(AT_RST); //让Wifi模块重启的命令
    printf(AT_GMR); //获取固件版本号
    printf(AT_SYSMSG); //获取系统信息
    printf(AT_WIFI_MODE_STATION); //设置WIFI模式
    printf(AT_RST); //让Wifi模块重启的命令

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
    printf(AT_WIFI_SCAN); //扫描WIFI
}

/************************************************
 * name: wifi_connect
 * function: 连接WIFI
 * input: ssid: WIFI名称
 *        password: WIFI密码
 * output: void
 * notice: 需要配合串口使用
************************************************/

void wifi_connect(char* ssid, char* password)
{
    // Connect to the wifi network
    printf("AT+CWJAP=\"%s\",\"%s\"\r\n",ssid,password); //连接WIFI
}

/************************************************
 * name: wifi_TCP_connect
 * function: 连接TCP服务器
 * input: ip: 服务器IP地址
 *        port: 服务器端口
 * output: void
 * notice: 需要配合串口使用
************************************************/

void wifi_TCP_connect(char* ip, char* port)
{
    // Connect to the wifi network
    printf("AT+CIPSTART=\"TCP\",\"%s\",%s\r\n",ip,port); //连接TCP
}

/************************************************
 * name: wifi_TCP_send
 * function: 发送数据
 * input: data: 发送的数据
 *        len: 数据长度
 * output: void
 * notice: 需要配合串口使用
************************************************/

void wifi_send_data(uint8_t *data, uint16_t len)
{
    // Send data over wifi
    printf("AT+CIPSEND=0,%d\r\n",len); //发送数据
    //可能需要延迟
    for(int i=0; i<len; i++)
    {
        putchar(data[i]); //发送数据
    }
    putchar(26); //发送结束符
}







