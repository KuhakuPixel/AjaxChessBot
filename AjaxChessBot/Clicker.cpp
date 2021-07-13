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
	
	
	INPUT inputs[2] = { 0 };
	
	//Inputs[0].type = INPUT_MOUSE;
	
	//Inputs[0].mi.dx = x;
	//Inputs[0].mi.dy = y; 
	//Inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;

	inputs[0].type = INPUT_MOUSE;
	inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
	inputs[0].mi.dx = x;
	inputs[0].mi.dy = y; 

	inputs[1].type = INPUT_MOUSE;
	inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;
	inputs[1].mi.dx = x;
	inputs[1].mi.dy = y;
	SendInput(2, inputs, sizeof(INPUT));
	
	std::cout << "Sending left click at :"<<std::to_string(x)<<","<<std::to_string(y)<<std::endl;
}