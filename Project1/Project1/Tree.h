#pragma once
#include <iostream>
using namespace std;
#include <list>
#include <string>
#include <algorithm>

//Node: each Node in the discussion tree
class Node
{
	/*void removeSonValue(string v)
	{

	}*/
public:
	list <Node*> responses;
	string content;
	bool isLeaf;
	Node(string v) { isLeaf = true;  content = v; }//ctor
	Node() { isLeaf = true; content = ""; }
	friend class Tree;
};



//Tree: the discussion Tree
class Tree
{
	Node* root;
	Node* search(Node* p, string val/*, Node*& parent*/)
	{
		if (p == nullptr)//if empty
			return nullptr;
		if (p->content == val)//if the given value in the current node
			return p;
		list <Node*>::iterator it;

		for (it = p->responses.begin(); it != p->responses.end(); it++)
		{
			Node* p2 = search(*it, val);
			if (p2)
			{
				return p2;
			}
		}
		return nullptr;
	}
	 	void print(Node* p,int level);
		void searchAndPrintPath(Node* p, string val);
public:
	Node * help(Node* p, string value);
	Tree() { root = NULL; }
	Tree(string val)
	{
		root = new Node(val);
	}
	~Tree() {
		deleteAllTree(root);//send to func
		root = 0;
	}
	void searchAndPrintPath(string val) { searchAndPrintPath(root,val); };
	bool deleteAllTree(Node* t);
	void addRoot(string newval);
	bool addSon(string father, string son);
	bool isEmpty() { return root == nullptr; }
	string getContent()//return content
	{
		return root->content;
	}
	void printAllTree() {print(root,0); }
	void printSubTree(string val) 
	{ printSubTree(root, val);
	 // searchAndPrintPath(val);
	}
	Node* findFather(Node* current, string val);
	void printSubTree(Node* curr, string val);
	bool deleteSubTree(string val);
	friend class treeList;
};

