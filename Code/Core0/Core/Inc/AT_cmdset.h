# ifndef _AT_CMDSET_H_
# define _AT_CMDSET_H_

/****************************************
参考AT指令集
https://github.com/CQUPTLei/ESP8266
****************************************/

//基础
#define AT_Test "AT\r\n" //测试AT指令
#define AT_RST "AT+RST\r\n" //重启模块
#define AT_GMR "AT+GMR\r\n" //获取固件版本号
#define AT_SYSMSG "AT+SYSMSG\r\n" //获取系统信息

//WIFI
#define AT_WIFI_INIT "AT+CWINT\r\n" //初始化WIFI
#define AT_WIFI_MODE "AT+CWMODE?\r\n" //查询WIFI模式
#define AT_WIFI_CONNECT "AT+CWJAP=\"SSID\",\"PASSWORD\"\r\n" //连接WIFI
#define AT_WIFI_MODE_STATION "AT+CWMODE=1\r\n" //设置WIFI模式 station
#define AT_WIFI_MODE_SOFTAP "AT+CWMODE=2\r\n" //设置WIFI模式 softap
#define AT_WIFI_MODE_SOFTAP_STATION "AT+CWMODE=3\r\n" //设置WIFI模式 softap+station
#define AT_WIFI_STATE "AT+CWSTATE\r\n" //查询WIFI状态
#define AT_WIFI_SCAN "AT+CWLAP\r\n" //扫描WIFI
#define AT_WIFI_DISCONNECT "AT+CWQAP\r\n" //断开WIFI
#define AT_WIFI_IP "AT+CIPSTA?\r\n" //查询IP地址
#define AT_WIFI_TRANSMISSION_MODE "AT+CIPMODE=1\r\n" //设置透传模式
#define AT_WIFI_SERVER "AT+CIPSERVER=1,8080\r\n" //开启服务器
#define AT_WIFI_SET "AT+CWSAP=\"ESP8266\",\"123456789\",1,4\r\n" //设置WIFI

# endif