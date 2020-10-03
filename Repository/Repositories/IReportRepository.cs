using Data.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository.Repositories
{

    public interface IReportRepository : IDisposable
    {
        IEnumerable<State> States { get; }
        int VicStateId { get; }

        IEnumerable<DataModel> GetAllData(int? stateId, int? allScore);
        IEnumerable<ReportModel> GetDisadges(int? stateId, int? allScore);
    }
}
