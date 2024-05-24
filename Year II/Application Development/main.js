var r = new Requests();
var map = new Map();



document.getElementById("editButton").onclick = function(){switchEditScreen()};
document.getElementById("button2").onclick = function(){plotPostUFOSighting()};
document.getElementById("button3").onclick = function(){plotUpdateUFOSighting()};
document.getElementById("button4").onclick = function(){plotDeleteUFOSighting()};


// This calls the plot UFOs function when the window loads aswell as the switchEditScreen to hide the
// edit buttons.
window.onload = function()
{
    map.createMap();
    plotUFOSightings();
    switchEditScreen();
}

// Function that gets the div of the buttons for editing data and switches between hidding them
// and showing them.
function switchEditScreen()
{
    var editDiv = document.getElementById("editScreen");

    if(editDiv.style.display == "none")
    {
        
        editDiv.style.display = "block";
    }
    else
    {
       
        editDiv.style.display = "none";
    }
}



// Function to draw every single point on the map
async function plotUFOSightings()
{
    
    let result = await r.getAllUFOSightings();
    map.redrawMap();
    for(i=0; i< result.length; i++)
    {
        map.addMarker(result[i]);
    }
    
}

// These functions all await for data to be updated when it is posted, updated or deleted.
// Then it re plots the map with the new data.
async function plotPostUFOSighting()
{
    await r.postUFOSighting();
    plotUFOSightings();
}

async function plotUpdateUFOSighting()
{
    await r.putUFOSighting();
    plotUFOSightings();
}

async function plotDeleteUFOSighting()
{
    await r.deleteUFOSighting();
    plotUFOSightings();
}



