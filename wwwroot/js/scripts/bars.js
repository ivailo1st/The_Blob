let domainUrl = window.location.hostname;
console.log(domainUrl);


fetch("api/CharacterAPI/3")
  .then(response => response.json())
  .then(data => console.log(data));

fetch("api/CharacterAPI/3", {
  method: 'PUT'
})
  .then(response => response.json())
  .then(data => console.log(data));



let hungerBar = document.getElementsByClassName('hunger-svg');
let sleepBar = document.getElementsByClassName('sleep-svg');
let funBar = document.getElementsByClassName('hunger-svg');

let date = new Date("2020-12-01T08:00:51.01")
let setDate = date.getTime();
console.log(setDate);
let currentDate = new Date();
let newDate = currentDate.getTime();
console.log(newDate);

let change = newDate - setDate; L
console.log(change);

let day, hour, minute, seconds;
seconds = Math.floor(change / 1000);
minute = Math.floor(seconds / 60);
seconds = seconds % 60;
hour = Math.floor(minute / 60);
minute = minute % 60;

console.log(
  'hour:' + hour,
  'minute:' + minute,
  'seconds:' + seconds
)
