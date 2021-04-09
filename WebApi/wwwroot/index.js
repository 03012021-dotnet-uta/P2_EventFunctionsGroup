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
        name: "Test",
        Date: "2021-04-16T18:11",
        description: "This is just a test",
        Capacity: 50,
        street: "1111 S Figueroa St",
        zip: 90015,
        city: "Los Angeles",
        state: "CA",
        eventtype: 'ea3bf8bf-5e12-45b0-bd9a-209defc23e9c',
        managerid: '28318984-7a0b-4cac-a61d-c0c2471acdb9'
    }
    let events = ['Sports', 'Music', 'Private', 'Conference', 'Expo', 'Other']
    let addressjson = JSON.stringify(address);

    await fetch(`api/Manager/createevent`, {
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
    await fetch(`api/Event/allupcoming`)
    .then(response => {
        if(!response.ok) {
            throw new Error(`Network reponse was not ok (${reponse.status})`);
        }
        else
            return response.json();
    })
    .then((jsonReponse) => {
        console.log(jsonReponse);
        mapReponse = jsonReponse;
    })
    .catch(function(err) {
        console.log("Failed to fetch page: ", err);
    });
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
    let stylejson = `https://api.mapbox.com/styles/v1/notasalad?${mapboxtoken}`;
    let pin = `pin-s-l+000(${mapReponse.features[0].center[0]},${mapReponse.features[0].center[1]})`
    let thediv = document.getElementById("map");
    let pic = document.createElement("img");
    pic.src = `https://api.mapbox.com/styles/v1/mapbox/streets-v11/static/${pin}/${mapReponse.features[0].center[0]}, ${mapReponse.features[0].center[1]}, 15, 0/400x400?${mapboxtoken}`;
    thediv.appendChild(pic);

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

