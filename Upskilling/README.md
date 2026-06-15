# CTS Upskilling - Exercise Solutions

This repository contains completed exercises for the CTS upskilling curriculum, organized by module.

## Folder Structure

```
UPSKILLING/
├── Module1-Web/              # HTML5, CSS3, JavaScript, jQuery, Bootstrap 5
│   ├── index.html             # Main "Local Community Event Portal" page
│   ├── help.html              # External help page (target="_blank")
│   ├── css/
│   │   └── styles.css         # All CSS3 exercises
│   ├── js/
│   │   ├── main.js            # JavaScript exercises 1-13 + HTML5 events/geo/video
│   │   └── jquery-demo.js      # jQuery exercises 1-6
│   └── sass-custom/
│       └── _custom-variables.scss   # Bootstrap Sass customization (Ex.19)
│
├── Module2-SQL/               # ANSI SQL (MySQL)
│   ├── schema.sql              # Database schema + sample data
│   └── queries.sql             # All 25 SQL exercises
│
└── Module3-CSharp-ADO/         # C# / ADO.NET
    └── CSharpExercises/
        ├── CSharpExercises.csproj
        ├── Program.cs           # Entry point / menu dispatcher
        └── Exercise01.cs ... Exercise30.cs
```

## How to Use

### Module 1 - Web (HTML5 / CSS3 / JS / jQuery / Bootstrap 5)
Open `Module1-Web/index.html` directly in a browser (Chrome recommended).
All exercises are integrated into a single themed site: **"Local Community Event Portal"**.

- Geolocation, localStorage, video events, and form validation are demonstrated live.
- Open Chrome DevTools (F12) to inspect console logs, debug, and test responsiveness.

### Module 2 - SQL (MySQL)
1. Run `Module2-SQL/schema.sql` first to create the database and load sample data.
2. Run `Module2-SQL/queries.sql` to execute all 25 exercise queries.

```bash
mysql -u root -p < schema.sql
mysql -u root -p community_event_portal < queries.sql
```

### Module 3 - C# / ADO.NET
Requires the .NET SDK (8.0+).

```bash
cd Module3-CSharp-ADO/CSharpExercises
dotnet run -- 5        # Run a specific exercise (e.g. Exercise 5)
dotnet run -- all      # Run exercises 2-29 sequentially
```

- Exercise 1 is an environment-setup exercise (prints "Hello, World!").
- Exercise 30 (ADO.NET CRUD) requires a local SQL Server instance and the `Employees`
  table described in the comments at the top of `Exercise30.cs`. Update the
  connection string in that file before running.

## Notes
- All code follows modern language features (C# 12 primary constructors, records,
  required members; ES6+ JS; Bootstrap 5.3).
- Sample data and themes are consistent across modules (the "Local Community Event
  Portal" theme ties together HTML5, CSS3, JS, jQuery, Bootstrap, and the SQL schema).
