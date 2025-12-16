using MyGraphqlApp.dtos.PainterDto;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Interface.IpaintService
{

    public interface IpaintService
    {
        public  Task<PainterDto.PainterResponse> RegisterPainter(Painter painter);
    }

}