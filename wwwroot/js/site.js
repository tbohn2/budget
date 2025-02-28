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

async function renderBudget(data) {
    console.log(data);

    const months = data.months;

    months.forEach(month => {

    });
}

$(document).ready(async function () {
    $('#year').text(yearVal);
    // const year = await getYear(yearVal);
    // renderBudget(year);

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
