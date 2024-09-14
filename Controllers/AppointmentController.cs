using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment/Index
        public async Task<IActionResult> Index(int? doctorId, int? patientId, DateTime? appointmentDate)
        {
            // Start with the base query
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();

            // Apply filters
            if (doctorId.HasValue)
            {
                appointments = appointments.Where(a => a.DoctorId == doctorId.Value);
            }
            if (patientId.HasValue)
            {
                appointments = appointments.Where(a => a.PatientId == patientId.Value);
            }
            if (appointmentDate.HasValue)
            {
                // Filter by exact date if provided
                appointments = appointments.Where(a => a.AppointmentDate.Date == appointmentDate.Value.Date);
            }
            else
            {
                // Default to upcoming appointments starting from today
                var today = DateTime.Today;
                appointments = appointments.Where(a => a.AppointmentDate >= today);
            }

            // Order by appointment date (ascending - soonest to farthest)
            appointments = appointments.OrderBy(a => a.AppointmentDate);

            // Prepare ViewBag data for dropdown filters
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            ViewBag.Patients = await _context.Patients.ToListAsync();

            // Execute the query and pass the sorted and filtered appointments to the view
            return View(await appointments.ToListAsync());
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            // Convert doctors list to SelectList
            var doctors = _context.Doctors
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();

            // Convert patients list to SelectList
            var patients = _context.Patients
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();

            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;

            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                // Convert DTO to Appointment entity
                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDTO.AppointmentDate,
                    PatientId = appointmentDTO.PatientId,
                    DoctorId = appointmentDTO.DoctorId
                };

                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Populate ViewBag with SelectList for doctors and patients
            ViewBag.Doctors = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name");
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");

            return View(appointmentDTO);
        }


        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentDTO = new AppointmentDTO
            {
                AppointmentDate = appointment.AppointmentDate,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId
            };

            // Populate ViewBag with SelectList for doctors and patients
            ViewBag.Doctors = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name", appointment.DoctorId);
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name", appointment.PatientId);

            return View(appointmentDTO);
        }


        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentDTO appointmentDTO)
        {
            if (id != appointmentDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var appointment = await _context.Appointments.FindAsync(id);

                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.AppointmentDate = appointmentDTO.AppointmentDate;
                appointment.DoctorId = appointmentDTO.DoctorId;
                appointment.PatientId = appointmentDTO.PatientId;

                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Populate ViewBag with SelectList for doctors and patients
            ViewBag.Doctors = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name", appointmentDTO.DoctorId);
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name", appointmentDTO.PatientId);

            return View(appointmentDTO);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}

