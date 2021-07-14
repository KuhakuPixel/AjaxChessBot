
#include <iostream>
#include <Windows.h>
#include "Clicker.h"
#include <string>
#include <vector>


std::vector<std::string> allLeftClick = {};



bool runProgram = true;
POINT registeredCoordinate;
void DisplayCurrentMouseCoordinates() {
	POINT point = Clicker::GetCurrentMouseCoordinates();
	std::string printedCoordinates = "{ " + std::to_string(point.x) + " , " + std::to_string(point.y) + " }";
	std::cout << printedCoordinates << std::endl;
}
//Program main entry

int main()
{
	std::cout << "Run this program as administrator or else it will not work" << std::endl;
	
	//lets test on how to send a left click???
	while (runProgram) {
		
		std::string command = "";
		std::cout << "Command :";
		std::cin >> command;
		if (command == "start") {
			if (registeredCoordinate.x ==NULL&& registeredCoordinate.y == NULL) {
				std::cout << "no coordinate has been register , Use \"registerPos\" To register coordinates  " << std::endl;
			}
			else {
				bool runClicker = true;
				while (runClicker) {


					Clicker::SendLeftClick(707, 367);


					if (GetKeyState(VK_RBUTTON) < 0) {
						runClicker = false;
					}


					//DisplayCurrentMouseCoordinates();
				}
			}
			
		}

		else if (command == "registerPos") {
			std::cout << "click left to register" << std::endl;
			if (GetKeyState(VK_LBUTTON) < 0) {
				registeredCoordinate = Clicker::GetCurrentMouseCoordinates();
				std::cout << "coordinate sucsessfully registered at : "<<registeredCoordinate.x<<","<<registeredCoordinate.y << std::endl;

			}
		}
		
	}
	




}


