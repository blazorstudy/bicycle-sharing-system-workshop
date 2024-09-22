namespace BicycleSharingSystem.Kiosk.Queries;

// DTO 모델
public class RentalOfficeDTO
{
    public string OfficeId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; } 
}

public interface IRentalOfficeQuery
{
    Task<List<RentalOfficeDTO>> Get();
}

// 실제 연결 데이터 
public class RentalOfficeQuery : IRentalOfficeQuery
{
    public async Task<List<RentalOfficeDTO>> Get()
    {
        throw new NotImplementedException();
    }
}
// 테스트 데이터 
public class TestRentalOfficeQuery : IRentalOfficeQuery
{
    public async Task<List<RentalOfficeDTO>> Get()
    {
        return new List<RentalOfficeDTO>
        {
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소1", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소2", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소3", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "BMW대여소1", Region = "경기"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "BMW대여소2", Region = "경기"},
        };
    }
}