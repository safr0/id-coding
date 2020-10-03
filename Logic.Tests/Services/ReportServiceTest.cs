using System.Collections.Generic;
using Data.ViewModel;
using Logic.Services.Report;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logic.Tests.Services
{

    [TestClass]
    public class ReportServiceTest
    {        
        private Mock<IReportService> mock;
        private Mock<IEnumerable<ReportModel>> mockReportData;
        private Mock<IEnumerable<DataModel>> mockScoretData;

        [TestMethod]
        public void ReportServiceGetDisadgesData_IsNotNull()
        {
            mock = new Mock<IReportService>();
            mockReportData = new Mock<IEnumerable<ReportModel>>();

            var test = mock.Setup(x => x.GetDisadges(It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(mockReportData.Object);

            Assert.IsNotNull(test);            
        }

        [TestMethod]
        public void ReportServiceGetAllData_IsNotNull()
        {
            mock = new Mock<IReportService>();
            mockScoretData = new Mock<IEnumerable<DataModel>>();

            var test = mock.Setup(x => x.GetAllData(It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(mockScoretData.Object);

            Assert.IsNotNull(test);
        }

        //todo remaining tests.

    }
}
