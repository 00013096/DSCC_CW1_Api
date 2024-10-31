using AutoMapper;
using DSCC_CW1_Api.Dto;
using DSCC_CW1_Api.Interfaces;
using DSCC_CW1_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_CW1_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

        [HttpGet("{id}", Name = "GetDepartmentById")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(_mapper.Map<DepartmentDto>(department));
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);
            await _departmentRepository.AddAsync(department);

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return CreatedAtRoute("GetDepartmentById", new { id = departmentDto.Id }, departmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentCreateDto departmentUpdateDto)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();

            _mapper.Map(departmentUpdateDto, department);
            await _departmentRepository.UpdateAsync(department);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();

            await _departmentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
