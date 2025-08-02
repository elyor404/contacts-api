namespace Contact.Api.Exceptions;

public class CustomConflictException(string errorMessage) : Exception(errorMessage) { };