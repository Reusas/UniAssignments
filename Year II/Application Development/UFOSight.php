<?php

class UFOSight
{
    public $latitude;
    public $longitude;
    public $yearSeen;
    public $cowIncident;
    public $cropCircle;
    public $alienSight;
    public $abductionEvent;

    public function __construct($longitude,$latitude,$yearSeen,$cowIncident,$cropCircle,$alienSight,$abductionEvent)
    {
        $this->longitude = $longitude;
        $this->latitude = $latitude;
        $this->yearSeen = $yearSeen;
        $this->cowIncident = $cowIncident;
        $this->cropCircle = $cropCircle;
        $this->alienSight = $alienSight;
        $this->abductionEvent = $abductionEvent;
    }

}
?>