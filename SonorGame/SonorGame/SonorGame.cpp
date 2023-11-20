#include <iostream>
#include <ctime>
#include <conio.h>
#include "GameMap.h"


using namespace std;

using ulong = unsigned long;

int main()
{
	setlocale(0, "ru");
	srand(time(NULL));
link:
	GameMap gameMap;
	ulong timer = 0;
	int key = 0;
	bool is_enemy_move = false;

	while (SHOW_MUST_GO_ON)
	{
		if (_kbhit())
		{
			key = _getch();
			if (key  == Q)
			{
				gameMap.disable_or_active_randome_enemy();
			}
			else if (key == A || key == D || key == W || key == S)
			{
				is_enemy_move = !is_enemy_move;
				if (!gameMap.get_is_disabeRandom())
				{
					gameMap.move_random();
				}
				gameMap.move(key, is_enemy_move);
			}
			else if (key == RELOAD)
			{
				goto link;
			}
		}

		if (timer % TIMER_TICK == 0)
		{
			system("cls");
			if (gameMap.get_is_win())
			{
				cout << "Убгающий победил\n";
				if (gameMap.get_file_is_not_open())
				{
					cout << "Ошибка окрытия файла!\n";
				}
			}
			else if (!gameMap.get_is_game())
			{
				cout << "Догоняющий победил!\n";
				if (gameMap.get_file_is_not_open())
				{
					cout << "Ошибка окрытия файла!\n";
				}
			}


			cout << (is_enemy_move ? "Ход убегающего" : "Ход догоняющего")
				<< "\nA - для хода налево\n"
				<< "S - для хода вниз\n"
				<< "D - для хода направо\n"
				<< "r - для перезапуска\n"
				<< "0 - догоняющий\n"
				<< "* - рандомно-ходящий догоняющий\n"
				<< "+ - убегающий\n"
				<< "Q - для того, чтобы деактивировать/активировать рандомного противника\n"
				<< "scores = " << gameMap.get_scores() << '\n'
				<< gameMap;



			key = timer = 0;
		}
		else
		{
			++timer;
		}
	}


	return 0;
}

