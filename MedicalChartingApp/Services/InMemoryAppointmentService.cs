using MedicalChartingApp.Models;

namespace MedicalChartingApp.Services;

public class InMemoryAppointmentService : IAppointmentService
{
    private readonly List<Appointment> _items = new();

    public Task<List<Appointment>> GetAllAsync()
        => Task.FromResult(_items.OrderBy(a => a.Start).ToList());

    public Task<Appointment?> GetAsync(Guid id)
        => Task.FromResult(_items.FirstOrDefault(a => a.Id == id));

    public Task AddAsync(Appointment appt)
    {
        ValidateBusinessRules(appt, appt.Id);
        _items.Add(appt);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Appointment appt)
    {
        ValidateBusinessRules(appt, appt.Id);
        var index = _items.FindIndex(a => a.Id == appt.Id);
        if (index >= 0)
            _items[index] = appt;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _items.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }

    // Business rules
    private static readonly TimeSpan Open = TimeSpan.FromHours(8);
    private static readonly TimeSpan Close = TimeSpan.FromHours(17);

    private void ValidateBusinessRules(Appointment appt, Guid selfId)
    {
        // 1) Only weekdays
        if (appt.Start.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            throw new InvalidOperationException("Appointments must be scheduled Monday through Friday.");

        // 2) Only 8am–5pm, including end time
        var startTime = appt.Start.TimeOfDay;
        var endTime = (appt.Start + appt.Length).TimeOfDay;

        if (startTime < Open || endTime > Close)
            throw new InvalidOperationException("Appointments must be between 8:00 and 17:00.");

        // 3) No double booking of the physician
        bool Overlaps(Appointment a, Appointment b)
        {
            var aEnd = a.Start + a.Length;
            var bEnd = b.Start + b.Length;
            return a.Start < bEnd && b.Start < aEnd;
        }

        if (_items.Any(a =>
                a.Id != selfId &&
                a.PhysicianId == appt.PhysicianId &&
                Overlaps(a, appt)))
        {
            throw new InvalidOperationException("This physician is already booked in that time slot.");
        }
    }
}
