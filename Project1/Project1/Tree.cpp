
#include <iostream>
using namespace std;
#include <list>
#include <string>
#include "Tree.h"

bool Tree::deleteAllTree(Node* t)
{
	if (t == nullptr)//if empty
		return true;
	if (t->isLeaf)//if its a leaf
	{
		delete t;
		t = nullptr;
	}
	//list <Node*>::iterator it;
	if (t)
	{
		for(auto it = t->responses.rbegin(); it != t->responses.rend(); it++)
		{
			if (deleteAllTree(*it))//send to func and check again if its a leaf-delete the node.otherwise,goes to its list and do it all again
				(*it)->responses.remove(t);
		}
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
	Node* ptr = search(root, val);//return pointer
	if (ptr)//if the value exists
	{
		if(deleteAllTree(ptr))//send to delete func
			return true;
	}
	return false;
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
//void Tree::print(Node* p)//print the tree from the given node
//{
//	if (!p)//if empty tree
//		return;//prints nothing
//	cout << p->content << endl;//print the disscution
//	int x = 1;//counts the spaces
//	list <Node*>::iterator it;
//	list <Node*> tmp;
//	bool flag = false;
//	for (it=p->responses.begin();it!=p->responses.end();it++)//runs on the respones list
//	{
//		tmp =(*it)->responses;//saves header to the list
//		int size = tmp.size();//amount of responses
//		while (size>0/*tmp.empty()==false*/ /*&& tmp.front()->isLeaf == false*//*tmp.front()->content != ""*/)//as long as there is a respone-not leaf
//		{
//			if (x == 1)
//			{
//				cout << "   " << (*it)->content << endl;
//				x++;
//			}
//				
//			flag = true;//entered the while loop
//			for (int i = 0; i < x; i++)//prints the spaces
//			{
//				cout << "   ";
//			}
//			cout << tmp.front()->content << endl;//prints the content-respone
//
//			x++;//promotes spaces
//			tmp.pop_front();//remove the first element-promotes the list
//			if (!tmp.empty())//if there are more responses/////
//				x--;
//			size--;
//			//check if doesnt cause any error-maybe tmp and it point to the same place?
//		}
//		x = 1;//restart spaces
//		if (!flag)//is leaf
//		{
//			for (int i = 0; i < x; i++)//prints the spaces
//			{
//				cout << "   ";
//			}
//			cout << (*it)->content << endl;
//		}
//		//goes to the next respone in the list
//	}
//}
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