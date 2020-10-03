using Data.ViewModel;
using Repository;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository = new ReportRepository();

        private IEnumerable<State> _States = Enumerable.Empty<State>();
        
        public IEnumerable<State> States
        {
            get
            {
                if (_States.Count() == 0)
                {
                    _States = reportRepository.States.ToList();
                }

                return _States;
            }
        }

        public IEnumerable<DataModel> GetAllData(int? stateId, int? allScore)
        {
            return reportRepository.GetAllData(stateId, allScore);            
        }

        public IEnumerable<ReportModel> GetDisadges(int? stateId, int? allScore)
        {
            return reportRepository.GetDisadges(stateId, allScore);
        }
    }
}
