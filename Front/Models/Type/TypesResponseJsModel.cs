namespace Front.Models.Type;

public class TypesResponseJsModel: BaseResponseJsModel
{
    public ICollection<TypeResponseJs> Types { get; set; }
}