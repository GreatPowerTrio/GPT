#include "spi.h"

void C2C_Write_Bit(uint8_t dat) 
{	
  uint8_t i;
	C2C_CS_Clr();
	for(i = 0; i < 8; i ++)
	{			  
		C2C_SCLK_Clr();
		if(dat & 0x80)
		{
		   C2C_MOSI_Set();
		}
		else
		{
		   C2C_SCLK_Set();
		}
		C2C_SCLK_Set();
		dat <<= 1;
	}	
  C2C_CS_Set();	
}

void C2C_Write_Uint(uint32_t dat)
{
  uint8_t i;
  for(i = 0; i < 4; i++)
  {
    C2C_Write_Bit(dat & 0xFF);
    dat >>= 8;
  }
}


