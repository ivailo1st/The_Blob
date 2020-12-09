function openFridge() {
  document.getElementById('js-fridge-element').style.display = "block";
  document.getElementById('js-store-element').style.display = "none";
}

// function allowDrop(ev) {
//   ev.preventDefault();
// }

// function drag(ev) {
//   ev.dataTransfer.setData("image", ev.target.id);
// }

// function drop(ev) {
//   ev.preventDefault();
//   var item = ev.dataTransfer.getData("image");
//   ev.target.appendChild(document.getElementById(item));
// }

function updateItem(FridgeId) {
  fetch("api/fridgeapi/" + FridgeId)
    .then(response => response.json())
    .then(data => {
      console.log(data)

      if (data.quantity > 1) {
        console.log(data.quantity)
        let newQuantity = data.quantity - 1;
        fetch("api/fridgeapi" + FridgeId + newQuantity, {
          method: "PATCH",
          headers: { "Content-Type": "application/json", "Accept": "application/json" },
        })
          .then(response => response.json())
          .then(json => console.log(json)).catch(err => console.log(err));
        document.getElementById("js-fridge-quantity").innerHTML = newQuantity;
      }
      else if (data.quantity = 1) {
        console.log('nope')
        fetch("api/fridgeapi/" + FridgeId, {
          method: "DELETE",
          headers: { "Content-Type": "application/json", "Accept": "application/json" }
        })
          .then(response => response.json())
          .then(json => console.log(json)).catch(err => console.log(err));
        document.getElementById("js-fridge-item").style.display = "none";
      }
    })
}

