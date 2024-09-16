# CareHub - HospitalManagementSystem  

[![CareHub](https://img.shields.io/badge/Live_Demo-View_on_Azure-blue)](https://carehub-dra2crd2bgdjhng3.southafricanorth-01.azurewebsites.net)

CareHub is a healthcare management system designed to streamline hospital operations by managing patient records, doctor schedules, and appointments efficiently. Built using ASP.NET Core MVC, this application aims to improve operational efficiency and enhance user experience in a healthcare setting.

## Features

- **Dashboard**: Provides an overview of the number of patients, doctors, and departments. Displays today's appointments.
- **Appointment Management**: Facilitates the creation, editing, and deletion of appointments. Allows filtering of appointments by doctor, patient, and date.
- **Patient Management**: Allows viewing, creating, editing, and deleting patient records. Displays appointments associated with each patient.
- **Doctor Management**: Enables viewing, creating, editing, and deleting doctor records. Displays appointments associated with each doctor.

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: Bootstrap for styling
- **Database**: SQL Server
- **Entity Framework Core**: For data access and ORM
- **Deployment**: Azure

## Installation

To run this project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/abdelghani-moussaid/HospitalManagementSystem.git
    ```

2. Navigate to the project directory:
    ```bash
    cd carehub
    ```

3. Restore the required NuGet packages:
    ```bash
    dotnet restore
    ```

4. Update the database connection string in `appsettings.json` if necessary.

5. Apply migrations to the database:
    ```bash
    Update-Database
    ```

6. Run the application:
    ```bash
    dotnet run
    ```

## Usage

- **Access the Dashboard**: Navigate to the home page to view an overview of the system.
- **Manage Patients**: Use the Patients section to handle patient records.
- **Manage Doctors**: Use the Doctors section to manage doctor information and view their appointments.
- **Manage Appointments**: Schedule and manage appointments through the Appointments section.

## Contributing

Feel free to fork the repository and submit pull requests. For any issues or feature requests, please open an issue on GitHub.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or feedback, you can reach me at [moussaidabdelghani.pro@gmail.com].
