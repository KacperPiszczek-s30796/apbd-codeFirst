namespace CodeFirstAproach.exceptions;

public class MedicationDoesnotExistException(int id): Exception($"Medication with id {id} does not exist")
{
    
}