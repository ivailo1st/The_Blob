function openFridge() {
  document.getElementById('js-fridge-element').style.display = "block";
  document.getElementById('js-store-element').style.display = "none";
}


function updateItem(FridgeId) {
  var audio = new Audio('../../audio/Eating.mp3');
  audio.play();
  console.log(FridgeId);
  fetch("api/fridgeapi/fridge/" + FridgeId)
    .then(response => response.json())
    .then(data => {
      console.log(data);
      let foodPercent = data.hungerPercentage;
      if (data.quantity > 1) {
        console.log("First Condition: " + data.quantity);
        let newQuantity = data.quantity - 1;

        fetch("api/fridgeapi/" + FridgeId, {
          method: "PATCH",
          headers: { "Content-Type": "application/json", "Accept": "application/json" },
        })
          .then(response => response.json())
          .then(json => console.log(json)).catch(err => console.log(err));
        document.getElementById("js-fridge-quantity").innerHTML = newQuantity;
        giveMoney();

        fetch("api/CharacterAPI/" + CharacterId)
          .then(response => response.json())
          .then(data => {

            let newHunger = Math.min((data.hunger + foodPercent), 100);
            let updateHunger = [
              newHunger
            ]
            fetch("api/CharacterAPI/", {
              method: "PATCH",
              headers: { "Content-Type": "application/json", "Accept": "application/json" },
              body: JSON.stringify(updateHunger)
            })
              .then(response => response.json())
              .then(json => console.log(json)).catch(err => console.log(err));

          })
      }
      else if (data.quantity = 1) {
        console.log('quantity = 1')
        fetch("api/fridgeapi/" + FridgeId, {
          method: "DELETE",
          headers: { "Content-Type": "application/json", "Accept": "application/json" }
        })
          .then(response => response.json())
          .then(json => console.log(json)).catch(err => console.log(err));
        giveMoney();

        fetch("api/CharacterAPI/" + CharacterId)
          .then(response => response.json())
          .then(data => {

            let newHunger = Math.min((data.hunger + data.hungerPercentage), 100);
            let updateHunger = [
              newHunger
            ]
            fetch("api/CharacterAPI/", {
              method: "PATCH",
              headers: { "Content-Type": "application/json", "Accept": "application/json" },
              body: JSON.stringify(updateHunger)
            })
              .then(response => response.json())
              .then(json => console.log(json)).catch(err => console.log(err));

          })
      }
    })
}

