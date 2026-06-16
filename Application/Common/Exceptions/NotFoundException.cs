namespace Application.Common.NotFoundException;

public class NotFoundException(Guid id, string entity) : Exception($"the {entity} with id: {id} not found.");

