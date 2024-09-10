using csharpwebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharpwebapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TutorialController : ControllerBase
{
    private readonly TutorialContext tutorialContext;

    public TutorialController(TutorialContext context){
        this.tutorialContext = context;
    }

    // GET: api/Tutorial
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tutorial>>> GetTutorials()
    {
        return await tutorialContext.Tutorials
            .Select(x => TutorialToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tutorial>> GetTutorial(int id)
    {
        var tutorial = await tutorialContext.Tutorials.FindAsync(id);

        if (tutorial == null)
        {
            return NotFound();
        }

        return TutorialToDTO(tutorial);
    }

    [HttpPost]
    public async Task<ActionResult<Tutorial>> PostTutorial(Tutorial tutorial)
    {
        var tutorial1 = new Tutorial
        {
            Published = tutorial.Published,
            Title = tutorial.Title,
            Description = tutorial.Description
        };

        tutorialContext.Tutorials.Add(tutorial1);
        await tutorialContext.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTutorials),
            new { id = tutorial.Id },
            TutorialToDTO(tutorial1));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNewTtutotial(int id, Tutorial tutotial)
    {
        if (id != tutotial.Id)
        {
            return BadRequest();
        }

        var newTtutotial = await tutorialContext.Tutorials.FindAsync(id);
        if (newTtutotial == null)
        {
            return NotFound();
        }

        newTtutotial.Title = tutotial.Title;
        newTtutotial.Description = tutotial.Description;
        newTtutotial.Published = tutotial.Published;

        try
        {
            await tutorialContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TutorialExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTutorial(int id)
    {
        var tutorial = await tutorialContext.Tutorials.FindAsync(id);
        if (tutorial == null)
        {
            return NotFound();
        }

        tutorialContext.Tutorials.Remove(tutorial);
        await tutorialContext.SaveChangesAsync();

        return NoContent();
    }

    private bool TutorialExists(int id)
    {
        return tutorialContext.Tutorials.Any(e => e.Id == id);
    }

    private static Tutorial TutorialToDTO(Tutorial tutorial) =>
       new Tutorial
       {
           Id = tutorial.Id,
           Title = tutorial.Title,
           Description = tutorial.Description,
           Published = tutorial.Published,
       };
}