using ShopTARge23.Core.Domain;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface ISpaceshipsServices
    {
        Task<Spaceship> DetailAsync(Guid id);
    }
}
