﻿
function addItem(itemName, itemPercent, itemURL, itemPrice,charID) {
    let addQuery = {
        foodName: itemName,
        hungerPercentage: itemPercent,
        quantity: 1,
        imageURL: itemURL
    }
    //Fetch for Finding a Fridge Item
    fetch("api/fridgeapi/" + itemName)
        .then(response => response.json())
        .then(data => {
            if (data.foodName != null) {
                //Fetch for Updating a Fridge Item
                fetch("api/fridgeapi", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json", "Accept": "application/json" },
                    body: JSON.stringify(addQuery)

                })
                    .then(response => response.json())
                    .then(json => console.log(json)).catch(err => console.log(err));
            }
            else {
                //Fetch for Creating a new Fridge Item
                fetch("api/fridgeapi", {
                    method: "POST",
                    headers: { "Content-Type": "application/json", "Accept": "application/json" },
                    body: JSON.stringify(addQuery)

                })
                    .then(response => response.json())
                    .then(json => {
                       //Fetch for fridgeID
                        fetch("api/fridgeapi/" + itemName)
                            .then(response => response.json())
                            .then(data => {
                                let fridgeID = data.fridgeId
                                //Fetch for finding CharacterFridge item
                                fetch("api/CharacterFridgeAPI/" + fridgeID).then(response => response.json())
                                    .then(data => {
                                        if ((data.characterId == null) && (data.fridgeId == null)) {
                                            let junctionQuery = {
                                                characterId: charID,
                                                character: null,
                                                fridgeId: fridgeID,
                                                fridge: null
                                            }
                                            //Fetch for Creating new CharacterFridge item
                                            fetch("api/CharacterFridgeAPI", {
                                                method: "POST",
                                                headers: { "Content-Type": "application/json", "Accept": "application/json" },
                                                body: JSON.stringify(junctionQuery)

                                            })
                                                .then(response => response.json())
                                                .then(json => console.log(json)).catch(err => console.log(err));
                                        }
                                    });

                            });
                    }).catch(err => console.log(err));
            }
        });
    
    //let userCurrency = document.getElementById("textCurrency").innerHTML;
    //let deductQuery = {
    //    currency: userCurrency - itemPrice
    //}
    //console.log(JSON.stringify(deductQuery));
    //fetch("api/characterapi/" + charID, {
    //    method: "PUT",
    //    headers: { "Content-Type": "application/json", "Accept": "application/json" },
    //    body: JSON.stringify(deductQuery)

    //})
    //    .then(response => response.json())
    //    .then(json => console.log(json)).catch(err => console.log(err));
}