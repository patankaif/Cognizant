console.log("Welcome to the Community Portal");

window.onload = () => {
    alert("Community Portal Loaded");
};

const eventName = "Music Festival";
const eventDate = "2026-08-10";
let seats = 50;

console.log(`${eventName} on ${eventDate}`);
seats--;

class Event {

    constructor(name, category, seats) {
        this.name = name;
        this.category = category;
        this.seats = seats;
    }
}

Event.prototype.checkAvailability = function () {
    return this.seats > 0;
};

let events = [

    new Event("Music Festival", "Music", 20),
    new Event("Sports Meet", "Sports", 15),
    new Event("Workshop", "Workshop", 0)

];

events.forEach(event => {

    if (event.checkAvailability()) {
        console.log(event.name);
    }
});

function addEvent(name, category, seats) {

    events.push(
        new Event(name, category, seats)
    );
}

function registerUser(eventObj) {

    try {

        if (eventObj.seats <= 0) {
            throw new Error("No seats available");
        }

        eventObj.seats--;

        renderEvents();

    } catch (error) {

        alert(error.message);
    }
}

function filterEventsByCategory(category, callback) {

    let filtered =
        events.filter(event =>
            category === "All" ||
            event.category === category
        );

    callback(filtered);
}

function registrationTracker() {

    let total = 0;

    return function () {

        total++;

        return total;
    };
}

const countRegistration =
    registrationTracker();

console.log(countRegistration());

let musicEvents =
    events.filter(event =>
        event.category === "Music"
    );

console.log(musicEvents);

let displayNames =
    events.map(event =>
        `Workshop on ${event.name}`
    );

console.log(displayNames);

const eventContainer =
    document.querySelector("#eventContainer");

function renderEvents(list = events) {

    eventContainer.innerHTML = "";

    list.forEach(event => {

        if (event.seats <= 0) {
            return;
        }

        const card =
            document.createElement("div");

        card.className =
            "event-card";

        card.innerHTML = `
            <h3>${event.name}</h3>
            <p>Category: ${event.category}</p>
            <p>Seats: ${event.seats}</p>
            <button onclick="registerUser(events[${events.indexOf(event)}])">
                Register
            </button>
        `;

        eventContainer.appendChild(card);
    });
}

document
.getElementById("categoryFilter")
.onchange = function () {

    filterEventsByCategory(
        this.value,
        renderEvents
    );
};

document
.getElementById("searchBox")
.addEventListener("keydown", () => {

    const text =
        document
        .getElementById("searchBox")
        .value
        .toLowerCase();

    const result =
        events.filter(event =>
            event.name
            .toLowerCase()
            .includes(text)
        );

    renderEvents(result);
});

function fetchEventsThen() {

    fetch("https://jsonplaceholder.typicode.com/posts")

    .then(response => response.json())

    .then(data => {
        console.log("Fetched with Then");
    })

    .catch(error => {
        console.log(error);
    });
}

async function fetchEventsAsync() {

    try {

        document
        .getElementById("loading")
        .style.display = "block";

        const response =
            await fetch(
                "https://jsonplaceholder.typicode.com/posts"
            );

        const data =
            await response.json();

        console.log(data);

    } catch (error) {

        console.log(error);

    } finally {

        document
        .getElementById("loading")
        .style.display = "none";
    }
}

fetchEventsThen();
fetchEventsAsync();

const copiedEvents =
    [...events];

const firstEvent =
    copiedEvents[0];

const {
    name,
    category,
    seats: availableSeats
} = firstEvent;

console.log(
    name,
    category,
    availableSeats
);

function greetUser(
    user = "Guest"
) {

    console.log(
        `Welcome ${user}`
    );
}

greetUser();

const form =
    document
    .getElementById("registerForm");

form.addEventListener(
    "submit",
    function (event) {

        event.preventDefault();

        console.log(
            "Form Submitted"
        );

        const name =
            form.elements["name"].value;

        const email =
            form.elements["email"].value;

        const selectedEvent =
            form.elements["event"].value;

        let valid = true;

        document
        .getElementById("nameError")
        .innerHTML = "";

        document
        .getElementById("emailError")
        .innerHTML = "";

        if (name === "") {

            document
            .getElementById("nameError")
            .innerHTML =
            "Name required";

            valid = false;
        }

        if (email === "") {

            document
            .getElementById("emailError")
            .innerHTML =
            "Email required";

            valid = false;
        }

        if (!valid) return;

        sendRegistration({
            name,
            email,
            selectedEvent
        });
    }
);

function sendRegistration(user) {

    console.log(
        "Sending Data",
        user
    );

    setTimeout(() => {

        fetch(
            "https://jsonplaceholder.typicode.com/posts",
            {
                method: "POST",

                headers: {
                    "Content-Type":
                    "application/json"
                },

                body:
                JSON.stringify(user)
            }
        )

        .then(response =>
            response.json()
        )

        .then(data => {

            document
            .getElementById("message")
            .innerHTML =
            "Registration Successful";

            console.log(data);
        })

        .catch(error => {

            document
            .getElementById("message")
            .innerHTML =
            "Registration Failed";

            console.log(error);
        });

    }, 2000);
}

$("#registerBtn").click(function () {

    $(".event-card")
    .fadeOut(500)
    .fadeIn(500);
});

console.log(
"React and Vue help build reusable UI components and manage large applications efficiently."
);

renderEvents();