let hungerBar = document.getElementsByClassName('hunger-svg');
let sleepBar = document.getElementsByClassName('sleep-svg');
let funBar = document.getElementsByClassName('hunger-svg');

setInterval(updateTime, 2000)
let CharacterId = document.getElementById('js-char-value').innerHTML;

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
      )

      console.log(data);

      let newHunger = '';
      let newSleep = '';
      let newFun = '';

      //If the blob is awake all bars decreasing
      if ((hour >= 1) && (data.awake = true)) {
        newHunger = data.hunger - (hour + '0');
        newSleep = data.sleep - (hour + '0');
        newFun = data.fun - (hour + '0');

        console.log(
          newHunger, newLogDate);

        //If the bars are above 0 decrease bars
        if (newSleep >= 1 && newFun >= 1 && newHunger >= 1) {
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
        } else {
          let updateBars = [
            1,
            1,
            1,
            newLogDate
          ]

          fetch("api/CharacterAPI/", {
            method: "PATCH",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)
          })
        };

        //If blob is sleeping decrease only fun and hunger + increase sleep
      } else if ((hour >= 1) && (data.awake = false)) {
        newHunger = data.hunger - (hour + '0');
        newFun = data.fun - (hour + '0');
        newSleep = data.sleep + (hour + '0');


        // Allow increase until sleep is 100
        if (newSleep <= 100) {
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
          })
          // Dont allow the sleep to increase over 100
        } else {
          let updateBars = [
            newHunger,
            newFun,
            100,
            newLogDate

          ]
          fetch("api/CharacterAPI/", {
            method: "PATCH",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)

          })
        };

      }
    });
};

