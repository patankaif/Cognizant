/* ============================================================
   jquery-demo.js - Local Community Event Portal
   Covers jQuery Exercises 1-6 (and JS Exercise 14)
   ============================================================ */

$(document).ready(function () {

  // ---------- Exercise 1: confirm jQuery loaded ----------
  console.log($("body").length ? "jQuery is working!" : "jQuery failed to load.");

  // ---------- Exercise 2: $() selectors ----------
  $("#jqBtn").click(function () {
    $("#jqHeading").text("Thanks for your feedback!");
    $(".jqPara").first().hide();
  });

  // ---------- Exercise 3: common methods + chaining ----------
  $("#hideBoxes").click(function () {
    $(".box").hide();
  });
  $("#showBoxes").click(function () {
    $(".box").show();
  });
  $("#fadeOutBoxes").click(function () {
    $(".box").fadeOut();
  });
  $("#fadeInBoxes").click(function () {
    $(".box").fadeIn();
  });
  $("#toggleBoxes").click(function () {
    $(".box").toggle();
  });
  // Bonus: chained methods
  $("#slideBoxes").click(function () {
    $(".box").slideUp().delay(800).slideDown();
  });

  // ---------- Exercise 4: DOM manipulation - add/remove list items ----------
  $("#addItemBtn").click(function () {
    const val = $("#itemInput").val().trim();
    if (val !== "") {
      $("<li>").addClass("list-group-item").text(val).appendTo("#itemList");
      $("#itemInput").val("");
    }
  });
  $("#removeAllBtn").click(function () {
    $("#itemList").empty();
  });

  // ---------- Exercise 5: color box interactions ----------
  $("#colorBtn").click(function () {
    $("#colorBox").css("background-color", "red").text("Box is now red. Double-click to reset.");
  });
  $("#colorBox").dblclick(function () {
    $(this).css("background-color", "white").text("Double-click to reset to white");
  });

  // ---------- Exercise 6: event helpers ----------
  $("#hoverDiv")
    .mouseenter(function () {
      $(this).css("background-color", "#d1e7dd").text("Mouse entered!");
    })
    .mouseleave(function () {
      $(this).css("background-color", "").text("Hover over me, click me, double-click me!");
    })
    .click(function () {
      $(this).append(" 🖱 Clicked!");
    })
    .dblclick(function () {
      alert("You double-clicked the box!");
    });

  $("#keypressInput").on("keypress", function (e) {
    $("#keypressOutput").text(`Key pressed: "${e.key}" (code ${e.which})`);
  });

  // ---------- JS Exercise 14: jQuery for register button + fade ----------
  // Demonstration: clicking any "Register" button briefly fades its card
  $(document).on("click", ".card .btn-primary", function () {
    $(this).closest(".card").fadeOut(150).fadeIn(150);
  });

});

/*
  Benefit of moving to a framework like React/Vue (JS Exercise 14):
  -------------------------------------------------------------
  React/Vue provide a component-based architecture with a virtual DOM
  (or reactive data bindings) that automatically keeps the UI in sync
  with application state. This eliminates a lot of manual DOM
  manipulation/selector code seen above, improves maintainability for
  larger apps, and encourages reusable, testable components.
*/
