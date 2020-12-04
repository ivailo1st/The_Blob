
let Character = document.getElementById('js-character');
Character.animate(
  [
    { transform: 'translateY(opx)' },
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

var angry = "~/pics/faces/angry.svg";
var happy = "~/pics/faces/happy.svg";
var average = "~/pics/faces/average.svg";
var sleeping = "~/pics/faces/sleeping.svg";
var sad = "~/pics/faces/sad.svg";

console.log('hello');







