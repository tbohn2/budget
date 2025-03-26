let yearVal = new Date().getFullYear();

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

async function addYear(yearValue) {
    const newYear = {
        YearValue: yearValue,
    };

    const response = await fetch("/AddYear", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newYear),
    });

    return await response.json();
}

async function getTotal(object) {
    const array = Object.entries(object);
    let total = 0

    array.forEach(pair => {
        if (pair[0] === "id" || pair[0] === "monthId") { return; }
        total += pair[1];
    })
    console.log(total);

    return total;
}

async function renderBudget(data) {
    console.log(data);

    const months = data.months;

    months.forEach(async (month) => {
        const id = month.name;
        const earnings = month.earnings;
        const expenses = month.expenses;

        const earningsTotal = await getTotal(earnings);
        const expensesTotal = await getTotal(expenses);

        const display = `
            <div class="values col-12">
                <div class="grid-square space-holder"></div>
                <input type="number" value="${earnings.primary}">
                <input type="number" value="${earnings.secondary}">
                <input type="number" value="${earnings.gifts}">
                <div class="grid-square space-holder">${earningsTotal}</div>
                <div class="grid-square space-holder"></div>
                <input type="number" value="${expenses.carInsurance}">
                <input type="number" value="${expenses.eatOut}">
                <input type="number" value="${expenses.fast}">
                <input type="number" value="${expenses.gas}">
                <input type="number" value="${expenses.groceries}">
                <input type="number" value="${expenses.holiday}">
                <input type="number" value="${expenses.medical}">
                <input type="number" value="${expenses.misc}">
                <input type="number" value="${expenses.rent}">
                <input type="number" value="${expenses.tithing}">
                <input type="number" value="${expenses.vacation}">
                <div class="grid-square space-holder">${expensesTotal}</div>
                <div class="grid-square space-holder">${earningsTotal - expensesTotal}</div>
            </div>
            `

        $(`#${id}`).append(display)
    });
}

$(document).ready(async function () {
    $('#year').text(yearVal);
    const year = await getYear(yearVal);
    renderBudget(year);

    async function changeYear() {
        $('#year').text(yearVal);
        let year = await getYear(yearVal);

        if (year == null) {
            // year = await addYear(yearVal);
        }
    }

    $('#prev').on('click', async function () {
        yearVal--;
        changeYear();
    });

    $('#next').on('click', async function () {
        yearVal++;
        changeYear();
    });
});
