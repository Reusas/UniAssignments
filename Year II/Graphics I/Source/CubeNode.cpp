#include "CubeNode.h"
#include "DirectXFramework.h"
#include "Geometry.h"


SceneNode::SceneNode()
{

}

CubeNode::CubeNode()
{

}

CubeNode::CubeNode(wstring name, Vector4 dirLight)
{
	_name = name;
	_dirLight = dirLight;
	
}

bool CubeNode::Initialise()
{
	GenerateNormals();
	BuildGeometryBuffers();
	BuildShaders();
	BuildVertexLayout();
	BuildConstantBuffer();



	return true;
	
}

void CubeNode::Render()
{
	_viewTransformation = DirectXFramework::GetDXFramework()->GetViewTransformation();
	_projectionTransformation = DirectXFramework::GetDXFramework()->GetProjectionTransformation();

	// Calculate the world x view x projection transformation
	Matrix completeTransformation = _cumulativeWorldTransformation * _viewTransformation * _projectionTransformation;

	// World transform needs to be set on constant buffer
	CBuffer constantBuffer;

	constantBuffer.World = _cumulativeWorldTransformation;

	constantBuffer.WorldViewProjection = completeTransformation;
	constantBuffer.AmbientLightColour = _dirLight;

	constantBuffer.DirectionalLightVector = Vector4(-1.0f, -1.0f, 1.0f, 0.0f);
	constantBuffer.DirectionalLightColor = Vector4(0.5f,0.5f,0.5f,1.0f);


	// Update the constant buffer. Note the layout of the constant buffer must match that in the shader
	DirectXFramework::GetDXFramework()->GetDeviceContext()->VSSetConstantBuffers(0, 1, _constantBuffer.GetAddressOf());
	DirectXFramework::GetDXFramework()->GetDeviceContext()->UpdateSubresource(_constantBuffer.Get(), 0, 0, &constantBuffer, 0, 0);

	// Now render the cube
	// Specify the distance between vertices and the starting point in the vertex buffer
	UINT stride = sizeof(Vertex);
	UINT offset = 0;
	// Set the vertex buffer and index buffer we are going to use
	DirectXFramework::GetDXFramework()->GetDeviceContext()->IASetVertexBuffers(0, 1, _vertexBuffer.GetAddressOf(), &stride, &offset);
	DirectXFramework::GetDXFramework()->GetDeviceContext()->IASetIndexBuffer(_indexBuffer.Get(), DXGI_FORMAT_R32_UINT, 0);

	// Specify the layout of the polygons (it will rarely be different to this)
	DirectXFramework::GetDXFramework()->GetDeviceContext()->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

	// Specify the layout of the input vertices.  This must match the layout of the input vertices in the shader
	DirectXFramework::GetDXFramework()->GetDeviceContext()->IASetInputLayout(_layout.Get());

	// Specify the vertex and pixel shaders we are going to use
	DirectXFramework::GetDXFramework()->GetDeviceContext()->VSSetShader(_vertexShader.Get(), 0, 0);
	DirectXFramework::GetDXFramework()->GetDeviceContext()->PSSetShader(_pixelShader.Get(), 0, 0);

	// Specify details about how the object is to be drawn
	DirectXFramework::GetDXFramework()->GetDeviceContext()->RSSetState(_rasteriserState.Get());

	// Now draw the first cube
	DirectXFramework::GetDXFramework()->GetDeviceContext()->DrawIndexed(ARRAYSIZE(indices), 0, 0);
}

void CubeNode::Shutdown()
{

}

void CubeNode::BuildGeometryBuffers()
{


	// This method uses the arrays defined in Geometry.h
	// 
	// Setup the structure that specifies how big the vertex 
	// buffer should be
	D3D11_BUFFER_DESC vertexBufferDescriptor = { 0 };
	vertexBufferDescriptor.Usage = D3D11_USAGE_IMMUTABLE;
	vertexBufferDescriptor.ByteWidth = sizeof(Vertex) * ARRAYSIZE(vertices);
	vertexBufferDescriptor.BindFlags = D3D11_BIND_VERTEX_BUFFER;
	vertexBufferDescriptor.CPUAccessFlags = 0;
	vertexBufferDescriptor.MiscFlags = 0;
	vertexBufferDescriptor.StructureByteStride = 0;

	// Now set up a structure that tells DirectX where to get the
	// data for the vertices from
	D3D11_SUBRESOURCE_DATA vertexInitialisationData = { 0 };
	vertexInitialisationData.pSysMem = &vertices;

	// and create the vertex buffer
	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreateBuffer(&vertexBufferDescriptor, &vertexInitialisationData, _vertexBuffer.GetAddressOf()));

	// Setup the structure that specifies how big the index 
	// buffer should be
	D3D11_BUFFER_DESC indexBufferDescriptor = { 0 };
	indexBufferDescriptor.Usage = D3D11_USAGE_IMMUTABLE;
	indexBufferDescriptor.ByteWidth = sizeof(UINT) * ARRAYSIZE(indices);
	indexBufferDescriptor.BindFlags = D3D11_BIND_INDEX_BUFFER;
	indexBufferDescriptor.CPUAccessFlags = 0;
	indexBufferDescriptor.MiscFlags = 0;
	indexBufferDescriptor.StructureByteStride = 0;

	// Now set up a structure that tells DirectX where to get the
	// data for the indices from
	D3D11_SUBRESOURCE_DATA indexInitialisationData;
	indexInitialisationData.pSysMem = &indices;

	// and create the index buffer
	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreateBuffer(&indexBufferDescriptor, &indexInitialisationData, _indexBuffer.GetAddressOf()));
}

void CubeNode::BuildVertexLayout()
{
	// Create the vertex input layout. This tells DirectX the format
// of each of the vertices we are sending to it. The vertexDesc array is
// defined in Geometry.h

	

	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreateInputLayout(vertexDesc, ARRAYSIZE(vertexDesc), _vertexShaderByteCode->GetBufferPointer(), _vertexShaderByteCode->GetBufferSize(), _layout.GetAddressOf()));
}

void CubeNode::BuildShaders()
{
	DWORD shaderCompileFlags = 0;
#if defined( _DEBUG )
	shaderCompileFlags = D3DCOMPILE_DEBUG | D3DCOMPILE_SKIP_OPTIMIZATION;
#endif

	ComPtr<ID3DBlob> compilationMessages = nullptr;

	//Compile vertex shader
	HRESULT hr = D3DCompileFromFile(ShaderFileName,
		nullptr, D3D_COMPILE_STANDARD_FILE_INCLUDE,
		VertexShaderName, "vs_5_0",
		shaderCompileFlags, 0,
		_vertexShaderByteCode.GetAddressOf(),
		compilationMessages.GetAddressOf());

	if (compilationMessages.Get() != nullptr)
	{
		// If there were any compilation messages, display them
		MessageBoxA(0, (char*)compilationMessages->GetBufferPointer(), 0, 0);
	}
	// Even if there are no compiler messages, check to make sure there were no other errors.
	ThrowIfFailed(hr);
	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreateVertexShader(_vertexShaderByteCode->GetBufferPointer(), _vertexShaderByteCode->GetBufferSize(), NULL, _vertexShader.GetAddressOf()));

	// Compile pixel shader
	hr = D3DCompileFromFile(ShaderFileName,
		nullptr, D3D_COMPILE_STANDARD_FILE_INCLUDE,
		PixelShaderName, "ps_5_0",
		shaderCompileFlags, 0,
		_pixelShaderByteCode.GetAddressOf(),
		compilationMessages.GetAddressOf());

	if (compilationMessages.Get() != nullptr)
	{
		// If there were any compilation messages, display them
		MessageBoxA(0, (char*)compilationMessages->GetBufferPointer(), 0, 0);
	}
	ThrowIfFailed(hr);
	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreatePixelShader(_pixelShaderByteCode->GetBufferPointer(), _pixelShaderByteCode->GetBufferSize(), NULL, _pixelShader.GetAddressOf()));
}

void CubeNode::BuildConstantBuffer()
{
	D3D11_BUFFER_DESC bufferDesc;
	ZeroMemory(&bufferDesc, sizeof(bufferDesc));
	bufferDesc.Usage = D3D11_USAGE_DEFAULT;
	bufferDesc.ByteWidth = sizeof(CBuffer);
	bufferDesc.BindFlags = D3D11_BIND_CONSTANT_BUFFER;

	ThrowIfFailed(DirectXFramework::GetDXFramework()->GetDevice()->CreateBuffer(&bufferDesc, NULL, _constantBuffer.GetAddressOf()));
}

void CubeNode::GenerateNormals()
{

	//For each Vertex, set the corresponding contributing count array entry to 0.
	for (int i = 0; i < ARRAYSIZE(vertices); i++)
	{
		verticeCount[i] = 0;
	}

	// Get total number of polygons. Polygons are 3 indices (triangle). 
	int polygonCount = 0;
	for (int i = 0; i < ARRAYSIZE(indices); i += 3)
	{
		polygonCount++;

	}
	// list of polygon normals
	vector<Vector3> polygonNormals;

	int startingIndiceCount = 0;

	// For each polygon -> a polygon is three indices, for example		0, 1, 2 or 0,2,3
	for (int i = 0; i < polygonCount; i++)
	{
		//   Get the 3 indices that make up that polygon -> so for polygon 1 the indices would be 0,1,2
		int indice1 = indices[startingIndiceCount];
		int indice2 = indices[startingIndiceCount + 1];
		int indice3 = indices[startingIndiceCount + 2];

		startingIndiceCount += 3;
		//   Get the vertices for those indices -> well the vertices are the indice value so indice 0 is vertice 0, indice 1 vertice 1 and indice 2 vertice 2
		Vertex v0 = vertices[indice1];
		Vertex v1 = vertices[indice2];
		Vertex v2 = vertices[indice3];
		//     Construct vector a by substracting vertex 0 from vertex 1 -> that would be vertice 1 - 0

		Vector3 vectorA = v1.Position - v0.Position;
		Vector3 vectorB = v2.Position - v0.Position;
		Vector3 normalVector = vectorA.Cross(vectorB);
		normalVector.Normalize();
		// assigning normalVector to list
		polygonNormals.push_back(normalVector);

		//Add the polygon normal to the vertex normal for each of the 3 vertices for that polygon
		vertices[indice1].Normal += polygonNormals[i];
		vertices[indice2].Normal += polygonNormals[i];
		vertices[indice3].Normal += polygonNormals[i];
		//add 1 to the contributing count for	each of the vertices

		verticeCount[indice1] ++;
		verticeCount[indice2] ++;
		verticeCount[indice3] ++;

	}

	for (int i = 0; i < ARRAYSIZE(vertices); i++)
	{
		vertices[i].Normal = vertices[i].Normal / verticeCount[i];
		vertices[i].Normal.Normalize();
	}



}