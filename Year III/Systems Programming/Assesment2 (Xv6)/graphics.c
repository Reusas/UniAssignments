#include "types.h"
#include "defs.h"
#include "memlayout.h"
#include "x86.h"
#define MAX_HANDLES 64		// Max proccesses in param.h is 64 aswell

int handleToReturn;

int startXPos = 0;
int startYPos = 0;

int penIndex = 15;


struct handle
{
	int active;			// 1 means handle is being used 0 means its released
	int xPos;
	int yPos;
	int penColor;
};

struct handle currentHandle;
struct handle handles[MAX_HANDLES];

int handleLength = sizeof(handles) / sizeof(struct handle);	// Calculate lenght of array



void clear320x200x256()
{

    for(int x=0; x<320; x++ )
    {
		
        for(int y=0; y<200; y++)
        {
			
            int *address = (int *)P2V(0xA0000 + 320 * y + x);
			*address =0x00;		// Convert memory address of pixel positions to virtual memory and set the bytes to 00 which is black
        }
    }
}

int sys_setpixel(void)
{
	
	int n;
	int xPos;
	int yPos;

	if (argint(0, &n) < 0)
	{
        return -1;			//Return -1 if no arguments were passed
    }

	argint(1, &xPos);
	argint(2, &yPos);
	
   
    int *address = (int *)P2V(0xA0000 + 320 * yPos + xPos);				// Convert memory address of pixel positions to virtual memory and set the bytes to 0F which is white
	*address =penIndex;
    return 0;


    return 0;
}

int sys_moveto(void)
{

	int hdc;


	argint(0, &hdc);
	argint(1, &handles[hdc].xPos);
	argint(2, &handles[hdc].yPos);

	//cprintf("Moving X to %d Y to %d\n",handles[hdc].xPos,handles[hdc].yPos );

	// Clip values to prevent them from going off screen
	if(handles[hdc].xPos> 319)
	{
		handles[hdc].xPos= 319;
	}
	if(handles[hdc].yPos > 199)
	{
		handles[hdc].yPos = 199;
	}

	

	return 0;
}


int sys_lineto(void)
{
	
	//cprintf("Drawing from %dx %dy\n", startXPos, startYPos);
	int endXPos;
	int endYPos;
	int hdc;




	argint(0, &hdc);
	argint(1, &endXPos);
	argint(2, &endYPos);

	// Clip values

    if (endXPos > 319)
    {
        endXPos = 319;
    }
    if (endYPos >= 199)
    {
        endYPos = 199;
    }


	//cprintf("Drawing to %dx %dy\n", endXPos, endYPos);
	
	int dx = endXPos - handles[hdc].xPos;
	int dy = endYPos - handles[hdc].yPos;

	// handle slopes.

	int sx = (dx>0) ? 1 : (dx< 0) ? -1 : 0;
	int sy = (dy > 0) ? 1 : (dy < 0) ? -1 : 0;

	dx = (dx < 0) ? -dx : dx;
    dy = (dy < 0) ? -dy : dy;

	// calculate 'error' value that will be used for drawing line

	int error = dx - dy;
	int error2;




	//cprintf("DX:%d DY:%d SX:%d SY:%d ERROR:%d \n",dx,dy,sx,sy,error);

	while(1)
	{
		uchar *memory_address = (uchar *)P2V(0xA0000 + 320 * handles[hdc].yPos+ handles[hdc].xPos );
        *memory_address = handles[hdc].penColor;
		//cprintf("HDC:%d, Drawing %dx and %dy\n",hdc, handles[hdc].xPos , handles[hdc].yPos);

		if (handles[hdc].xPos == endXPos && handles[hdc].yPos == endYPos)
		{

			cprintf("HDC %d finished drawing line\n", hdc);
			break;
		}

		error2 = 2 * error;
		
		if(error2 > -dy)
		{
			
			error -= dy;
			handles[hdc].xPos +=sx;
			
		}

		if (error2 < dx)
		{
			
			error +=dx;
			handles[hdc].yPos +=sy;
		}

		
	}

	return 0;
}  


int sys_setpencolour(void)
{
	int index;
	int r,g,b;

	argint(0,&index);
	argint(1,&r);
	argint(2,&g);
	argint(3,&b);

	// Error case
	if(index < 16 || index > 255)
	{
		return -1;
	}
	// Clip rgb values
	if (r>63)
	{
		r = 63;
	}
	if(g>63)
	{
		g = 63;
	}
	if(b>63)
	{
		b = 63;
	}

	outb(0x3C8, index);
	outb(0x3C9, r);
	outb(0x3C9, g);
	outb(0x3C9, b);





	//cprintf("I:%d R:%d G%d B%d", index, r,g,b);





	return 0;
}

int sys_selectpen(void)
{
	int hdc;
	int index;
	argint(0, &hdc);
	argint(1,&index);
	int oldPen = handles[hdc].penColor = index;		// Save index of old pen
	




	if(index < 0 || index > 255)
	{
		return -1;
	}

	handles[hdc].penColor = index;
	//penIndex = index;		// Set penIndex to the passed value

	

	return oldPen;
}

int sys_fillrect(void)
{
	// Create struct rect
	int hdc;
	struct rect
	{
		int top;
		int left;
		int bottom;
		int right;
	};

	struct rect *myRect;

	// Pass in pointer to the created struct
	argint(0,&hdc);
	argptr(1, (void*)&myRect, sizeof(myRect));

	// Retrieve rect positions. This basically retrieves two point x1y1 and x2y2
	int y = myRect->top;
	int y2 = myRect->bottom;
	int x = myRect->left;
	int x2 = myRect->right;



	int currentX = x;
	int currentY = y;
	while(1)
	{
		uchar *memory_address = (uchar *)P2V(0xA0000 + 320 * currentY + currentX);
    	*memory_address = handles[hdc].penColor;

		currentX++;

		// Check if the pixel has reached the right side of the rectangle
		if(currentX > x2)
		{
			// If it has reset the X and increase the line to draw the next "line" of the rectangle.
			currentX = x;
			currentY++;
		}
		// Once the y reaches the bottom position of the rectangle the drawing is done.
		if(currentY > y2)
		{
			break;
		}
	}

	

	return 0;
}
	
int sys_beginpaint(void)
{


	int i = 0;

	while(i<handleLength)		// Loop through every handle
	{
		
		if(handles[i].active== 0)		// If its not active then set it be active and return its index.
		{
			handles[i].active = 1;		
			handles[i].penColor = 15;		// Default color should be white
			cprintf("Handle id:%d\n", i);
			return i;
			break;
		}
		i++;
	}

	return -1;			// If no handle is available -1 is returned
	

	

}

int sys_endpaint(void)
{
	int hdc;


	argint(0,&hdc);
	if(hdc <0 || hdc > MAX_HANDLES)
	{
		return -1; // HDC out of range
	}

	handles[hdc].active = 0;
	handles[hdc].xPos = 0;
	handles[hdc].yPos = 0;
	handles[hdc].penColor = 15;
	return 0;
}


