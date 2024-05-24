#include "SceneGraph.h"

bool SceneGraph::Initialise(void)
{
	// Loop through all the children nodes
	for (SceneNodePointer s : _children)
	{
		// call initialise and store result in bool i
		bool i = s->Initialise();		
		// if at any time i is false, the function returns false
		if (i == false)
		{
			return false;
		}
	}
	
	// this will return true if the above loop never breaks, meaning its never returns false
	return true;
}

void SceneGraph::Update(const Matrix& worldTransformation)
{
	this->_cumulativeWorldTransformation = worldTransformation;

	for (SceneNodePointer s : _children)
	{
		s->Update(_thisWorldTransformation * _cumulativeWorldTransformation);
		
	}
}

void SceneGraph::Render(void)
{
	 //Loop through each node in children and call the render method
	for (SceneNodePointer s : _children)
	{
		s->Render();
	}
}

void SceneGraph::Shutdown(void)
{
	// Loop through each node in children and call the shutdown method
	for (SceneNodePointer s : _children)
	{
		s->Shutdown();
	}
}

void SceneGraph::Add(SceneNodePointer node)
{	
	// add a node to the children vector.
	_children.push_back(node);
}

void SceneGraph::Remove(SceneNodePointer node)
{
	for (SceneNodePointer s : _children)
	{
		// If node in children matches this node remove it.
		if (s == node)
		{
			s->Remove(s);
		}
			
	}
}

SceneNodePointer SceneGraph::Find(wstring name)
{
	//Loop through every node in chidlren
	for (SceneNodePointer s : _children)
	{
		// Check if name being searched is this name. If it is then return the pointer to this
		if (name == _name)
		{
			return SceneNodePointer();
		}
		// Check if the find name does not equal to NULL meaning it succeedes. If it succedes it returns that node
		else if (s->Find(name) != NULL)
		{
			return s;
		}
		else
		{
			// this else statement will be executed when the Find method fails in which case a null pointer is returned.
			return nullptr;
		}
	}

	
}
