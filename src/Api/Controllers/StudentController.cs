namespace Api.Controllers
{
    using System.Linq;
    using Api.Models;
    using Api.Repositories;
    using Api.Validators;
    using CSharpFunctionalExtensions;
    using DomainModel;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/students")]
    public class StudentController : ApplicationController
    {
        private readonly CourseRepository courseRepository;
        private readonly StateRepository stateRepository;
        private readonly StudentRepository studentRepository;

        public StudentController(
            StudentRepository studentRepository,
            CourseRepository courseRepository,
            StateRepository stateRepository)
        {
            this.courseRepository = courseRepository;
            this.stateRepository = stateRepository;
            this.studentRepository = studentRepository;
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            var v = new RegisterRequestValidator();
            ValidationResult result = v.Validate(request);

            //var v = new StudentValidator();
            //ValidationResult result = v.Validate(
            //    request, options => options.IncludeRuleSets("Email").IncludeRulesNotInRuleSet());

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            //Models.Address[] addresses = request.Addresses
            //    .Select(x => DomainModel.Address.Create(x.Street, x.City, x.State, x.ZipCode, _stateRepository.GetAll()).Value)
            //    .ToArray();

            //Email email = Email.Create(request.Email).Value;
            //string name = request.Name.Trim();

            //Student existingStudent = _studentRepository.GetByEmail(email);
            //if (existingStudent != null)
            //    return Error(Errors.Student.EmailIsTaken());

            //var student = new Student(email, name, addresses);
            //_studentRepository.Save(student);

            //var response = new RegisterResponse
            //{
            //    Id = student.Id
            //};
            //return Ok(response);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, EditPersonalInfoRequest request)
        {
            var v = new EditPersonalInfoRequestValidator();
            ValidationResult result = v.Validate(request);

            //var v = new StudentValidator();
            //ValidationResult result = v.Validate(request);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }
            DomainModel.Address[] adresses = request.Addresses.Select(
                a => new DomainModel.Address(a.Street, a.City, a.State, a.ZipCode)).ToArray();

            //Address[] addresses = request.Addresses
            //    .Select(x => Address.Create(x.Street, x.City, x.State, x.ZipCode, _stateRepository.GetAll()).Value)
            //    .ToArray();
            //string name = request.Name.Trim();

            DomainModel.Student student = studentRepository.GetById(id);
            if (student == null)
                return Error(Errors.General.NotFound(), nameof(id));



            //student.EditPersonalInfo(name, addresses);
            //_studentRepository.Save(student);

            return Ok();
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, EnrollRequest request)
        {
            DomainModel.Student student = studentRepository.GetById(id);
            if (student == null)
                return Error(Errors.General.NotFound(), nameof(id));

            (string Course, string Grade)[] input = request.Enrollments
                .Select(x => (x.Course, x.Grade))
                .ToArray();
            Course[] allCourses = courseRepository.GetAll();

            Result<Enrollment[], Error> enrollmentsOrError = Enrollment.Create(input, allCourses);
            if (enrollmentsOrError.IsFailure)
                return Error(enrollmentsOrError.Error);

            Result<object, Error> result = student.Enroll(enrollmentsOrError.Value);
            if (result.IsFailure)
                return Error(result.Error);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            DomainModel.Student student = studentRepository.GetById(id);

            //var resonse = new GetResonse
            //{
            //    Addresses = student.Addresses.Select(x =>
            //        new DomainModel.Address
            //        {
            //            Street = x.Street,
            //            City = x.City,
            //            State = x.State.Value,
            //            ZipCode = x.ZipCode
            //        })
            //        .ToArray(),
            //    Email = student.Email.Value,
            //    Name = student.Name,
            //    Enrollments = student.Enrollments.Select(x => new CourseEnrollmentDto
            //    {
            //        Course = x.Enrollment.Course.Name,
            //        Grade = x.Enrollment.Grade.ToString()
            //    }).ToArray()
            //};
            //return Ok(resonse);
            return Ok();
        }
    }
}
