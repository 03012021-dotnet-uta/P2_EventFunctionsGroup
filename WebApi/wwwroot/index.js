const button = document.getElementById("add");

button.addEventListener("click", () => {
    let user = {
        firstName: "Another",
        lastName: "Test",
        email: "testmail@gmail.com",
        password: "Nick"
    }

    fetch('https://eventfunctionsp2.azurewebsites.net/api/User/register', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type':'application/json'
        },
        body: JSON.stringify(user),
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

    console.log("---------------------")
    fetch('https://eventfunctionsp2.azurewebsites.net/api/User/all')
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
})

