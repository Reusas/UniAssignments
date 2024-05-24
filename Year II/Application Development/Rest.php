<?php

class Rest
{
    private $supportedMethods;
    private $apiStringToMatch;

    public function __construct($apiStringToMatch)
    {
        $this->apiStringToMatch = $apiStringToMatch;
        $this->supportedMethods = "GET, PUT, POST, DELETE";
    }

    public function handleRawRequest()
    {
        $url = $this->getFullUrl();
        $method = $_SERVER['REQUEST_METHOD'];
        $requestBody = file_get_contents('php://input');

        if(isset($_GET['q']))
        {
            $parameters = explode("/", $_GET['q']);

            if(strlen($this->apiStringToMatch) > 0 && count($parameters) > 0)
            {
                if(strcmp($this->apiStringToMatch, $parameters[0]) !=0)
                {
                    $this->notImplementedResponse();
                    return;
                }
            }
        }
        else
        {
            $parameters = array();
        }
        if(isset($_SERVER['HTTP_ACCEPT']))
        {
            $accept = $_SERVER['HTTP_ACCEPT'];
        }
        else
        {
            $accept = "";
        }
        $this->handleRequest($url, $method, $parameters, $requestBody, $accept);

    }

    public function getFullUrl()
    {
        if(isset($_SERVER['HTTPS']))
        {
            $protocol = $_SERVER['HTTPS'] == 'on' ? 'https' : 'http';
        }
        else
        {
            $protocol = 'http';
        }
        $location = $_SERVER['REQUEST_URI'];

        return $protocol.'://'.$_SERVER['HTTP_HOST'].$location;
    }

    public function handleRequest($url, $method, $parameters, $requestBody, $accept)
    {
        switch($method)
        {
            case 'GET':
                $this->performGet($url, $parameters, $requestBody, $accept);
                break;
            case 'POST':
                $this->performPost($url, $parameters, $requestBody, $accept);
                break;
            case 'PUT':
                $this->performPut($url, $parameters, $requestBody, $accept);
                break;
            case 'DELETE':
                $this->performDelete($url, $parameters, $requestBody, $accept);
                break;
            default:
                $this->notImplementedResponse();

        }
    }

    public function performGet($url, $parameters, $requestBody, $accept)
    {
        $this->methodNotAllowedResponse();
    }

    public function performPost($url, $parameters, $requestBody, $accept)
    {
        $this->methodNotAllowedResponse();
    }

    public function performPut($url, $parameters, $requestBody, $accept)
    {
        $this->methodNotAllowedResponse();
    }

    public function performDelete($url, $parameters, $requestBody, $accept)
    {
        $this->methodNotAllowedResponse();
    }

    protected function notImplementedResponse()
    {
        header('Allow: ' . $this->supportedMethods, true, 501);
    }

    protected function methodNotAllowedResponse()
    {
        header('Allow: ' . $this->supportedMethods, true, 405);
    }

    protected function errorResponse($errorMsg)
    {
        header("Error: $errorMsg", true, 500);
    }

    protected function notFoundResponse()
    {
        header("HTTP/1.1 404 Not Found");
    }

    protected function noContentResponse()
    {
        header("HTTP/1.1 204 No Content");
    }

}



?>