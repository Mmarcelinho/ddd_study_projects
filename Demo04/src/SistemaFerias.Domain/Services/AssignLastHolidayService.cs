namespace SistemaFerias.Domain.Services;

public static class AssignLastHolidayService
{
    public static void AssignLastHoliday(Employee employee, VacationRequest vacationRequest)
    {
        if (vacationRequest.Status != EStatus.Approved)
            return;

        employee.UpdateLastVacationDate(vacationRequest.EndDate);
    }
}
