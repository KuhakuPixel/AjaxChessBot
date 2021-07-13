#include "CommandReader.h"
#include <iostream>
void  CommandReader::ProcessCommand(std::string command) {

	if (command=="help") {
		std::cout <<" \"Listen\" : Start Recording all location in the chess board," ;
		std::cout << " \"Save\" : Save all of the recorded position";
		std::cout << " \"Load\" : Load all of the recorded position";
	}
}