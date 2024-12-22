const ctxCalories = document.getElementById('caloriesChart').getContext('2d');
const caloriesChart = new Chart(ctxCalories, {
    type: 'line',
    data: {
        labels: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
        datasets: [{
            label: 'Сожженные калории',
            data: [300, 600, 500, 900, 200, 600, 800],
            borderColor: 'rgba(52, 179, 160, 1)',
            borderWidth: 2,
            fill: false,
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

const ctxWorkouts = document.getElementById('workoutsChart').getContext('2d');
const workoutsChart = new Chart(ctxWorkouts, {
    type: 'bar',
    data: {
        labels: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
        datasets: [{
            label: 'Тренировки',
            data: [1, 2, 3, 0, 1, 4, 2],
            backgroundColor: 'rgba(52, 179, 160, 0.2)',
            borderColor: 'rgba(52, 179, 160, 1)',
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});