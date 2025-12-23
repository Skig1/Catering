
document.addEventListener('DOMContentLoaded', function () {
const categories = document.querySelectorAll('.category-item');
const dishes = document.querySelectorAll('.dish-item');


function filterDishes(selectedCategory) {
    dishes.forEach(dish => {
        const dishCategory = dish.getAttribute('data-category');

        if (selectedCategory === 'all' || dishCategory === selectedCategory) {
            dish.style.display = 'flex';
        } else {
            dish.style.display = 'none';
        }
    });
}


categories.forEach(category => {
    category.addEventListener('click', function () {

        categories.forEach(cat => cat.classList.remove('active'));

  
        this.classList.add('active');


        const selectedCategory = this.getAttribute('data-category');
        filterDishes(selectedCategory);
    });
});


filterDishes('all');


if (categories.length > 0) {
    categories[0].classList.add('active');
}
});
