using AutoMapper;
using DataAccess.Services;
using Domain.Models;
using Domain.Models.Crm;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.ViewModel;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class BeatsController : Controller
{
    private readonly IService<Beat> _beatService;
    private readonly IService<Section> _sectionService;
    private readonly IService<Funnel> _funnelService;
    private readonly IMapper _mapper;

    public BeatsController(IService<Beat> beatService, IService<Section> sectionService, 
        IService<Funnel> funnelService, IMapper mapper)
    {
        _beatService = beatService;
        _sectionService = sectionService;
        _funnelService = funnelService;
        _mapper = mapper;
    }


    /// <summary>
    /// Получить все биты на сайте
    /// </summary>
    /// <returns>Коллекция всех битов на сайте</returns>
    [HttpGet]
    public ICollection<Beat> Get()
    {
        return _beatService.GetList();
    }

    /// <summary>
    /// Получить все биты воронки
    /// </summary>
    /// <param name="funnelId"></param>
    /// <returns></returns>
    [HttpGet("funnel={funnelId:Guid}")]
    public ICollection<Beat> GetForFunnel(Guid funnelId)
    {
        return _funnelService.Get(funnelId)!
            .Sections
            .SelectMany(s => s.Beats)
            .ToList();
    }
    
    /// <summary>
    /// Получить все биты секции
    /// </summary>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    [HttpGet("section={funnelId:Guid}")]
    public ICollection<Beat> GetForSection(Guid sectionId)
    {
        return _sectionService.Get(sectionId)!
            .Beats
            .ToList();
    }

    /// <summary>
    /// Получить бит по Id
    /// </summary>
    /// <param name="id">Guid бита</param>
    /// <returns>бит или null, если такого бита нет</returns>
    [HttpGet("{id:Guid}")]
    public Beat? Get(Guid id)
    {
        return _beatService.Get(id);
    }
    
    /// <summary>
    /// Добавить бит в секцию (временный контроллер для теста)
    /// </summary>
    /// <param name="beatModel">Модель бита</param>
    /// <param name="sectionId">Guid секции, в которую нужно добавить бит</param>
    /// <returns>Имя добавленной секции</returns>
    [HttpPost("forSection={sectionId:Guid}")]
    public ActionResult<Beat> AddBeat(AddBeatModel beatModel, Guid sectionId)
    {
        var beat = _mapper.Map<Beat>(beatModel);
        beat.SectionId = sectionId;
        _beatService.Create(beat);
        return Ok($"Добавлен бит {beat.Name} \nв секцию {_sectionService.Get(sectionId)!.Funnel.Name}");
    }

    /// <summary>
    /// Обновляет доступные данные бита
    /// </summary>
    /// <param name="beatModel">Модель бита с доступными данными</param>
    /// <param name="id">Guid обновляемого бита</param>
    /// <returns>Сообщит, что бит с таким-то названием обновлен</returns>
    [HttpPut("id:Guid")]
    public ActionResult<Beat> Put(UpdateBeatModel beatModel, Guid id)
    {
        var beat = _beatService.Get(id);
        if (beat is null)
            return BadRequest("Бита нема");
        UpdateSection(beatModel, beat);
        _beatService.Update(beat);
        return Ok("Обновлен бит: \n" + beat.Name);
    }

    /// <summary>
    /// Удалить бит по Id. Тестовый контроллер (не удаляет папку в root)
    /// </summary>
    /// <param name="id">Guid удаляемого бита</param>
    /// <returns>Уведомит об успешном возврате</returns>
    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {
        _beatService.Delete(id);
        return Ok($"Бит {id} удален");
    }

    private void UpdateSection(UpdateBeatModel beatModel, Beat beat)
    {
        beat.Name = beatModel.Name;
        beat.Description = beatModel.Description;
        beat.PriceToBuy = beatModel.PriceToBuy;
        beat.PriceToLease = beatModel.PriceToLease;
        beat.Bpm = beatModel.Bpm;
    }

}