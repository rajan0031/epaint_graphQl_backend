using MyGraphqlApp.Model;

namespace MyGraphqlApp.Interface.IpaintService
{

    public interface IpaintService
    {
        public  Task<Painter> RegisterPainter(Painter painter);
    }

}