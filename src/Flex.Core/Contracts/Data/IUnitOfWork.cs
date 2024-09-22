namespace Flex.Core.Contracts.Data
{
    public interface IUnitOfWork
    {
        #region Repositories
        #endregion

        Task<int> CompleteAsync();
    }
}