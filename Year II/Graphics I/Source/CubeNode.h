#pragma once
#include "SceneNode.h"
//#include "DirectXFramework.h"
class CubeNode : public SceneNode
{




public:
	CubeNode();
	CubeNode(wstring name, Vector4 dirLight);

	virtual bool Initialise() override;
	virtual void Render() override;
	virtual void Shutdown() override;

	void BuildGeometryBuffers();
	void BuildVertexLayout();
	void BuildConstantBuffer();
	void BuildShaders();
	void GenerateNormals();

	ComPtr<ID3DBlob>				_vertexShaderByteCode = nullptr;
	ComPtr<ID3D11InputLayout>		_layout;
	ComPtr<ID3D11VertexShader>		_vertexShader;
	ComPtr<ID3D11PixelShader>		_pixelShader;
	ComPtr<ID3DBlob>				_pixelShaderByteCode = nullptr;

	ComPtr<ID3D11RasterizerState>   _rasteriserState;
	// Maybe not needeed
	Matrix								_viewTransformation;
	Matrix								_projectionTransformation;


	ComPtr<ID3D11Buffer>			_vertexBuffer;
	ComPtr<ID3D11Buffer>			_indexBuffer;
	ComPtr<ID3D11Buffer>			_constantBuffer;






};

