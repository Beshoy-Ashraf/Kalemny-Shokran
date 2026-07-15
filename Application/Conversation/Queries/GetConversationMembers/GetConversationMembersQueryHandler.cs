using Application.Users.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetConversationMembers;

public class GetConversationMembersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetConversationMembersQuery, IEnumerable<UserResponse>>
{
      public async Task<IEnumerable<UserResponse>> Handle(GetConversationMembersQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.ConversationRepository.GetConversationMembersAsync(request.ConversationId, cancellationToken);
            var userResponses = result.Select(u => new UserResponse(u));
            return userResponses;
      }
}
