using Data.ViewModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly LGAContext db = new LGAContext();

        private int _VicStateId = -1;
        public int VicStateId
        {
            get
            {
                if (_VicStateId == -1)
                {
                    var state = db.States.Single(p => p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase));
                    _VicStateId = state != null ? state.StateId : -1;
                }
                return _VicStateId;
            }
        }
        private IEnumerable<State> _States = Enumerable.Empty<State>();
        public IEnumerable<State> States
        {
            get
            {
                if (_States.Count() == 0)
                {
                    _States = db.States.ToList();
                }

                return _States;
            }
        }


        public IEnumerable<DataModel> GetAllData(int? stateId, int? allScore)
        {
            // Default state
            if (!stateId.HasValue)
            {
                stateId = VicStateId;
            }

            if (!allScore.HasValue)
            {
                allScore = 0;
            }

            var data = Enumerable.Empty<DataModel>();

            if (allScore.HasValue)
            {
                if (allScore.Value > 0)
                {
                    data = db.Scores
                    .Where(p => p.Location.State.StateId == stateId)
                    .Join(db.Locations,
                    s => s.Location.LocationId,
                    l => l.LocationId,
                    (s, l) => new
                    {
                        l.State.StateId,
                        l.PlaceName,
                        s.AdvantageDisadvantageScore,
                        s.DisadvantageScore,
                        s.Year
                    }).Join(db.States,
                    s => s.StateId,
                    st => st.StateId,
                    (s, st) => new DataModel()
                    {
                        StateName = st.StateName,
                        PlaceName = s.PlaceName,
                        Year = s.Year,
                        AdvantageDisadvantageScore = s.AdvantageDisadvantageScore,
                        DisadvantageScore = s.DisadvantageScore,
                        MedianScore = (int?)st.Median
                    })
                    .OrderBy(p => new { p.StateName, p.PlaceName });
                }
                else
                {
                    data = db.Scores
                            .Where(p => p.Location.State.StateId == stateId)
                            .Join(db.Locations,
                            s => s.Location.LocationId,
                            l => l.LocationId,
                            (s, l) => new
                            {
                                l.State.StateId,
                                l.PlaceName,
                                s.AdvantageDisadvantageScore,
                                s.DisadvantageScore,
                                s.Year
                            }).Join(db.States,
                            s => s.StateId,
                            st => st.StateId,
                            (s, st) => new DataModel()
                            {
                                StateName = st.StateName,
                                PlaceName = s.PlaceName,
                                Year = s.Year,
                                AdvantageDisadvantageScore = s.AdvantageDisadvantageScore,
                                DisadvantageScore = s.DisadvantageScore,
                                MedianScore = (int?)st.Median
                            })
                            .Where(x => x.DisadvantageScore > x.MedianScore)
                            .OrderBy(p => new { p.StateName, p.PlaceName });

                }
            }

            return data.ToList();
        }

        public IEnumerable<ReportModel> GetDisadges(int? stateId, int? allScore)
        {
            // Default state
            if (!stateId.HasValue)
            {
                stateId = VicStateId;
            }

            if (!allScore.HasValue)
            {
                allScore = 0;
            }

            var data = Enumerable.Empty<ReportModel>();

            if (stateId != -1)
            {
                var data2011 = db.Scores
                    .Where(p => p.Year == 2011
                        && p.Location.State.StateId == stateId)
                    .Select(p => new { p.Location.LocationId, p.DisadvantageScore });

                var data2016 = db.Scores
                    .Where(p => p.Year == 2016
                        && p.Location.State.StateId == stateId)
                    .Select(p => new { p.Location.LocationId, p.DisadvantageScore });

                var both = data2011.Join(
                    data2016,
                    d11 => d11.LocationId,
                    d16 => d16.LocationId,
                    (d11, d16) => new
                    {
                        d11.LocationId,
                        Score2011 = d11.DisadvantageScore,
                        Score2016 = d16.DisadvantageScore
                    });

                if (allScore.HasValue)
                {
                    if (allScore.Value > 0)
                    {
                        data = both.Join(db.Locations,
                        s => s.LocationId,
                        l => l.LocationId,
                        (s, l) => new { s.Score2011, s.Score2016, s.LocationId, l.PlaceName, l.State.StateId })
                        .Join(db.States,
                        s => s.StateId,
                        st => st.StateId,
                        (s, st) => new ReportModel()
                        {
                            Disadvantage2011 = s.Score2011,
                            Disadvantage2016 = s.Score2016,
                            PlaceName = s.PlaceName,
                            StateName = st.StateName,
                            MedianScore = (int?)st.Median
                        })
                        .OrderBy(p => new { p.StateName, p.PlaceName });
                    }
                    else
                    {
                        data = both.Join(db.Locations,
                        s => s.LocationId,
                        l => l.LocationId,
                        (s, l) => new { s.Score2011, s.Score2016, s.LocationId, l.PlaceName, l.State.StateId })
                        .Join(db.States,
                        s => s.StateId,
                        st => st.StateId,
                        (s, st) => new ReportModel()
                        {
                            Disadvantage2011 = s.Score2011,
                            Disadvantage2016 = s.Score2016,
                            PlaceName = s.PlaceName,
                            StateName = st.StateName,
                            MedianScore = (int?)st.Median
                        }).Where(x => x.MedianScore < x.Disadvantage2011 || x.MedianScore < x.Disadvantage2016)
                        .OrderBy(p => new { p.StateName, p.PlaceName });
                    }
                }

                return data.ToList();
            }

            return Enumerable.Empty<ReportModel>();
        }
    }
}
