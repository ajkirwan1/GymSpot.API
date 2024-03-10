namespace GymSpot.API.Models.Domain
{
    public class ExerciseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string BodyArea { get; set; }

        public Guid DifficultyId { get; set; }

        // Navigation properties
        public Difficulty Difficulty { get; set; }
    }
}
