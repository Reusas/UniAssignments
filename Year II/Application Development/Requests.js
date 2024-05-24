class Requests
{
    constructor()
    {
        this.xhr = new XMLHttpRequest();
        this.url = '/Assesment/ufo';
    }

    getAllUFOSightings()
    {
        return new Promise(function(resolve, reject)
        {
            let xhr = new XMLHttpRequest();
            let url = '/Assesment/ufo';
            xhr.onload = function()
            {
                if(this.status == 200)
                {
                    var data = JSON.parse(this.responseText);
                    resolve(data);
                }
                else
                {
                    reject();
                }
            }
            
        
            xhr.open('GET',url + (/\?/.test(url) ? "&" : "?") + new Date().getTime(), true);
            xhr.send(null);
        });
    }

    postUFOSighting()
    {

        return new Promise(function(resolve, reject)
        {
            let xhr = new XMLHttpRequest();
            let url = '/Assesment/ufo';
            xhr.onload = function()
            {
                if(this.status == 204)
                {
                   
                    resolve();
                    Requests.showResult("lonInput","latInput","yearInput","cowIncInput","cropCircInput","alienSigInput","abdEvntInput","Data submitted!","green",1);
                }
                else
                {
                    reject();
                    Requests.showResult("lonInput","latInput","yearInput","cowIncInput","cropCircInput","alienSigInput","abdEvntInput","Failed to submit data. Check that it is correct according to the input in the boxes!","red",1);
                }
            }
            
            let _longitude = parseFloat(document.getElementById("lonInput").value);
            let _latitude = parseFloat(document.getElementById("latInput").value);
            let _yearSeen = parseFloat(document.getElementById("yearInput").value);
            let _cowIncident = document.getElementById("cowIncInput").value;
            let _cropCircle = document.getElementById("cropCircInput").value;
            let _alienSight = document.getElementById("alienSigInput").value;
            let _abductionEvent = document.getElementById("abdEvntInput").value;

            var UFOSighting = 
            {
                longitude : _longitude,
                latitude : _latitude,
                yearSeen : _yearSeen,
                cowIncident: _cowIncident,
                cropCircle: _cropCircle,
                alienSight: _alienSight,
                abductionEvent: _abductionEvent
            };



            xhr.open('POST', url + (/\?/.test(url) ? "&" : "?") + new Date().getTime(), true);
            xhr.send(JSON.stringify(UFOSighting));
        });
    }

    putUFOSighting()
    {
        return new Promise(function(resolve, reject)
        {
            let xhr = new XMLHttpRequest();
            let url = '/Assesment/ufo';
        xhr.onload = function()
        {
            if(this.status == 204)
            {
                resolve();
                Requests.showResult("lonInput2","latInput2","yearInput2","cowIncInput2","cropCircInput2","alienSigInput2","abdEvntInput2","Data updated!","green",1);
            }
            else
            {
                reject();
                Requests.showResult("lonInput2","latInput2","yearInput2","cowIncInput2","cropCircInput2","alienSigInput2","abdEvntInput2","Failed to submit data. Check that it is correct according to the input in the boxes!","red",1);
            }
        }
        xhr.open('PUT', url + (/\?/.test(url) ? "&" : "?") + new Date().getTime(), true);
    
        let _longitude = parseFloat(document.getElementById("lonInput2").value);
        let _latitude = parseFloat(document.getElementById("latInput2").value);
        let _yearSeen = parseFloat(document.getElementById("yearInput2").value);
        let _cowIncident = document.getElementById("cowIncInput2").value;
        let _cropCircle = document.getElementById("cropCircInput2").value;
        let _alienSight = document.getElementById("alienSigInput2").value;
        let _abductionEvent = document.getElementById("abdEvntInput2").value;

        var UFOSighting = 
        {
            longitude : _longitude,
            latitude : _latitude,
            yearSeen : _yearSeen,
            cowIncident: _cowIncident,
            cropCircle: _cropCircle,
            alienSight: _alienSight,
            abductionEvent: _abductionEvent
        };
    
        xhr.send(JSON.stringify(UFOSighting));
    });
    }
    
    deleteUFOSighting()
    {
        return new Promise(function(resolve, reject)
        {
            let xhr = new XMLHttpRequest();
            let url = '/Assesment/ufo';
        xhr.onload = function()
        {
            if(this.status == 204)
            {
               resolve();
               Requests.showResult("lonInput3","latInput3","","","","","","Data Removed!","green",0);
            }
            else
            {
                reject();
                Requests.showResult("lonInput3","latInput3","","","","","","Failed to submit data. Check that it is correct according to the input in the boxes!","red",0);
            }
        }
        xhr.open('DELETE', url + (/\?/.test(url) ? "&" : "?") + new Date().getTime(), true);
    
    
        let _longitude = parseFloat(document.getElementById("lonInput3").value);
        let _latitude = parseFloat(document.getElementById("latInput3").value);

        var UFOSighting = 
        {
            longitude : _longitude,
            latitude : _latitude,

        };
    
        xhr.send(JSON.stringify(UFOSighting));
    });
    }

    static showResult(lon,lat,yr,cI,cC,aS,aE, _resultText,col,index)
    {
        document.getElementById(lon).value = "";
        document.getElementById(lat).value = "";
        if(index == 1)
        {
        document.getElementById(yr).value = "";
        document.getElementById(cI).value = "";
        document.getElementById(cC).value = "";
        document.getElementById(aS).value = "";
        document.getElementById(aE).value = "";
        }
        var resultText = document.getElementById("result");
        resultText.style.color = col;
        resultText.innerHTML = _resultText;
    }
    


}