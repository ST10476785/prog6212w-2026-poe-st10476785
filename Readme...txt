# RaceDay Database System

## Description
This repository contains the database schema and initialization scripts for the RaceDay event management system. The system tracks events, race categories, user enrollments (organizers and participants), and race results. 


## Tools & Technologies Used
* **Microsoft SQL Server 2022 Express:** Primary database engine.
* **SQL Server Management Studio (SSMS):** Database management and script execution.
* **Visual Studio:** Secondary SQL script editor and local execution environment.
* **Microsoft Word:** 

## Repository Structure
* `/docs` - Contains project documentation, the Entity-Relationship Diagram (ERD), and the primary SQL script.
* `/video` - Contains the video demonstration of the database creation and execution.

## Prerequisites
To run this project, you will need:
* Microsoft SQL Server 2022 Express (or higher)
* SQL Server Management Studio (SSMS) or Visual Studio

## Installation & Execution
1. Clone or download this repository to your local machine.
2. Open **Microsoft SQL Server Management Studio (SSMS)** and connect to your local database engine (e.g., `.\SQLEXPRESS`).
3. Open the `RaceDay_Database.sql` script located in the `/docs` folder.
4. Execute the script 
5. The script includes a `DROP DATABASE IF EXISTS` command, meaning it can be run multiple times safely. It will automatically build the schema and seed the tables with sample data.

## Tables Included
1. **UserType:** Defines roles (Organiser, Participant).
2. **USER:** Stores user credentials and personal details.
3. **EVENT:** Stores race event details (location, date, total distance).
4. **Category:** Defines specific races within an event (e.g., 5km Park Run, 400m Sprint).
5. **EventEnrollment:** Links users to specific categories.
6. **Result:** Records finishing times and positions for enrolled participants.
