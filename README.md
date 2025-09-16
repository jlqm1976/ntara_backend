# Team Score Management App

This project is composed of two main parts:
1. A **React frontend** built with Material-UI.
2. A **.NET 8 REST API** implemented using Clean Architecture and Entity Framework with SQLite.

The application allows users to upload CSV files containing team scores and provides functionality to browse, search, and filter data interactively.

## Setup Instructions

1. You need to install Node.Js (for this project Node.Js 20.19 was used)
2. You need Visual Studio 2022
3. Clone the repository from https://github.com/jlqm1976/ntara_backend.git
4. Open CollegeFootball.sln, the solution contains both Apps for REST API and UI
5. Right click on the solution, select Configure Startup Projects, make sure Multiple Projects is selected as well as CollegeFootball.Client and CollegeFootball.API are set to Start.
6. Build the Solution
7. Execute
8. 2 browser windows should will be opened, one for API App and another for UI.

---

## Features

### React Frontend
- **CSV Upload Page**
  - Upload team score data in CSV format.
  - Material-UI styled interface.
  - Option to navigate directly to the data view page.

- **Data View Page**
  - Displays team scores in a responsive Material-UI table.
  - **Search functionality**:
    - Enter a search value in a textbox.
    - Select one or more searchable columns using a multi-select dropdown with checkboxes.
    - Option to fetch **all records**.
  - **Table Enhancements**:
    - Row highlighting on hover.
    - Bold headers with custom background.
    - Pagination (25, 50, 100 rows per page).
  - Validation to ensure search value is not empty.

### .NET 8 REST API
- **Architecture**: Clean Architecture with layers:
  - API
  - Domain
  - Repository
  - Services
- **Database**: SQLite v3
- **ORM**: Entity Framework Core

#### Available Endpoints
- **Upload CSV**  
  `POST /TeamScore/UploadCsv`  
  Uploads a CSV file containing team score data.  

- **Get All Data**  
  `GET /TeamScore/GetAll`  
  Returns all records from the database.  

- **Search Data**  
  `GET /TeamScore/Search?searchValue={value}&columns={col1}&columns={col2}`  
  Returns filtered data based on a search value and selected columns.  

- **Get Searchable Columns**  
  `GET /SearchableColumn/GetAll`  
  Returns a list of columns available for searching.  

---

## Technologies Used

- **Frontend**
  - React
  - Material-UI (MUI)
  - Fetch (to perform requests to back-end endpoints)

- **Backend**
  - .NET 8
  - Entity Framework Core
  - SQLite v3

---
