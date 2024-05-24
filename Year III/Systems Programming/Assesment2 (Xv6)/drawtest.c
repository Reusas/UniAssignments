#include "types.h"
#include "user.h"



int main(int argc, char* argv[])
{
    setvideomode(0x13);



    setpencolour(16,62,0,0);    // Create red color
    setpencolour(17,0,63,0);    // Create green color
    int pid = fork();

    if(pid == 0)
    {
       // printf(0,"Starting child proccess\n");
        int hdc = beginpaint(0);
        
        selectpen(hdc,16);
        moveto(hdc, 100, 50);
        lineto(hdc, 200, 50);
        lineto(hdc, 200, 150);
        lineto(hdc, 100, 150);
        lineto(hdc, 100, 50);
        

        // Draw filled rectangle somewhere in the middle of the red rectangle
        struct rect myRect;

        myRect.top = 90;        
        myRect.bottom = 100;
        myRect.left = 150;
        myRect.right = 160;
        fillrect(hdc, &myRect);

        endpaint(hdc);
        sleep(1);
    }
    else if (pid > 0)
    {
        //printf(0,"Main process running\n");
        int hdc = beginpaint(0);
        //
        selectpen(hdc,17);
        moveto(hdc, 50, 0);
        lineto(hdc, 150, 0);
        lineto(hdc, 150, 100);
        lineto(hdc, 50, 100);
        lineto(hdc, 50, 0);


        // Draw filled rectangle somewhere in the middle of the green rectangle
        struct rect myRect;

        myRect.top = 40;        
        myRect.bottom = 50;
        myRect.left = 100;
        myRect.right = 110;
        fillrect(hdc, &myRect);
        
    
    

        sleep(1);
        endpaint(hdc);

        wait();
        

    }
    else
    {
        printf(1, "Fork error");
    }



    getch();
    setvideomode(0x03);
    exit();

}