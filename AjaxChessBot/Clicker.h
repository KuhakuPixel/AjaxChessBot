#pragma once
#include<Windows.h>


class Clicker
{
public:

	static POINT GetCurrentMouseCoordinates();
	static void SendLeftClick(int x, int y);
private:

};

