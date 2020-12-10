﻿function openStore() {
  fetch('/Store/Store')
    .then(function (response) {
      return response.text();
    })
    .then(function (body) {
      document.getElementById("storeSpace").innerHTML = body;
      document.getElementById('js-store-element').style.display = "block";
      document.getElementById('js-fridge-element').style.display = "none";
    });
};

function closeElement(element) {
  if (element == "store") {
    document.getElementById('js-store-element').style.display = "none";
  }
  else if (element == "fridge") {
    document.getElementById('js-fridge-element').style.display = "none";
  }
}

function addItemSignal() {
  document.getElementById("id-item-signal").style.display = "block";
  setTimeout(closeItemSignal, 800);
}

function closeItemSignal() {
  document.getElementById("id-item-signal").style.display = "none";
}

function addItem(itemName, itemPercent, itemURL, itemPrice, charID) {
  //Currency Deduction
  let userCurrency = parseInt(document.getElementById("textCurrency").innerHTML);
  let newCurrency = userCurrency - itemPrice;
  console.log(newCurrency);
  if (newCurrency >= 0) {
    //Fetch for Updating Character Currency
    fetch("api/characterapi/" + newCurrency, {
      method: "PATCH",
      headers: { "content-type": "application/json", "accept": "application/json" }

    })
      .then(response => response.json())
      .then(json => console.log(json)).catch(err => console.log(err));
    document.getElementById("textCurrency").innerHTML = newCurrency;
    addItemSignal();

    //Fridge Item Logic
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
        console.log(data)
        let fridgeID = data.fridgeId
        //Fetch for finding CharacterFridge item
        fetch("api/CharacterFridgeAPI/" + fridgeID).then(response => response.json())
          .then(data => {
            if ((data.characterId != null) && (data.fridgeId != null)) {
              //Fetch for Updating a Fridge Item
              fetch("api/fridgeapi", {
                method: "PUT",
                headers: { "Content-Type": "application/json", "Accept": "application/json" },
                body: JSON.stringify(addQuery)

              })
                .then(response => response.json())
                .then(data => console.log(data)).catch(err => console.log(err));
            }
            else {
              //Fetch for Creating a new Fridge Item
              fetch("api/fridgeapi", {
                method: "POST",
                headers: { "Content-Type": "application/json", "Accept": "application/json" },
                body: JSON.stringify(addQuery)

              })
                .then(response => response.json())
                .then(data => {
                  fetch("api/fridgeapi/specific/" + itemName)
                    .then(response => response.json())
                    .then(data => {
                      console.log(data.fridgeId);

                      let junctionQuery = {
                        characterId: charID,
                        character: null,
                        fridgeId: data.fridgeId,
                        fridge: null
                      }
                      //Fetch for Creating new CharacterFridge item
                      fetch("api/CharacterFridgeAPI", {
                        method: "POST",
                        headers: { "Content-Type": "application/json", "Accept": "application/json" },
                        body: JSON.stringify(junctionQuery)

                      })
                    });
                });
            }
          });
      });
  }
  else {
    alert("Not Enough Money");
  }

}