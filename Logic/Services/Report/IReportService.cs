using Data.ViewModel;
using System.Collections.Generic;

namespace Logic.Services.Report
{
    public interface IReportService
    {
        IEnumerable<State> States { get; }
        
        IEnumerable<DataModel> GetAllData(int? stateId, int? allScore);
        IEnumerable<ReportModel> GetDisadges(int? stateId, int? allScore);
    }
}