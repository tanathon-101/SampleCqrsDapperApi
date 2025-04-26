namespace SampleCqrsDapperApi.Application.DTOs
{
    public class CustomerWithOrdersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<OrderDto> Orders { get; set; } = new();
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}