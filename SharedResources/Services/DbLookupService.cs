using Microsoft.EntityFrameworkCore;
using static SharedResources.Services.IDbLookupService;

namespace SharedResources.Services;

public class DbLookupService : IDbLookupService
{
    public (ItemExistStatus, DataType) VerifyItemID<DataType>(int Id, string IdProperty, List<DataType> list) where DataType : class
    {
        var itemType = typeof(DataType).GetProperty(IdProperty);
        var item = list.ToList().FirstOrDefault(item =>
            itemType!.GetValue(item)!.Equals(Id)
        ); ;
        return (item == default ?
            ItemExistStatus.ItemDoesNotExist :
            ItemExistStatus.ItemExists, item)!;
    }
}