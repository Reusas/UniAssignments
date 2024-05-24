class Map
{
    constructor()
    {
        // Some options for the map. By default it has a lot of options enabled that allow the user to 
        // scroll around the map, zoom in etc. This is not what I want so all of these are disabled.
        this.mapOptions = {
            zoomControl : false,
            dragging : false,
            scrollWheelZoom : false,
            doubleClickZoom : false,
            keyboard : false,
            fadeAnimation: false,
        }

        this.map;
        this.layerGroup;
    }

    createMap()
    {
        // The view is set at the latitute and longitute of Madrid to focus the map on Spain
        this.map = L.map('map',this.mapOptions).setView([40.4168,-3.7038],50);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 6,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(this.map);

        this.layerGroup = L.layerGroup().addTo(this.map);
    }

    redrawMap()
    {
       this.layerGroup.clearLayers();
    }

    addMarker(data)
    {
        
        var _icon = this.generateIcon(data);
        var yearSeenText = "Year:" + data.yearSeen;

        var markerOptions = 
        {
            icon: _icon,
            title: yearSeenText,
        }

        // Convert lat and lon into one with the latLng function from the Leaflet library and add it 
        // to the map.
        var latLon = L.latLng(data.latitude,data.longitude);



        L.marker(latLon,markerOptions).addTo(this.layerGroup);
    }

    generateIcon(data)
    {
        // This method chooses a different image to use for the marker based on its properties.
        // Multiple properties can be "Yes", but for this visualization this method picks the last one
        // which is why the if statements are not else ifs.
        var _iconUrl = 'icon.png';
        if(data.cowIncident == "Yes")
        {
            _iconUrl = "icon2.png";
        }
        if(data.cropCircle == "Yes")
        {
            _iconUrl = "icon3.png";
        }
        if(data.alienSight == "Yes")
        {
            _iconUrl = "icon4.png";
        }
        if(data.abductionEvent =="Yes")
        {
            _iconUrl = "icon5.png";
        }

        // Set the icon image to the iconurl and the icon size to 20 20
        var _icon = L.icon({
            iconUrl: _iconUrl,
            iconSize: [20, 20],
        });
        return _icon;
    }
}





