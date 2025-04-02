let yearVal = new Date().getFullYear();
let year = {};
let earningsToUpdate = {};
let expensesToUpdate = {};

async function getYear(year) {
    const response = await fetch(`Year/${year}`,
        {
            method: "GET",
            headers: { "Content-Type": "application/json" },
        })
    if (!response.ok) {
        console.error("Year not found");
        return null;
    }
    return await response.json();
}

async function updateEarnings() {
    const array = Object.values(earningsToUpdate);

    const response = await fetch("/UpdateEarnings", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(array),
    });

    console.log(await response.json());
}

async function updateExpenses() {
    const array = Object.values(expensesToUpdate);

    const response = await fetch("/UpdateExpenses", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(array),
    });

    console.log(await response.json());
}

async function getTotal(object) {
    const array = Object.entries(object);
    let total = 0

    array.forEach(pair => {
        if (pair[0] === "id" || pair[0] === "monthId") { return; }
        total += pair[1];
    })

    return total;
}

async function handleChange(className, newValue, name, id) {
    objectToUpdate = className === "earnings" ? earningsToUpdate : expensesToUpdate;

    const totalName = className === "earnings" ? "earningsTotal" : "expensesTotal"
    const currentTotal = $(`div[name=${totalName}][data-id="${id}"]`).html();

    if (objectToUpdate[id] === undefined) {
        const newObject = {};

        $(`.${className}[data-id="${id}"]`).each(function () {
            const element = $(this);
            newObject[element.attr('name')] = parseInt(element.val());
        })

        newObject.id = id;
        objectToUpdate[id] = newObject;
    } else {
        objectToUpdate[id][name] = newValue;
    }

    const newTotal = await getTotal(objectToUpdate[id])
    const change = newTotal - currentTotal
    $(`div[name=${totalName}][data-id="${id}"]`).html(newTotal);

    const currentNet = parseInt($(`div[name="netTotal"][data-id="${id}"]`).html());
    const newNet = className === "earnings" ? currentNet + change : currentNet - change;

    $(`div[name="netTotal"][data-id="${id}"]`).html(newNet);
}

async function renderBudget(data) {
    const months = data.months;

    for (const month of months) {
        const monthName = month.name;
        const earnings = month.earnings;
        const expenses = month.expenses;

        const earningsTotal = await getTotal(earnings);
        const expensesTotal = await getTotal(expenses);

        const display = `
            <div class="values col-12">
                <div class="grid-square space-holder"></div>
                <input name="primary" data-id="${earnings.id}" class="earnings" type="number" value="${earnings.primary}">
                <input name="secondary" data-id="${earnings.id}" class="earnings" type="number" value="${earnings.secondary}">
                <input name="gifts" data-id="${earnings.id}" class="earnings" type="number" value="${earnings.gifts}">
                <div name="earningsTotal" data-id="${earnings.id}" class="grid-square space-holder">${earningsTotal}</div>
                <div class="grid-square space-holder"></div>
                <input name="carInsurance" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.carInsurance}">
                <input name="eatOut" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.eatOut}">
                <input name="fast" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.fast}">
                <input name="gas" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.gas}">
                <input name="groceries" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.groceries}">
                <input name="holiday" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.holiday}">
                <input name="medical" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.medical}">
                <input name="misc" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.misc}">
                <input name="rent" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.rent}">
                <input name="tithing" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.tithing}">
                <input name="vacation" data-id="${expenses.id}" class="expenses" type="number" value="${expenses.vacation}">
                <div name="expensesTotal" data-id="${expenses.id}" class="grid-square space-holder">${expensesTotal}</div>
                <div name="netTotal" data-id="${expenses.id}" class="grid-square space-holder">${earningsTotal - expensesTotal}</div>
            </div>
            `

        $(`#${monthName}`).append(display)
    };

    $('body').on('change', '.earnings, .expenses', function () {
        const input = $(this)
        const className = input.attr('class')
        const newValue = parseInt(input.val());
        const name = input.attr('name');
        const id = parseInt(input.attr('data-id'));

        handleChange(className, newValue, name, id);
    });
}

$(document).ready(async function () {
    $('#year').text(yearVal);
    year = await getYear(yearVal);
    renderBudget(year);

    async function changeYear() {
        $('#year').text(yearVal);
        year = await getYear(yearVal);
        $('.values').remove();
        renderBudget(year);
    }

    $('#prev').on('click', async function () {
        yearVal--;
        changeYear();
    });

    $('#next').on('click', async function () {
        yearVal++;
        changeYear();
    });

    $('#save').on('click', async function () {
        if (Object.keys(earningsToUpdate).length > 0) {
            updateEarnings();
        }
        if (Object.keys(expensesToUpdate).length > 0) {
            updateExpenses();
        }
    })

    $('#cancel').on('click', function () {
        if (Object.keys(earningsToUpdate).length > 0 || Object.keys(expensesToUpdate).length > 0) {
            earningsToUpdate = {};
            expensesToUpdate = {};
            $('.values').remove();
            renderBudget(year);
        }
    })

    $('#delete').on('click', function handleDelete() {
        $('#delete').off()
        $('#delete').html('Delete all data for this year? 3')
        $('#delete').after('<button id="cancelDelete" class="btn btn-dark">Cancel</button>')

        let timer = 3;
        const timerInterval = setInterval(() => {
            timer--;
            if (timer < 1) {
                $('#delete').html(`Delete all data for this year?`);
                clearInterval(timerInterval);
                return;
            }
            $('#delete').html(`Delete all data for this year? ${timer}`);
        }, 1000);

        function resetDelete() {
            clearInterval(timerInterval);
            clearTimeout(attachEventListenerTimeout);
            $('#cancelDelete').remove();
            $('#delete').html('Clear Data');
            $('#delete').off().on('click', handleDelete);
        }

        $('#cancelDelete').off().on('click', resetDelete);

        const attachEventListenerTimeout = setTimeout(() => {
            $('#delete').off().on('click', async function deleteYear() {
                const response = await fetch(`DeleteYear/${year.id}`,
                    {
                        method: "DELETE",
                        headers: { "Content-Type": "application/json" },
                    })
                if (!response.ok) {
                    console.error("Year not found");
                    return null;
                }
                console.log('Deleted year:', await response.json());

                resetDelete();

                yearVal = new Date().getFullYear();
                $('#year').text(yearVal);
                year = await getYear(yearVal);
                $('.values').remove();
                renderBudget(year);
            })
        }, 3000);
    })
});
