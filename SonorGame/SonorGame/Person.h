#pragma once
#include <iostream>
#include <ctime>

using namespace std;

template <class T>
class Person
{
public:
	Person(T designation, int i, int j);
	T get_designation() const;
	friend ostream& operator <<(ostream& stream, const Person<T>& person);
	Person<T>& operator ++();
	Person<T>& operator --();
	int i, j;
private:
	T designation;
};

template<class T>
inline Person<T>::Person(T designation, int i, int j)
{
	this->i = i;
	this->j = j;
	this->designation = designation;
}

template<class T>
inline T Person<T>::get_designation() const
{
	return this->designation;
}


template<class T>
inline Person<T>& Person<T>::operator++()
{
	++(this->i);
	return *(this);
}

template<class T>
inline Person<T>& Person<T>::operator--()
{
	--(this->i);
	return *(this);
}

template <class T>
ostream& operator<<(ostream& stream, const Person<T>& person)
{
	return stream << person.get_designation();
}