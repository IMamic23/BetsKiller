using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.Leaders
{
    public class LeadersViewModel
    {
        public List<LeadersCategoryPlayerViewModel> PointsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> AssistsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> ReboundsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> FieldGoal { get; set; }
        public List<LeadersCategoryPlayerViewModel> FreeThrow { get; set; }
        public List<LeadersCategoryPlayerViewModel> ThreePointFieldGoal { get; set; }
        public List<LeadersCategoryPlayerViewModel> OffensiveReboundsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> DefensiveReboundsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> BlocksPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> StealsPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> AssistsToTurnovers { get; set; }
        public List<LeadersCategoryPlayerViewModel> StealsToTurnovers { get; set; }
        public List<LeadersCategoryPlayerViewModel> MinutesPerGame { get; set; }
        public List<LeadersCategoryPlayerViewModel> GamesPlayed { get; set; }
        public List<LeadersCategoryPlayerViewModel> DoubleDoubles { get; set; }
        public List<LeadersCategoryPlayerViewModel> TripleDoubles { get; set; }
    }
}
