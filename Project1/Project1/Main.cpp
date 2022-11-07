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


/*
DISCUSSION TREE
Choose one of the following:
n: New discussion tree
s: Add a new response
d: Delete a sub-discussion
p: Print all discussion trees
r: Print a sub-tree
w: Search a string in all discussion trees
e: exit:
a
ERROR
n
enter the discussion title (with no space) a
s
enter the discussion title (with no space) a
enter the last message (with no space) a
enter the new respond a1
success
s
enter the discussion title (with no space) a
enter the last message (with no space) a1
enter the new respond a2
success
s
enter the discussion title (with no space) a
enter the last message (with no space) a1
enter the new respond a3
success
s
enter the discussion title (with no space) a
enter the last message (with no space) a1
enter the new respond a4
success
s
enter the discussion title (with no space) b
enter the last message (with no space) b1
enter the new respond b2
ERROR
p
Tree #1
a
   a1
      a2
      a3
      a4

n
enter the discussion title (with no space) b
s
enter the discussion title (with no space) b
enter the last message (with no space) b1
enter the new respond b1
ERROR
s
enter the discussion title (with no space) b
enter the last message (with no space) b
enter the new respond b1
success
s
enter the discussion title (with no space) b
enter the last message (with no space) b1
enter the new respond b2
success
s
enter the discussion title (with no space) b
enter the last message (with no space) b1
enter the new respond b3
success
s
enter the discussion title (with no space) b
enter the last message (with no space) b1
enter the new respond b4
success
p
Tree #1
b
   b1
      b2
      b3
      b4

Tree #2
a
   a1
      a2
      a3
      a4

r
enter the discussion title (with no space) a
enter the last message (with no space) c

r
enter the discussion title (with no space) a
enter the last message (with no space) a1
a1
   a2
   a3
   a4
a1=>a

r
enter the discussion title (with no space) b
enter the last message (with no space) c

r
enter the discussion title (with no space) b
enter the last message (with no space) b4
b4
b4=>b1 =>b

d
enter the discussion title (with no space) b
enter string of subtree to delete (with no space) b4
success
r
enter the discussion title (with no space) b
enter the last message (with no space) b4

d
enter the discussion title (with no space) b
enter string of subtree to delete (with no space) b
success
r
enter the discussion title (with no space) b
enter the last message (with no space) b3

r
enter the discussion title (with no space) b
enter the last message (with no space) b

d
enter the discussion title (with no space) b
enter string of subtree to delete (with no space) b3
ERROR
p
Tree #1
a
   a1
      a2
      a3
      a4

d
enter the discussion title (with no space) a
enter string of subtree to delete (with no space) f
ERROR
p
Tree #1
a
   a1
      a2
      a3
      a4

d
enter the discussion title (with no space) a
enter string of subtree to delete (with no space) a
success
p
r
enter the discussion title (with no space) a
enter the last message (with no space) a

e
bye
C:\Users\AAA\source\repos\or*/