
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
#include <vector>


std::vector<std::string> allLeftClick = {};
POINT point;


bool runProgram = true;

void DisplayCurrentMouseCoordinates() {
	point = Clicker::GetCurrentMouseCoordinates();
	std::string printedCoordinates = "{ " + std::to_string(point.x) + " , " + std::to_string(point.y) + " }";
	std::cout << printedCoordinates << std::endl;
}
//Program main entry

int main()
{
	std::cout << "Run this program as administrator or else it will not work" << std::endl;
	
	//lets test on how to send a left click???
	while (runProgram) {
		bool runClicker = false;
		std::string command = "";
		std::cout << "Command :";
		std::cin >> command;
		if (command == "start") {
			runClicker = true;
		}
		while (runClicker) {


			Clicker::SendLeftClick(355, 440);


			if (GetKeyState(VK_RBUTTON) < 0) {
				runClicker = false;
			}


			//DisplayCurrentMouseCoordinates();
		}
	}
	




}


