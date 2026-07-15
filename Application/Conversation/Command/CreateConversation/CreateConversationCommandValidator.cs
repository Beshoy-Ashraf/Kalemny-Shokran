using FluentValidation;

namespace Application.Conversation.Command.CreateConversation;

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
      public CreateConversationCommandValidator()
      {
            RuleFor(x => x.CreatorId).NotEmpty().WithMessage("Creator Id is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ProfilePictureUrl).NotEmpty().WithMessage("ProfilePictureUrl is required");
            RuleFor(x => x.UsersId).NotEmpty().WithMessage("UsersId is required");
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId is required");
      }
}
