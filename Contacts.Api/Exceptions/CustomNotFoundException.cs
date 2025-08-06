namespace Contacts.Api.Exceptions;

public class CustomNotFoundException(string errorMassage) : Exception(errorMassage) { };