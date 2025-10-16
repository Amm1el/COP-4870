using MedicalChartingMaui.Models;

namespace MedicalChartingMaui.Services;

public class InMemoryAppointmentService : IAppointmentService
{
    private readonly List<Appointment> _items = new();

    public Task<List<Appointment>> GetAllAsync() => Task.FromResult(_items.OrderBy(a => a.Start).ToList());
    public Task<Appointment?> GetAsync(Guid id) => Task.FromResult(_items.FirstOrDefault(a => a.Id == id));

    public Task AddAsync(Appointment appt)
    {
        ValidateBusinessRules(appt, appt.Id);
        _items.Add(appt);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Appointment appt)
    {
        ValidateBusinessRules(appt, appt.Id);
        var i = _items.FindIndex(a => a.Id == appt.Id);
        if (i >= 0) _items[i] = appt;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _items.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }

    private static readonly TimeSpan Open = TimeSpan.FromHours(8);
    private static readonly TimeSpan Close = TimeSpan.FromHours(17);

    private void ValidateBusinessRules(Appointment appt, Guid selfId)
    {
        // 1) Business days
        if (appt.Start.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            throw new InvalidOperationException("Appointments must be Mon–Fri.");

        // 2) Within 8–5 window (must end by 5)
        var startT = appt.Start.TimeOfDay;
        var endT = (appt.Start + appt.Length).TimeOfDay;
        if (startT < Open || endT > Close)
            throw new InvalidOperationException("Appointments must be between 8:00 and 17:00.");

        // 3) No physician double-booking (overlap)
        bool Overlaps(Appointment a, Appointment b)
        {
            var aEnd = a.Start + a.Length;
            var bEnd = b.Start + b.Length;
            return a.Start < bEnd && b.Start < aEnd;
        }

        if (_items.Any(a => a.Id != selfId &&
                            a.PhysicianId == appt.PhysicianId &&
                            Overlaps(a, appt)))
        {
            throw new InvalidOperationException("Physician is already booked in that slot.");
        }
    }
}
