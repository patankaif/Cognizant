console.log("Welcome to the Community Portal");

window.addEventListener("load", () => {
  alert("Welcome to the Local Community Event Portal! The page has loaded.");
  initApp();
});

class CommunityEvent {
  constructor(id, title, category, date, seats, description, price = 0) {
    this.id = id;
    this.title = title;
    this.category = category;
    this.date = date;
    this.seats = seats;
    this.description = description;
    this.price = price;
  }
}

CommunityEvent.prototype.checkAvailability = function() {
  return this.seats > 0;
};

let eventsList = [];
const categoryTrackers = {};

function getCategoryTracker(category) {
  if (!categoryTrackers[category]) {
    let registrationCount = 0;
    categoryTrackers[category] = function() {
      registrationCount++;
      return registrationCount;
    };
  }
  return categoryTrackers[category];
}

async function fetchEventsMock() {
  const spinner = document.getElementById("loadingSpinner");
  if (spinner) spinner.style.display = "block";
  
  try {
    const response = await fetch("events.json");
    if (!response.ok) {
      throw new Error("HTTP error " + response.status);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Failed fetching events: ", error);
    return [];
  } finally {
    if (spinner) spinner.style.display = "none";
  }
}

function initApp() {
  fetchEventsMock().then(data => {
    eventsList = data.map(item => new CommunityEvent(
      item.id,
      item.title,
      item.category,
      item.date,
      item.seats,
      item.description,
      item.price
    ));
    
    if (eventsList.length > 0) {
      const firstEvent = eventsList[0];
      console.log("Inspecting Event Object Properties:");
      for (const [key, value] of Object.entries(firstEvent)) {
        console.log(`${key}: ${value}`);
      }
    }
    
    renderEvents(eventsList);
    populateEventDropdown(eventsList);
    loadUserPreferences();
  });
  
  const searchInput = document.getElementById("searchEvent");
  if (searchInput) {
    searchInput.addEventListener("keydown", handleSearchKeyDown);
  }
  
  const feedbackTextarea = document.getElementById("feedbackText");
  if (feedbackTextarea) {
    feedbackTextarea.addEventListener("keyup", countFeedbackCharacters);
  }
}

function renderEvents(events, categoryFilter = "All") {
  const container = document.getElementById("eventsContainer");
  if (!container) return;
  
  container.innerHTML = "";
  
  let filtered = [...events];
  
  if (categoryFilter !== "All") {
    filtered = filtered.filter(evt => evt.category === categoryFilter);
  }
  
  const formattedCards = filtered.map(evt => {
    const isAvailable = evt.checkAvailability();
    const availabilityBadge = isAvailable 
      ? `<span class="highlight">Seats: ${evt.seats}</span>`
      : `<span style="color: #ef4444; font-weight: 600;">SOLD OUT</span>`;
      
    const registerButton = isAvailable
      ? `<button class="btn-primary" style="padding: 5px 10px; width: auto;" onclick="selectEventForRegistration(${evt.id})">Register</button>`
      : `<button class="btn-primary" style="background-color: #475569; cursor: not-allowed; padding: 5px 10px; width: auto;" disabled>Full</button>`;
      
    return {
      html: `
        <div class="eventCard" data-category="${evt.category}" id="event-card-${evt.id}">
          <h3>${evt.title}</h3>
          <p><strong>Date:</strong> ${evt.date}</p>
          <p><strong>Category:</strong> ${evt.category}</p>
          <p>${evt.description}</p>
          <p><strong>Fee:</strong> $${evt.price}</p>
          <div style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
            ${availabilityBadge}
            ${registerButton}
          </div>
        </div>
      `,
      category: evt.category
    };
  });
  
  formattedCards.forEach(cardData => {
    const tempDiv = document.createElement("div");
    tempDiv.innerHTML = cardData.html.trim();
    const element = tempDiv.firstChild;
    container.appendChild(element);
  });
}

function populateEventDropdown(events) {
  const select = document.getElementById("eventSelect");
  if (!select) return;
  
  select.innerHTML = '<option value="" disabled selected>Choose an event</option>';
  events.forEach(evt => {
    const option = document.createElement("option");
    option.value = evt.id;
    option.textContent = `${evt.title} ($${evt.price})`;
    select.appendChild(option);
  });
}

function filterByCategory(selectElement) {
  const category = selectElement.value;
  renderEvents(eventsList, category);
  
  localStorage.setItem("preferredCategory", category);
}

function loadUserPreferences() {
  const savedCategory = localStorage.getItem("preferredCategory");
  const filterSelect = document.getElementById("filterCategory");
  if (savedCategory && filterSelect) {
    filterSelect.value = savedCategory;
    renderEvents(eventsList, savedCategory);
  }
  
  const savedEventId = localStorage.getItem("preferredEventId");
  const registerSelect = document.getElementById("eventSelect");
  if (savedEventId && registerSelect) {
    registerSelect.value = savedEventId;
    updateEventFee();
  }
}

function clearUserPreferences() {
  localStorage.removeItem("preferredCategory");
  localStorage.removeItem("preferredEventId");
  sessionStorage.clear();
  
  const filterSelect = document.getElementById("filterCategory");
  if (filterSelect) filterSelect.value = "All";
  
  const registerSelect = document.getElementById("eventSelect");
  if (registerSelect) registerSelect.value = "";
  
  const feeDisplay = document.getElementById("feeDisplay");
  if (feeDisplay) feeDisplay.textContent = "";
  
  renderEvents(eventsList, "All");
  alert("All preferences cleared successfully.");
}

function selectEventForRegistration(eventId) {
  const select = document.getElementById("eventSelect");
  if (select) {
    select.value = eventId;
    updateEventFee();
    localStorage.setItem("preferredEventId", eventId);
    select.scrollIntoView({ behavior: "smooth" });
  }
}

function updateEventFee() {
  const select = document.getElementById("eventSelect");
  const feeDisplay = document.getElementById("feeDisplay");
  if (!select || !feeDisplay) return;
  
  const eventId = parseInt(select.value, 10);
  const selectedEvent = eventsList.find(evt => evt.id === eventId);
  
  if (selectedEvent) {
    feeDisplay.textContent = `Registration Fee: $${selectedEvent.price}`;
    localStorage.setItem("preferredEventId", eventId);
  } else {
    feeDisplay.textContent = "";
  }
}

function handleSearchKeyDown(event) {
  const query = event.target.value.toLowerCase();
  
  const filtered = eventsList.filter(evt => {
    return evt.title.toLowerCase().includes(query) || 
           evt.description.toLowerCase().includes(query);
  });
  
  renderEvents(filtered);
}

function validatePhoneNumber(input) {
  const phone = input.value.trim();
  const errorDiv = document.getElementById("phoneError");
  const phoneRegex = /^\+?[0-9\s\-]{7,15}$/;
  
  if (phone !== "" && !phoneRegex.test(phone)) {
    input.classList.add("invalid-input");
    if (errorDiv) {
      errorDiv.textContent = "Please enter a valid phone number.";
      errorDiv.style.display = "block";
    }
  } else {
    input.classList.remove("invalid-input");
    if (errorDiv) {
      errorDiv.style.display = "none";
    }
  }
}

function countFeedbackCharacters(event) {
  const text = event.target.value;
  const countSpan = document.getElementById("charCount");
  if (countSpan) {
    countSpan.textContent = text.length;
  }
}

function handleRegistrationSubmit(event) {
  event.preventDefault();
  
  const form = event.target;
  const nameInput = form.elements["regName"];
  const emailInput = form.elements["regEmail"];
  const dateInput = form.elements["regDate"];
  const eventSelect = form.elements["regEvent"];
  const phoneInput = form.elements["regPhone"];
  const confirmationOutput = document.getElementById("registrationOutput");
  
  if (!nameInput.value || !emailInput.value || !eventSelect.value) {
    alert("Please fill in all required fields.");
    return;
  }
  
  const eventId = parseInt(eventSelect.value, 10);
  const selectedEvent = eventsList.find(evt => evt.id === eventId);
  
  try {
    if (!selectedEvent) {
      throw new Error("Invalid event selected.");
    }
    
    if (!selectedEvent.checkAvailability()) {
      throw new Error(`Sorry, "${selectedEvent.title}" is currently sold out!`);
    }
    
    selectedEvent.seats--;
    renderEvents(eventsList);
    
    const tracker = getCategoryTracker(selectedEvent.category);
    const categoryRegs = tracker();
    
    const payload = {
      name: nameInput.value,
      email: emailInput.value,
      phone: phoneInput.value,
      eventId: eventId,
      date: dateInput.value
    };
    
    if (confirmationOutput) {
      confirmationOutput.textContent = "Submitting registration details...";
      confirmationOutput.style.color = "#fbbf24";
    }
    
    fetch("register_mock.json", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    })
    .then(response => response.json())
    .then(data => {
      setTimeout(() => {
        if (data.success) {
          if (confirmationOutput) {
            confirmationOutput.textContent = `Success! Registered for "${selectedEvent.title}". (${categoryRegs} registrations recorded in ${selectedEvent.category} category)`;
            confirmationOutput.style.color = "#4ade80";
          }
          form.reset();
          const feeDisplay = document.getElementById("feeDisplay");
          if (feeDisplay) feeDisplay.textContent = "";
        } else {
          throw new Error("Registration rejected by server.");
        }
      }, 1200);
    })
    .catch(err => {
      alert("Registration failed: " + err.message);
      selectedEvent.seats++;
      renderEvents(eventsList);
    });
    
  } catch (error) {
    alert("Error: " + error.message);
    if (confirmationOutput) {
      confirmationOutput.textContent = "Error: " + error.message;
      confirmationOutput.style.color = "#ef4444";
    }
  }
}

window.addEventListener("beforeunload", (event) => {
  const nameVal = document.getElementById("regName")?.value;
  const emailVal = document.getElementById("regEmail")?.value;
  
  if (nameVal || emailVal) {
    event.preventDefault();
    event.returnValue = "You have unsaved changes. Are you sure you want to leave?";
  }
});

function handleVideoCanPlay() {
  const statusDiv = document.getElementById("videoStatus");
  if (statusDiv) {
    statusDiv.textContent = "Video ready to play";
  }
}

function enlargeImage(imgElement) {
  const overlay = document.createElement("div");
  overlay.className = "img-enlarged";
  
  const cloneImg = document.createElement("img");
  cloneImg.src = imgElement.src;
  
  overlay.appendChild(cloneImg);
  document.body.appendChild(overlay);
  
  overlay.addEventListener("click", () => {
    document.body.removeChild(overlay);
  });
}

function getNearbyEvents() {
  const resultDiv = document.getElementById("geolocationResult");
  if (!resultDiv) return;
  
  if (!navigator.geolocation) {
    resultDiv.textContent = "Geolocation is not supported by your browser.";
    return;
  }
  
  resultDiv.textContent = "Retrieving location coordinates...";
  
  const options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
  };
  
  navigator.geolocation.getCurrentPosition(
    (position) => {
      const lat = position.coords.latitude;
      const lon = position.coords.longitude;
      resultDiv.textContent = `Coordinates: Latitude ${lat.toFixed(4)}, Longitude ${lon.toFixed(4)}`;
    },
    (error) => {
      switch (error.code) {
        case error.PERMISSION_DENIED:
          resultDiv.textContent = "Access denied: Please grant location permissions to locate events.";
          break;
        case error.POSITION_UNAVAILABLE:
          resultDiv.textContent = "Location information is unavailable.";
          break;
        case error.TIMEOUT:
          resultDiv.textContent = "Location request timed out. Please try again.";
          break;
        default:
          resultDiv.textContent = "An unknown error occurred while retrieving location.";
      }
    },
    options
  );
}
