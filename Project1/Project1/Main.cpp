#include <iostream>//Orit Rokach 213279631 and Shirel Maitar 214439101
#include <list>
#include <string>
#include "Tree.h"
#include "ListTree.h"

using namespace std;
int main()
{
	ListTree tl;
	string title, father, val, son;
	char ch;
	cout << "\nDISCUSSION TREE\n";
	cout << "Choose one of the following:" << endl;
	cout << "n: New discussion tree" << endl;
	cout << "s: Add a new response" << endl;
	cout << "d: Delete a sub-discussion" << endl;
	cout << "p: Print all discussion trees" << endl;
	cout << "r: Print a sub-tree" << endl;
	cout << "w: Search a string in all discussion trees" << endl;
	cout << "e: exit:" << endl;
	do
	{
		cin >> ch;
		switch (ch)
		{
		case 'n':cout << "enter the discussion title (with no space) "; cin >> val; tl.addNewTree(val); break;
		case 's':cout << "enter the discussion title (with no space) "; cin >> title;
			cout << "enter the last message (with no space) "; cin >> father;
			cout << "enter the new respond "; cin >> son;
			if (tl.addResponse(title, father, son)) cout << "success\n"; else cout << "ERROR\n"; break;
		case 'd':cout << "enter the discussion title (with no space) "; cin >> title;
			cout << "enter string of subtree to delete (with no space) "; cin >> val;
			if (tl.delResponse(title, val)) cout << "success\n"; else cout << "ERROR\n"; break;
		case 'p':tl.printAllTrees();  break;
		case 'r':
			cout << "enter the discussion title (with no space) "; cin >> title;
			cout << "enter the last message (with no space) "; cin >> val;
			tl.printSubTree(title, val); cout << endl;  break;
		case 'w':cout << "enter a string (with no space) "; cin >> val;
			tl.searchAndPrint(val); cout << endl;  break;
		case 'e':cout << "bye "; break;
		default: cout << "ERROR\n";  break;
		}
	} while (ch != 'e');
}