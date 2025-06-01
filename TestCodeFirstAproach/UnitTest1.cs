using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TestCodeFirstAproach;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CheckIfMedicamentExists_WhenExists()
    {
        var options = new DbContextOptionsBuilder<DbContext1>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new DbContext1(options);
        context.Medicaments.Add(new Medicament
        {
            IdMedicament = 1,
            Name = "TestMed",
            Description = "Test Desc",
            Type = "Tablet"
        });
        await context.SaveChangesAsync();
        var repository = new MedicamentRepository(context);
        var result = await repository.DoesMedicamentExist(1, CancellationToken.None);
        Assert.IsTrue(result);
    }
    [Test]
    public async Task CreatePatientIfDoesntExist_AddsNewPatient_WhenNotExists()
    {
        var options = new DbContextOptionsBuilder<DbContext1>()
            .UseInMemoryDatabase("CreatePatientTest1")
            .Options;

        using var context = new DbContext1(options);
        var repo = new PatientRepository(context);

        var patient = new Patient { IdPatient = 1, FirstName = "John", LastName = "Doe", BirthDate = DateTime.Now };

        var result = await repo.createPatientIfDoesntExist(patient, CancellationToken.None);

        Assert.IsTrue(result);
        Assert.AreEqual(1, context.Patients.Count());
    }

    [Test]
    public async Task CreatePatientIfDoesntExist_ReturnsFalse_WhenPatientExists()
    {
        var options = new DbContextOptionsBuilder<DbContext1>()
            .UseInMemoryDatabase("CreatePatientTest2")
            .Options;

        using var context = new DbContext1(options);
        
        context.Patients.Add(new Patient 
        { 
            IdPatient = 1, 
            FirstName = "John", 
            LastName = "Doe", 
            BirthDate = new DateTime(1990, 1, 1) 
        });
        await context.SaveChangesAsync();

        var repo = new PatientRepository(context);
        
        var result = await repo.createPatientIfDoesntExist(new Patient 
        { 
            IdPatient = 1, 
            FirstName = "Duplicate", 
            LastName = "Person", 
            BirthDate = new DateTime(1985, 5, 5) 
        }, CancellationToken.None);

        Assert.IsFalse(result);
        Assert.AreEqual(1, context.Patients.Count());
    }
    [Test]
public async Task AddPrescription_SavesPrescriptionWithMedicaments()
{
    var options = new DbContextOptionsBuilder<DbContext1>()
        .UseInMemoryDatabase("AddPrescriptionTest")
        .Options;

    using var context = new DbContext1(options);

    var repo = new PrescriptionRepository(context);

    var prescription = new Prescription
    {
        IdDoctor = 1,
        IdPatient = 1,
        Date = DateTime.Today,
        DueDate = DateTime.Today.AddDays(7)
    };

    var medicament = new Medicament { IdMedicament = 1, Name = "Aspirin", Description = "Painkiller", Type = "Tablet" };
    context.Medicaments.Add(medicament);
    await context.SaveChangesAsync();

    var prescriptionMedicaments = new List<PrescriptionMedicament>
    {
        new PrescriptionMedicament
        {
            IdMedicament = 1,
            Dose = 2,
            Details = "Take twice daily"
        }
    };

    var result = await repo.AddPrescription(prescription, prescriptionMedicaments, CancellationToken.None);

    Assert.IsTrue(result);
    Assert.AreEqual(1, context.Prescriptions.Count());
    Assert.AreEqual(1, context.PrescriptionMedicaments.Count());
}

    [Test]
    public async Task GetPrescriptionsByPatientId_ReturnsCorrectPrescriptions()
    {
        var options = new DbContextOptionsBuilder<DbContext1>()
            .UseInMemoryDatabase("GetPrescriptionsTest")
            .Options;

        using var context = new DbContext1(options);

        var patient = new Patient
        {
            IdPatient = 5,
            FirstName = "Jane",
            LastName = "Doe",
            BirthDate = new DateTime(1995, 4, 12)
        };

        var doctor = new Doctor
        {
            IdDoctor = 10,
            FirstName = "Dr",
            LastName = "Smith",
            Email = "dr@example.com"
        };

        context.Patients.Add(patient);
        context.Doctors.Add(doctor);

        var prescription = new Prescription
        {
            IdPrescription = 1,
            IdPatient = patient.IdPatient,
            Patient = patient,
            IdDoctor = doctor.IdDoctor,
            Doctor = doctor,
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(3)
        };

        context.Prescriptions.Add(prescription);
        await context.SaveChangesAsync();

        var repo = new PrescriptionRepository(context);
        var result = await repo.GetPrescriptionsByPatientId(5, CancellationToken.None);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(5, result[0].IdPatient);
        Assert.AreEqual("Jane", result[0].Patient.FirstName);
        Assert.AreEqual("Smith", result[0].Doctor.LastName);
    }

[Test]
public async Task GetPrescriptionMedicamentsByPrescriptionIdAsync_ReturnsCorrectMedicaments()
{
    var options = new DbContextOptionsBuilder<DbContext1>()
        .UseInMemoryDatabase("GetPrescriptionMedsTest")
        .Options;

    using var context = new DbContext1(options);
    context.Medicaments.Add(new Medicament { IdMedicament = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Capsule" });
    context.Prescriptions.Add(new Prescription
    {
        IdPrescription = 7,
        IdPatient = 1,
        IdDoctor = 1,
        Date = DateTime.Today,
        DueDate = DateTime.Today.AddDays(5)
    });
    await context.SaveChangesAsync();

    context.PrescriptionMedicaments.Add(new PrescriptionMedicament
    {
        IdPrescription = 7,
        IdMedicament = 2,
        Dose = 1,
        Details = "After meal"
    });
    await context.SaveChangesAsync();

    var repo = new PrescriptionRepository(context);
    var result = await repo.GetPrescriptionMedicamentsByPrescriptionIdAsync(7, CancellationToken.None);

    Assert.AreEqual(1, result.Count);
    Assert.AreEqual("Ibuprofen", result[0].Medicament.Name);
}
}