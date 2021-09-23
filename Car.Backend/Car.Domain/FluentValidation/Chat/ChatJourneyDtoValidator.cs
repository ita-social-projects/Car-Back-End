using Car.Data.Constants;
using Car.Domain.Dto;
using Car.Domain.Dto.ChatDto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ChatJourneyDtoValidator : AbstractValidator<ChatJourneyDto>
    {
        public ChatJourneyDtoValidator()
        {
            RuleFor(chatJourney => chatJourney.DepartureTime).NotNull(); // TODO
        }
    }
}