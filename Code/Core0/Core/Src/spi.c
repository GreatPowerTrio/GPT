#include "spi.h"



void C2C_Write_Uint16(uint16_t dat) 
{	
	// Once send z bytes.
  uint8_t i;
	C2C_CS_Clr();
	for(int j = 0; j < 10; j++);
	for(i = 0; i < 16; i++)
	{			  
		if(dat & 0x8000)
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





