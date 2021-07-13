
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
int main()
{
    while (true) {
        POINT point = Clicker::GetCurrentMouseCoordinates();
        std::cout <<"Current position of mouse: { "+std::to_string(point.x)+" , "+std::to_string(point.y)+" }"<<std::endl;
    }
    
}


