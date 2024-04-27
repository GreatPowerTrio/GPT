#include "wifi.h"
#include "AT_cmdset.h"
#include "main.h"

extern UART_HandleTypeDef huart2;
// ��ʱms
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
 * function: ��ʼ��WIFIģ��Ϊ������ģʽ
 * input: void
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/
extern char u_buf[100];
void wifi_server_init(void)
{
    // Initialize the wifi module
    print_wifi(AT_Test);             // ����豸�Ƿ�����
    print_wifi(AT_RST);              // ��Wifiģ������������
    print_wifi(AT_GMR);              // ��ȡ�̼��汾��
    print_wifi(AT_SYSMSG);           // ��ȡϵͳ��Ϣ
    print_wifi(AT_WIFI_MODE_SOFTAP); // ����WIFIģʽ
    print_wifi(AT_RST);              // ��Wifiģ������������
    print_wifi(AT_WIFI_SET);         // ����WIFI
    print_wifi(AT_WIFI_SERVER);      // ����������
}

/************************************************
 * name: wifi_client_init
 * function: ��ʼ��WIFIģ��Ϊ�ͻ���ģʽ
 * input: void
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
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
 * function: ��������
 * input: data: ���͵�����
 *        len: ���ݳ���
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/
void wifi_client_send(uint16_t *data, uint8_t len)
{
    // Connect to the wifi network
    print_wifi("AT+CIPSTART=\"TCP\",\"192.168.66.28\", 8080\r\n"); // ����TCP
    delay_ms(100);
    print_wifi("AT+CIPMODE=1\r\n");
    delay_ms(100);
    print_wifi("AT+CIPSEND\r\n");
    delay_ms(100);
    // print_wifi("AT+CIPSEND=0,%d\r\n", len); // ��������
    delay_ms(100);
    print_wifi("HELLO\r\n ");
    for(int i = 0; i < len; i++)
    {
        print_wifi("%u", data[i]); // ��������
    }
    print_wifi("\r\n"); // ���ͽ�����
    delay_ms(100);
    print_wifi("AT+CIPCLOSE\r\n");
}
/************************************************
 * name: wifi_AP_init
 * function: ��ʼ��WIFIģ��ΪAPģʽ(�ȵ�ģʽ)
 * input: void
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/

void wifi_AP_init(void)
{
    // Initialize the wifi module
    print_wifi("AT\r\n"); // ����豸�Ƿ�����
    delay_ms(100);
    print_wifi("AT+RST\r\n"); // ��Wifiģ������������
    delay_ms(100);
    print_wifi("AT+GMR\r\n"); // ��ȡ�̼��汾��
    delay_ms(100);
    print_wifi("AT+SYSMSG\r\n"); // ��ȡϵͳ��Ϣ
    delay_ms(100);
    print_wifi("AT+CWMODE=2\r\n"); // ����WIFIģʽ
    delay_ms(100);
    print_wifi("AT+CWSAP=\"ESP8266\",\"123456789\"\r\n"); // ����WIFI
    delay_ms(100);
    print_wifi("AT+CIPMUX=1\r\n"); // ���ö�����
    delay_ms(100);
    print_wifi("AT+CIPSERVER=1,8080\r\n"); // ����������
    delay_ms(100);
    print_wifi("AT+CIPSTO=500\r\n"); // ���ó�ʱʱ��
    delay_ms(100);
    print_wifi("AT+UART=115200,8,1,0,0\r\n"); // ���ò�����
    delay_ms(100);

    // print_wifi(AT_RST); //��Wifiģ������������
    // print_wifi(AT_WIFI_SET); //����WIFI
    // print_wifi(AT_WIFI_SERVER); //����������
}

/************************************************
 * name: wifi_SCAN
 * function: ɨ��WIFI
 * input: void
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/

void wifi_SCAN(void)
{
    // Connect to the wifi network
    print_wifi(AT_WIFI_SCAN); // ɨ��WIFI
}

/************************************************
 * name: wifi_connect
 * function: ����WIFI
 * input: ssid: WIFI����
 *        password: WIFI����
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/

void wifi_connect(char *ssid, char *password)
{
    // Connect to the wifi network
    print_wifi("AT+CWJAP=\"%s\",\"%s\"\r\n", ssid, password); // ����WIFI
}

/************************************************
 * name: wifi_TCP_connect
 * function: ����TCP������
 * input: ip: ������IP��ַ
 *        port: �������˿�
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/

void wifi_TCP_connect(char *ip, char *port)
{
    // Connect to the wifi network
    print_wifi("AT+CIPSTART=\"TCP\",\"%s\",%s\r\n", ip, port); // ����TCP
}

/************************************************
 * name: wifi_TCP_send
 * function: ��������
 * input: data: ���͵�����
 *        len: ���ݳ���
 * output: void
 * notice: ��Ҫ��ϴ���ʹ��
 ************************************************/

void wifi_send_data(unsigned char *data, unsigned int len)
{
    // Send data over wifi
    print_wifi("AT+CIPSEND=0,%d\r\n", len); // ��������
    // ������Ҫ�ӳ�
    for (int i = 0; i < len; i++)
    {
        print_wifi("%u", data[i]); // ��������
    }
    print_wifi("\r\n"); // ���ͽ�����
}

