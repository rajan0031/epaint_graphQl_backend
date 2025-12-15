using HotChocolate.Authorization;
using MyGraphqlApp.InputType.PainterInputs;
using MyGraphqlApp.Interface.IpaintService;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Mutation.PainterMutation
{


    [ExtendObjectType(typeof(RootMutation))]
    public class PainterMutation
    {

        private readonly IpaintService _ipaintService;

        public PainterMutation(IpaintService ipaintService)
        {
            _ipaintService = ipaintService;
        }
        [Authorize]
        public Task<Painter> RegisterPainter(createPainterInput createPainterInput)
        {
            var registerObj = new Painter();
            registerObj.Name = createPainterInput.Name;
            registerObj.UserName = createPainterInput.UserName;
            registerObj.Email = createPainterInput.Email;
            registerObj.Password = createPainterInput.Password;
            registerObj.PhoneNumber = createPainterInput.PhoneNumber;
            registerObj.Address = createPainterInput.Address;
            registerObj.City = createPainterInput.City;
            registerObj.ServiceTypes = createPainterInput.ServiceTypes;
            registerObj.ExperienceYears = createPainterInput.ExperienceYears;
            registerObj.Rating = createPainterInput.Rating;
            registerObj.PinCodes = createPainterInput.PinCodes;

            return _ipaintService.RegisterPainter(registerObj);
        }

    }


}