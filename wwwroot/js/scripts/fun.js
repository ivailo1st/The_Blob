
element = document.getElementById("js-fun-item");


element.addEventListener("click", function (e) {
  e.preventDefault;
  var audio = new Audio('../../audio/Laugh.mp3');
  audio.play();

  // -> removing the class
  element.classList.remove("bounce");
  void element.offsetWidth;
  // -> and re-adding the class
  element.classList.add("bounce");

  setTimeout(function () { element.classList.remove("bounce"); }, 1500);


  giveMoney();

  fetch("api/CharacterAPI/" + CharacterId)
    .then(response => response.json())
    .then(data => {
      let newHunger = data.hunger;
      let newSleep = data.sleep;
      let newFun = Math.min((data.fun + 10), 100);
      let newLogDate = data.logDate;

      let updateBars = [
        newHunger,
        newSleep,
        newFun,
        newLogDate
      ]
      fetch("api/CharacterAPI/", {
        method: "PATCH",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify(updateBars)
      })
        .then(response => response.json())
        .then(json => console.log(json)).catch(err => console.log(err));
    })
}, false);