namespace Contact.Api.Exceptions;

public class CustomNoContentException(string errorMassage): Exception(errorMassage) { };