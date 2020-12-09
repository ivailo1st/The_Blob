let hungerBar = document.getElementsByClassName('hunger-svg');
let sleepBar = document.getElementsByClassName('sleep-svg');
let funBar = document.getElementsByClassName('hunger-svg');


setInterval(updateTime, 360000);

let CharacterId = document.getElementById('js-char-value').innerHTML;

function goToSleep() {
  fetch("api/CharacterAPI/" + CharacterId)
    .then(response => response.json())
    .then(data => {
      let newAwake = !data.awake;
      fetch("api/CharacterAPI/Bars/" + newAwake, {
        method: "PATCH",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
      });
    });
}

function updateTime() {
  // fetch all the data about character
  fetch("api/CharacterAPI/" + CharacterId)
    .then(response => response.json())
    .then(data => {

      let setDate = data.logDate;
      let currentDate = new Date();
      let newLogDate = currentDate.getTime();
      let change = newLogDate - setDate;
      console.log(change)
      let hour, minute, seconds;
      seconds = Math.floor(change / 1000);
      minute = Math.floor(seconds / 60);
      seconds = seconds % 60;
      hour = Math.floor(minute / 60);
      minute = minute % 60;

      console.log(
        'hour:' + hour,
        'minute:' + minute

      );

      console.log(data);

      let newHunger;
      let newSleep;
      let newFun;

      //If the blob is awake all bars decreasing
      if ((hour >= 1) && (data.awake == true)) {
        newHunger = Math.max((data.hunger - (hour * 10)), 1);
        newSleep = Math.max((data.sleep - (hour * 10)), 1);
        newFun = Math.max((data.fun - (hour * 10)), 1);

        console.log(newHunger, newLogDate);

        //If the bars are above 0 decrease bars
          let updateBars = [
            newHunger,
            newSleep,
            newFun,
            newLogDate
          ]
          console.log(updateBars)
          fetch("api/CharacterAPI/", {
            method: "PATCH",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)
          })
            .then(response => response.json())
            .then(json => console.log(json)).catch(err => console.log(err));


          // If bar values are lower then 1 set to 1 - so blob doesnt die (for now :D)

        //If blob is sleeping decrease only fun and hunger + increase sleep
      } else if ((hour >= 1) && (data.awake == false)) {
            newHunger = Math.max((data.hunger - (hour * 10)), 1);
            newFun = Math.max((data.fun - (hour * 10)), 1);
            newSleep = Math.min((data.sleep + (hour * 10)),100);

            let updateBars = [
                newHunger,
                newFun,
                newSleep,
                newLogDate
            ]

            fetch("api/CharacterAPI/", {
                method: "PATCH",
                headers: { "Content-Type": "application/json", "Accept": "application/json" },
                body: JSON.stringify(updateBars)
            });
      }
    });
}

