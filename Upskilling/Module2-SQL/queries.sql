-- ============================================================
-- queries.sql
-- Local Community Event Portal - 25 SQL Exercises (ANSI SQL / MySQL)
-- Run schema.sql first to create the database & sample data.
-- ============================================================

USE community_event_portal;

-- ============================================================
-- 1. User Upcoming Events
-- Show a list of all upcoming events a user is registered for
-- in their city, sorted by date.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    e.event_id,
    e.title,
    e.city,
    e.start_date
FROM Users u
JOIN Registrations r ON r.user_id = u.user_id
JOIN Events e ON e.event_id = r.event_id
WHERE e.status = 'upcoming'
  AND e.city = u.city
ORDER BY u.user_id, e.start_date;


-- ============================================================
-- 2. Top Rated Events
-- Identify events with the highest average rating, considering
-- only those that have received at least 10 feedback submissions.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(f.feedback_id) AS feedback_count,
    AVG(f.rating)        AS avg_rating
FROM Events e
JOIN Feedback f ON f.event_id = e.event_id
GROUP BY e.event_id, e.title
HAVING COUNT(f.feedback_id) >= 10
ORDER BY avg_rating DESC;


-- ============================================================
-- 3. Inactive Users
-- Retrieve users who have not registered for any events
-- in the last 90 days.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    u.email
FROM Users u
WHERE u.user_id NOT IN (
    SELECT r.user_id
    FROM Registrations r
    WHERE r.registration_date >= DATE_SUB(CURDATE(), INTERVAL 90 DAY)
);


-- ============================================================
-- 4. Peak Session Hours
-- Count how many sessions are scheduled between 10 AM to 12 PM
-- for each event.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(s.session_id) AS sessions_between_10_and_12
FROM Events e
LEFT JOIN Sessions s
    ON s.event_id = e.event_id
    AND TIME(s.start_time) >= '10:00:00'
    AND TIME(s.start_time) <  '12:00:00'
GROUP BY e.event_id, e.title
ORDER BY sessions_between_10_and_12 DESC;


-- ============================================================
-- 5. Most Active Cities
-- List the top 5 cities with the highest number of distinct
-- user registrations.
-- ============================================================
SELECT
    u.city,
    COUNT(DISTINCT r.user_id) AS distinct_registered_users
FROM Registrations r
JOIN Users u ON u.user_id = r.user_id
GROUP BY u.city
ORDER BY distinct_registered_users DESC
LIMIT 5;


-- ============================================================
-- 6. Event Resource Summary
-- Generate a report showing the number of resources
-- (PDFs, images, links) uploaded for each event.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    SUM(CASE WHEN res.resource_type = 'pdf'   THEN 1 ELSE 0 END) AS pdf_count,
    SUM(CASE WHEN res.resource_type = 'image' THEN 1 ELSE 0 END) AS image_count,
    SUM(CASE WHEN res.resource_type = 'link'  THEN 1 ELSE 0 END) AS link_count,
    COUNT(res.resource_id) AS total_resources
FROM Events e
LEFT JOIN Resources res ON res.event_id = e.event_id
GROUP BY e.event_id, e.title
ORDER BY e.event_id;


-- ============================================================
-- 7. Low Feedback Alerts
-- List all users who gave feedback with a rating less than 3,
-- along with their comments and associated event names.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    e.title    AS event_name,
    f.rating,
    f.comments,
    f.feedback_date
FROM Feedback f
JOIN Users u  ON u.user_id  = f.user_id
JOIN Events e ON e.event_id = f.event_id
WHERE f.rating < 3
ORDER BY f.feedback_date DESC;


-- ============================================================
-- 8. Sessions per Upcoming Event
-- Display all upcoming events with the count of sessions
-- scheduled for them.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(s.session_id) AS session_count
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
WHERE e.status = 'upcoming'
GROUP BY e.event_id, e.title
ORDER BY session_count DESC;


-- ============================================================
-- 9. Organizer Event Summary
-- For each event organizer, show the number of events created
-- and their current status (upcoming, completed, cancelled).
-- ============================================================
SELECT
    u.user_id   AS organizer_id,
    u.full_name AS organizer_name,
    e.status,
    COUNT(e.event_id) AS event_count
FROM Users u
JOIN Events e ON e.organizer_id = u.user_id
GROUP BY u.user_id, u.full_name, e.status
ORDER BY u.user_id, e.status;


-- ============================================================
-- 10. Feedback Gap
-- Identify events that had registrations but received
-- no feedback at all.
-- ============================================================
SELECT DISTINCT
    e.event_id,
    e.title
FROM Events e
JOIN Registrations r ON r.event_id = e.event_id
LEFT JOIN Feedback f ON f.event_id = e.event_id
WHERE f.feedback_id IS NULL;


-- ============================================================
-- 11. Daily New User Count
-- Find the number of users who registered each day
-- in the last 7 days.
-- ============================================================
SELECT
    registration_date,
    COUNT(user_id) AS new_user_count
FROM Users
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
GROUP BY registration_date
ORDER BY registration_date;


-- ============================================================
-- 12. Event with Maximum Sessions
-- List the event(s) with the highest number of sessions.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(s.session_id) AS session_count
FROM Events e
JOIN Sessions s ON s.event_id = e.event_id
GROUP BY e.event_id, e.title
HAVING COUNT(s.session_id) = (
    SELECT MAX(session_counts.cnt)
    FROM (
        SELECT COUNT(session_id) AS cnt
        FROM Sessions
        GROUP BY event_id
    ) AS session_counts
);


-- ============================================================
-- 13. Average Rating per City
-- Calculate the average feedback rating of events conducted
-- in each city.
-- ============================================================
SELECT
    e.city,
    AVG(f.rating) AS avg_rating
FROM Events e
JOIN Feedback f ON f.event_id = e.event_id
GROUP BY e.city
ORDER BY avg_rating DESC;


-- ============================================================
-- 14. Most Registered Events
-- List top 3 events based on the total number
-- of user registrations.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(r.registration_id) AS registration_count
FROM Events e
LEFT JOIN Registrations r ON r.event_id = e.event_id
GROUP BY e.event_id, e.title
ORDER BY registration_count DESC
LIMIT 3;


-- ============================================================
-- 15. Event Session Time Conflict
-- Identify overlapping sessions within the same event
-- (i.e., session start and end times that conflict).
-- ============================================================
SELECT
    a.event_id,
    a.session_id AS session_a_id,
    a.title      AS session_a_title,
    b.session_id AS session_b_id,
    b.title      AS session_b_title,
    a.start_time AS a_start, a.end_time AS a_end,
    b.start_time AS b_start, b.end_time AS b_end
FROM Sessions a
JOIN Sessions b
    ON a.event_id = b.event_id
    AND a.session_id < b.session_id
WHERE a.start_time < b.end_time
  AND b.start_time < a.end_time;


-- ============================================================
-- 16. Unregistered Active Users
-- Find users who created an account in the last 30 days
-- but haven't registered for any events.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    u.email,
    u.registration_date
FROM Users u
LEFT JOIN Registrations r ON r.user_id = u.user_id
WHERE u.registration_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
  AND r.registration_id IS NULL;


-- ============================================================
-- 17. Multi-Session Speakers
-- Identify speakers who are handling more than one session
-- across all events.
-- ============================================================
SELECT
    speaker_name,
    COUNT(session_id) AS session_count
FROM Sessions
GROUP BY speaker_name
HAVING COUNT(session_id) > 1
ORDER BY session_count DESC;


-- ============================================================
-- 18. Resource Availability Check
-- List all events that do not have any resources uploaded.
-- ============================================================
SELECT
    e.event_id,
    e.title
FROM Events e
LEFT JOIN Resources res ON res.event_id = e.event_id
WHERE res.resource_id IS NULL;


-- ============================================================
-- 19. Completed Events with Feedback Summary
-- For completed events, show total registrations
-- and average feedback rating.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    COUNT(DISTINCT r.registration_id) AS total_registrations,
    AVG(f.rating)                     AS avg_rating
FROM Events e
LEFT JOIN Registrations r ON r.event_id = e.event_id
LEFT JOIN Feedback f      ON f.event_id = e.event_id
WHERE e.status = 'completed'
GROUP BY e.event_id, e.title;


-- ============================================================
-- 20. User Engagement Index
-- For each user, calculate how many events they attended
-- and how many feedbacks they submitted.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    COUNT(DISTINCT r.event_id) AS events_attended,
    COUNT(DISTINCT f.feedback_id) AS feedback_count
FROM Users u
LEFT JOIN Registrations r ON r.user_id = u.user_id
LEFT JOIN Feedback f      ON f.user_id = u.user_id
GROUP BY u.user_id, u.full_name
ORDER BY u.user_id;


-- ============================================================
-- 21. Top Feedback Providers
-- List top 5 users who have submitted the most feedback entries.
-- ============================================================
SELECT
    u.user_id,
    u.full_name,
    COUNT(f.feedback_id) AS feedback_count
FROM Users u
JOIN Feedback f ON f.user_id = u.user_id
GROUP BY u.user_id, u.full_name
ORDER BY feedback_count DESC
LIMIT 5;


-- ============================================================
-- 22. Duplicate Registrations Check
-- Detect if a user has been registered more than once
-- for the same event.
-- ============================================================
SELECT
    user_id,
    event_id,
    COUNT(registration_id) AS registration_count
FROM Registrations
GROUP BY user_id, event_id
HAVING COUNT(registration_id) > 1;


-- ============================================================
-- 23. Registration Trends
-- Show a month-wise registration count trend
-- over the past 12 months.
-- ============================================================
SELECT
    DATE_FORMAT(registration_date, '%Y-%m') AS reg_month,
    COUNT(registration_id) AS registration_count
FROM Registrations
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 12 MONTH)
GROUP BY DATE_FORMAT(registration_date, '%Y-%m')
ORDER BY reg_month;


-- ============================================================
-- 24. Average Session Duration per Event
-- Compute the average duration (in minutes) of sessions
-- in each event.
-- ============================================================
SELECT
    e.event_id,
    e.title,
    AVG(TIMESTAMPDIFF(MINUTE, s.start_time, s.end_time)) AS avg_session_duration_minutes
FROM Events e
JOIN Sessions s ON s.event_id = e.event_id
GROUP BY e.event_id, e.title;


-- ============================================================
-- 25. Events Without Sessions
-- List all events that currently have no sessions
-- scheduled under them.
-- ============================================================
SELECT
    e.event_id,
    e.title
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
WHERE s.session_id IS NULL;
