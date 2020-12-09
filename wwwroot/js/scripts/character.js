
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

function giveMoney() {
    let userCurrency = document.getElementById("textCurrency").innerHTML;
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






