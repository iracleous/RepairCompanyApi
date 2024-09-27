namespace RepairCompanyApi.Dtos;

public class ApiResult<T>
{
    public T? Result { get; set; }
    public string Error { get; set; } = string.Empty;
    public int Status { get; set; }
}
