/* ============================================================
   main.js - Local Community Event Portal
   Covers JavaScript Exercises 1-13 and HTML5 Ex.5-9
   ============================================================ */

/* ---------- Exercise 1: Basics & Setup ---------- */
console.log("Welcome to the Community Portal");
window.addEventListener("load", () => {
  console.log("Page fully loaded.");
});

/* ---------- Exercise 2: Data types, template literals, ++/-- ---------- */
const EVENT_NAME = "Tech Innovators Meetup";
const EVENT_DATE = "2025-06-10";
let availableSeats = 5;

console.log(`${EVENT_NAME} is happening on ${EVENT_DATE}. Seats left: ${availableSeats}`);

function registerSeat() {
  if (availableSeats > 0) {
    availableSeats--; // decrement on registration
  }
  return availableSeats;
}

/* ---------- Exercise 5: Objects & "Prototypes" (ES6 class) ---------- */
class Event {
  constructor(id, name, category, city, date, seats, fee) {
    this.id = id;
    this.name = name;
    this.category = category;
    this.city = city;
    this.date = date;
    this.seats = seats;
    this.fee = fee;
  }

  // method on prototype
  checkAvailability() {
    return this.seats > 0;
  }
}

/* ---------- Exercise 6: Array of events + array methods ---------- */
let events = [
  new Event(1, "Tech Innovators Meetup", "Tech", "New York", "2025-06-10", 5, 10),
  new Event(2, "AI & ML Conference", "Tech", "Chicago", "2025-05-15", 0, 25),
  new Event(3, "Frontend Development Bootcamp", "Workshop", "Los Angeles", "2025-07-01", 8, 0),
  new Event(4, "Sunday Jazz Night", "Music", "New York", "2025-06-20", 3, 15),
  new Event(5, "Baking Workshop", "Workshop", "Chicago", "2025-06-25", 0, 5),
];

// .push() - add a new event (Exercise 6)
events.push(new Event(6, "Community Choir Meetup", "Music", "Los Angeles", "2025-07-05", 10, 0));

// .filter() - show only music events (used by category filter below)
function getEventsByCategory(category) {
  if (category === "all") return events;
  return events.filter((e) => e.category === category);
}

// .map() - format display cards text
function formatEventLabel(e) {
  return `${e.category} on ${e.name}`;
}
console.log(events.map(formatEventLabel));

// Object.entries() demo (Exercise 5)
console.log("Event #1 properties:", Object.entries(events[0]));

/* ---------- Exercise 4: Functions, closures, higher-order functions ---------- */
function addEvent(newEvent) {
  events.push(newEvent);
  return events;
}

function registerUser(eventId) {
  const ev = events.find((e) => e.id === eventId);
  if (!ev) throw new Error("Event not found");
  if (!ev.checkAvailability()) throw new Error("No seats available");
  ev.seats--;
  return ev;
}

// Closure: track total registrations per category
function createCategoryCounter() {
  const counts = {};
  return function (category) {
    counts[category] = (counts[category] || 0) + 1;
    return counts[category];
  };
}
const trackRegistration = createCategoryCounter();

// Higher-order function: pass callback to filter
function filterEventsByCategory(list, predicate) {
  return list.filter(predicate);
}

/* ---------- Exercise 7: DOM Manipulation - render events ---------- */
function renderEvents(category = "all") {
  const container = document.getElementById("eventContainer");
  container.innerHTML = ""; // clear

  const filtered = getEventsByCategory(category);

  // Exercise 3: only show upcoming events with seats using if-else + forEach
  filtered.forEach((ev) => {
    const isUpcoming = new Date(ev.date) >= new Date("2025-01-01"); // demo logic
    const hasSeats = ev.checkAvailability();

    const col = document.createElement("div");
    col.className = "col-md-4";

    const card = document.createElement("div");
    card.className = "card h-100 shadow-sm";

    const body = document.createElement("div");
    body.className = "card-body";

    const title = document.createElement("h5");
    title.className = "card-title";
    title.textContent = ev.name;

    const meta = document.createElement("p");
    meta.className = "card-text text-muted";
    meta.textContent = `${ev.category} • ${ev.city} • ${ev.date}`;

    const seatInfo = document.createElement("p");
    seatInfo.className = "card-text";

    const btn = document.createElement("button");
    btn.className = "btn btn-sm btn-primary";
    btn.textContent = "Register";

    if (!isUpcoming) {
      seatInfo.innerHTML = '<span class="badge bg-secondary">Past Event</span>';
      btn.disabled = true;
    } else if (!hasSeats) {
      seatInfo.innerHTML = '<span class="badge bg-danger">Sold Out</span>';
      btn.disabled = true;
    } else {
      seatInfo.innerHTML = `<span class="badge bg-success">${ev.seats} seats left</span>`;
    }

    // Exercise 8: onclick for Register buttons + try-catch error handling (Ex.3)
    btn.onclick = function () {
      try {
        registerUser(ev.id);
        trackRegistration(ev.category);
        renderEvents(document.getElementById("categoryFilter").value);
        alert(`Thanks for registering for "${ev.name}"!`);
      } catch (err) {
        alert(`Registration failed: ${err.message}`);
      }
    };

    body.appendChild(title);
    body.appendChild(meta);
    body.appendChild(seatInfo);
    body.appendChild(btn);
    card.appendChild(body);
    col.appendChild(card);
    container.appendChild(col);
  });

  if (filtered.length === 0) {
    container.innerHTML = '<p class="text-muted">No events found for this category.</p>';
  }
}

// Exercise 8: onchange to filter events by category
document.getElementById("categoryFilter").addEventListener("change", (e) => {
  renderEvents(e.target.value);
});

// Exercise 8: keydown for quick search by name
document.getElementById("searchEvent").addEventListener("keydown", function () {
  // Use a tiny delay so the input value includes the latest keystroke
  setTimeout(() => {
    const term = this.value.toLowerCase();
    const container = document.getElementById("eventContainer");
    container.innerHTML = "";
    const matches = events.filter((e) => e.name.toLowerCase().includes(term));
    if (matches.length === 0) {
      container.innerHTML = '<p class="text-muted">No matching events.</p>';
      return;
    }
    matches.forEach((ev) => {
      const col = document.createElement("div");
      col.className = "col-md-4";
      col.innerHTML = `
        <div class="card h-100 shadow-sm">
          <div class="card-body">
            <h5 class="card-title">${ev.name}</h5>
            <p class="card-text text-muted">${ev.category} • ${ev.city} • ${ev.date}</p>
          </div>
        </div>`;
      container.appendChild(col);
    });
  }, 0);
});

// Initial render
renderEvents();

/* ---------- Exercise 10: Modern JS features (destructuring, spread, defaults) ---------- */
function describeEvent({ name, city = "Unknown City" } = {}) {
  return `${name} will be held in ${city}.`;
}
const { name: firstEventName, city: firstEventCity } = events[0];
console.log(describeEvent({ name: firstEventName, city: firstEventCity }));

// spread operator to clone before filtering
const eventsCopy = [...events];
const techEventsCopy = eventsCopy.filter((e) => e.category === "Tech");
console.log("Tech events (copy):", techEventsCopy.map((e) => e.name));

/* ---------- Exercise 11: Registration Form handling ---------- */
const regForm = document.getElementById("registrationForm");

regForm.addEventListener("submit", function (e) {
  e.preventDefault(); // Exercise 11: prevent default form submission

  const fullName = document.getElementById("fullName").value.trim();
  const email = document.getElementById("email").value.trim();
  const eventType = document.getElementById("eventType");
  const output = document.getElementById("confirmationOutput");

  let errors = [];
  if (!fullName) errors.push("Name is required.");
  if (!email || !email.includes("@")) errors.push("A valid email is required.");
  if (!eventType.value) errors.push("Please choose an event type.");

  if (errors.length > 0) {
    output.textContent = errors.join(" ");
    output.classList.remove("text-success");
    output.classList.add("text-danger");
    return;
  }

  // Exercise 12: AJAX & Fetch - simulate POST to mock API
  output.textContent = "Submitting...";
  output.classList.remove("text-danger");
  output.classList.add("text-success");

  submitRegistration({ fullName, email, eventType: eventType.value })
    .then((msg) => {
      output.textContent = msg;
      // HTML5 Ex.5: output element shows confirmation
    })
    .catch((err) => {
      output.textContent = "Submission failed: " + err.message;
      output.classList.add("text-danger");
    });
});

/* ---------- Exercise 12: fetch() simulation with setTimeout ---------- */
function submitRegistration(data) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (data.email.includes("@")) {
        resolve(`Registration confirmed for ${data.fullName}!`);
      } else {
        reject(new Error("Invalid email"));
      }
    }, 1000); // simulate delay
  });
}

/* ---------- Exercise 9: Async JS - fetch mock event data ---------- */
async function loadRemoteEvents() {
  try {
    // Using a public mock endpoint
    const response = await fetch("https://jsonplaceholder.typicode.com/posts?_limit=3");
    const data = await response.json();
    console.log("Fetched mock remote events:", data);
    return data;
  } catch (err) {
    console.error("Failed to load remote events:", err);
  }
}
// Uncomment to test in a browser with network access:
// loadRemoteEvents();

/* ---------- HTML5 Ex.6: blur, change, click, dblclick, keydown ---------- */

// onblur to validate phone number
const phoneInput = document.getElementById("phone");
phoneInput.addEventListener("blur", function () {
  const feedback = document.getElementById("phoneFeedback");
  const digitsOnly = /^\d{10}$/;
  if (this.value && !digitsOnly.test(this.value)) {
    feedback.textContent = "Phone number must be exactly 10 digits.";
    feedback.classList.add("text-danger");
  } else {
    feedback.textContent = this.value ? "Looks good!" : "";
    feedback.classList.remove("text-danger");
    feedback.classList.add("text-success");
  }
});

// onchange dropdown to display selected event fee
document.getElementById("eventType").addEventListener("change", function () {
  const fee = this.value;
  const feeText = fee === "0" ? "This event is free!" : `Registration fee: $${fee}`;
  console.log(feeText);
});

// keydown in feedback textarea - count characters
const messageBox = document.getElementById("message");
const charCount = document.getElementById("charCount");
messageBox.addEventListener("keyup", function () {
  charCount.textContent = `${this.value.length} characters`;
});

/* ---------- HTML5 Ex.7: video oncanplay ---------- */
const promoVideo = document.getElementById("promoVideo");
const videoStatus = document.getElementById("videoStatus");
promoVideo.addEventListener("canplay", function () {
  videoStatus.textContent = "Video ready to play.";
});

/* ---------- HTML5 Ex.9: Geolocation ---------- */
document.getElementById("findNearbyBtn").addEventListener("click", function () {
  const result = document.getElementById("geoResult");

  if (!navigator.geolocation) {
    result.textContent = "Geolocation is not supported by your browser.";
    return;
  }

  result.textContent = "Locating you...";

  navigator.geolocation.getCurrentPosition(
    (position) => {
      const { latitude, longitude } = position.coords;
      result.textContent = `You're near (${latitude.toFixed(4)}, ${longitude.toFixed(4)}). Showing nearby events...`;
    },
    (error) => {
      switch (error.code) {
        case error.PERMISSION_DENIED:
          result.textContent = "Location permission denied.";
          break;
        case error.TIMEOUT:
          result.textContent = "Location request timed out.";
          break;
        default:
          result.textContent = "Unable to retrieve your location.";
      }
    },
    {
      enableHighAccuracy: true,
      timeout: 10000,
      maximumAge: 0,
    }
  );
});

/* ---------- HTML5 Ex.8: localStorage / sessionStorage preferences ---------- */
const prefSelect = document.getElementById("prefEventType");
const prefStatus = document.getElementById("prefStatus");

// On load, retrieve and pre-select preference
window.addEventListener("DOMContentLoaded", () => {
  const savedPref = localStorage.getItem("preferredEventType");
  if (savedPref) {
    prefSelect.value = savedPref;
    prefStatus.textContent = `Loaded saved preference: ${savedPref}`;
  }
});

document.getElementById("savePrefBtn").addEventListener("click", () => {
  localStorage.setItem("preferredEventType", prefSelect.value);
  sessionStorage.setItem("sessionPreferredEventType", prefSelect.value);
  prefStatus.textContent = `Preference saved: ${prefSelect.value}`;
});

document.getElementById("clearPrefBtn").addEventListener("click", () => {
  localStorage.removeItem("preferredEventType");
  sessionStorage.removeItem("sessionPreferredEventType");
  prefStatus.textContent = "Preferences cleared.";
});
