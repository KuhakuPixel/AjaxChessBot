
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
#include <vector>


std::vector<std::string> allLeftClick = {};
POINT point;
bool leftClicking = false;
//Program main entry
int main()
{
	//lets test on how to send a left click???
	while (true) {
		if (GetKeyState(VK_LBUTTON) < 0) {
			point = Clicker::GetCurrentMouseCoordinates();
			allLeftClick.push_back("{ " + std::to_string(point.x) + " , " + std::to_string(point.y) + " }");
			leftClicking = true;
		}
		if (leftClicking) {
			Clicker::SendLeftClick(point.x,point.y);
		}

	}




}


