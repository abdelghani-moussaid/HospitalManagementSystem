using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Fetch statistics
            var totalPatients = await _context.Patients.CountAsync();
            var totalDoctors = await _context.Doctors.CountAsync();
            var uniqueSpecialties = await _context.Doctors.Select(d => d.Specialty).Distinct().CountAsync();
            var todaysAppointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.AppointmentDate.Date == DateTime.Today)
                .ToListAsync();

            // Filter appointments
            var filteredAppointments = Enumerable.Empty<Appointment>().ToList();
            if (startDate.HasValue && endDate.HasValue)
            {
                filteredAppointments = await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .Where(a => a.AppointmentDate.Date >= startDate.Value.Date && a.AppointmentDate.Date <= endDate.Value.Date)
                    .ToListAsync();
            }

            // Pass data to the view
            ViewData["TotalPatients"] = totalPatients;
            ViewData["TotalDoctors"] = totalDoctors;
            ViewData["UniqueSpecialties"] = uniqueSpecialties;
            ViewData["TodaysAppointments"] = todaysAppointments;
            ViewData["FilteredAppointments"] = filteredAppointments;

            return View();
        }
    }
}
