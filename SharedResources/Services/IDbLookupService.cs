using Microsoft.EntityFrameworkCore;

namespace SharedResources.Services;

public interface IDbLookupService
{
    public enum ItemExistStatus
    {
        ItemExists,
        ItemDoesNotExist
    }
    public (ItemExistStatus, DataType) VerifyItemID<DataType>(int Id, string IdProperty, List<DataType> list) where DataType : class;
}