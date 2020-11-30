
var Character = document.getElementById('Character');
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

var Shadow = document.getElementById('Shadow');
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







