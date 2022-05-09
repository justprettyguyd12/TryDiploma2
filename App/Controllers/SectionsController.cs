using AutoMapper;
using DataAccess.Services;
using Domain.Models.Crm;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.ViewModel.SectionModels;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class SectionsController : Controller
{
    private readonly IService<Section> _sectionsService;
    private readonly IService<Funnel> _funnelService;
    private readonly IMapper _mapper;

    public SectionsController(IService<Section> service, IMapper mapper, IService<Funnel> funnelsService, IService<Funnel> funnelService)
    {
        _sectionsService = service;
        _mapper = mapper;
        _funnelService = funnelService;
    }

    /// <summary>
    /// Получить список всех секций
    /// Для каждой секции показывается соответствующая воронка
    /// </summary>
    /// <returns>Коллекция всех секций</returns>
    [HttpGet]
    public ICollection<Section> Get()
    {
        var sections = _sectionsService.GetList();
        foreach (var section in sections)
        {
            section.Funnel = _funnelService.Get(section.FunnelId)!;
        }

        return sections;
    }

    /// <summary>
    /// Получить список всех секций выбранной воронки (зачем?)
    /// </summary>
    /// <param name="funnelId">Guid выбранной воронки</param>
    /// <returns>Коллекция всех секций воронки</returns>
    [HttpGet("funnel={funnelId:Guid}")]
    public ICollection<Section> GetForFunnel(Guid funnelId)
    {
        return _funnelService.GetList()
            .SelectMany(f => f.Sections)
            .ToList();
    }

    /// <summary>
    /// Получить секцию по id
    /// </summary>
    /// <param name="id">Guid нужной секции</param>
    /// <returns>Секция или null, если с таким ключом секции нет</returns>
    [HttpGet("{id:Guid}")]
    public Section? Get(Guid id)
    {
        var section = _sectionsService.Get(id);
        section!.Funnel = _funnelService.Get(section.FunnelId)!;
        return section;
    }
    
    /// <summary>
    /// Добавить секцию в воронку
    /// </summary>
    /// <param name="sectionModel">Модель секции, содержащая имя и описание</param>
    /// <param name="funnelId">Guid воронки, в которую нужно добавить секцию</param>
    /// <returns>Имя добавленной секции</returns>
    [HttpPost("forFunnel={funnelId:Guid}")]
    public ActionResult<Section> AddSection(AddSectionModel sectionModel, Guid funnelId)
    {
        var section = _mapper.Map<Section>(sectionModel);
        section.FunnelId = funnelId;
        _sectionsService.Create(section);
        return Ok($"Добавлена секция {section.Name} \nв воронку {funnelId}");
    }

    /// <summary>
    /// Обновить секцию
    /// </summary>
    /// <param name="sectionModel">Модель секции для обновления с заданными именем и описанием</param>
    /// <param name="id">Guid обновляемой секции</param>
    /// <returns></returns>
    [HttpPut("{id:Guid}")]
    public ActionResult<Section> Put(UpdateSectionModel sectionModel, Guid id)
    {
        var section = _sectionsService.Get(id);
        if (section is null)
            return BadRequest("Секции нема");
        UpdateSection(sectionModel, section);
        _sectionsService.Update(section);
        return Ok("Обновлена секция: \n" + section.Name);
    }
    
    /// <summary>
    /// Удалить секцию
    /// </summary>
    /// <param name="id">Guid удаляемой секции</param>
    /// <returns>Уведомит об удалении секции с данным id</returns>
    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {
        _sectionsService.Delete(id);
        return Ok($"Секция {id} удалена");
    }

    private void UpdateSection(UpdateSectionModel sectionModel, Section section)
    {
        section.Name = sectionModel.Name;
        section.Description = sectionModel.Description;
    }
}