namespace Hospital.Application;

public class AppointmentService(AppointmentRepository appointmentRepository, DepartmentValidatorFactory departmentValidatorFactory)
{
    private readonly AppointmentRepository _repository = appointmentRepository ?? throw new ArgumentNullException();
    private readonly DepartmentValidatorFactory _validatorFactory = departmentValidatorFactory ?? throw new ArgumentNullException();

    public async Task<bool> ScheduleAppointment(
        string cpr, string patientName, DateTime appointmentDate,
        string department, string doctorName)
    {
        // Basic validation
        if (string.IsNullOrEmpty(cpr) || string.IsNullOrEmpty(department) ||
            string.IsNullOrEmpty(doctorName) || appointmentDate < DateTime.Now)
        {
            Console.WriteLine("[ERROR] Invalid appointment request.");
            return false;
        }

        // Validate CPR before scheduling
        if (!await new NationalRegistryService().ValidateCpr(cpr))
        {
            Console.WriteLine("[ERROR] Invalid CPR number. Cannot schedule appointment.");
            return false;
        }

        Console.WriteLine($"[LOG] Scheduling appointment for {patientName} (CPR: {cpr}) in {department} with {doctorName} on {appointmentDate}");


        var validator = _validatorFactory.GetValidator(department);
        if (validator == null)
        {
            Console.WriteLine($"[ERROR] Unsupported department: {department}");
            return false;
        }

        var (isValid, errorMessage) = await validator.ValidateAsync(cpr, doctorName);
        if (!isValid)
        {
            Console.WriteLine($"[ERROR] {errorMessage}");
            return false;
        }

        await appointmentRepository.AddAsync(new Appointment
        {
            Cpr = cpr,
            PatientName = patientName,
            AppointmentDate = appointmentDate,
            Department = department,
            DoctorName = doctorName
        });

        Console.WriteLine($"[LOG] Appointment successfully scheduled for {patientName} (CPR: {cpr})");
        return true;
    }
}