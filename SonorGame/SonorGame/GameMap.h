#pragma once
#include<iostream>
#include<fstream>
#include<string>
#include<vector>
#include <iomanip>
#include "Person.h"

#define WIGTH 16
#define HEIGHT 22

#define Q 113
#define A 97
#define S 115
#define D 100
#define W 119
#define RELOAD 114

#define TIMER_TICK 10
#define SHOW_MUST_GO_ON 1

using namespace std;

class GameMap
{
public:
	GameMap();
	void Reload();
	Person<char>& get_enemy();
	Person<char>& get_random_enemy();
	Person<char>& get_user();
	bool get_file_is_not_open();
	bool get_is_win();
	bool get_is_game();
	const bool get_is_disabeRandom()const;
	void move_random();
	void disable_or_active_randome_enemy();
	void move(int key,bool is_enemy_move);
	void SaveData(string file_name);
	int get_scores();
	friend ostream& operator <<(ostream& stream, const GameMap& counter);

private:
	int scores;
	bool is_game,is_win,file_is_not_open,disabeRandom;
	std::vector<std::string> _map;
	Person<char> enemy = Person<char>('0', 1, WIGTH / 2);
	Person<char> random_enemy = Person<char>('*', 1, WIGTH / 2 + 1);
	Person<char> user = Person<char>('+', HEIGHT - 1, WIGTH / 2);
};

