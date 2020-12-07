function openFridge() {
  document.getElementById('js-fridge-element').style.display = "block";
}

function allowDrop(ev) {
  ev.preventDefault();
}

function drag(ev) {
  ev.dataTransfer.setData("image", ev.target.id);
}

fetch("api/fridgeapi/1")
  .then(response => response.json())
  .then(data => {
    if (data.quantity >= 1) {
      let updateQuantity = data.quantity - 1;
      let updateFridge = {
        quantity: updateQuantity
      }

      fetch("api/fridgeapi", {
        method: "PUT",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify(updateFridge)
      })
        .then(response => response.json())
        .then(json => console.log(json)).catch(err => console.log(err));

    } else {

      let updateFridge = {
        quantity: 0
      }

      fetch("api/fridgeapi", {
        method: "PUT",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify(updateFridge)
      })
        .then(response => response.json())
        .then(json => console.log(json)).catch(err => console.log(err));

      function drop(ev) {
        ev.preventDefault();
        var item = ev.dataTransfer.getData("image");
        ev.target.appendChild(document.getElementById(item));
      }
    }
  })


