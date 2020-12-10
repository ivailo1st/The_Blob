
let Face = document.getElementById('js-face');
Face.animate(
  [
    { transform: 'translateY(0px)' },
    { transform: 'translateY(-15px)' }
  ],
  {
    duration: 2500,
    iterations: Infinity,
    direction: 'alternate'
  }

);

let Character = document.getElementById('js-character');
Character.animate(
  [
    { transform: 'translateY(0px)' },
    { transform: 'translateY(-15px)' }
  ],
  {
    duration: 2500,
    iterations: Infinity,
    direction: 'alternate'
  }

);

let Shadow = document.getElementById('js-shadow');
Shadow.animate(
  [
    { transform: 'scale(1)' },
    { transform: 'scale(0.75)' }
  ],
  {
    duration: 2500,
    iterations: Infinity,
    direction: 'alternate'
  }

);

function setBars(hunger, sleep, fun) {
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

      let updateBars = [
        hunger,
        sleep,
        fun,
        newLogDate
      ]

      console.log(updateBars)
      fetch("api/CharacterAPI/", {
        method: "PATCH",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify(updateBars)
      })
    });
}

function giveMoney() {
  let userCurrency = parseInt(document.getElementById("textCurrency").innerHTML);
  let newCurrency = userCurrency + 50;
  //Fetch for Updating Character Currency
  fetch("api/characterapi/" + newCurrency, {
    method: "PATCH",
    headers: { "content-type": "application/json", "accept": "application/json" }

  })
    .then(response => response.json())
    .then(json => console.log(json)).catch(err => console.log(err));
  document.getElementById("textCurrency").innerHTML = newCurrency;
}



function sendEmail() {
  let userEmail = document.getElementById('js-user-email').innerHTML;
  console.log(userEmail)
  Email.send({
    Host: "	smtp.elasticemail.com",
    Username: "gabcoo1110@gmail.com",
    Password: "33C6D8AC709BCF19D3B7499A0628A1DD0052",
    To: userEmail,
    From: "gabcoo1110@gmail.com",
    Subject: "Your Blob is almost dead",
    Body: "Don't forget to take care of your Blob, it needs your love and attention.",
  }).then(
    message => alert(message)
  )
}




