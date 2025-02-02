 
public class MusicStoreAddress
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAdres { get; set; }
     public string Name { get; set; }
    public string Descriptions { get; set; }
    public string Country { get; set; }
    public string PostCode { get; set; }
}

public class OpeningHour
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; }
    public string Openhours { get; set; }
}
