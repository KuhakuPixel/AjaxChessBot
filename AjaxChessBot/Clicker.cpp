#include "Clicker.h"
#include <Windows.h>
#include <iostream>
#include <string>
POINT Clicker::GetCurrentMouseCoordinates() {
	POINT point;
	
	GetCursorPos(&point);
	return point;
}
void Clicker::SendLeftClick(int x, int y) {
	
	
	INPUT Inputs[3] = { 0 };
	
	Inputs[0].type = INPUT_MOUSE;
	
	Inputs[0].mi.dx = x; // desired X coordinate
	Inputs[0].mi.dy = y; 
	Inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;

	Inputs[1].type = INPUT_MOUSE;
	Inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

	Inputs[2].type = INPUT_MOUSE;
	Inputs[2].mi.dwFlags = MOUSEEVENTF_LEFTUP;

	SendInput(3, Inputs, sizeof(INPUT));
	std::cout << "Sending left click at :"<<std::to_string(x)<<","<<std::to_string(y)<<std::endl;
}