namespace stock_manager_api.Repository
{
    public interface IRepository<Res, Req>
    {
        Res Add(Req resource);
        Res Update(Req resource, int entityId);
        void Delete(int entityId);
        IEnumerable<Res> GetAll();
        Res GetById(int entityId);
    }
}