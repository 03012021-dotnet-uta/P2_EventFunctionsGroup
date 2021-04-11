const button = document.getElementById("add");

let imglink = "";
var mapReponse;
let mapboxurl = "https://api.mapbox.com";
let mapboxtoken = "access_token=sk.eyJ1Ijoibm90YXNhbGFkIiwiYSI6ImNrbmFucHY5MDBmNWgydmxsZTBsdmZrY3oifQ._ZzlvInwLBJvA6gGIqdLPA"

button.addEventListener("click", async () => {
    let user = {
        firstName: "Another",
        lastName: "Test",
        email: "abc123@gmail.com",
        password: "123456"
    }
    let address = {
        name: "Random Event",
        Date: "2021-04-16T18:11",
        description: "This is a test for zipcode fix",
        Capacity: 10000,
        street: "73 Iroquois Lane",
        ZipCode: 11710,
        city: "Bellmore",
        state: "NY",
        eventtype: 'a9ea5da4-07ec-40c4-be96-9b1c204d0899',
        managerid: '28318984-7a0b-4cac-a61d-c0c2471acdb9'
    }

    let userSignup = {
        uid: "c3955591-4d58-4d2d-adb9-8469957bf6b8",
        eid: "c2123ab5-f0c0-4abc-90d8-017e6bdce141"
    }
    let events = ['Sports', 'Music', 'Private', 'Conference', 'Expo', 'Other']
    let addressjson = JSON.stringify(address);

    await fetch('api/Manager/createevent', {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type':'application/json'
    },
    body: JSON.stringify(address),
    })
    .then(response => {
        if(!response.ok) {
            throw new Error(`Network reponse was not ok (${reponse.status})`);
        }
        else
            return response.json();
    })
    .then((jsonReponse) => {
        console.log(jsonReponse);
    })
    .catch(function(err) {
        console.log("Failed to fetch page: ", err);
    });

    // await fetch(`api/Event/signup/${userSignup.uid}/${userSignup.eid}`, {
    // })
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    //     //mapReponse = jsonReponse;
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });

    await fetch(`api/Event/allsigned/${userSignup.uid}`, {
    })
    .then(response => {
        if(!response.ok) {
            throw new Error(`Network reponse was not ok (${reponse.status})`);
        }
        else
            return response.json();
    })
    .then((jsonReponse) => {
        console.log(jsonReponse);
        //mapReponse = jsonReponse;
    })
    .catch(function(err) {
        console.log("Failed to fetch page: ", err);
    });

    await fetch(`api/Manager/allattend/${userSignup.eid}`, {
    })
    .then(response => {
        if(!response.ok) {
            throw new Error(`Network reponse was not ok (${reponse.status})`);
        }
        else
            return response.json();
    })
    .then((jsonReponse) => {
        console.log(jsonReponse);
        //mapReponse = jsonReponse;
    })
    .catch(function(err) {
        console.log("Failed to fetch page: ", err);
    });

    // await fetch(`${mapboxurl}/geocoding/v5/mapbox.places/${address.street}%20${address.zip}%20${address.city}%20${address.state}.json?country=US&${mapboxtoken}`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    //     mapReponse = jsonReponse;
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    // await fetch(`api/Event/allupcoming`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    //     //mapReponse = jsonReponse;
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    //mapbox://styles/mapbox/streets-v11
    //await fetch(`${mapboxurl}/styles/v1/mapbox/streets-v11/static/${mapReponse.features[0].bbox}/400x400?${mapboxtoken}`)
    // await fetch(`https://api.mapbox.com/styles/v1/notasalad?${mapboxtoken}`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    // mapbox://styles/mapbox/streets-v11
    //let stylejson = `https://api.mapbox.com/styles/v1/notasalad?${mapboxtoken}`;
    //let pin = `pin-s-l+000(${mapReponse.features[0].center[0]},${mapReponse.features[0].center[1]})`
    let thediv = document.getElementById("map");
    let pic = document.createElement("img");
    //pic.src = `https://api.mapbox.com/styles/v1/mapbox/streets-v11/static/${pin}/${mapReponse.features[0].center[0]}, ${mapReponse.features[0].center[1]}, 15, 0/400x400?${mapboxtoken}`;
    //thediv.appendChild(pic);
    
    // await fetch(`https://api.mapbox.com/v4/mapbox.mapbox-streets-v8/1/0/0.mvt?${mapboxtoken}`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    //     //pic.src = jsonReponse.locationMap;
    //     //thediv.appendChild(pic);
    //     //mapReponse = jsonReponse;
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    // await fetch(`api/Event/eventdetail/a52d49d4-58a2-4488-a13a-9e6a1c9725a6`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    //     pic.src = jsonReponse.locationMap;
    //     thediv.appendChild(pic);
    //     //mapReponse = jsonReponse;
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    // await fetch('api/Test/typeinit', {
    //     method: 'POST',
    //     headers: {
    //         'Accept': 'application/json',
    //         'Content-Type':'application/json'
    //     },
    //     body: JSON.stringify(events),
    // })
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });

    // console.log("---------------------")
    // fetch('https://eventfunctionsp2.azurewebsites.net/api/User/all')
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log(jsonReponse);
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
    // console.log("---------------------")
    // fetch(`https://eventfunctionsp2.azurewebsites.net/api/User/login/${user.email}/${user.password}`)
    // .then(response => {
    //     if(!response.ok) {
    //         throw new Error(`Network reponse was not ok (${reponse.status})`);
    //     }
    //     else
    //         return response.json();
    // })
    // .then((jsonReponse) => {
    //     console.log("Got follow user: ")
    //     console.log(jsonReponse);
    // })
    // .catch(function(err) {
    //     console.log("Failed to fetch page: ", err);
    // });
})

