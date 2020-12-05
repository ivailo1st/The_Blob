
function addItem(itemName, itemPercent, itemURL) {
    var query = {
        foodName: itemName,
        hungerPercentage: itemPercent,
        quantity: 1,
        imageURL: itemURL
    }
    console.log(JSON.stringify(query));
    fetch("api/fridgeapi/" + itemName)
        .then(response => response.json())
        .then(data => {
            if (data.foodName != null) {
                fetch("api/fridgeapi", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json", "Accept": "application/json" },
                    body: JSON.stringify(query)

                })
                    .then(response => response.json())
                    .then(json => console.log(json)).catch(err => console.log(err));
            }
            else {
                fetch("api/fridgeapi", {
                    method: "POST",
                    headers: { "Content-Type": "application/json", "Accept": "application/json" },
                    body: JSON.stringify(query)

                })
                    .then(response => response.json())
                    .then(json => console.log(json)).catch(err => console.log(err));
            }
        });
}