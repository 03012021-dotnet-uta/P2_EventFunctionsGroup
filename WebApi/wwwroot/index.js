const button = document.getElementById("add");

button.addEventListener("click", async () => {
    let user = {
        firstName: "Another",
        lastName: "Test",
        email: "abc123@gmail.com",
        password: "123456"
    }
    let events = ['Sports', 'Music', 'Private', 'Conference', 'Expo', 'Other']

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

