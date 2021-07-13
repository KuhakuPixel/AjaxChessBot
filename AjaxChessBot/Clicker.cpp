#include "Clicker.h"
#include <Windows.h>
POINT Clicker::GetCurrentMouseCoordinates() {
	POINT point;
	
	GetCursorPos(&point);
	return point;
}