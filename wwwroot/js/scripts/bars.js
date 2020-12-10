let hungerBar = document.getElementsByClassName('hunger-svg');
let sleepBar = document.getElementsByClassName('sleep-svg');
let funBar = document.getElementsByClassName('hunger-svg');

window.onload = function () {
  updateTime();
};

setInterval(updateTime, 60000);

let CharacterId = document.getElementById('js-char-value').innerHTML;


function ifAsleep() {
  fetch("api/CharacterAPI/" + CharacterId)
    .then(response => response.json())
    .then(data => {
      let newAwake = !data.awake;
      if (newAwake) {
        document.getElementById("bottomSleepIcon").style.filter = "brightness(0.75)";
        document.getElementById("js-face").style.backgroundImage = "none";
        document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sleeping.svg')";
      }
      else {
        document.getElementById("bottomSleepIcon").style.filter = "brightness(1)";
        document.getElementById("js-face").style.backgroundImage = "none";

        if (data.hunger >= 80 || data.sleep >= 80 || data.fun >= 80) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/happy.svg')";
        }
        else if ((data.hunger >= 50 || data.sleep >= 50 || data.fun >= 50) && (data.hunger < 80 || data.sleep < 80 || data.fun < 80)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/average.svg')";
        }
        else if ((data.hunger >= 30 || data.sleep >= 30 || data.fun >= 30) && (data.hunger < 50 || data.sleep < 50 || data.fun < 50)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sad.svg')";
        }
        else if ((data.hunger >= 1 || data.sleep >= 1 || data.fun >= 1) && (data.hunger < 30 || data.sleep < 30 || data.fun < 30)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/angry.svg')";
          sendEmail()
        }
      }
    });
}


function goToSleep() {
  fetch("api/CharacterAPI/" + CharacterId)
    .then(response => response.json())
    .then(data => {
      let newAwake = !data.awake
      if (newAwake) {
        document.getElementById("bottomSleepIcon").style.filter = "brightness(1)";
        document.getElementById("js-face").style.backgroundImage = "none";

        if (data.hunger >= 80 || data.sleep >= 80 || data.fun >= 80) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/happy.svg')";
        }
        else if ((data.hunger >= 50 || data.sleep >= 50 || data.fun >= 50) && (data.hunger < 80 || data.sleep < 80 || data.fun < 80)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/average.svg')";
        }
        else if ((data.hunger >= 30 || data.sleep >= 30 || data.fun >= 30) && (data.hunger < 50 || data.sleep < 50 || data.fun < 50)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sad.svg')";
        }
        else if ((data.hunger >= 1 || data.sleep >= 1 || data.fun >= 1) && (data.hunger < 30 || data.sleep < 30 || data.fun < 30)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/angry.svg')";
          sendEmail()
        }
      }
      else {
        document.getElementById("bottomSleepIcon").style.filter = "brightness(0.75)";
        document.getElementById("js-face").style.backgroundImage = "none";
        document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sleeping.svg')";
      }
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

      let element = document.getElementById("js-increase-hunger");
      let fun = document.getElementById("js-increase-fun");
      let sleep = document.getElementById("js-increase-sleep");
      element.classList.remove("barfill");
      fun.classList.remove("barfill");
      sleep.classList.remove("barfill");

      element.style.transform = "scaleY(" + data.hunger / 100 + ")";
      fun.style.transform = "scaleY(" + data.fun / 100 + ")";
      sleep.style.transform = "scaleY(" + data.sleep / 100 + ")";

      void element.offsetWidth;
      void fun.offsetWidth;
      void sleep.offsetWidth;
      // -> and re-adding the class

      setTimeout(function () {

        element.classList.add("barfill");
        fun.classList.add("barfill");
        sleep.classList.add("barfill");

      }, 6)

      console.log(data);
      if (data.awake) {
        if (data.hunger >= 80 || data.sleep >= 80 || data.fun >= 80) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/happy.svg')";
        }
        else if ((data.hunger >= 50 || data.sleep >= 50 || data.fun >= 50) && (data.hunger < 80 || data.sleep < 80 || data.fun < 80)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/average.svg')";
        }
        else if ((data.hunger >= 30 || data.sleep >= 30 || data.fun >= 30) && (data.hunger < 50 || data.sleep < 50 || data.fun < 50)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sad.svg')";
        }
        else if ((data.hunger >= 1 || data.sleep >= 1 || data.fun >= 1) && (data.hunger < 30 || data.sleep < 30 || data.fun < 30)) {
          document.getElementById("js-face").style.backgroundImage = "none";
          document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/angry.svg')";
          sendEmail();
        }
      } else {
        document.getElementById("bottomSleepIcon").style.filter = "brightness(0.75)";
        document.getElementById("js-face").style.backgroundImage = "none";
        document.getElementById("js-face").style.backgroundImage = "url('/pics/faces/sleeping.svg')";
      }

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
        newSleep = Math.min((data.sleep + (hour * 10)), 100);
        giveMoney()

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

      };



    });






}
