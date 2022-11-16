namespace Khoaluan.InterfacesService
{
    public interface IDiscussionService
    {
        void comment(string postID, string userName, string message);
    }
}
