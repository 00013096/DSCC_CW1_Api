using AutoMapper;
using DSCC_CW1_Api.Dto;
using DSCC_CW1_Api.Interfaces;
using DSCC_CW1_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_CW1_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeCreateDto employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            await _employeeRepository.AddAsync(employee);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeCreateDto employeeUpdateDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();

            _mapper.Map(employeeUpdateDto, employee);
            await _employeeRepository.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();

            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
