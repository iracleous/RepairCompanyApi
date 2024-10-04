namespace RepairCompanyApi.Dtos;

public class ApiResult<T>
{
    public T? Result { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}
