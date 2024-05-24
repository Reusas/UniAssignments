#include "DirectXApp.h"
#include "CubeNode.h"

DirectXApp app;
// Now add nodes to the scene graph

float rotAngle = 0;

void DirectXApp::CreateSceneGraph()
{
	SceneGraphPointer sceneGraph = GetSceneGraph();
	// Now add nodes to the scene graph
	shared_ptr<CubeNode> cube = make_shared<CubeNode>(L"Body", Vector4(0.25f, 0, 0, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(5, 8, 2.5f)) * Matrix::CreateTranslation(Vector3(0, 23, 0)));
	sceneGraph->Add(cube);

	cube = make_shared<CubeNode>(L"Left Leg", Vector4(0, 0.25f, 0, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(1, 7.5, 1)) * Matrix::CreateTranslation(Vector3(-4, 7.5f, 0)));
	sceneGraph->Add(cube);

	cube = make_shared<CubeNode>(L"Right Leg", Vector4(0, 0.25f, 0, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(1, 7.5, 1)) * Matrix::CreateTranslation(Vector3(4, 7.5, 0)));
	sceneGraph->Add(cube);

	cube = make_shared<CubeNode>(L"Head", Vector4(0, 0, 0.25f, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(3, 3, 3)) * Matrix::CreateTranslation(Vector3(0, 34, 0)));
	sceneGraph->Add(cube);

	cube = make_shared<CubeNode>(L"Left Arm", Vector4(0, 0.25f, 0, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(1, 8.5, 1)) * Matrix::CreateTranslation(Vector3(-6, 22, 0)));
	sceneGraph->Add(cube);

	cube = make_shared<CubeNode>(L"Right Arm", Vector4(0, 0.25f, 0, 1));
	cube->SetWorldTransform(Matrix::CreateScale(Vector3(1, 8.5, 1)) * Matrix::CreateTranslation(Vector3(6, 22, 0)));
	sceneGraph->Add(cube);

}

void DirectXApp::UpdateSceneGraph()
{
	SceneGraphPointer sceneGraph = GetSceneGraph();

	rotAngle += .025f;

	sceneGraph->SetWorldTransform(Matrix::CreateRotationY(rotAngle));



	
}
