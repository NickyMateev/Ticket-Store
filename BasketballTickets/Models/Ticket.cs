namespace BasketballTickets.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SeatNo { get; set; }
        public decimal Price { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
