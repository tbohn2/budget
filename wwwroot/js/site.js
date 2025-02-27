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

$('#addYear').click(async function () {
    const yearValue = $('#yearInput').val();
    const year = await addYear(yearValue);
    console.log(year);
}
);

$(document).ready(async function () {
    const yearVal = $('#year').text();
    const year = await getYear(yearVal);
    console.log(year);
}
);
