using HospitalManagementSystem.Models;
using System;

namespace HospitalManagementSystem.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if any data exists
            if (context.Doctors.Any() || context.Patients.Any() || context.Appointments.Any())
            {
                return; // DB has been seeded
            }

            // Add Doctors
            var doctors = new[]
            {
            new Doctor { Name = "Dr. Smith", Specialty = "Cardiology" },
            new Doctor { Name = "Dr. Johnson", Specialty = "Neurology" }
        };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Add Patients
            var patients = new[]
            {
            new Patient { Name = "Alice Brown", DateOfBirth = new DateTime(1985, 5, 15), Address = "123 Maple St" },
            new Patient { Name = "Bob Green", DateOfBirth = new DateTime(1990, 7, 20), Address = "456 Oak St" }
        };
            context.Patients.AddRange(patients);
            context.SaveChanges();

            // Add Appointments
            var appointments = new[]
            {
            new Appointment
            {
                AppointmentDate = new DateTime(2024, 9, 20, 10, 30, 0),
                PatientId = patients[0].Id,
                DoctorId = doctors[0].Id
            },
            new Appointment
            {
                AppointmentDate = new DateTime(2024, 9, 21, 14, 00, 0),
                PatientId = patients[1].Id,
                DoctorId = doctors[1].Id
            }
        };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();
        }
    }
}
