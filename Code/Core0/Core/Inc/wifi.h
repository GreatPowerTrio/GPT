#ifndef _WIFI_H_
#define _WIFI_H_



#include "main.h"

/* esp8266返回代码的枚举类型，对应相应数组的下标。若要添加代码，在末尾添加即可 */
typedef enum
{
  OK         = 0,
  READY         ,
  CONNECT       ,
  SEND_OK       ,
  CLOSED        ,
} ESP8266_RETURN_CODE;


void wifi_delay(uint16_t tick);

void wifi_check_flag(ESP8266_RETURN_CODE code, char rec_data);
void wifi_clear_flag(ESP8266_RETURN_CODE code);
void wifi_set_flag(ESP8266_RETURN_CODE code);
bool wifi_get_flag(ESP8266_RETURN_CODE code);

void wifi_wait_code(ESP8266_RETURN_CODE code);
void wifi_wait_code_ex(ESP8266_RETURN_CODE code);

void wifi_set_STATION(void);
void wifi_set_AP(void);

void wifi_send_data(uint16_t data);

void wifi_send_package(uint16_t *buf);

void wifi_send(uint16_t dat);

#endif
