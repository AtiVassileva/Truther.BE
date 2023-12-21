namespace Truther.API.Models
{
    public partial class Like
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }
        public int? Count { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
