
#include <iostream>
using namespace std;
#include <list>
#include <string>
#include "Tree.h"
#include "ListTree.h"
#include <algorithm>


ListTree::ListTree()//ctor
{
	////
}
ListTree::~ListTree() //dtor
{
	list <Tree*>::iterator it;
	for (it = trees.begin(); it != trees.end(); it++)//runs on the list
	{
		delete (*it);//send to dtor of Tree class 
	}
}
void ListTree::addNewTree(string s)//add a tree to the list
{
	Tree* t;
	t = new Tree(s);//create a new node-root for the new tree
	trees.push_front(t);//add the new item to the begining of list
}
void ListTree::deleteTree(Tree* current)//delete the given tree
{
	trees.remove(current);//remove the given value from the list
}
void ListTree::searchAndPrint(string val)//search for the given respone in the trees-and prints the path from the root(of the tree where the respone is)until th respone
{

	list <Tree*> ::iterator it;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		(*it)->printSubTree(val);//prints from the given value to the leaf
	}
}
bool ListTree::addResponse(string strRoot, string strFather, string strAdd)//add a respone to a given tree and a disscution
{
	list <Tree*> ::iterator it;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		if ((*it)->getContent() == strRoot)//if the current tree is the one with the given root
		{
			if((*it)->addSon(strFather, strAdd))//send to func to add the new respone below the given father in the current-given root
				return true;//end this func
		}
	}
	return false;
}
bool ListTree::delResponse(string diss, string son)//delete a respone and its sub tree from the list
{
	list <Tree*> ::iterator it;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		if ((*it)->getContent() == diss)//if the current tree is the one with the given root
		{
			if((*it)->deleteSubTree(son))//send to func for deleting the sub tree
				return true;//end of this func
		}
	}
	return false;
}
void ListTree::printTree(string rt)//prints a tree that its root is the giveb string
{
	list <Tree*> ::iterator it;
	int index = 1;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		if ((*it)->getContent() == rt)//if the current root equals to the given value
		{
			cout << "Tree #" << index << endl;//prints num of tree
			(*it)->printAllTree();//prints the tree
		}
			
	}
}
void ListTree::printAllTrees()//prints all the trees
{
	list <Tree*> ::iterator it;

	int index = 1;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		cout << "Tree #" << index << endl;//prints num of tree
		(*it)->printAllTree();//prints the tree
		cout << endl;
		index++;
	}
}
void ListTree::printSubTree(string rt, string s)//prints from the given respone tho the leaf
{
	list <Tree*> ::iterator it;
	for (it = trees.begin(); it != trees.end(); it++)
	{
		if ((*it)->getContent() == rt)//if the current root equals to the given value
		{
			(*it)->printSubTree(s);
		}
	}
}
