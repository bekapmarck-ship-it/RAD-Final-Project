# The Hotel – Hotel Booking System

A Windows desktop application where customers search for available hotel rooms, book them, and manage their bookings, and where an administrator manages rooms, reservations, and hotel settings.

Built for the Rapid Application Development course (team project).

## Features

**Customer**
- Search rooms by check-in date, check-out date, and number of guests
- See available rooms with type, price, and amenities
- Book a room and see a confirmation with subtotal, tax, and total
- View and cancel your bookings (My Bookings)

**Administrator**
- Log in securely (password stored as a hash)
- Add, edit, delete, and take rooms out of service
- View, filter, and cancel reservations
- Set the hotel name and local tax rate (Settings). The name appears on customer screens and the tax is added to every new booking
- Statistics tab with charts: booked rooms (pie) and revenue over time (line) *(if completed)*

**System rules**
- A room cannot be double-booked, even if two people book at the same time
- Each reservation keeps the tax rate it was booked with

Not included by design: payment processing and email sending.

## Tech stack

- C# and Windows Forms (.NET)
- MySQL 8
- Visual Studio 2022

## Getting started

### 1. Prerequisites
- Visual Studio 2022 with the ".NET desktop development" workload
- MySQL Server 8 (and MySQL Workbench, optional)

### 2. Set up the database
1. Open MySQL Workbench (or the `mysql` command line) and run `SQL/schema_v2.sql` to create the `hotel_booking` database and tables.
2. Run the sample data script `SQL/starter_data.sql` (and later the large data script) to load about 100 rooms, 500 customers, and 1000 reservations.

### 3. Configure the connection
Copy `App.config.example` to `App.config` (same folder) and set your own MySQL details:

```
Server=localhost; Database=hotel_booking; User ID=[your user]; Password=[your password];
```

Do not commit your real password. See "Team workflow" below.

### 4. Run
1. Open `HotelBooking.sln` in Visual Studio.
2. Restore NuGet packages (this happens automatically on build).
3. Press **F5** to start.

### Admin login
- Username: `[admin]`
- Password: `[password for the demo account]`

## Project structure

```
/Forms       Windows Forms screens (customer and admin)
/Models      Room, Customer, Reservation, Settings classes
/Data        Database access (MySQL)
/Services    Booking and pricing logic
/Assets      Logo and icon images
/SQL         schema_v2.sql and sample data
/Docs        Proposal, milestones, UML, test checklist
```

## Course topics covered

| Topic | Where it appears |
|---|---|
| User interface | Customer and admin screens, shared branding |
| Event-driven programming | Button, grid, and form events |
| Data connections | MySQL database access |
| Asynchronous programming | Loading data without freezing the UI |
| Networking | Client application connected to the MySQL server |
| Concurrency | Transaction and row lock to prevent double-booking |
| Design | UML design and layered code structure |

## Team

| Member | Role |
|---|---|
| Ralph Machado | UI and Design |
| Angel Barron Castro | Database and Data |
| Kevin Ye | Booking Logic |
| Marc Kellyanne Bekap | Admin and Integration/Testing |

## Team workflow

- `main` is protected. Nobody pushes to it directly.
- Create a branch for each task: `feature/search-screen`, `fix/tax-rounding`.
- Open a pull request into `main` and get one teammate to review it before merging.
- Pull `main` into your branch before opening the pull request.
- Keep passwords out of the repository. Use a local config file that is listed in `.gitignore`.
