using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Dtos;

namespace RepairCompanyApi.Services;

public interface IPropertyOwnerService2
{

    Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerData(int pageCount, int pageSize);

}
