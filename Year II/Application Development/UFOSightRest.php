<?php
    require "Database.php";
    require "Rest.php";
    require "UFOSight.php";

    class UFOSightRest extends Rest
    {
        private $UFOSightings;
        private $availableQueryStrings;

        public function __construct()
        {
            // The string that will be used for api calls.
            parent::__construct("ufo");

            // The available parameters for searching UFO sightings with one of these values being true
            $this->availableQueryStrings = array("Cow_incidents", "Crop_Circle_found", "Alien_sighted", "Abduction_event");

        }

        public function performGet($url, $parameters, $requestBody, $accept)
        {
            switch(count($parameters))
            {
                case 1:
                header('Content-Type: application/json; charset=utf-8');
                header('no-cache,no-store');
                $this->getAllUFOSightings();
                echo json_encode($this->UFOSightings);
                break;
                
                case 2:
                header('Content-Type: application/json; charset=utf-8');
                header('no-cache,no-store');
                // If the parameter is in the availableQueryStrings array then call method to get UFO Sightings with that parameter.
                if(in_array($parameters[1], $this->availableQueryStrings))
                {
                    $this->getSpecificUFOSightings($parameters[1]);
                    echo json_encode($this->UFOSightings);
                }
                else
                {
                    // Otherwise call the method to get UFO Sightings in a specific year.
                    $this->getUFOSightingsInYear($parameters[1]);
                    echo json_encode($this->UFOSightings);
                }
                break;
                
                default:
                $this->methodNotAllowedResponse();

            
                
            }

        }

        public function performPost($url, $parameters, $requestBody, $accept)
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            $UFO_Sight = $this->JSONToUFOSight($requestBody);
            
            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);

            if(!$connection->connect_error)
            {
                $query = "insert into UFO_Sightings (longitude,latitude,year,Cow_incidents,Crop_circle_found,Alien_sighted,Abduction_event) values (?, ?, ?, ?, ?, ?, ?)";
                $statement = $connection->prepare($query);

                $longitude = $UFO_Sight->longitude;
                $latitude = $UFO_Sight->latitude;
                $year = $UFO_Sight->yearSeen;
                $cowIncident = $UFO_Sight->cowIncident;
                $cropCircle = $UFO_Sight->cropCircle;
                $alienSight = $UFO_Sight->alienSight;
                $abductionEvent = $UFO_Sight->abductionEvent;

                $statement->bind_param("ddissss",$longitude,$latitude,$year,$cowIncident,$cropCircle,$alienSight,$abductionEvent);

                $result = $statement->execute();

                if($result == false)
                {
                    $errorMessage = $statement->error;
                }
                $statement->close();
                $connection->close();
                if($result == TRUE)
                {
                    $this->noContentResponse();
                }
                else
                {
                    $this->errorResponse($errorMessage);
                }

            }
        }

        public function performPut($url, $parameters, $requestBody, $accept)
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            $UFO_Sight = $this->JSONToUFOSight($requestBody);

            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);
            if(!$connection->connect_error)
            {
                $query = "update UFO_Sightings set longitude = ?, latitude = ?, year = ?, Cow_incidents = ?, Crop_circle_found = ?, Alien_sighted = ?, Abduction_event = ? where longitude = ? and latitude = ?";
                $statement = $connection->prepare($query);

                $longitude = $UFO_Sight->longitude;
                $latitude = $UFO_Sight->latitude;
                $year = $UFO_Sight->yearSeen;
                $cowIncident = $UFO_Sight->cowIncident;
                $cropCircle = $UFO_Sight->cropCircle;
                $alienSight = $UFO_Sight->alienSight;
                $abductionEvent = $UFO_Sight->abductionEvent;

                $statement->bind_param("ddissssdd",$longitude,$latitude,$year,$cowIncident,$cropCircle,$alienSight,$abductionEvent,$longitude,$latitude);

                $result = $statement->execute();

                if($result == FALSE)
                {
                    $errorMessage = $statement->error();
                }
                $statement->close();
                $connection->close();
                if($result == TRUE)
                {
                    $this->noContentResponse();
                }
                else
                {
                    $this->errorResponse($errorMessage);
                }

            }
        }

        public function performDelete($url, $parameters, $requestBody, $accept)
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            
            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);

            if(!$connection->connect_error)
            {
                $query = "delete from UFO_Sightings where longitude = ? and latitude = ?";
                $dataArray = json_decode($requestBody,true);
                $longitude = $dataArray["longitude"];
                $latitude = $dataArray["latitude"];
                $statement = $connection->prepare($query);
                $statement->bind_param("dd",$longitude,$latitude);
                $result = $statement->execute();
                
                if($result == FALSE)
                {
                    $errorMessage = $statement->error();
                }
                $statement->close();
                $connection->close();
                if($result == TRUE)
                {
                    $this->noContentResponse();
                }
                else
                {
                    $this->errorResponse($errorMessage);
                }
            }
        }

        public function getAllUFOSightings()
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            
            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);

            if(!$connection->connect_error)
            {
                $query = "select longitude, latitude, year, Cow_incidents, Crop_Circle_found, Alien_sighted, Abduction_event from UFO_Sightings";

                if($result = $connection->query(($query)))
                {
                    while($row = $result->fetch_assoc())
                    {
                        $this->UFOSightings[] = new UFOSight($row["longitude"], $row["latitude"], $row["year"], $row["Cow_incidents"], $row["Crop_Circle_found"], $row["Alien_sighted"], $row["Abduction_event"]);
                    }
                    $result->close();
                }

                $connection->close();
            }
        }

        private function getUFOSightingsInYear($year)
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);

            if(!$connection->connect_error)
            {
                $query = "select longitude, latitude, year, Cow_incidents, Crop_Circle_found, Alien_sighted, Abduction_event from UFO_Sightings WHERE year = ?";
                $statement = $connection->prepare($query);
                $statement->bind_param("i", $year);
                $statement->execute();
                $statement->store_result();
                $statement->bind_result($longitude, $latitude, $year, $cowIncident, $cropCircleFound, $alienSighted, $abductionEvent);

                while($statement->fetch())
                {
                    $this->UFOSightings[] = new UFOSight($longitude, $latitude, $year, $cowIncident, $cropCircleFound, $alienSighted, $abductionEvent);
                }
                $statement->close();
                $connection->close();
            }

        }

        private function getSpecificUFOSightings($parameter)
        {
            global $dbServer, $dbUsername, $dbPassword, $dbDatabase;
            $connection = new mysqli($dbServer, $dbUsername, $dbPassword, $dbDatabase);
            if(!$connection->connect_error)
            {
                $queryResult = 'Yes';
                $query = "select longitude, latitude, year, Cow_incidents, Crop_Circle_found, Alien_sighted, Abduction_event from UFO_Sightings WHERE ". $parameter."= ?";
                $statement = $connection->prepare($query);

                $statement->bind_param("s", $queryResult);
                $statement->execute();
                $statement->store_result();
                $statement->bind_result($longitude, $latitude, $year, $cowIncident, $cropCircleFound, $alienSighted, $abductionEvent);

                while($statement->fetch())
                {
                    $this->UFOSightings[] = new UFOSight($longitude, $latitude, $year, $cowIncident, $cropCircleFound, $alienSighted, $abductionEvent);
                }
                $statement->close();
                $connection->close();
            }

        }

        // Convert JSON data into a UFOSight object.
        private function JSONToUFOSight($JSON)
        {
            // Create associative array and use it to assing values to a new UFO_Sight object
            $dataArray = json_decode($JSON, true);
            $UFO_Sight = new UFOSight(
                $dataArray["longitude"],
                $dataArray["latitude"],
                $dataArray["yearSeen"],
                $dataArray["cowIncident"],
                $dataArray["cropCircle"],
                $dataArray["alienSight"],
                $dataArray["abductionEvent"]
            );
            
            return $UFO_Sight;
        }

    }

?>