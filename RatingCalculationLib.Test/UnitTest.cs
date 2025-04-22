using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using StaffQualificationAssessment.Models;

namespace RatingCalculationLib.Test
{
    [TestClass]
    public sealed class UnitTest
    {
        private readonly RatingCalculation _service;
        public UnitTest()
        {
            var dbMock = new Mock<PmContext>();
            var employees = new List<Staff>()
            { new() { Id = 1 },  new() { Id = 2 }, new() { Id = 3 }};
            var metrics = new List<Metric>()
            { new() { Id = 1, Weight = 0.1 },  new() { Id = 2, Weight = 0.2 }, new() { Id = 3, Weight = 0.3 }};
            var employeeMetrics = new List<EmployeeMetric>()
            { new() { StaffId = 1, Metric = metrics[0] },
              new() { StaffId = 2, Metric =metrics[0]}, new() { StaffId = 2, Metric = metrics[1] },
              new() { StaffId = 3, Metric =metrics[0]}, new() { StaffId = 3, Metric = metrics[1] }, new() { StaffId = 3, Metric = metrics[2] }};

            dbMock.Setup(x => x.Staff).ReturnsDbSet(employees);
            dbMock.Setup(x => x.Metrics).ReturnsDbSet(metrics);
            dbMock.Setup(x => x.EmployeeMetrics).ReturnsDbSet(employeeMetrics);

            _service = new(dbMock.Object);
        }
        [TestMethod]
        public void GetAbsoluteRating_PassExistingUserId_ReturnRightResult()
        {
            int userId = 1;
            double expected = 0.1;

            double actual = _service.GetAbsoluteRating(userId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetUserRelativeRank_PassCorrectData_ReturnRightRank()
        {
            int userId = 1;
            List<int> userIds = [1, 2];
            int expected = 2;

            int actual = _service.GetUserRelativeRank(userIds, userId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetRatingList_PassCorrectData_ReturnRightList()
        {
            List<int> userIds = [1, 2];
            List<RatingResult> expected =
            [
                new RatingResult { UserId = 1, RelativeRank = 2, AbsoluteRank = 3, AbsoluteRating = 0.1 },
                new RatingResult { UserId = 2, RelativeRank = 1, AbsoluteRank = 2, AbsoluteRating = 0.3 }
            ];

            var actual = _service.GetRatingList(userIds).ToList();

            Assert.IsNotNull(actual );
            Assert.IsTrue(actual.Count > 0 );
            Assert.AreEqual(expected.Count, actual.Count );
            for(int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].UserId, actual[i].UserId);
                Assert.AreEqual(expected[i].RelativeRank, actual[i].RelativeRank);
                Assert.AreEqual(expected[i].AbsoluteRank, actual[i].AbsoluteRank);
                Assert.AreEqual(expected[i].AbsoluteRating, actual[i].AbsoluteRating, 1e-10);            
            }
        }
        [TestMethod]
        public void GetAbsoluteRating_PassNonExistingUserId_ReturnZero()
        {
            int userId = 4;
            double expected = 0;

            double actual = _service.GetAbsoluteRating(userId);

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void GetUserRelativeRank_PassInvalidData_ReturnZero()
        {
            int userId = 6;
            List<int> userIds = [1, 2];
            int expected = 0;

            int actual = _service.GetUserRelativeRank(userIds, userId);

            Assert.AreEqual(expected, actual);

        }
    }
}
