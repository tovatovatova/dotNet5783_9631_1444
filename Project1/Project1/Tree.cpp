
#include <iostream>
using namespace std;
#include <list>
#include <string>
#include "Tree.h"

bool Tree::deleteAllTree(Node* t)
{
	if (!t->isLeaf)//has responses
	{
		for (auto it = t->responses.begin(); it != t->responses.end(); it++)//runs on list of responses
			deleteAllTree(*it);//send again to func

	}
	else
	{
		delete t;
		t = nullptr;
		return true;
	}
}
void Tree::addRoot(string newval)//create a new tree-deletes if existed one
{
	if (root)//if there is a tree
	{
		delete root;//delete the tree
		root = nullptr;
	}
	root = new Node(newval);//create a new tree
}
bool Tree::addSon(string father, string son)//add respone to discussion
{
	Node* ptr = search(root, father);//return pointer to the father
	if (ptr)//if its exist
	{
		
		Node* newElement;
		newElement=new Node(son);//create a newv node
		ptr->responses.push_back(newElement);//add a response
		ptr->isLeaf = false; 
		return true;
	}
	return false;
}
bool Tree::deleteSubTree(string val)//delete the sub tree from the given value
{
	Node* ptr = search(root, val);//return pointe to the given value
	if (ptr)//if exist
	{
		Node* father = findFather(root, val);//return the father to val
		if (father)//if has father
		{
			father->responses.remove(ptr);//delete the given response(ptr) from the discussion(father)

		}
		else//has no father-its the root
		{
			deleteAllTree(root);//delete all the tree
			root = nullptr;
		}
		return true;
	}
	return false;//couldn't delete
}

Node* Tree::findFather(Node* current, string val)//return a pointer to the father of the given val if exist, ig not return false
{//we know already that the val in the tree...
	if (current->content == val)//the val in the root-has no father
		return nullptr;
	list <Node*>::iterator it;
	for (it = current->responses.begin(); it != current->responses.end(); it++)
	{
		if ((*it)->content == val)//if has son with val-the current is the father
			return current;
		else
		{
			Node * p = findFather(*it, val);
			if (p)
				return p;
		}
	}
	return nullptr;

}


void Tree::print(Node* p, int level = 0)
{
	if (p == nullptr)//if empty
		return;
	for (int i = 0; i < level; i++)
	{
		cout << "   ";
	}
	level++;//
	cout << p->content << endl;
	list <Node*>::iterator it;
	for (it = p->responses.begin(); it != p->responses.end(); it++)
	{
		if (p->isLeaf)//if a leaf
			level--;
		print(*it,level);//send to func and prints
	}
}

void Tree::printSubTree(Node* curr, string val)//find the given value and prints the sub tree from the value to the leaf 
{
	Node* ptr = search(root, val);
	if (!ptr)
		return;
	print(ptr);
	searchAndPrintPath(curr, val);//prints path from the current node to the leaf
	cout << "=>" << root->content << endl;//end of path
}
void Tree:: searchAndPrintPath(Node* p, string val)//find the value and pr`ints from the value to the root
{
	Node* newNode;
	if (!p)//if empty
		return;
	if (p->content==val)
	{
		cout << val ;
		return;
	}
	newNode = help( p , val);
	searchAndPrintPath(newNode, val);//send again to the func with the value and new node of its ancestor
	if(newNode->content != val)
		cout << "=>" << newNode->content <<  " ";
}
Node* Tree:: help(Node* p, string value)
{
	list <Node*>::iterator it;
	for (it = p->responses.begin(); it != p->responses.end(); it++)
	{
		if (search((*it), value))//check if in this sub tree the given value exist
			return (*it);//return ancestor of the given value
	}
	return nullptr;//nothing was found
}