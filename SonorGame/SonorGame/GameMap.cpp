#include "GameMap.h"

GameMap::GameMap()
{
	std::string str;
	is_game = true;
	disabeRandom = file_is_not_open = is_win = false;
	
	for (size_t i = 0; i < 23; ++i)
	{
		str = "#";
		for (size_t j = 1; j < 16; ++j)
		{

			str += (i == 0 || i == 22 ? '#' : ' ');
		}
		str += ('#');
		_map.push_back(str);
	}
	_map[0][0] = '1';
	_map[2][0] = '2';
	_map[4][0] = '3';
	scores = 0;
}

void GameMap::Reload()
{
	scores = 0;
}

Person<char>& GameMap::get_enemy()
{
	return this->enemy;
}

Person<char>& GameMap::get_random_enemy()
{
	return this->random_enemy;
}

Person<char>& GameMap::get_user()
{
	return this->user;
}

bool GameMap::get_file_is_not_open()
{
	return file_is_not_open;
}

bool GameMap::get_is_win()
{
	return is_win;
}

bool GameMap::get_is_game()
{
	return is_game;
}

const bool GameMap::get_is_disabeRandom()const
{
	return disabeRandom;
}

void GameMap::move_random()
{
	int direction = rand() % 4;
	switch (direction)
	{
	case 0: random_enemy.i - 1 < 1 ? ++random_enemy.i : --random_enemy.i; break;

	case 1: random_enemy.i + 1 > WIGTH ? --random_enemy.i : ++random_enemy.i; break;

	case 2: random_enemy.j + 1 > HEIGHT ? --random_enemy.j : ++random_enemy.j; break;

	case 3: random_enemy.j - 1 < 1 ? ++random_enemy.j : --random_enemy.j; break;
	}
}

void GameMap::disable_or_active_randome_enemy()
{
	disabeRandom = !disabeRandom;
}

void GameMap::move(int key, bool is_enemy_move)
{
	if (!is_game)
	{
		return;
	}
	if (is_enemy_move)
	{
		switch (key)
		{
		case W: enemy.i - 1 < 1 ? enemy.i = 1 : --enemy.i; break;

		case A: enemy.j - 1 < 1 ? enemy.j = 1 : --enemy.j; break;

		case D: enemy.j + 1 > WIGTH - 1 ? enemy.j = WIGTH - 1 : ++enemy.j; break;

		case S: enemy.i + 1 > HEIGHT - 1 ? enemy.i = HEIGHT - 1 : ++enemy.i; break;
		}
	}
	else
	{
		switch (key)
		{
		case W: user.i - 1 == 0 ? is_win = true : --user.i; break;

		case A: user.j - 1 < 1 ? user.j = 1 : --user.j; break;

		case D: user.j + 1 > WIGTH - 1 ? user.j = WIGTH - 1 : ++user.j; break;

		case S: user.i + 1 == HEIGHT - 1 ? user.i = HEIGHT - 1 : ++user.i; break;
		}

		if (user.i == 0 && scores < 6)
		{
			scores += 6;
		}
		else if(user.i == 2 && scores < 3)
		{
			scores += 3;
		}
		else if (user.i == 4 && scores < 1)
		{
			scores += 1;
		}

	}
	if (is_win || (user.i == enemy.i && user.j == enemy.j 
			||(!disabeRandom && user.i == random_enemy.i && user.j == random_enemy.j) ))
	{
		SaveData("data.txt");
		is_game = false;
	}
	
}

void GameMap::SaveData(string file_name)
{
	ofstream fs;
	fs.open(file_name, ios::app);

	if (fs.is_open())
	{
		fs << scores << '\n';
	}
	else
	{
		file_is_not_open = true;
	}

	fs.close();
}

int GameMap::get_scores()
{
	return scores;
}


ostream& operator<<(ostream& stream, const GameMap& counter)
{
	for (size_t i = 0; i < counter._map.size(); ++i)
	{
		stream << counter._map[i][0];
		for (size_t j = 1; j < counter._map[i].size() - 1; ++j)
		{
			if (counter.user.i == i && counter.user.j == j)
			{
				stream << setw(3) << counter.user.get_designation();
			}
			else if (counter.enemy.i == i && counter.enemy.j == j)
			{
				stream << setw(3) << counter.enemy.get_designation();
			}
			else if (!counter.get_is_disabeRandom() && counter.random_enemy.i == i && counter.random_enemy.j == j)
			{
				stream << setw(3) << counter.random_enemy.get_designation();
			}
			else
			{
				stream << setw(3) << counter._map[i][j];
			}
		}
		stream << counter._map[i][counter._map[i].size() - 1] << '\n';
	}

	return stream;
}
