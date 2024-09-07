namespace TMS.Domain.Entities
{
    public class UserTeam
    {
        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;

        public int TeamId { get; set; }
        public Team Team { get; set; } = default!;
    }
}
