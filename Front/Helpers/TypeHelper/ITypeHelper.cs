using Front.Models.Type;

namespace Front.Helpers.TypeHelper;

public interface ITypeHelper
{
    Task<TypesResponseJsModel> GetTypes();
}