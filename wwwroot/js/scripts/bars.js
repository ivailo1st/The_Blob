let hungerBar = document.getElementsByClassName('hunger-svg');
let sleepBar = document.getElementsByClassName('sleep-svg');
let funBar = document.getElementsByClassName('hunger-svg');

// fetch all the data about character
fetch("api/CharacterAPI/3")
  .then(response => response.json())
  .then(data => {

    setInterval(updateTime, 36000)

    function updateTime() {
      let date = new Date("2020-12-06T08:00:00");
      let setDate = date.getTime();
      let currentDate = new Date();
      let newDate = currentDate.getTime();

      let change = newDate - setDate;

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
      let newLogDate = '';

      //If the blob is awake all bars decreasing
      if ((hour >= 1) && (data.awake = true)) {
        newHunger = data.hunger - (hour + '0');
        newSleep = data.sleep - (hour + '0');
        newFun = data.fun - (hour + '0');
        newLogDate = new Date(); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        console.log(
          newHunger, newLogDate);

        //If the bars are above 0 decrease bars
        if (newSleep >= 1 && newFun >= 1 && newHunger >= 1) {
          let updateBars = {
            hunger: newHunger,
            sleep: newSleep,
            fun: newFun,
          }
          fetch("api/CharacterAPI/3", {
            method: "PUT",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)
          })
            .then(response => response.json())
            .then(json => console.log(json)).catch(err => console.log(err));

          // If bar values are lower then 1 set to 1 - so blob doesnt die (for now :D)
        } else {
          newHunger = 1;
          newSleep = 1;
          newFun = 1;

          let updateBars = {
            hunger: 1,
            sleep: 1,
            fun: 1,
          }
          console.log(
            newHunger);

          fetch("api/CharacterAPI/3", {
            method: "PUT",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)
          })
        };

        //If blob is sleeping decrease only fun and hunger + increase sleep
      } else if ((hour >= 1) && (data.awake = false)) {
        newHunger = data.hunger - (hour + '0');
        newFun = data.fun - (hour + '0');
        newSleep = data.sleep + (hour + '0');
        newLogDate = newDate;

        // Allow increase until sleep is 100
        if (newSleep <= 100) {
          let updateBars = {
            hunger: newHunger,
            fun: newFun,
            sleep: newSleep,
            logDate: newLogDate
          }
          fetch("api/CharacterAPI/3", {
            method: "PUT",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)
          })
          // Dont allow the sleep to increase over 100
        } else {
          let updateBars = {
            hunger: newHunger,
            fun: newFun,
            sleep: 100,
            logDate: newLogDate
          }
          fetch("api/CharacterAPI/3", {
            method: "PUT",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(updateBars)

          })
        };
        console.log(
          newHunger, newLogDate
        );
      }
    };
  });

