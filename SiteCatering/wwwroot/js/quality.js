document.addEventListener('DOMContentLoaded', function () {
   
    const quantityInputs = document.querySelectorAll('input[name="quantity"]');

    quantityInputs.forEach(input => {
        input.addEventListener('change', function () {
            const dishId = this.getAttribute('data-dish-id');
            const quantity = parseInt(this.value);

            if (isNaN(quantity) || quantity < 1 || quantity > 99) {
                alert('Количество должно быть от 1 до 99.');
                return;
            }

            fetch('/Cart/UpdateQuantityAjax', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken':
                        document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: `dishId=${dishId}&quantity=${quantity}`
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        const row = this.closest('tr');
                        row.querySelector('.item-total').textContent = data.totalPrice;
                        document.getElementById('total-cart-price').textContent = data.totalCartPrice;
                    } else {
                        alert('Ошибка при обновлении корзины.');
                    }
                })
                .catch(error => {
                    console.error('Ошибка:', error);
                });
        });
    });

  
    const calculateButton = document.getElementById('calculateBtn'); 
    if (!calculateButton) {
        console.error('Кнопка с id="calculateBtn" не найдена в DOM. Добавь её в View.');
        return;
    }

    calculateButton.addEventListener('click', function () {
        const peopleCountElement = document.getElementById('peopleCount');
        if (!peopleCountElement) {
            alert('Элемент с id="peopleCount" не найден.');
            return;
        }

        const peopleCount = parseInt(peopleCountElement.value);
        if (isNaN(peopleCount) || peopleCount < 1) {
            alert('Количество человек должно быть числом ≥ 1.');
            return;
        }

        let foodWeight = 0, drinkWeight = 0;
        quantityInputs.forEach(input => {
            const name = input.getAttribute('data-name');
            if (!name) {
                console.warn('data-name отсутствует для input. Добавь его.');
                return;
            }

            const weightStr = input.getAttribute('data-weight');
            if (!weightStr) {
                console.warn('data-weight отсутствует для input. Добавь его.');
                return;
            }

            const weight = parseFloat(weightStr);
            const quantity = parseInt(input.value);

            if (isNaN(weight) || isNaN(quantity)) {
                console.warn(`Неверные данные: weight=${weightStr}, quantity=${input.value}`);
                return;
            }

            const totalWeight = weight * quantity;

            // Классификация по названию (настрой массив ключевых слов под названия напитков)
            const drinkKeywords = ['напиток', 'сок', 'вода', 'пиво', 'чай', 'кофе', 'кола', 'спрайт', 'лимонад'];
            const isDrink = drinkKeywords.some(keyword => name.toLowerCase().includes(keyword));

            if (isDrink) {
                drinkWeight += totalWeight;
            } else {
                foodWeight += totalWeight;
            }
        });

        const foodPerPerson = peopleCount > 0 ? (foodWeight / peopleCount) : 0;
        const drinkPerPerson = peopleCount > 0 ? (drinkWeight / peopleCount) : 0;

       
        const foodElement = document.getElementById('foodWeightPerPerson');
        const drinkElement = document.getElementById('drinkWeightPerPerson');
        const resultsBlock = document.getElementById('calculationResults');

        if (foodElement && drinkElement && resultsBlock) {
            foodElement.textContent = isNaN(foodPerPerson) ? '0' : Math.round(foodPerPerson);
            drinkElement.textContent = isNaN(drinkPerPerson) ? '0' : Math.round(drinkPerPerson);
            resultsBlock.style.display = 'block'; 
        } else {
            alert('Элементы для вывода результатов (foodWeightPerPerson, drinkWeightPerPerson, calculationResults) не найдены. Добавь их в View.');
            console.error('Отсутствуют id для вывода: foodWeightPerPerson, drinkWeightPerPerson, calculationResults');
        }

      
        console.log(`Общий вес еды: ${foodWeight} г, на человека: ${foodPerPerson}`);
        console.log(`Общий вес напитков: ${drinkWeight} г, на человека: ${drinkPerPerson}`);
    });
});
