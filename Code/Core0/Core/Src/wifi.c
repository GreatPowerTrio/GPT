#include "wifi.h"
#include "AT_cmdset.h"
#include "main.h"
#include "FreeRTOS.h"
#include "cmsis_os2.h"

extern uint8_t rec_data;
extern UART_HandleTypeDef huart2;
extern char print_buf[PRINT_BUF_SIZE];

#define SPRINTF_BUF_SIZE 20
/* �ɺ��� wifi_send_data( data ) ���ã�������ת��Ϊ�ַ��������� sprintf_buf �� */
char sprintf_buf[SPRINTF_BUF_SIZE];



char *esp8266_return_code[] = { "OK", "ready", "CONNECT", "SEND OK", "CLOSED"};
uint8_t  return_code_size[] = {    2,       5,         7,         7,        6};
bool           flag_table[] = {false,   false,     false,     false,    false};
uint8_t      flag_pointer[] = {    0,       0,         0,         0,        0};



/**
  * @brief  wifi�ڲ������ʱ����
  * @param  tick : ��ʱ�Ľ�����
  * @retval ��
  * @note   û�о�ȷ��ʱ���Դ�0��500��ѭ������Ϊһ������
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
  * @brief  ��ȡָ�����ش���ı�־λ
  * @param  code : ESP8266���ش����ö������
  * @retval ָ������ı�־λ
  */
bool wifi_get_flag(ESP8266_RETURN_CODE code)
{
    return flag_table[code];
}



/**
  * @brief  ���ָ�����ش���ı�־λ
  * @param  code : ESP8266���ش����ö������
  * @retval ��
  */
void wifi_clear_flag(ESP8266_RETURN_CODE code)
{
    flag_table[code] = false;
    flag_pointer[code] = 0;
}


/**
  * @brief  ����ָ�����ش���ı�־λ
  * @param  code : ESP8266���ش����ö������
  * @retval ��
  * @note   ����ֻ���һ�εı�־λ�����ú��ڴ����ж��в����ظ����
  */
void wifi_set_flag(ESP8266_RETURN_CODE code)
{
    flag_table[code] = true;
}




/**
  * @brief  �ڴ��ڽ����ж��У����ַ��ؼ��ָ����־λ
  * @param  code        ESP8266���ش����ö������
  * @param  rec_data    ���ڽ����жϽ��յ��ַ�
  * @retval ��
  * @note   �� stmf1xx.it.c �� HAL_UART_RxCpltCallback() �е���
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
  * @brief  �ȴ�esp8266����ָ������
  * @param  code  ESP8266���ش����ö������
  * @retval ��
  * @note   ��û�н��յ�ָ�����룬�ú������������ܹ�ʵ��ͬ����Ӧ
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
  * @brief  ����ΪSTATIONģʽ�����������豸��wifi
  * @retval ��
  */
void wifi_set_STATION(void)
{
    // ����ΪSTATIONģʽ
    print_wifi("AT+CWMODE=1\r\n");
    wifi_wait_code(OK);

    // ������Ч
    print_wifi("AT+RST\r\n");
    wifi_wait_code(READY);
    wifi_clear_flag(OK);

    // ��ѯ��ǰWIFI�б���Ҫ����ʱ��
    print_wifi("AT+CWLAP\r\n");
    wifi_wait_code(OK);
  
    // ����Ҫ���ӵ�wifi���ƺ�����
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
  * @brief  ����APģʽ�������ȵ��ź�
  * @retval ��
  */
void wifi_set_AP(void)
{
    // ����ΪAPģʽ
    print_wifi("AT+CWMODE=2\r\n");
    wifi_wait_code(OK);

    // ������Ч
    print_wifi("AT+RST\r\n");
    wifi_wait_code(READY);
    wifi_clear_flag(OK);

    // �鿴esp8266 IP���̶�λ192.168.4.1
    print_wifi("AT+CIFSR\r\n");
    wifi_wait_code(OK);

    print_wifi("AT+CIPMUX=1\r\n");
    wifi_wait_code(OK);
 
    print_wifi("AT+CIPSERVER=1,8080\r\n");
    wifi_wait_code(OK);

    // �����ȵ�����ƺ�����
    print_wifi("AT+CWSAP=\"GPT\",\"12345678\",1,3\r\n");
    wifi_wait_code(OK);

    // ready ��־λ���һ�μ���
    wifi_set_flag(READY);
}


/**
  * @brief  ���͵�������
  * @param  data Ҫ���͵�16λ����
  * @retval ��
  */
void wifi_send_data(uint16_t data)
{
    // ���յ��Ͽ���־���������ӱ�־���ж�
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
