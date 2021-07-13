
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
#include <vector>


std::vector<std::string> allLeftClick = {};
//Program main entry
int main()
{
	while (true) {
		if (GetKeyState(VK_LBUTTON) < 0) {
			POINT point = Clicker::GetCurrentMouseCoordinates();
			allLeftClick.push_back("{ " + std::to_string(point.x) + " , " + std::to_string(point.y) + " }");
		
		}


	}




}


