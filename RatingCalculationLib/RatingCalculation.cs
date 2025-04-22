using DynamicData;
using Microsoft.EntityFrameworkCore;
using StaffQualificationAssessment.Models;
using System;

namespace RatingCalculationLib
{
    public class RatingCalculation
    {
        private readonly PmContext _db;
        public RatingCalculation(PmContext db)
        {
            _db = db;
        }

        public double GetAbsoluteRating(int userId) 
            => _db.EmployeeMetrics.Include(em => em.Metric)
            .Where(em => em.StaffId == userId)
            .Sum(em => em.Metric.Weight) ?? 0; 
        private List<KeyValuePair<int, double>> GetRatingDictByIds(IEnumerable<int> userIds)
            => userIds.ToDictionary(id => id, GetAbsoluteRating)
                    .OrderByDescending(item => item.Value).ToList();
        public int GetUserRelativeRank(IEnumerable<int> userIds, int userId)
        {
            try
            {
                var relativeRating = GetRatingDictByIds(userIds);
                return relativeRating.FindIndex(item => item.Key == userId) + 1;
            }
            catch
            {
                return 0;
            }
        }
        public IEnumerable<RatingResult> GetRatingList(IEnumerable<int> userIds)
        {
            try
            {
                var relativeRating = GetRatingDictByIds(userIds);
                var absoluteRating = GetRatingDictByIds(_db.Staff.Select(s => s.Id));

                return userIds.Select(id => new RatingResult
                {
                    UserId = id,
                    RelativeRank = relativeRating.FindIndex(item => item.Key == id) + 1,
                    AbsoluteRank = absoluteRating.FindIndex(item => item.Key == id) + 1,
                    AbsoluteRating = absoluteRating.Find(item => item.Key == id).Value
                });
            }
            catch
            {
                return [];
            }
        }
    }
}
