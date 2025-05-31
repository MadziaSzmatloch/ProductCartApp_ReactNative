namespace Cart.Domain.Events.Cart
{
    public record CartCreated
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid UserId { get; set; }
    }
}
