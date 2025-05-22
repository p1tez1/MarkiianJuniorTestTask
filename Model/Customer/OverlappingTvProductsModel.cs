namespace Model
{
    public class OverlappingTvProductsModel
    {
        public Guid ProductId1 { get; set; }
        public Guid CustomerId {  get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProductName1 {  get; set; }
        public DateTime StartDate1 { get; set; }
        public DateTime? EndDate1 { get; set; }
        public Guid ProductId2 {  get; set; }
        public string ProductName2 { get; set; }
        public DateTime StartDate2 {  get; set; }
        public DateTime? EndDate2 {  get; set; }
    }
}
