namespace BicycleSharingSystem.Kiosk.Queries;
// DTO 모델
public sealed class BicycleDTO
{
    public string BicycleId { get; set; }
    public string RentalOfficeId { get; set; }
    public DateTime? StartRentalTime { get; set; }
    public string Name { get; set; }
    public DateTime? ExpireRentalTime { get; set; }
    public bool IsRental => StartRentalTime.HasValue;
}

public interface IBicycleQuery
{
    Task<List<BicycleDTO>> Get(string RentalOfficeId);
}

// 실제 연결 데이터 
public class BicycleQuery: IBicycleQuery
{
    public async Task<List<BicycleDTO>> Get(string RentalOfficeId)
    {
        throw new NotImplementedException();
    }
}

// 테스트 데이터 쿼리
public class TestBicycleQuery :IBicycleQuery
{
    public async Task<List<BicycleDTO>> Get(string RentalOfficeId)
    {
        return new List<BicycleDTO>()
        {
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 1호기"
            },
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 2호기"
            },
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 3호기",
                StartRentalTime = DateTime.Now,
                ExpireRentalTime = DateTime.Now.AddMinutes(5)
            },
        };
    }
}    