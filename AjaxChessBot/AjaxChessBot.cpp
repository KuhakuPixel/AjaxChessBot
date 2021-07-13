
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
#include <vector>


std::vector<std::string> allLeftClick = {};
POINT point;
bool leftClicking = false;
bool runProgram = false;


void DisplayCurrentMouseCoordinates() {
	point = Clicker::GetCurrentMouseCoordinates();
	std::string printedCoordinates = "{ " + std::to_string(point.x) + " , " + std::to_string(point.y) + " }";
	std::cout << printedCoordinates<<std::endl;
}
//Program main entry

int main()
{
	std::string command = "";
	std::cout << "Command :";
	std::cin >> command;
	if (command == "start") {
		runProgram = true;
	}
	//lets test on how to send a left click???
	while (runProgram) {
		
		if (GetKeyState(VK_LBUTTON) < 0) {
			
			leftClicking = true;
		}
		if (leftClicking) {
			
			Clicker::SendLeftClick(352,515);
		}

		if (GetKeyState(VK_RBUTTON) < 0) {
			runProgram = false;
		}
		

		//DisplayCurrentMouseCoordinates();
	}




}


