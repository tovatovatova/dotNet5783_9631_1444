
#pragma once
#include <iostream>
using namespace std;
#include <list>
#include <string>
#include "Tree.h"
#include "ListTree.h"
class ListTree
{
	list<Tree*> trees;

public:
	ListTree();
	~ListTree();//dtor
	void addNewTree(string s);
	void deleteTree(Tree* current);
	void searchAndPrint(string val);
	bool addResponse(string rt, string prnt, string res);
	bool delResponse(string diss, string son);
	void printTree(string rt);
	void printAllTrees();
	void printSubTree(string rt, string s);
};