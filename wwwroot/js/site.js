document.addEventListener('DOMContentLoaded', function () {
    const menuToggle = document.getElementById('menuToggle');
    const slideMenu = document.getElementById('slideMenu');
    const menuClose = document.getElementById('menuClose');

    if (menuToggle && slideMenu && menuClose) {
        // Toggle menu
        menuToggle.addEventListener('click', () => {
            const isVisible = window.getComputedStyle(slideMenu).display === 'block';
            slideMenu.style.display = isVisible ? 'none' : 'block';
        });

        // Close menu when clicking the close button
        menuClose.addEventListener('click', () => {
            slideMenu.style.display = 'none';
        });

        // OPTIONAL: Close menu if click outside
        document.addEventListener('click', (e) => {
            if (!slideMenu.contains(e.target) && !menuToggle.contains(e.target)) {
                slideMenu.style.display = 'none';
            }
        });
    }
});
