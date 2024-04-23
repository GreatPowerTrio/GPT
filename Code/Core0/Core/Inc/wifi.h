#ifndef _WIFI_H_
#define _WIFI_H_
#include "main.h"
void wifi_server_init(void);
void wifi_client_init(void);
void wifi_SCAN(void);
void wifi_connect(char* ssid, char* password);
void wifi_TCP_connect(char* ip, char* port);
void wifi_send_data(unsigned char *data, unsigned int len);                                     
void wifi_AP_init(void);
void delay_ms(uint32_t ms);
void wifi_client_send(uint16_t *data, uint8_t len);







#endif
