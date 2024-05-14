#include "spi.h"

void C2C_Init(void)
{
	C2C_CS_Set();

	C2C_SCLK_Set();

	C2C_MOSI_Clr();
	C2C_MISO_Clr();
}

void C2C_Write_Byte(uint8_t dat) 
{	
  uint8_t i;
	C2C_CS_Clr();
	for(int j = 0; j < 10; j++);
	for(i = 0; i < 8; i++)
	{			  
		if(dat & 0x80)
		{
		  C2C_MOSI_Set();
		}
		else
		{
		  C2C_MOSI_Clr();
		}

		C2C_SCLK_Clr();
		for(int j = 0; j < 20; j++);
		C2C_SCLK_Set();
		dat <<= 1;
	}	
  C2C_CS_Set();	
}





