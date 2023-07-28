using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Posts
{
    
    public class LikePostResponse : Notifiable
    {
        public LikePostResponse()
        {
        }

        public LikePostResponse(Guid id, bool value)
        {
            Id = id;
            Value = value;
        }

        public Guid Id { get; set; }
        public bool Value { get; set; }


    }
}
