#include "Clicker.h"
#include <Windows.h>
POINT Clicker::GetCurrentMouseCoordinates() {
	POINT point;
	
	GetCursorPos(&point);
	return point;
}
void Clicker::SendLeftClick(int x, int y) {
	
	
	INPUT Inputs[3] = { 0 };
	
	Inputs[0].type = INPUT_MOUSE;
	Inputs[0].mi.dx = x; // desired X coordinate
	Inputs[0].mi.dy = y; // desired Y coordinate
	Inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;

	Inputs[1].type = INPUT_MOUSE;
	Inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

	Inputs[2].type = INPUT_MOUSE;
	Inputs[2].mi.dwFlags = MOUSEEVENTF_LEFTUP;

	SendInput(3, Inputs, sizeof(INPUT));
}