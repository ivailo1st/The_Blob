
element = document.getElementById("js-fun-item");

element.addEventListener("click", function (e) {
  e.preventDefault;

  // -> removing the class
  element.classList.remove("bounce");
  void element.offsetWidth;
  // -> and re-adding the class
  element.classList.add("bounce");



  giveMoney();

}, false);