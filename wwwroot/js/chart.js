
    const ctx = document.getElementById('revenueChart').getContext('2d');
    const chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4'],
            datasets: [{
                label: 'Doanh thu',
                data: [12000000, 15000000, 18000000, 20000000],
                borderColor: 'rgba(75, 192, 192, 1)',
                fill: true,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                tension: 0.3
            }]
        }
    });
