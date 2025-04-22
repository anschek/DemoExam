using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingCalculationLib
{
    public class RatingResult
    {
        public int UserId { get; set; }
        public double AbsoluteRating { get; set; }
        public int AbsoluteRank { get; set; }
        public int RelativeRank { get; set; }
    }
}
